using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class Phone
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PhoneID { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Contact User { get; set; }
        public int? CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }
        public string PhoneNumber { get; set; }
        public int PhoneTypeID { get; set; }
        public virtual PhoneType PhoneType { get; set; }
        public bool Defaut { get; set; }
    }
}
