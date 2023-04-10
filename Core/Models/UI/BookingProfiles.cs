using System;
using System.Collections.Generic;
using System.Text;

namespace BlitzerCore.Models.UI
{
    public class BookingProfiles
    {
        public int TripID { get; set; }
        public List<UIContact> Travellers { get; set; }
    }
}
