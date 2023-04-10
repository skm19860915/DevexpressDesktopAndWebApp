using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Xpo;
using DevExpress.Utils.Menu;
using BlitzerCore.Models;
using BlitzerCore.Utilities;
using BlitzerCore.Models.UI;
using BlitzerCore.Business;
using BlitzerCore.Helpers;
using BlitzerCore.UIHelpers;
using Desktop.AppLogic;
using Desktop.BaseControls;
using Desktop.DataServices;

namespace Desktop.ModuleDetail
{
    public partial class ContactDetail : ModDetailBase
    {
        UIContact Client = null;
        private List<FOP> CreditCards = new List<FOP>();

        public override string ModuleName { get { return "ContactDetail"; } }
        public ContactDetail(Form parent, UIContact customer)
            : base(parent, customer)
        {
            InitializeComponent();
            Text = customer != null ? customer.Name : ConstStrings.Get("NewCustomer");
            grdCreditCards.DataSource = new ContactBusiness(RepositoryContext.Instance).GetCreditCards(customer).ToList();
            Client = customer;
            Logger.LogTracing("ContactDetail::ctor - called");
        }

        protected override void InitData()
        {
            base.InitData();
            //InitEditors();
            lblContactName.Text = Client.Name;
            lblMobile.Text = Client.Cell;
            lblPEmail.Text = Client.PrimaryEmail;
            lblStreet.Text = ContactUIHelper.AddressLine(Client);
            lblCityState.Text = ContactUIHelper.CityStateLine(Client);
            lblDOB.Text = Client.DOB;
            rdbFemale.Checked = Client.Gender == Gender.Female;
            rdbMale.Checked = Client.Gender == Gender.Male;
            Logger.LogTracing("ContactDetail::InitData - called");
        }
    }
}
