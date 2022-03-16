using Shared;
using System;
using System.Threading;

namespace PriceUI
{
    // This class holds price calculations reduced to strings (or treenode) for painting on the screen
    
    public class PainterHistoryString
    {
        private PriceFormString pf;

        // Painting stack
        private PaintStackString paintStack = new PaintStackString();

        public PainterHistoryString(PriceFormString priceFormString)
        {
            Logs.Info("In PainterString");

            this.pf = priceFormString;

            paintStack = pf.decorator.getDecorator();

            Logs.Info("Out PainterString");
        }

        internal void StartPainter()
        {
            Logs.Info("In StartPainter");

            // Check the price history queue
            while (Thread.CurrentThread.IsAlive)
            {
                // Delay painting if the delay buffer is set
                Thread.Sleep(Program.PaintingBufferDelay);

                // Blocking call
                PaintString ui = paintStack.Take();

                Painting(ui);
            }

            Logs.Info("Out StartPainter");
        }

        public void Painting(PaintString ui)
        {

            try {
                pf.historyPanel.Invoke(new Action(() =>
                {
                    pf.historyLabel.Text = ui.market;

                    string[] history = ui.newHistory.ToArray();

                    pf.history1.Text = history[0];
                    pf.history2.Text = history[1];
                    pf.history3.Text = history[2];
                    pf.history4.Text = history[3];
                    pf.history5.Text = history[4];
                    pf.history6.Text = history[5];
                    pf.history7.Text = history[6];
                    pf.history8.Text = history[7];
                    pf.history9.Text = history[8];
                    pf.history10.Text = history[9];

                    // pf.historyPanel.Refresh();
                }));
            }
            catch (ArgumentOutOfRangeException)
            {
                Logs.Warn("History not yet at 10 records");
            }
            catch (Exception e)
            {
                if (e is ObjectDisposedException || e is ThreadAbortException)
                    Logs.Warn("Price form closing for " + pf.market);
                else
                    Logs.Error("ManagePrice: ", e);
            }
        }
    }
}
