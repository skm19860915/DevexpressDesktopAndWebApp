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
    public partial class NewTrip : ModDetailBase
    {
        UITrip Trip { get; set; }
        public NewTrip(Form aParent, UITrip aOpp)
            : base(aParent)
        {
            InitializeComponent();
            Text = "New Trip";

            Trip = aOpp != null ? aOpp : new UITrip();
        }

        protected override void InitData()
        {
            base.InitData();
            //txtTripName.Text = Trip.Name;
            ////txtDestination.Text = Trip.
            //cmbDepature.Text = Trip.OutBoundAirport;
            //cmbReturn.Text = Trip.InBoundAirPort;
            //dteDepart.Text = Trip.OutBoundDate;
            //dteDeparture.Text = Trip.InBoundDate;
            //lstTravelers.DataSource = Trip.Travelers.Select(x => x.Name).ToList();
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

        private void NewTrip_Load(object sender, System.EventArgs e)
        {

        }

        private void lstTravelers_DoubleClick(object sender, System.EventArgs e)
        {
             BlitzerMainForm.ViewController.Instance.ShowModule(new ContactEdit(ParentForm, Trip.Travelers[0]));

        }

        private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }
    }
}
