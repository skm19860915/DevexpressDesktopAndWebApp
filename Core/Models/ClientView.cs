using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class ClientView
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime When { get; set; }
        public string IPAddress { get; set; }
        public string Cookie { get; set; }
        public string Page { get; set; }
        public int? QuoteGroupId { get; set; }
        [ForeignKey("QuoteGroupId")]
        public virtual QuoteGroup QuoteGroup { get; set; }
        public string Key { get; set; }
    }
}
