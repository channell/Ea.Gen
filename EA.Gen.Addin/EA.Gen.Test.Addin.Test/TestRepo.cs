using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace EA.Gen.Addin.Test
{
    [TestClass]
    public class TestRepo
    {
        private EA.Repository _repo;
        [TestInitialize]
        public void Setup ()
        {
            _repo = new Repository();
            _repo.OpenFile(@"c:\development\cephei\2.2\quantlib-1.2.1.eap");

        }
        [TestMethod]
        public void EnumerateRepo()
        {
            foreach (var v in from r in EnumColl<EA.Package>(_repo.Models) select r)
            {
                Console.WriteLine(v.Name);
                EnumPack(v.Packages, "\t");
            } 
        }
        public void EnumPack (EA.Collection c, string tabs)
        {
            foreach (var v in from r in EnumColl<EA.Package>(c) select r)
            {
                Console.WriteLine("{0}{1}", tabs, v.Name);
                EnumPack(v.Packages, tabs + "\t");
            }
        }

        public IEnumerable<T> EnumColl<T>(EA.Collection c)
        {
            foreach (var v in c)
            {
                yield return (T)v;
            }
        }
    }
}
