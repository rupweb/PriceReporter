using System.Collections.Concurrent;

namespace Shared
{
    // This class creates a blocking concurrent quote stack

    public class PaintStackString
    {
        private BlockingCollection<PaintString> paintStack;

        public PaintStackString()
        {
            paintStack = new BlockingCollection<PaintString>(new ConcurrentStack<PaintString>());
        }

        public void Add(PaintString p)
        {
            paintStack.Add(p);
        }

        public PaintString Take()
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
