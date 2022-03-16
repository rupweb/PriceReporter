using System.Collections.Concurrent;

namespace Shared
{
    // This class creates a blocking concurrent quote queue

    internal class QuoteQueue
    {
        private BlockingCollection<Quote> quoteQueue;

        public QuoteQueue()
        {
            quoteQueue = new BlockingCollection<Quote>(new ConcurrentQueue<Quote>());
        }

        public void AddQuote(Quote q)
        {
            quoteQueue.Add(q);
        }

        public Quote Take()
        {
            return quoteQueue.Take();
        }

        public int Length()
        {
            return quoteQueue.Count;
        }
    }
}
