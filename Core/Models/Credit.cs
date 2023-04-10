using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;

namespace BlitzerCore.Models
{
    public enum CreditTypes { Voucher, Vendor}
    public class Credit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Reference { get; set; }
        public double Amount { get; set; }
        public DateTime When { get; set; }
        public string Note { get; set; }
        public string ContactId { get; set; }
        [ForeignKey("ContactId")]
        public Contact Traveler { get; set; }
        public int OriginalBookingId { get; set; }
        [ForeignKey("OriginalBookingId")]
        public virtual Booking OriginalBooking { get; set; }
    }
}
