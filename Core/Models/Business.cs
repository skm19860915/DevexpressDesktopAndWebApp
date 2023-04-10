using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;
using System.Collections.Generic;

namespace BlitzerCore.Models
{
    public class Business
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Phone> PhoneNumbers { get; set; }
        [Required]
        public List<Email> Emails { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public int BusinessTypeID { get; set; }
        [ForeignKey("BusinessTypeID")]
        public virtual BusinessType BusinessType { get; set; }
        public int IndustryID { get; set; }
        public virtual Industry Industry { get; set; }
        public int AddressMapID { get; set; }
        [ForeignKey("AddressMapID")]
        public virtual AddressMap AddressMap { get; set; }
    }
}
