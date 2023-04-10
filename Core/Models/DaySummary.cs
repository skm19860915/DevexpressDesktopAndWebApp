using System;
using System.Collections.Generic;
using System.Text;

namespace BlitzerCore.Models
{
    public class DaySummary
    {
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public List<Uri> EventTypes { get; set; }
    }
}
