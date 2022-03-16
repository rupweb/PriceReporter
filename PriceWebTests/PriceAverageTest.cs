using PriceWeb;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Shared;

namespace PriceWebTests
{
    [TestFixture]
    internal class PriceAverageTest
    {
        [TestCase("BP.L", "1455.95")]
        [TestCase("Dow", "12,965")]
        [TestCase("Oil", "84")]
        [TestCase("BT.L", "1103")]
        [TestCase("VOD.L", "92.3")]
        public void FirstEverAveragesTest(string instrument, string price)
        {
            Quote q = new Quote();
            q.Name = instrument;
            q.Price = decimal.Parse(price);

            string average = new PaintString().SetAverage(q);
            Assert.IsNotEmpty(average);
        }

        [TestCase("BP.L", new string[] { "1455.95", "1455.95", "1455.95", "1455.95", "1455.95", "1455.95", "1455.95" }, 1455.95)]
        [TestCase("Dow", new string[] { "12,965", "12,965", "12,965", "12,965", "12,965", "12,965", "12,965" }, 12965)]
        [TestCase("Oil", new string[] { "84", "85", "86", "87", "88", "89", "90" }, 87.6)]
        [TestCase("BT.L", new string[] { "1103", "1102", "1096", "1098", "1109", "1110" }, 1103.2)]
        [TestCase("VOD.L", new string[] { "92.4", "92.8", "93.2", "93.2", "93.4", "95.5", "94.3", "92.6", "92.1", "91.3", "90.9", "90.8"}, 91.5)]
        public void AveragesMathsTest(string instrument, string[] prices, decimal expected)
        {
            List<Quote> quotes = new List<Quote>();
            foreach (string s in prices)
            {
                Quote q = new Quote();
                q.Name = instrument;
                q.Price = decimal.Parse(s);

                quotes.Add(q);
            }

            PaintString pc = new PaintString();
            quotes.ForEach(x => pc.SetAverage(x));
            string average = pc.SetAverage(quotes[0]);

            Assert.That(average.Equals(expected.ToString()));
        }
    }
}
