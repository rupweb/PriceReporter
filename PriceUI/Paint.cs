using Shared;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PriceUI
{
    // This class manages calculations from incoming quotes
    
    public class Paint
    {
        public string market { get; set; }
        public string newPrice { get; set; }
        public string newDirection { get; set; }
        public string newAverage { get; set; }
        public List<TreeNode> newHistory { get; set; }

        public override string ToString()
        {
            return "market: " + market + ", newPrice: " + newPrice + ", newDirection: " + newDirection + ", newAverage: " + newAverage;
        }

        // A specific object for past prices and maths
        volatile QuoteMaths quoteMaths = new QuoteMaths();

        // Last 10 prices history
        private volatile List<TreeNode> history = new List<TreeNode>();

        public string SetPrice(Quote q)
        {
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

        public List<TreeNode> SetHistory(Quote q)
        {
            Logs.Trace("In SetHistory with quote: " + q.Name + " " + q.Price);

            TreeNode tn = new TreeNode();
            tn.Text = q.Price.ToString();

            if (history.Count < 10)
            {
                // Add the latest quote to the top
                history.Insert(0, tn);
            }
            else
            {
                // Add the latest quote to the top
                history.Insert(0, tn);

                // Remove the last quote from the end
                history.RemoveAt(10);
            }

            return history;
        }
    }
}
