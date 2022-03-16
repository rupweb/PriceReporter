using PriceUI;
using NUnit.Framework;
using Shared;

namespace PriceUITests
{
    [TestFixture]
    class BasicTest
    {
        bool manual = false;
        
        [TestCase("EUR", "DOW", "OIL")]
        [TestCase("USD", "BP.L", "VOD.L")]
        [TestCase("WRONG", "OIL", "ZN")]
        [TestCase("123", "234", "345")]
        public void SplashFormTest(string instrument1, string instrument2, string instrument3)
        {
            SplashForm sf = new SplashForm("test");

            // Test the data binding
            Instrument i1 = new Instrument(instrument1);
            Instrument i2 = new Instrument(instrument2);
            Instrument i3 = new Instrument(instrument3);

            InstrumentList il = new InstrumentList().getInstance();
            if (!il.InstrumentNames(i1)) il.Add(i1);
            if (!il.InstrumentNames(i2)) il.Add(i2);
            if (!il.InstrumentNames(i3)) il.Add(i3);

            // Blocking call to UI
            if (manual)
                sf.ShowDialog();
        }

        [TestCase("EUR")]
        [TestCase("USD")]
        [TestCase("WRONG")]
        [TestCase("123")]
        public void PriceFormTest(string instrument)
        {
            PriceFormTree pf = new PriceFormTree(instrument);

            pf.instrument = new System.Windows.Forms.Label();
            pf.instrument.Text = instrument;

            Assert.That(instrument == pf.instrument.Text);
        }
    }
}
