using Shared;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.ServiceProcess;
using System.Threading;

namespace PriceWeb
{

    // This class manages the service
    public partial class WebPublisher : ServiceBase
    {
        private Thread serviceThread;
        private UdpListen udpServer;
        private HttpListen httpServer;
        public Boolean IsRunning() { return serviceThread.IsAlive; }

        public static string Host = ConfigurationManager.AppSettings["Host"];
        public static string UdpPort = ConfigurationManager.AppSettings["UdpPort"];
        public static string HttpPort = ConfigurationManager.AppSettings["HttpPort"];
        public static string PricesEndpoint = ConfigurationManager.AppSettings["PricesEndpoint"];


        public const string PROJECT_NAME = "PriceReader";
        public bool InitComplete = false;

        public WebPublisher()
        {
            log4net.Config.XmlConfigurator.Configure();
            Logs.Enter();

            InitializeComponent();
#if DEBUG
            OnStart(null);
#endif

            Logs.Exit();
        }

        protected override void OnStart(string[] args)
        {
            Logs.Enter();

            // The onStart interface needs to return right away but the async event handlers run in the background
            ThreadStart threadDelegate = new ThreadStart(PriceWebProcess);
            serviceThread = new Thread(threadDelegate);
            serviceThread.Name = PROJECT_NAME;
            serviceThread.IsBackground = true;
            serviceThread.Start();

            Program.setInstance(this);

            // Wait for everything to initialize before return
            while (!InitComplete)
                Thread.Sleep(100);

            Logs.Info("Init complete");
        }

        private void PriceWebProcess()
        {
            Logs.Info("Starting " + PROJECT_NAME);
            new UpdateServiceStatus().StartPending(ServiceHandle);

            InitEventLog();

            try
            {
                EventLog.WriteEntry(PROJECT_NAME + " was started", EventLogEntryType.Information);
                Logs.Info("Event Log Initiated");
            }
            catch (SecurityException)
            {
                Logs.Warn(PROJECT_NAME + " event log not accessible. Either run as admin or use powershell to set up event logs");
            }

            // Setup a Udp listener to publish to ring buffer
            Logs.Info("Starting Udp");
            udpServer = new UdpListen();
            udpServer.RunServer();

            // Setup an Http publisher that also handles ring buffer subscriptions
            Logs.Info("Starting Http");
            httpServer = new HttpListen();
            httpServer.RunServer();

            Logs.Exit();
        }

        public void InitEventLog()
        {
            EventLog.Source = PROJECT_NAME + "Source";

            try
            {
                EventLog.WriteEntry("Starting " + PROJECT_NAME, EventLogEntryType.Information);
            }
            catch (Exception e)
            {
                Logs.Error("Can't write " + PROJECT_NAME + " event log", e);
            }
            finally
            {
                Logs.Info("Process id: " + Process.GetCurrentProcess().Id);
                Logs.Info("Service dir: " + Directory.GetCurrentDirectory());
                Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
                Logs.Info("New current dir: " + Directory.GetCurrentDirectory());
            }
        }

        protected override void OnStop()
        {

            Logs.Enter();
            EventLog.WriteEntry("Stopping " + PROJECT_NAME, EventLogEntryType.Information);
            Logs.Info("Process id: " + Process.GetCurrentProcess().Id);

            // Stop Http listener thread
            if (udpServer != null)
                if (udpServer.getInstance().GetThread().IsAlive)
                    udpServer.getInstance().GetThread().Abort();

            try
            {
                if (serviceThread != null)
                {
                    if (!serviceThread.Join(1000))
                        serviceThread.Abort();
                }
            }
            catch (Exception e)
            {
                Logs.Error("Failed to stop: ", e);
            }

            Logs.Exit();
        }
    }
}
