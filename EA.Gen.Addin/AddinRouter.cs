using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Security;
using System.Security.Permissions;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting;
using System.Runtime.InteropServices;

namespace EA.Gen.Addin
{
    /// <summary>
    /// This class is used to provide a dynamically loaded addin that does not need to
    /// be deployed to the client computer.
    /// </summary>
    [ComVisible(true)]
    [Guid("2EFC4D86-18E8-4921-B098-BB3F371E5E8F")]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("EA.Gen.Addin.AddinRouter")]
    [ComDefaultInterfaceAttribute(typeof(IAddin))]
    public class AddinRouter : AbstractAddin, ISponsor
    {
        /// <summary>
		/// Reference to the real addin
		/// </summary>
        private Dictionary<string,IAddin> _reals;
        private Model.Customisation _model;
        private EA.ObjectType _lastType;
        private string _lastGUID;
        private AppDomain _domain;
        private List<ILease> _leases;

        /// <summary>
        /// Initialise the maps
        /// </summary>
        public void Init (EA.Repository repo)
        {
            try
            {
                if (repo != null && IsProjectOpen(repo))
                {
                    var xml = repo.SQLQuery("SELECT Script FROM t_script WHERE Notes like '%EA-Gen-Addin%JavaScript%'");

                    if (xml.Contains('{'))
                    {
                        var json = xml.Substring(xml.IndexOf('{'));
                        json = json.Substring(0, json.LastIndexOf('}') + 1);
                        Logger.Write(String.Format("loading addins using definition {0}", json), "Startup", 1);
                        _model = ConfigSerialiser.ToGraph(json);

                        Logger.Write("Init", "Startup", 3);

                        PermissionSet ps1 = new PermissionSet(PermissionState.Unrestricted);
                        ps1.AddPermission(new SecurityPermission(SecurityPermissionFlag.AllFlags));
                        var ads = new AppDomainSetup();
                        var s = ads.ApplicationBase;
                        ads.ApplicationBase = _model.Path;
                        ads.DisallowApplicationBaseProbing = false;
                        ads.DisallowCodeDownload = false;
                        ads.PrivateBinPath = _model.Path;
                        
                        // switch the config file if one provided
                        if (_model.Config != null)
                            ads.ConfigurationFile = _model.Config;

                        _domain = AppDomain.CreateDomain("EA.Gen.Addin", null, ads, ps1);

                        try
                        {
                            foreach (var v in _model.Addins)
                            {
                                var o = _domain.CreateInstanceAndUnwrap(_model.Assembly, v);
                                var lease = RemotingServices.GetLifetimeService((MarshalByRefObject)o) as ILease;
                                if (lease != null)
                                {
                                    lease.Register(this);
                                    _leases.Add(lease);
                                }
                                var addin = (IAddin)o;
                                _reals.Add(v, addin);
                            }
                        }
                        catch (Exception e)
                        {
                            if (Environment.UserInteractive)
                                System.Windows.Forms.MessageBox.Show("Assembly " + _model.Assembly + " not found", "EA.Gen.Addin not found", System.Windows.Forms.MessageBoxButtons.OK);
                            Logger.Write(e.Message, "Startup", 10);
                        }
                        Logger.Write("Completed Connect", "Startup", 3);
                    }
                    else
                        Logger.Write("Invalid Script", "Startup", 3);
                }
            }
            catch (Exception e)
            {
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin script format error", System.Windows.Forms.MessageBoxButtons.OK);
                Logger.Write(e.Message, "Startup", 10);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        static AddinRouter() 
        {
            try
            {
                Logger.SetLogWriter(new LogWriterFactory().Create());
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK);
            }
        }

        #region EAAddinBase

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override string EA_Connect(EA.Repository Repository)
        {
            _reals = new Dictionary<string, IAddin>();
            _reals.Add("EA.Gen.Addin.AboutAddin", new AboutAddin());
            _model = new Model.Customisation
                (new Model.Menu("EA.Gen.Addin", "EA.Gen.Addin.AboutAddin")
                , "EA.Gen.Addin.AboutAddin"
                , new string[] { "EA.Gen.Addin.AboutAddin" }
                , null
                );
            return "MDG";
        }
        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override object EA_GetMenuItems(EA.Repository Repository, string MenuLocation, string MenuName)
        {
            if (_model != null)
            {
                var m = _model.Menu.Find(MenuName);
                if (MenuName == null || MenuName == "" && m != null) 
                {
                    if (m.HasContents)
                        return "-" + m.Name;
                    else
                        return m.Name;
                }
                else if (m != null)
                {
                    return m.GetContent(MenuLocation, _lastType).ToArray();
                }
            }
            return "";
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override void EA_GetMenuState(EA.Repository Repository, string Location, string MenuName, string ItemName, ref bool IsEnabled, ref bool IsChecked)
        {
            if (IsProjectOpen(Repository))
            {
                var m = _model.Menu.Find(MenuName, Location, _lastType)
                                   .Find(ItemName, Location, _lastType);
                if (m != null)
                    IsEnabled = true;
                else
                    IsEnabled = false;
            }
            else
            {
                // If no open project, disable all menu options
                IsEnabled = false;
            }
        }

        /// <summary>
        /// Execute the addin associated with this option
        /// </summary>
        /// <param name="Repository"></param>
        /// <param name="MenuLocation"></param>
        /// <param name="MenuName"></param>
        /// <param name="ItemName"></param>
        public override void EA_MenuClick(EA.Repository Repository, string MenuLocation, string MenuName, string ItemName)
        {
            var m = _model.Menu.Find(MenuName, MenuLocation, _lastType)
                               .Find(ItemName, MenuLocation, _lastType);
            IAddin a;
            try
            {
                if (_reals.TryGetValue(m.Addin, out a))
                    a.EA_MenuClick(Repository, MenuLocation, MenuName, ItemName);
            }
            catch (System.Runtime.Remoting.RemotingException re)
            {

            }

            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override void EA_Disconnect()
        {
            try
            { 
                foreach (var r in _reals) r.Value.EA_Disconnect();
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override void EA_OnOutputItemClicked(EA.Repository Repository, string TabName, string LineText, long ID)
        {
            try
            { 
                foreach (var r in _reals) r.Value.EA_OnOutputItemClicked(Repository, TabName, LineText, ID);
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override void EA_OnOutputItemDoubleClicked(EA.Repository Repository, string TabName, string LineText, long ID)
        {
            try
            { 
                foreach (var r in _reals) r.Value.EA_OnOutputItemDoubleClicked(Repository, TabName, LineText, ID);
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
        }
        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override void EA_ShowHelp(EA.Repository Repository, string MenuLocation, string MenuName, string ItemName)
        {
            try
            { 
                foreach (var r in _reals) r.Value.EA_ShowHelp(Repository, MenuLocation, MenuName, ItemName);
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override void EA_FileOpen(EA.Repository Repository)
        {
            _reals = new Dictionary<string, IAddin>();
            _leases = new List<ILease>();
            Init(Repository);

            // always have at least one addin
            if (_reals.Count == 0)
                EA_Connect(Repository);
            try
            {
                foreach (var r in _reals) r.Value.EA_FileOpen(Repository);
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override void EA_FileClose(EA.Repository Repository)
        {
            try
            { 
                foreach (var r in _reals) r.Value.EA_FileClose(Repository);
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
            if (_domain != null)
            {
                AppDomain.Unload(_domain);
            }
            EA_Connect(Repository);
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override void EA_FileNew(EA.Repository Repository)
        {
            try
            { 
                foreach (var r in _reals) r.Value.EA_FileNew(Repository);
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override void EA_OnPostCloseDiagram(EA.Repository Repository, int DiagramID)
        {
            try
            { 
                foreach (var r in _reals) r.Value.EA_OnPostCloseDiagram(Repository, DiagramID);
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override void EA_OnPostOpenDiagram(EA.Repository Repository, int DiagramID)
        {
            try
            { 
                foreach (var r in _reals) r.Value.EA_OnPostOpenDiagram(Repository, DiagramID);
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override bool EA_OnPreDeleteElement(EA.Repository Repository, EA.EventProperties Info)
        {
            try
            {
                foreach (var r in _reals)
                {
                    if (!r.Value.EA_OnPreDeleteElement(Repository, Info))
                        return false;
                }
                return true;
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
                return false;
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override bool EA_OnPreDeleteAttribute(EA.Repository Repository, EA.EventProperties Info)
        {
            try
            {
                foreach (var r in _reals)
                {
                    if (!r.Value.EA_OnPreDeleteAttribute(Repository, Info))
                        return false;
                }
                return true;
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
                return false;
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override bool EA_OnPreDeleteMethod(EA.Repository Repository, EA.EventProperties Info)
        {
            try
            {
                foreach (var r in _reals)
                {
                    if (!r.Value.EA_OnPreDeleteMethod(Repository, Info))
                        return false;
                }
                return true;
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
                return false;
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override bool EA_OnPreDeleteConnector(EA.Repository Repository, EA.EventProperties Info)
        {
            try
            {
                foreach (var r in _reals)
                {
                    if (!r.Value.EA_OnPreDeleteConnector(Repository, Info))
                        return false;
                }
                return true;
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
                return false;
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override bool EA_OnPreDeleteDiagram(EA.Repository Repository, EA.EventProperties Info)
        {
            try
            {
                foreach (var r in _reals)
                {
                    if (!r.Value.EA_OnPreDeleteDiagram(Repository, Info))
                        return false;
                }
                return true;
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
                return false;
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override bool EA_OnPreDeletePackage(EA.Repository Repository, EA.EventProperties Info)
        {
            try
            {
                foreach (var r in _reals)
                {
                    if (!r.Value.EA_OnPreDeletePackage(Repository, Info))
                        return false;
                }
                return true;
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
                return false;
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override bool EA_OnPreDeleteGlossaryTerm(EA.Repository Repository, EA.EventProperties Info)
        {
            try
            {
                foreach (var r in _reals)
                {
                    if (!r.Value.EA_OnPreDeleteGlossaryTerm(Repository, Info))
                        return false;
                }
                return true;
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
                return false;
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override bool EA_OnPreNewElement(EA.Repository Repository, EA.EventProperties Info)
        {
            try
            {
                foreach (var r in _reals)
                    if (!r.Value.EA_OnPreNewElement(Repository, Info))
                        return false;
                return true;
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
                return false;
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override bool EA_OnPreNewConnector(EA.Repository Repository, EA.EventProperties Info)
        {
            try
            {
                foreach (var r in _reals)
                    if (!r.Value.EA_OnPreNewConnector(Repository, Info))
                        return false;
                return true;
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
                return false;
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override bool EA_OnPreNewDiagram(EA.Repository Repository, EA.EventProperties Info)
        {
            try
            {
                foreach (var r in _reals)
                    if (!r.Value.EA_OnPreNewDiagram(Repository, Info))
                        return false;
                return true;
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
                return false;
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override bool EA_OnPreNewDiagramObject(EA.Repository Repository, EA.EventProperties Info)
        {
            try
            {
                foreach (var r in _reals)
                    if (!r.Value.EA_OnPreNewDiagramObject(Repository, Info))
                        return false;
                return true;
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
                return false;
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override bool EA_OnPreNewAttribute(EA.Repository Repository, EA.EventProperties Info)
        {
            try
            {
                foreach (var r in _reals)
                    if (!r.Value.EA_OnPreNewAttribute(Repository, Info))
                        return false;
                return true;
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
                return false;
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override bool EA_OnPreNewMethod(EA.Repository Repository, EA.EventProperties Info)
        {
            try
            {
                foreach (var r in _reals)
                    if (r.Value.EA_OnPreNewMethod(Repository, Info))
                        return false;
                return true;
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
                return false;
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override bool EA_OnPreNewPackage(EA.Repository Repository, EA.EventProperties Info)
        {
            try
            {
                foreach (var r in _reals)
                    if (!r.Value.EA_OnPreNewPackage(Repository, Info))
                        return false;
                return true;
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
                return false;
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override bool EA_OnPreNewGlossaryTerm(EA.Repository Repository, EA.EventProperties Info)
        {
            try
            {
                foreach (var r in _reals)
                    if (!r.Value.EA_OnPreNewGlossaryTerm(Repository, Info))
                        return false;
                return true;
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
                return false;
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override void EA_OnPreExitInstance(EA.Repository Repository)
        {
            try
            {
                foreach (var r in _reals) r.Value.EA_OnPreExitInstance(Repository);
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override bool EA_OnPostNewElement(EA.Repository Repository, EA.EventProperties Info)
        {
            try
            {
                foreach (var r in _reals)
                    if (!r.Value.EA_OnPostNewElement(Repository, Info))
                        return false;
                return true;
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
                return false;
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override bool EA_OnPostNewConnector(EA.Repository Repository, EA.EventProperties Info)
        {
            try
            {
                foreach (var r in _reals)
                    if (!r.Value.EA_OnPostNewConnector(Repository, Info))
                        return false;
                return true;
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
                return false;
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override bool EA_OnPostNewDiagram(EA.Repository Repository, EA.EventProperties Info)
        {
            try
            {
                foreach (var r in _reals)
                    if (!(r.Value.EA_OnPostNewDiagram(Repository, Info)))
                        return false;
                return true;
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
                return false;
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override bool EA_OnPostNewDiagramObject(EA.Repository Repository, EA.EventProperties Info)
        {
            try
            {
                foreach (var r in _reals)
                    if (!r.Value.EA_OnPostNewDiagramObject(Repository, Info))
                        return false;
                return true;
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
                return false;
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override bool EA_OnPostNewAttribute(EA.Repository Repository, EA.EventProperties Info)
        {
            try
            {
                foreach (var r in _reals)
                    if (!r.Value.EA_OnPostNewAttribute(Repository, Info))
                        return false;
                return true;
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
                return false;
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override bool EA_OnPostNewMethod(EA.Repository Repository, EA.EventProperties Info)
        {
            try
            {
                foreach (var r in _reals)
                    if (!r.Value.EA_OnPostNewMethod(Repository, Info))
                        return false;
                return true;
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
                return false;
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override bool EA_OnPostNewPackage(EA.Repository Repository, EA.EventProperties Info)
        {
            try
            {
                foreach (var r in _reals)
                    if (!r.Value.EA_OnPostNewPackage(Repository, Info))
                        return false;
                return true;
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
                return false;
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override bool EA_OnPostNewGlossaryTerm(EA.Repository Repository, EA.EventProperties Info)
        {
            try
            {
                foreach (var r in _reals)
                    if (!r.Value.EA_OnPostNewGlossaryTerm(Repository, Info))
                        return false;
                return true;
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
                return false;
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override void EA_OnPostInitialized(EA.Repository Repository)
        {
            try
            {
                foreach (var r in _reals) r.Value.EA_OnPostInitialized(Repository);
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override bool EA_OnPostTransform(EA.Repository Repository, EA.EventProperties Info)
        {
            try
            {
                foreach (var r in _reals)
                    if (!r.Value.EA_OnPostTransform(Repository, Info))
                        return false;
                return true;
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
                return false;
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override object EA_OnInitializeTechnologies(EA.Repository Repository)
        {
            try
            {
                foreach (var r in _reals)
                {
                    var v = r.Value.EA_OnInitializeTechnologies(Repository);
                    if (v != null)
                        return v;
                }
                return null;
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
                return null;
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override bool EA_OnPreActivateTechnology(EA.Repository Repository, EA.EventProperties Info)
        {
            try
            {
                foreach (var r in _reals)
                    if (!r.Value.EA_OnPreActivateTechnology(Repository, Info))
                        return false;
                return true;
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
                return false;
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override bool EA_OnPostActivateTechnology(EA.Repository Repository, EA.EventProperties Info)
        {
            try
            {
                foreach (var r in _reals)
                    if (!r.Value.EA_OnPostActivateTechnology(Repository, Info))
                        return false;
                return true;
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
                return false;
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override void EA_OnContextItemChanged(EA.Repository Repository, string GUID, EA.ObjectType ot)
        {
            _lastGUID = GUID;
            _lastType = ot;
            try
            {
                foreach (var r in _reals) r.Value.EA_OnContextItemChanged(Repository, GUID, ot);
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
        }
        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override bool EA_OnContextItemDoubleClicked(EA.Repository Repository, string GUID, EA.ObjectType ot)
        {
            try
            {
                foreach (var r in _reals)
                    if (!r.Value.EA_OnContextItemDoubleClicked(Repository, GUID, ot))
                        return false;
                return true;
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
                return false;
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override void EA_OnNotifyContextItemModified(EA.Repository Repository, string GUID, EA.ObjectType ot)
        {
            try
            {
                foreach (var r in _reals) r.Value.EA_OnNotifyContextItemModified(Repository, GUID, ot);
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override object EA_QueryAvailableCompartments(EA.Repository Repository)
        {
            try
            {
                foreach (var r in _reals)
                {
                    var v = r.Value.EA_QueryAvailableCompartments(Repository);
                    if (v != null)
                        return v;
                }
                return null;
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
                return null;
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override object EA_GetCompartmentData(EA.Repository Repository, string sCompartment, string sGUID, EA.ObjectType oType)
        {
            try
            {
                foreach (var r in _reals)
                {
                    var v = r.Value.EA_GetCompartmentData(Repository, sCompartment, sGUID, oType);
                    if (v != null)
                        return v;
                }
                return null;
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
                return null;
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override void EA_OnInitializeUserRules(EA.Repository Repository)
        {
            try
            {
                foreach (var r in _reals) r.Value.EA_OnInitializeUserRules(Repository);
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override void EA_OnStartValidation(EA.Repository Repository, object Args)
        {
            try
            {
                foreach (var r in _reals) r.Value.EA_OnStartValidation(Repository, Args);
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override void EA_OnEndValidation(EA.Repository Repository, object Args)
        {
            try
            {
                foreach (var r in _reals) r.Value.EA_OnEndValidation(Repository, Args);
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
        }
        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override void EA_OnRunElementRule(EA.Repository Repository, string RuleID, EA.Element Element)
        {
            try
            {
                foreach (var r in _reals) r.Value.EA_OnRunElementRule(Repository, RuleID, Element);
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override void EA_OnRunPackageRule(EA.Repository Repository, string RuleID, long PackageID)
        {
            try
            {
                foreach (var r in _reals) r.Value.EA_OnRunPackageRule(Repository, RuleID, PackageID);
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override void EA_OnRunDiagramRule(EA.Repository Repository, string RuleID, long DiagramID)
        {
            try
            {
                foreach (var r in _reals) r.Value.EA_OnRunDiagramRule(Repository, RuleID, DiagramID);
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override void EA_OnRunConnectorRule(EA.Repository Repository, string RuleID, long ConnectorID)
        {
            try
            {
                foreach (var r in _reals) r.Value.EA_OnRunConnectorRule(Repository, RuleID, ConnectorID);
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override void EA_OnRunAttributeRule(EA.Repository Repository, string RuleID, string AttributeGUID, long ObjectID)
        {
            try
            {
                foreach (var r in _reals) r.Value.EA_OnRunAttributeRule(Repository, RuleID, AttributeGUID, ObjectID);
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override void EA_OnRunMethodRule(EA.Repository Repository, string RuleID, string MethodGUID, long ObjectID)
        {
            try
            {
                foreach (var r in _reals) r.Value.EA_OnRunMethodRule(Repository, RuleID, MethodGUID, ObjectID);
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override void EA_OnRunParameterRule(EA.Repository Repository, string RuleID, string ParameterGUID, string MethodGUID, long ObjectID)
        {
            try
            {
                foreach (var r in _reals) r.Value.EA_OnRunParameterRule(Repository, RuleID, ParameterGUID, MethodGUID, ObjectID);
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override void EA_OnAttributeTagEdit(EA.Repository Repository, long AttributeID, ref string TagName, ref string TagValue, ref string TagNotes)
        {
            try
            {
                foreach (var r in _reals) r.Value.EA_OnAttributeTagEdit(Repository, AttributeID, ref TagName, ref TagValue, ref TagNotes);
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override void EA_OnConnectorTagEdit(EA.Repository Repository, long ConnectorID, ref string TagName, ref string TagValue, ref string TagNotes)
        {
            try
            {
                foreach (var r in _reals) r.Value.EA_OnConnectorTagEdit(Repository, ConnectorID, ref TagNotes, ref TagValue, ref TagNotes);
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
        }


        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override void EA_OnElementTagEdit(EA.Repository Repository, long ObjectID, ref string TagName, ref string TagValue, ref string TagNotes)
        {
            try
            {
                foreach (var r in _reals) r.Value.EA_OnElementTagEdit(Repository, ObjectID, ref TagName, ref TagValue, ref TagNotes);
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override void EA_OnMethodTagEdit(EA.Repository Repository, long MethodID, ref string TagName, ref string TagValue, ref string TagNotes)
        {
            try
            {
                foreach (var r in _reals) r.Value.EA_OnMethodTagEdit(Repository, MethodID, ref TagName, ref TagValue, ref TagNotes);
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override string EA_OnRetrieveModelTemplate(EA.Repository Repository, string sLocation)
        {
            try
            {
                foreach (var r in _reals)
                {
                    var s = r.Value.EA_OnRetrieveModelTemplate(Repository, sLocation);
                    if (s != null)
                        return s;
                }
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
            return null;
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override void MDG_BuildProject(EA.Repository Repository, string PackageGuid)
        {
            try
            {
                foreach (var r in _reals) r.Value.MDG_BuildProject(Repository, PackageGuid);
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override long MDG_Connect(EA.Repository Repository, long PackageID, string PackageGuid)
        {
            try
            {
                foreach (var r in _reals)
                {
                    var v = r.Value.MDG_Connect(Repository, PackageID, PackageGuid);
                    if (v != 0)
                        return v;
                }
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
            return 0;
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override long MDG_Disconnect(EA.Repository Repository, string PackageGuid)
        {
            try
            {
                foreach (var r in _reals)
                {
                    var v = r.Value.MDG_Disconnect(Repository, PackageGuid);
                    if (v != 0)
                        return v;
                }
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
            return 0;
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override object MDG_GetConnectedPackages(EA.Repository Repository)
        {
            try
            {
                foreach (var r in _reals)
                {
                    var v = r.Value.MDG_GetConnectedPackages(Repository);
                    if (v != null)
                        return v;
                }
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
            return null;
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override object MDG_GetProperty(EA.Repository Repository, string PackageGuid, string PropertyName)
        {
            try
            {
                foreach (var r in _reals)
                {
                    var v = r.Value.MDG_GetProperty(Repository, PackageGuid, PropertyName);
                    if (v != null)
                        return v;
                }
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
            return null;
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override long MDG_Merge(EA.Repository Repository, string PackageGuid, ref object SynchObjects,
                                      ref string SynchType, ref object ExportObjects, ref object ExportFiles,
                                      ref object ImportFiles, ref string IgnoreLocked, ref string Language)
        {
            try
            {
                foreach (var r in _reals)
                {
                    var v = r.Value.MDG_Merge
                        (Repository
                        , PackageGuid
                        , ref SynchObjects
                        , ref SynchType
                        , ref ExportObjects
                        , ref ExportFiles
                        , ref ImportFiles
                        , ref IgnoreLocked
                        , ref Language);
                    if (v != 0)
                        return v;
                }
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
            return 0;
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override string MDG_NewClass(EA.Repository Repository, string PackageGuid, string CodeID, ref string Language)
        {
            try
            {
                foreach (var r in _reals)
                {
                    var v = r.Value.MDG_NewClass(Repository, PackageGuid, CodeID, ref Language);
                    if (v != null)
                        return v;
                }
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
            return null;
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override long MDG_PostGenerate(EA.Repository Repository, string PackageGuid, string FilePath, string FileContents)
        {
            try
            {
                foreach (var r in _reals)
                {
                    var v = r.Value.MDG_PostGenerate(Repository, PackageGuid, FilePath, FileContents);
                    if (v != 0)
                        return v;
                }
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
            return 0;
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override long MDG_PostMerge(EA.Repository Repository, string PackageGuid)
        {
            try
            {
                foreach (var r in _reals)
                {
                    var v = r.Value.MDG_PostMerge(Repository, PackageGuid);
                    if (v != 0)
                        return v;
                }
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
            return 0;
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override long MDG_PreGenerate(EA.Repository Repository, string PackageGuid)
        {
            try
            {
                foreach (var r in _reals)
                {
                    var v = r.Value.MDG_PreGenerate(Repository, PackageGuid);
                    if (v != 0)
                        return v;
                }
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
            return 0;
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override long MDG_PreMerge(EA.Repository Repository, string PackageGuid)
        {
            try
            {
                foreach (var r in _reals)
                {
                    var v = r.Value.MDG_PreMerge(Repository, PackageGuid);
                    if (v != 0)
                        return v;
                }
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
            return 0;
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override void MDG_PreReverse(EA.Repository Repository, string PackageGuid, object FilePaths)
        {
            try
            {
                foreach (var r in _reals) r.Value.MDG_PreReverse(Repository, PackageGuid, FilePaths);
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override void MDG_RunExe(EA.Repository Repository, string PackageGuid)
        {
            try
            {
                foreach (var r in _reals) r.Value.MDG_RunExe(Repository, PackageGuid);
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
        }

        /// <see cref="EA.Gen.Addin.IAddin"/>
        public override long MDG_View(EA.Repository Repository, string PackageGuid, string CodeID)
        {
            try
            {
                foreach (var r in _reals)
                {
                    var v = r.Value.MDG_View(Repository, PackageGuid, CodeID);
                    if (v != 0)
                        return v;
                }
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, "AddinRouter", 1);
                if (Environment.UserInteractive)
                    System.Windows.Forms.MessageBox.Show(e.Message, "EA.Gen.Addin", System.Windows.Forms.MessageBoxButtons.OK);
            }
            return 0;
        }

        /// <summary>
        /// Sponsor the renewal of the remote connections to addins
        /// </summary>
        /// <param name="lease"></param>
        /// <returns></returns>
        public TimeSpan Renewal(ILease lease)
        {
            return TimeSpan.FromMinutes(5);
        }
        #endregion
    }
}
