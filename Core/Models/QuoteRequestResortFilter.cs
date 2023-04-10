using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class QuoteRequestResortFilter
    {
        [Key, Column(Order = 0)]
        public int QuoteRequestID { get; set; }
        [Key, Column(Order = 1)]
        public int AccommodationID { get; set; }

        [ForeignKey("QuoteRequestID")]
        public virtual QuoteRequest QuoteRequest { get; set; }
        [ForeignKey("AccommodationID")]
        public virtual Hotel Accommodation { get; set; }
    }
}
