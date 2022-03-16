using PriceWeb;
using NUnit.Framework;
using System.Threading;
using System.Configuration;
using Shared;
using System.Collections.Concurrent;

namespace PriceWebTests
{
    [TestFixture]
    internal class PriceCurrentTest
    {
        [TestCase("BP.L", "1455.95")]
        [TestCase("Dow", "12,965")]
        [TestCase("Oil", "84")]
        [TestCase("BT.L", "1103")]
        [TestCase("VOD.L", "92.3")]
        public void PriceCalcTest(string instrument, string price)
        {
            Quote q = new Quote();
            q.Name = instrument;
            q.Price = decimal.Parse(price);

            string newPrice = new PaintString().SetPrice(q);

            Assert.AreEqual(q.Price.ToString(), newPrice);
        }

        [TestCase("BP.L", "1455.95")]
        [TestCase("Dow", "12,965")]
        [TestCase("Oil", "84")]
        [TestCase("BT.L", "1103")]
        [TestCase("VOD.L", "92.3")]
        public void PriceEventTest(string instrument, string price)
        {
            Quote q = new Quote();
            q.Name = instrument;
            q.Price = decimal.Parse(price);

            PriceEvent pe = new PriceEvent();
            pe.OnEvent(q, 1, false);
            pe.OnEvent(q, 1, false);
            pe.OnEvent(q, 1, false);
            pe.OnEvent(q, 1, false);

            ConcurrentDictionary<string, PaintString> paintStrings = pe.getMarkets();

            Assert.That(paintStrings[instrument].newPrice.Equals(q.Price.ToString()));
        }
    }
}
