using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace BlitzerCore.Models
{
    public class AddressMap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int AddressTypeID { get; set; }
        [ForeignKey("AddressTypeID")]
        public virtual AddressType AddressType { get; set; }
        public string UserID {get; set;}
        [ForeignKey("UserID")]
        public virtual Contact User { get; set; }
        public int AddressID { get; set; }
        [ForeignKey("AddressID")]
        public virtual Address Address { get; set; }
    }
}
