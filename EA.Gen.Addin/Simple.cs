using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA.Gen.Addin.Simple
{
    public class SimpleItem : IItem
    {
        private string _name;
        public SimpleItem (string i)
        {
            _name = i;
        }
        public string[] Locations
        {
            get
            {
                return new string[] { "TreeView", "MainMenu", "Diagram" };
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }
    }
    public class SimpleMenu : IMenu
    {
        private string _name;
        private IItem[] _items;

        public SimpleMenu (string name, IItem[] i)
        {
            _name = name;
            _items = i;
        }
        public IItem[] Children
        {
            get
            {
                return _items;
            }
        }

        public string[] Locations
        {
            get
            {
                return new string[] { "Class", "Activity" };
            }
        }

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }

    public class SimpleBehavior : AbstractAddin, IBehavior
    {
        private string _name;

        public SimpleBehavior (string name)
        {
            _name = name;
        }

        public string[] Locations
        {
            get
            {
                return new string[] { "TreeView", "MainMenu", "Diagram" };
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public string[] Types
        {
            get
            {
                return new string[] { "Class", "Activity" };
            }
        }
    }
}
