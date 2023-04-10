using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class Presentation
    {
        public enum Statuses {  NotReady, Ready, Completed }
        public Presentation()
        {
            Queue = new List<PresentationQueueItem>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "GUID")]
        public string Guid { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "URL")]
        public string Url { get; set; }
        public string ClientName { get; set; }
        public DateTime Created { get; set; }
        public Statuses Status { get; set; }
        public string Cookie { get; set; }
        public string IPAddr { get; set; }
        public int?  CurrentWebPageId { get; set; }
        public virtual WebPage CurrentWebPage { get; set; }
        public ICollection<PresentationQueueItem> Queue { get; set; }
    }
}
