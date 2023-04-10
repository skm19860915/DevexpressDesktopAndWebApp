using BlitzerCore.Business;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using Desktop.BaseControls;
using Desktop.DataServices;
using Desktop.ModuleDetail;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop.Modules
{
    public partial class Presentations : Desktop.BaseControls.ModuleBase
    {
        public static string ModName = "PresentationsModule";
        public static string Caption = "Presentations";

        List<Presentation> IPresentations = new List<Presentation>();
        public Presentations()
        {
            Logger.LogTracing($"{ModName}::ctor");
            InitializeComponent();
        }

        private void gridPresenations_Load(object sender, EventArgs e)
        {
            RefreshGridDataSource();
        }

        protected override void RefreshGridDataSource()
        {
            IPresentations = new PresentationBusiness(RepositoryContext.Instance).Get();

            List<UIPresentationGrid> list = new List<UIPresentationGrid>();

            foreach (var item in IPresentations)
            {
                list.Add(new UIPresentationGrid
                {
                    Id = item.Id,
                    Guid = item.Guid,
                    ClientName = item.ClientName,
                    Created =  item.Created,
                    Status = item.Status/*, item.CurrentWebPage.Name */
                });
            }

            gridPresenations.DataSource = list;

            viewPresenations.Columns[0].Visible = false;
            viewPresenations.Columns[1].Visible = false;
        }
        protected override void BeginRefreshData()
        {
            base.BeginRefreshData();
            RefreshGridDataSource();
        }

        public override ModDetailBase Add()
        {
            Logger.LogDebug($"{ModName}::Add - Called");
            base.Details();
            return AddPresentation(true);
        }

        public override ModDetailBase Details()
        {
            Logger.LogDebug($"{ModName}::Details - Called");
            base.Details();
            return AddPresentation();
        }

        public override ModDetailBase Edit()
        {
            Logger.LogDebug($"{ModName}::Edit - Called");
            base.Edit();
            return AddPresentation();
        }

        public override ModDetailBase GridDoubleClick()
        {
            Logger.LogDebug($"{ModName}::GridDoubleClick - Called");
            base.Details();
            return AddPresentation();
        }
        ModDetailBase AddPresentation(bool isNews = false)
        {
            Logger.LogTracing($"{ModName}::NewPresentation creating PresentationAddDetailModule");
            return new NewPresentation(this.FindForm(), isNews ? null : this.CurrentPresentation);
        }
        protected override bool AllowEdit(GridHitInfo info)
        {
            return true;
        }
        protected override ColumnView MainView { get { return viewPresenations; } }
        protected override ColumnView CurrentView { get { return gridPresenations.MainView as ColumnView; } }
        //protected override ColumnView AlternateView { get { return grdControl; } }
        protected override Object CurrentEditObject { get { return CurrentPresentation; } }
        private UIPresentationGrid CurrentPresentation
        {
            get
            {
                if (CurrentView == null || CurrentView.FocusedRowHandle < 0) return null;
                var lOpp = CurrentView.GetRow(CurrentView.FocusedRowHandle);
                //Logger.LogDebug("Opportunity::CurrentOpportunity = " + (lOpp as Presentation).ClientName);
                return lOpp as UIPresentationGrid;
            }
        }
    }
}
