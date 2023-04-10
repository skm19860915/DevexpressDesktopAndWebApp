using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class IncludedSKUs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int FilteredAccommodationId { get; set; }
        [ForeignKey("FilteredAccommodationId")]
        public virtual FilteredAccommodation FilteredAccommodation { get; set; }
        public int SKUId { get; set; }
        [ForeignKey("SKUId")]
        public virtual  SKU SKU { get; set; }
    }
}
