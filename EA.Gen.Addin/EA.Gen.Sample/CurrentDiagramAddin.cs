using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EA.Gen.Addin;
using System.Runtime.InteropServices;

namespace EA.Gen.Sample
{
    /// <summary>
    /// A PoC addin to display the current diagram
    /// </summary>
    public class CurrentDiagram : AbstractAddin
    {
 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Repository"></param>
        /// <param name="MenuLocation"></param>
        /// <param name="MenuName"></param>
        /// <param name="ItemName"></param>
        public override void EA_MenuClick(Repository Repository, string MenuLocation, string MenuName, string ItemName)
        {

            var e = Repository.GetCurrentDiagram();
            System.Windows.Forms.MessageBox.Show(e.Name);
        }
    }
}
