
namespace Desktop.Modules
{
    partial class Trips
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.grdTrips = new DevExpress.XtraGrid.GridControl();
            this.uITripBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.grdVwTrips = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTripStage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDaysToStart = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBalance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFinalPayment = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTripStageStr = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTripStatusStr = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOutBoundDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInBoundDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdTrips)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uITripBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdVwTrips)).BeginInit();
            this.SuspendLayout();
            // 
            // grdTrips
            // 
            this.grdTrips.DataSource = this.uITripBindingSource;
            this.grdTrips.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTrips.Location = new System.Drawing.Point(0, 0);
            this.grdTrips.MainView = this.grdVwTrips;
            this.grdTrips.Name = "grdTrips";
            this.grdTrips.Size = new System.Drawing.Size(753, 613);
            this.grdTrips.TabIndex = 0;
            this.grdTrips.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdVwTrips});
            // 
            // uITripBindingSource
            // 
            this.uITripBindingSource.DataSource = typeof(BlitzerCore.Models.UI.UITrip);
            // 
            // grdVwTrips
            // 
            this.grdVwTrips.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTripStage,
            this.colDaysToStart,
            this.colBalance,
            this.colFinalPayment,
            this.colTripStageStr,
            this.colTripStatusStr,
            this.colOutBoundDate,
            this.colInBoundDate,
            this.colName});
            this.grdVwTrips.GridControl = this.grdTrips;
            this.grdVwTrips.Name = "grdVwTrips";
            // 
            // colTripStage
            // 
            this.colTripStage.FieldName = "TripStage";
            this.colTripStage.Name = "colTripStage";
            this.colTripStage.Visible = true;
            this.colTripStage.VisibleIndex = 1;
            // 
            // colDaysToStart
            // 
            this.colDaysToStart.FieldName = "DaysToStart";
            this.colDaysToStart.Name = "colDaysToStart";
            this.colDaysToStart.Visible = true;
            this.colDaysToStart.VisibleIndex = 2;
            // 
            // colBalance
            // 
            this.colBalance.FieldName = "Balance";
            this.colBalance.Name = "colBalance";
            this.colBalance.Visible = true;
            this.colBalance.VisibleIndex = 5;
            // 
            // colFinalPayment
            // 
            this.colFinalPayment.FieldName = "FinalPayment";
            this.colFinalPayment.Name = "colFinalPayment";
            this.colFinalPayment.Visible = true;
            this.colFinalPayment.VisibleIndex = 6;
            // 
            // colTripStageStr
            // 
            this.colTripStageStr.FieldName = "TripStageStr";
            this.colTripStageStr.Name = "colTripStageStr";
            this.colTripStageStr.Visible = true;
            this.colTripStageStr.VisibleIndex = 7;
            // 
            // colTripStatusStr
            // 
            this.colTripStatusStr.FieldName = "TripStatusStr";
            this.colTripStatusStr.Name = "colTripStatusStr";
            this.colTripStatusStr.Visible = true;
            this.colTripStatusStr.VisibleIndex = 8;
            // 
            // colOutBoundDate
            // 
            this.colOutBoundDate.FieldName = "OutBoundDate";
            this.colOutBoundDate.Name = "colOutBoundDate";
            this.colOutBoundDate.Visible = true;
            this.colOutBoundDate.VisibleIndex = 3;
            // 
            // colInBoundDate
            // 
            this.colInBoundDate.FieldName = "InBoundDate";
            this.colInBoundDate.Name = "colInBoundDate";
            this.colInBoundDate.Visible = true;
            this.colInBoundDate.VisibleIndex = 4;
            // 
            // colName
            // 
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            // 
            // Trips
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdTrips);
            this.Name = "Trips";
            ((System.ComponentModel.ISupportInitialize)(this.grdTrips)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uITripBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdVwTrips)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdTrips;
        private DevExpress.XtraGrid.Views.Grid.GridView grdVwTrips;
        private System.Windows.Forms.BindingSource uITripBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colTripStage;
        private DevExpress.XtraGrid.Columns.GridColumn colDaysToStart;
        private DevExpress.XtraGrid.Columns.GridColumn colBalance;
        private DevExpress.XtraGrid.Columns.GridColumn colFinalPayment;
        private DevExpress.XtraGrid.Columns.GridColumn colTripStageStr;
        private DevExpress.XtraGrid.Columns.GridColumn colTripStatusStr;
        private DevExpress.XtraGrid.Columns.GridColumn colOutBoundDate;
        private DevExpress.XtraGrid.Columns.GridColumn colInBoundDate;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
    }
}
