using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class TripComponent
    {
        [Key, Column(Order = 0)]
        public int TransportationID { get; set; }
        [Key, Column(Order = 1)]
        public int AccommodationID { get; set; }

        [ForeignKey("TransportationID")]
        public virtual Transportation Transportation { get; set; }
        [ForeignKey("AccommodationID")]
        public virtual Hotel Accommodation { get; set; }
        public int Order { get; set; }
    }
}
