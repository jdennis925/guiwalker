using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Windows.Automation;

namespace GUIWalker
{
    public static class PatternManager
    {
        static public void executePattern(AutomationElement subject, AutomationPattern inPattern, string inValue = null)
        {
            try
            {
                switch (inPattern.ProgrammaticName)
                {
                    case "InvokePatternIdentifiers.Pattern":
                        {
                            InvokePattern invoke = (InvokePattern)subject.GetCurrentPattern(InvokePattern.Pattern);
                            invoke.Invoke();
                            break;
                        }
                    case "SelectionItemPatternIdentifiers.Pattern":
                        {
                            SelectionItemPattern select = (SelectionItemPattern)subject.GetCurrentPattern(SelectionItemPattern.Pattern);
                            select.Select();
                            break;
                        }
                    case "TogglePatternIdentifiers.Pattern":
                        {
                            TogglePattern toggle = (TogglePattern)subject.GetCurrentPattern(TogglePattern.Pattern);
                            toggle.Toggle();
                            break;
                        }
                    case "ExpandCollapsePatternIdentifiers.Pattern":
                        {
                            ExpandCollapsePattern exColPat = (ExpandCollapsePattern)subject.GetCurrentPattern(ExpandCollapsePattern.Pattern);
                            // exColPat.Expand();
                            break;
                        }
                    case "ValuePatternIdentifiers.Pattern":
                        {
                            ValuePattern valpat = (ValuePattern)subject.GetCurrentPattern(ValuePattern.Pattern);
                            if (inValue != null)
                                valpat.SetValue(inValue);
                            break;
                        }
                }
            }
            catch (Exception e)
            {
            }

        }
    }
}
