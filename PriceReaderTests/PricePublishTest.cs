using NUnit.Framework;
using PriceReader;
using Shared;
using System.Linq;
using System.Threading;

namespace PriceReaderTests
{
    [TestFixture]
    internal class PricePublishTest
    {
        PriceSubscribe priceSub = new PriceSubscribe();
        int ThreadSleep = 10000;

        [TestCase("Data.txt")]
        public void PriceRun(string name)
        {
            // A Udp listener is setup by the service if internal listen config is on
            UdpListen listen = new UdpListen();
            while (null == listen.getListener())
                Thread.Sleep(100);
            
            // Thread to produce quotes
            Thread t = new Thread(Quoter);
            t.Start();

            // Start the subscriber
            priceSub.Subscribe(name, 100);

            // Don't wait for the price thread
            // Is there anything on the endpoint
            PriceStore store = new PriceStore();
            Quote q = store.GetLatestQuote();

            Assert.IsNotNull(q);
            Assert.That(q.Name, Is.Not.Null);
            Assert.That(q.Price, Is.Not.Null);
        }

        private void Quoter()
        {
            Thread.Sleep(3000); // Wait for buffer to start
            Thread.Sleep(ThreadSleep); // Wait for prices
            Program.getInstance().Stop(); // Stop
        }

    }
}
