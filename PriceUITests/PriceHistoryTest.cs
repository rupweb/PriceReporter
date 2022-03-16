﻿using PriceUI;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Windows.Forms;
using System.Threading;
using Shared;

namespace PriceUITests
{
    [TestFixture]
    internal class PriceHistoryTest
    {
        List<TreeNode> myNodes = new List<TreeNode>();

        [TestCase("BP.L", "1455.95")]
        [TestCase("Dow", "12,965")]
        [TestCase("Oil", "84")]
        [TestCase("BT.L", "1103")]
        [TestCase("VOD.L", "93.0")]
        public void FirstEverHistoryTest(string instrument, string price)
        {
            Quote q = new Quote();
            q.Name = instrument;
            q.Price = decimal.Parse(price);

            myNodes = new Paint().SetHistory(q);
            Assert.Greater(myNodes.Count, 0);
        }

        [TestCase("BP.L", new string[] { "1455.95", "1455.95", "1455.95", "1455.95", "1455.95", "1455.95", "1455.95", "1455.95", "1455.95", "1455.95", "1455.95", "1455.95" })]
        [TestCase("Dow", new string[] { "12,965", "12,966", "12,967", "12,968", "12,969", "12,970", "12,971", "12,972", "12,973", "12,974" })]
        [TestCase("Oil", new string[] { "84", "85", "86", "87", "88", "89", "90", "88", "89", "90", "91", "92" })]
        [TestCase("BT.L", new string[] { "1103", "1102", "1096", "1098", "1109", "1110", "1103", "1102", "1096", "1098", "1109", "1110", "1103", "1102", "1096", "1098", "1109", "1110" })]
        [TestCase("VOD.L", new string[] { "93.0", "93.1", "93.2", "93.3", "93.4", "93.5", "93.6", "93.7", "93.8", "93.9", "94.0", "94.1" })]
        public void RunningHistoryTest(string instrument, string[] prices)
        {
            List<Quote> quotes = new List<Quote>();
            foreach (string s in prices)
            {
                Quote q = new Quote();
                q.Name = instrument;
                q.Price = decimal.Parse(s);

                quotes.Add(q);
            }

            Paint set = new Paint();
            List<TreeNode> testList = new List<TreeNode>();

            foreach (Quote q in quotes)
            {
                testList = set.SetHistory(q);
            }

            int len = quotes.Count;

            // Assert quotes are displayed with latest quote at the top
            Assert.That(testList[0].Text.Equals(quotes[len - 1].Price.ToString()));
            Assert.That(testList[1].Text.Equals(quotes[len - 2].Price.ToString()));
            Assert.That(testList[2].Text.Equals(quotes[len - 3].Price.ToString()));
            Assert.That(testList[3].Text.Equals(quotes[len - 4].Price.ToString()));
            Assert.That(testList[4].Text.Equals(quotes[len - 5].Price.ToString()));
            Assert.That(testList[5].Text.Equals(quotes[len - 6].Price.ToString()));
            Assert.That(testList[6].Text.Equals(quotes[len - 7].Price.ToString()));
            Assert.That(testList[7].Text.Equals(quotes[len - 8].Price.ToString()));
            Assert.That(testList[8].Text.Equals(quotes[len - 9].Price.ToString()));
            Assert.That(testList[9].Text.Equals(quotes[len - 10].Price.ToString()));

            // Assert no more than 10 quotes returned
            Assert.That(testList.Count == 10);
        }

        [TestCase("VOD.L", new string[] { "93.0" })]
        public void HistoryTestOneQuote(string instrument, string[] prices)
        {
            List<Quote> quotes = new List<Quote>();
            foreach (string s in prices)
            {
                Quote q = new Quote();
                q.Name = instrument;
                q.Price = decimal.Parse(s);

                quotes.Add(q);
            }

            Paint set = new Paint();
            List<TreeNode> testList = new List<TreeNode>();

            foreach (Quote q in quotes)
            {
                testList = set.SetHistory(q);
            }

            Assert.That(testList[0].Text.Equals(quotes[0].Price.ToString()));
        }

        [TestCase("VOD.L", new string[] { "93.0", "93.1" })]
        public void HistoryTestTwoQuote(string instrument, string[] prices)
        {
            List<Quote> quotes = new List<Quote>();
            foreach (string s in prices)
            {
                Quote q = new Quote();
                q.Name = instrument;
                q.Price = decimal.Parse(s);

                quotes.Add(q);
            }

            Paint set = new Paint();
            List<TreeNode> testList = new List<TreeNode>();

            foreach (Quote q in quotes)
            {
                testList = set.SetHistory(q);
            }

            // Assert quotes are displayed with latest quote at the top
            Assert.That(testList[0].Text.Equals(quotes[1].Price.ToString()));
            Assert.That(testList[1].Text.Equals(quotes[0].Price.ToString()));
        }

        [TestCase("VOD.L", new string[] { "93.0", "93.1", "93.2", "93.3", "93.4" })]
        public void HistoryTestFiveQuote(string instrument, string[] prices)
        {
            List<Quote> quotes = new List<Quote>();
            foreach (string s in prices)
            {
                Quote q = new Quote();
                q.Name = instrument;
                q.Price = decimal.Parse(s);

                quotes.Add(q);
            }

            Paint set = new Paint();
            List<TreeNode> testList = new List<TreeNode>();

            foreach (Quote q in quotes)
            {
                testList = set.SetHistory(q);
            }

            // Assert quotes are displayed with latest quote at the top
            Assert.That(testList[0].Text.Equals(quotes[4].Price.ToString()));
            Assert.That(testList[1].Text.Equals(quotes[3].Price.ToString()));
            Assert.That(testList[2].Text.Equals(quotes[2].Price.ToString()));
            Assert.That(testList[3].Text.Equals(quotes[1].Price.ToString()));
            Assert.That(testList[4].Text.Equals(quotes[0].Price.ToString()));
        }
    }
}
