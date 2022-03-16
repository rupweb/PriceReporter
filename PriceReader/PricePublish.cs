using Disruptor;
using Microsoft.Extensions.DependencyInjection;
using Shared;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace PriceReader
{
    // This class handles events from the ring buffer

    internal class PricePublish : IEventHandler<Quote>
    {
        private static UdpClient udpClient = new UdpClient();

        public void OnEvent(Quote q, long sequence, bool endOfBatch)
        {
            Logs.Trace("Event handled: Value = {0} sequence {1}", q.ToString(), sequence.ToString());

            if (Reader.sendInternalMessage) 
                PostInternalUdp(q);
                
            PostExternalUdp(q);

            // There could be a pause to manually check UI
            Thread.Sleep(Reader.DataPause);
        }

        private void PostInternalUdp(Quote q)
        {
            string host = Reader.Host;
            int port = Convert.ToInt32(Reader.InternalUdpPort);

            IPAddress ipAddress = IPAddress.Parse(host);
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

            udpClient.Connect(remoteEP);

            var data = new StringContent(q.ToJson(q), Encoding.UTF8, "application/json");
            byte[] byteData = data.ReadAsByteArrayAsync().Result;

            udpClient.Send(byteData, byteData.Length);
        }

        private void PostExternalUdp(Quote q)
        {
            string host = Reader.Host;
            int port = Convert.ToInt32(Reader.ExternalUdpPort);

            IPAddress ipAddress = IPAddress.Parse(host);
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

            udpClient.Connect(remoteEP);

            var data = new StringContent(q.ToJson(q), Encoding.UTF8, "application/json");
            byte[] byteData = data.ReadAsByteArrayAsync().Result;

            udpClient.Send(byteData, byteData.Length);
        }
    }
}
