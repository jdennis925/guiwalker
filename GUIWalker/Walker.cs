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
    class Walker
    {
        private Process pApp = null;
        private AutomationElement ParentWindow = null;
        private ElementPicker elemPicker = new ElementPicker();

        public void DFSwalk()
        {

            //pApp = Process.Start(@"C:\Program Files\Microsoft Games\Minesweeper\MineSweeper.exe");
            //pApp = Process.Start(@"D:\Users\Dennis\Desktop\Hackathon\guitesting\WpfApplication1\WpfApplication1\bin\Debug\WpfApplication1.exe");
            //pApp = Process.Start(@"D:\Users\Dennis\Visual Studio 2010\Projects\MockApplication\MockApplication\bin\Debug\MockApplication.exe");
            //pApp = Process.Start(@"D:\Users\Dennis\RPCPro.NET_local\RPCPro .NET\Bin\ProjectManager.exe");
           // while (pApp.MainWindowHandle == IntPtr.Zero)
           //    Thread.Sleep(1000);
           // AutomationElement ParentWindow = AutomationElement.FromHandle(pApp.MainWindowHandle);




            Random rand = new Random();
            while (true)
            {
                AutomationElementCollection ElementCollection = ParentWindow.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.IsEnabledProperty, true));


                //AutomationElement iter = elemPicker.GetElementRandom(ElementCollection);
                AutomationElement iter = elemPicker.GetElementPriority(ElementCollection);

                if (iter == null)
                    continue;

                AutomationPattern[] validPatterns = iter.GetSupportedPatterns();
                if (validPatterns.Count() > 0)
                {
                    int patternIndex = rand.Next(0, validPatterns.Count() - 1);
                    PatternManager.executePattern(iter, validPatterns[patternIndex]);

                    try
                    {
                        StreamManager.WriteLine(validPatterns[patternIndex].ProgrammaticName + " " + iter.Current.Name.ToString(), true, false);
                        StreamManager.WriteLine(iter.Current.Name + " " + iter.Current.AutomationId + " " + validPatterns[patternIndex].ProgrammaticName, false);
                    }
                    catch (Exception e)
                    {
                    }

                }
            }

        }

    }
}
