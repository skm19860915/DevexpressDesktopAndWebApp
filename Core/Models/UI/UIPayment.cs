using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models.UI
{
    public class UIPayment : BaseUI
    {
        public int PaymentId { get; set; }
        public int BookingId { get; set; }
        public string PayeeId { get; set; }
        public string Payee { get; set; }
        public string PaymentType { get; set; }
        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; }
        public PaymentStatus Status { get; set; }
        public string PaymentDateStr { get; set; }
        public string Amount { get; set; }
        public string CreditCard { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string Expiration { get; set; }
        public string Confirmation { get; set; }
        public string Memo { get; set; }
        public List<UIFOP> Cards { get; set; }
        public int? CardID { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedOn { get; set; }

    }
}
