using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models.UI
{
    [NotMapped]
    public class RoomQuote
    {
        public int QuoteRequestResortID { get; set; }
        public RoomQuote() { }
        public RoomQuote(BlitzerCore.Models.Quote aQuote )
        {
            //MealPlan = aQuote.MealPlan;
            RoomType = aQuote.AccommodationRoomType.Name;
            Price = aQuote.SubTotal.ToString("C2");
            //QuoteRequestResortID = aQuote.QuoteRequestResortID;
            //RoomURL = aQuote.RoomURL;
            //Exclude = aQuote.Exclude;
        }
        public string MealPlan { get; set; }
        public string RoomType { get; set; }
        public string Price { get; set; }
        public Uri RoomURL { get; set; }
        public bool Exclude { get; set; }
        public bool Booked { get; set; }
    }
}
