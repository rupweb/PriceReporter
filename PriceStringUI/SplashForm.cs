using Shared;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace PriceUI
{
    // This class manages all the forms

    public partial class SplashForm : Form
    {
        private string market;
        private static SplashForm instance;
        public SplashForm GetInstance() { return instance; }

        // A list of price forms, of any type, to ensure only one form can be opened per instrument    
        private static List<String> pfList = new List<String>();
        public List<String> getPfList() { return pfList; }

        // A lock for concurrent access to the list
        private readonly object syncLock = new object();

        public SplashForm() { }

        public SplashForm(string market)
        {
            Logs.Enter();

            this.market = market;
            Load += new EventHandler(SplashForm_Load);

            instance = this;

            Logs.Exit();
        }

        private void SplashForm_Load(Object sender, EventArgs e)
        {
            Logs.Info("In SplashForm_Load");

            InitializeComponent();

            new InstrumentList(market);

            // Once form is shown
            this.Shown += new System.EventHandler(this.SplashForm_Shown);

            Logs.Info("Out SplashForm_Load");
        }

        private void SplashForm_Shown(object sender, EventArgs e)
        {
            Logs.Info("In SplashForm_Shown");

            Refresh(); // Display what we have so far

            Background();

            Logs.Info("Out SplashForm_Shown");
        }

        private void Background()
        {
            Logs.Info("In Background");

            UdpListen ul = new UdpListen();
            ul.RunServer();

            Logs.Info("Out Background");
        }

        private void PriceForm_New(object sender, EventArgs e)
        {
            Logs.Info("In PriceForm_New");

            Instrument i = (Instrument)instruments.SelectedItem;
            market = i.Name;

            ThreadStart threadDelegate = new ThreadStart(RunPriceFormString);
            Thread t = new Thread(threadDelegate);
            t.Start();

            Logs.Info("Out PriceForm_New");
        }

        private void RunPriceFormString()
        {
            Logs.Info("In RunPriceFormString");

            if (!new PriceEvent().getExists(market))
            {
                MessageBox.Show("No prices for " + market + ". Is the PriceReader service on?");
                Logs.Info("No prices for " + market + ". Is the PriceReader service on?");
                return;
            }

            lock (syncLock)
                if (pfList.Contains(market))
                    return;
                else pfList.Add(market);

            PriceFormString pf = new PriceFormString(market);
            pf.ShowDialog();
            // The show dialog blocks until form is closed

            pf.PriceFormString_Close();
           
            Logs.Info("Out RunPriceForm");
        }

        private void SplashForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Logs.Info("In SplashForm_FormClosed");

            // Down any open forms
            Environment.Exit(0);
        }
    }
}
