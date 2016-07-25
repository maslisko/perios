using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perios.Capture.TestApp
{
    class Program
    {
        static PeriodicVideoCapture pvc;
        Bitmap frame;

        private static int counter = 0;
        private static string outputPath = @"C:\temp\";
        private static string outputFilePattern = "image";
        private static string outputFileExtension = ".bmp";

        static void Main(string[] args)
        {
            pvc =  new PeriodicVideoCapture();
            pvc.setInterval(1000);

            Program.ShowVideoDevices(pvc);

            pvc.SelectVideoDevice(pvc.ListVideoDevices().First<string>());
            
            pvc.TimerElapsed += Pvc_TimerElepsed;
            pvc.StartCapture();
            while (true)
            {
                ConsoleKeyInfo cki = Console.ReadKey();
                switch (cki.KeyChar)
                {
                    case 't':pvc.StopCapture();
                        break;
                    case 's': pvc.StartCapture();
                        break;
                }
                if (cki.KeyChar.ToString().ToLower().Equals("q"))
                    return;
            }
        }

        private static void ShowVideoDevices(PeriodicVideoCapture pic)
        {
            List<string> videoDevices = pic.ListVideoDevices() as List<string>;
            foreach (string videoDevice in videoDevices)
            {
                Console.WriteLine(videoDevice);
            }
        }

        private static void Pvc_TimerElepsed(object sender, EventArgs e)
        {
            string path = outputPath + outputFilePattern + counter + outputFileExtension;

            Bitmap b = pvc.GetFrame();
            b.Save(path);
            Console.WriteLine("New image captured. Stored in: " + path);
            counter++;
        }
    }
}
