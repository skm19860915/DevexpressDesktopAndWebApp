using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class Amenity
    {
        public enum AmenityTypes { Invalid, AllInclusive = 1, AdultsOnly = 8 }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Type { get; set; }
        public static string GetAmenityName ( AmenityTypes aType )
        {
            switch ( aType)
            {
                case AmenityTypes.AdultsOnly:
                    return "Adults Only";
                case AmenityTypes.AllInclusive:
                    return "All Inclusive";
            }

            return "N/A";
        }
    }
}
