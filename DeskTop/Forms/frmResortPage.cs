using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using BlitzerCore.DataAccess;
using Desktop.DataServices;

namespace Desktop
{
    public partial class frmResortPage : Form
    {
        UIResortPage ResortPage = null;

        public frmResortPage(UIResortPage aResortPage)
        {
            InitializeComponent();
            ResortPage = aResortPage;
            ucWebPage1.UIPage = aResortPage;
        }

        private void ucWebPage1_Load(object sender, EventArgs e)
        {

        }

        private void frmResortPage_Load(object sender, EventArgs e)
        {
            ucWebPage1.LoadPage();
        }

        private void frmResortPage_ResizeEnd(object sender, EventArgs e)
        {

        }

        private void ucWebPage1_SaveButtonClicked(object sender, EventArgs e)
        {
            new ResortPageDataAccess(RepositoryContext.Instance).Save(ResortPage);
        }

        private void btnTiles_Click(object sender, EventArgs e)
        {
            if (ResortPage.RightPanel == null)
                ResortPage.RightPanel = new BlitzerCore.Models.UI.Panel();

            using ( var lForm = new frmPanel(ResortPage.RightPanel, ResortPage.Id))
            {
                lForm.Location = new Point(this.Location.X + this.Width + 20, this.Location.Y);

                if ( lForm.ShowDialog() == DialogResult.OK )
                {

                }
            }
        }
    }
}
