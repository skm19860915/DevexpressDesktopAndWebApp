using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;

namespace BlitzerCore.Models
{
    public class Hotel : Company
    {
        public Hotel() : base()
        {
            Amenities = new List<AmenityMap>();
            RoomTypeQuotes = new List<QuoteRequestResort>();
            BusinessTypeID = 3;
        }

        public enum PreferredBy { AAVacations, DeltaVacations }
        public bool AAPreferredProvider { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string Bedding { get; set; }
        public string Inclusions { get; set; }
        public string Address1 { get; set; }

        public bool IsAdultOnly()
        {
            return Amenities.Any(x => x.AmenityID == (int)Amenity.AmenityTypes.AdultsOnly);
        }

        public string Address2 { get; set; }
        public string Area { get; set; }
        public double AirportDistance { get; set; }
        public double? FoodRating { get; set; }
        public double? RoomRating { get; set; }
        public int? QuoteRequestID { get; set; }
        [ForeignKey("QuoteRequestID")]
        public virtual QuoteRequest QuoteRequest { get; set; }
        public List<AmenityMap> Amenities { get; set; }
        public List<QuoteRequestResort> RoomTypeQuotes { get; set; }
        public string Header { get; set; }
        public string Summary { get; set; }
        public Uri Video {get; set;}
        public List<Paragraph> Paragraphs { get; set; }
        public int? AirPortID { get; set; }
        [ForeignKey("AirPortID")]
        public virtual AirPort AirPort { get; set; }
        public override string ToString()
        {
            return Name + " Id = " + Id;
        }
    }
}
