
namespace Desktop
{
    partial class frmMedia
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
            this.btnSave = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.picThumbnail = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.categoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.countryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmbResort = new System.Windows.Forms.ComboBox();
            this.uIResortBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.cbx560x460 = new System.Windows.Forms.CheckBox();
            this.cbx1024x640 = new System.Windows.Forms.CheckBox();
            this.link1024x640 = new System.Windows.Forms.LinkLabel();
            this.cbx1600x1200 = new System.Windows.Forms.CheckBox();
            this.link1600x1200 = new System.Windows.Forms.LinkLabel();
            this.cbxMPEG = new System.Windows.Forms.CheckBox();
            this.linkVideo = new System.Windows.Forms.LinkLabel();
            this.btnUpload = new System.Windows.Forms.Button();
            this.mOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.lblRequired = new System.Windows.Forms.Label();
            this.lblSaveVal = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.picThumbnail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.categoryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uIResortBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(356, 405);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(511, 404);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(87, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // picThumbnail
            // 
            this.picThumbnail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picThumbnail.Location = new System.Drawing.Point(499, 16);
            this.picThumbnail.Name = "picThumbnail";
            this.picThumbnail.Size = new System.Drawing.Size(400, 248);
            this.picThumbnail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picThumbnail.TabIndex = 2;
            this.picThumbnail.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Title";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtTitle
            // 
            this.txtTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTitle.Location = new System.Drawing.Point(145, 16);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(324, 26);
            this.txtTitle.TabIndex = 4;
            this.txtTitle.TextChanged += new System.EventHandler(this.txtTitle_TextChanged);
            this.txtTitle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTitle_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(22, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Category";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(26, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Location";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(26, 239);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Resort";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // cmbCategory
            // 
            this.cmbCategory.DataSource = this.categoryBindingSource;
            this.cmbCategory.DisplayMember = "Name";
            this.cmbCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(145, 82);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(324, 28);
            this.cmbCategory.TabIndex = 8;
            this.cmbCategory.ValueMember = "Id";
            this.cmbCategory.SelectedIndexChanged += new System.EventHandler(this.cmbCategory_SelectedIndexChanged);
            // 
            // categoryBindingSource
            // 
            this.categoryBindingSource.DataSource = typeof(BlitzerCore.Models.Category);
            // 
            // cmbLocation
            // 
            this.cmbLocation.DataSource = this.countryBindingSource;
            this.cmbLocation.DisplayMember = "Name";
            this.cmbLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(145, 166);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(324, 28);
            this.cmbLocation.TabIndex = 9;
            this.cmbLocation.ValueMember = "Id";
            this.cmbLocation.SelectedIndexChanged += new System.EventHandler(this.cmbLocation_SelectedIndexChanged);
            // 
            // countryBindingSource
            // 
            this.countryBindingSource.DataSource = typeof(BlitzerCore.Models.Country);
            // 
            // cmbResort
            // 
            this.cmbResort.DataSource = this.uIResortBindingSource;
            this.cmbResort.DisplayMember = "Title";
            this.cmbResort.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbResort.FormattingEnabled = true;
            this.cmbResort.Location = new System.Drawing.Point(145, 236);
            this.cmbResort.Name = "cmbResort";
            this.cmbResort.Size = new System.Drawing.Size(324, 28);
            this.cmbResort.TabIndex = 10;
            this.cmbResort.ValueMember = "Id";
            // 
            // uIResortBindingSource
            // 
            this.uIResortBindingSource.DataSource = typeof(BlitzerCore.Models.UI.UIResortPage);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(35, 331);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(70, 20);
            this.linkLabel1.TabIndex = 11;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "560x460";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // cbx560x460
            // 
            this.cbx560x460.AutoSize = true;
            this.cbx560x460.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbx560x460.Location = new System.Drawing.Point(119, 335);
            this.cbx560x460.Name = "cbx560x460";
            this.cbx560x460.Size = new System.Drawing.Size(15, 14);
            this.cbx560x460.TabIndex = 12;
            this.cbx560x460.UseVisualStyleBackColor = true;
            this.cbx560x460.CheckedChanged += new System.EventHandler(this.cbx560x460_CheckedChanged);
            // 
            // cbx1024x640
            // 
            this.cbx1024x640.AutoSize = true;
            this.cbx1024x640.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbx1024x640.Location = new System.Drawing.Point(292, 335);
            this.cbx1024x640.Name = "cbx1024x640";
            this.cbx1024x640.Size = new System.Drawing.Size(15, 14);
            this.cbx1024x640.TabIndex = 14;
            this.cbx1024x640.UseVisualStyleBackColor = true;
            this.cbx1024x640.CheckedChanged += new System.EventHandler(this.cbx1024x640_CheckedChanged);
            // 
            // link1024x640
            // 
            this.link1024x640.AutoSize = true;
            this.link1024x640.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.link1024x640.Location = new System.Drawing.Point(201, 331);
            this.link1024x640.Name = "link1024x640";
            this.link1024x640.Size = new System.Drawing.Size(79, 20);
            this.link1024x640.TabIndex = 13;
            this.link1024x640.TabStop = true;
            this.link1024x640.Text = "1024x640";
            this.link1024x640.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // cbx1600x1200
            // 
            this.cbx1600x1200.AutoSize = true;
            this.cbx1600x1200.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbx1600x1200.Location = new System.Drawing.Point(479, 335);
            this.cbx1600x1200.Name = "cbx1600x1200";
            this.cbx1600x1200.Size = new System.Drawing.Size(15, 14);
            this.cbx1600x1200.TabIndex = 16;
            this.cbx1600x1200.UseVisualStyleBackColor = true;
            this.cbx1600x1200.CheckedChanged += new System.EventHandler(this.cbx1600x1200_CheckedChanged);
            // 
            // link1600x1200
            // 
            this.link1600x1200.AutoSize = true;
            this.link1600x1200.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.link1600x1200.Location = new System.Drawing.Point(382, 331);
            this.link1600x1200.Name = "link1600x1200";
            this.link1600x1200.Size = new System.Drawing.Size(88, 20);
            this.link1600x1200.TabIndex = 15;
            this.link1600x1200.TabStop = true;
            this.link1600x1200.Text = "1600x1200";
            this.link1600x1200.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link1600x1200_LinkClicked);
            // 
            // cbxMPEG
            // 
            this.cbxMPEG.AutoSize = true;
            this.cbxMPEG.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxMPEG.Location = new System.Drawing.Point(684, 331);
            this.cbxMPEG.Name = "cbxMPEG";
            this.cbxMPEG.Size = new System.Drawing.Size(15, 14);
            this.cbxMPEG.TabIndex = 18;
            this.cbxMPEG.UseVisualStyleBackColor = true;
            this.cbxMPEG.CheckedChanged += new System.EventHandler(this.cbxMPEG_CheckedChanged);
            // 
            // linkVideo
            // 
            this.linkVideo.AutoSize = true;
            this.linkVideo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkVideo.Location = new System.Drawing.Point(616, 327);
            this.linkVideo.Name = "linkVideo";
            this.linkVideo.Size = new System.Drawing.Size(59, 20);
            this.linkVideo.TabIndex = 17;
            this.linkVideo.TabStop = true;
            this.linkVideo.Text = "MPeg4";
            this.linkVideo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkVideo_LinkClicked);
            // 
            // btnUpload
            // 
            this.btnUpload.BackColor = System.Drawing.Color.Crimson;
            this.btnUpload.Enabled = false;
            this.btnUpload.ForeColor = System.Drawing.Color.White;
            this.btnUpload.Location = new System.Drawing.Point(40, 390);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(122, 48);
            this.btnUpload.TabIndex = 19;
            this.btnUpload.Text = "Upload Image";
            this.btnUpload.UseVisualStyleBackColor = false;
            this.btnUpload.Visible = false;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // mOpenFileDialog
            // 
            this.mOpenFileDialog.FileName = "openFileDialog1";
            // 
            // lblRequired
            // 
            this.lblRequired.AutoSize = true;
            this.lblRequired.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRequired.ForeColor = System.Drawing.Color.Red;
            this.lblRequired.Location = new System.Drawing.Point(34, 371);
            this.lblRequired.Name = "lblRequired";
            this.lblRequired.Size = new System.Drawing.Size(167, 13);
            this.lblRequired.TabIndex = 20;
            this.lblRequired.Text = "Requires Title and Dimension first!";
            this.lblRequired.Visible = false;
            // 
            // lblSaveVal
            // 
            this.lblSaveVal.AutoSize = true;
            this.lblSaveVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSaveVal.ForeColor = System.Drawing.Color.Red;
            this.lblSaveVal.Location = new System.Drawing.Point(326, 380);
            this.lblSaveVal.Name = "lblSaveVal";
            this.lblSaveVal.Size = new System.Drawing.Size(62, 13);
            this.lblSaveVal.TabIndex = 21;
            this.lblSaveVal.Text = "Starter Text";
            this.lblSaveVal.Click += new System.EventHandler(this.label5_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(770, 325);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(105, 24);
            this.checkBox1.TabIndex = 22;
            this.checkBox1.Text = "For Gallery";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // frmMedia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 450);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.lblSaveVal);
            this.Controls.Add(this.lblRequired);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.cbxMPEG);
            this.Controls.Add(this.linkVideo);
            this.Controls.Add(this.cbx1600x1200);
            this.Controls.Add(this.link1600x1200);
            this.Controls.Add(this.cbx1024x640);
            this.Controls.Add(this.link1024x640);
            this.Controls.Add(this.cbx560x460);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.cmbResort);
            this.Controls.Add(this.cmbLocation);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picThumbnail);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnSave);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "frmMedia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Media";
            this.Load += new System.EventHandler(this.frmMedia_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picThumbnail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.categoryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uIResortBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox picThumbnail;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.ComboBox cmbResort;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.CheckBox cbx560x460;
        private System.Windows.Forms.CheckBox cbx1024x640;
        private System.Windows.Forms.LinkLabel link1024x640;
        private System.Windows.Forms.CheckBox cbx1600x1200;
        private System.Windows.Forms.LinkLabel link1600x1200;
        private System.Windows.Forms.CheckBox cbxMPEG;
        private System.Windows.Forms.LinkLabel linkVideo;
        private System.Windows.Forms.BindingSource categoryBindingSource;
        private System.Windows.Forms.BindingSource countryBindingSource;
        private System.Windows.Forms.BindingSource uIResortBindingSource;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.OpenFileDialog mOpenFileDialog;
        private System.Windows.Forms.Label lblRequired;
        private System.Windows.Forms.Label lblSaveVal;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}