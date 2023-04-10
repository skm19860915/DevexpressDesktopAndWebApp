using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace BlitzerCore.Models
{
    public class Passport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Number { get; set; }
        public DateTime Expiration { get; set; }
        public DateTime Issued { get; set; }
        public string IssuedCity { get; set; }
        public string IssuedCountry { get; set; }
        public string TSA { get; set; }
    }
}
