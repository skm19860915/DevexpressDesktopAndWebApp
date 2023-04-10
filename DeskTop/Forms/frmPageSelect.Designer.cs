
namespace Desktop
{
    partial class frmPageSelect
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
            this.grdCntrl = new DevExpress.XtraGrid.GridControl();
            this.pageBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.grdView_Pages = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTitle = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPageTitle = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPageCaption = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHeaderImageID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHeaderImage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContentID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCenterContent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMainImageID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMainImage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPublished = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPublishedOn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAuthorID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnNewPage = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdCntrl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pageBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdView_Pages)).BeginInit();
            this.SuspendLayout();
            // 
            // grdCntrl
            // 
            this.grdCntrl.DataSource = this.pageBindingSource;
            this.grdCntrl.Location = new System.Drawing.Point(0, 0);
            this.grdCntrl.MainView = this.grdView_Pages;
            this.grdCntrl.Name = "grdCntrl";
            this.grdCntrl.Size = new System.Drawing.Size(861, 458);
            this.grdCntrl.TabIndex = 0;
            this.grdCntrl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdView_Pages});
            this.grdCntrl.Click += new System.EventHandler(this.grdCntrl_Click);
            this.grdCntrl.DoubleClick += new System.EventHandler(this.grdCntrl_DoubleClick);
            // 
            // pageBindingSource
            // 
            this.pageBindingSource.DataSource = typeof(BlitzerCore.Models.UI.Page);
            this.pageBindingSource.CurrentChanged += new System.EventHandler(this.pageBindingSource_CurrentChanged);
            // 
            // grdView_Pages
            // 
            this.grdView_Pages.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colTitle,
            this.colPageTitle,
            this.colPageCaption,
            this.colHeaderImageID,
            this.colHeaderImage,
            this.colContentID,
            this.colCenterContent,
            this.colMainImageID,
            this.colMainImage,
            this.colPublished,
            this.colPublishedOn,
            this.colAuthorID});
            this.grdView_Pages.GridControl = this.grdCntrl;
            this.grdView_Pages.Name = "grdView_Pages";
            this.grdView_Pages.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.grdView_Pages.OptionsBehavior.Editable = false;
            this.grdView_Pages.OptionsBehavior.ReadOnly = true;
            this.grdView_Pages.OptionsFind.AlwaysVisible = true;
            this.grdView_Pages.OptionsView.ShowGroupPanel = false;
            // 
            // colId
            // 
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            // 
            // colTitle
            // 
            this.colTitle.FieldName = "Title";
            this.colTitle.Name = "colTitle";
            this.colTitle.Visible = true;
            this.colTitle.VisibleIndex = 0;
            // 
            // colPageTitle
            // 
            this.colPageTitle.FieldName = "PageTitle";
            this.colPageTitle.Name = "colPageTitle";
            this.colPageTitle.Visible = true;
            this.colPageTitle.VisibleIndex = 1;
            // 
            // colPageCaption
            // 
            this.colPageCaption.FieldName = "PageCaption";
            this.colPageCaption.Name = "colPageCaption";
            // 
            // colHeaderImageID
            // 
            this.colHeaderImageID.FieldName = "HeaderImageID";
            this.colHeaderImageID.Name = "colHeaderImageID";
            // 
            // colHeaderImage
            // 
            this.colHeaderImage.FieldName = "HeaderImage";
            this.colHeaderImage.Name = "colHeaderImage";
            this.colHeaderImage.Visible = true;
            this.colHeaderImage.VisibleIndex = 2;
            // 
            // colContentID
            // 
            this.colContentID.FieldName = "ContentID";
            this.colContentID.Name = "colContentID";
            // 
            // colCenterContent
            // 
            this.colCenterContent.FieldName = "CenterContent";
            this.colCenterContent.Name = "colCenterContent";
            // 
            // colMainImageID
            // 
            this.colMainImageID.FieldName = "MainImageID";
            this.colMainImageID.Name = "colMainImageID";
            // 
            // colMainImage
            // 
            this.colMainImage.FieldName = "MainImage";
            this.colMainImage.Name = "colMainImage";
            // 
            // colPublished
            // 
            this.colPublished.FieldName = "Published";
            this.colPublished.Name = "colPublished";
            this.colPublished.Visible = true;
            this.colPublished.VisibleIndex = 3;
            // 
            // colPublishedOn
            // 
            this.colPublishedOn.FieldName = "PublishedOn";
            this.colPublishedOn.Name = "colPublishedOn";
            this.colPublishedOn.Visible = true;
            this.colPublishedOn.VisibleIndex = 4;
            // 
            // colAuthorID
            // 
            this.colAuthorID.FieldName = "AuthorID";
            this.colAuthorID.Name = "colAuthorID";
            this.colAuthorID.Visible = true;
            this.colAuthorID.VisibleIndex = 5;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(338, 482);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Select";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(474, 482);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnNewPage
            // 
            this.btnNewPage.Location = new System.Drawing.Point(23, 482);
            this.btnNewPage.Name = "btnNewPage";
            this.btnNewPage.Size = new System.Drawing.Size(75, 23);
            this.btnNewPage.TabIndex = 3;
            this.btnNewPage.Text = "New";
            this.btnNewPage.UseVisualStyleBackColor = true;
            this.btnNewPage.Click += new System.EventHandler(this.btnNewPage_Click);
            // 
            // frmPageSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 526);
            this.Controls.Add(this.btnNewPage);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.grdCntrl);
            this.Name = "frmPageSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select a Page";
            ((System.ComponentModel.ISupportInitialize)(this.grdCntrl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pageBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdView_Pages)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdCntrl;
        private DevExpress.XtraGrid.Views.Grid.GridView grdView_Pages;
        private System.Windows.Forms.BindingSource pageBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colTitle;
        private DevExpress.XtraGrid.Columns.GridColumn colPageTitle;
        private DevExpress.XtraGrid.Columns.GridColumn colPageCaption;
        private DevExpress.XtraGrid.Columns.GridColumn colHeaderImageID;
        private DevExpress.XtraGrid.Columns.GridColumn colHeaderImage;
        private DevExpress.XtraGrid.Columns.GridColumn colContentID;
        private DevExpress.XtraGrid.Columns.GridColumn colCenterContent;
        private DevExpress.XtraGrid.Columns.GridColumn colMainImageID;
        private DevExpress.XtraGrid.Columns.GridColumn colMainImage;
        private DevExpress.XtraGrid.Columns.GridColumn colPublished;
        private DevExpress.XtraGrid.Columns.GridColumn colPublishedOn;
        private DevExpress.XtraGrid.Columns.GridColumn colAuthorID;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnNewPage;
    }
}