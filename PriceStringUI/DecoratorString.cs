using Shared;
using System;
using System.Threading;

namespace PriceUI
{
    internal class DecoratorString
    {
        string market = String.Empty;
        
        // Price history queue
        private QuoteQueue priceQueue = new QuoteQueue();

        // Painting stacks
        private PaintStackString decorator = new PaintStackString();
        public PaintStackString getDecorator() { return decorator; }

        // A paint object
        PaintString oldUi = new PaintString();

        public DecoratorString() { }

        public DecoratorString(string market)
        {
            this.market = market;
        }

        internal void Pricing()
        {
            Logs.Info("Start pricing decorator for instrument: " + market);

            // Check the price history queue
            while (Thread.CurrentThread.IsAlive)
            {
                // Blocking call
                priceQueue = new PriceEvent().getCollection(market);

                // If network queue gets too big then drain it
                if (priceQueue.Length() > Program.MaxNetworkPriceQueueSize)
                    DrainPriceQueue();

                Quote q = priceQueue.Take();
                ManagePrice(q);
            }

            Logs.Info("Stopped pricing decorator for instrument: " + market);
        }

        private void DrainPriceQueue()
        {
            Logs.Info("Price queue count: " + priceQueue.Length() + ". Clearing " + market);
            while (priceQueue.Length() > Program.MaxNetworkPriceQueueSize)
            {
                priceQueue.Take();
            }
        }

        public void ManagePrice(Quote q)
        {
            // For the UI to keep up with pricing the data has to be prepared first then added to a queue / timer for painting
            PaintString newUi = new PaintString();

            newUi.market = q.Name;
            newUi.newPrice = oldUi.SetPrice(q);
            newUi.newDirection = oldUi.SetDirection(q);
            newUi.newAverage = oldUi.SetAverage(q);
            newUi.newHistory = oldUi.SetHistory(q);

            Logs.Trace("newUi: " + newUi.ToString());

            // Add the paint objects to the paint stacks for the painters
            decorator.Add(newUi);

            // Drain UI stack if it's overloaded
            DrainDecoratorQueue();
        }

        private void DrainDecoratorQueue()
        {
            if (decorator.Length() > Program.MaxPaintQueueSize)
            {
                Logs.Info("Decorator count: " + decorator.Length() + ". Clearing " + market);
                decorator.Clear();
            }
        }
    }
}
