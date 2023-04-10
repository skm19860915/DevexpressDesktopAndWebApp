using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class PhoneType
    {
        public enum ShortCuts { Invalid, Cell, Home, Office }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PhoneTypeID { get; set; }
        public string Name { get; set; }
    }
}