using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class UserMap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserMapID { get; set; }
        public int OpportunityID { get; set; }
        [ForeignKey("OpportunityID")]
        public virtual Opportunity Opportunity { get; set; }
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual Contact User { get; set; }
        public bool Primary { get; set; }
    }
}
