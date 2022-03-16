using PriceUI;
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
    internal class PriceHistoryPaintingTest
    {
        List<TreeNode> myNodes = new List<TreeNode>();

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

            PriceFormTree pf = InitTestForm(instrument);
            pf.decorator = new Decorator(instrument);

            foreach (Quote q in quotes)
            {
                pf.decorator.ManagePrice(q);
            }

            PainterHistory painter = new PainterHistory(pf);
            PaintStack ps = pf.decorator.getDecoratorHistory();

            while (ps.Length() > 0)
            {
                Paint ui = ps.Take();
                painter.Painting(ui);
            }

            int len = quotes.Count;

            // Assert quotes are displayed with latest quote at the top
            Assert.That(pf.historyTree.TopNode.Nodes[0].Text.Equals(quotes[len - 1].Price.ToString()));
            Assert.That(pf.historyTree.TopNode.Nodes[1].Text.Equals(quotes[len - 2].Price.ToString()));
            Assert.That(pf.historyTree.TopNode.Nodes[2].Text.Equals(quotes[len - 3].Price.ToString()));
            Assert.That(pf.historyTree.TopNode.Nodes[3].Text.Equals(quotes[len - 4].Price.ToString()));
            Assert.That(pf.historyTree.TopNode.Nodes[4].Text.Equals(quotes[len - 5].Price.ToString()));
            Assert.That(pf.historyTree.TopNode.Nodes[5].Text.Equals(quotes[len - 6].Price.ToString()));
            Assert.That(pf.historyTree.TopNode.Nodes[6].Text.Equals(quotes[len - 7].Price.ToString()));
            Assert.That(pf.historyTree.TopNode.Nodes[7].Text.Equals(quotes[len - 8].Price.ToString()));
            Assert.That(pf.historyTree.TopNode.Nodes[8].Text.Equals(quotes[len - 9].Price.ToString()));
            Assert.That(pf.historyTree.TopNode.Nodes[9].Text.Equals(quotes[len - 10].Price.ToString()));

            // Assert no more than 10 quotes displayed
            Assert.That(pf.historyTree.TopNode.Nodes.Count == 10);
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

            PriceFormTree pf = InitTestForm(instrument);

            pf.decorator = new Decorator(instrument);
            pf.decorator.ManagePrice(quotes[0]);
            PainterHistory painter = new PainterHistory(pf);

            // Blocking call to the painting stack
            Paint ui = pf.decorator.getDecoratorHistory().Take();
            painter.Painting(ui);

            Assert.That(pf.historyTree.TopNode.Nodes[0].Text.Equals(quotes[0].Price.ToString()));
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

            PriceFormTree pf = InitTestForm(instrument);
            pf.decorator = new Decorator(instrument);

            foreach (Quote q in quotes)
            {
                pf.decorator.ManagePrice(q);
            }

            PainterHistory painter = new PainterHistory(pf);
            PaintStack ps = pf.decorator.getDecoratorHistory();

            while (ps.Length() > 0)
            {
                Paint ui = ps.Take();
                painter.Painting(ui);
            }

            // Assert quotes are displayed with latest quote at the top
            Assert.That(pf.historyTree.TopNode.Nodes[0].Text.Equals(quotes[1].Price.ToString()));
            Assert.That(pf.historyTree.TopNode.Nodes[1].Text.Equals(quotes[0].Price.ToString()));
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

            PriceFormTree pf = InitTestForm(instrument);
            pf.decorator = new Decorator(instrument);

            foreach (Quote q in quotes)
            {
                pf.decorator.ManagePrice(q);
            }

            PainterHistory painter = new PainterHistory(pf);
            PaintStack ps = pf.decorator.getDecoratorHistory();

            while (ps.Length() > 0)
            {
                Paint ui = ps.Take();
                painter.Painting(ui);
            }

            // Assert quotes are displayed with latest quote at the top
            Assert.That(pf.historyTree.TopNode.Nodes[0].Text.Equals(quotes[4].Price.ToString()));
            Assert.That(pf.historyTree.TopNode.Nodes[1].Text.Equals(quotes[3].Price.ToString()));
            Assert.That(pf.historyTree.TopNode.Nodes[2].Text.Equals(quotes[2].Price.ToString()));
            Assert.That(pf.historyTree.TopNode.Nodes[3].Text.Equals(quotes[1].Price.ToString()));
            Assert.That(pf.historyTree.TopNode.Nodes[4].Text.Equals(quotes[0].Price.ToString()));
        }

        private PriceFormTree InitTestForm(string instrument)
        {
            PriceFormTree pf = new PriceFormTree(instrument);
            pf.price = new Label();
            pf.direction = new Label();
            pf.average = new Label();

            pf.historyTree = new BufferedTreeView();
            TreeNode treeNode1 = new TreeNode("Node0");
            pf.historyTree.Nodes.AddRange(new TreeNode[] { treeNode1 });

            // A treeview requires further initialisation
            pf.historyTree.TopNode = treeNode1;

            // Ensure invoke handle created
            _ = pf.historyTree.Handle;

            return pf;
        }
    }
}
