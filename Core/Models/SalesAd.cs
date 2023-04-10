using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;

namespace BlitzerCore.Models
{
    public class SalesAd : Post
    {
        public double Price { get; set; }
        public string CallToAction { get; set; }
        public int AccommodationID { get; set; }
        public Hotel Accommodation { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int DestinationID { get; set; }
        public AirPort Destination { get; set; }
    }
}
