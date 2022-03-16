using Disruptor;
using Shared;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace PriceWeb
{
    public class HttpListen
    {
        HttpListener listener = new HttpListener();
        public HttpListen getInstance() { return this; }
        public HttpListener getListener() { return listener; }

        private Thread t;
        public Thread GetThread() { return t; }

        // A lock for concurrent access to the painting strings
        private readonly object syncLock = new object();

        public void RunServer()
        {
            Logs.Info("Starting Http Listener");

            // Thread to listen
            t = new Thread(StartListener);
            t.Start();

            Logs.Info("Started Http Listener");
        }

        private void StartListener()
        {
            string prefix = "http://" + WebPublisher.Host + ":" + WebPublisher.HttpPort + "/" + WebPublisher.PricesEndpoint + "/";
            Logs.Info("Endpoint: " + prefix);
            listener.Prefixes.Add(prefix);

            try
            {
                listener.Start();
            }
            catch (HttpListenerException hlex)
            {
                Logs.Error("Http listener: " + hlex.Message);
                // TODO: Alert
            }

            Logs.Info("Listener listening");
            while (listener.IsListening)
            {
                HttpListenerContext context = listener.GetContext();
                lock (syncLock)
                    Response(context);
            }

            Logs.Info("Stopped endpoint: " + prefix);
        }


        private void Response(HttpListenerContext context)
        {
            Logs.Info("Received Http request");
            
            string html = "<HTML><BODY><TABLE border = 1><TR>";
            html += "<TH>Market</TH><TH>Price</TH><TH>Average</TH><TH>History</TH>";
            html += "</TR>";
            ConcurrentDictionary<string, PaintString> paintStrings = new PriceEvent().getMarkets();

            if (paintStrings.Count == 0)
            {
                Logs.Warn("No pricing to send out");
                return;
            }

            foreach (KeyValuePair<string, PaintString> markets in paintStrings)
            {
                string name = markets.Key;
                PaintString values = markets.Value;

                html += "<TR>";
                html += "<TD>" + name + "</TD>";
                html += "<TD>" + values.newPrice + " " + values.newDirection + "</TD>";
                html += "<TD>" + values.newAverage + "</TD>";

                string history = String.Empty;
                foreach(string s in values.newHistory)
                {
                    history += s + ", ";
                }
                history = history.TrimEnd(' ', ',');

                html += "<TD>" + history + "</TD>";
                html += "</TR>";
            }

            Logs.Info(html);

            byte[] send = Encoding.UTF8.GetBytes(html + "</TABLE></BODY></HTML>");

            context.Response.StatusCode = 200;
            context.Response.KeepAlive = true;
            context.Response.ContentLength64 = send.Length;

            var output = context.Response.OutputStream;
            output.Write(send, 0, send.Length);
            context.Response.Close();
        }
    }
}
