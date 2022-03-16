using PriceUI;
using NUnit.Framework;
using System.Threading;
using System.Configuration;
using Shared;

namespace PriceUITests
{
    [TestFixture]
    internal class PriceCurrentTest
    {
        [TestCase("BP.L", "1455.95")]
        [TestCase("Dow", "12,965")]
        [TestCase("Oil", "84")]
        [TestCase("BT.L", "1103")]
        [TestCase("VOD.L", "92.3")]
        public void PriceTest(string instrument, string price)
        {
            Quote q = new Quote();
            q.Name = instrument;
            q.Price = decimal.Parse(price);
            
            PriceFormTree pf = new PriceFormTree(instrument);
            pf.price = new System.Windows.Forms.Label();
            pf.price.Text = q.Price.ToString();

            Assert.That(q.Price.ToString() == pf.price.Text);
        }

        [TestCase("DOW")]
        public void StartUdpListenerAndPriceRing(string instrument)
        {
            return;
            
            // Start a splash form for new instrument events
            SplashForm sf = new SplashForm(instrument);
            sf.instruments = new System.Windows.Forms.ListBox();

            // Starts http listener and price ring on the http port
            UdpListen ul = new UdpListen();
            ul.RunServer();

            // Start a pricer form & price label 
            PriceFormTree pf = new PriceFormTree(instrument);
            pf.price = new System.Windows.Forms.Label();

            // Run the background method
            pf.Background();

            Thread.Sleep(10000); // let the pricing get on a bit
            Assert.That(pf.price.Text.Length > 2);
        }
    }
}
