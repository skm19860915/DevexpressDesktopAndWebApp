using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models.UI
{
    public class UIBooking : BaseUI
    {
        public UIBooking()
        {
            Payments = new List<UIPayment>();
        }
        public int BookingID { get; set; }
        public string BookingNumber { get; set; }
        public string Agent { get; set; }
        public string Supplier { get; set; }
        public int? SupplierId { get; set; }
        public string Amount { get; set; }
        public string GrossCommission { get; set; }
        public string HQCommission { get; set; }
        public string ICCommission { get; set; }
        public string HostAgencyCommission { get; set; }
        public string Received { get; set; } // The amount we received from the Tour Operator for the booking
        public int TripID { get; set; }
        public string TripName { get; set; }
        public UITrip Trip { get; set; }
        public BookingStatus Status { get; set; }
        public string EndDate { get; set; }
        public string Paid { get; set; }
        public DateTime? TargetPayment { get; set; }
        public int SettlementAge { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? DepositDueDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedOn { get; set; }
        public string Deposit { get; set; }
        public string Balance { get; set; }
        public int? TourOperatorID { get; set; }
        public string TourOperatorName{ get; set; }
        public List<UIPayment> Payments {get; set;}
        public List<UICredit> Credits { get; set; }
        [DataType(DataType.Date)]
        public DateTime? FinalPayment { get; set; }
        public string FinalPaymentStr { get; set; }
        public String Memo { get; set; }
        public string PaymentMemo { get; set; }
        public int DaysWaiting { get; set; }
    }
}
