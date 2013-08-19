using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Windows.Automation;
using System.Runtime.InteropServices;
using System.Windows;
using System.IO;

namespace GUIWalker
{
    class ElementPicker
    {
        private Random rand = new Random();

        public AutomationElement GetElementRandom(AutomationElementCollection inSet)
        {
            if (inSet.Count == 0)
                return null;
            int index = rand.Next(0, inSet.Count - 1);
            if (SkipElement(inSet[index]))
                return null;
            return inSet[index];
        }

        //names of elements we intend not to interact with.
        private static string[] skipNames = {
                                           "Close",
                                           "Exit",
                                           "Minimize",
                                           "Maximize",
										   "Import",
										   "Data Set...",
										   "Workspace...",
                                           "System",
                               };
        //function for pruning elements we do not wish to interact with.
        private bool SkipElement(AutomationElement inElement)
        {
            if (skipNames.Contains(inElement.Current.Name))
                return true;
            return false;
        }
    }
}
