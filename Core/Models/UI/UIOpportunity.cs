using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models.UI
{
    [NotMapped]
    public class UIOpportunity : BaseUI
    {
        public UIOpportunity()
        {
            Travelers = new List<UIContact>();
            QuoteRequests = new List<UIQuoteRequest>();
        }
        public int Id { get; set; }
        public List<UIContact> Travelers { get; set; }
        public string OutBoundAirport { get; set; }
        public string InBoundAirPort { get; set; }
        public string OutBoundDate { get; set; }
        public string InBoundDate { get; set; }
        public OpportunityStages Stage { get; set; }
        public string StageStr { get; set; }
        public string Owner { get; set; }
        public string AgentId { get; set; }
        public string Notes { get; set; }
        public bool AllInclusive { get; set; }
        public bool HotelOnly { get; set; }
        public bool Cruise { get; set; }
        public string PerPersonBudget { get; set; }
        public string TripBudget { get; set; }
        public List<UIQuoteRequest> QuoteRequests { get; set; }
        public string Name { get; set; }
        public string Activity { get; set; }
        public bool IsTrip { get; set; }
        public List<UITask> Tasks { get; set; }
        public string Age { get; internal set; }
        public string Note_Who { get; set; }
        public string Note_Where { get; set; }
        public string Note_Text { get; set; }
        public IEnumerable<UINote> NoteEntries { get; set; }
        public List<UIFile> Files { get; set; }


    }
}
