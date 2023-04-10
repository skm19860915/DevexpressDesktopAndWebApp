using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Xpo;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.Utils.Menu;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraLayout;
using DevExpress.XtraCharts;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraBars.Ribbon;
using DevExpress.LookAndFeel;
using System.Drawing.Imaging;
using DevExpress.XtraCharts.Printing;
using DevExpress.Data;
using DevExpress.XtraPrinting.Links;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using BlitzerCore.Utilities;
using Desktop.AppLogic;
using System.Reflection;
using System.Collections;
using DevExpress.XtraNavBar;

namespace Desktop.BaseControls
{
    public partial class ModuleBase : BusinessModuleBase
    {
        RibbonPage ribbonPage = null;
        ModuleBase parent = null;
        ModuleDetailCollection moduleDetailCollection = null;
        public virtual string EditObjectName { get { return string.Empty; } }
        public virtual string EditObjectsName { get { return string.Empty; } }
        protected virtual ColumnView CurrentView { get { return MainView; } }
        protected virtual ColumnView MainView { get { return null; } }
        protected virtual ColumnView AlternateView { get { return null; } }
        protected virtual DockManager MainDockManager { get { return null; } }
        protected virtual LayoutControl MainLayoutControl { get { return null; } }
        protected virtual Object CurrentEditObject { get { return null; } }
        protected internal virtual ViewType ChartViewType { get { return ViewType.Bar; } }
        protected virtual bool AllowRotateLayout { get { return false; } }
        protected virtual bool AllowFlipLayout { get { return false; } }

        internal void DisposeModule()
        {
            var lModule = ModulesInfo.GetItem(ModuleName);
            if ( lModule != null )
            {
                lModule.ModuleDispose();
            } else
            {
                Logger.LogDebug("ModuleBase::DisposeModule - Unable to display of ModuleInfo module because it couldn't be found in collection");
            }

            Logger.LogTracing($"ModuleBase::DisposeModule - {ModuleName}");
            base.Dispose();
        }

        public ModuleDetailCollection ModuleDetailCollection
        {
            get
            {
                if (moduleDetailCollection == null)
                    moduleDetailCollection = new ModuleDetailCollection(this);
                return moduleDetailCollection;
            }
        }
        public ModuleBase()
        {
            InitializeComponent();
        }
        public IDXMenuManager MenuManager
        {
            get
            {
                if (parent == null) return null;
                return parent.MenuManager;
            }
        }
        public string DetailTypeName
        {
            get
            {
                if (ActiveDetailControl != null) return ActiveDetailControl.GetType().Name;
                return null;
            }
        }
        public bool IsSuitablePage(RibbonPage page)
        {
            if (page.Tag == null) return false;
            if (ModuleDetailCollection != null)
            {
                foreach (ModDetailBase detail in ModuleDetailCollection)
                    if (detail.ActiveRibbonPage == page) return true;
            }
            return ActiveRibbonPage == page;
        }
        public RibbonMenuController RibbonMenuController
        {
            get
            {
                if (parent != null) return parent.RibbonMenuController;
                return null;
            }
        }
        public string TypeName { get { return GetType().Name; } }

        public string ModuleName { get; set; }

        public void SetParent(Form parent)
        {
            //if (this.ParentFormMain == parent) return;
            //this.ParentFormMain = parent as frmMain;
            //if (parent != null)
            //{
            //    AddMenuManager(this.ParentFormMain.MenuManager);
            //    this.RibbonMenuController.AddPageForControl(this);
            //}
            DoParentChanged();
        }

        //public frmMain ParentFormMain
        //{
        //    get { return parent; }
        //    set
        //    {
        //        if (parent != null) return;
        //        parent = value;
        //    }
        //}

        protected virtual void BeginRefreshData()
        {
            Cursor.Current = Cursors.WaitCursor;
        }

