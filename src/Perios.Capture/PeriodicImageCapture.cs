using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Drawing;
using System.Configuration;

namespace Perios.Capture
{
    public class PeriodicImageCapture : IPeriodicImageCapture
    {
        
        private Timer _t;

        
        private int _interval = 500;

        /// <summary>
        /// Interval of the built-in timer
        /// </summary>
        public int Interval
        {
            get
            {
                return _interval;
            }
            set
            {
                _interval = Interval;
            }
        }

        public PeriodicImageCapture(int interval = 0)
        {
            _interval = (interval == 0) ? Int32.Parse(ConfigurationManager.AppSettings["defaultInterval"]) : interval;
            _t = new Timer(_interval);
            _t.Elapsed += T_Elapsed;           
        }

        /// <summary>
        /// Event handler for timer
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event arguments</param>
        private void T_Elapsed(object sender, ElapsedEventArgs e)
        {
            OnTimerElapsed(e);    
        }

        public virtual void StartCapture()
        {
            _t.Start();
        }

        public virtual void StopCapture()
        {
            _t.Stop();
        }

        public virtual Bitmap GetFrame()
        {
            // returns something in derived classes
            return null;
        }


        public virtual void OnTimerElapsed(EventArgs e)
        {
            EventHandler handler = TimerElapsed;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void setInterval(int interval)
        {
            _t.Interval = interval;
        }

        public event EventHandler TimerElapsed;


        
    }
}
