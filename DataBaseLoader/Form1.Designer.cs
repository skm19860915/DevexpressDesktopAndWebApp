namespace DataBaseLoader
{
    partial class Form1
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
            this.mOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.ImportAirports = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mOpenFileDialog
            // 
            this.mOpenFileDialog.FileName = "mOpenFileDialog";
            this.mOpenFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.FileSelected_ClickedOk);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(22, 40);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(322, 20);
            this.txtFilePath.TabIndex = 0;
            // 
            // ImportAirports
            // 
            this.ImportAirports.Location = new System.Drawing.Point(367, 40);
            this.ImportAirports.Name = "ImportAirports";
            this.ImportAirports.Size = new System.Drawing.Size(75, 23);
            this.ImportAirports.TabIndex = 1;
            this.ImportAirports.Text = "Import";
            this.ImportAirports.UseVisualStyleBackColor = true;
            this.ImportAirports.Click += new System.EventHandler(this.ImportAirports_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(150, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "List of Airports";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkBlue;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ImportAirports);
            this.Controls.Add(this.txtFilePath);
            this.Name = "Form1";
            this.Text = "Blitizer DB Utility";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog mOpenFileDialog;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button ImportAirports;
        private System.Windows.Forms.Label label1;
    }
}

