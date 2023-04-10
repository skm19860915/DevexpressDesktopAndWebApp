using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace BlitzerCore.Models
{
    public class QuoteSKUs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int QuoteID { get; set; }
        [ForeignKey("QuoteID")]
        public virtual Quote Quote { get; set; }
        public int SKUID { get; set; }
        [ForeignKey("SKUID")]
        public virtual SKU SKU { get; set; }
    }
}
