using Shared;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PriceUI
{
    // This class paints strings of history prices to the UI

    public class PainterHistory
    {
        private PriceFormTree pf;

        // Painting stack
        private PaintStack paintStack = new PaintStack();
        private PaintChannel paintChannel = new PaintChannel();

        public PainterHistory(PriceFormTree priceFormTree)
        {
            Logs.Info("In Painter");

            this.pf = priceFormTree;

            paintStack = pf.decorator.getDecoratorHistory();
            // paintChannel = pf.decorator.getDecoratorHistory();

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
                pf.historyTree.Invoke(new Action(() => { 
                    if (pf.historyTree == null) 
                        return;

                    if (pf.historyTree.TopNode == null)
                        return;

                    pf.historyTree.TopNode.Text = ui.market;
                    pf.historyTree.TopNode.Nodes.Clear();
                    pf.historyTree.TopNode.Nodes.AddRange(ui.newHistory.ToArray());

                    pf.historyTree.Refresh();
                }));
            }
            catch (Exception e)
            {
                if (e is ObjectDisposedException || e is ThreadAbortException)
                    Logs.Warn("Price form tree closing for " + pf.market);
                else
                    Logs.Error("PainterHistory: ", e);
            }

            return Task.CompletedTask;
        }
    }
}
