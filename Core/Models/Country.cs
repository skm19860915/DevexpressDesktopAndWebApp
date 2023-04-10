using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlitzerCore.Models.UI;
using Newtonsoft.Json;

namespace BlitzerCore.Models
{
    public class Country
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Name")]
        public string Name { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "RegionId")]
        public int RegionId { get; set; }
        [ForeignKey("RegionId")]
        public virtual Region Region { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "ImageLocation")]
        public string ImageLocation { get; set; }
        public virtual List<City> Cities { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "PageId")]
        public int? PageId { get; set; }
        [ForeignKey("PageId")]
        public virtual UICountry CountryPage { get; set; }
    }
}
