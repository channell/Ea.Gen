using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace EA.Gen.Model.Test
{
    /// <summary>
    /// Summary description for DiagramTest
    /// </summary>
    [TestClass]
    public class DiagramTest
    {
        public DiagramTest()
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

        private EA.Gen.Model.Sparx _context;
        [TestInitialize]
        public void MyTestInitialize()
        {
            _context = new Sparx();
        } 
        [TestMethod]
        public void TestDiagram()
        {
            foreach (var v in from r in _context.Diagrams.Include("Elements") select r)
            {
                Console.WriteLine("Diagram {0} in package {1}", v.Name, v.Package.Name);
                foreach (var o in from r in v.Elements select r)
                {
                    Console.WriteLine("\tElement {0} from {1}", o.Element.Name, o.Element.package.Name);
                }
            }
        }
    }
}
