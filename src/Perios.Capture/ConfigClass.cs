using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perios.Capture
{
    public class ConfigClass
    {
        public string OutputPath { get; set; }

        public int Interval { get; set; }

        public string Source { get; set; }

        public int OutputQuality { get; set; }

        public ConfigClass() { }


    }
}
