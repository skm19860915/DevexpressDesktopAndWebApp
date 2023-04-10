
namespace Desktop.UserControls
{
    partial class ucWebPage
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
            this.components = new System.ComponentModel.Container();
            this.cbxPublished = new System.Windows.Forms.CheckBox();
            this.linkHeader = new System.Windows.Forms.LinkLabel();
            this.txtBlockTitle = new System.Windows.Forms.TextBox();
            this.btnAddBlock = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.lnkPick = new System.Windows.Forms.LinkLabel();
            this.rdbMain = new System.Windows.Forms.RadioButton();
            this.rdbHeader = new System.Windows.Forms.RadioButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.WaitIndicator = new DevExpress.Utils.Animation.TransitionManager(this.components);
            this.mOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.Conclusion = new System.Windows.Forms.Label();
            this.txtConclusion = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPicture = new System.Windows.Forms.TextBox();
            this.txtVideo = new System.Windows.Forms.TextBox();
            this.btnSelectPicture = new System.Windows.Forms.Button();
            this.btnSelectVideo = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSummary = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtCaption = new System.Windows.Forms.TextBox();
            this.txtHeader = new System.Windows.Forms.TextBox();
            this.txtPageTitle = new System.Windows.Forms.TextBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxPublished
            // 
            this.cbxPublished.AutoSize = true;
            this.cbxPublished.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbxPublished.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxPublished.Location = new System.Drawing.Point(60, 236);
            this.cbxPublished.Name = "cbxPublished";
            this.cbxPublished.Size = new System.Drawing.Size(97, 24);
            this.cbxPublished.TabIndex = 69;
            this.cbxPublished.Text = "Published";
            this.cbxPublished.UseVisualStyleBackColor = true;
            // 
            // linkHeader
            // 
            this.linkHeader.AutoSize = true;
            this.linkHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkHeader.Location = new System.Drawing.Point(98, 184);
            this.linkHeader.Name = "linkHeader";
            this.linkHeader.Size = new System.Drawing.Size(42, 13);
            this.linkHeader.TabIndex = 67;
            this.linkHeader.TabStop = true;
            this.linkHeader.Text = "Header";
            this.linkHeader.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkHeader_LinkClicked);
            // 
            // txtBlockTitle
            // 
            this.txtBlockTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBlockTitle.Location = new System.Drawing.Point(163, 530);
            this.txtBlockTitle.Name = "txtBlockTitle";
            this.txtBlockTitle.Size = new System.Drawing.Size(576, 26);
            this.txtBlockTitle.TabIndex = 66;
            // 
            // btnAddBlock
            // 
            this.btnAddBlock.Location = new System.Drawing.Point(773, 592);
            this.btnAddBlock.Name = "btnAddBlock";
            this.btnAddBlock.Size = new System.Drawing.Size(109, 23);
            this.btnAddBlock.TabIndex = 65;
            this.btnAddBlock.Text = "Add Block";
            this.btnAddBlock.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(68, 533);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 20);
            this.label7.TabIndex = 64;
            this.label7.Text = "Blocks";
            // 
            // listView1
            // 
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(70, 562);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(669, 97);
            this.listView1.TabIndex = 63;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // lnkPick
            // 
            this.lnkPick.AutoSize = true;
            this.lnkPick.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkPick.Location = new System.Drawing.Point(3, 180);
            this.lnkPick.Name = "lnkPick";
            this.lnkPick.Size = new System.Drawing.Size(65, 20);
            this.lnkPick.TabIndex = 62;
            this.lnkPick.TabStop = true;
            this.lnkPick.Text = "Change";
            this.lnkPick.UseMnemonic = false;
            this.lnkPick.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkPick_LinkClicked);
            // 
            // rdbMain
            // 
            this.rdbMain.AutoSize = true;
            this.rdbMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbMain.Location = new System.Drawing.Point(150, 184);
            this.rdbMain.Name = "rdbMain";
            this.rdbMain.Size = new System.Drawing.Size(14, 13);
            this.rdbMain.TabIndex = 61;
            this.rdbMain.UseVisualStyleBackColor = true;
            this.rdbMain.CheckedChanged += new System.EventHandler(this.rdbMain_CheckedChanged);
            // 
            // rdbHeader
            // 
            this.rdbHeader.AutoSize = true;
            this.rdbHeader.Checked = true;
            this.rdbHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbHeader.Location = new System.Drawing.Point(78, 184);
            this.rdbHeader.Name = "rdbHeader";
            this.rdbHeader.Size = new System.Drawing.Size(14, 13);
            this.rdbHeader.TabIndex = 60;
            this.rdbHeader.TabStop = true;
            this.rdbHeader.UseVisualStyleBackColor = true;
            this.rdbHeader.Click += new System.EventHandler(this.rdbHeader_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(232, 176);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(314, 26);
            this.textBox1.TabIndex = 59;
            // 
            // mOpenFileDialog
            // 
            this.mOpenFileDialog.FileName = "mOpenFileDialog";
            this.mOpenFileDialog.Filter = "All Video Files|*.mp4;";
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(167, 184);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(58, 13);
            this.linkLabel2.TabIndex = 68;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "ThumbNail";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // Conclusion
            // 
            this.Conclusion.AutoSize = true;
            this.Conclusion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Conclusion.Location = new System.Drawing.Point(437, 682);
            this.Conclusion.Name = "Conclusion";
            this.Conclusion.Size = new System.Drawing.Size(84, 16);
            this.Conclusion.TabIndex = 58;
            this.Conclusion.Text = "Conclusion";
            this.Conclusion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtConclusion
            // 
            this.txtConclusion.Location = new System.Drawing.Point(60, 701);
            this.txtConclusion.Multiline = true;
            this.txtConclusion.Name = "txtConclusion";
            this.txtConclusion.Size = new System.Drawing.Size(822, 68);
            this.txtConclusion.TabIndex = 57;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(453, 422);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 20);
            this.label6.TabIndex = 56;
            this.label6.Text = "Titles";
            // 
            // txtPicture
            // 
            this.txtPicture.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPicture.Location = new System.Drawing.Point(163, 491);
            this.txtPicture.Name = "txtPicture";
            this.txtPicture.Size = new System.Drawing.Size(576, 26);
            this.txtPicture.TabIndex = 55;
            // 
            // txtVideo
            // 
            this.txtVideo.Enabled = false;
            this.txtVideo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVideo.Location = new System.Drawing.Point(163, 445);
            this.txtVideo.Name = "txtVideo";
            this.txtVideo.ReadOnly = true;
            this.txtVideo.Size = new System.Drawing.Size(576, 26);
            this.txtVideo.TabIndex = 54;
            // 
            // btnSelectPicture
            // 
            this.btnSelectPicture.Location = new System.Drawing.Point(70, 494);
            this.btnSelectPicture.Name = "btnSelectPicture";
            this.btnSelectPicture.Size = new System.Drawing.Size(87, 23);
            this.btnSelectPicture.TabIndex = 53;
            this.btnSelectPicture.Text = "Select Picture";
            this.btnSelectPicture.UseVisualStyleBackColor = true;
            // 
            // btnSelectVideo
            // 
            this.btnSelectVideo.Location = new System.Drawing.Point(70, 445);
            this.btnSelectVideo.Name = "btnSelectVideo";
            this.btnSelectVideo.Size = new System.Drawing.Size(87, 23);
            this.btnSelectVideo.TabIndex = 52;
            this.btnSelectVideo.Text = "Select Video";
            this.btnSelectVideo.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(436, 234);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 20);
            this.label5.TabIndex = 51;
            this.label5.Text = "Summary";
            // 
            // txtSummary
            // 
            this.txtSummary.Location = new System.Drawing.Point(60, 268);
            this.txtSummary.Multiline = true;
            this.txtSummary.Name = "txtSummary";
            this.txtSummary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSummary.Size = new System.Drawing.Size(822, 136);
            this.txtSummary.TabIndex = 50;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(575, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(333, 189);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 49;
            this.pictureBox1.TabStop = false;
            // 
            // txtCaption
            // 
            this.txtCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCaption.Location = new System.Drawing.Point(232, 139);
            this.txtCaption.Name = "txtCaption";
            this.txtCaption.Size = new System.Drawing.Size(314, 26);
            this.txtCaption.TabIndex = 48;
            // 
            // txtHeader
            // 
            this.txtHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHeader.Location = new System.Drawing.Point(232, 97);
            this.txtHeader.Name = "txtHeader";
            this.txtHeader.Size = new System.Drawing.Size(314, 26);
            this.txtHeader.TabIndex = 47;
            // 
            // txtPageTitle
            // 
            this.txtPageTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPageTitle.Location = new System.Drawing.Point(232, 55);
            this.txtPageTitle.Name = "txtPageTitle";
            this.txtPageTitle.Size = new System.Drawing.Size(314, 26);
            this.txtPageTitle.TabIndex = 46;
            // 
            // txtTitle
            // 
            this.txtTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTitle.Location = new System.Drawing.Point(232, 13);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(314, 26);
            this.txtTitle.TabIndex = 45;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(66, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 20);
            this.label4.TabIndex = 44;
            this.label4.Text = "Caption";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(66, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 20);
            this.label3.TabIndex = 43;
            this.label3.Text = "Header";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(66, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 20);
            this.label2.TabIndex = 42;
            this.label2.Text = "Page Title(SEO)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(66, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 20);
            this.label1.TabIndex = 41;
            this.label1.Text = "Title (Internal)";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(535, 775);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 40;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(306, 775);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 39;
            this.btnSave.Text = "Ok";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ucWebPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbxPublished);
            this.Controls.Add(this.linkHeader);
            this.Controls.Add(this.txtBlockTitle);
            this.Controls.Add(this.btnAddBlock);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.lnkPick);
            this.Controls.Add(this.rdbMain);
            this.Controls.Add(this.rdbHeader);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.Conclusion);
            this.Controls.Add(this.txtConclusion);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtPicture);
            this.Controls.Add(this.txtVideo);
            this.Controls.Add(this.btnSelectPicture);
            this.Controls.Add(this.btnSelectVideo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtSummary);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtCaption);
            this.Controls.Add(this.txtHeader);
            this.Controls.Add(this.txtPageTitle);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnSave);
            this.Name = "ucWebPage";
            this.Size = new System.Drawing.Size(940, 817);
            this.Load += new System.EventHandler(this.ucWebPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbxPublished;
        private System.Windows.Forms.LinkLabel linkHeader;
        private System.Windows.Forms.TextBox txtBlockTitle;
        private System.Windows.Forms.Button btnAddBlock;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.LinkLabel lnkPick;
        private System.Windows.Forms.RadioButton rdbMain;
        private System.Windows.Forms.RadioButton rdbHeader;
        private System.Windows.Forms.TextBox textBox1;
        private DevExpress.Utils.Animation.TransitionManager WaitIndicator;
        private System.Windows.Forms.OpenFileDialog mOpenFileDialog;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Label Conclusion;
        private System.Windows.Forms.TextBox txtConclusion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPicture;
        private System.Windows.Forms.TextBox txtVideo;
        private System.Windows.Forms.Button btnSelectPicture;
        private System.Windows.Forms.Button btnSelectVideo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSummary;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtCaption;
        private System.Windows.Forms.TextBox txtHeader;
        private System.Windows.Forms.TextBox txtPageTitle;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnSave;
    }
}
