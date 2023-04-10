using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BlitzerCore.Models
{
    public class AirPort
    {
        [JsonProperty(NullValueHandling =NullValueHandling.Ignore, PropertyName ="AirPortID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AirPortID { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Code")]
        [MinLength (3, ErrorMessage = "AirPort Codes are 3 characters")]
        [MaxLength (3, ErrorMessage = "AirPort Codes are 3 characters")]
        public string Code { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Name")]
        public string Name { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "URL")]
        public Uri URL { get; set; }
        public string Address { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "City")]
        public string City { get; set; }
        public string State { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "CountryId")]
        public int? CountryId { get; set; }
        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }
        public string CountryName
        {
            get
            {
                if (Country != null)
                    return Country.Name;
                else
                    return $"No Country Defined for {Code}";
            }
        }
        public string ZipCode { get; set; }
        public bool Default { get; set; }
    }
}
