
namespace Desktop
{
    partial class frmRankingPage
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
            this.SuspendLayout();
            // 
            // ucWebPage1
            // 
            this.ucWebPage1.Location = new System.Drawing.Point(2, 2);
            this.ucWebPage1.Name = "ucWebPage1";
            this.ucWebPage1.Size = new System.Drawing.Size(940, 817);
            this.ucWebPage1.TabIndex = 0;
            this.ucWebPage1.UIPage = null;
            this.ucWebPage1.SaveButtonClicked += new System.EventHandler(this.ucWebPage1_SaveButtonClicked);
            this.ucWebPage1.Load += new System.EventHandler(this.ucWebPage1_Load);
            // 
            // frmRankingPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 807);
            this.Controls.Add(this.ucWebPage1);
            this.Name = "frmRankingPage";
            this.Text = "Ranking Page";
            this.Load += new System.EventHandler(this.frmRankingPage_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.ucWebPage ucWebPage1;
    }
}