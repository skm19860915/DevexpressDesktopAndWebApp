
namespace Desktop.Modules
{
    partial class Opportunities
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
            this.grdOpportunities = new DevExpress.XtraGrid.GridControl();
            this.grdVwOpportunities = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.uIOpportunityGridBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.colOpportunityID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOutBoundAirport = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInBoundAirPort = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOutBoundDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInBoundDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAgentID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNotes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAllInclusive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHotelOnly = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCruise = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPerPersonBudget = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTripBudget = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdOpportunities)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdVwOpportunities)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uIOpportunityGridBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // grdOpportunities
            // 
            this.grdOpportunities.DataSource = this.uIOpportunityGridBindingSource;
            this.grdOpportunities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdOpportunities.Location = new System.Drawing.Point(0, 0);
            this.grdOpportunities.MainView = this.grdVwOpportunities;
            this.grdOpportunities.Name = "grdOpportunities";
            this.grdOpportunities.Size = new System.Drawing.Size(692, 619);
            this.grdOpportunities.TabIndex = 0;
            this.grdOpportunities.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdVwOpportunities});
            // 
            // grdVwOpportunities
            // 
            this.grdVwOpportunities.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colOpportunityID,
            this.colOutBoundAirport,
            this.colInBoundAirPort,
            this.colOutBoundDate,
            this.colInBoundDate,
            this.colAgentID,
            this.colNotes,
            this.colAllInclusive,
            this.colHotelOnly,
            this.colCruise,
            this.colPerPersonBudget,
            this.colTripBudget,
            this.colName});
            this.grdVwOpportunities.GridControl = this.grdOpportunities;
            this.grdVwOpportunities.Name = "grdVwOpportunities";
            this.grdVwOpportunities.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colName, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // uIOpportunityGridBindingSource
            // 
            this.uIOpportunityGridBindingSource.DataSource = typeof(BlitzerCore.Models.UI.UIOpportunityGrid);
            this.uIOpportunityGridBindingSource.CurrentChanged += new System.EventHandler(this.uIOpportunityGridBindingSource_CurrentChanged);
            // 
            // colOpportunityID
            // 
            this.colOpportunityID.FieldName = "OpportunityID";
            this.colOpportunityID.Name = "colOpportunityID";
            // 
            // colOutBoundAirport
            // 
            this.colOutBoundAirport.FieldName = "OutBoundAirport";
            this.colOutBoundAirport.Name = "colOutBoundAirport";
            // 
            // colInBoundAirPort
            // 
            this.colInBoundAirPort.FieldName = "InBoundAirPort";
            this.colInBoundAirPort.Name = "colInBoundAirPort";
            // 
            // colOutBoundDate
            // 
            this.colOutBoundDate.FieldName = "OutBoundDate";
            this.colOutBoundDate.Name = "colOutBoundDate";
            this.colOutBoundDate.Visible = true;
            this.colOutBoundDate.VisibleIndex = 1;
            // 
            // colInBoundDate
            // 
            this.colInBoundDate.FieldName = "InBoundDate";
            this.colInBoundDate.Name = "colInBoundDate";
            this.colInBoundDate.Visible = true;
            this.colInBoundDate.VisibleIndex = 2;
            // 
            // colAgentID
            // 
            this.colAgentID.FieldName = "AgentID";
            this.colAgentID.Name = "colAgentID";
            // 
            // colNotes
            // 
            this.colNotes.FieldName = "Notes";
            this.colNotes.Name = "colNotes";
            // 
            // colAllInclusive
            // 
            this.colAllInclusive.FieldName = "AllInclusive";
            this.colAllInclusive.Name = "colAllInclusive";
            this.colAllInclusive.Visible = true;
            this.colAllInclusive.VisibleIndex = 3;
            // 
            // colHotelOnly
            // 
            this.colHotelOnly.FieldName = "HotelOnly";
            this.colHotelOnly.Name = "colHotelOnly";
            this.colHotelOnly.Visible = true;
            this.colHotelOnly.VisibleIndex = 4;
            // 
            // colCruise
            // 
            this.colCruise.FieldName = "Cruise";
            this.colCruise.Name = "colCruise";
            // 
            // colPerPersonBudget
            // 
            this.colPerPersonBudget.FieldName = "PerPersonBudget";
            this.colPerPersonBudget.Name = "colPerPersonBudget";
            // 
            // colTripBudget
            // 
            this.colTripBudget.FieldName = "TripBudget";
            this.colTripBudget.Name = "colTripBudget";
            // 
            // colName
            // 
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            // 
            // Opportunities
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdOpportunities);
            this.Name = "Opportunities";
            this.Size = new System.Drawing.Size(692, 619);
            ((System.ComponentModel.ISupportInitialize)(this.grdOpportunities)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdVwOpportunities)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uIOpportunityGridBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdOpportunities;
        private DevExpress.XtraGrid.Views.Grid.GridView grdVwOpportunities;
        private System.Windows.Forms.BindingSource uIOpportunityGridBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colOpportunityID;
        private DevExpress.XtraGrid.Columns.GridColumn colOutBoundAirport;
        private DevExpress.XtraGrid.Columns.GridColumn colInBoundAirPort;
        private DevExpress.XtraGrid.Columns.GridColumn colOutBoundDate;
        private DevExpress.XtraGrid.Columns.GridColumn colInBoundDate;
        private DevExpress.XtraGrid.Columns.GridColumn colAgentID;
        private DevExpress.XtraGrid.Columns.GridColumn colNotes;
        private DevExpress.XtraGrid.Columns.GridColumn colAllInclusive;
        private DevExpress.XtraGrid.Columns.GridColumn colHotelOnly;
        private DevExpress.XtraGrid.Columns.GridColumn colCruise;
        private DevExpress.XtraGrid.Columns.GridColumn colPerPersonBudget;
        private DevExpress.XtraGrid.Columns.GridColumn colTripBudget;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
    }
}
