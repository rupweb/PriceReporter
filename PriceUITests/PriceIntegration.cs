using PriceUI;
using NUnit.Framework;
using Shared;

namespace PriceUITests
{
    [TestFixture]
    internal class PriceIntegration
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
            _ = pf.price.Handle; // Ensure invoke price handle 
            pf.decorator = new Decorator(instrument);
            pf.decorator.ManagePrice(q);

            PainterPrice painter = new PainterPrice(pf);

            // Blocking call to the painting stack
            Paint ui = pf.decorator.getDecoratorPrice().Take();
            painter.Painting(ui);

            Assert.That(q.Price.ToString() == pf.price.Text);
        }
    }
}
