using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models.UI
{
    [NotMapped]
    public class ResortQuote
    {
        public int AccommodiationID { get; set; }
        public string Name { get; set; }
        public double? Stars { get; set; }
        public string Area { get; set; }
        public Uri AccommodiationPictureURL { get; set; }
        public Uri ResortURL { get; set; }
        public Uri RoomPictureURL { get; set; }
        public List <RoomQuote> RoomQuotes { get; set; }
        public string Address1 { get; set; }
        public bool AdultsOnly { get; set; }
        public int? CityId { get; set; }
        [ForeignKey("CityId")]
        public virtual City City { get; set; }
        public string State { get; set; }
        public void Import(Hotel aResort)
        {
            Stars = aResort.Rating;
            AccommodiationID = aResort.Id;
            Address1 = aResort.Address1;
            //CityId = aResort.CityId;
            Area = aResort.Area;
            State = aResort.State;
            Name = aResort.Name;
            AdultsOnly = aResort.IsAdultOnly();
            AccommodiationPictureURL = aResort.ThumbNail;
            ResortURL = aResort.HyperLink;
        }
        public ResortQuote ()
        {
            RoomQuotes = new List<RoomQuote>();
        }
    }
}
