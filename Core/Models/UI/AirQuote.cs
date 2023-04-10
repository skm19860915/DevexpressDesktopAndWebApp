using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models.UI
{
    [NotMapped]
    public class AirQuote
    {
        public int TicketID { get; set; }
        public bool Booked { get; set; }
        public string OutBoundDepartDate { get; set; }
        public string OutBoundDepartTime { get; set; }
        public string OutBoundConnectionCity { get; set; }
        public string OutBoundConnectionArrivalTime { get; set; }
        public string OutBoundLayOverDuration { get; set; }
        public string OutBoundArriveTime { get; set; }

        public string InBoundDepartDate { get; set; }
        public string InBoundDepartTime { get; set; }
        public string InBoundConnectionCity { get; set; }
        public string InBoundConnectionArrivalTime { get; set; }
        public string InBoundLayOverDuration { get; set; }
        public string InBoundArriveTime { get; set; }
    }
}
