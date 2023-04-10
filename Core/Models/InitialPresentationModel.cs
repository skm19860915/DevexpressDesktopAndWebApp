using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlitzerCore.Models
{
    public class InitialPresentation
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Name")]
        public string Name { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "GUID")]
        public string Guid { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "URL")]
        public string Url { get; set; }
    }
}
