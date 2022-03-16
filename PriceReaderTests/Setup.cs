using NUnit.Framework;
using System;
using PriceReader;
using System.Configuration;
using Shared;

namespace PriceReaderTests
{
    [SetUpFixture]
    class Setup
    {
        public const string PROJECT_NAME = Reader.PROJECT_NAME;
        public static string DataFileName = ConfigurationManager.AppSettings["DataFileName"];
        public static int DataFileRate = Convert.ToInt32(ConfigurationManager.AppSettings["DataFileRate"]);

        [OneTimeSetUp]
        public static void RunBeforeAnyTests()
        {
            // Setup logs for the tests
            log4net.Config.XmlConfigurator.Configure();

            Console.WriteLine("Setting up");
            Logs.Info("Setting up logs");

            // Start the app
            Program.Main();
        }

        [OneTimeTearDown]
        public static void RunAfterAllTests()
        {
            Program.getInstance().MakeStop();
        }
    }
}
