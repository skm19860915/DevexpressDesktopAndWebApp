using System;
using System.Collections.Generic;
using BlitzerCore.Utilities;
using Desktop.BaseControls;
using Desktop.AppLogic;
using System.Reflection;
using System.Collections;
using DevExpress.XtraNavBar;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Ribbon;
using Desktop.Modules;

namespace Desktop
{
    partial class BlitzerMainForm
    {
        public class ViewController
        {
            ModuleBase mModuleBase = null;
            public BlitzerMainForm Parent { get; set; }
            List<ModuleBase> Modules { get; set; }
            public ModuleBase ActiveModule
            {
                get { return mModuleBase; }
                set
                {
                    mModuleBase = value;
                    if (value == null)
                        Logger.LogWarning("ActiveModule was just set to NULL");
                    else
                        Logger.LogDebug("Active Module was just set to " + value.ModuleName);
                }
            }
            public string InActiveModuleCaption { get; set; }
            ModuleBase StaleModule { get; set; }
            ModDetailBase ActiveDetailMod { get; set; }
            private static ViewController instance = null;

            public static ViewController Instance
            {
                get
                {
                    if (instance == null)
                        instance = new ViewController();

                    return instance;
                }
            }

            private ViewController()
            //private ViewController(BlitzerMainForm aParent)
            {
                Modules = new List<ModuleBase>();
            }

            public bool ShowModule(ModDetailBase aModule)
            {
                const string FuncName = "ViewController::ShowModule(ModDetailBase)";
                Logger.EnterFunction(FuncName);
                Logger.LogDebug($"{aModule.ModuleName} 1 is Disposed [{aModule.IsDisposed}]");

                if (ActiveModule == null)
                {
                    Logger.LogWarning("It is not possible to swtich to detail view because there is no active module");
                    Logger.LeaveFunction(FuncName);
                    return false;
                }
                Logger.LogTracing($"ViewController::ShowModule for => DetailMod = {aModule.ModuleName} ActiveMod = {ActiveModule.ModuleName}");

                ActiveModule.ShowModuleDialog(aModule);
                Logger.LogDebug($"{aModule.ModuleName} 2 is Disposed [{aModule.IsDisposed}]");

                // Clear up from the Old Module

                // 12/28 - Don't want to dispose of the module because we need to selected Object
                //       - Thinking is to keep the module around for the current user
                //InActiveModuleCaption = ActiveModule.ModuleName;
                //Parent.pnlControl.Controls.Remove(ActiveModule);
                //ActiveModule.DisposeModule();
                //ActiveModule = null;

                ActiveDetailMod = aModule;
                Logger.LogDebug($"{aModule.ModuleName} 3 is Disposed [{aModule.IsDisposed}]");

                // Adds the User Control to the panel 
                Parent.pnlControl.Controls.Add(ActiveDetailMod);
                Logger.LogDebug($"{aModule.ModuleName} 4 is Disposed [{aModule.IsDisposed}]");

                ActiveDetailMod.Dock = DockStyle.Fill;
                Parent.rpgMain.Visible = true;

                //-----Set----
                ActiveDetailMod.Visible = true;
                Logger.LogTracing($"   Bringing to the front -> {aModule.ModuleName}");
                ActiveDetailMod.BringToFront();
                Logger.LeaveFunction(FuncName);
                return true;
            }

            internal void CloseDetail(bool aActivateParentModule = true)
            {
                if (aActivateParentModule == true)
                    ShowModule(InActiveModuleCaption);
                if (ActiveDetailMod != null)
                {
                    Logger.LogTracing("ViewController::CloseDetail -> " + ActiveDetailMod.ModuleName);
                    ActiveDetailMod.Dispose();
                    ActiveDetailMod = null;
                }
            }

            public void ShowModule(ModuleBase aModule)
            {
                var lStaleModule = ActiveModule;
                ActiveModule = aModule;
            }

            public void ShowModule(string name)
            {
                string FuncName = $"ViewController::ShowModule(string {name})";
                Logger.EnterFunction(FuncName);

                ModuleInfo item = ModulesInfo.GetItem(name);
                if (item == null)
                {
                    Logger.LogWarning($"ViewController::ShowModule Module Name = {name} return null Module");
                    Logger.LeaveFunction(FuncName);
                    return;
                }

                Cursor currentCursor = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    ModuleBase oldModule = null;
                    ModuleBase lModule = null;

                    // Switching from One Module to Another
                    if (ActiveModule != null)
                    {
                        Logger.LogDebug("Current ActiveModule = " + ActiveModule.ModuleName);
                        if (ActiveModule.Name == name)
                        {
                            lModule = item.TModule as ModuleBase;
                            lModule.Visible = true;
                            Logger.LogDebug($"Bringing {lModule.ModuleName} Module to the front");
                            lModule.BringToFront();
                            Logger.LeaveFunction(FuncName);
                            return;
                        }
                        oldModule = ActiveModule;
                        if (oldModule != null)
                        {
                            Logger.LogTracing($"Removing OldModule [{oldModule.ModuleName}] from PnlControl");
                            Parent.pnlControl.Controls.Remove(oldModule);
                        }

                        // Destory graphical elements
                        oldModule.DisposeModule();
                        oldModule = null;

                    }
                    else
                    {
                        // Close the detail view if open
                        CloseDetail(false);
                    }
                    ActiveModule = item.TModule as ModuleBase;
                    ActiveModule.Bounds = Parent.pnlControl.DisplayRectangle;
                    ActiveModule.Visible = false;
                    // Adds the User Control to the panel 
                    Parent.pnlControl.Controls.Add(ActiveModule);
                    ActiveModule.Dock = DockStyle.Fill;

                    //-----Set----
                    ActiveModule.InitData();
                    ActiveModule.RefreshData();
                    ActiveModule.ModuleName = name;

                    ActiveModule.Visible = true;
                    Logger.LogTracing($"   Bringing Module [{ActiveModule.ModuleName}] to the front");
                    ActiveModule.BringToFront();
                }
                finally
                {
                    Cursor.Current = currentCursor;
                    Logger.LeaveFunction(FuncName);
                }
            }

