using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class Transportation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransportationID { get; set; }
        public enum Types { AIRLINE, CAR, TRAIN, BUS }

        public Transportation()
        {
        }

        public string Confirmation { get; set; }
        public Types Type { get; set; }
        public DateTime Depart { get; set; }
        public int? DepartAirPortID { get; set; }
        [ForeignKey("DepartAirPortID")]
        public virtual AirPort DepartAirPort { get; set; }
        public int? ArrivalAirPortID { get; set; }
        [ForeignKey("ArrivalAirPortID")]
        public virtual AirPort ArrivalAirPort { get; set; }

        public DateTime Arrive { get; set; }
        public string Carrier { get; set; }
        public string Identifer { get; set; }
        public int TourOperatorId { get; set; }
        [ForeignKey("TourOperatorId")]
        public TourOperator TourOperator { get; set; }
        public int QuoteGroupId { get; set; }
        [ForeignKey("QuoteGroupId")]
        public virtual QuoteGroup QuoteGroup { get; set; }

        public int? QuoteID { get; set; }
        [ForeignKey("QuoteID")]
        public virtual Quote Quote { get; set; }
    }
}
