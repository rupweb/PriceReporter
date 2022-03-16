using PriceUI;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Shared;

namespace PriceUITests
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

            string average = new QuoteMaths().average5(q);
            Assert.IsNotEmpty(average);
        }

        [TestCase("BP.L", new string[] { "1455.95", "1455.95", "1455.95", "1455.95", "1455.95", "1455.95", "1455.95"})]
        [TestCase("Dow", new string[] { "12,965", "12,965", "12,965", "12,965", "12,965", "12,965", "12,965" })]
        [TestCase("Oil", new string[] { "84", "85", "86", "87", "88", "89", "90" })]
        [TestCase("BT.L", new string[] { "1103", "1102", "1096", "1098", "1109", "1110" })]
        [TestCase("VOD.L", new string[] { "92.4", "92.8", "93.2", "93.2", "93.4", "95.5", "94.3", "92.6", "92.1", "91.3", "90.9", "90.8"})]
        public void RunningAveragesTest(string instrument, string[] prices)
        {
            List<Quote> quotes = new List<Quote>();
            foreach (string s in prices)
            {
                Quote q = new Quote();
                q.Name = instrument;
                q.Price = decimal.Parse(s);

                quotes.Add(q);
            }

            QuoteMaths qm = new QuoteMaths();
            quotes.ForEach(x => qm.average5(x));

            string average = qm.average5(quotes[0]);

            PriceFormTree pf = new PriceFormTree(instrument);
            pf.average = new System.Windows.Forms.Label();
            pf.average.Text = average;

            Assert.That(pf.average.Text.Length > 0);
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

            QuoteMaths qm = new QuoteMaths();
            quotes.ForEach(x => qm.average5(x));

            string average = qm.average5(quotes[0]);

            PriceFormTree pf = new PriceFormTree(instrument);
            pf.average = new System.Windows.Forms.Label();
            pf.average.Text = average;

            Assert.That(pf.average.Text.Equals(expected.ToString()));
        }
    }
}
