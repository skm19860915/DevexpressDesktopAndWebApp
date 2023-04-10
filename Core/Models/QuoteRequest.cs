using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class QuoteRequest
    {
        public enum QuoteTypes { Package, LandOnly, Both}
        public QuoteRequest()
        {
            TransportationFilters = new List<Transportation>();
            HotelFilters = new List<Hotel>();
            QuoteGroups = new List<QuoteGroup>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuoteRequestID { get; set; }
        //public virtual List<Lead> Leads { get; set; }
        public string AgentId { get; set; }
        [ForeignKey("AgentId")]
        public virtual Agent Agent { get; set; }
        public QuoteTypes QuoteType { get; set; }
        public int OpportunityID { get; set; }
        [ForeignKey("OpportunityID")]
        public virtual Opportunity Opportunity { get; set; }
        public DateTime When { get; set; }
        public DateTime? Finished { get; set; }
        public DateTime? SentQuote {get; set;}
        public string Notes { get; set; }
        public int DepartureAirPortID { get; set; }
        [ForeignKey("DepartureAirPortID")]
        public virtual AirPort DepartureAirPort { get; set; }
        public int DestinationAirPortID { get; set; }
        [ForeignKey("DestinationAirPortID")]
        public virtual AirPort DestinationAirPort { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public virtual ICollection<QuoteGroup> QuoteGroups { get; set; }
        public virtual ICollection<FlightItinerary> Tickets { get; set; }
        public virtual ICollection<Transportation> TransportationFilters { get; set; }
        public virtual ICollection<Hotel> HotelFilters { get; set; }
        public int? DepartureAirPortID2 { get; set; }
        [ForeignKey("DepartureAirPortID2")]
        public virtual AirPort DepartureAirPort2 { get; set; }
        public int? DepartureAirPortID3 { get; set; }
        [ForeignKey("DepartureAirPortID3")]
        public virtual AirPort DepartureAirPort3 { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public int NumberOfRooms { get; set; }
        public int? Child1Age { get; set; }
        public int? Child2Age { get; set; }
        public int? Child3Age { get; set; }
        [NotMapped]
        public List<int> AgesOfKids { get; internal set; }
        [NotMapped]
        public List<int> DOBsOfKids { get; internal set; }
        public int? RefferalId { get; internal set; }
    }
}
