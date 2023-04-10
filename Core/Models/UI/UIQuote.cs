using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlitzerCore.Models.UI
{
    [NotMapped]
    public class UIQuote : BaseUI
    {
        public UIQuote()
        {
        }
        public int QuoteID { get; set; }
        public UIContact Primary { get; set; }
        public int QuoteRequestID { get; set; }
        public UIQuoteRequest QuoteRequest { get; set; }
        public string QuoteType { get; set; }
        public string ResortName { get; set; }
        public string Sent { get; set; }
        public bool Booked { get; set; }
        public string Viewed { get; set; }
        public int? SupplierId { get; set; }
        public double? SupplerRank { get; set; }
        public string SupplierName { get; set; }
        public UICompany Supplier { get; set; }
        [Required]
        public int? SKUID { get; set; }
        public int SortOrder { get; set; }
        public int? TourOperatorID { get; set; }
        [DisplayName("Tour Operator")]
        public string TourOperator { get; set; }
        [DisplayName("Package Price")]
        public string PackagePrice { get; set; }
        [DisplayName("Resort Price")]
        public string ResortPrice { get; set; }
        [DisplayName("Flight Price")]
        public string FlightPrice { get; set; }
        [DisplayName("Fee")]
        public string Adjustment { get; set; }
        public double Total { get; set; }
        public string SKU { get; set; }
        public QuoteStatus Status { get; set; }
        public string StatusDisplay 
        { get
            {
                if (Status == QuoteStatus.NotReady)
                    return "Not Ready";
                else
                    return Status.ToString();
            }
        }
        public int Out_Leg1_Id { get; set; }
        public string Out_Leg1_DepartTime { get; set; }
        public string Out_Leg1_ArriveTime { get; set; }
        public string Out_Leg1_Flight { get; set; }
        public string Out_ConnectionAirport { get; set; }
        public int Out_Leg2_Id { get; set; }
        public string Out_Leg2_DepartTime { get; set; }
        public string Out_Leg2_ArriveTime { get; set; }
        public string Out_Leg2_Flight { get; set; }

        public int In_Leg1_Id { get; set; }
        public string In_Leg1_DepartTime { get; set; }
        public string In_Leg1_ArriveTime { get; set; }
        public string In_Leg1_Flight { get; set; }
        public string In_ConnectionAirport { get; set; }
        public int In_Leg2_Id { get; set; }
        public string In_Leg2_DepartTime { get; set; }
        public string In_Leg2_ArriveTime { get; set; }
        public string In_Leg2_Flight { get; set; }
        public Quote.TransferTypes? Transfer { get; set; }
        public Quote.InsuranceTypes? Insurance { get; set; }
    }
}

