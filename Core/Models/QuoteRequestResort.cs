using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlitzerCore.Models;

namespace BlitzerCore.Models
{
    public class QuoteRequestResort
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuoteRequestResortID { get; set; }

        public QuoteRequestResort()
        {
            When = DateTime.Now;
        }

        public int QuoteGroupId { get; set; }
        [ForeignKey("QuoteGroupId")]
        public virtual QuoteGroup QuoteGroup { get; set; }
        public bool LandOnly { get; set; }
        public string MealPlan { get; set; }
        public bool Exclude { get; set; }
        public Uri RoomURL { get; set; }
        public int ResortId { get; set; }
        [ForeignKey("ResortId")]
        public virtual Hotel Resort { get; set; }
        public int ResortRoomTypeID { get; set; }
        [ForeignKey("ResortRoomTypeID")]
        public virtual SKU ResortRoomType { get; set; }
        public int TourOperatorID { get; set; }
        [ForeignKey("TourOperatorID")]
        public virtual TourOperator TourOperator { get; set; }
        public double Price { get; set; }
        public DateTime When {get; set;}
    }
}
