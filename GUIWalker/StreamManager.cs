using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace GUIWalker
{
   
    static public  class StreamManager
    {
        static private string DesktopLoc = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\HackLog\\WalkerLog.txt";


        static public void WriteLine(string s, bool console = true, bool file = true)
        {
            if(console)
                Console.WriteLine(s);
            if (file)
            {
                StreamWriter mLog = new StreamWriter(DesktopLoc, true);
                mLog.WriteLine(s);
                mLog.Close();
            }
        }
    }
}
