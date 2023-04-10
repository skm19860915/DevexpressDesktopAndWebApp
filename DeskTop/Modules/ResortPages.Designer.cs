
namespace Desktop.Modules
{
    partial class ResortPages
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
            this.grdResortPages = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.grdResortPages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // grdResortPages
            // 
            this.grdResortPages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdResortPages.Location = new System.Drawing.Point(0, 0);
            this.grdResortPages.MainView = this.gridView1;
            this.grdResortPages.Name = "grdResortPages";
            this.grdResortPages.Size = new System.Drawing.Size(731, 566);
            this.grdResortPages.TabIndex = 0;
            this.grdResortPages.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.grdResortPages.DoubleClick += new System.EventHandler(this.grdResortPages_DoubleClick);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.grdResortPages;
            this.gridView1.Name = "gridView1";
            // 
            // ResortPages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdResortPages);
            this.Name = "ResortPages";
            this.Size = new System.Drawing.Size(731, 566);
            ((System.ComponentModel.ISupportInitialize)(this.grdResortPages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdResortPages;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}
