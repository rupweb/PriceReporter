using Shared;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PriceUI
{
    // This class paints strings of average prices to the UI
    
    public class PainterAverage
    {
        private PriceFormTree pf;

        // Painting stack
        private PaintStack paintStack = new PaintStack();
        private PaintChannel paintChannel = new PaintChannel();

        public PainterAverage(PriceFormTree priceFormTree)
        {
            Logs.Info("In Painter");

            this.pf = priceFormTree;

            paintStack = pf.decorator.getDecoratorAverage();
            // paintChannel = pf.decorator.getDecoratorAverage();

            Logs.Info("Out Painter");
        }

        internal async Task StartPainter() 
        {
            Logs.Info("In StartPainter");

            // Check the price history queue
            while (Thread.CurrentThread.IsAlive)
            {
                // Delay painting if the delay buffer is set
                Thread.Sleep(Program.PaintingBufferDelay);

                // Blocking call to the painting stack
                Paint ui = paintStack.Take();
                // Paint ui = paintChannel.TakeAsync().Result;

                await Painting(ui);
            }

            Logs.Info("Out StartPainter");
        }

        public Task Painting(Paint ui)
        {
            try
            {
                pf.average.Invoke(new Action(() =>
                {
                    if (Program.ForceUIRepaint)
                    {
                        pf.average.Invalidate();
                    }

                    pf.average.Text = ui.newAverage;
                }));
            }
            catch (Exception e)
            {
                if (e is ObjectDisposedException || e is ThreadAbortException)
                    Logs.Warn("Price form tree closing for " + pf.market);
                else
                    Logs.Error("PainterAverage: ", e);
            }

            return Task.CompletedTask;
        }
    }
}
