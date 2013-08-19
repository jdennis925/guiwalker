using System.Windows.Automation;

namespace GUIWalker
{
    class PatternManager
    {
        public void executePattern(AutomationElement subject, AutomationPattern inPattern)
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
            }
        }
    }
}
