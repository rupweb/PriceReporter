using Disruptor;
using Shared;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace PriceWeb
{
    internal class PricePublish : IEventHandler<Quote>
    {
        public void OnEvent(Quote q, long sequence, bool endOfBatch)
        {
            Logs.Trace("Event handled: Value = {0} sequence {1}", q.ToString(), sequence.ToString());
                
            PostExternalSocket(q);
        }

        private void PostInternalSocket(Quote q)
        {
            string host = WebPublisher.Host;
            int port = Convert.ToInt32(WebPublisher.HttpPort);

            IPAddress ipAddress = IPAddress.Parse(host);
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

            // Create a TCP socket
            Socket sock = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            sock.Connect(remoteEP);

            var data = new StringContent(q.ToJson(q), Encoding.UTF8, "application/json");
            byte[] byteData = data.ReadAsByteArrayAsync().Result;

            var post = $"POST /prices HTTP/1.1" + Environment.NewLine;
            post += "Content-Type: application/json" + Environment.NewLine;
            post += "Host: " + host + ":" + port + Environment.NewLine;
            post += "Content-Length: " + byteData.Length + Environment.NewLine;
            post += Environment.NewLine;
            byte[] postData = Encoding.UTF8.GetBytes(post);

            byte[] sendData = new byte[byteData.Length + postData.Length];
            Array.Copy(postData, 0, sendData, 0, postData.Length);
            Array.Copy(byteData, 0, sendData, postData.Length, byteData.Length);

            sock.SendTimeout = 500;
            sock.Send(sendData);
            sock.Disconnect(false);
        }

        private void PostExternalSocket(Quote q)
        {
            string host = WebPublisher.Host;
            int port = Convert.ToInt32(WebPublisher.HttpPort);

            IPAddress ipAddress = IPAddress.Parse(host);
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

            // Create a TCP socket
            Socket sock = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                sock.Connect(remoteEP);
            }
            catch (SocketException)
            {
                Logs.Warn("No listener on port: " + WebPublisher.HttpPort);
                return;
            }

            var data = new StringContent(q.ToJson(q), Encoding.UTF8, "application/json");
            byte[] byteData = data.ReadAsByteArrayAsync().Result;

            var post = $"POST /prices HTTP/1.1" + Environment.NewLine;
            post += "Content-Type: application/json" + Environment.NewLine;
            post += "Host: " + host + ":" + port + Environment.NewLine;
            post += "Content-Length: " + byteData.Length + Environment.NewLine;
            post += Environment.NewLine;
            byte[] postData = Encoding.UTF8.GetBytes(post);

            byte[] sendData = new byte[byteData.Length + postData.Length];
            Array.Copy(postData, 0, sendData, 0, postData.Length);
            Array.Copy(byteData, 0, sendData, postData.Length, byteData.Length);

            sock.SendTimeout = 500;
            sock.Send(sendData);
            sock.Disconnect(false);
        }

        private void PostWeb(Quote q)
        {
            var url = "http://" + WebPublisher.Host + ":" + WebPublisher.HttpPort + "/" + WebPublisher.PricesEndpoint;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            var data = new StringContent(q.ToJson(q), Encoding.UTF8, "application/json");

            byte[] byteData = data.ReadAsByteArrayAsync().Result;

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = byteData.LongLength;

            var stream = request.GetRequestStream();
            stream.Write(byteData, 0, byteData.Length);
            stream.Close();
            request.Abort();
        }
    }
}
