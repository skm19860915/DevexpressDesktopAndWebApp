using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class Flight : Transportation
    {
        public Flight()
        {
        }
        public enum SIDES { OUTBOUND, INBOUND }
        public DateTime Quoted { get; set; }
        public int? FlightItinId { get; set; }
        [ForeignKey("FlightItinId")]
        public virtual FlightItinerary FlightItinerary { get; set; }
        public Guid LegGUID { get; set; }
        public SIDES Side { get; set; }
        public int LegId { get; set; }
        [ForeignKey("LegId")]
        public virtual Leg Leg { get; set; }
    }
}
