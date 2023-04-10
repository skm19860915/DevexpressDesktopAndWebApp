
namespace Desktop
{
    partial class frmTile
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lnkSelectPics = new System.Windows.Forms.LinkLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.ucGalleryPic2 = new Desktop.UserControls.ucGalleryPic();
            this.ucGalleryPic1 = new Desktop.UserControls.ucGalleryPic();
            this.ucGalleryPic3 = new Desktop.UserControls.ucGalleryPic();
            this.ucGalleryPic4 = new Desktop.UserControls.ucGalleryPic();
            this.ucGalleryPic5 = new Desktop.UserControls.ucGalleryPic();
            this.ucGalleryPic6 = new Desktop.UserControls.ucGalleryPic();
            this.ucGalleryPic7 = new Desktop.UserControls.ucGalleryPic();
            this.ucGalleryPic8 = new Desktop.UserControls.ucGalleryPic();
            this.categoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.categoryBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.DataSource = this.categoryBindingSource;
            this.comboBox1.DisplayMember = "Name";
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(611, 55);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(214, 28);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.ValueMember = "Id";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(514, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Category";
            // 
            // lnkSelectPics
            // 
            this.lnkSelectPics.AutoSize = true;
            this.lnkSelectPics.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkSelectPics.Location = new System.Drawing.Point(1089, 62);
            this.lnkSelectPics.Name = "lnkSelectPics";
            this.lnkSelectPics.Size = new System.Drawing.Size(115, 20);
            this.lnkSelectPics.TabIndex = 10;
            this.lnkSelectPics.TabStop = true;
            this.lnkSelectPics.Text = "Select Pictures";
            this.lnkSelectPics.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSelectPics_LinkClicked);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(574, 606);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // ucGalleryPic2
            // 
            this.ucGalleryPic2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ucGalleryPic2.Location = new System.Drawing.Point(309, 156);
            this.ucGalleryPic2.Media = null;
            this.ucGalleryPic2.Name = "ucGalleryPic2";
            this.ucGalleryPic2.Size = new System.Drawing.Size(300, 200);
            this.ucGalleryPic2.TabIndex = 21;
            // 
            // ucGalleryPic1
            // 
            this.ucGalleryPic1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ucGalleryPic1.Location = new System.Drawing.Point(3, 156);
            this.ucGalleryPic1.Media = null;
            this.ucGalleryPic1.Name = "ucGalleryPic1";
            this.ucGalleryPic1.Size = new System.Drawing.Size(300, 200);
            this.ucGalleryPic1.TabIndex = 20;
            // 
            // ucGalleryPic3
            // 
            this.ucGalleryPic3.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ucGalleryPic3.Location = new System.Drawing.Point(615, 156);
            this.ucGalleryPic3.Media = null;
            this.ucGalleryPic3.Name = "ucGalleryPic3";
            this.ucGalleryPic3.Size = new System.Drawing.Size(300, 200);
            this.ucGalleryPic3.TabIndex = 22;
            // 
            // ucGalleryPic4
            // 
            this.ucGalleryPic4.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ucGalleryPic4.Location = new System.Drawing.Point(921, 156);
            this.ucGalleryPic4.Media = null;
            this.ucGalleryPic4.Name = "ucGalleryPic4";
            this.ucGalleryPic4.Size = new System.Drawing.Size(300, 200);
            this.ucGalleryPic4.TabIndex = 23;
            // 
            // ucGalleryPic5
            // 
            this.ucGalleryPic5.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ucGalleryPic5.Location = new System.Drawing.Point(3, 362);
            this.ucGalleryPic5.Media = null;
            this.ucGalleryPic5.Name = "ucGalleryPic5";
            this.ucGalleryPic5.Size = new System.Drawing.Size(300, 200);
            this.ucGalleryPic5.TabIndex = 24;
            // 
            // ucGalleryPic6
            // 
            this.ucGalleryPic6.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ucGalleryPic6.Location = new System.Drawing.Point(309, 362);
            this.ucGalleryPic6.Media = null;
            this.ucGalleryPic6.Name = "ucGalleryPic6";
            this.ucGalleryPic6.Size = new System.Drawing.Size(300, 200);
            this.ucGalleryPic6.TabIndex = 25;
            // 
            // ucGalleryPic7
            // 
            this.ucGalleryPic7.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ucGalleryPic7.Location = new System.Drawing.Point(615, 362);
            this.ucGalleryPic7.Media = null;
            this.ucGalleryPic7.Name = "ucGalleryPic7";
            this.ucGalleryPic7.Size = new System.Drawing.Size(300, 200);
            this.ucGalleryPic7.TabIndex = 26;
            // 
            // ucGalleryPic8
            // 
            this.ucGalleryPic8.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ucGalleryPic8.Location = new System.Drawing.Point(921, 362);
            this.ucGalleryPic8.Media = null;
            this.ucGalleryPic8.Name = "ucGalleryPic8";
            this.ucGalleryPic8.Size = new System.Drawing.Size(300, 200);
            this.ucGalleryPic8.TabIndex = 27;
            // 
            // categoryBindingSource
            // 
            this.categoryBindingSource.DataSource = typeof(BlitzerCore.Models.Category);
            // 
            // frmTile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1231, 641);
            this.Controls.Add(this.ucGalleryPic8);
            this.Controls.Add(this.ucGalleryPic7);
            this.Controls.Add(this.ucGalleryPic6);
            this.Controls.Add(this.ucGalleryPic5);
            this.Controls.Add(this.ucGalleryPic4);
            this.Controls.Add(this.ucGalleryPic3);
            this.Controls.Add(this.ucGalleryPic2);
            this.Controls.Add(this.ucGalleryPic1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lnkSelectPics);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Name = "frmTile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tile";
            this.Load += new System.EventHandler(this.frmTile_Load);
            ((System.ComponentModel.ISupportInitialize)(this.categoryBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel lnkSelectPics;
        private System.Windows.Forms.Button button1;
        private UserControls.ucGalleryPic ucGalleryPic1;
        private UserControls.ucGalleryPic ucGalleryPic2;
        private UserControls.ucGalleryPic ucGalleryPic3;
        private UserControls.ucGalleryPic ucGalleryPic4;
        private UserControls.ucGalleryPic ucGalleryPic5;
        private UserControls.ucGalleryPic ucGalleryPic6;
        private UserControls.ucGalleryPic ucGalleryPic7;
        private UserControls.ucGalleryPic ucGalleryPic8;
        private System.Windows.Forms.BindingSource categoryBindingSource;
    }
}