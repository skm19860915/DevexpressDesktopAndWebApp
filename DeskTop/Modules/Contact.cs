using DevExpress.XtraEditors;
using System;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Desktop.BaseControls;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Business;
using BlitzerCore.Utilities;
using BlitzerCore.DataAccess;
using Desktop.DataServices;
using Desktop.AppLogic;
using Desktop.ModuleDetail;

namespace Desktop.Modules
{
    public partial class Contacts : Desktop.BaseControls.ModuleBase
    {
        public static string ModName = "ContactsModule";
        public static string Caption = "Contacts";
        public Contacts()
        {
            Logger.LogTracing("ContactModule::ctor");
            InitializeComponent();
        }

        private void Contacts_Load(object sender, EventArgs e)
        {
            Logger.LogTracing("ContactsModule::Load");
            RefreshGridDataSource();
        }

        private void uIClientBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
        protected override bool AllowEdit(GridHitInfo info)
        {
            return true;
        }

        public override ModDetailBase Details()
        {
            Logger.LogDebug("Contact::Add - Called");
            base.Details();
            return AddContact();
        }

        public override ModDetailBase Edit()
        {
            Logger.LogDebug("Contact::Edit - Called");
            base.Edit();
            return AddContact();
        }
        public override ModDetailBase GridDoubleClick() {
            Logger.LogDebug("Contact::GridDoubleClick - Called");
            base.Details();
            return ViewContact();
        }
        protected override bool IsDetailExist(string id)
        {
            return false;
        }

        ModDetailBase AddContact()
        {
            Logger.LogTracing("ContactModule::AddContact creating ContactAddModule");
            return new ContactEdit(this.FindForm(), this.CurrentContact);
        }

        ModDetailBase EditContact()
        {
            Logger.LogTracing("ContactModule::EditContact creating ContactDetailModule");
            return new ContactEdit(this.FindForm(), this.CurrentContact);
        }

        ModDetailBase ViewContact()
        {
            Logger.LogTracing("ContactModule::EditContact creating ContactDetailModule");
            return new ContactDetail(this.FindForm(), this.CurrentContact);
        }
        public override string EditObjectName { get { return ConstStrings.Get("Customer"); } }
        public override string EditObjectsName { get { return ConstStrings.Get("Customers"); } }
        protected override void RefreshGridDataSource()
        {
            grdControl.DataSource = new ContactBusiness(RepositoryContext.Instance).GetAll(Blitzer.Instance.CurrentUser() as Agent);
            //WinHelper.GridViewFocusObject(MainView, current);
        }
        protected override void BeginRefreshData()
        {
            base.BeginRefreshData();
            RefreshGridDataSource();
        }
        protected override ColumnView MainView { get { return advBandedGridView1; } }
        protected override ColumnView CurrentView { get { return grdControl.MainView as ColumnView; } }
        //protected override ColumnView AlternateView { get { return grdControl; } }
        protected override Object CurrentEditObject { get { return CurrentContact; } }
        private UIContact CurrentContact
        {
            get
            {
                if (CurrentView == null || CurrentView.FocusedRowHandle < 0) return null;
                var lUser = CurrentView.GetRow(CurrentView.FocusedRowHandle);
                Logger.LogDebug("Contact::CurrentContact = " + (lUser as UIContact).Id);
                return lUser as UIContact;
            }
        }
    }
}
