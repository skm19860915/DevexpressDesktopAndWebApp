using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace BlitzerCore.Models
{
    public class NameReplacement
    {
        public enum ReplacementTypes { Hotel, HotelRoom }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? TourOperatorId { get; set; }
        [ForeignKey("TourOperatorId")]
        public virtual TourOperator Trip { get; set; }
        public ReplacementTypes ReplaceType { get; set; }

        public string Original { get; set; }
        public int HotelId { get; set; }
        [ForeignKey("HotelId")]
        public virtual Hotel Hotel { get; set; }
        public int? SKUId { get; set; }
        [ForeignKey("SKUId")]
        public virtual SKU SKU { get; set; }
    }
}
