
namespace Desktop.ModuleDetail
{
    partial class EditOpportunity
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtOppName = new System.Windows.Forms.TextBox();
            this.lstTravelers = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDestination = new System.Windows.Forms.TextBox();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dteDeparture = new DevExpress.XtraEditors.DateEdit();
            this.dteDepart = new DevExpress.XtraEditors.DateEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbDepature = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbReturn = new DevExpress.XtraEditors.ComboBoxEdit();
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.txtKid1Age = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtKidAge = new System.Windows.Forms.TextBox();
            this.txtKidAge3 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnAddPresentaion = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.dteDeparture.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDeparture.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDepart.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDepart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDepature.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReturn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(40, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name";
            // 
            // txtOppName
            // 
            this.txtOppName.Enabled = false;
            this.txtOppName.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtOppName.Location = new System.Drawing.Point(105, 31);
            this.txtOppName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtOppName.Name = "txtOppName";
            this.txtOppName.ReadOnly = true;
            this.txtOppName.Size = new System.Drawing.Size(405, 32);
            this.txtOppName.TabIndex = 2;
            // 
            // lstTravelers
            // 
            this.lstTravelers.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lstTravelers.FormattingEnabled = true;
            this.lstTravelers.ItemHeight = 24;
            this.lstTravelers.Location = new System.Drawing.Point(44, 134);
            this.lstTravelers.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lstTravelers.Name = "lstTravelers";
            this.lstTravelers.Size = new System.Drawing.Size(206, 76);
            this.lstTravelers.TabIndex = 5;
            this.lstTravelers.DoubleClick += new System.EventHandler(this.lstTravelers_DoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(40, 107);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 24);
            this.label2.TabIndex = 6;
            this.label2.Text = "Travelers";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(279, 145);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(88, 28);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(279, 192);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(88, 28);
            this.btnRemove.TabIndex = 8;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(533, 34);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 24);
            this.label3.TabIndex = 9;
            this.label3.Text = "Destination";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // txtDestination
            // 
            this.txtDestination.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtDestination.Location = new System.Drawing.Point(643, 31);
            this.txtDestination.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.Size = new System.Drawing.Size(429, 32);
            this.txtDestination.TabIndex = 10;
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.Name = "sqlDataSource1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(776, 145);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 24);
            this.label4.TabIndex = 11;
            this.label4.Text = "Departure";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(776, 192);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 24);
            this.label5.TabIndex = 12;
            this.label5.Text = "Return";
            // 
            // dteDeparture
            // 
            this.dteDeparture.EditValue = null;
            this.dteDeparture.Location = new System.Drawing.Point(876, 137);
            this.dteDeparture.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dteDeparture.Name = "dteDeparture";
            this.dteDeparture.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dteDeparture.Properties.Appearance.Options.UseFont = true;
            this.dteDeparture.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteDeparture.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteDeparture.Size = new System.Drawing.Size(182, 30);
            this.dteDeparture.TabIndex = 13;
            // 
            // dteDepart
            // 
            this.dteDepart.EditValue = null;
            this.dteDepart.Location = new System.Drawing.Point(876, 188);
            this.dteDepart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dteDepart.Name = "dteDepart";
            this.dteDepart.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dteDepart.Properties.Appearance.Options.UseFont = true;
            this.dteDepart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteDepart.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteDepart.Size = new System.Drawing.Size(182, 30);
            this.dteDepart.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(542, 192);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 24);
            this.label6.TabIndex = 16;
            this.label6.Text = "Return";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(542, 145);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 24);
            this.label7.TabIndex = 15;
            this.label7.Text = "Departure";
            // 
            // cmbDepature
            // 
            this.cmbDepature.Location = new System.Drawing.Point(643, 142);
            this.cmbDepature.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbDepature.Name = "cmbDepature";
            this.cmbDepature.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbDepature.Properties.Appearance.Options.UseFont = true;
            this.cmbDepature.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDepature.Size = new System.Drawing.Size(117, 30);
            this.cmbDepature.TabIndex = 17;
            // 
            // cmbReturn
            // 
            this.cmbReturn.Location = new System.Drawing.Point(643, 188);
            this.cmbReturn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbReturn.Name = "cmbReturn";
            this.cmbReturn.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbReturn.Properties.Appearance.Options.UseFont = true;
            this.cmbReturn.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbReturn.Size = new System.Drawing.Size(117, 30);
            this.cmbReturn.TabIndex = 18;
            // 
            // memoEdit1
            // 
            this.memoEdit1.Location = new System.Drawing.Point(44, 432);
            this.memoEdit1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.memoEdit1.Properties.Appearance.Options.UseFont = true;
            this.memoEdit1.Size = new System.Drawing.Size(1028, 160);
            this.memoEdit1.TabIndex = 19;
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(168, 282);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(140, 32);
            this.comboBox1.TabIndex = 20;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(56, 286);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(109, 24);
            this.label8.TabIndex = 21;
            this.label8.Text = "# of Adults";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label9.Location = new System.Drawing.Point(56, 331);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(114, 24);
            this.label9.TabIndex = 22;
            this.label9.Text = "# of Rooms";
            // 
            // comboBox2
            // 
            this.comboBox2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(168, 327);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(140, 32);
            this.comboBox2.TabIndex = 23;
            // 
            // txtKid1Age
            // 
            this.txtKid1Age.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtKid1Age.Location = new System.Drawing.Point(663, 282);
            this.txtKid1Age.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtKid1Age.Name = "txtKid1Age";
            this.txtKid1Age.Size = new System.Drawing.Size(70, 32);
            this.txtKid1Age.TabIndex = 24;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label10.Location = new System.Drawing.Point(556, 286);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(96, 24);
            this.label10.TabIndex = 25;
            this.label10.Text = "Kids Ages";
            // 
            // txtKidAge
            // 
            this.txtKidAge.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtKidAge.Location = new System.Drawing.Point(771, 282);
            this.txtKidAge.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtKidAge.Name = "txtKidAge";
            this.txtKidAge.Size = new System.Drawing.Size(70, 32);
            this.txtKidAge.TabIndex = 26;
            // 
            // txtKidAge3
            // 
            this.txtKidAge3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtKidAge3.Location = new System.Drawing.Point(880, 282);
            this.txtKidAge3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtKidAge3.Name = "txtKidAge3";
            this.txtKidAge3.Size = new System.Drawing.Size(70, 32);
            this.txtKidAge3.TabIndex = 27;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox1.Location = new System.Drawing.Point(988, 282);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(70, 32);
            this.textBox1.TabIndex = 28;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label11.Location = new System.Drawing.Point(530, 405);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(64, 24);
            this.label11.TabIndex = 29;
            this.label11.Text = "Memo";
            // 
            // btnAddPresentaion
            // 
            this.btnAddPresentaion.Location = new System.Drawing.Point(47, 608);
            this.btnAddPresentaion.Name = "btnAddPresentaion";
            this.btnAddPresentaion.Size = new System.Drawing.Size(118, 36);
            this.btnAddPresentaion.TabIndex = 30;
            this.btnAddPresentaion.Text = "Add Presentaion";
            this.btnAddPresentaion.Click += new System.EventHandler(this.btnAddPresentaion_Click);
            // 
            // EditOpportunity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAddPresentaion);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.txtKidAge3);
            this.Controls.Add(this.txtKidAge);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtKid1Age);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.memoEdit1);
            this.Controls.Add(this.cmbReturn);
            this.Controls.Add(this.cmbDepature);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dteDepart);
            this.Controls.Add(this.dteDeparture);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDestination);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstTravelers);
            this.Controls.Add(this.txtOppName);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "EditOpportunity";
            this.Size = new System.Drawing.Size(1112, 663);
            this.Load += new System.EventHandler(this.NewOpportunity_Load);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.txtOppName, 0);
            this.Controls.SetChildIndex(this.lstTravelers, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.btnAdd, 0);
            this.Controls.SetChildIndex(this.btnRemove, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.txtDestination, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.dteDeparture, 0);
            this.Controls.SetChildIndex(this.dteDepart, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.cmbDepature, 0);
            this.Controls.SetChildIndex(this.cmbReturn, 0);
            this.Controls.SetChildIndex(this.memoEdit1, 0);
            this.Controls.SetChildIndex(this.comboBox1, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.comboBox2, 0);
            this.Controls.SetChildIndex(this.txtKid1Age, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.txtKidAge, 0);
            this.Controls.SetChildIndex(this.txtKidAge3, 0);
            this.Controls.SetChildIndex(this.textBox1, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.btnAddPresentaion, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dteDeparture.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDeparture.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDepart.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDepart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDepature.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReturn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOppName;
        private System.Windows.Forms.ListBox lstTravelers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDestination;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.DateEdit dteDeparture;
        private DevExpress.XtraEditors.DateEdit dteDepart;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.ComboBoxEdit cmbDepature;
        private DevExpress.XtraEditors.ComboBoxEdit cmbReturn;
        private DevExpress.XtraEditors.MemoEdit memoEdit1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.TextBox txtKid1Age;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtKidAge;
        private System.Windows.Forms.TextBox txtKidAge3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label11;
        private DevExpress.XtraEditors.SimpleButton btnAddPresentaion;
    }
}
