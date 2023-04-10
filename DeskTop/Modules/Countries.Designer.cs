
namespace Desktop.Modules
{
    partial class Countries
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
            this.grdCountries = new DevExpress.XtraGrid.GridControl();
            this.grdViewCountries = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.grdCountries)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdViewCountries)).BeginInit();
            this.SuspendLayout();
            // 
            // grdCountries
            // 
            this.grdCountries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCountries.Location = new System.Drawing.Point(0, 0);
            this.grdCountries.MainView = this.grdViewCountries;
            this.grdCountries.Name = "grdCountries";
            this.grdCountries.Size = new System.Drawing.Size(859, 532);
            this.grdCountries.TabIndex = 0;
            this.grdCountries.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdViewCountries});
            this.grdCountries.DoubleClick += new System.EventHandler(this.grdCountries_DoubleClick);
            // 
            // grdViewCountries
            // 
            this.grdViewCountries.GridControl = this.grdCountries;
            this.grdViewCountries.Name = "grdViewCountries";
            this.grdViewCountries.OptionsBehavior.Editable = false;
            this.grdViewCountries.OptionsSelection.EnableAppearanceFocusedCell = false;
            // 
            // Countries
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdCountries);
            this.Name = "Countries";
            this.Size = new System.Drawing.Size(859, 532);
            ((System.ComponentModel.ISupportInitialize)(this.grdCountries)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdViewCountries)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdCountries;
        private DevExpress.XtraGrid.Views.Grid.GridView grdViewCountries;
    }
}
