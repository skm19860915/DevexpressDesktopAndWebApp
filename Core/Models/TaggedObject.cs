using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class TaggedObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }

        public int TagId { get; set; }
        [ForeignKey("TagId")]
        public virtual  Tag Tag { get; set; }
        public string ContactId { get; set; }
        [ForeignKey("ContactId")]
        public virtual Contact Contact { get; set; }
        public int? OpportunityId { get; set; }
        [ForeignKey("OpportunityId")]
        public virtual Opportunity Opportunity{ get; set; }
    }
}
