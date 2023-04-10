using System;
using System.Collections.Generic;
using System.Text;

namespace BlitzerCore.Models
{
    public class TripSummary
    {
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public ICollection<DaySummary> DayEvents { get; set; }
        public Uri Picture { get; set; }
    }
}