        public void RefreshData()
        {
            RefreshGridDataSource();
        }
        protected virtual void DoParentChanged()
        {
            RefreshGridDataSource();
        }
        protected virtual void RefreshGridDataSource() { }
        public ModuleBase ParentFormMain
        {
            get { return parent; }
            set
            {
                if (parent != null) return;
                parent = value;
            }
        }
        public virtual void InitData()
        {
            if (MainView != null)
            {
                MainView.FocusedRowChanged += new FocusedRowChangedEventHandler(MainView_FocusedRowChanged);
                MainView.ColumnFilterChanged += new EventHandler(MainView_ColumnFilterChanged);
                if (MainView.GridControl != null)
                {
                    MainView.GridControl.MouseDoubleClick += new MouseEventHandler(GridControl_MouseDoubleClick);
                    MainView.GridControl.KeyDown += new KeyEventHandler(GridControl_KeyDown);
                }
                else
                    Logger.LogWarning("ModuleBase::InitData - MainView.GridControl was null which prevented the mouse double click and key down handlers being registered");

                MainView.OptionsBehavior.Editable = false;
                ((DevExpress.XtraGrid.Views.Grid.GridView)MainView).OptionsSelection.EnableAppearanceFocusedCell = false;
            }
            if (AlternateView != null)
            {
                AlternateView.FocusedRowChanged += new FocusedRowChangedEventHandler(MainView_FocusedRowChanged);
                AlternateView.ColumnFilterChanged += new EventHandler(MainView_ColumnFilterChanged);
            }
        }
        void GridControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (MainView.OptionsBehavior.Editable == false && CurrentEditObject != null && e.KeyCode == Keys.Enter)
                DoEdit();
        }
        void DoEdit()
        {
            Logger.LogTracing("ModuleBase::DoEdit");
            Cursor.Current = Cursors.WaitCursor;
            var lDetailMod = Edit();
            if (lDetailMod != null) {
                if (Desktop.BlitzerMainForm.ViewController.Instance.ShowModule(lDetailMod) == false)
                    Desktop.BlitzerMainForm.ViewController.Instance.DisposeModule(lDetailMod);
            }
            else
                Logger.LogWarning("ModuleBase::DoEdit - Edit Detail Module is null");
            Cursor.Current = Cursors.Default;
        }
        void DoDoubleClick()
        {
            Logger.LogTracing("ModuleBase::DoEdit");
            Cursor.Current = Cursors.WaitCursor;
            var lDetailMod = GridDoubleClick();
            if (lDetailMod != null)
            {
                if (Desktop.BlitzerMainForm.ViewController.Instance.ShowModule(lDetailMod) == false)
                    Desktop.BlitzerMainForm.ViewController.Instance.DisposeModule(lDetailMod);
            }
            else
                Logger.LogWarning("ModuleBase::DoEdit - Edit Detail Module is null");
            Cursor.Current = Cursors.Default;
        }
        void GridControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            const string FuncName = "MouseDoubleClick";
            Logger.EnterFunction(FuncName);
            try
            {
                Logger.LogDebug("ModuleBase::MouseDoubleClick");
                GridView gv = ((GridControl)sender).MainView as GridView;
                if (gv != null)
                {
                    GridHitInfo info = gv.CalcHitInfo(new Point(e.X, e.Y));
                    if (CurrentEditObject == null || !info.InRow || AllowEdit(info) == false)
                    {
                        Logger.LogWarning("CurrentEditObject was null, so can't can't DetailModule");
                        return;
                    }
                    DoDoubleClick();
                }
                else
                {
                    Logger.LogDebug("ModuleBase::MouseDoubleClick - GridView IS NULL");
                    ColumnView cv = ((GridControl)sender).MainView as ColumnView;
                    if (cv != null)
                    {
                        if (CurrentEditObject == null) return;
                        DoDoubleClick();
                    }
                }
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }
        protected virtual bool IsDetailExist(string id)
        {
            return false;
        }
        protected virtual bool IsDetailExist(int id)
        {
            return false;
        }

        public virtual ModDetailBase Add() { return null; }
        public virtual ModDetailBase Edit() { return null; }
        public virtual ModDetailBase GridDoubleClick() { return Edit(); }
        public virtual ModDetailBase Details() { return null; }
        protected internal virtual bool Delete()
        {
            //if (CurrentEditObject == null) return false;
            //if (IsDetailExist(CurrentEditObject.Oid)) return false;
            //if (XtraMessageBox.Show(this.FindForm(),
            //    string.Format(ConstStrings.Get("DeleteRecord"), EditObjectName), ConstStrings.Get("Question"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            //{
            //    bool ret = ObjectHelper.SafeDelete(this.FindForm(), CurrentEditObject, true);
            //    return ret;
            //}
            return false;
        }

