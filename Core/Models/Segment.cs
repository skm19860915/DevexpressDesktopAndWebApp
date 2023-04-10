using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class Segment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SegmentID { get; set; }
        public string DepartureGate { get; set; }
        public string DepartureTerminal { get; set; }
        public DateTime DepartureTime { get; set; }
        public int DepartureID { get; set; }
        [ForeignKey("DepartureID")]
        public virtual AirPort DepartureCity { get; set; }
        public string ArrivalTerminal { get; set; }
        public string ArrivalGate { get; set; }
        public DateTime ArriveTime { get; set; }
        public int ArrivalID { get; set; }
        [ForeignKey("ArrivalID")]
        public virtual AirPort ArrivalCity { get; set; }
    }
}
