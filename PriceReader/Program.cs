using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PriceReader
{
    // This class starts the service

    public static class Program
    {
        private static Reader instance;
        public static Reader getInstance() { return instance; }
        public static void setInstance(Reader reader) { instance = reader; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main()
        {
            Logs.Info("In Main");

#if DEBUG
            instance = new Reader();
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                instance = new Reader()
            };
            ServiceBase.Run(ServicesToRun);
#endif

            Logs.Info("Out Main");
        }
    }
}
