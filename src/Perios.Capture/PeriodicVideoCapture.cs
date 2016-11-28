using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Perios.Capture
{
    public class PeriodicVideoCapture : PeriodicImageCapture, IPeriodicImageCapture
    {
        private VideoCaptureDevice captureDevice;
        private FilterInfoCollection videoDevices;
        private Bitmap frame;

        public PeriodicVideoCapture() : base()
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            frame = new Bitmap(1, 1);
         }

        public bool IsVideoDeviceInitialized()
        {
            // TODO - is it actually necessary?
            return (captureDevice.Equals(null));
        }

        public void SelectVideoDevice(string deviceName)
        {
            foreach(FilterInfo capDev in videoDevices)
            {
                if (capDev.Name.Equals(deviceName))
                {
                    captureDevice = new VideoCaptureDevice(capDev.MonikerString);
                    captureDevice.NewFrame += _captureDevice_NewFrame;
                    captureDevice.VideoSourceError += _captureDevice_VideoSourceError;
                    var vidCap = captureDevice.VideoCapabilities;

                    captureDevice.VideoResolution = captureDevice.VideoCapabilities[0];
                }
            }
        }

        public void SetResolution(Size resolution)
        {
            foreach (var vidCap in captureDevice.VideoCapabilities)
            {
                if (vidCap.FrameSize.Equals(resolution))
                {
                    captureDevice.VideoResolution = vidCap;
                }
            }
        }

        private void _captureDevice_VideoSourceError(object sender, AForge.Video.VideoSourceErrorEventArgs eventArgs)
        {

            Console.WriteLine(eventArgs.Description);
        }

        private void _captureDevice_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            // TODO: cloning necessary?
            //frame = (Bitmap)eventArgs.Frame.Clone();
        }

        public override void StartCapture()
        {
            captureDevice.Start();
            base.StartCapture();
        }

        public override void StopCapture()
        {
            captureDevice.Stop();
            base.StopCapture();
        }

        public override Bitmap GetFrame()
        {
            return frame;
        }

        public IEnumerable<string> ListVideoDevices()
        {
            var videoDevices = new List<string>();
            foreach (FilterInfo captureDevice in this.videoDevices)
            {
                videoDevices.Add(captureDevice.Name);

            }
            return videoDevices;
        }

        public IEnumerable<Size> ListVideoResolutions()
        {
            var videoResolutions = new List<Size>();

            foreach(VideoCapabilities vc in captureDevice.VideoCapabilities)
            {
                videoResolutions.Add(vc.FrameSize);
            }

            return videoResolutions;
            
        }

        /// <summary>
        /// Set timer period is over
        /// </summary>
        /// <param name="e"></param>
        public override void OnTimerElapsed(EventArgs e)
        {
            // no special magic here, may come later
            base.OnTimerElapsed(e);
        }

    }
}

