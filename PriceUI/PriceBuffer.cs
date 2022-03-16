using Disruptor;
using Disruptor.Dsl;
using Shared;
using System.Threading.Tasks;

namespace PriceUI
{
    // This class sets up a disruptor ring buffer
    internal class PriceBuffer
    {
        private static readonly int _ringSize = 8388608;  // Power of 2

        public Disruptor<Quote> startBuffer()
        {
            Disruptor<Quote> disruptor = new Disruptor<Quote>(() => new Quote(), _ringSize, TaskScheduler.Default);

            disruptor.HandleEventsWith(new PriceEvent(), new InstrumentEvent());

            return disruptor;
        }
    }
}
