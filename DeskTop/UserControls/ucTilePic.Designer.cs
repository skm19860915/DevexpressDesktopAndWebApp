
namespace Desktop.UserControls
{
    partial class ucTilePic
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
            this.picTile = new System.Windows.Forms.PictureBox();
            this.lblCategory = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picTile)).BeginInit();
            this.SuspendLayout();
            // 
            // picTile
            // 
            this.picTile.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.picTile.Location = new System.Drawing.Point(0, 0);
            this.picTile.Name = "picTile";
            this.picTile.Size = new System.Drawing.Size(150, 150);
            this.picTile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picTile.TabIndex = 0;
            this.picTile.TabStop = false;
            this.picTile.Click += new System.EventHandler(this.picTile_Click);
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.BackColor = System.Drawing.Color.Transparent;
            this.lblCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCategory.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.lblCategory.Location = new System.Drawing.Point(34, 122);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(82, 15);
            this.lblCategory.TabIndex = 1;
            this.lblCategory.Text = "Category Name";
            // 
            // ucTilePic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.picTile);
            this.Name = "ucTilePic";
            this.Load += new System.EventHandler(this.ucTilePic_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picTile)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picTile;
        private System.Windows.Forms.Label lblCategory;
    }
}
