using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class DeadQuote
    {
        public DeadQuote()
        {
            Transportations = new List<Transportation>();
            Accommodations = new List<Hotel>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuoteID { get; set; }
        public int TripID { get; set; }
        [ForeignKey("TripID")]
        public virtual Trip Trip { get; set; }
        public string ClientID { get; set; }
        [ForeignKey("ClientID")]
        public virtual Client Client { get; set; }
        public double Amount { get; set; }
        public DateTime When { get; set; }
        public int DepartureID { get; set; }
        [ForeignKey("DepartureID")]
        public virtual AirPort DepartureCity { get; set; }
        public int DesitinationID { get; set; }
        [ForeignKey("ArrivalID")]
        public virtual AirPort DestinationCity { get; set; }
        public virtual ICollection<Transportation> Transportations { get; set; }
        public virtual ICollection<Hotel> Accommodations { get; set; }
    }
}
