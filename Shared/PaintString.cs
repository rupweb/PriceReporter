using System;
using System.Collections.Generic;

namespace Shared
{
    // This class holds string only price calculations for painting on the screen

    public class PaintString
    {
        public string market { get; set; }
        public string newPrice { get; set; }
        public string newDirection { get; set; }
        public string newAverage { get; set; }
        public List<String> newHistory { get; set; }

        public override string ToString()
        {
            return "market: " + market + ", newPrice: " + newPrice + ", newDirection: " + newDirection + ", newAverage: " + newAverage;
        }

        // A specific object for past prices and maths
        volatile QuoteMaths quoteMaths = new QuoteMaths();

        // Last 10 prices history
        private volatile List<String> history = new List<String>();

        public string SetPrice(Quote q)
        {
            Logs.Trace("In SetPrice with quote: " + q.Name + " " + q.Price);
            return q.Price.ToString();
        }

        public string SetDirection(Quote q)
        {
            return quoteMaths.direction(q);
        }

        public string SetAverage(Quote q)
        {
            return quoteMaths.average5(q);
        }

        public List<String> SetHistory(Quote q)
        {
            Logs.Trace("In SetHistory with quote: " + q.Name + " " + q.Price);

            if (history.Count < 10)
            {
                // Add the latest quote to the top
                history.Insert(0, q.Price.ToString());
            }
            else
            {
                // Add the latest quote to the top
                history.Insert(0, q.Price.ToString());

                // Remove the last quote from the end
                history.RemoveAt(10);
            }

            return history;
        }
    }
}
