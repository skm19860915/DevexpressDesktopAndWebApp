using System;
using System.Collections.Generic;
using System.Text;

namespace BlitzerCore.Models.UI
{
    public class FinancialSnapShot
    {
        public string Sales_YTD { get; set; }
        public string Realized_ThisMonth { get; set; }
        public string Realized_NextMonth { get; set; }
        public string Realized_YTD { get; set; }
        public string Unrealized_YTD { get; set; }
        public string PL_MTD { get; set; }
        public List<UIBooking> Bookings { get; set; }
    }
}