        protected virtual bool AllowEdit(GridHitInfo info)
        {
            return false;
        }
        void MainView_ColumnFilterChanged(object sender, EventArgs e)
        {
            DoFocusedRowChanged();
        }
        void MainView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            DoFocusedRowChanged();
        }
        protected virtual void DoFocusedRowChanged()
        {
            //SetCurrentObject(CurrentEditObject);
        }
        //protected override void SetControlManager(Control ctrl, IDXMenuManager manager)
        //{
        //    GridControl grid = ctrl as GridControl;
        //    if (grid != null) grid.MenuManager = manager;
        //    LayoutControl layout = ctrl as LayoutControl;
        //    if (layout != null) layout.MenuManager = manager;
        //    PivotGridControl pGrid = ctrl as PivotGridControl;
        //    if (pGrid != null) pGrid.MenuManager = manager;
        //    SchedulerControl scheduler = ctrl as SchedulerControl;
        //    if (scheduler != null) scheduler.MenuManager = manager;
        //}
        //protected override void UpdateMenu()
        //{
        //    base.UpdateMenu();
        //    if (RibbonMenuController != null) RibbonMenuController.UpdateMenu();
        //}
        //protected override void CreateDetailRibbon()
        //{
        //    base.CreateDetailRibbon();
        //    if (RibbonMenuController != null) RibbonMenuController.CreateDetailRibbon();
        //}
        //protected override void DoShow()
        //{
        //    //base.DoShow();
        //    RibbonMenuController.CurrentControl = this;
        //    SetCurrentObject(CurrentEditObject);
        //    RibbonMenuController.CalcCloseAllDetails();
        //    LoadFormLayout();
        //    if (CurrentView != null)
        //    {
        //        CurrentView.GridControl.ForceInitialize();
        //        CurrentView.Focus();
        //    }
        //    AllowExport();
        //    EnableSimpleActions();
        //}
        public void ShowModuleDialog(ModDetailBase module)
        {
            ShowModuleDialog(module, false);
        }
        public RibbonPage ActiveRibbonPage
        {
            get { return ribbonPage; }
            set
            {
                if (ribbonPage != null) return;
                ribbonPage = value;
            }
        }
        public void ShowModuleDialog(ModDetailBase module, bool readOnly)
        {
            module.Bounds = this.DisplayRectangle;
            module.Parent = this.Parent;
            module.Dock = DockStyle.Fill;
            // What did this do.
            //ModuleDetailCollection.Add(module as ModDetailBase);
            module.ReadOnly = readOnly;
            
        }
        void UpdateActiveDetailControl()
        {
            if (ActiveDetailControl != null) ShowActiveDetailControlRibbonPage();
        }
        ModDetailBase activeDetailControl = null;
        public ModDetailBase ActiveDetailControl
        {
            get { return activeDetailControl; }
            set
            {
                if (activeDetailControl == value)
                {
                    UpdateActiveDetailControl();
                    return;
                }
                activeDetailControl = value;
                if (ActiveDetailControl == null)
                {
                    if (this.ActiveRibbonPage != null)
                        this.ActiveRibbonPage.Ribbon.SelectedPage = ActiveRibbonPage;
                    RemoveCloseDetailButton();
                }
                else
                {
                    CreateDetailRibbon();
                    UpdateMenu();
                    activeDetailControl.BringToFront();
                    this.SendToBack();
                    ShowActiveDetailControlRibbonPage();
                    AddCloseDetailButton();
                }
            }
        }
        internal void RemoveCloseDetailButton()
        {
            //ParentFormMain.RibbonControl.PageHeaderItemLinks.Remove(ParentFormMain.CloseButton);
        }
        protected virtual void UpdateMenu() { }
        protected virtual void CreateDetailRibbon() { }
        internal void DumpActiveDetailControl()
        {
            activeDetailControl = null;
            RemoveCloseDetailButton();
        }
        internal void AddCloseDetailButton()
        {
            //if (ActiveDetailControl == null) return;
            //if (!RibbonPageHeaderItemLinkCollectionContainsBarItem(ParentFormMain.CloseButton,
            //    ParentFormMain.RibbonControl.PageHeaderItemLinks))
            //    ParentFormMain.RibbonControl.PageHeaderItemLinks.Add(ParentFormMain.CloseButton);
        }
        void ShowActiveDetailControlRibbonPage()
        {
            //ActiveDetailControl.ActiveRibbonPage.Ribbon.SelectedPage = ActiveDetailControl.ActiveRibbonPage;
        }
        void CloseActiveDetailForm()
        {
            ModuleDetailCollection.Remove(ActiveDetailControl);
        }
        protected virtual void CloseDetailForm(DialogResult result, object currentObject)
        {
            CloseActiveDetailForm();
        }
        internal void ShowMasterModule()
        {
            ActiveDetailControl = null;
        }
    }
}
