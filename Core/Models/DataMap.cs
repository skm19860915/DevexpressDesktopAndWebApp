using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class DataMap
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int TourOperatorID { get; set; }
        [ForeignKey("TourOperatorID")]
        public TourOperator TourOperator { get; set; }
        public string input { get; set; }
        public string output { get; set; }
    }
}
