using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class FilteredAccommodation
    {
        [Key]
        public int FilterAcommodationID { get; set; }
        public int AccommodationID { get; set; }
        [ForeignKey("AccommodationID")]
        public virtual Hotel Accommodation { get; set; }
        public virtual List<IncludedSKUs> IncludedSKUs { get; set; } 
        public int? FilterID { get; set; }
        [ForeignKey("FilterID")]
        public virtual Filter Filter { get; set; }
        public int? PreferenceId { get; set; }
        [ForeignKey("PreferenceId")]
        public virtual AgentAirPortPreference Preference { get; set; }
    }
}
