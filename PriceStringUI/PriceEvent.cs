using Disruptor;
using Shared;
using System;
using System.Collections.Concurrent;

namespace PriceUI
{
    // This class handles events from the ring buffer

    internal class PriceEvent : IEventHandler<Quote>
    {
        private int MaxQueueSize = Program.MaxNetworkPriceQueueSize;
        
        // A singleton dictionary of unprocessed prices in each instrument
        private static ConcurrentDictionary<string, QuoteQueue> priceHistory = new ConcurrentDictionary<string, QuoteQueue>();

        // Return the price queue for the given instrument
        public QuoteQueue getCollection(string market) {
            if (Program.TraceNetworkQueues)
            {
                foreach (var pair in priceHistory)
                    Logs.Trace(pair.Key.ToString() + ": " + pair.Value.Length());
            }

            if (priceHistory.TryGetValue(market, out QuoteQueue queue))
                return queue;

            Logs.Info("Creating new price queue for market: " + market);
            queue = new QuoteQueue();
            priceHistory.TryAdd(market, queue);
            return queue;
        }

        // Return existence of queue for market
        public bool getExists(string market)
        {
            if (priceHistory.ContainsKey(market))
                return true;
            else
                return false;
        }

        public void OnEvent(Quote q, long sequence, bool endOfBatch)
        {          
            if (priceHistory.ContainsKey(q.Name))
            {
                if (priceHistory[q.Name].Length() > MaxQueueSize)
                    priceHistory[q.Name] = new QuoteQueue();

                priceHistory[q.Name].AddQuote(q);
            }
            else
            {
                QuoteQueue queue = new QuoteQueue();
                queue.AddQuote(q);
                priceHistory.TryAdd(q.Name, queue);           
            }
        }
    }
}
