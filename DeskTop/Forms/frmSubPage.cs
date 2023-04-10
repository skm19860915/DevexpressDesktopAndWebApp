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
    public partial class frmSubPage : Form
    {
        SubPage SubPage = null;
        public frmSubPage(SubPage aSubPage)
        {
            InitializeComponent();
            SubPage = aSubPage;
            ucWebPage1.UIPage = aSubPage;
        }

        private void frmSubPage_Load(object sender, EventArgs e)
        {
            ucWebPage1.LoadPage();
        }

        private void ucWebPage1_SaveButtonClicked(object sender, EventArgs e)
        {
            new SubPageDataAccess(RepositoryContext.Instance).Save(SubPage);
        }

        private void ucWebPage1_Load(object sender, EventArgs e)
        {

        }
    }
}
