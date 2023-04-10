
namespace Desktop
{
    partial class CountryPage
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtPageTitle = new System.Windows.Forms.TextBox();
            this.txtHeader = new System.Windows.Forms.TextBox();
            this.txtCaption = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtSummary = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSelectVideo = new System.Windows.Forms.Button();
            this.btnSelectPicture = new System.Windows.Forms.Button();
            this.txtVideo = new System.Windows.Forms.TextBox();
            this.txtPicture = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.Conclusion = new System.Windows.Forms.Label();
            this.mOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.WaitIndicator = new DevExpress.Utils.Animation.TransitionManager(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.rdbHeader = new System.Windows.Forms.RadioButton();
            this.rdbMain = new System.Windows.Forms.RadioButton();
            this.lnkPick = new System.Windows.Forms.LinkLabel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.label7 = new System.Windows.Forms.Label();
            this.btnAddBlock = new System.Windows.Forms.Button();
            this.txtBlockTitle = new System.Windows.Forms.TextBox();
            this.linkHeader = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.cbxPublished = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(283, 778);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(506, 778);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(63, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Title (Internal)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(63, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Page Title(SEO)";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(63, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Header";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(63, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "Caption";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // txtTitle
            // 
            this.txtTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTitle.Location = new System.Drawing.Point(229, 16);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(314, 26);
            this.txtTitle.TabIndex = 6;
            this.txtTitle.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // txtPageTitle
            // 
            this.txtPageTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPageTitle.Location = new System.Drawing.Point(229, 58);
            this.txtPageTitle.Name = "txtPageTitle";
            this.txtPageTitle.Size = new System.Drawing.Size(314, 26);
            this.txtPageTitle.TabIndex = 7;
            this.txtPageTitle.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // txtHeader
            // 
            this.txtHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHeader.Location = new System.Drawing.Point(229, 100);
            this.txtHeader.Name = "txtHeader";
            this.txtHeader.Size = new System.Drawing.Size(314, 26);
            this.txtHeader.TabIndex = 8;
            this.txtHeader.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // txtCaption
            // 
            this.txtCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCaption.Location = new System.Drawing.Point(229, 142);
            this.txtCaption.Name = "txtCaption";
            this.txtCaption.Size = new System.Drawing.Size(314, 26);
            this.txtCaption.TabIndex = 9;
            this.txtCaption.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(572, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(333, 189);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // txtSummary
            // 
            this.txtSummary.Location = new System.Drawing.Point(57, 271);
            this.txtSummary.Multiline = true;
            this.txtSummary.Name = "txtSummary";
            this.txtSummary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSummary.Size = new System.Drawing.Size(822, 136);
            this.txtSummary.TabIndex = 11;
            this.txtSummary.TextChanged += new System.EventHandler(this.textBox5_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(433, 237);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "Summary";
            // 
            // btnSelectVideo
            // 
            this.btnSelectVideo.Location = new System.Drawing.Point(67, 448);
            this.btnSelectVideo.Name = "btnSelectVideo";
            this.btnSelectVideo.Size = new System.Drawing.Size(87, 23);
            this.btnSelectVideo.TabIndex = 13;
            this.btnSelectVideo.Text = "Select Video";
            this.btnSelectVideo.UseVisualStyleBackColor = true;
            this.btnSelectVideo.Click += new System.EventHandler(this.btnSelectVideo_Click);
            // 
            // btnSelectPicture
            // 
            this.btnSelectPicture.Location = new System.Drawing.Point(67, 497);
            this.btnSelectPicture.Name = "btnSelectPicture";
            this.btnSelectPicture.Size = new System.Drawing.Size(87, 23);
            this.btnSelectPicture.TabIndex = 14;
            this.btnSelectPicture.Text = "Select Picture";
            this.btnSelectPicture.UseVisualStyleBackColor = true;
            // 
            // txtVideo
            // 
            this.txtVideo.Enabled = false;
            this.txtVideo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVideo.Location = new System.Drawing.Point(160, 448);
            this.txtVideo.Name = "txtVideo";
            this.txtVideo.ReadOnly = true;
            this.txtVideo.Size = new System.Drawing.Size(576, 26);
            this.txtVideo.TabIndex = 15;
            // 
            // txtPicture
            // 
            this.txtPicture.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPicture.Location = new System.Drawing.Point(160, 494);
            this.txtPicture.Name = "txtPicture";
            this.txtPicture.Size = new System.Drawing.Size(576, 26);
            this.txtPicture.TabIndex = 16;
            this.txtPicture.TextChanged += new System.EventHandler(this.textBox2_TextChanged_1);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(450, 425);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 20);
            this.label6.TabIndex = 17;
            this.label6.Text = "Titles";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(57, 704);
            this.textBox6.Multiline = true;
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(822, 68);
            this.textBox6.TabIndex = 24;
            // 
            // Conclusion
            // 
            this.Conclusion.AutoSize = true;
            this.Conclusion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Conclusion.Location = new System.Drawing.Point(434, 685);
            this.Conclusion.Name = "Conclusion";
            this.Conclusion.Size = new System.Drawing.Size(84, 16);
            this.Conclusion.TabIndex = 26;
            this.Conclusion.Text = "Conclusion";
            this.Conclusion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mOpenFileDialog
            // 
            this.mOpenFileDialog.FileName = "mOpenFileDialog";
            this.mOpenFileDialog.Filter = "All Video Files|*.mp4;";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(229, 179);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(314, 26);
            this.textBox1.TabIndex = 27;
            // 
            // rdbHeader
            // 
            this.rdbHeader.AutoSize = true;
            this.rdbHeader.Checked = true;
            this.rdbHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbHeader.Location = new System.Drawing.Point(75, 187);
            this.rdbHeader.Name = "rdbHeader";
            this.rdbHeader.Size = new System.Drawing.Size(14, 13);
            this.rdbHeader.TabIndex = 28;
            this.rdbHeader.TabStop = true;
            this.rdbHeader.UseVisualStyleBackColor = true;
            this.rdbHeader.CheckedChanged += new System.EventHandler(this.rdbHeader_CheckedChanged);
            this.rdbHeader.Click += new System.EventHandler(this.rdbHeader_Click);
            // 
            // rdbMain
            // 
            this.rdbMain.AutoSize = true;
            this.rdbMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbMain.Location = new System.Drawing.Point(147, 187);
            this.rdbMain.Name = "rdbMain";
            this.rdbMain.Size = new System.Drawing.Size(14, 13);
            this.rdbMain.TabIndex = 29;
            this.rdbMain.UseVisualStyleBackColor = true;
            this.rdbMain.CheckedChanged += new System.EventHandler(this.rdbMain_CheckedChanged);
            this.rdbMain.Click += new System.EventHandler(this.rdbMain_Click);
            // 
            // lnkPick
            // 
            this.lnkPick.AutoSize = true;
            this.lnkPick.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkPick.Location = new System.Drawing.Point(0, 183);
            this.lnkPick.Name = "lnkPick";
            this.lnkPick.Size = new System.Drawing.Size(65, 20);
            this.lnkPick.TabIndex = 30;
            this.lnkPick.TabStop = true;
            this.lnkPick.Text = "Change";
            this.lnkPick.UseMnemonic = false;
            this.lnkPick.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkPick_LinkClicked);
            // 
            // listView1
            // 
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(67, 565);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(669, 97);
            this.listView1.TabIndex = 31;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.Click += new System.EventHandler(this.listView1_Click);
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(65, 536);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 20);
            this.label7.TabIndex = 32;
            this.label7.Text = "Blocks";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // btnAddBlock
            // 
            this.btnAddBlock.Location = new System.Drawing.Point(770, 595);
            this.btnAddBlock.Name = "btnAddBlock";
            this.btnAddBlock.Size = new System.Drawing.Size(109, 23);
            this.btnAddBlock.TabIndex = 33;
            this.btnAddBlock.Text = "Add Block";
            this.btnAddBlock.UseVisualStyleBackColor = true;
            this.btnAddBlock.Click += new System.EventHandler(this.btnAddBlock_Click);
            // 
            // txtBlockTitle
            // 
            this.txtBlockTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBlockTitle.Location = new System.Drawing.Point(160, 533);
            this.txtBlockTitle.Name = "txtBlockTitle";
            this.txtBlockTitle.Size = new System.Drawing.Size(576, 26);
            this.txtBlockTitle.TabIndex = 34;
            // 
            // linkHeader
            // 
            this.linkHeader.AutoSize = true;
            this.linkHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkHeader.Location = new System.Drawing.Point(95, 187);
            this.linkHeader.Name = "linkHeader";
            this.linkHeader.Size = new System.Drawing.Size(42, 13);
            this.linkHeader.TabIndex = 35;
            this.linkHeader.TabStop = true;
            this.linkHeader.Text = "Header";
            this.linkHeader.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkHeader_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(164, 187);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(58, 13);
            this.linkLabel2.TabIndex = 37;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "ThumbNail";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // cbxPublished
            // 
            this.cbxPublished.AutoSize = true;
            this.cbxPublished.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbxPublished.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxPublished.Location = new System.Drawing.Point(57, 239);
            this.cbxPublished.Name = "cbxPublished";
            this.cbxPublished.Size = new System.Drawing.Size(97, 24);
            this.cbxPublished.TabIndex = 38;
            this.cbxPublished.Text = "Published";
            this.cbxPublished.UseVisualStyleBackColor = true;
            // 
            // CountryPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 813);
            this.Controls.Add(this.cbxPublished);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.linkHeader);
            this.Controls.Add(this.txtBlockTitle);
            this.Controls.Add(this.btnAddBlock);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.lnkPick);
            this.Controls.Add(this.rdbMain);
            this.Controls.Add(this.rdbHeader);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Conclusion);
            this.Controls.Add(this.textBox6);
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
            this.Controls.Add(this.button1);
            this.Name = "CountryPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CountryPage";
            this.Load += new System.EventHandler(this.CountryPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtPageTitle;
        private System.Windows.Forms.TextBox txtHeader;
        private System.Windows.Forms.TextBox txtCaption;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtSummary;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSelectVideo;
        private System.Windows.Forms.Button btnSelectPicture;
        private System.Windows.Forms.TextBox txtVideo;
        private System.Windows.Forms.TextBox txtPicture;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label Conclusion;
        private System.Windows.Forms.OpenFileDialog mOpenFileDialog;
        private DevExpress.Utils.Animation.TransitionManager WaitIndicator;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RadioButton rdbHeader;
        private System.Windows.Forms.RadioButton rdbMain;
        private System.Windows.Forms.LinkLabel lnkPick;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnAddBlock;
        private System.Windows.Forms.TextBox txtBlockTitle;
        private System.Windows.Forms.LinkLabel linkHeader;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.CheckBox cbxPublished;
    }
}