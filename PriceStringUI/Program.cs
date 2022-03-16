using Shared;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace PriceUI
{
    // This class starts the service

    public static class Program
    {
        public static string PricesEndpoint = ConfigurationManager.AppSettings["PricesEndpoint"];
        public static string Market = ConfigurationManager.AppSettings["Market"];
        public static string Host = ConfigurationManager.AppSettings["Host"];
        public static string Port = ConfigurationManager.AppSettings["Port"];
        public static string HttpOrUdp = ConfigurationManager.AppSettings["HttpOrUdp"];
        public static int MaxNetworkPriceQueueSize = Convert.ToInt32(ConfigurationManager.AppSettings["MaxNetworkPriceQueueSize"]);
        public static int PaintingBufferDelay = Convert.ToInt32(ConfigurationManager.AppSettings["PaintingBufferDelay"]);
        public static bool TraceNetworkPrices = Convert.ToBoolean(ConfigurationManager.AppSettings["TraceNetworkPrices"]);
        public static bool TraceNetworkQueues = Convert.ToBoolean(ConfigurationManager.AppSettings["TraceNetworkQueues"]);
        public static int MaxPaintQueueSize = Convert.ToInt32(ConfigurationManager.AppSettings["MaxPaintQueueSize"]);
        public static bool ForceUIRepaint = Convert.ToBoolean(ConfigurationManager.AppSettings["ForceUIRepaint"]);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Logs.Info("In Main");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SplashForm(Market));

            Logs.Info("Out Main");
        }
    }
}
