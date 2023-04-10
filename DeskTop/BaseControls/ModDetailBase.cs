using DevExpress.XtraEditors;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.Xpo;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Base;
using Desktop.AppLogic;
using BlitzerCore.Utilities;
using System;

namespace Desktop.BaseControls
{
    public partial class ModDetailBase : XtraUserControl
    {
        public object EditObject { get; set; }
        readonly BlitzerMainForm parent;
        bool dirty = false;
        //bool createNewObject = false;
        //CloseDetailForm closeForm = null;
        readonly DialogResult returnResult = DialogResult.Cancel;
        RibbonPage page = null;
        readonly object Id = null;
        bool readOnly = false;
        public object ID { get { return Id; } }
        public ModDetailBase()
        {
            InitializeComponent();
        }
        public virtual string ModuleName { get { return "ModDetailBasea"; } }
        public ModDetailBase(Form parent,  object editObject = null) : this()
        {
            this.parent = parent as BlitzerMainForm;
            this.EditObject = editObject;
            //lcMain.MenuManager = manager;
            //this.closeForm = closeForm;
        }


        public void DisposeModule()
        {
            string FuncName = $"ModDetailBase::DisposeModule -> {ModuleName}";
            Logger.EnterFunction (FuncName);
            base.Dispose();
            Logger.LeaveFunction(FuncName);
        }

        public RibbonPage ActiveRibbonPage
        {
            get { return page; }
        }
        public bool ReadOnly
        {
            get { return readOnly; }
            set
            {
                readOnly = value;
                UpdateReadOnlyData();
            }
        }
        public virtual void Save() { }
        protected virtual void UpdateReadOnlyData()
        {
            //if (page != null)
            //{
            //    foreach (RibbonPageGroup group in page.Groups)
            //    {
            //        UpdateItemLinks(group, ReadOnly);
            //        group.Visible = !IsEmpty(group);
            //    }
            //}
            //SetDefaultOptions();
            //if (ReadOnly)
            //{
            //    foreach (Control item in lcMain.Controls)
            //    {
            //        BaseEdit be = item as BaseEdit;
            //        if (be != null) be.Properties.ReadOnly = true;
            //    }
            //    ActiveRibbonPage.Text = string.Format(ConstStrings.Get("ReadOnlyCaption"), ActiveRibbonPage.Text);
            //}
        }
        static bool IsEmpty(RibbonPageGroup group)
        {
            //foreach (BarButtonItemLink link in group.ItemLinks)
            //    if (link.Visible) return false;
            //return true;
            return false;
        }
        void SetDefaultOptions()
        {
            //if (LayoutManager == null) return;
            //foreach (Control item in lcMain.Controls)
            //{
            //    //ucGridEditBar editBar = item as ucGridEditBar;
            //    //if (editBar != null)
            //    //{
            //    //    editBar.SetAllowEditing(LayoutManager.Properties.CurrentProperty.DefaultEditGridViewInDetailForms && !ReadOnly);
            //    //    if (ReadOnly) editBar.DasabledButtons();
            //    //}
            //}
        }
        public object LayoutManager
        //public FormLayoutManager LayoutManager
        {
            get
            {
                //if (parent is IFormWithLayoutManager)
                //    return ((IFormWithLayoutManager)parent).LayoutManager;
                return null;
            }
        }
        static void UpdateItemLinks(RibbonPageGroup group, bool ReadOnly)
        {
            //foreach (BarButtonItemLink link in group.ItemLinks)
            //    if ("ReadOnly".Equals(link.Item.Tag))
            //        link.Visible = !ReadOnly;
        }
        public void CreateActiveRibbonPage(RibbonPage page)
        {
            //this.page = page.Clone() as RibbonPage;
            //UpdateActiveRibbonPageCaption();
            //ActiveRibbonPage.Tag = this;
            //page.Ribbon.Pages.Add(ActiveRibbonPage);
            //page.Category.Pages.Add(ActiveRibbonPage);
            // todo: Uncomment this for buttons
            //foreach (RibbonPageGroup group in ActiveRibbonPage.Groups)
            //    foreach (BarButtonItemLink link in group.ItemLinks)
            //        if (link.Item.Caption == ParentFormMain.HomeButton.Caption && HomeButtonCaption != string.Empty)
            //        {
            //            if (link.Item.Hint != string.Empty)
            //            {
            //                link.UserDefine = BarLinkUserDefines.Caption;
            //                link.UserCaption = HomeButtonCaption;
            //            }
            //        }
        }
        protected virtual string HomeButtonCaption { get { return string.Empty; } }

