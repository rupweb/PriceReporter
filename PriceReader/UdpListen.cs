using Shared;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace PriceReader
{
    // This class is an internal UDP listener that can put prices into the internal price store

    public class UdpListen
    {
        static UdpClient listener = new UdpClient();
        public UdpListen getInstance() { return this; }
        public UdpClient getListener() { return listener; }
        PriceStore ps = new PriceStore();
        private Thread t;
        public Thread GetThread() { return t; }

        public void RunServer()
        {
            Logs.Info("Starting Udp Listener");

            // Thread to listen
            t = new Thread(StartListener);
            t.Start();

            Logs.Info("Started Udp Listener");
        }

        private void StartListener()
        {
            string host = Reader.Host;
            int port = Convert.ToInt32(Reader.InternalUdpPort);

            IPAddress ipAddress = IPAddress.Parse(host);
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

            listener = new UdpClient(remoteEP);

            while (Program.getInstance().IsRunning())
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
                    // TODO: alert
                }
            }

            Logs.Info("Stopped Udp Listener");
        }

        private void ProcessRequest(String body)
        {
            Logs.Trace("Price Reader Udp listener: " + body);

            // Add it to the price store
            try
            {
                Quote q = new Quote().ToQuote(body);
                ps.AddLatestQuote(q);
            }
            catch (Exception ex)
            {
                Logs.Error("Could not quote: " + body, ex);
            }
        }
    }
}
