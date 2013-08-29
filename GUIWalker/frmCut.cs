using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;
using System.Diagnostics;


namespace GUIWalker
{
    //class for executing events on the cut tool.
    public class frmCut
    {
        private AutomationElement ModelScreen = null;
        private AutomationElement CutWindow = null;

        public frmCut(frmCutOptions options)
        {
            AutomationElementCollection CutCollection = getCutCollection();

            //timehistory && regions
            if (options.inTimeHistory != null || options.inRegions != null)
            {
                AutomationElement inputGroup = null;
                for (int i = 0; i < CutCollection.Count && inputGroup == null; i++)
                {
                    if (CutCollection[i].Current.AutomationId == "fraInputs")
                        inputGroup = CutCollection[i];
                }

                AutomationElementCollection inputChildren = inputGroup.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.IsEnabledProperty, true));
                foreach (AutomationElement inputChild in inputChildren)
                {
                    if (options.inRegions != null && inputChild.Current.AutomationId == "brInputRegions")
                    {
                        AutomationElementCollection regionCollection = inputChild.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.IsEnabledProperty, true));
                        foreach (AutomationElement RegionsItem in regionCollection)
                        {
                            if (RegionsItem.Current.ControlType == ControlType.ListItem && RegionsItem.Current.Name == options.inRegions)
                            {
                                PatternManager.executePattern(RegionsItem, SelectionItemPattern.Pattern);
                                break;
                            }
                        }
                    }


                    if (options.inTimeHistory != null && inputChild.Current.AutomationId == "brInputTH")
                    {
                        AutomationElementCollection thCollection = inputChild.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.IsEnabledProperty, true));
                        foreach (AutomationElement thItem in thCollection)
                        {
                            if (thItem.Current.ControlType == ControlType.Edit)
                            {
                                PatternManager.executePattern(thItem, ValuePattern.Pattern, options.inTimeHistory);
                                break;
                            }
                        }
                    }
                }
            }

            //taperMode
            if (options.taperMode.HasValue)
            {
                string searchString = null;
                switch (options.taperMode.Value)
                {
                    case frmCutOptions.eTaperMode.None:
                        searchString = "None"; break;
                    case frmCutOptions.eTaperMode.Normal:
                        searchString = "Normal"; break;
                    case frmCutOptions.eTaperMode.Extended:
                        searchString = "Extended"; break;
                    case frmCutOptions.eTaperMode.Mean:
                        searchString = "Mean"; break;
                    default:
                        Debug.Assert(false); break;
                }

                foreach (AutomationElement iter in CutCollection)
                {
                    if (iter.Current.Name == searchString)
                    {
                        PatternManager.executePattern(iter, SelectionItemPattern.Pattern);
                        break;
                    }
                }
            }

            //checkboxes
            if (options.TaperEntirely != null)
            {
                //scaffolding
            }


        }


        private AutomationElementCollection getCutCollection()
        {
            AutomationElementCollection DesktopChildren = AutomationElement.RootElement.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.IsWindowPatternAvailableProperty, true));
            foreach (AutomationElement iter in DesktopChildren)
            {
                if (iter.Current.AutomationId == "mdiMain")
                {
                    ModelScreen = iter;
                }
            }
            AutomationElementCollection ModelCollection = ModelScreen.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.IsEnabledProperty, true));
            foreach (AutomationElement iter in ModelCollection)
            {
                if (iter.Current.AutomationId == "frmCut")
                {
                    CutWindow = iter;
                }
            }
            AutomationElementCollection CutCollection = CutWindow.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.IsEnabledProperty, true));
            return CutCollection;
        }

    }

}
