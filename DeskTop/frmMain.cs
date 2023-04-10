using DevExpress.XtraBars;
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
using BlitzerCore.DataAccess;
using Desktop.DataServices;
using BlitzerCore.Business;
using BlitzerCore.Utilities;
using Desktop.AppLogic;
using DevExpress.XtraNavBar;

namespace Desktop
{
    public partial class BlitzerMainForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public BlitzerMainForm()
        {
            InitializeComponent();
            ViewManager = ViewController.Instance;
            ViewController.Instance.Parent = this;

            nbMain.SelectedLinkChanged += new DevExpress.XtraNavBar.ViewInfo.NavBarSelectedLinkChangedEventHandler(nbMain_SelectedLinkChanged);
            //nbMain.LinkClicked += new DevExpress.XtraNavBar.ViewInfo.navbar
            //nbMain.CustomDrawGroupCaption += new DevExpress.XtraNavBar.ViewInfo.CustomDrawNavBarElementEventHandler(nbMain_CustomDrawGroupCaption);
            nbMain.LinkClicked += new NavBarLinkEventHandler(nbMain_LinkClicked);

        }

        private void BlitzerMainForm_Load(object sender, EventArgs e)
        {
            ViewManager.Register();
            ShowModule(ModulesInfo.GetItem(0).Caption);
        }

        private void barResorts_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void navBarControl1_Click(object sender, EventArgs e)
        {

        }

        private void grdPages_DoubleClick(object sender, EventArgs e)
        {

        }

        private void grdPages_Click(object sender, EventArgs e)
        {

        }

        private void grdMain_Click(object sender, EventArgs e)
        {

        }

        private void grdMain_DoubleClick(object sender, EventArgs e)
        {
            Page lPage = null;

            foreach (int i in grdPagesGridView.GetSelectedRows())
            {
                lPage = grdPagesGridView.GetRow(i) as Page;
            }

            if (lPage == null)
                return;

            if (lPage.PageTypeId == 1)
            {
                UIResortPage lResortPage = new ResortPageBusiness(RepositoryContext.Instance).Get(lPage.Id) as UIResortPage;

                using (var lForm = new frmResortPage(lResortPage))
                {
                    Hide();
                    if (lForm.ShowDialog() == DialogResult.OK)
                    {
                        //lForm.Save();
                    }
                    Show();
                }
            }
            else if (lPage.PageTypeId == 2)
            {
                using (var lForm = new CountryPage(lPage.Id))
                {
                    Hide();
                    if (lForm.ShowDialog() == DialogResult.OK)
                    {
                        lForm.Save();
                    }
                    Show();
                }
            }
            else if (lPage.PageTypeId == 3)
            {
                UIRanking lRankingPage = new RankingPageDataAccess(RepositoryContext.Instance).Get(lPage.Id);

                using (var lForm = new frmRankingPage(lRankingPage))
                {
                    Hide();
                    if (lForm.ShowDialog() == DialogResult.OK)
                    {

                    }
                    Show();
                }
            }
            else if (lPage.PageTypeId == 4)
            {
                SubPage lSubPage = new SubPageDataAccess(RepositoryContext.Instance).Get(lPage.Id);

                using (var lForm = new frmSubPage(lSubPage))
                {
                    Hide();
                    if (lForm.ShowDialog() == DialogResult.OK)
                    {
                    }
                    Show();
                }
            }
        }

