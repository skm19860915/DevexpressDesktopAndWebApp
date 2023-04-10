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
using Desktop;

namespace Desktop.Modules
{
    public partial class Countries : ModuleBase
    {

        public static string ModName = "CountriesModule";
        public static string Caption = "Countries";

        public Countries()
        {
            InitializeComponent();
        }

        private void Contacts_Load(object sender, EventArgs e)
        {
            RefreshGridDataSource();
        }

        private void uIClientBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
        protected override bool AllowEdit(GridHitInfo info)
        {
            return true;
        }

        public override ModDetailBase Edit()
        {
            Logger.LogDebug("Contact::Edit - Called");
            base.Edit();
            return EditCountry();
        }
        protected override bool IsDetailExist(string id)
        {
            return false;
        }

        ModDetailBase EditCountry()
        {
            return null;
            //if (!IsDetailExist(CurrentCountry.Id))
            //    ShowModuleDialog(new ContactDetail(this.FindForm(), this.CurrentCountry, MenuManager));
        }
        public override string EditObjectName { get { return ConstStrings.Get("Customer"); } }
        public override string EditObjectsName { get { return ConstStrings.Get("Customers"); } }
        protected override void RefreshGridDataSource()
        {
            this.grdCountries.DataSource = new CountryPageDataAccess(RepositoryContext.Instance).GetAll();
        }
        protected override void BeginRefreshData()
        {
            base.BeginRefreshData();
            RefreshGridDataSource();
        }
        protected override ColumnView MainView { get { return this.grdViewCountries as ColumnView; } }
        protected override ColumnView CurrentView { get { return this.grdCountries.MainView as ColumnView; } }
        //protected override ColumnView AlternateView { get { return grdControl; } }
        protected override object CurrentEditObject { get { return CurrentCountry; } }
        private UICountry CurrentCountry
        {
            get
            {
                if (CurrentView == null || CurrentView.FocusedRowHandle < 0) return null;
                var lRowItem = CurrentView.GetRow(CurrentView.FocusedRowHandle);
                return lRowItem as UICountry;
            }
        }

        private void grdCountries_DoubleClick(object sender, EventArgs e)
        {
            Logger.LogDebug("GrdCountries::Doubleclick - CurrentCountry = " + CurrentCountry);
            using ( var lForm = new Desktop.CountryPage(CurrentCountry.Id) )
            {
                if ( lForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    lForm.Save();
                }
            }
        }
    }
}
