using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Windows.Automation;
using System.Runtime.InteropServices;
using System.Windows;


namespace GUIWalker
{
    class Program
    {
        static void Main(string[] args)
        {
            Walker myWalker = new Walker();
            myWalker.DFSwalk();
            System.Console.ReadLine();
        }

    }
}
