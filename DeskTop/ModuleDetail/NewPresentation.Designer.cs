
namespace Desktop.ModuleDetail
{
    partial class NewPresentation
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
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            this._selectedPageView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._selectedPageGrid = new DevExpress.XtraGrid.GridControl();
            this.dragDropEvents1 = new DevExpress.Utils.DragDrop.DragDropEvents(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.WebPagesGrid = new DevExpress.XtraGrid.GridControl();
            this.WebPageView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnMoveDown = new DevExpress.XtraEditors.SimpleButton();
            this.txtClientName = new DevExpress.XtraEditors.TextEdit();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCurrentPageLink = new DevExpress.XtraEditors.TextEdit();
            this.btnTakeUserForDemo = new DevExpress.XtraEditors.SimpleButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCreatePresentation = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._selectedPageView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._selectedPageGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WebPagesGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WebPageView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtClientName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrentPageLink.Properties)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _selectedPageView
            // 
            this.behaviorManager1.SetBehaviors(this._selectedPageView, new DevExpress.Utils.Behaviors.Behavior[] {
            ((DevExpress.Utils.Behaviors.Behavior)(DevExpress.Utils.DragDrop.DragDropBehavior.Create(typeof(DevExpress.XtraGrid.Extensions.ColumnViewDragDropSource), true, true, true, true, this.dragDropEvents1)))});
            this._selectedPageView.GridControl = this._selectedPageGrid;
            this._selectedPageView.Name = "_selectedPageView";
            this._selectedPageView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this._selectedPageView.OptionsBehavior.AllowSortAnimation = DevExpress.Utils.DefaultBoolean.False;
            this._selectedPageView.OptionsBehavior.Editable = false;
            this._selectedPageView.OptionsBehavior.ReadOnly = true;
            this._selectedPageView.OptionsCustomization.AllowSort = false;
            this._selectedPageView.OptionsFind.AllowFindPanel = false;
            this._selectedPageView.OptionsFind.AlwaysVisible = true;
            this._selectedPageView.OptionsFind.ClearFindOnClose = false;
            this._selectedPageView.OptionsView.ShowGroupPanel = false;
            this._selectedPageView.MouseUp += new System.Windows.Forms.MouseEventHandler(this._selectedPageView_MouseUp);
            // 
            // _selectedPageGrid
            // 
            this._selectedPageGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._selectedPageGrid.Location = new System.Drawing.Point(2, 2);
            this._selectedPageGrid.MainView = this._selectedPageView;
            this._selectedPageGrid.Name = "_selectedPageGrid";
            this._selectedPageGrid.Size = new System.Drawing.Size(575, 339);
            this._selectedPageGrid.TabIndex = 6;
            this._selectedPageGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this._selectedPageView});
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(661, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 25);
            this.label2.TabIndex = 19;
            this.label2.Text = "Client";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.WebPagesGrid);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(3, 3);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(578, 343);
            this.panelControl2.TabIndex = 7;
            // 
            // WebPagesGrid
            // 
            this.WebPagesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WebPagesGrid.Location = new System.Drawing.Point(2, 2);
            this.WebPagesGrid.MainView = this.WebPageView;
            this.WebPagesGrid.Name = "WebPagesGrid";
            this.WebPagesGrid.Size = new System.Drawing.Size(574, 339);
            this.WebPagesGrid.TabIndex = 0;
            this.WebPagesGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.WebPageView});
            // 
            // WebPageView
            // 
            this.WebPageView.GridControl = this.WebPagesGrid;
            this.WebPageView.Name = "WebPageView";
            this.WebPageView.OptionsBehavior.Editable = false;
            this.WebPageView.OptionsFilter.ShowInHeaderSearchTextMode = DevExpress.XtraGrid.Views.Grid.ShowInHeaderSearchTextMode.Tooltip;
            this.WebPageView.OptionsFind.AllowFindPanel = false;
            this.WebPageView.OptionsFind.AlwaysVisible = true;
            this.WebPageView.OptionsFind.ClearFindOnClose = false;
            this.WebPageView.OptionsView.ShowGroupPanel = false;
            this.WebPageView.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.WebPageGrid_RowClick);
            this.WebPageView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.WebPageView_MouseUp);
            this.WebPageView.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this._selectedPageGrid);
            this.panelControl1.Controls.Add(this.btnDelete);
            this.panelControl1.Controls.Add(this.btnMoveDown);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(587, 3);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(579, 343);
            this.panelControl1.TabIndex = 6;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(419, 5);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(79, 29);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.Visible = false;
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveDown.Location = new System.Drawing.Point(318, 5);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(87, 29);
            this.btnMoveDown.TabIndex = 8;
            this.btnMoveDown.Text = "&Down";
            this.btnMoveDown.Visible = false;
            // 
            // txtClientName
            // 
            this.txtClientName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClientName.Location = new System.Drawing.Point(759, 26);
            this.txtClientName.Name = "txtClientName";
            this.txtClientName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtClientName.Properties.Appearance.Options.UseFont = true;
            this.txtClientName.Size = new System.Drawing.Size(183, 30);
            this.txtClientName.TabIndex = 18;
            // 
            // webBrowser1
            // 
            this.webBrowser1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.webBrowser1.AllowNavigation = false;
            this.webBrowser1.AllowWebBrowserDrop = false;
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.webBrowser1, 2);
            this.webBrowser1.Location = new System.Drawing.Point(3, 352);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(18, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.tableLayoutPanel1.SetRowSpan(this.webBrowser1, 2);
            this.webBrowser1.Size = new System.Drawing.Size(1163, 364);
            this.webBrowser1.TabIndex = 5;
            this.webBrowser1.WebBrowserShortcutsEnabled = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(9, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 25);
            this.label1.TabIndex = 16;
            this.label1.Text = "Current Location";
            // 
            // txtCurrentPageLink
            // 
            this.txtCurrentPageLink.Enabled = false;
            this.txtCurrentPageLink.Location = new System.Drawing.Point(198, 27);
            this.txtCurrentPageLink.Name = "txtCurrentPageLink";
            this.txtCurrentPageLink.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtCurrentPageLink.Properties.Appearance.Options.UseFont = true;
            this.txtCurrentPageLink.Size = new System.Drawing.Size(294, 30);
            this.txtCurrentPageLink.TabIndex = 15;
            // 
            // btnTakeUserForDemo
            // 
            this.btnTakeUserForDemo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTakeUserForDemo.Appearance.BackColor = System.Drawing.Color.Lime;
            this.btnTakeUserForDemo.Appearance.BackColor2 = System.Drawing.Color.Lime;
            this.btnTakeUserForDemo.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnTakeUserForDemo.Appearance.Options.UseBackColor = true;
            this.btnTakeUserForDemo.Appearance.Options.UseFont = true;
            this.btnTakeUserForDemo.AppearancePressed.BackColor = System.Drawing.Color.White;
            this.btnTakeUserForDemo.AppearancePressed.BackColor2 = System.Drawing.Color.White;
            this.btnTakeUserForDemo.AppearancePressed.Options.UseBackColor = true;
            this.btnTakeUserForDemo.Location = new System.Drawing.Point(997, 24);
            this.btnTakeUserForDemo.Name = "btnTakeUserForDemo";
            this.btnTakeUserForDemo.Size = new System.Drawing.Size(148, 33);
            this.btnTakeUserForDemo.TabIndex = 14;
            this.btnTakeUserForDemo.Text = "Start tour";
            this.btnTakeUserForDemo.Click += new System.EventHandler(this.btnTakeUserForDemo_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panelControl1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelControl2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.webBrowser1, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1169, 719);
            this.tableLayoutPanel1.TabIndex = 17;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(592, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(186, 24);
            this.label3.TabIndex = 20;
            this.label3.Text = "Presentation Queue";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(9, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(196, 24);
            this.label4.TabIndex = 12;
            this.label4.Text = "Available Web Pages";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Location = new System.Drawing.Point(0, 122);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1175, 725);
            this.panel1.TabIndex = 0;
            // 
            // btnCreatePresentation
            // 
            this.btnCreatePresentation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreatePresentation.Appearance.BackColor = System.Drawing.Color.White;
            this.btnCreatePresentation.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnCreatePresentation.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCreatePresentation.Appearance.Options.UseBackColor = true;
            this.btnCreatePresentation.Appearance.Options.UseFont = true;
            this.btnCreatePresentation.AppearancePressed.BackColor = System.Drawing.Color.White;
            this.btnCreatePresentation.AppearancePressed.BackColor2 = System.Drawing.Color.White;
            this.btnCreatePresentation.AppearancePressed.Options.UseBackColor = true;
            this.btnCreatePresentation.Location = new System.Drawing.Point(948, 25);
            this.btnCreatePresentation.Name = "btnCreatePresentation";
            this.btnCreatePresentation.Size = new System.Drawing.Size(197, 33);
            this.btnCreatePresentation.TabIndex = 21;
            this.btnCreatePresentation.Text = "Create Presentation";
            this.btnCreatePresentation.Click += new System.EventHandler(this.btnCreatePresentation_Click);
            // 
            // NewPresentation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCreatePresentation);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtClientName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCurrentPageLink);
            this.Controls.Add(this.btnTakeUserForDemo);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "NewPresentation";
            this.Size = new System.Drawing.Size(1175, 850);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.Controls.SetChildIndex(this.btnTakeUserForDemo, 0);
            this.Controls.SetChildIndex(this.txtCurrentPageLink, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.txtClientName, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.btnCreatePresentation, 0);
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._selectedPageView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._selectedPageGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.WebPagesGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WebPageView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtClientName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrentPageLink.Properties)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl WebPagesGrid;
        private DevExpress.XtraGrid.Views.Grid.GridView WebPageView;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl _selectedPageGrid;
        private DevExpress.XtraGrid.Views.Grid.GridView _selectedPageView;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnMoveDown;
        private DevExpress.XtraEditors.TextEdit txtClientName;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtCurrentPageLink;
        private DevExpress.XtraEditors.SimpleButton btnTakeUserForDemo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.Utils.DragDrop.DragDropEvents dragDropEvents1;
        private DevExpress.XtraEditors.SimpleButton btnCreatePresentation;
    }
}
