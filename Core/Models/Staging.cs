using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class Staging
    {
        public class Hotel
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int HotelStagingID { get; set; }
            public string Name { get; set; }
            public string Location { get; set; }
            public string Stars { get; set; }
            public List<HotelRate> HotelRateTypes { get; set; }
            public int TourOperatorID { get; set; }
            public int RequestID { get; set; }
            public int QuoteGroupId { get; set; }
            [ForeignKey("QuoteGroupId")]
            public QuoteGroup QuoteGroup { get; set; }
            public bool AAPreferred { get; set; }
            public string Price { get; set; }
            public string ChildPrice { get; set; }
            public List<AmenityMap> Amenities { get; set; }
            public DateTime When { get; set; }
            public Hotel()
            {
                HotelRateTypes = new List<HotelRate>();
                Amenities = new List<AmenityMap>();
                When = DateTime.Now;
            }

        }

        public class HotelRate
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
            public int HotelRateStagingID { get; set; }
            public string RateType { get; set; }
            public string RoomType { get; set; }
            public string Price { get; set; }
            public bool LandOnly { get; set; }
            public string ChildPrice { get; set; }
            
            public int HotelStagingID { get; set; }
            [ForeignKey("HotelStagingID")]
            public virtual Hotel HotelStaging { get; set; }
        }

        public class Flight
        {
            public enum PullTypes { Automated, Manual }
            public enum SIDES {  DEPARTURE, RETURN }
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
            public int FlightStagingID { get; set; }
            public int TourOperatorID { get; set; }
            [ForeignKey("TourOperatorID")]
            public TourOperator TourOperator { get; set; }
            public string Carrier { get; set; }
            public string DepartDate { get; set; }
            public string DepartLocation { get; set; }
            public string DepartTime { get; set; }

            public string ArrivalDate { get; set; }
            public string ArrivalLocation { get; set; }
            public string ArrivalTime { get; set; }
            public string Aircraft { get; set; }
            public string NumberOfStop { get; set; }
            public int QuoteGroupId { get; set; }
            [ForeignKey("QuoteGroupId")]
            public QuoteGroup QuoteGroup { get; set; }
            [NotMapped]
            public PullTypes PullType { get; set; }
            public Guid LegGUID { get; set; }
            public Guid ItineraryGUID { get; set; }
            public SIDES Side { get; set; }
            public string ExtraCost { get; set; }
            public override string ToString()
            {
                return DepartLocation + " " + DepartDate + " " + DepartTime +
                    ArrivalLocation + " " + ArrivalDate + " " + ArrivalTime + Side;
            }
            public Flight() { }
        }

        public class FlightHotelInformation
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int HotelStagingID { get; set; }

            public FlightHotelInformation ()
            {
                Flights = new List<Flight>();
                Hotels = new List<Hotel>();
            }

            public int QuoteGroupID { get; set; }
            [ForeignKey("QuoteID")]
            public virtual QuoteGroup Quote { get; set; }
            public virtual List<Flight> Flights { get; set; }
            public virtual List<Hotel> Hotels { get; set; }
        }
    }
}
