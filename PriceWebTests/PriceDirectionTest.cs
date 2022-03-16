using PriceWeb;
using NUnit.Framework;
using Shared;
using System.Collections.Concurrent;

namespace PriceWebTests
{
    [TestFixture]
    internal class PriceDirectionTest
    {
        [TestCase("BP.L", "1455.95")]
        [TestCase("Dow", "12,965")]
        [TestCase("Oil", "84")]
        [TestCase("BT.L", "1103")]
        [TestCase("VOD.L", "92.3")]
        public void FirstEverPriceTest(string instrument, string price)
        {
            Quote q = new Quote();
            q.Name = instrument;
            q.Price = decimal.Parse(price);

            string direction = new PaintString().SetDirection(q);
            Assert.AreEqual("+", direction);
        }

        [TestCase("BP.L", "1455.95")]
        [TestCase("Dow", "12,965")]
        [TestCase("Oil", "84")]
        [TestCase("BT.L", "1103")]
        [TestCase("VOD.L", "92.3")]
        public void UptickPriceTest(string instrument, string price)
        {
            Quote q = new Quote();
            q.Name = instrument;
            q.Price = decimal.Parse(price);

            PriceEvent pe = new PriceEvent();
            pe.OnEvent(q, 1, false);

            Quote q2 = new Quote();
            q2.Name = q.Name;
            q2.Price = q.Price + decimal.Parse("0.01");
            pe.OnEvent(q2, 1, false);

            ConcurrentDictionary<string, PaintString> paintStrings = pe.getMarkets();

            Assert.That(paintStrings[instrument].newDirection.Equals("+"));
        }

        [TestCase("BP.L", "1455.95")]
        [TestCase("Dow", "12,965")]
        [TestCase("Oil", "84")]
        [TestCase("BT.L", "1103")]
        [TestCase("VOD.L", "92.3")]
        public void DowntickPriceTest(string instrument, string price)
        {
            Quote q = new Quote();
            q.Name = instrument;
            q.Price = decimal.Parse(price);

            PriceEvent pe = new PriceEvent();
            pe.OnEvent(q, 1, false);

            Quote q2 = new Quote();
            q2.Name = q.Name;
            q2.Price = q.Price - decimal.Parse("0.01");
            pe.OnEvent(q2, 1, false);

            ConcurrentDictionary<string, PaintString> paintStrings = pe.getMarkets();

            Assert.That(paintStrings[instrument].newDirection.Equals("-"));
        }

        [TestCase("BP.L", "1455.95")]
        [TestCase("Dow", "12,965")]
        [TestCase("Oil", "84")]
        [TestCase("BT.L", "1103")]
        [TestCase("VOD.L", "92.3")]
        public void NoChangePriceTest(string instrument, string price)
        {
            Quote q = new Quote();
            q.Name = instrument;
            q.Price = decimal.Parse(price);

            PriceEvent pe = new PriceEvent();
            pe.OnEvent(q, 1, false);

            Quote q2 = new Quote();
            q2.Name = q.Name;
            q2.Price = q.Price;

            pe.OnEvent(q2, 1, false);

            ConcurrentDictionary<string, PaintString> paintStrings = pe.getMarkets();

            Assert.That(paintStrings[instrument].newDirection.Equals("no change"));
        }
    }
}
