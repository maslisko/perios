using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perios.Capture
{
    public class PeriodicVideoCapture : PeriodicImageCapture, IPeriodicImageCapture
    {
        VideoCaptureDevice _captureDevice;
        FilterInfoCollection _videoDevices;
        private Bitmap frame;

        public PeriodicVideoCapture() : base()
        {
            _videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            frame = new Bitmap(1, 1);
         }

        public bool IsVideoDeviceInitialized()
        {
            // TODO
            return false;
        }

        public void SelectVideoDevice(string deviceName)
        {
            foreach(FilterInfo captureDevice in _videoDevices)
            {

                if (captureDevice.Name.Equals(deviceName))
                {
                    _captureDevice = new VideoCaptureDevice(captureDevice.MonikerString);
                    _captureDevice.NewFrame += _captureDevice_NewFrame;
                    _captureDevice.VideoSourceError += _captureDevice_VideoSourceError;
                }
                   
            }
        }

        private void _captureDevice_VideoSourceError(object sender, AForge.Video.VideoSourceErrorEventArgs eventArgs)
        {
            throw new NotImplementedException();
        }

        private void _captureDevice_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            frame = (Bitmap)eventArgs.Frame.Clone();
        }

        public override void StartCapture()
        {
            _captureDevice.Start();
            base.StartCapture();
        }

        public override void StopCapture()
        {
            _captureDevice.Stop();
            base.StopCapture();
        }

        public override Bitmap GetFrame()
        {
            return frame;
        }

        public IEnumerable<string> ListVideoDevices()
        {
            var videoDevices = new List<string>();
            foreach (FilterInfo captureDevice in _videoDevices)
            {
                videoDevices.Add(captureDevice.Name);

            }
            return videoDevices;
        }

        public override void OnTimerElapsed(EventArgs e)
        {
            // no special magic here, may come later
            base.OnTimerElapsed(e);
        }

    }
}

