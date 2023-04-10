using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Collections.Generic;

namespace BlitzerCore.Models
{
    public class AgentAirPortPreference
    {
        public AgentAirPortPreference() {
            PreferredHotels = new List<FilteredAccommodation>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public bool Stops_Equals_0 { get; set; }
        public bool Stops_Equals_1 { get; set; }
        public bool AdultOnly { get; set; }
        public int StarRating { get; set; }
        public bool AdultsOnlySection { get; set; }
        public bool AllInclusive { get; set; }
        public double PerPersonBudget { get; set; }
        public double TripBudget { get; set; }
        public double TripMinBudget { get; set; }
        public int? OutboundAirFilterMapID { get; set; }
        public int? InboundAirFilterMapID { get; set; }
        public int? AirFilterMapID { get; set; }
        public int? AccommodationFilterMapID { get; set; }
        public int? FilteredAccommodationId { get; set; }
        [ForeignKey("FilteredAccommodationId")]
        public virtual List<FilteredAccommodation> PreferredHotels  { get; set; }
        public int AirportID { get; set; }
        [ForeignKey("AirportID")]
        public virtual AirPort Airport { get; set; }
        public string AgentId { get; set; }
        [ForeignKey("AgentId")]
        public virtual Agent Agent { get; set; }
        ////[ForeignKey("OutboundAirFilterID")]
        ////public AIFilterMAP OutboundAirFilter { get; set; }
        ////[ForeignKey("InboundAirFilterID")]
        ////public AIFilterMAP InboundAirFilter { get; set; }
        ////[ForeignKey("AccommodationFilterID")]
        ////public AIFilterMAP AccommodationFilter { get; set; }
        ////[ForeignKey("AirFilterID")]
        ////public AIFilterMAP AirFilter { get; set; }
    }
}
