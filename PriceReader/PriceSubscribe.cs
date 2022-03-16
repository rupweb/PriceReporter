using Disruptor;
using Disruptor.Dsl;
using Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace PriceReader
{
    // This class reads the data file and puts the prices into a ring buffer

    public class PriceSubscribe
    {
        Disruptor<Quote> ring;

        public Disruptor<Quote> getRing() { return ring; }

        public void Subscribe(string name, int rate)
        {
            Logs.Info("Opening file: " + name);

            FileReader fr = new FileReader();
            string[] lines = fr.ReadFile(name);

            // Start ring buffer
            Logs.Info("Starting prices buffer");
            ring = new PriceBuffer().startBuffer();
            ring.Start();

            // Tell the world init is completed before blocking subscription
            Program.getInstance().InitComplete = true;

            Logs.Info("Filling prices");
            while (Program.getInstance().IsRunning())
            {
                AddElements(lines);
                Thread.Sleep(rate);
            }
        }

        private void AddElements(string[] lines)
        {
            Array.ForEach(lines, x =>
            {
                using (var scope = ring.PublishEvent())
                {
                    Quote q = scope.Event();
                    q.Name = x.Substring(0, x.IndexOf(":")).ToUpper();
                    q.Price = decimal.Parse(x.Substring(x.IndexOf(":") + 1));
                }
            });
        }
    }
}
