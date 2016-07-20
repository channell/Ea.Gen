using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace EA.Gen.Addin.Test
{
    [TestClass]
    public class TestSampleFixture
    {
        const string json = @"
{""Menu"":
	{""Name"":""Sampler""
	,""Locations"":null
	,""Contains"":
        [{""Name"":""From Menu"",""Locations"":[""MainMenu""],""Contains"":null,""Addin"":""EA.Gen.Sample.Class2"",""Types"":[]
    }
		,{""Name"":""From Tree"",""Locations"":[""TreeView""],""Contains"":null,""Addin"":""EA.Gen.Sample.Class2"",""Types"":[]
}
		]
	,""Addin"":null
	,""Types"":[]}
, ""Path"":""C:\\Development\\Cephei\\2.2\\EA.Gen.Addin\\EA.Gen.Sample\\bin\\Debug""
,""Assembly"":""EA.Gen.Sample""
,""Addins"":[""EA.Gen.Sample.Class2""]
,""Config"":""EA.Gen.Sample.dll.config""
,""References"":null
}";
/*
        ,""References"":[""EntityFramework""
                ,""EntityFramework.SQLServer""
                ,""NewtonsoftJson""]
*/
        [TestMethod]
        public void TestSample()
        {
            var moqRepo = new Moq.Mock<EA.Repository>();
            var sql = "SELECT Script FROM t_script WHERE Notes like '%EA-Gen-Addin%JavaScript%'";
            moqRepo
                .Setup(p => p.SQLQuery(sql))
                .Returns(json);
            moqRepo
                .SetupGet(p => p.ConnectionString)
                .Returns("");

            var repo = moqRepo.Object;
/*
            var a = Assembly.Load(@"EA.Gen.Sample");
            var t = a.GetType("EA.Gen.Sample.Class2");
            Assert.IsNull(t);
*/
            var ar = new AddinRouter();
            ar.EA_FileOpen(repo);
            ar.EA_MenuClick(null, "MainMenu", "Sampler", "From Menu");
            ar.EA_FileClose(null);
        }
    }
}