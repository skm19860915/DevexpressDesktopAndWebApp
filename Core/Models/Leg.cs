using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class Leg
    {
        /// <summary>
        /// Represents all the connections in a leg of a trip.  
        /// Example  BWI to cancun can have 1 connection in ATL
        /// The TripTicketId is the parent which has both Legs
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LegID { get; set; }
        // This is all the Connection Flights
        public List<Flight> Flights { get; set; }
        public int QuoteGroupId { get; set; }
        [ForeignKey("QuoteGroupId")]
        public QuoteGroup QuoteGroup {get; set; }
        public int TourOperatorId { get; set; }
        [ForeignKey("TourOperatorId")]
        public TourOperator TourOperator { get; set; }
        public int TripTicketId { get; set; }
        public int Stops { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
