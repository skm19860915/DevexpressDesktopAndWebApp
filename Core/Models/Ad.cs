using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BlitzerCore.Models
{
    public class Ad
    {
        public enum AdTypes { Destination, Experience, Service }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdID { get; set; }
        public AdTypes AdType { get; set; }
        public int MerchantId { get; set; }
        [ForeignKey("MerchantId")]
        public virtual Merchant Merchant { get; set; }
        public virtual List<Blob> Blobs { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public virtual List<Ad> SupplementalMaterial {get; set;}

    }
}
