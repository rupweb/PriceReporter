using PriceUI;
using NUnit.Framework;
using System.Threading;
using Shared;

namespace PriceUITests
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

            string direction = new QuoteMaths().direction(q);
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

            QuoteMaths qm = new QuoteMaths();
            qm.setPreviousPrice(q.Price - decimal.Parse("0.01"));
            string direction = qm.direction(q);

            PriceFormTree pf = new PriceFormTree(instrument);
            pf.direction = new System.Windows.Forms.Label();
            pf.direction.Text = direction;

            Assert.That(pf.direction.Text.Equals("+"));
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

            QuoteMaths qm = new QuoteMaths();
            qm.setPreviousPrice(q.Price + decimal.Parse("0.01"));
            string direction = qm.direction(q);

            PriceFormTree pf = new PriceFormTree(instrument);
            pf.direction = new System.Windows.Forms.Label();
            pf.direction.Text = direction;

            Assert.That(pf.direction.Text.Equals("-"));
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

            QuoteMaths qm = new QuoteMaths();
            qm.setPreviousPrice(q.Price);
            string direction = qm.direction(q);

            PriceFormTree pf = new PriceFormTree(instrument);
            pf.direction = new System.Windows.Forms.Label();
            pf.direction.Text = direction;

            Assert.That(pf.direction.Text.Equals("no change"));
        }
    }
}
