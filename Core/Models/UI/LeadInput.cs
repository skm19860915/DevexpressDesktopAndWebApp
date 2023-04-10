using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models.UI
{
    [NotMapped]
    public class LeadInput
    {
        public List<UIContact> Leads { get; set; }
        public List<int> RelationshipOrder { get; set; }
    }
}
