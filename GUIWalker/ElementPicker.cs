using System;
using System.Collections.Generic;
using System.Windows.Automation;
using System.Linq;
using System.Diagnostics;

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


        private static int PrioritizeElementEntries(ElementEntry left, ElementEntry right)
        {
            if (left == null)
            {
                return right == null ? 0 : -1;
            }
            else if (right == null)
            {
                return 1;
            }
            else
            {
                if (left.Priority == right.Priority)
                    return 0;
                return left.Priority > right.Priority ? 1 : -1;
            }
            Debug.Assert(false);
            return 0;
        }

        //comparison conversion between AutomationElement and ElementEntry objects.
        public bool ElementIsEntry(AutomationElement element, ElementEntry entry)
        {
            if (element.Current.Name == entry.Name
                && element.Current.AutomationId == entry.AutomationId
                && element.Current.ControlType == entry.mControlType
                )
                return true;
            return false;
        }



        List<ElementEntry> mElementTracker = new List<ElementEntry> { };
        public AutomationElement GetElementPriority(AutomationElementCollection inCollection)
        {
            //gather the current elements in the GUI.
            List<ElementEntry> currentElements = new List<ElementEntry> { };
            foreach (AutomationElement iter in inCollection)
            {
                if (SkipElement(iter))
                    continue;

                var elementLog =
                    from myElem in mElementTracker
                    where (ElementIsEntry(iter, myElem))
                    select myElem;

                if (elementLog.Count() == 0)
                { //new element
                    mElementTracker.Add(new ElementEntry { Name = iter.Current.Name, AutomationId = iter.Current.AutomationId, mControlType = iter.Current.ControlType, Priority = 1 });
                }
                if (elementLog.Count() <= 1)
                { //recover old element
                    currentElements.Add(elementLog.First());
                }
                else
                { //multiple matches to element. Add more criteria.
                    Debug.Assert(false);
                }
            }

            //sort current elements by rank.
            //currentElements.Sort(PrioritizeElementEntries);
            foreach (var elem in currentElements.OrderBy(p => p.Priority))
            {
                StreamManager.WriteLine(elem.Name + " " + elem.AutomationId + " " + elem.Priority);
            }


            int sumPriority = 0;
            foreach (ElementEntry elem in currentElements)
            {
                sumPriority += elem.Priority;
            }

            //randomly choose a ElementEntry weighted by priority.
            AutomationElement returnElement = null;
            int randomVal = rand.Next(sumPriority);
            int count = randomVal;
            foreach (ElementEntry myEntry in currentElements)
            {
                if (count < myEntry.Priority && returnElement == null)
                {
                    myEntry.Priority--;
                    var elementLog =
                    from AutomationElement myElem in inCollection
                    where (ElementIsEntry(myElem, myEntry))
                    select myElem;
                    StreamManager.WriteLine("CHOOSE: " + elementLog.First().Current.Name + " " + elementLog.First().Current.AutomationId);
                    myEntry.Priority = 1;
                    returnElement = elementLog.First();
                }
                else
                {
                    count -= myEntry.Priority;
                    myEntry.Priority++;
                }

                    var updateLog =
                    from myElem in mElementTracker
                    where (myElem.Name == myEntry.Name)
                    select myElem;
                
                    updateLog.First().Priority = myEntry.Priority;
            }

            return returnElement;
        }

        //names of elements we intend not to interact with.
        private static string[] skipNames = {
                                           "Close",
                                           "Exit",
                                           "Cancel",
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
