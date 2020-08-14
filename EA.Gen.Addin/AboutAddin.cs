using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA.Gen.Addin
{
    /// <summary>
    /// A default sample addin that just loads a dialog
    /// </summary>
    public class AboutAddin : AbstractAddin 
    {
        /// <summary>
        /// Repond to the menu click by opening the dialog
        /// </summary>
        /// <param name="Repository"></param>
        /// <param name="MenuLocation"></param>
        /// <param name="MenuName"></param>
        /// <param name="ItemName"></param>
        public override void EA_MenuClick(Repository Repository, string MenuLocation, string MenuName, string ItemName)
        {
            var f = new AboutBox();
            f.Show();
            base.EA_MenuClick(Repository, MenuLocation, MenuName, ItemName);
        }

    }   
}
