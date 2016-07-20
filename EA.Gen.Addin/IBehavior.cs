using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA.Gen.Addin
{
    public interface IItem
    {
        /// <summary>
        /// Name of the item. when the item is an IMenu, a "-" is prepended for Sparx
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Locations that the item should be enabled for 
        /// Can be TreeView, MainMenu or Diagram.</param>
        /// </summary>
        string[] Locations { get; }
    }

    /// <summary>
    /// An intermediate menu item 
    /// </summary>
    public interface IMenu : IItem
    {
        IItem[] Children { get; }
    }

    /// <summary>
    /// An item that provides behaviour 
    /// </summary>
    public interface IBehavior : IItem
    {
        /// <summary>
        /// Type of elements that this behavior is for
        /// </summary>
        string[] Types { get; }
    }
}
