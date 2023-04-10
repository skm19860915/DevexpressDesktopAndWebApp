
namespace Desktop
{
    partial class frmMediaSelect
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.grdMedias = new DevExpress.XtraGrid.GridControl();
            this.selectMediaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridVw_SelectMedia = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMediaLocation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.colCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCountry = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTitle = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colForGallery = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSize560x460 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSize1600x1200 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVideo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnAddMedia = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdMedias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectMediaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVw_SelectMedia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // grdMedias
            // 
            this.grdMedias.DataSource = this.selectMediaBindingSource;
            this.grdMedias.Dock = System.Windows.Forms.DockStyle.Top;
            this.grdMedias.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdMedias.Location = new System.Drawing.Point(0, 0);
            this.grdMedias.MainView = this.gridVw_SelectMedia;
            this.grdMedias.Name = "grdMedias";
            this.grdMedias.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPictureEdit1});
            this.grdMedias.Size = new System.Drawing.Size(1030, 508);
            this.grdMedias.TabIndex = 0;
            this.grdMedias.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridVw_SelectMedia});
            this.grdMedias.Click += new System.EventHandler(this.grdMedias_Click);
            this.grdMedias.DoubleClick += new System.EventHandler(this.grdMedias_DoubleClick);
            // 
            // selectMediaBindingSource
            // 
            this.selectMediaBindingSource.DataSource = typeof(BlitzerCore.Models.UI.SelectMedia);
            // 
            // gridVw_SelectMedia
            // 
            this.gridVw_SelectMedia.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colMediaLocation,
            this.colCategory,
            this.colCountry,
            this.colTitle,
            this.colForGallery,
            this.colSize560x460,
            this.colSize1600x1200,
            this.colVideo});
            this.gridVw_SelectMedia.GridControl = this.grdMedias;
            this.gridVw_SelectMedia.Name = "gridVw_SelectMedia";
            this.gridVw_SelectMedia.OptionsBehavior.AutoSelectAllInEditor = false;
            this.gridVw_SelectMedia.OptionsBehavior.Editable = false;
            this.gridVw_SelectMedia.OptionsFilter.ShowAllTableValuesInFilterPopup = true;
            this.gridVw_SelectMedia.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridVw_SelectMedia.OptionsView.HeaderFilterButtonShowMode = DevExpress.XtraEditors.Controls.FilterButtonShowMode.Button;
            this.gridVw_SelectMedia.OptionsView.ShowAutoFilterRow = true;
            this.gridVw_SelectMedia.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways;
            this.gridVw_SelectMedia.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridVw_SelectMedia_FocusedRowChanged);
            this.gridVw_SelectMedia.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gridVw_SelectMedia_KeyUp);
            // 
            // colId
            // 
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            // 
            // colMediaLocation
            // 
            this.colMediaLocation.ColumnEdit = this.repositoryItemPictureEdit1;
            this.colMediaLocation.FieldName = "MediaLocation";
            this.colMediaLocation.ImageOptions.ImageUri.ResourceType = typeof(BlitzerCore.Models.UI.SelectMedia);
            this.colMediaLocation.ImageOptions.ImageUri.Uri = "https://blitzerblobs.blob.core.windows.net/images/560x460/104.jpg";
            this.colMediaLocation.Name = "colMediaLocation";
            // 
            // repositoryItemPictureEdit1
            // 
            this.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
            this.repositoryItemPictureEdit1.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            // 
            // colCategory
            // 
            this.colCategory.FieldName = "Category";
            this.colCategory.Name = "colCategory";
            this.colCategory.Visible = true;
            this.colCategory.VisibleIndex = 1;
            this.colCategory.Width = 202;
            // 
            // colCountry
            // 
            this.colCountry.FieldName = "Country";
            this.colCountry.Name = "colCountry";
            this.colCountry.Visible = true;
            this.colCountry.VisibleIndex = 2;
            this.colCountry.Width = 202;
            // 
            // colTitle
            // 
            this.colTitle.FieldName = "Title";
            this.colTitle.Name = "colTitle";
            this.colTitle.Visible = true;
            this.colTitle.VisibleIndex = 0;
            this.colTitle.Width = 202;
            // 
            // colForGallery
            // 
            this.colForGallery.FieldName = "ForGallery";
            this.colForGallery.Name = "colForGallery";
            this.colForGallery.Visible = true;
            this.colForGallery.VisibleIndex = 3;
            this.colForGallery.Width = 100;
            // 
            // colSize560x460
            // 
            this.colSize560x460.FieldName = "Size560x460";
            this.colSize560x460.Name = "colSize560x460";
            this.colSize560x460.Visible = true;
            this.colSize560x460.VisibleIndex = 4;
            this.colSize560x460.Width = 100;
            // 
            // colSize1600x1200
            // 
            this.colSize1600x1200.FieldName = "Size1600x1200";
            this.colSize1600x1200.Name = "colSize1600x1200";
            this.colSize1600x1200.Visible = true;
            this.colSize1600x1200.VisibleIndex = 5;
            this.colSize1600x1200.Width = 100;
            // 
            // colVideo
            // 
            this.colVideo.FieldName = "Video";
            this.colVideo.Name = "colVideo";
            this.colVideo.Visible = true;
            this.colVideo.VisibleIndex = 6;
            this.colVideo.Width = 100;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(607, 563);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Select";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(765, 563);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnAddMedia
            // 
            this.btnAddMedia.BackColor = System.Drawing.Color.Crimson;
            this.btnAddMedia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddMedia.ForeColor = System.Drawing.Color.White;
            this.btnAddMedia.Location = new System.Drawing.Point(133, 550);
            this.btnAddMedia.Name = "btnAddMedia";
            this.btnAddMedia.Size = new System.Drawing.Size(87, 56);
            this.btnAddMedia.TabIndex = 3;
            this.btnAddMedia.Text = "Add Media";
            this.btnAddMedia.UseVisualStyleBackColor = false;
            this.btnAddMedia.Click += new System.EventHandler(this.btnAddMedia_Clicked);
            // 
            // frmMediaSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1030, 627);
            this.Controls.Add(this.btnAddMedia);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.grdMedias);
            this.Name = "frmMediaSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form Selector";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMediaSelect_FormClosing);
            this.Load += new System.EventHandler(this.frmMediaSelect_Load);
            this.Move += new System.EventHandler(this.frmMediaSelect_Move);
            ((System.ComponentModel.ISupportInitialize)(this.grdMedias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectMediaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVw_SelectMedia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdMedias;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.BindingSource selectMediaBindingSource;
        private DevExpress.XtraGrid.Views.Grid.GridView gridVw_SelectMedia;
        private System.Windows.Forms.Button btnAddMedia;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colMediaLocation;
        private DevExpress.XtraGrid.Columns.GridColumn colCategory;
        private DevExpress.XtraGrid.Columns.GridColumn colCountry;
        private DevExpress.XtraGrid.Columns.GridColumn colTitle;
        private DevExpress.XtraGrid.Columns.GridColumn colForGallery;
        private DevExpress.XtraGrid.Columns.GridColumn colSize560x460;
        private DevExpress.XtraGrid.Columns.GridColumn colSize1600x1200;
        private DevExpress.XtraGrid.Columns.GridColumn colVideo;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit1;
    }
}