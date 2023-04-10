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
    public partial class Trips : ModuleBase
    {
        public static string ModName = "TripsModule";
        public static string Caption = "Trips";
        public Trips()
        {
            Logger.LogTracing($"{ModName}::ctor");
            InitializeComponent();
        }

        private void uITripGridBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
        public override ModDetailBase Add()
        {
            Logger.LogDebug($"{ModName}::Add - Called");
            base.Details();
            return AddTrip();
        }

        public override ModDetailBase Details()
        {
            Logger.LogDebug($"{ModName}::Details - Called");
            base.Details();
            return AddTrip();
        }

        public override ModDetailBase Edit()
        {
            Logger.LogDebug($"{ModName}::Edit - Called");
            base.Edit();
            return AddTrip();
        }
        public override ModDetailBase GridDoubleClick()
        {
            Logger.LogDebug($"{ModName}::GridDoubleClick - Called");
            base.Details();
            return ViewTrip();
        }
        protected override bool IsDetailExist(string id)
        {
            return false;
        }

        ModDetailBase AddTrip()
        {
            Logger.LogTracing($"{ModName}::AddTrip creating TripAddDetailModule");
            return new NewTrip(this.FindForm(), this.CurrentTrip);
        }

        ModDetailBase EditTrip()
        {
            Logger.LogTracing($"{ModName}::EditTrip creating TripDetailModule");
            return new NewTrip(this.FindForm(), this.CurrentTrip);
        }

        ModDetailBase ViewTrip()
        {
            Logger.LogTracing($"{ModName}::EditTrip creating TripDetailModule");
            return new NewTrip(this.FindForm(), this.CurrentTrip);
        }
        protected override bool AllowEdit(GridHitInfo info)
        {
            return true;
        }

        public override string EditObjectName { get { return ConstStrings.Get("Trip"); } }
        public override string EditObjectsName { get { return ConstStrings.Get("Trips"); } }
        protected override void RefreshGridDataSource()
        {
            var lTrips = new TripBusiness(RepositoryContext.Instance).GetAll(Blitzer.Instance.CurrentUser() as Agent);
            Logger.LogTracing($"{ModName}::RefreshGridDataSource There are {lTrips.Count} trips");

            List<UITrip> UITrips = new List<UITrip>();
            foreach (var lTrip in lTrips)
                UITrips.Add(TripUIHelper.Convert(lTrip));
            grdTrips.DataSource = UITrips;
        }
        protected override void BeginRefreshData()
        {
            base.BeginRefreshData();
            RefreshGridDataSource();
        }
        protected override ColumnView MainView { get { return grdVwTrips; } }
        protected override ColumnView CurrentView { get { return grdTrips.MainView as ColumnView; } }
        //protected override ColumnView AlternateView { get { return grdControl; } }
        protected override Object CurrentEditObject { get { return CurrentTrip; } }
        private UITrip CurrentTrip
        {
            get
            {
                if (CurrentView == null || CurrentView.FocusedRowHandle < 0) return null;
                var lOpp = CurrentView.GetRow(CurrentView.FocusedRowHandle);
                Logger.LogDebug("Trip::CurrentTrip = " + (lOpp as UITrip).Name);
                return lOpp as UITrip;
            }
        }
    }
}
