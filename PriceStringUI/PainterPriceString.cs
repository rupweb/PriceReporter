using Shared;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PriceUI
{
    // This class paints strings of current price and direction to the UI

    public class PainterPriceString
    {
        private PriceFormString pf;

        // Painting stack
        private PaintStackString paintStack = new PaintStackString();
        // private PaintChannel paintChannel = new PaintChannel();

        public PainterPriceString(PriceFormString priceFormString)
        {
            Logs.Info("In Painter");

            this.pf = priceFormString;

            paintStack = pf.decorator.getDecorator();
            // paintChannel = pf.decorator.getDecoratorPrice();

            Logs.Info("Out Painter");
        }

        internal void StartPainter() 
        {
            Logs.Info("In StartPainter");

            // Check the price history queue
            while (Thread.CurrentThread.IsAlive)
            {
                // Delay painting if the delay buffer is set
                Thread.Sleep(Program.PaintingBufferDelay);

                // Blocking call to the painting stack
                PaintString ui = paintStack.Take();
                // Paint ui = paintChannel.TakeAsync().Result;

                Painting(ui);
            }

            Logs.Info("Out StartPainter");
        }

        public void Painting(PaintString ui)
        {
            try
            {            
                pf.price.Invoke(new Action(() =>
                {
                    if (Program.ForceUIRepaint)
                    {
                        pf.price.Invalidate();
                        pf.direction.Invalidate();
                    }
                    
                    pf.price.Text = ui.newPrice;
                    pf.direction.Text = ui.newDirection;
                }));
            }
            catch (Exception e)
            {
                if (e is ObjectDisposedException || e is ThreadAbortException)
                    Logs.Warn("Price form tree closing for " + pf.market);
                else
                    Logs.Error("PainterPrice: ", e);
            }
        }
    }
}
