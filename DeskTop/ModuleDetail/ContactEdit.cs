using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;
using BlitzerCore.Utilities;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.DataAccess;
using BlitzerCore.Helpers;
using Desktop.AppLogic;
using Desktop.BaseControls;
using Desktop.DataServices;

namespace Desktop.ModuleDetail
{
    public partial class ContactEdit : ModDetailBase
    {
        UIContact Contact { get; set; }
        public override string ModuleName { get { return "ContactEdit"; } }

        public ContactEdit(Form aParent, UIContact aUser) : base(aParent)
        {
            InitializeComponent();
            Text = "Edit Contact";

            Contact = aUser;
        }

        protected override void InitData()
        {
            base.InitData();

            if (Contact == null )
            {
                Logger.LogWarning("ContactEdit::InitData - Can't load data because contact was null");
                return;
            } else if (Contact.Id == null)
            {
                Logger.LogWarning("ContactEdit::InitData - Can't load Contact was key is null for " + Contact.Name);
                return;
            }


            Contact lUser = new ContactDataAccess(RepositoryContext.Instance).Get(Contact.Id);
            if ( lUser == null )
            {
                Logger.LogWarning("ContactEdit::InitData - Can't load data because AppUser was null for ID = " + Contact.Id);
                return;
            }

            txtTitle.Text = lUser.Title;
            txtFirstName.Text = lUser.First;
            txtMiddle.Text = lUser.Middle;
            txtLastName.Text = lUser.Last;
            txtSuffix.Text = lUser.Suffix;
            txtStreet.Text = lUser.Address1;
            txtApp_Suite.Text = lUser.Address2;
            txtCity.Text = lUser.City;
            txtState.Text = lUser.State;
            txtZip.Text = lUser.ZipCode;
            txtDOB.Text = DataHelper.GetDateString(lUser.DOB);
            rdbFemale.Checked = lUser.Gender == Gender.Female;
            rdbMale.Checked = lUser.Gender == Gender.Male;
        }

        private void label3_Click(object sender, System.EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            ParentFormMain.ViewManager.CloseDetail();
        }

        private void SaveContact()
        {
            var lUserDA = new ContactDataAccess(RepositoryContext.Instance);
            Contact lUser = lUserDA.Get(Contact.Id);
            lUser.Title = txtTitle.Text;
            lUser.First = txtFirstName.Text;
            lUser.Middle = txtMiddle.Text;
            lUser.Last = txtLastName.Text;
            lUser.Suffix = txtSuffix.Text;
            lUser.Address1 = txtStreet.Text;
            lUser.Address2 = txtApp_Suite.Text;
            lUser.City = txtCity.Text;
            lUser.State = txtState.Text;
            lUser.ZipCode = txtZip.Text;
            if (rdbFemale.Checked)
                lUser.Gender = Gender.Female;
            else
                lUser.Gender = Gender.Male;
            lUser.DOB = DataHelper.GetDate(txtDOB.Text);
            lUserDA.Save(lUser);
        }

        public override void Save() {
            Logger.LogTracing("ContactEdit::Save");
            SaveContact();
        }

        private void rdbMale_CheckedChanged(object sender, EventArgs e)
        {
            rdbFemale.Checked = !rdbMale.Checked;
        }

        private void rdbFemale_CheckedChanged(object sender, EventArgs e)
        {
            rdbMale.Checked = !rdbFemale.Checked;
        }

        private void xtraTabPage1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
