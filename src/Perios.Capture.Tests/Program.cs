using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Perios.Capture.TestApp
{
    class Program
    {
        static PeriodicVideoCapture pvc;
        static ConfigClass configuration = new ConfigClass();
        private static List<string> videoDevices = new List<string>();

        private static int selectedVideoDeviceIndex = 0;
        private static int counter = 0;
        private static string outputPath = @"C:\temp\";
        private static string outputFilePattern = "image";
        private static string outputFileExtension = ".png";

        

        static void Main(string[] args)
        {
            
            ProcessInputParameters(args);
            
            pvc =  new PeriodicVideoCapture();
            pvc.setInterval(1000);

            Program.ShowVideoDevices(pvc);

            pvc.SelectVideoDevice(pvc.ListVideoDevices().First<string>());
            pvc.SetResolution(new Size(1600, 1200));

            Console.WriteLine("Video Capabilities: ");
            Program.ShowVideoResolutions(pvc);
            
            // TODO: set video capture device
            
            // TODO: test setting video resolution

            pvc.TimerElapsed += Pvc_TimerElapsed;

            while (true)
            {
                var cki = Console.ReadKey();
                switch (cki.KeyChar.ToString().ToLower())
                {
                    case "t":pvc.StopCapture();
                        break;
                    case "s": pvc.StartCapture();
                        break;
                    case "r": ListVideoResolutions();
                        break;
                    case "d": ListVideoDevices();
                        break;
                    case "q": return;
                }
            }
        }

        private static void ProcessInputParameters(string[] args)
        {
            // TODO: return bool - test correctness of config property assignment
            foreach (string argument in args)
            {
                string[] argumentPair = argument.Split('=');
                Console.WriteLine(argumentPair[0] + "  =  " + argumentPair[1]);
                
                PropertyInfo propertyInfo = configuration.GetType().GetProperty(argumentPair[0]);
                propertyInfo.SetValue(configuration, Convert.ChangeType(argumentPair[1], propertyInfo.PropertyType));

            }
            
            var configProperties = configuration.GetType().GetProperties();
            foreach(var configProperty in configProperties)
            {
                Console.WriteLine(configProperty.Name + " = " + configProperty.GetValue(configuration));
            }
        }

        private static void ListVideoDevices()
        {
            List<string> videoDevices = pvc.ListVideoDevices() as List<string>;
            videoDevices.ForEach(vd => Console.WriteLine(vd));
        }

        private static void ListVideoResolutions()
        {
            List<Size> videoResolutions = pvc.ListVideoResolutions() as List<Size>;
            videoResolutions.ForEach(vr => Console.WriteLine(vr.Width+"x"+vr.Height));
        }

        private static void ShowVideoDevices(PeriodicVideoCapture pic)
        {
            videoDevices = pic.ListVideoDevices() as List<string>;
            foreach (string videoDevice in videoDevices)
            {
                Console.WriteLine(videoDevice);
            }
        }

        private static void ShowVideoResolutions(PeriodicVideoCapture pic)
        {
            List<Size> videoResolutions = pic.ListVideoResolutions() as List<Size>;
            foreach (var vr in videoResolutions)
            {
                Console.WriteLine(vr.Width+"x"+vr.Height);
            }
        }

        private static void Pvc_TimerElapsed(object sender, EventArgs e)
        {
            SaveToDisk();
            counter++;
        }

        private static void SaveToDisk()
        {
            string path = Path.Combine(outputPath, outputFilePattern, counter.ToString().PadLeft(6, '0'), outputFileExtension);
            Bitmap b = pvc.GetFrame();
            b.Save(path, ImageFormat.Png);
            Console.WriteLine("New image captured. Stored in: " + path);
        }
    }
}
