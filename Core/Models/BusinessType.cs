using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace BlitzerCore.Models
{
    public class BusinessType
    {
        public const string CRUISING = "Cruising";
        public const string HOTEL = "Hotel";

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Type { get; set; }
        public bool Commissionable { get; set; }
    }
}