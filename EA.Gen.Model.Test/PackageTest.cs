using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using System.Linq;

namespace EA.Gen.Model.Test
{
    /// <summary>
    /// Summary description for Package
    /// </summary>
    [TestClass]
    public class PackageTest
    {
        public PackageTest()
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
        public void TestRoot()
        {
            var root = from r in _context.Packages
                       where r.ParentId == null || r.ParentId == 0
                       select r;
            var res = root.ToArray();

            foreach (var v in res)
            {
                TestContext.WriteLine("Root packages in repository {0} {1} {2}", v.Id, v.Name, v.ParentId);
            }
        }

        [TestMethod]
        public void TestChildren ()
        {
            foreach (var v in from r in _context.Packages
                              where r.ParentId == 0
                              select r)
            {
                DumpChildren(v, "");
            }
        }

        public void DumpChildren (Package p, string tabs)
        {
            TestContext.WriteLine("{3} {0} {1} {2} {3}", p.Id, p.Name, p.ParentId, tabs, p.ea_guid);

            foreach (var c in from r in p.Children select r)
            {
                DumpChildren(c, tabs + "\t");
            }
        }

    }
}
