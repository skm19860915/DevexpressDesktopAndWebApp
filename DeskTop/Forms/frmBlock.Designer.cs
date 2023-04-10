
namespace Desktop
{ 
    partial class frmBlock
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtPageTitle = new System.Windows.Forms.TextBox();
            this.txtCaption = new System.Windows.Forms.TextBox();
            this.txtBody = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.picBlock = new System.Windows.Forms.PictureBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.linkSelectPic = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.linkSelectPage = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.picPage = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.mOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.lblPicTitle = new System.Windows.Forms.Label();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtOrder = new System.Windows.Forms.TextBox();
            this.txtListTitle = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbxPublished = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBlock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPage)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Title (Internal)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Block Title (Public)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Caption";
            // 
            // txtTitle
            // 
            this.txtTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTitle.Location = new System.Drawing.Point(152, 22);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(512, 26);
            this.txtTitle.TabIndex = 3;
            // 
            // txtPageTitle
            // 
            this.txtPageTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPageTitle.Location = new System.Drawing.Point(152, 58);
            this.txtPageTitle.Name = "txtPageTitle";
            this.txtPageTitle.Size = new System.Drawing.Size(512, 26);
            this.txtPageTitle.TabIndex = 4;
            // 
            // txtCaption
            // 
            this.txtCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCaption.Location = new System.Drawing.Point(152, 90);
            this.txtCaption.Name = "txtCaption";
            this.txtCaption.Size = new System.Drawing.Size(512, 26);
            this.txtCaption.TabIndex = 5;
            // 
            // txtBody
            // 
            this.txtBody.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBody.Location = new System.Drawing.Point(62, 224);
            this.txtBody.Multiline = true;
            this.txtBody.Name = "txtBody";
            this.txtBody.Size = new System.Drawing.Size(686, 148);
            this.txtBody.TabIndex = 6;
            this.txtBody.TextChanged += new System.EventHandler(this.txtBody_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(363, 190);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Body";
            // 
            // picBlock
            // 
            this.picBlock.Location = new System.Drawing.Point(62, 443);
            this.picBlock.Name = "picBlock";
            this.picBlock.Size = new System.Drawing.Size(227, 144);
            this.picBlock.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBlock.TabIndex = 8;
            this.picBlock.TabStop = false;
            this.picBlock.Click += new System.EventHandler(this.picBlock_Click);
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(311, 601);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(413, 601);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(159, 391);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "Picture";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // linkSelectPic
            // 
            this.linkSelectPic.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkSelectPic.Appearance.Options.UseFont = true;
            this.linkSelectPic.Location = new System.Drawing.Point(367, 477);
            this.linkSelectPic.Name = "linkSelectPic";
            this.linkSelectPic.Size = new System.Drawing.Size(73, 16);
            this.linkSelectPic.TabIndex = 12;
            this.linkSelectPic.Text = "Select Media";
            this.linkSelectPic.Click += new System.EventHandler(this.linkSelectPic_Click);
            // 
            // linkSelectPage
            // 
            this.linkSelectPage.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkSelectPage.Appearance.Options.UseFont = true;
            this.linkSelectPage.Location = new System.Drawing.Point(367, 531);
            this.linkSelectPage.Name = "linkSelectPage";
            this.linkSelectPage.Size = new System.Drawing.Size(67, 16);
            this.linkSelectPage.TabIndex = 14;
            this.linkSelectPage.Text = "Select Page";
            this.linkSelectPage.Click += new System.EventHandler(this.linkSelectPage_Click);
            // 
            // picPage
            // 
            this.picPage.Location = new System.Drawing.Point(521, 442);
            this.picPage.Name = "picPage";
            this.picPage.Size = new System.Drawing.Size(227, 144);
            this.picPage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPage.TabIndex = 15;
            this.picPage.TabStop = false;
            this.picPage.Click += new System.EventHandler(this.picPage_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(614, 391);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 20);
            this.label6.TabIndex = 16;
            this.label6.Text = "Page";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // mOpenFileDialog
            // 
            this.mOpenFileDialog.FileName = "mOpenFileDialog";
            this.mOpenFileDialog.Title = "Select a File";
            // 
            // lblPicTitle
            // 
            this.lblPicTitle.AutoSize = true;
            this.lblPicTitle.Location = new System.Drawing.Point(160, 420);
            this.lblPicTitle.Name = "lblPicTitle";
            this.lblPicTitle.Size = new System.Drawing.Size(35, 13);
            this.lblPicTitle.TabIndex = 18;
            this.lblPicTitle.Text = "label7";
            this.lblPicTitle.Click += new System.EventHandler(this.lblPicTitle_Click);
            // 
            // lblPageTitle
            // 
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.Location = new System.Drawing.Point(615, 420);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Size = new System.Drawing.Size(35, 13);
            this.lblPageTitle.TabIndex = 19;
            this.lblPageTitle.Text = "label8";
            this.lblPageTitle.Click += new System.EventHandler(this.lblPageTitle_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(62, 197);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 20);
            this.label7.TabIndex = 20;
            this.label7.Text = "Order";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // txtOrder
            // 
            this.txtOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrder.Location = new System.Drawing.Point(123, 195);
            this.txtOrder.Name = "txtOrder";
            this.txtOrder.Size = new System.Drawing.Size(51, 26);
            this.txtOrder.TabIndex = 21;
            // 
            // txtListTitle
            // 
            this.txtListTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtListTitle.Location = new System.Drawing.Point(152, 122);
            this.txtListTitle.Name = "txtListTitle";
            this.txtListTitle.Size = new System.Drawing.Size(512, 26);
            this.txtListTitle.TabIndex = 23;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(46, 130);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Block List Title";
            // 
            // cbxPublished
            // 
            this.cbxPublished.AutoSize = true;
            this.cbxPublished.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cbxPublished.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxPublished.Location = new System.Drawing.Point(686, 65);
            this.cbxPublished.Name = "cbxPublished";
            this.cbxPublished.Size = new System.Drawing.Size(82, 38);
            this.cbxPublished.TabIndex = 24;
            this.cbxPublished.Text = "Published";
            this.cbxPublished.UseVisualStyleBackColor = true;
            // 
            // frmBlock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 647);
            this.Controls.Add(this.cbxPublished);
            this.Controls.Add(this.txtListTitle);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtOrder);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblPageTitle);
            this.Controls.Add(this.lblPicTitle);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.picPage);
            this.Controls.Add(this.linkSelectPage);
            this.Controls.Add(this.linkSelectPic);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.picBlock);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtBody);
            this.Controls.Add(this.txtCaption);
            this.Controls.Add(this.txtPageTitle);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmBlock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmBlock";
            this.Load += new System.EventHandler(this.frmBlock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBlock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtPageTitle;
        private System.Windows.Forms.TextBox txtCaption;
        private System.Windows.Forms.TextBox txtBody;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox picBlock;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.HyperlinkLabelControl linkSelectPic;
        private DevExpress.XtraEditors.HyperlinkLabelControl linkSelectPage;
        private System.Windows.Forms.PictureBox picPage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.OpenFileDialog mOpenFileDialog;
        private System.Windows.Forms.Label lblPicTitle;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtOrder;
        private System.Windows.Forms.TextBox txtListTitle;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox cbxPublished;
    }
}