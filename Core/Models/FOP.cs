using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace BlitzerCore.Models
{
    public enum CardTypes { MasterCard, Visa, AmericaExpress, Discover}
    public class FOP
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public CardTypes CardType { get; set; }
        public string NameOnCard { get; set; }
        public string Number { get; set; }
        public string Expiration { get; set; }
        public string CVN { get; set; }
        public bool AddressSameAsResidence { get; set; }
        public string Memo { get; set; }
        public string OwnerID { get; set; }
        [ForeignKey("OwnerID")]
        public virtual Contact Owner { get; set; }
        public string CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        public Contact CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedById { get; set; }
        [ForeignKey("UpdatedById")]
        public Contact UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }

    }
}
