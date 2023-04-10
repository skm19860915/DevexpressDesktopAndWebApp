using System;
using System.Collections.Generic;
using System.Text;

namespace BlitzerCore.Models.UI
{
    public class UITripList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Stage { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double GrossCommission { get; set; }
        public double ICCommission { get; set; }
    }
}
