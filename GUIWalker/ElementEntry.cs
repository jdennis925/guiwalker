using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;

namespace GUIWalker
{
    //attribute class for logging AutomationElements.
    public class ElementEntry
    {
        public string Name { get; set; }
        public string AutomationId { get; set; }
        public ControlType mControlType { get; set; }
        public int Priority { get; set; }
    }
}
