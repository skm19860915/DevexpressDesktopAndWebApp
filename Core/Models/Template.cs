using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class Template
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentID { get; set; }
        public DateTime PaymentDate { get; set; }

        public double Amount { get; set; }

        public int BookingID { get; set; }
        [ForeignKey("BookingID")]
        public virtual Booking Booking {get; set; }
        public string PayeeID { get; set; }
        [ForeignKey("PayeeID")]
        public virtual Client Payee { get; set; }
        public int? FOPId { get; set; }
        [ForeignKey("FOPId")]
        public virtual FOP Card { get; set; }
        public string Confirmation { get; set; }
        public string Memo { get; set; }
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
