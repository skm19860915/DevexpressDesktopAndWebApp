using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class SKU
    {
        public int SKUID { get; set; }
        public int ProviderID { get; set; }
        [ForeignKey("ProviderID")]
        public virtual Company Provider { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Uri URL { get; set; }
        public int SortOrder { get; set; }
    }
}
