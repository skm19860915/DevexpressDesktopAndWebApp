using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class QuoteRequestTourOperatorFilter
    {
        [Key, Column(Order = 0)]
        public int QuoteRequestID { get; set; }
        [Key, Column(Order = 1)]
        public int TourOperatorID { get; set; }

        [ForeignKey("QuoteRequestID")]
        public virtual QuoteRequest QuoteRequest { get; set; }
        [ForeignKey("TourOperatorID")]
        public virtual TourOperator TourOperator { get; set; }
    }
}