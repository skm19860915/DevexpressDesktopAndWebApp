using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop
{
    public partial class frmImageUploader : Form
    {
        public frmImageUploader()
        {
            InitializeComponent();
        }

        private void lblFileName_Click(object sender, EventArgs e)
        {
            var lResult = dlgOpenFile.ShowDialog();
            if ( lResult.Equals ( DialogResult.OK))
            {
                txtFileName.Text = dlgOpenFile.FileName;
            }
        }

        private void xtraFolderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }
    }
}
