using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GUIWalker
{
    //helper class for initializing parameters to implement the frmCut class.
    public class frmCutOptions
    {
        //properties
        public string inTimeHistory { get; set; }
        public string inRegions { get; set; }
        public string outTimeHistory { get; set; }
        public bool? appendSuffix { get; set; }
        public bool? sendToPlotter { get; set; }
        public eTaperMode? taperMode { get; set; }
        public eTaperType? taperType { get; set; }
        public string taperTime { get; set; }
        public bool? TaperEntirely { get; set; }
        public bool? TaperEnd { get; set; }
        public bool? useCustom { get; set; }

        public enum eTaperType
        {
            Smooth,
            Hann,
        }

        public enum eTaperMode
        {
            None,
            Extended,
            Normal,
            Mean,
        }
    }
}
