
namespace Desktop
{
    partial class frmResortPage
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
            this.ucWebPage1 = new Desktop.UserControls.ucWebPage();
            this.btnTiles = new System.Windows.Forms.Button();
            this.btnComp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ucWebPage1
            // 
            this.ucWebPage1.Location = new System.Drawing.Point(3, 5);
            this.ucWebPage1.Name = "ucWebPage1";
            this.ucWebPage1.Size = new System.Drawing.Size(940, 817);
            this.ucWebPage1.TabIndex = 0;
            this.ucWebPage1.UIPage = null;
            this.ucWebPage1.SaveButtonClicked += new System.EventHandler(this.ucWebPage1_SaveButtonClicked);
            this.ucWebPage1.Load += new System.EventHandler(this.ucWebPage1_Load);
            // 
            // btnTiles
            // 
            this.btnTiles.Location = new System.Drawing.Point(777, 637);
            this.btnTiles.Name = "btnTiles";
            this.btnTiles.Size = new System.Drawing.Size(108, 23);
            this.btnTiles.TabIndex = 1;
            this.btnTiles.Text = "Tiles";
            this.btnTiles.UseVisualStyleBackColor = false;
            this.btnTiles.Click += new System.EventHandler(this.btnTiles_Click);
            // 
            // btnComp
            // 
            this.btnComp.Location = new System.Drawing.Point(777, 559);
            this.btnComp.Name = "btnComp";
            this.btnComp.Size = new System.Drawing.Size(108, 23);
            this.btnComp.TabIndex = 3;
            this.btnComp.Text = "Compariable";
            this.btnComp.UseVisualStyleBackColor = false;
            // 
            // frmResortPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 834);
            this.Controls.Add(this.btnComp);
            this.Controls.Add(this.btnTiles);
            this.Controls.Add(this.ucWebPage1);
            this.Name = "frmResortPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Resort Page";
            this.Load += new System.EventHandler(this.frmResortPage_Load);
            this.ResizeEnd += new System.EventHandler(this.frmResortPage_ResizeEnd);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.ucWebPage ucWebPage1;
        private System.Windows.Forms.Button btnTiles;
        private System.Windows.Forms.Button btnComp;
    }
}