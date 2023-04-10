using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class FlightItinerary
    {
        [Key]
        public int FlightItineraryId { get; set; }
        public Leg OutBound { get; set; }
        public Leg InBound { get; set; }
        public double ExtraCost { get; set; }
        public int TourOperatorId { get; set; }
        [ForeignKey("TourOperatorId")]
        public TourOperator TourOperator { get; set; }
        public int QuoteRequestID { get; set; }
        [ForeignKey("QuoteRequestID")]
        public virtual QuoteRequest QuoteRequest { get; set; }
        public int QuoteGroupId { get; set; }
        [ForeignKey("QuoteGroupId")]
        public virtual QuoteGroup QuoteGroup { get; set; }
    }
}
