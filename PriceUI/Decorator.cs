using Shared;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace PriceUI
{
    // This class listens to the prices for the market and splits them into price, average and history FIFO stacks for painting
    public class Decorator
    {
        string market = String.Empty;
        
        // Price history queue
        private QuoteQueue priceQueue = new QuoteQueue();

        // Painting stacks
        private PaintStack decoratorPrice = new PaintStack();
        private PaintStack decoratorAverage = new PaintStack();
        private PaintStack decoratorHistory = new PaintStack();
        public PaintStack getDecoratorPrice() { return decoratorPrice; }
        public PaintStack getDecoratorAverage() { return decoratorAverage; }
        public PaintStack getDecoratorHistory() { return decoratorHistory; }

        // A paint object
        Paint oldUi = new Paint();

        public Decorator() { }

        public Decorator(string market)
        {
            this.market = market;
        }

        internal void Pricing()
        {
            Logs.Info("Start pricing queue for instrument: " + market);

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

            Logs.Info("Stopped pricing queue for instrument: " + market);
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
            Paint newUi = new Paint();

            newUi.market = q.Name;
            newUi.newPrice = oldUi.SetPrice(q);
            newUi.newDirection = oldUi.SetDirection(q);
            newUi.newAverage = oldUi.SetAverage(q);
            newUi.newHistory = oldUi.SetHistory(q);

            Logs.Trace("newUi: " + newUi.ToString());

            // Add the paint objects to the paint stacks for the painters
            decoratorPrice.Add(newUi);
            decoratorAverage.Add(newUi);
            decoratorHistory.Add(newUi);

            // Drain UI stacks if overloaded
            DrainDecoratorQueue();
        }

        private void DrainDecoratorQueue()
        {
            if (decoratorPrice.Length() > Program.MaxPaintQueueSize)
            {
                Logs.Info("Decorator count: " + decoratorPrice.Length() + ". Clearing " + market);
                decoratorPrice.Clear();
            }

            if (decoratorAverage.Length() > Program.MaxPaintQueueSize)
            {
                Logs.Info("Decorator count: " + decoratorAverage.Length() + ". Clearing " + market);
                decoratorAverage.Clear();
            }

            if (decoratorHistory.Length() > Program.MaxPaintQueueSize)
            {
                Logs.Info("Decorator count: " + decoratorHistory.Length() + ". Clearing " + market);
                decoratorHistory.Clear();
            }
        }
    }
}
