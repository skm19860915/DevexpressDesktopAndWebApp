
namespace Desktop.Modules
{
    partial class Presentations
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
            this.gridPresenations = new DevExpress.XtraGrid.GridControl();
            this.viewPresenations = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridPresenations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewPresenations)).BeginInit();
            this.SuspendLayout();
            // 
            // gridPresenations
            // 
            this.gridPresenations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridPresenations.Location = new System.Drawing.Point(0, 0);
            this.gridPresenations.MainView = this.viewPresenations;
            this.gridPresenations.Name = "gridPresenations";
            this.gridPresenations.Size = new System.Drawing.Size(1406, 687);
            this.gridPresenations.TabIndex = 0;
            this.gridPresenations.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewPresenations});
            this.gridPresenations.Load += new System.EventHandler(this.gridPresenations_Load);
            // 
            // viewPresenations
            // 
            this.viewPresenations.GridControl = this.gridPresenations;
            this.viewPresenations.Name = "viewPresenations";
            // 
            // Presentations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridPresenations);
            this.Name = "Presentations";
            this.Size = new System.Drawing.Size(1406, 687);
            ((System.ComponentModel.ISupportInitialize)(this.gridPresenations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewPresenations)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridPresenations;
        private DevExpress.XtraGrid.Views.Grid.GridView viewPresenations;
    }
}
