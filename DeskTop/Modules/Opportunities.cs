using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Desktop.BaseControls;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Business;
using BlitzerCore.Utilities;
using BlitzerCore.DataAccess;
using BlitzerCore.UIHelpers;
using Desktop.DataServices;
using Desktop.AppLogic;
using Desktop.ModuleDetail;

namespace Desktop.Modules
{
    public partial class Opportunities : Desktop.BaseControls.ModuleBase
    {
        public static string ModName = "OpportunitiesModule";
        public static string Caption = "Opportunities";
        public Opportunities()
        {
            Logger.LogTracing($"{ModName}::ctor");
            InitializeComponent();
        }

        private void uIOpportunityGridBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
        public override ModDetailBase Add()
        {
            Logger.LogDebug($"{ModName}::Add - Called");
            base.Details();
            return AddOpportunity();
        }

        public override ModDetailBase Details()
        {
            Logger.LogDebug($"{ModName}::Details - Called");
            base.Details();
            return AddOpportunity();
        }

        public override ModDetailBase Edit()
        {
            Logger.LogDebug($"{ModName}::Edit - Called");
            base.Edit();
            return AddOpportunity();
        }
        public override ModDetailBase GridDoubleClick()
        {
            Logger.LogDebug($"{ModName}::GridDoubleClick - Called");
            base.Details();
            return ViewOpportunity();
        }
        protected override bool IsDetailExist(string id)
        {
            return false;
        }

        ModDetailBase AddOpportunity()
        {
            Logger.LogTracing($"{ModName}::AddOpportunity creating OpportunityAddDetailModule");
            return new EditOpportunity(this.FindForm(), this.CurrentOpportunity);
        }

        ModDetailBase EditOpportunity()
        {
            Logger.LogTracing($"{ModName}::EditOpportunity creating OpportunityDetailModule");
            return new EditOpportunity(this.FindForm(), this.CurrentOpportunity);
        }

        ModDetailBase ViewOpportunity()
        {
            Logger.LogTracing($"{ModName}::EditOpportunity creating OpportunityDetailModule");
            return new EditOpportunity(this.FindForm(), this.CurrentOpportunity);
        }
        protected override bool AllowEdit(GridHitInfo info)
        {
            return true;
        }

        public override string EditObjectName { get { return ConstStrings.Get("Opportunity"); } }
        public override string EditObjectsName { get { return ConstStrings.Get("Opportunities"); } }
        protected override void RefreshGridDataSource()
        {
            var lOpps = new OpportunityBusiness(RepositoryContext.Instance).GetAll();

            List<UIOpportunity> UIOpps = new List<UIOpportunity>();
            foreach (var lOpp in lOpps)
                UIOpps.Add(OpportunityUIHelper.Convert(RepositoryContext.Instance,lOpp));
            grdOpportunities.DataSource = UIOpps;
        }
        protected override void BeginRefreshData()
        {
            base.BeginRefreshData();
            RefreshGridDataSource();
        }
        protected override ColumnView MainView { get { return grdVwOpportunities; } }
        protected override ColumnView CurrentView { get { return grdOpportunities.MainView as ColumnView; } }
        //protected override ColumnView AlternateView { get { return grdControl; } }
        protected override Object CurrentEditObject { get { return CurrentOpportunity; } }
        private UIOpportunity CurrentOpportunity
        {
            get
            {
                if (CurrentView == null || CurrentView.FocusedRowHandle < 0) return null;
                var lOpp = CurrentView.GetRow(CurrentView.FocusedRowHandle);
                Logger.LogDebug("Opportunity::CurrentOpportunity = " + (lOpp as UIOpportunity).Name);
                return lOpp as UIOpportunity;
            }
        }

    }
}
