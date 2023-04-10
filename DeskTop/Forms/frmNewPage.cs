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
using BlitzerCore.Business;
using BlitzerCore.Utilities;
using Desktop.DataServices;

namespace Desktop
{
    public partial class frmNewPage : Form
    {
        public WebPageType PageType
        {
            get
            {
                if (rdbSubPage.Checked == true)
                    return WebPageType.SubPage;
                else if (rdbCountry.Checked == true)
                        return WebPageType.Country;
                else if (rdbResort.Checked == true)
                    return WebPageType.Resort;

                return WebPageType.SubPage;
            }            
        }

        public string Title { get { return txtTitle.Text; } }
        public string Header { get { return txtHeader.Text; } }

        public frmNewPage()
        {
            InitializeComponent();
        }

        private void frmNewPage_Load(object sender, EventArgs e)
        {

        }

        public Page Save()
        {
            return new WebPageBusiness(RepositoryContext.Instance).CreatePage(PageType, Title, Header);
        }
    }
}