        private void nvbCountries_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.pageBindingSource.DataSource = new CountryDataAccess(RepositoryContext.Instance).GetAll();
        }

        private void navBarResorts_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.pageBindingSource.DataSource = new PageDataAccess(RepositoryContext.Instance).GetAll()
                .Where(x => x.PageTypeId == 1).OrderBy(x => x.Title);
        }

        private void nvbCountryPages_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.pageBindingSource.DataSource = new PageDataAccess(RepositoryContext.Instance).GetAll()
                .Where(x => x.PageTypeId == 2).OrderBy(x => x.Title);
        }

        private void nvbRankingPages_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.pageBindingSource.DataSource = new PageDataAccess(RepositoryContext.Instance).GetAll()
                .Where(x => x.PageTypeId == 3).OrderBy(x => x.Title);
        }

        private void nvbSubPages_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.pageBindingSource.DataSource = new PageDataAccess(RepositoryContext.Instance).GetAll()
                .Where(x => x.PageTypeId == 4).OrderBy(x => x.Title);
        }

        private void brbNewPage_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var lForm = new frmNewPage())
            {
                if (lForm.ShowDialog() == DialogResult.OK)
                {
                    lForm.Save();
                    ReLoadData();
                }
            }
        }

        void ReLoadData()
        {
            this.pageBindingSource.DataSource = new PageDataAccess(RepositoryContext.Instance).GetAll()
                .Where(x => x.PageTypeId == 1).OrderBy(x => x.Title);

            this.pageBindingSource.DataSource = new PageDataAccess(RepositoryContext.Instance).GetAll()
                .Where(x => x.PageTypeId == 2).OrderBy(x => x.Title);

            this.pageBindingSource.DataSource = new PageDataAccess(RepositoryContext.Instance).GetAll()
                .Where(x => x.PageTypeId == 3).OrderBy(x => x.Title);

            this.pageBindingSource.DataSource = new PageDataAccess(RepositoryContext.Instance).GetAll()
                .Where(x => x.PageTypeId == 4).OrderBy(x => x.Title);
        }

        private void nbMain_SelectedLinkChanged(object sender, DevExpress.XtraNavBar.ViewInfo.NavBarSelectedLinkChangedEventArgs e)
        {
            string FuncName = $"frmMain::SelectedLinkChanged - {e.Link.Caption}";
            Logger.EnterFunction(FuncName);
            ShowModule(e.Link.Caption);
            Logger.LeaveFunction(FuncName);
        }
        void nbMain_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            string FuncName = $"frmMain::LinkChanged - {e.Link.Caption}";
            Logger.EnterFunction(FuncName);
            if (e.Link == null) 
                return;
            ShowModule(e.Link.Caption);
            Logger.LeaveFunction(FuncName);
        }

        private void ShowModule(string caption)
        {
            string FuncName = $"frmMain::ShowModule - {caption}";
            try
            {
                Logger.EnterFunction(FuncName);
                pnlControl.Parent.SuspendLayout();
                pnlControl.SuspendLayout();
                ViewManager.ShowModule(caption);
            }
            finally
            {
                pnlControl.ResumeLayout();
                pnlControl.Parent.ResumeLayout();
                Logger.LeaveFunction(FuncName);
            }
        }

        private void bbiNewOpp_ItemClick(object sender, ItemClickEventArgs e)
        {
            ViewManager.Add();
        }

        private void bbiEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            const string FuncName = "frmMain::Edit Ribbon Element clicked";
            Logger.EnterFunction(FuncName);
            ViewManager.Edit();
            Logger.LeaveFunction(FuncName);
        }

        private void bbiSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            const string FuncName = "frmMain::Save Ribbon Element clicked";
            Logger.EnterFunction(FuncName);
            ViewManager.Save();
            Logger.LeaveFunction(FuncName);
        }

        private void bbiClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            const string FuncName = "frmMain::CloseClick";
            Logger.EnterFunction(FuncName);                
            ViewManager.Close();
            Logger.LeaveFunction(FuncName);
        }

        private void bbiSaveClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            const string FuncName = "frmMain::SaveClose Ribbon Element clicked";
            Logger.EnterFunction(FuncName);
            ViewManager.Save();
            ViewManager.Close();
            Logger.LeaveFunction(FuncName);
        }

        private void bbiFind_ItemClick(object sender, ItemClickEventArgs e)
        {
            const string FuncName = "frmMain::Find Ribbon Element clicked";
            Logger.EnterFunction(FuncName);
            ViewManager.Find();
            Logger.LeaveFunction(FuncName);
        }

        private void bbiNewOpp2_ItemClick(object sender, ItemClickEventArgs e)
        {
            const string FuncName = "frmMain::New Opportunity Ribbon Element clicked";
            Logger.EnterFunction(FuncName);
            ViewManager.NewOpportunity();
            Logger.LeaveFunction(FuncName);
        }

        private void bbiTool_ItemClick(object sender, ItemClickEventArgs e)
        {
            var lContext = RepositoryContext.Instance;
            foreach (var lContact in lContext.Contacts)
            {
                lContact.Emails.Add(new Email() { Address = lContact.Id, EmailTypeID = 1, Preferred = true, UserId = lContact.Id });
                lContext.Update(lContact);
            }

            lContext.SaveChanges();
        }
    }
}