using System;
using System.Collections.Generic;
using System.Text;

namespace BlitzerCore.Models.UI
{
    public class UINote
    {
        public int Id { get; set; }
        public string ContactId { get; set; }
        public string Contact { get; set; }
        public int? CompanyId { get; set; }
        public string Company { get; set; }
        public int? OpportunityId { get; set; }
        public string Opportunity { get; set; }
        public string When { get; set; }
        public string WriterId { get; set; }
        public string Writer { get; set; }
        public string Memo { get; set; }
        public string Who { get; set; }
        public string Where { get; set; }
        public bool IsTrip { get; internal set; }
    }
}
