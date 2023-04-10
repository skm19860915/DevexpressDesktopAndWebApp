using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public enum BookingStatus {
        [Display(Name = "Awaiting Deposit")]
        AwaitingDeposit,
        [Display(Name = "Balance Outstanding")]
        BalanceOutstanding,
        [Display(Name = "Paid In Full")]
        PaidInFull,
        [Display(Name = "Pending Cancellation")]
        PendingCancellation,
        [Display(Name = "Cancelled")]
        Cancelled,
        [Display(Name = "Deleted")]
        Deleted
    }
    public class Booking
    {
        public Booking()
        {
            Payments = new List<Payment>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingID { get; set; }
        public string BookingNumber { get; set; }
        public int? TourOperatorID { get; set; }
        [ForeignKey("TourOperatorID")]
        public virtual TourOperator TourOperator { get; set; }
        public int? SupplierId { get; set; }
        [ForeignKey("SupplierId")]
        public virtual Company Supplier { get; set; }
        public int? CruiseLineId { get; set; }
        [ForeignKey("CruiseLineId")]
        public virtual CruiseLine Cruise { get; set; }
        public DateTime? FinalPayment { get; set; }
        public int TripID { get; set; }
        [ForeignKey("TripID")]
        public virtual Trip Trip { get; set; }
        public virtual List<Credit> Credits { get; set; }  // This is used to burn down the credit given to the user
        public BookingStatus Status { get; set; }
        public double Deposit { get; set; }
        public DateTime? DepositDueDate { get; set; }
        public double Amount { get; set; }
        public double Received { get; set; } // The amount we received from the Tour Operator for the booking
        public double HQCommission { get; set; }
        public double HostAgencyCommission { get; set; }
        public double ICCommission { get; set; }
        public double GrossCommission { get; set; }
        public DateTime? DepositDue { get; set; }    
        public DateTime? TargetPayment { get; set; }
        public DateTime? PaymentDate { get; set; }               // When the comission payment will be made
        public int SettlementAge { get; set; }
        public virtual List<Payment> Payments { get; set; }
        public string Memo { get; set; }
        public string PaymentMemo { get; set; }
        public string CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        public Contact CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedById { get; set; }
        [ForeignKey("UpdatedById")]
        public Contact UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }
    }
}
