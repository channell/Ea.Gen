using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA.Gen.Sample
{
    public class Class2 : EA.Gen.Addin.AbstractAddin
    {
        public override void EA_MenuClick(Repository Repository, string MenuLocation, string MenuName, string ItemName)
        {
            EA.Gen.Model.Sparx repo = new Model.Sparx();

            var s = "";

            foreach (var q in from r in repo.Packages
                              select r.Name)
            {
                s += ", " + q; 
            }
            s = s.Substring(1);

            AboutBox1 box = new AboutBox1(s);
            box.Show();
        }
    }
}
