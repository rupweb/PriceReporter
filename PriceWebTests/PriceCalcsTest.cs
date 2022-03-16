using PriceWeb;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Shared;
using System.Collections.Concurrent;

namespace PriceWebTests
{
    [TestFixture]
    internal class PriceCalcsTest
    {
        [TestCase("BP.L", "1455.95")]
        [TestCase("Dow", "12,965")]
        [TestCase("Oil", "84")]
        [TestCase("BT.L", "1103")]
        [TestCase("VOD.L", "92.3")]
        public void FirstEverPriceCalcTest(string instrument, string price)
        {
            Quote q = new Quote();
            q.Name = instrument;
            q.Price = decimal.Parse(price);

            PriceEvent pe = new PriceEvent();
            pe.OnEvent(q, 1, false);
            ConcurrentDictionary<string, PaintString> markets = pe.getMarkets();

            Assert.IsNotEmpty(markets);
        }

        [TestCase("BP.L", new string[] { "1455.95", "1455.95", "1455.95", "1455.95", "1455.95", "1455.95", "1455.95" }, 1455.95)]
        [TestCase("Dow", new string[] { "12,965", "12,965", "12,965", "12,965", "12,965", "12,965", "12,965" }, 12965)]
        [TestCase("Oil", new string[] { "84", "85", "86", "87", "88", "89", "90" }, 87.6)]
        [TestCase("BT.L", new string[] { "1103", "1102", "1096", "1098", "1109", "1110" }, 1103.2)]
        [TestCase("VOD.L", new string[] { "92.4", "92.8", "93.2", "93.2", "93.4", "95.5", "94.3", "92.6", "92.1", "91.3", "90.9", "90.8" }, 91.5)]
        public void PriceCalcsMathsTest(string instrument, string[] prices, decimal expected)
        {
            List<Quote> quotes = new List<Quote>();
            foreach (string s in prices)
            {
                Quote q = new Quote();
                q.Name = instrument;
                q.Price = decimal.Parse(s);

                quotes.Add(q);
            }

            PriceEvent pe = new PriceEvent();
            quotes.ForEach(x => pe.OnEvent(x, 1, false));

            ConcurrentDictionary<string, PaintString> markets = pe.getMarkets();

            Assert.That(markets[instrument].SetPrice(quotes[0]).Equals(quotes[0].Price.ToString()));
            Assert.That(markets[instrument].SetAverage(quotes[0]).Equals(expected.ToString()));

            List<String> myHistory = new List<String>();
            myHistory = markets[instrument].SetHistory(quotes[0]);
            Assert.That(myHistory[0].Equals(quotes[0].Price.ToString()));
        }
    }
}
