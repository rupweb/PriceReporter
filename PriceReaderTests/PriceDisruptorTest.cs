using NUnit.Framework;
using PriceReader;
using Disruptor;
using System.Threading;
using Shared;

namespace PriceReaderTests
{
    [TestFixture]
    internal class PriceDisruptorTest
    {
        PriceSubscribe priceSub = new PriceSubscribe();
        Quote[] quotes = new Quote[600];
        int ThreadSleep = 5000;

        [TestCase("Data.txt")]
        public void PriceSubscribeOne(string name)
        {
            // Separate thread to stop the service thread after a second
            Thread t = new Thread(RunService);
            t.Start();

            // Subscribe to some quotes
            priceSub.Subscribe(name, Setup.DataFileRate);

            // Wait for the init
            while (!Program.getInstance().InitComplete)
                Thread.Sleep(100);

            // Check the ring for a quote
            Quote q = priceSub.getRing()[0];

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
            priceSub.Subscribe(name, Setup.DataFileRate);

            // Wait for the init
            while (!Program.getInstance().InitComplete)
                Thread.Sleep(100);

            Assert.IsNotNull(quotes);
            Assert.That(quotes[0].Price, Is.Not.Null);
            Assert.That(quotes[599].Price, Is.Not.Null);
        }

        private void Quoter()
        {
            Thread.Sleep(3000); // Wait for buffer to start
            
            int i = 0;  
            while(quotes[599] is null)
            {
                quotes[i] = priceSub.getRing()[i];
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
