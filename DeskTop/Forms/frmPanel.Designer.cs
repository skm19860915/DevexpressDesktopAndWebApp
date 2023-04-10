
namespace Desktop
{
    partial class frmPanel
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
            this.btnOk = new System.Windows.Forms.Button();
            this.ucTilePic4 = new Desktop.UserControls.ucTilePic();
            this.ucTilePic3 = new Desktop.UserControls.ucTilePic();
            this.ucTilePic2 = new Desktop.UserControls.ucTilePic();
            this.ucTilePic1 = new Desktop.UserControls.ucTilePic();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(85, 738);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.button1_Click);
            // 
            // ucTilePic4
            // 
            this.ucTilePic4.Location = new System.Drawing.Point(56, 555);
            this.ucTilePic4.Name = "ucTilePic4";
            this.ucTilePic4.Size = new System.Drawing.Size(150, 150);
            this.ucTilePic4.TabIndex = 8;
            this.ucTilePic4.Tile = null;
            // 
            // ucTilePic3
            // 
            this.ucTilePic3.Location = new System.Drawing.Point(56, 384);
            this.ucTilePic3.Name = "ucTilePic3";
            this.ucTilePic3.Size = new System.Drawing.Size(150, 150);
            this.ucTilePic3.TabIndex = 7;
            this.ucTilePic3.Tile = null;
            // 
            // ucTilePic2
            // 
            this.ucTilePic2.Location = new System.Drawing.Point(56, 211);
            this.ucTilePic2.Name = "ucTilePic2";
            this.ucTilePic2.Size = new System.Drawing.Size(150, 150);
            this.ucTilePic2.TabIndex = 6;
            this.ucTilePic2.Tile = null;
            // 
            // ucTilePic1
            // 
            this.ucTilePic1.Location = new System.Drawing.Point(56, 38);
            this.ucTilePic1.Name = "ucTilePic1";
            this.ucTilePic1.Size = new System.Drawing.Size(150, 150);
            this.ucTilePic1.TabIndex = 5;
            this.ucTilePic1.Tile = null;
            this.ucTilePic1.Load += new System.EventHandler(this.ucTilePic1_Load);
            // 
            // frmPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 775);
            this.Controls.Add(this.ucTilePic4);
            this.Controls.Add(this.ucTilePic3);
            this.Controls.Add(this.ucTilePic2);
            this.Controls.Add(this.ucTilePic1);
            this.Controls.Add(this.btnOk);
            this.Name = "frmPanel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Tiles";
            this.Load += new System.EventHandler(this.frmTiles_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnOk;
        private UserControls.ucTilePic ucTilePic1;
        private UserControls.ucTilePic ucTilePic2;
        private UserControls.ucTilePic ucTilePic3;
        private UserControls.ucTilePic ucTilePic4;
    }
}