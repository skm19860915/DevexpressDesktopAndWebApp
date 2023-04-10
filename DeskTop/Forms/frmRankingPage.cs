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
    public partial class frmRankingPage : Form
    {
        UIRanking RankingPage = null;

        public frmRankingPage(UIRanking aRankingPage)
        {
            InitializeComponent();
            RankingPage = aRankingPage;
            ucWebPage1.UIPage = RankingPage;
            ucWebPage1.LoadPage();
        }

        private void frmRankingPage_Load(object sender, EventArgs e)
        {
            ucWebPage1.UIPage = RankingPage;
        }

        private void ucWebPage1_Load(object sender, EventArgs e)
        {

        }

        private void ucWebPage1_SaveButtonClicked(object sender, EventArgs e)
        {
            new RankingPageDataAccess(RepositoryContext.Instance).Save(RankingPage);
        }
    }
}
