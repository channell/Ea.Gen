using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace EA.Gen.Addin.Model
{
    /// <summary>
    /// Class for the JSON serialisation of a menu definition
    /// </summary>
    [DataContract]
    public class Menu
    {
        /// <summary>
        /// default constructor for object serialisation
        /// </summary>
        public Menu() { }

        /// <summary>
        /// Create a Menu menu item with other menus as content
        /// </summary>
        /// <param name="name"></param>
        /// <param name="content"></param>
        public Menu(string name, Menu[] content)
        {
            Name = name;
            Contains = content;
        }
        /// <summary>
        /// Create a menu with content and location filter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="content"></param>
        /// <param name="locations">TreeView, MainMenu or Diagram</param>
        public Menu(string name, Menu[] content, string[] locations)
        {
            Name = name;
            Contains = content;
        }
        /// <summary>
        /// create a base level menu to invoke an addin
        /// </summary>
        /// <param name="name"></param>
        /// <param name="addin"></param>
        public Menu(string name, string addin)
        {
            Name = name;
            Addin = addin;
        }
        /// <summary>
        /// Create a leaf level menu for an addin with a list of type to filter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="addin"></param>
        /// <param name="types"></param>
        public Menu(string name, string addin, string[] types)
        {
            Name = name;
            Addin = addin;
            Types = types;
        }
        /// <summary>
        /// Create a leaf level menu for an addin with a list of type to filter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="addin"></param>
        /// <param name="types"></param>
        /// <param name="locations"></param>
        public Menu(string name, string addin, string[] types, string[] locations)
        {
            Name = name;
            Addin = addin;
            Types = types;
            Locations = locations;
        }

        /// <summary>
        /// name of the menu item as it appears in Sparx
        /// </summary>
        [DataMember(IsRequired = true, Order = 0)]
        public string Name;

        /// <summary>
        /// Locations that the item should be enabled for 
        /// Can be TreeView, MainMenu or Diagram.
        /// </summary>
        [DataMember(IsRequired = false, Order = 1)]
        public string[] Locations;

        /// <summary>
        /// Menu level memu that contains other items
        /// </summary>
        [DataMember(IsRequired = false, Order = 2)]
        public Menu[] Contains;

        /// <summary>
        /// property to determine whether the menu has content
        /// </summary>
        public bool HasContents
        {
            get
            {
                if (Contains == null)
                    return false;
                return Contains.Length > 0;
            }
        }

        /// <summary>
        /// name of the addin that should be called for this item
        /// </summary>
        [DataMember(IsRequired = false, Order = 3)]
        public string Addin;

        /// <summary>
        /// Type of elements that this behavior is for
        /// </summary>
        [DataMember(IsRequired = false, Order = 4)]
        public string[] Types;

        /// <summary>
        /// property to flag whether the addin filters for types
        /// </summary>
        public bool HasTypes
        {
            get
            {
                if (Types == null)
                    return false;
                return Types.Length > 0;
            }
        }

        private SortedSet<EA.ObjectType> _types;
        /// <summary>
        /// read-only translation of the type list into Sparx object types
        /// </summary>
        public SortedSet<EA.ObjectType> ObjectTypes
        {
            get
            {
                if (_types == null && Types != null)
                {
                    var e = from t in Types
                            orderby t
                            select (EA.ObjectType)Enum.Parse(typeof(EA.ObjectType), "ot" + t, true);
                    _types = new SortedSet<EA.ObjectType>(e);
                }
                return _types;
            }
        }

        /// <summary>
        /// Find a Menu in the Tree that matches this criteria
        /// </summary>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public Menu Find(string name, string location, EA.ObjectType type)
        {
            if (name == null || name == "") return this;
            if ((Locations == null || Locations.Length == 0 || Locations.Contains(location)) &&
                (ObjectTypes == null || ObjectTypes.Count == 0 || ObjectTypes.Contains(type)))
            {
                if (Name == name || "-" + Name == name)
                    return this;
                else if (Contains != null)
                {
                    foreach (var m in Contains)
                    {
                        var r = m.Find(name, location, type);
                        if (r != null)
                            return r;
                    }
                    return null;
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Menu Find(string name)
        {
            if (name == null || name == "") return this;
            if (Name == name || "-" + Name == name)
                return this;
            else if (Contains != null)
            {
                foreach (var m in Contains)
                {
                    var r = m.Find(name);
                    if (r != null)
                        return r;
                }
                return null;
            }
            return null;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public IEnumerable<string> GetContent(string location, EA.ObjectType type)
        {
            foreach (var m in Contains)
            {
                if (m.Locations == null || m.Locations.Length == 0 || m.Locations.Contains(location) &&
                    m.ObjectTypes.Count == 0 || m.ObjectTypes.Contains(type))
                    yield return m.Name;
            }
        }
    }
}
