using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Xpo;
using DevExpress.Utils.Menu;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using Desktop.BaseControls;
using Desktop.AppLogic;


namespace Desktop.ModuleDetail
{
    public partial class NewOpportunity : ModDetailBase
    {
        UIOpportunity Opportunity { get; set; }
        public NewOpportunity(Form aParent, UIOpportunity aOpp)
            : base(aParent)
        {
            InitializeComponent();
            Text = "Edit Opportunity";

            Opportunity = aOpp != null ? aOpp : new UIOpportunity();
        }

        protected override void InitData()
        {
            base.InitData();
            //txtOppName.Text = Opportunity.Name;
            //txtDestination.Text = Opportunity.
            cmbDepature.Text = Opportunity.OutBoundAirport;
            cmbReturn.Text = Opportunity.InBoundAirPort;
            dteDepart.Text = Opportunity.OutBoundDate;
            dteDeparture.Text = Opportunity.InBoundDate;
            //lstTravelers.DataSource = Opportunity.Travelers.Select(x => x.Name).ToList();
        }

        private void label3_Click(object sender, System.EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            ParentFormMain.ViewManager.CloseDetail();
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {

        }

        private void New1Opportunity_Load(object sender, System.EventArgs e)
        {

        }

        private void lstTravelers_DoubleClick(object sender, System.EventArgs e)
        {
             BlitzerMainForm.ViewController.Instance.ShowModule(new ContactEdit(ParentForm, Opportunity.Travelers[0]));

        }

        private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, System.EventArgs e)
        {

        }
    }
}
