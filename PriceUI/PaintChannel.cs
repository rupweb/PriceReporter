using System.Threading.Channels;
using System.Threading.Tasks;

namespace PriceUI
{
    // This class creates a channel of paint objects

    public class PaintChannel
    {
        private Channel<Paint> paintChannel;

        public PaintChannel()
        {
            paintChannel = Channel.CreateUnbounded<Paint>();
        }

        public void Add(Paint p)
        {
            paintChannel.Writer.WriteAsync(p);
        }

        public Paint Take()
        {
            return paintChannel.Reader.ReadAsync().Result;
        }

        public async Task<Paint> TakeAsync()
        {
            return await paintChannel.Reader.ReadAsync();
        }

        public int Length()
        {
            return paintChannel.Reader.Count;
        }

        public void Clear()
        {
            while (paintChannel.Reader.Count > 0)
                paintChannel.Reader.TryRead(out _);
        }
    }
}
