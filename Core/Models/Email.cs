using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class Email
    {
        public const int PERSONNEL_EMAIL = 1;
        public const int BUSINESS_EMAIL = 2; 
            
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmailID { get; set; }
        public int EmailTypeID { get; set; }
        [ForeignKey("EmailTypeID")]
        public virtual EmailType Type {get; set; }
        public string Address { get; set; }
        public bool Preferred { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Contact User { get; set; }
        public int? CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }
        public Email()
        {
            Math.Round(560.34);
        }
    }
}
