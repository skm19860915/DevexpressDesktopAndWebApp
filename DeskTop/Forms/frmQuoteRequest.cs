using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlitzerCore.Business;
using BlitzerCore.DataAccess;
//using BlitzerCore.Services;
using BlitzerCore.Models;
using Desktop.DataServices;
using BlitzerCore.Utilities;

namespace Desktop
{
    public partial class frmQuoteRequest : Form
    {
        public frmQuoteRequest()
        {
            InitializeComponent();
        }

        // Create QuoteRequest 
        //    * Depart City, Arrive City, From Date, To Date
        //    * Hotel Filter, Carrier Filter

        private void Submit_Click(object sender, EventArgs e)
        {
            try
            {
                //string lUserID = "";
                var lRequestUI = new BlitzerCore.Models.UI.UIQuoteRequest();
                lRequestUI.DepartureCityCode = cmbDepature.Text;
                lRequestUI.DestinationCityCode = cmbDestination.Text;
                lRequestUI.StartDate = dtpDepart.Text;
                lRequestUI.EndDate = dtpReturn.Text;



                using (RepositoryContext lContext = RepositoryContext.Instance)
                {
                    QuoteBusiness lQBiz = new QuoteBusiness(lContext, null);
                    //var lQuoteRequest = lQBiz.SaveRequest(lUserID, lRequestUI);
                    //lQBiz.Execute(lQuoteRequest);
                    //populateFlightList(lFlights);
                }
            } catch ( Exception e1)
            {
                Logger.LogException("Failed to process quote request", e1);
            }
        }

        private void populateFlightList(IEnumerable<Flight> aFlights)
        {
            //foreach (var lFlight in aFlights)
            //{
            //    string[] row = {lFlight.Departure.Code, lFlight.Arrival.Code, lFlight.Depart.ToShortDateString(), lFlight.Arrive.ToShortDateString(), "$3498.99" };
            //    var listViewItem = new ListViewItem(row);
            //    lstFlights.Items.Add(listViewItem);
            //}
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void QuoteRequest_Load(object sender, EventArgs e)
        {

            using (var lContext = RepositoryContext.Instance)
            {
                var lAirPrtDA = new AirPortDataAccess(lContext);
                cmbDepature.Items.AddRange( lAirPrtDA.GetAll().ToArray());
                cmbDepature.DisplayMember = "Code";
                cmbDepature.ValueMember = "AirPortID";

                cmbDestination.Items.AddRange(lAirPrtDA.GetAll().ToArray());
                cmbDestination.DisplayMember = "Code";
                cmbDestination.ValueMember = "AirPortID";
            }

            lstFlights.Columns.Add("Departure City");
            lstFlights.Columns.Add("Arrival City");
            lstFlights.Columns.Add("Depature Time");
            lstFlights.Columns.Add("Arrival Time");
            lstFlights.Columns.Add("Price");
        }
    }
}
