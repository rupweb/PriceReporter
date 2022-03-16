using Shared;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Shared
{
    public class QuoteMaths
    {
        private volatile Quote previousQuote = new Quote();
        public void setPreviousPrice(decimal price) { previousQuote.Price = price; }

        private ConcurrentQueue<decimal> averagePrices = new ConcurrentQueue<decimal> ();

        public string direction(Quote q)
        {
            Logs.Trace("In direction with quote: " + q.Name + ", price: " + q.Price + " and previous price: " + previousQuote.Price);
            
            String direction = String.Empty;
            
            if (q.Price == previousQuote.Price)
                direction = "no change";
            else if (q.Price > previousQuote.Price)
                direction = "+";
            else if (q.Price < previousQuote.Price)
                direction = "-";

            previousQuote = q;
            return direction;
        }

        public string average5(Quote q)
        {
            Logs.Trace("In average5 with quote: " + q.Name + " " + q.Price);

            if (averagePrices.Count < 5)
                averagePrices.Enqueue(q.Price);
            else
            {
                averagePrices.TryDequeue(out _); // discard 
                averagePrices.Enqueue(q.Price);
            }
                
            decimal average = averagePrices.Average();
            return String.Format("{0:0.#######}", average);
        }
    }
}
