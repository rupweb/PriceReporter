using Shared;
using System;
using System.Configuration;
using System.ServiceProcess;
using System.Threading;

namespace PriceWeb
{
    // This class starts the service

    public static class Program
    {
        private static WebPublisher instance;
        public static WebPublisher getInstance() { return instance; }
        public static void setInstance(WebPublisher publisher) { instance = publisher; }

        public static bool TraceNetworkPrices = Convert.ToBoolean(ConfigurationManager.AppSettings["TraceNetworkPrices"]);
        public static int MaxNetworkPriceQueueSize = Convert.ToInt32(ConfigurationManager.AppSettings["MaxNetworkPriceQueueSize"]);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main()
        {
            Logs.Info("In Main");

#if DEBUG
            instance = new WebPublisher();
            while (instance.IsRunning())
                Thread.Sleep(100);
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                instance = new WebPublisher()
            };
            ServiceBase.Run(ServicesToRun);
#endif

            Logs.Info("Out Main");
        }
    }
}
