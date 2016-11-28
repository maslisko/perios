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
        /// Fires an event when the timer elapses
        /// </summary>
        /// <param name="e">Event arguments</param>
        void OnTimerElapsed(EventArgs e);

        /// <summary>
        /// Gets current frame
        /// </summary>
        Bitmap GetFrame();

        /// <summary>
        /// Sets the interval of capturing the image
        /// </summary>
        /// <param name="interval">Interval of capturing</param>
        void setInterval(int interval);

        event EventHandler TimerElapsed;        
    }
}
