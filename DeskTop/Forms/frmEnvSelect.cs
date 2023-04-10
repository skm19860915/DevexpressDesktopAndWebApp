using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Desktop.DataServices;
using BlitzerCore.Models;
using BlitzerCore.DataAccess;

namespace Desktop
{
    public partial class frmEnvSelect : Form
    {
        public Desktop.DataServices.Environment Environment
        {
            get
            {
                if (rdbProd.Checked)
                    return Desktop.DataServices.Environment.PROD;
                else if (rdbBeta.Checked)
                    return Desktop.DataServices.Environment.BETA;
                else
                    return Desktop.DataServices.Environment.DEV;
            }
        }
        public frmEnvSelect()
        {
            InitializeComponent();
        }

        private void frmEnvSelect_Load(object sender, EventArgs e)
        {
            lblAppVer.Text = Desktop.BlitzerDesktop.Label;
            lblDbVersion.Text = new BlitzerDataAccess(RepositoryContext.Instance).GetLatestDBVersion().Label;
        }

        private void rdbProd_Click(object sender, EventArgs e)
        {

            DialogResult = DialogResult.OK;
            Close();
        }

        private void rdbBeta_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();

        }

        private void rdbProd_CheckedChanged(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void rdbDev_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }

}
