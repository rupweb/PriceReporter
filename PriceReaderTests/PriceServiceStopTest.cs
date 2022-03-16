using NUnit.Framework;
using PriceReader;
using Shared;
using System.Threading;

namespace PriceReaderTests
{
    [TestFixture]
    public class PriceServiceStopTest
    {
        [TestCase]
        public void RunToStop()
        {
            Logs.Info("In PriceServiceStopTest");

            // Wait for all threads to start
            if (!Program.getInstance().InitComplete)
                Thread.Sleep(100);

            // Wait another couple of seconds
            Thread.Sleep(2000);

            // Stop the service
            Program.getInstance().MakeStop();

            Logs.Info("Out PriceServiceStopTest");
        }
    }
}
