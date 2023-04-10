using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;

namespace BlitzerCore.Models.UI
{
    public enum FinalPaymentStatus {Good, Warning, Critical, Paid};

    public class UITrip : UIOpportunity
    {
        public UITrip()
        {
            Travelers = new List<UIContact>();
            Bookings = new List<UIBooking>();
        }
        public int InBoundAirPortID { get; set; }
        public int OutboundAirPortID { get; set; }
        public int StageID { get; set; }
        public BlitzerCore.Models.TripStage TripStage { get; set; }
        public virtual List<UIBooking> Bookings { get; set; }
        public int DaysToStart { get; set; }
        public UIContact Agent { get; set; }
        public string Total { get; set; }
        public string Balance { get; set; }
        public FinalPaymentStatus FinalPaymentStatus { get; set; }
        public string GrossCommission { get; set; }
        public string ICCommission { get; set; }
        public string FinalPayment { get; set; }
        public BlitzerCore.Models.Trip.Statuses Status { get; set; }
        public string TripStageStr { get; set; }
        public string TripStatusStr { get; set; }
        public Trip.Statuses TripStatus { get; set; }
        public string CreditMemo { get; set; }
        public bool HasTransfer { get; set; }
    }
}
