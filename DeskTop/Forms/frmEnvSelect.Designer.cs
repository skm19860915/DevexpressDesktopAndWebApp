
namespace Desktop
{
    partial class frmEnvSelect
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
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.rdbProd = new System.Windows.Forms.RadioButton();
            this.rdbBeta = new System.Windows.Forms.RadioButton();
            this.rdbDev = new System.Windows.Forms.RadioButton();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblAppVer = new System.Windows.Forms.Label();
            this.lblDbVersion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Environment";
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(13, 57);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Size = new System.Drawing.Size(142, 170);
            this.radioGroup1.TabIndex = 1;
            // 
            // rdbProd
            // 
            this.rdbProd.AutoSize = true;
            this.rdbProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbProd.Location = new System.Drawing.Point(26, 90);
            this.rdbProd.Name = "rdbProd";
            this.rdbProd.Size = new System.Drawing.Size(103, 24);
            this.rdbProd.TabIndex = 2;
            this.rdbProd.Text = "Production";
            this.rdbProd.UseVisualStyleBackColor = true;
            this.rdbProd.CheckedChanged += new System.EventHandler(this.rdbProd_CheckedChanged);
            this.rdbProd.Click += new System.EventHandler(this.rdbProd_Click);
            // 
            // rdbBeta
            // 
            this.rdbBeta.AutoSize = true;
            this.rdbBeta.Checked = true;
            this.rdbBeta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbBeta.Location = new System.Drawing.Point(26, 136);
            this.rdbBeta.Name = "rdbBeta";
            this.rdbBeta.Size = new System.Drawing.Size(61, 24);
            this.rdbBeta.TabIndex = 3;
            this.rdbBeta.TabStop = true;
            this.rdbBeta.Text = "Beta";
            this.rdbBeta.UseVisualStyleBackColor = true;
            this.rdbBeta.Click += new System.EventHandler(this.rdbBeta_Click);
            // 
            // rdbDev
            // 
            this.rdbDev.AutoSize = true;
            this.rdbDev.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbDev.Location = new System.Drawing.Point(26, 182);
            this.rdbDev.Name = "rdbDev";
            this.rdbDev.Size = new System.Drawing.Size(121, 24);
            this.rdbDev.TabIndex = 4;
            this.rdbDev.Text = "Development";
            this.rdbDev.UseVisualStyleBackColor = true;
            this.rdbDev.Click += new System.EventHandler(this.rdbDev_Click);
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Location = new System.Drawing.Point(45, 294);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 39);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // lblAppVer
            // 
            this.lblAppVer.AutoSize = true;
            this.lblAppVer.Location = new System.Drawing.Point(27, 239);
            this.lblAppVer.Name = "lblAppVer";
            this.lblAppVer.Size = new System.Drawing.Size(66, 13);
            this.lblAppVer.TabIndex = 6;
            this.lblAppVer.Text = "Version : 1.0";
            // 
            // lblDbVersion
            // 
            this.lblDbVersion.AutoSize = true;
            this.lblDbVersion.Location = new System.Drawing.Point(27, 265);
            this.lblDbVersion.Name = "lblDbVersion";
            this.lblDbVersion.Size = new System.Drawing.Size(66, 13);
            this.lblDbVersion.TabIndex = 8;
            this.lblDbVersion.Text = "Version : 1.0";
            // 
            // frmEnvSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(182, 345);
            this.ControlBox = false;
            this.Controls.Add(this.lblDbVersion);
            this.Controls.Add(this.lblAppVer);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.rdbDev);
            this.Controls.Add(this.rdbBeta);
            this.Controls.Add(this.rdbProd);
            this.Controls.Add(this.radioGroup1);
            this.Controls.Add(this.label1);
            this.Name = "frmEnvSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Environment";
            this.Load += new System.EventHandler(this.frmEnvSelect_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private System.Windows.Forms.RadioButton rdbProd;
        private System.Windows.Forms.RadioButton rdbBeta;
        private System.Windows.Forms.RadioButton rdbDev;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblAppVer;
        private System.Windows.Forms.Label lblDbVersion;
    }
}