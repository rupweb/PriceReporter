using Shared;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;

namespace PriceReader
{
    // This class puts 10k quotes into a blocking store that could be read later

    public class PriceStore
    {
        private static BlockingCollection<Quote> Quotes = new BlockingCollection<Quote>();
        public BlockingCollection<Quote> GetQuotesCollection() { return Quotes; }

        public Quote GetLatestQuote()
        {
            return Quotes.Take();
        }

        public void AddLatestQuote(Quote quote)
        {
            // For now only store 10k quotes
            if (Quotes.Count > 10000)
            {
                Logs.Trace("Reset quote store");
                Quotes = new BlockingCollection<Quote>();
            }

            Quotes.Add(quote);
        }

        public void Store(string name)
        {
            FileReader fr = new FileReader();
            string[] lines = fr.ReadFile(name);

            Stopwatch timer = new Stopwatch();
            timer.Start();
            while (Program.getInstance().IsRunning())
            {
                StoreElements(lines);
                Thread.Sleep(100);
            }
        }

        private void StoreElements(string[] lines)
        {
            Array.ForEach(lines, x =>
            {
                Quote q = new Quote();

                q.Name = x.Substring(0, x.IndexOf(":"));
                q.Price = decimal.Parse(x.Substring(x.IndexOf(":") + 1));

                AddLatestQuote(q);
            });
        }
    }
}
