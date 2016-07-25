using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Drawing;

namespace Perios.Capture
{
    public interface IPeriodicImageCapture
    {

        /// <summary>
        /// Starts the image capture
        /// </summary>
        void StartCapture();

        /// <summary>
        /// Stops the image capture
        /// </summary>
        void StopCapture();

        /// <summary>
        /// Fires an event 
        /// </summary>
        /// <param name="e">Event arguments</param>
        void OnTimerElapsed(EventArgs e);

        Bitmap GetFrame();

        void setInterval(int interval);

        event EventHandler TimerElapsed;        
    }
}
