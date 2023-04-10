using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlitzerCore.Models.UI;
using BlitzerCore.DataAccess;
using Desktop.DataServices;
using BlitzerCore.Business;

namespace Desktop
{
    public partial class frmPageSelect : Form
    {
        public int PageId { get; set; }
        public frmPageSelect()
        {
            InitializeComponent();
            LoadPage();
        }

        void LoadPage()
        {
            grdCntrl.DataSource = new PageDataAccess(RepositoryContext.Instance).GetAll();
        }

        private void grdCntrl_Click(object sender, EventArgs e)
        {
            SelectedPage();
        }

        private void SelectedPage()
        {
            Page lPage = null;

            foreach (int i in grdView_Pages.GetSelectedRows())
            {
                lPage = grdView_Pages.GetRow(i) as Page;
            }

            PageId = lPage.Id;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void grdCntrl_DoubleClick(object sender, EventArgs e)
        {
            SelectedPage();
        }

        private void btnNewPage_Click(object sender, EventArgs e)
        {
            using ( var lForm = new frmNewPage() )
            {
                if (lForm.ShowDialog() == DialogResult.OK )
                {
                    lForm.Save();
                    LoadPage();
                }
            }
        }

        private void pageBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}
