using Shared;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PriceUI
{
    // This class handles a form composed of nothing but strings, panels and labels

    public partial class PriceFormString : Form
    {
        private Thread t1;
        private Thread t2;
        private Thread t3;
        private Thread t4;

        // UI fields
        internal string market = String.Empty;

        // A lock for concurrent access to the forms list
        private readonly object syncLock = new object();

        // A decorator class that handles incoming prices
        internal DecoratorString decorator;

        // Painter classes that each paint a panel
        public PainterPriceString painterPrice;
        public PainterAverageString painterAverage;
        public PainterHistoryString painterHistory;

        public PriceFormString() { }

        public PriceFormString(string market)
        {
            Logs.Info("In PriceFormString");

            this.market = market;

            Load += new EventHandler(PriceFormString_Load);

            Logs.Info("Out PriceFormString");
        }

        public void PriceFormString_Load(Object sender, EventArgs e)
        {
            Logs.Info("In PriceFormString_Load");

            InitializeComponent();
            this.Text = market;
            this.instrument.Text = market;

            // Once form is shown
            this.Shown += new EventHandler(this.PriceFormString_Shown);

            Logs.Info("Out PriceFormString_Load");
        }

        private async void PriceFormString_Shown(object sender, EventArgs e)
        {
            Logs.Info("In PriceFormString_Shown");

            await Background();

            Logs.Info("Out PriceFormString_Shown");
        }

        public void PriceFormString_Close()
        {
            Logs.Info("In PriceFormString_Close");

            if (t1.IsAlive)
                t1.Abort();

            if (t2.IsAlive)
                t2.Abort();

            if (t3.IsAlive)
                t3.Abort();

            if (t4.IsAlive)
                t4.Abort();

            lock (syncLock)
                new SplashForm().getPfList().Remove(market);

            Logs.Info("Out PriceFormString_Close");
        }

        public Task Background()
        {
            Logs.Info("In PriceFormString.Background");

            decorator = new DecoratorString(market);
            painterPrice = new PainterPriceString(this);
            painterAverage = new PainterAverageString(this);
            painterHistory = new PainterHistoryString(this);

            t1 = new Thread(Pricing);
            t1.Start();

            t2 = new Thread(PainterPrices);
            t2.Start();

            t3 = new Thread(PainterAverages);
            t3.Start();

            t4 = new Thread(PainterHistories);
            t4.Start();

            Logs.Info("Out PriceFormString.Background");
            return Task.CompletedTask;
        }

        private void Pricing()
        {
            Logs.Info("Start pricing queue for instrument: " + market);
            decorator.Pricing();
        }

        private void PainterPrices()
        {
            Logs.Info("Start paint prices thread for instrument: " + market);
            painterPrice.StartPainter();
        }

        private void PainterAverages()
        {
            Logs.Info("Start paint averages thread for instrument: " + market);
            painterAverage.StartPainter();
        }

        private void PainterHistories()
        {
            Logs.Info("Start paint histories thread for instrument: " + market);
            painterHistory.StartPainter();
        }
    }
}
