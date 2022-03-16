using System.Collections.Concurrent;

namespace PriceUI
{
    // This class creates a blocking concurrent quote stack

    public class PaintStack
    {
        private BlockingCollection<Paint> paintStack;

        public PaintStack()
        {
            paintStack = new BlockingCollection<Paint>(new ConcurrentStack<Paint>());
        }

        public void Add(Paint p)
        {
            paintStack.Add(p);
        }

        public Paint Take()
        {
            return paintStack.Take();
        }

        public int Length()
        {
            return paintStack.Count;
        }

        public void Clear()
        {
            while (paintStack.Count > 0)
                paintStack.Take();
        }
    }
}
