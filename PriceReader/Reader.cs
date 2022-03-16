using Shared;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.ServiceProcess;
using System.Threading;

namespace PriceReader
{
    // This class manages the service

    public partial class Reader : ServiceBase
    {
        private Thread serviceThread;
        private PriceSubscribe priceSub;
        private UdpListen udpServer;
        public Boolean IsRunning() { return serviceThread.IsAlive; }

        public static string NewGuid() { return Guid.NewGuid().ToString().Substring(0, 8).ToUpper(); }
        public static string PricesEndpoint = ConfigurationManager.AppSettings["PricesEndpoint"];
        public static string Host = ConfigurationManager.AppSettings["Host"];
        public static string ExternalUdpPort = ConfigurationManager.AppSettings["ExternalUdpPort"];
        public static bool sendInternalMessage = Convert.ToBoolean(ConfigurationManager.AppSettings["SendInternalMessage"]);
        public static bool runInternalListener = Convert.ToBoolean(ConfigurationManager.AppSettings["RunInternalListener"]);
        public static string InternalUdpPort = ConfigurationManager.AppSettings["InternalUdpPort"];
        public static string DataFileName = ConfigurationManager.AppSettings["DataFileName"];
        public static int DataFileRate = Convert.ToInt32(ConfigurationManager.AppSettings["DataFileRate"]);
        public static int DataPause = Convert.ToInt32(ConfigurationManager.AppSettings["DataPause"]);

        public const string PROJECT_NAME = "PriceReader";
        public bool InitComplete = false;

        public Reader()
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
            ThreadStart threadDelegate = new ThreadStart(PriceReaderProcess);
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

        private void PriceReaderProcess()
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

            // Used to test the comms with wireshark, logs, netstat         
            if (runInternalListener)
            {
                // Setup a Udp listener
                udpServer = new UdpListen();
                udpServer.RunServer();
            }

            // Start the price subscriber / publisher
            Logs.Info("Starting price subscriber");
            priceSub = new PriceSubscribe();
            priceSub.Subscribe(DataFileName, DataFileRate);

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

        public void MakeStop()
        {
            Logs.Info("In MakeStop");
            OnStop();
            Logs.Info("Out MakeStop");
        }

        protected override void OnStop()
        {
            Logs.Enter();
            EventLog.WriteEntry("Stopping " + PROJECT_NAME, EventLogEntryType.Information);
            Logs.Info("Process id: " + Process.GetCurrentProcess().Id);

            // Stop Udp listener thread
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

        protected override void OnShutdown()
        {
            Logs.Enter();
            EventLog.WriteEntry("Shutting down " + PROJECT_NAME, EventLogEntryType.Information);
            Logs.Info("Process id: " + Process.GetCurrentProcess().Id);

            if (serviceThread != null)
            {
                if (!serviceThread.Join(3000))
                    serviceThread.Abort();
            }

            Logs.Exit();
        }
    }
}