        public virtual bool Dirty
        {
            get
            {
                return dirty;
            }
            set
            {
                dirty = value;
                UpdateActiveRibbonPageCaption();
            }
        }


        public void UpdateActiveRibbonPageCaption()
        {
            //if (ActiveRibbonPage == null) return;
            //ActiveRibbonPage.Text = string.Format("{0}{1}", PageCaption, Dirty ? "*" : string.Empty);
        }
        public string PageCaption
        {
            get
            {
                int maxLength = 70;
                if (Text.Length <= maxLength)
                    return Text;
                return string.Format("{0}...", Text.Substring(0, maxLength));
            }
        }

        public BlitzerMainForm ParentFormMain { get { return parent as BlitzerMainForm; } }

        private void ModDetailBase_Load(object sender, System.EventArgs e)
        {
            string FuncName = $"ModDetailBase::Load -> {ModuleName}";
            Logger.EnterFunction(FuncName);
            base.OnLoad(e);
            InitData();
            Logger.LeaveFunction(FuncName);
        }
        protected virtual void SetEditObject( object editObject)
        {
            InitData();
        }
        protected virtual void InitValidation() { }

        protected virtual void InitData() { }
        public void Close()
        {
            BeforeModuleClose();
            if (returnResult != DialogResult.OK)
            {
                //if (showCloseDialog && Dirty)
                //{
                //    DialogResult result = XtraMessageBox.Show(this, ConstStrings.Get("CloseCancelFormWarning"), ConstStrings.Get("Warning"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                //    if (result == DialogResult.Cancel) return;
                //    if (result == DialogResult.Yes) Save();
                //}
                //else
                //{
                //    CloseCancelForm();
                //}
            }
            Cursor.Current = Cursors.Default;
            //if (closeForm != null) closeForm(returnResult, editObject);
            this.Dispose();
        }

        protected virtual void BeforeModuleClose()
        {
            foreach (Control ctrl in lcMain.Controls)
            {
                GridControl grid = ctrl as GridControl;
                if (grid != null)
                {
                    ColumnView view = grid.MainView as ColumnView;
                    if (view == null) continue;
                    view.CloseEditor();
                    view.UpdateCurrentRow();
                }
            }
        }

        protected void AddControl(Control item)
        {
            //BaseEdit edit = item as BaseEdit;
            //if (edit != null)
            //{
            //    edit.MenuManager = lcMain.MenuManager;
            //    edit.EditValueChanged += new System.EventHandler(edit_EditValueChanged);
            //}
            //IEditsContainer userControl = item as IEditsContainer;
            //if (userControl != null)
            //{
            //    userControl.EditValueChanged += new EventHandler(edit_EditValueChanged);
            //}
            GridControl grid = item as GridControl;
            if (grid != null && lcMain != null)
            {
                grid.MenuManager = lcMain.MenuManager;
            }
        }
        //protected override void OnLoad(EventArgs e)
        //{
        //    base.OnLoad(e);
        //    SetEditObject(externalSession, editObject);
        //    InitValidation();
        //    if (parent != null)
        //        this.Location = new Point(parent.Left + (parent.Width - this.Width) / 2, parent.Top + (parent.Height - this.Height) / 2);
        //    LoadFormLayout();
        //    SetDefaultOptions();
        //    foreach (Control item in lcMain.Controls)
        //    {
        //        AddControl(item);
        //    }
        //}
    }
}
