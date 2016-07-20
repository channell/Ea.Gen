using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Reflection;

namespace EA.Gen.Addin.Model
{
    /// <summary>
    /// Create a customisation graph for the EA.Gen.Addin to use to add other addins
    /// </summary>
    [DataContract]
    public class Customisation
    {
        /// <summary>
        /// default constructor for serialisation
        /// </summary>
        public Customisation() { }

        /// <summary>
        /// Create a customidsation using the full set of parameters
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="assembly"></param>
        /// <param name="addins"></param>
        /// <param name="config"></param>
        public Customisation (Menu menu, string assembly, string[] addins, string config)
        {
            Menu = menu;
            Assembly = assembly;
            Addins = addins;
            Config = config;
        }

        /// <summary>
        /// Menu is the base menu item that appears from the Sparx Extentions menu
        /// </summary>
        [DataMember(IsRequired =true, Order =0)]
        public Menu Menu;


        /// <summary>
        /// 
        /// </summary>
        [DataMember(IsRequired =true, Order=1)]
        public string Path;

        /// <summary>
        /// Name of the assembly that contains the addins
        /// </summary>
        [DataMember(IsRequired =true,Order =2)]
        public string Assembly;

        /// <summary>
        /// list of addin names that are refered to in the menu
        /// Each addin gets notified of all events, so there should be no pulication between them
        /// </summary>
        [DataMember(IsRequired =true, Order = 3)]
        public string[] Addins;

        /// <summary>
        /// Path to a configuraiton file with properties used by the addins
        /// </summary>
        [DataMember(IsRequired =true, Order =4)]
        public string Config;
    }
}
