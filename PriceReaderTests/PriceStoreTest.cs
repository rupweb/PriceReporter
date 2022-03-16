using NUnit.Framework;
using PriceReader;
using Shared;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;

namespace PriceReaderTests
{
    [TestFixture]
    internal class PriceStoreTest
    {
        Quote[] quotes = new Quote[600];
        int ThreadSleep = 1000;

        [TestCase("Data.txt")]
        public void PriceStoreOne(string name)
        {
            // Separate thread to stop the service thread after a second
            Thread t = new Thread(RunService);
            t.Start();

            // Subscribe to some quotes
            PriceStore priceStore = new PriceStore();
            priceStore.Store(name);

            // Check the queue for a quote
            Quote q = priceStore.GetLatestQuote();

            Assert.IsNotNull(q);
            Assert.That(q.Name, Is.Not.Null);
            Assert.That(q.Price, Is.Not.Null);
        }

        [TestCase("Data.txt")]
        public void PriceSubscribeMany(string name)
        {
            // Thread to gather 600 quotes
            Thread t = new Thread(Quoter);
            t.Start();

            // Start the subscriber
            PriceStore priceStore = new PriceStore();
            priceStore.Store(name);

            // Wait for the gathering thread
            while (!Program.getInstance().InitComplete)
                Thread.Sleep(100);

            Assert.IsNotNull(quotes);
            Assert.That(quotes[0].Price, Is.Not.Null);
            Assert.That(quotes[599].Price, Is.Not.Null);
        }

        private void Quoter()
        {
            BlockingCollection<Quote> mySub = new PriceStore().GetQuotesCollection();

            int i = 0;          
            while(quotes[599] is null)
            {
                quotes[i] = mySub.Take();
                i++;
            }

            Program.getInstance().Stop();
        }

        private void RunService()
        {
            Thread.Sleep(ThreadSleep);
            Program.getInstance().Stop();
        }
    }
}
