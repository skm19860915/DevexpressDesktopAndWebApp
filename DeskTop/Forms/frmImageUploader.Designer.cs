
namespace Desktop
{
    partial class frmImageUploader
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
            this.xtraFolderBrowserDialog1 = new DevExpress.XtraEditors.XtraFolderBrowserDialog(this.components);
            this.lblFileName = new DevExpress.XtraEditors.LabelControl();
            this.txtFileName = new DevExpress.XtraEditors.TextEdit();
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.rdo560x460 = new System.Windows.Forms.RadioButton();
            this.rdo1600x1200 = new System.Windows.Forms.RadioButton();
            this.btnUpload = new System.Windows.Forms.Button();
            this.dlgOpenFile = new DevExpress.XtraEditors.XtraOpenFileDialog(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraFolderBrowserDialog1
            // 
            this.xtraFolderBrowserDialog1.SelectedPath = "xtraFolderBrowserDialog1";
            this.xtraFolderBrowserDialog1.HelpRequest += new System.EventHandler(this.xtraFolderBrowserDialog1_HelpRequest);
            // 
            // lblFileName
            // 
            this.lblFileName.Location = new System.Drawing.Point(52, 48);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(46, 13);
            this.lblFileName.TabIndex = 0;
            this.lblFileName.Text = "File Name";
            this.lblFileName.Click += new System.EventHandler(this.lblFileName_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(123, 45);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(164, 20);
            this.txtFileName.TabIndex = 1;
            // 
            // memoEdit1
            // 
            this.memoEdit1.Location = new System.Drawing.Point(52, 124);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Size = new System.Drawing.Size(515, 160);
            this.memoEdit1.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(278, 95);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(53, 13);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Description";
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(343, 26);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Size = new System.Drawing.Size(114, 60);
            this.radioGroup1.TabIndex = 4;
            // 
            // rdo560x460
            // 
            this.rdo560x460.AutoSize = true;
            this.rdo560x460.Location = new System.Drawing.Point(364, 33);
            this.rdo560x460.Name = "rdo560x460";
            this.rdo560x460.Size = new System.Drawing.Size(66, 17);
            this.rdo560x460.TabIndex = 5;
            this.rdo560x460.TabStop = true;
            this.rdo560x460.Text = "560x460";
            this.rdo560x460.UseVisualStyleBackColor = true;
            // 
            // rdo1600x1200
            // 
            this.rdo1600x1200.AutoSize = true;
            this.rdo1600x1200.Location = new System.Drawing.Point(364, 57);
            this.rdo1600x1200.Name = "rdo1600x1200";
            this.rdo1600x1200.Size = new System.Drawing.Size(78, 17);
            this.rdo1600x1200.TabIndex = 6;
            this.rdo1600x1200.TabStop = true;
            this.rdo1600x1200.Text = "1600x1200";
            this.rdo1600x1200.UseVisualStyleBackColor = true;
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(278, 336);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 23);
            this.btnUpload.TabIndex = 7;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            // 
            // dlgOpenFile
            // 
            this.dlgOpenFile.FileName = "xtraOpenFileDialog1";
            // 
            // frmImageUploader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 392);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.rdo1600x1200);
            this.Controls.Add(this.rdo560x460);
            this.Controls.Add(this.radioGroup1);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.memoEdit1);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.lblFileName);
            this.Name = "frmImageUploader";
            this.Text = "Image Uploader";
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.XtraFolderBrowserDialog xtraFolderBrowserDialog1;
        private DevExpress.XtraEditors.LabelControl lblFileName;
        private DevExpress.XtraEditors.TextEdit txtFileName;
        private DevExpress.XtraEditors.MemoEdit memoEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private System.Windows.Forms.RadioButton rdo560x460;
        private System.Windows.Forms.RadioButton rdo1600x1200;
        private System.Windows.Forms.Button btnUpload;
        private DevExpress.XtraEditors.XtraOpenFileDialog dlgOpenFile;
    }
}

