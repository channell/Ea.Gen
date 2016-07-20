using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace EA.Gen.Model.Test
{
    /// <summary>
    /// Summary description for ElementTest
    /// </summary>
    [TestClass]
    public class ElementTest
    {
        public ElementTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestInitialize()]
        public void MyTestInitialize()
        {
            _context = new Sparx();
        }

        private Sparx _context;

        [TestMethod]
        public void TestElement()
        {
            foreach (var v in from r in _context.Packages
                              where r.ParentId == 0
                              select r)
            {
                Dump(v, "");
            }
            //
            // TODO: Add test logic here
            //
        }

        private void Dump(Element e, string tabs)
        {
            TestContext.WriteLine("{0}E {1} {2} {3}", tabs, e.Id, e.Name, e.ObjectType);
        }
        private void Dump(Package e, string tabs)
        {
            TestContext.WriteLine("{0}P {1} {2}", tabs, e.Id, e.Name);

            foreach (var v in from r in e.Children select r)
            {
                Dump(v, tabs + "\t");
            }
            foreach (var v in from r in e.Elements select r)
            {
                Dump(v, tabs + "\t");
            }
        }

        [TestMethod]
        public void TestParent ()
        {
            var q = from row in _context.Elements
                    where row.ParentID != 0 && row.ParentID != null
                    select row;

            var a = q.ToArray();

            foreach (var v in a)
            {
                var p = v.Parent;

                Console.WriteLine("Element {0} has parent {1}", v.Name, v.Parent.Name);
                foreach (var c in v.Parent.Children)
                {
                    Console.WriteLine("\t which has children {0}", c.Name);
                }
            }
        }
        [TestMethod]
        public void TablesAndColumns()
        {
            foreach (var t in from r in _context.Elements
                              where r.ObjectType == "Class"
                              && r.Stereotype == "table"
                              select r)
            {
                Console.WriteLine("Table {0}", t.Name);

                foreach (var a in from r in t.Attributes
                                  select r)
                {
                    Console.WriteLine("\tColumn {0} {1}", a.Name, a.Type);
                }
            }
        }
        [TestMethod]
        public void TablesAndOps()
        {
            foreach (var t in from r in _context.Elements
                              where r.ObjectType == "Class"
                              && r.Stereotype == "table"
                              select r)
            {
                Console.WriteLine("Table {0}", t.Name);

                foreach (var a in from r in t.Operations 
                                  select r)
                {
                    Console.WriteLine("\tOperation {0} {1}", a.Name, a.Type);
                }
            }
        }
    }
}
