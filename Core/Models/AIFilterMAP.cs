using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class AIFilterMAP
    {
        public enum MapTypes { AIR,InBound, OutBound}
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public MapTypes MapType { get; set; }
        //[NotMapped]
        //public int FilterID { get; set; }
        //[ForeignKey("FilterID")]
        //[NotMapped]
        //public virtual Filter Filter { get; set; }
        public int AIFilterID { get; set; }
        [ForeignKey("AIFilterID")]
        public virtual AIFilter AIFilter { get; set; }
    }
}
