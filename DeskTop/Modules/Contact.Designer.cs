
namespace Desktop.Modules
{
    partial class Contacts
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
            this.grdControl = new DevExpress.XtraGrid.GridControl();
            this.advBandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
            this.gridBand4 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.uIClientBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.colLeadID = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colNickName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colEmailConfirmed = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colRelationshipID = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colRelationship = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colFirst = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colMiddle = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colLast = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colSuffix = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colDOB = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colEmail = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colCell = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colAddress1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colAddress2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colCity = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colState = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colZipCode = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colAgentID = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colGE_OR_TSA = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.advBandedGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uIClientBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // grdControl
            // 
            this.grdControl.DataSource = this.uIClientBindingSource;
            this.grdControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdControl.Location = new System.Drawing.Point(0, 0);
            this.grdControl.MainView = this.advBandedGridView1;
            this.grdControl.Name = "grdControl";
            this.grdControl.ShowOnlyPredefinedDetails = true;
            this.grdControl.Size = new System.Drawing.Size(954, 681);
            this.grdControl.TabIndex = 0;
            this.grdControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.advBandedGridView1});
            // 
            // advBandedGridView1
            // 
            this.advBandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand4});
            this.advBandedGridView1.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colLeadID,
            this.colNickName,
            this.colEmailConfirmed,
            this.colRelationshipID,
            this.colRelationship,
            this.colFirst,
            this.colMiddle,
            this.colLast,
            this.colSuffix,
            this.colName,
            this.colDOB,
            this.colEmail,
            this.colCell,
            this.colAddress1,
            this.colAddress2,
            this.colCity,
            this.colState,
            this.colZipCode,
            this.colAgentID,
            this.colGE_OR_TSA});
            this.advBandedGridView1.GridControl = this.grdControl;
            this.advBandedGridView1.Name = "advBandedGridView1";
            this.advBandedGridView1.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            this.advBandedGridView1.OptionsBehavior.Editable = false;
            this.advBandedGridView1.OptionsDetail.EnableMasterViewMode = false;
            this.advBandedGridView1.OptionsFind.AlwaysVisible = true;
            this.advBandedGridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.advBandedGridView1.OptionsSelection.UseIndicatorForSelection = false;
            this.advBandedGridView1.OptionsView.ColumnAutoWidth = true;
            this.advBandedGridView1.OptionsView.ShowAutoFilterRow = true;
            this.advBandedGridView1.OptionsView.ShowBands = false;
            this.advBandedGridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridBand4
            // 
            this.gridBand4.Caption = "Name";
            this.gridBand4.Columns.Add(this.colLeadID);
            this.gridBand4.Columns.Add(this.colNickName);
            this.gridBand4.Columns.Add(this.colEmailConfirmed);
            this.gridBand4.Columns.Add(this.colRelationshipID);
            this.gridBand4.Columns.Add(this.colRelationship);
            this.gridBand4.Columns.Add(this.colFirst);
            this.gridBand4.Columns.Add(this.colMiddle);
            this.gridBand4.Columns.Add(this.colLast);
            this.gridBand4.Columns.Add(this.colSuffix);
            this.gridBand4.Columns.Add(this.colName);
            this.gridBand4.Columns.Add(this.colDOB);
            this.gridBand4.Columns.Add(this.colEmail);
            this.gridBand4.Columns.Add(this.colCell);
            this.gridBand4.Columns.Add(this.colAddress1);
            this.gridBand4.Columns.Add(this.colAddress2);
            this.gridBand4.Columns.Add(this.colCity);
            this.gridBand4.Columns.Add(this.colState);
            this.gridBand4.Columns.Add(this.colZipCode);
            this.gridBand4.Columns.Add(this.colAgentID);
            this.gridBand4.Columns.Add(this.colGE_OR_TSA);
            this.gridBand4.Name = "gridBand4";
            this.gridBand4.VisibleIndex = 0;
            this.gridBand4.Width = 300;
            // 
            // uIClientBindingSource
            // 
            this.uIClientBindingSource.DataSource = typeof(BlitzerCore.Models.UI.UIContact);
            // 
            // colLeadID
            // 
            this.colLeadID.FieldName = "LeadID";
            this.colLeadID.Name = "colLeadID";
            // 
            // colNickName
            // 
            this.colNickName.FieldName = "NickName";
            this.colNickName.Name = "colNickName";
            // 
            // colEmailConfirmed
            // 
            this.colEmailConfirmed.FieldName = "EmailConfirmed";
            this.colEmailConfirmed.Name = "colEmailConfirmed";
            // 
            // colRelationshipID
            // 
            this.colRelationshipID.FieldName = "RelationshipID";
            this.colRelationshipID.Name = "colRelationshipID";
            // 
            // colRelationship
            // 
            this.colRelationship.FieldName = "Relationship";
            this.colRelationship.Name = "colRelationship";
            // 
            // colFirst
            // 
            this.colFirst.FieldName = "First";
            this.colFirst.Name = "colFirst";
            // 
            // colMiddle
            // 
            this.colMiddle.FieldName = "Middle";
            this.colMiddle.Name = "colMiddle";
            // 
            // colLast
            // 
            this.colLast.FieldName = "Last";
            this.colLast.Name = "colLast";
            // 
            // colSuffix
            // 
            this.colSuffix.FieldName = "Suffix";
            this.colSuffix.Name = "colSuffix";
            // 
            // colName
            // 
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.OptionsColumn.ReadOnly = true;
            this.colName.Visible = true;
            // 
            // colDOB
            // 
            this.colDOB.FieldName = "DOB";
            this.colDOB.Name = "colDOB";
            this.colDOB.Visible = true;
            // 
            // colEmail
            // 
            this.colEmail.FieldName = "Email";
            this.colEmail.Name = "colEmail";
            this.colEmail.Visible = true;
            // 
            // colCell
            // 
            this.colCell.FieldName = "Cell";
            this.colCell.Name = "colCell";
            this.colCell.Visible = true;
            // 
            // colAddress1
            // 
            this.colAddress1.FieldName = "Address1";
            this.colAddress1.Name = "colAddress1";
            // 
            // colAddress2
            // 
            this.colAddress2.FieldName = "Address2";
            this.colAddress2.Name = "colAddress2";
            // 
            // colCity
            // 
            this.colCity.FieldName = "City";
            this.colCity.Name = "colCity";
            // 
            // colState
            // 
            this.colState.FieldName = "State";
            this.colState.Name = "colState";
            // 
            // colZipCode
            // 
            this.colZipCode.FieldName = "ZipCode";
            this.colZipCode.Name = "colZipCode";
            // 
            // colAgentID
            // 
            this.colAgentID.FieldName = "AgentID";
            this.colAgentID.Name = "colAgentID";
            // 
            // colGE_OR_TSA
            // 
            this.colGE_OR_TSA.FieldName = "GE_OR_TSA";
            this.colGE_OR_TSA.Name = "colGE_OR_TSA";
            // 
            // Contacts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdControl);
            this.Name = "Contacts";
            this.Size = new System.Drawing.Size(954, 681);
            this.Load += new System.EventHandler(this.Contacts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.advBandedGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uIClientBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdControl;
        private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand4;
        private System.Windows.Forms.BindingSource uIClientBindingSource;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colLeadID;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colNickName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colEmailConfirmed;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colRelationshipID;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colRelationship;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colFirst;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colMiddle;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colLast;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSuffix;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colDOB;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colEmail;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colCell;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colAddress1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colAddress2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colCity;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colState;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colZipCode;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colAgentID;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colGE_OR_TSA;
    }
}
