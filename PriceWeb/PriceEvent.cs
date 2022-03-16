using Disruptor;
using Shared;
using System;
using System.Collections.Concurrent;

namespace PriceWeb
{
    // This class handles events from the ring buffer

    public class PriceEvent : IEventHandler<Quote>
    {
        // A singleton historical dictionary of string calculations for prices in each instrument
        private static ConcurrentDictionary<string, PaintString> markets = new ConcurrentDictionary<string, PaintString>();
        public ConcurrentDictionary<string, PaintString> getMarkets() { return markets; }

        public void OnEvent(Quote q, long sequence, bool endOfBatch)
        {
            // Run the calculations and place the string results into a queue for availability

            if (markets.ContainsKey(q.Name))
                runCalculations(markets[q.Name], q);
            else
                newCalculations(q);
        }

        private void runCalculations(PaintString paintString, Quote q)
        {
            paintString.newPrice = paintString.SetPrice(q);
            paintString.newDirection = paintString.SetDirection(q);
            paintString.newAverage = paintString.SetAverage(q);
            paintString.newHistory = paintString.SetHistory(q);

            markets[q.Name] = paintString;
        }

        private void newCalculations(Quote q)
        {
            PaintString newPaintStrings = new PaintString();

            newPaintStrings.newPrice = newPaintStrings.SetPrice(q);
            newPaintStrings.newDirection = newPaintStrings.SetDirection(q);
            newPaintStrings.newAverage = newPaintStrings.SetAverage(q);
            newPaintStrings.newHistory = newPaintStrings.SetHistory(q);

            markets.TryAdd(q.Name, newPaintStrings);
        }
    }
}
