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
    public partial class ResortPages : ModuleBase
    {
        public static string ModName = "ResortPagesModule";
        public static string Caption = "Resorts";
        public ResortPages()
        {
            InitializeComponent();
        }

        protected override bool AllowEdit(GridHitInfo info)
        {
            return true;
        }

        public override ModDetailBase Edit()
        {
            Logger.LogDebug("Contact::Edit - Called");
            base.Edit();
            return EditResort();
        }
        protected override bool IsDetailExist(string id)
        {
            return false;
        }

        ModDetailBase EditResort()
        {
            return null;
           //     ShowModuleDialog(new ContactDetail(this.FindForm(), this.CurrentResort, MenuManager));
        }
        public override string EditObjectName { get { return ConstStrings.Get("Customer"); } }
        public override string EditObjectsName { get { return ConstStrings.Get("Customers"); } }
        protected override void RefreshGridDataSource()
        {
            this.grdResortPages.DataSource = new ResortPageBusiness(RepositoryContext.Instance).GetAll();
        }
        protected override void BeginRefreshData()
        {
            base.BeginRefreshData();
            RefreshGridDataSource();
        }
        protected override ColumnView MainView { get { return this.gridView1; } }
        protected override ColumnView CurrentView { get { return grdResortPages.MainView as ColumnView; } }
        //protected override ColumnView AlternateView { get { return grdControl; } }
        protected override Object CurrentEditObject { get { return CurrentResort; } }
        private UIResortPage CurrentResort
        {
            get
            {
                if (CurrentView == null || CurrentView.FocusedRowHandle < 0) return null;
                var lRowObj = CurrentView.GetRow(CurrentView.FocusedRowHandle) as UIResortPage;
                return new ResortPageBusiness(RepositoryContext.Instance).Get(lRowObj.Id) as UIResortPage;
            }
        }

        private void grdResortPages_DoubleClick(object sender, EventArgs e)
        {
            Logger.LogDebug("GrdResorts::Doubleclick - CurrentResort = " + CurrentResort);
            using (var lForm = new Desktop.frmResortPage (CurrentResort))
            {
                if (lForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //lForm.Save();
                }
            }

        }
    }
}