            public void Register()
            {
                ModulesInfo.Add(Desktop.Modules.Contacts.ModName, Desktop.Modules.Contacts.Caption, typeof(Desktop.Modules.Contacts), Parent.nvbContacts, Parent.rbpCustomer);
                ModulesInfo.Add(Desktop.Modules.Opportunities.ModName, Desktop.Modules.Opportunities.Caption, typeof(Desktop.Modules.Opportunities), Parent.nvbOpportunities, null);
                ModulesInfo.Add(Desktop.Modules.Trips.ModName, Desktop.Modules.Trips.Caption, typeof(Desktop.Modules.Trips), Parent.nvbTrips, null);
                //ModulesInfo.Add(Desktop.Modules.DashBoard.ModName, typeof(Desktop.Modules.DashBoard), "", null, "Home");
                ModulesInfo.Add(Desktop.Modules.ResortPages.ModName, Desktop.Modules.ResortPages.Caption, typeof(Desktop.Modules.ResortPages), Parent.nvbResorts, null);
                ModulesInfo.Add(Desktop.Modules.Countries.ModName, Desktop.Modules.Countries.Caption, typeof(Desktop.Modules.Countries), Parent.nvbCountries, null);
                ModulesInfo.Add(Desktop.Modules.Presentations.ModName, Desktop.Modules.Presentations.Caption, typeof(Desktop.Modules.Presentations), Parent.nvbPresentations, null);
            }

            internal void DisposeModule(ModDetailBase aDetailMod)
            {
                if (aDetailMod != null)
                {
                    Logger.LogTracing($"Removing {aDetailMod.ToString()} from PnlControl");
                    Parent.pnlControl.Controls.Remove(aDetailMod);
                }

                // Destory graphical elements
                aDetailMod.DisposeModule();
                aDetailMod = null;
            }

            private void ActivateEditModule(ModuleBase aBase)
            {
                if (aBase == null)
                {
                    Logger.LogDebug("ViewController::ActivateEditModule - Unable to activate edit module because because is null");
                    return;
                }

                ActiveDetailMod = aBase.Edit();
                if (ActiveDetailMod != null)
                {
                    ShowModule(ActiveDetailMod);
                }
                else
                    Logger.LogWarning($"ViewController::Edit - {ActiveModule.ModuleName} didn't return Detail Module instance");
            }

            public void Edit()
            {
                if (ActiveModule != null)
                {
                    Logger.LogTracing($"ViewController::Edit clicked for {ActiveModule.ModuleName}");
                    ActivateEditModule(ActiveModule);
                }
                // Check if the Module is Inactive because one of the detail windows is active
                else if (InActiveModuleCaption != null && InActiveModuleCaption.Length > 0)
                {
                    CloseDetail();
                    ActivateEditModule(ActiveModule);
                }
                else
                {
                    Logger.LogTracing($"ViewController::Edit clicked, BUT ActiveModule is null");
                }
            }

            public void Add()
            {
                if (ActiveModule != null)
                {
                    Logger.LogTracing($"ViewController::Add clicked for {ActiveModule.ModuleName}");
                    ActiveDetailMod = ActiveModule.Add();
                    if (ActiveDetailMod != null)
                        ShowModule(ActiveDetailMod);
                    else
                        Logger.LogWarning($"ViewController::Add - {ActiveModule.ModuleName} didn't return Detail Module instance");
                }
                else
                {
                    Logger.LogTracing($"ViewController::Add clicked, BUT ActiveModule is null");
                }
            }

            internal void Save()
            {
                if (ActiveDetailMod != null)
                    ActiveDetailMod.Save();
            }

            internal void Close()
            {
                if (ActiveDetailMod != null)
                {
                    CloseDetail();
                    Parent.rpgMain.Visible = false;
                }
            }

            internal void Find()
            {
                ShowModule(Desktop.Modules.Contacts.Caption);
            }

            internal void NewOpportunity()
            {
                ShowModule(Desktop.Modules.Opportunities.Caption);
                Add();
            }

            internal void NewPresentation()
            {
                ShowModule(Desktop.Modules.Presentations.Caption);
            }
        }

        public ViewController ViewManager { get; set; }
    }
}
