using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class AmenityMap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int? AccommodationID { get; set; }
        [ForeignKey("AccommodationID")]
        public virtual Hotel Accommodation { get; set; }
        public int? StagingHotelID { get; set; }
        [ForeignKey("StagingHotelID")]
        public virtual Staging.Hotel StagingHotel { get; set; }
        public int AmenityID { get; set; }
        [ForeignKey("AmenityID")]
        public virtual Amenity Amenity { get; set; }
    }
}
