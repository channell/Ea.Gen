using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EA.Gen.Addin.Test
{
    /// <summary>
    /// Summary description for testModel
    /// </summary>
    [TestClass]
    public class testModel
    {

        public testModel()
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
        }

        [TestMethod]
        public void TestSerial()
        {
            var c = new Model.Customisation
                (new Model.Menu
                    ("DB"
                    , new Model.Menu[]
                        {
                            new Model.Menu ("Add", "DB.Adder", new string[] {"Activty","Class"}),
                            new Model.Menu ("Del", "DB.Deleter")
                        }
                    , new string[] {"TreeView", "MainMenu"}
                    )
                , "EA.Gen.Addin"
                , new string[] { "DB.Adder", "DB.Deleter" }
                , "app.config"
                );

            var s = ConfigSerialiser.ToString(c);
            var o = ConfigSerialiser.ToGraph(s);

            Assert.AreEqual(c.Menu.Name, o.Menu.Name);
            Assert.AreEqual<int>(c.Addins.Length, o.Addins.Length);

            Console.WriteLine(s);
        }

        [TestMethod]
        public void TestDeserial ()
        {
            var s = @"
{""Menu"":

    {""Name"":""DB""
	,""Locations"":[""TreeView"",""MainMenu""]
	,""Contains"":
	[
		{""Name"":""Add""
		,""Locations"":null
		,""Contains"":null
		,""Addin"":""DB.Adder""
		,""Types"":[""Activty"",""Class""]
    },
		{""Name"":""Del""
		,""Locations"":null
		,""Contains"":null	
		,""Addin"":""DB.Deleter""
		,""Types"":null
		}
	]
	,""Addin"":null
	,""Types"":null
	}
,""Path"": null
,""Assembly"":""a""
,""Config"":""app.config""
,""Addins"":[""DB.Adder"",""DB.Deleter""]
}
";
            var o = ConfigSerialiser.ToGraph(s);
        }

        [TestMethod]
        public void AboutTest()
        {
            var c = new Model.Customisation
                (new Model.Menu("About EA.Gen.Addin", "EA.Gen.Addin.AboutAddin")
                , "EA.Gen.Addin"
                , new string[] { "EA.Gen.Addin.AboutAddin" }
                , null
                );
            Console.WriteLine(ConfigSerialiser.ToString(c));
        }
        [TestMethod]
        public void TestMenu ()
        {
            var c = new Model.Customisation
                (new Model.Menu("Addin", new Model.Menu[]
                    {new Model.Menu("From Menu", "EA.Gen.Addin.AboutAddin",null, new String[] {"MainMenu" })
                    ,new Model.Menu("From Menu", "EA.Gen.Addin.AboutAddin", null, new String[] {"TreeView" })
                    })
                    , @"C:\Development\Cephei\2.2\EA.Gen.Addin\bin\Debug\EA.Gen.Addin.dll"
                    , new string[] { "EA.Gen.Addin.AboutAddin" }
                    , null
                );

            Console.WriteLine(ConfigSerialiser.ToString(c));
        }
    }
}
