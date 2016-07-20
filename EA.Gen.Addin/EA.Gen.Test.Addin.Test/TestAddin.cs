using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EA.Gen.Addin.Test
{
    /// <summary>
    /// Summary description for TestAddin
    /// </summary>
    [TestClass]
    public class TestAddin
    {
        private EA.Repository repo = null;

        public TestAddin()
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
            var moqRepo = new Moq.Mock<EA.Repository>();
            var sql = "SELECT Script FROM t_script WHERE Notes like \"%EA.Gen.Addin%JavaScript%\"";
            var json = "{\"Menu\":{\"Name\":\"About EA.Gen.Addin\", \"Path\",:null, \"Addin\":\"EA.Gen.Addin.AboutAddin\"},\"Assembly\":\"EA.Gen.Addin\",\"Addins\":[\"EA.Gen.Addin.AboutAddin\"]}";
            moqRepo
                .Setup(p => p.SQLQuery(sql))
                .Returns(json);
            moqRepo
                .SetupGet(p => p.ConnectionString)
                .Returns("");

            repo = moqRepo.Object;
        }
    
        [TestMethod]
        public void TestConnect()
        {
            var router = new AddinRouter();
            router.EA_Connect(repo);
        }

        [TestMethod]
        public void TestNoJSO()
        {
            var moqRepo = new Moq.Mock<EA.Repository>();
            var sql = "SELECT Script FROM t_script WHERE Notes like \"%EA.Gen.Addin%JavaScript%\"";
            string json = null;
            moqRepo
                .Setup(p => p.SQLQuery(sql))
                .Returns(json);
            moqRepo
                .SetupGet(p => p.ConnectionString)
                .Returns("");
            var router = new AddinRouter();
            router.EA_Connect(moqRepo.Object);
            var s = (string)router.EA_GetMenuItems(moqRepo.Object, "MainMenu", null);
            Console.WriteLine(s);
            router.EA_MenuClick(moqRepo.Object, "MainMenu", null, s);
        }
        [TestMethod]
        public void TestBlankJSON()
        {
            var moqRepo = new Moq.Mock<EA.Repository>();
            var sql = "SELECT Script FROM t_script WHERE Notes like \"%EA.Gen.Addin%JavaScript%\"";
            string json = "</>";
            moqRepo
                .Setup(p => p.SQLQuery(sql))
                .Returns(json);
            moqRepo
                .SetupGet(p => p.ConnectionString)
                .Returns("");

            var router = new AddinRouter();
            router.EA_Connect(moqRepo.Object);
        }
    }
}
