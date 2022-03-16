using Disruptor;
using Shared;

namespace PriceUI
{
    // This class handles new instruments

    internal class InstrumentEvent : IEventHandler<Quote>
    {
        InstrumentList il = new InstrumentList().getInstance();

        public void OnEvent(Quote q, long sequence, bool endOfBatch)
        {
            Instrument i = new Instrument(q.Name);
            if (!il.InstrumentNames(i))
            {
                new InstrumentList(i.Name);
                Logs.Info("Added instrument: " + i.Name);
            }          
        }
    }
}
