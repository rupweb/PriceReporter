using Disruptor.Dsl;
using Shared;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace PriceWeb
{
    // This class listens for prices on a UDP port and puts them into a disruptor buffer
    
    public class UdpListen
    {
        UdpClient listener = new UdpClient();
        public UdpListen getInstance() { return this; }
        public UdpClient getListener() { return listener; }

        static Disruptor<Quote> ring;
        public Disruptor<Quote> getRing() { return ring; }

        private Thread t;
        public Thread GetThread() { return t; }

        public void RunServer()
        {
            Logs.Info("Starting Udp Listener");

            // Start internal ring buffer to receive prices
            Logs.Info("Starting internal prices buffer");
            ring = new PriceBuffer().startBuffer();
            ring.Start();

            // Thread to listen
            t = new Thread(StartListener);
            t.Start();

            Logs.Info("Started Udp Listener");
        }

        private void StartListener()
        {
            string host = WebPublisher.Host;
            int port = Convert.ToInt32(WebPublisher.UdpPort);

            IPAddress ipAddress = IPAddress.Parse(host);
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

            listener = new UdpClient(remoteEP);

            while (Thread.CurrentThread.IsAlive)
            {
                try
                {
                    Byte[] receiveBytes = listener.Receive(ref remoteEP);
                    string returnData = Encoding.ASCII.GetString(receiveBytes);

                    ProcessRequest(returnData);
                }
                catch (Exception ex)
                {
                    Logs.Error("Udp listener: " + ex.Message);
                    return; // TODO: Alerts here
                }
            }

            Logs.Info("Stopped Udp Listener");
        }

        private void ProcessRequest(String body)
        {
            if (Program.TraceNetworkPrices)
                Logs.Trace("Udp listener: " + body);

            // Add it to the internal price buffer
            Quote q = new Quote().ToQuote(body);

            using (var scope = ring.PublishEvent())
            {
                Quote q2 = scope.Event();
                q2.Name = q.Name;
                q2.Price = q.Price;
            }
        }
    }
}
