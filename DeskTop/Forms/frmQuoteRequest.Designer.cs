namespace Desktop
{
    partial class frmQuoteRequest
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDepature = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbDestination = new System.Windows.Forms.ComboBox();
            this.lblDepart = new System.Windows.Forms.Label();
            this.dtpDepart = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpReturn = new System.Windows.Forms.DateTimePicker();
            this.lstFlights = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(434, 442);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(159, 55);
            this.button1.TabIndex = 0;
            this.button1.Text = "Request";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Submit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Depature City";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // cmbDepature
            // 
            this.cmbDepature.FormattingEnabled = true;
            this.cmbDepature.Location = new System.Drawing.Point(207, 84);
            this.cmbDepature.Name = "cmbDepature";
            this.cmbDepature.Size = new System.Drawing.Size(121, 33);
            this.cmbDepature.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Destination";
            // 
            // cmbDestination
            // 
            this.cmbDestination.FormattingEnabled = true;
            this.cmbDestination.Location = new System.Drawing.Point(207, 135);
            this.cmbDestination.Name = "cmbDestination";
            this.cmbDestination.Size = new System.Drawing.Size(121, 33);
            this.cmbDestination.TabIndex = 4;
            // 
            // lblDepart
            // 
            this.lblDepart.AutoSize = true;
            this.lblDepart.Location = new System.Drawing.Point(413, 84);
            this.lblDepart.Name = "lblDepart";
            this.lblDepart.Size = new System.Drawing.Size(76, 25);
            this.lblDepart.TabIndex = 5;
            this.lblDepart.Text = "Depart";
            // 
            // dtpDepart
            // 
            this.dtpDepart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDepart.Location = new System.Drawing.Point(523, 80);
            this.dtpDepart.Name = "dtpDepart";
            this.dtpDepart.Size = new System.Drawing.Size(200, 31);
            this.dtpDepart.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(419, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 25);
            this.label3.TabIndex = 7;
            this.label3.Text = "Return";
            // 
            // dtpReturn
            // 
            this.dtpReturn.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpReturn.Location = new System.Drawing.Point(523, 143);
            this.dtpReturn.Name = "dtpReturn";
            this.dtpReturn.Size = new System.Drawing.Size(200, 31);
            this.dtpReturn.TabIndex = 8;
            // 
            // lstFlights
            // 
            this.lstFlights.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstFlights.GridLines = true;
            this.lstFlights.HideSelection = false;
            this.lstFlights.Location = new System.Drawing.Point(43, 237);
            this.lstFlights.Name = "lstFlights";
            this.lstFlights.Size = new System.Drawing.Size(922, 151);
            this.lstFlights.TabIndex = 9;
            this.lstFlights.UseCompatibleStateImageBehavior = false;
            this.lstFlights.View = System.Windows.Forms.View.Details;
            // 
            // QuoteRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 549);
            this.Controls.Add(this.lstFlights);
            this.Controls.Add(this.dtpReturn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpDepart);
            this.Controls.Add(this.lblDepart);
            this.Controls.Add(this.cmbDestination);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbDepature);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "QuoteRequest";
            this.Text = "QuoteRequest";
            this.Load += new System.EventHandler(this.QuoteRequest_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDepature;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbDestination;
        private System.Windows.Forms.Label lblDepart;
        private System.Windows.Forms.DateTimePicker dtpDepart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpReturn;
        private System.Windows.Forms.ListView lstFlights;
    }
}