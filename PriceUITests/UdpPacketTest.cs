using NUnit.Framework;
using PriceUI;
using Shared;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PriceUITests
{
    [TestFixture]
    internal class UdpPacketTest
    {
        private static UdpClient udpClient = new UdpClient();

        [TestCase("BP.L", "1455.95")]
        [TestCase("Dow", "12,965")]
        [TestCase("Oil", "84")]
        [TestCase("BT.L", "1103")]
        [TestCase("VOD.L", "93.0")]
        public void SendUdpTest(string instrument, string price)
        {
            Quote q = new Quote();
            q.Name = instrument;
            q.Price = decimal.Parse(price);

            PriceFormTree pft = InitForm(instrument);

            PostExternalUdp(q);
            Thread.Sleep(2000); // Wait a long time for network traffic throughput

            // Not actually testing an assert
        }

        private void PostExternalUdp(Quote q)
        {
            string host = ConfigurationManager.AppSettings["Host"];
            int port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);

            IPAddress ipAddress = IPAddress.Parse(host);
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

            udpClient.Connect(remoteEP);

            var data = new StringContent(q.ToJson(q), Encoding.UTF8, "application/json");
            byte[] byteData = data.ReadAsByteArrayAsync().Result;

            udpClient.Send(byteData, byteData.Length);
        }

        private PriceFormTree InitForm(string instrument)
        {
            SplashForm sf = new SplashForm(instrument);
            sf.Background(); // Starts Udp listener
            _ = sf.Handle; // Ensure invoke handle created
            Thread.Sleep(1000); // Let tasks initialize

            PriceFormTree pft = new PriceFormTree(instrument);
            pft.price = new Label();
            pft.direction = new Label();
            pft.average = new Label();
            pft.historyTree = new BufferedTreeView();
            _ = pft.price.Handle; // Ensure invoke handle created
            _ = pft.direction.Handle; // Ensure invoke handle created
            _ = pft.average.Handle; // Ensure invoke handle created
            _ = pft.historyTree.Handle; // Ensure invoke handle created

            pft.Background(); // Also called by the shown event
            Thread.Sleep(1000); // Let tasks initialize

            return pft;
        }
    }
}
