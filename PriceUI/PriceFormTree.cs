using Shared;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PriceUI
{
    // This class is a principal form class, holding any type of control

    public partial class PriceFormTree : Form
    {
        Process process = Process.GetCurrentProcess();

        private Thread t1;
        private Task t2;
        private Task t3;
        private Task t4;

        // UI fields
        internal string market = String.Empty;

        [DllImport("user32.dll")]
        public static extern bool LockWindowUpdate(IntPtr hWndLock);

        // A lock for concurrent access to the forms list
        private readonly object syncLock = new object();

        // A decorator class that handles incoming prices
        // public DecoratorChannel decorator;
        public Decorator decorator;

        // Painter classes that each paint a panel
        public PainterPrice painterPrice;
        public PainterAverage painterAverage;
        public PainterHistory painterHistory;

        public PriceFormTree() { }

        public PriceFormTree(string market)
        {
            Logs.Info("In PriceFormTree");

            this.market = market;

            Load += new EventHandler(PriceFormTree_Load);

            Logs.Info("Out PriceFormTree");
        }

        public void PriceFormTree_Load(Object sender, EventArgs e)
        {
            Logs.Info("In PriceFormTree_Load");

            InitializeComponent();
            this.Text = market;
            this.instrument.Text = market;

            // Once form is shown
            this.Shown += new EventHandler(this.PriceFormTree_Shown);

            Logs.Info("Out PriceFormTree_Load");
        }

        private void PriceFormTree_Shown(object sender, EventArgs e)
        {
            Logs.Info("In PriceFormTree_Shown");

            Background();

            Logs.Info("Out PriceFormTree_Shown");
        }

        public void PriceFormTree_Close()
        {
            Logs.Info("In PriceFormTree_Close");

            if (t1.IsAlive)
                t1.Abort();

            lock (syncLock)
                new SplashForm().getPfList().Remove(market);

            Logs.Info("Out PriceFormTree_Close");
        }

        public void Background()
        {
            Logs.Info("In PriceFormTree.Background");

            // decorator = new DecoratorChannel(market);
            decorator = new Decorator(market);
            painterPrice = new PainterPrice(this);
            painterAverage = new PainterAverage(this);
            painterHistory = new PainterHistory(this);

            t1 = new Thread(Pricing);
            t1.Name = "PricingThread";
            t1.Priority = ThreadPriority.Highest;
            t1.Start();

            t2 = Task.Run(PainterPricesAsync);
            t3 = Task.Run(PainterAveragesAsync);
            t4 = Task.Run(PainterHistoriesAsync);

            process.Refresh();

            Logs.Info("Out PriceFormTree.Background");
            // return Task.CompletedTask;
        }

        private void Pricing()
        {
            Logs.Info("Start pricing thread for instrument: " + market);
            decorator.Pricing();
        }

        private async Task PainterPricesAsync()
        {
            Logs.Info("Start paint prices thread for instrument: " + market);
            await painterPrice.StartPainterAsync();
        }

        private async Task PainterAveragesAsync()
        {
            Logs.Info("Start paint averages thread for instrument: " + market);
            await painterAverage.StartPainter();
        }

        private async Task PainterHistoriesAsync()
        {
            Logs.Info("Start paint histories thread for instrument: " + market);
            await painterHistory.StartPainter();
        }
    }
}
