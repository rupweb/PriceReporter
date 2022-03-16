using Shared;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace PriceUI
{
    // This class maintains a list of instruments
    
    public class InstrumentList
    {
        static BindingList<Instrument> list = new BindingList<Instrument>();
        public InstrumentList getInstance() { return this; }

        // A lock for concurrent access to the instrument list
        private readonly object syncLock = new object();

        public InstrumentList() { }

        public InstrumentList(string name)
        {
            // Setup the base instruments
            Instrument i = new Instrument(name);

            // Lock it or race condition on new instrument
            lock (syncLock)
                if (!InstrumentNames(i)) list.Add(i);

            BindInstrument(name);
        }

        public void BindInstrument(string name)
        {
            SplashForm sf = new SplashForm().GetInstance();

            try
            {
                if (sf.InvokeRequired)
                {
                    sf.Invoke(new MethodInvoker(delegate { BindInstrument(name); }));
                    return; // If no return then action will carry on
                }

                var binding = new BindingSource();

                // Have to force a binding update https://stackoverflow.com/questions/638859/how-to-update-listbox-items-with-inotifypropertychanged
                binding.DataSource = null;
                sf.instruments.DataSource = binding.DataSource;

                // Binding update
                binding.DataSource = list;
                sf.instruments.DataSource = binding.DataSource;
                sf.instruments.DisplayMember = "Name";

                Logs.Info("Display new instrument: " + name);
            }
            catch (Exception e)
            {
                Logs.Error("BindInstrument: ", e);
            }
        }

        public bool InstrumentNames(Instrument inst)
        {
            for (int j = 0; j < list.Count; j++)
            {
                if (list[j].Name.Equals(inst.Name))
                    return true;
            }

            return false;
        }

        public void Add(Instrument inst)
        {
            list.Add(inst);
        }
    }
}
