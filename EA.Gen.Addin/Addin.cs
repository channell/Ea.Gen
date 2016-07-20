using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EA;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace EA.Gen.Addin
{
    /// <summary>
    /// summary
    /// </summary>
    [Guid ("85C10214-3502-4B50-B01B-608CE83991F8")]
    [ComVisible (true)]
    public class Addin : AbstractAddin 
    {
        // define menu constants
        private const string _Header = "-Deutsche Bank";
        private string[] _subMenus =
        {
            "JIRA Export Requirements",
            "JIRA Import Change Requests",
            "JIRA Import Status",
            "HP ALM Export Requirements",
            "HP ALM Import Coverage",
        };
        const string menuJIRAExport = "JIRA Export Requirements";
        const string menuJIRAImport = "JIRA Import Change Requests";
        const string menuJIRAStatus = "JIRA Import Status";
        const string menuTDExport = "HP ALM Export Requirements";
        const string menuTDImport = "HP ALM Import Coverage";

        public Addin ()
        {
        }

        private Repository _repository;
        private EA.Collection _model;
        private EA.Element _currentElement;
        private string _id;
        private ObjectType _objectType;

        ///
        /// Called Before EA starts to check Add-In Exists
        /// Nothing is done here.
        /// This operation needs to exists for the addin to work
        ///
        /// <param name="Repository" />the EA repository
        /// a string
        public override String EA_Connect (EA.Repository Repository)
        {
            System.Diagnostics.Debugger.Log(1, ">>Connect", "Connect");
            _repository = Repository;
            //No special processing required.
            return "MDG";
        }
 
        ///
        /// Called when user Clicks Add-Ins Menu item from within EA.
        /// Populates the Menu with our desired selections.
        /// Location can be "TreeView" "MainMenu" or "Diagram".
        ///
        /// <param name="Repository" />the repository
        /// <param name="Location" />the location of the menu
        /// <param name="MenuName" />the name of the menu
        ///
        public override object EA_GetMenuItems(EA.Repository Repository, string Location, string MenuName)
        {
            System.Diagnostics.Debugger.Log(1, ">>GetMenuItems", String.Format ("Location:{0}, Menu:{1}", Location , MenuName));
            return base.EA_GetMenuItems(Repository, Location, MenuName);
        }
 
        ///
        /// Called once Menu has been opened to see what menu items should active.
        ///
        /// <param name="Repository" />the repository
        /// <param name="Location" />the location of the menu
        /// <param name="MenuName" />the name of the menu
        /// <param name="ItemName" />the name of the menu item
        /// <param name="IsEnabled" />boolean indicating whethe the menu item is enabled
        /// <param name="IsChecked" />boolean indicating whether the menu is checked
        public override void EA_GetMenuState(EA.Repository Repository, string Location, string MenuName, string ItemName, ref bool IsEnabled, ref bool IsChecked)
        {
            System.Diagnostics.Debugger.Log(1, ">>GetMenuState", String.Format ("Menu : {0}, Item : {1}, Location:{2} ", MenuName, ItemName,Location));

            if (IsProjectOpen(Repository))
            {
                switch (ItemName)
                {
                    case menuJIRAExport :
                    case menuJIRAImport :
                    case menuJIRAStatus :
                    case menuTDExport :
                    case menuTDImport :
                        if (_objectType  == ObjectType.otPackage || _objectType == ObjectType.otElement) 
                        IsEnabled = true;
                        break;
                    case _Header :
                        IsEnabled = true;
                        break;
                    default :
                        IsEnabled = false;
                        break;
                }
            }
            else
            {
                // If no open project, disable all menu options
                IsEnabled = false;
            }
        }
 
        ///
        /// Called when user makes a selection in the menu.
        /// This is your main exit point to the rest of your Add-in
        ///
        /// <param name="Repository" />the repository
        /// <param name="Location" />the location of the menu
        /// <param name="MenuName" />the name of the menu
        /// <param name="ItemName" />the name of the selected menu item
        public override void EA_MenuClick(EA.Repository Repository, string Location, string MenuName, string ItemName)
        {
            System.Diagnostics.Debugger.Log(1, ">>MenuClick", "Connect");
            switch (ItemName)
            {
                case menuJIRAExport :
                case menuJIRAImport :
                case menuJIRAStatus :
                case menuTDExport :
                case menuTDImport :
                    string  s = String.Format 
                        ( "Location : {0}\nMenu Name : {1}\nItem Name : {2}\nId: {3}"
                        , Location
                        , MenuName
                        , ItemName
                        , _id 
                        );
                    System.Diagnostics.Debugger.Log
                        (1
                        , ">>MenuClick"
                        , String.Format
                            ("Location : {0}\nMenu Name : {1}\nItem Name : {2}\nId: {3}"
                            , Location
                            , MenuName
                            , ItemName
                            , _id
                            )
                        );
                    break;
            }
        }
 
        ///
        /// EA calls this operation when it exists. Can be used to do some cleanup work.
        ///
        public override void EA_Disconnect()
        {
            System.Diagnostics.Debugger.Log(1, ">>Disconnect", "Connect");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            _repository = null;
        }

        /// <summary>
        /// Called when EA start model validation. Just shows a message box
        /// </summary>
        /// <param name="Repository">the repository</param>
        /// <param name="Args">the arguments</param>
        public override void EA_OnStartValidation (EA.Repository Repository, object Args)
        {
            System.Diagnostics.Debugger.Log(1, ">>StartValidation", Args.ToString());
        }

        /// <summary>
        /// Called when EA ends model validation. Just shows a message box
        /// </summary>
        /// <param name="Repository">the repository</param>
        /// <param name="Args">the arguments</param>
        public override void EA_OnEndValidation (EA.Repository Repository, object Args)
        {
            System.Diagnostics.Debugger.Log(1, ">>EndValidation", Args.ToString());
        }

        /// <summary>
        /// called when the selected item changes
        /// This operation will show the guid of the selected element in the eaControl
        /// </summary>
        /// <param name="Repository">the EA.Repository</param>
        /// <param name="id">the guid of the selected item</param>
        /// <param name="ot">the object type of the selected item</param>
        public override void EA_OnContextItemChanged (EA.Repository Repository, string id, EA.ObjectType ot)
        {
            _id = id;
            _objectType = ot;

        }
        /// <summary>
        /// ..
        /// </summary>
        /// <param name="Repository"></param>
        /// <param name="id"></param>
        /// <param name="ot"></param>
        public override void EA_OnNotifyContextItemModified (EA.Repository Repository, string id, EA.ObjectType ot)
        {
            System.Diagnostics.Debugger.Log(1, ">>EA_OnNotifyContextItemModified", string.Format ("Id:{0} Type:{1}", id, ot.ToString()));
        }
    }
}
