using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class RegisterBooking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SupplyId { get; set; }
        [ForeignKey("SupplyId")]
        public virtual Company Supplier { get; set; }
        public string URL { get; set; }
        public string Directions { get; set; }
    }
}
