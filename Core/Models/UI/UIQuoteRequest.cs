using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
using System.Collections.Specialized;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlitzerCore.Models.UI
{
    [NotMapped]
    public class UIQuoteRequest
    {
        public UIQuoteRequest() {
            Contacts = new List<UIContact>();
            AirPortCodes = new List<SelectListItem>();
            RelationShips = new List<SelectListItem>();
            ErrorMsgs = new List<ErrorMsg>();
            ActiveQuoteGroups = new List<UIQuoteGroup>();
        }
        public int Id { get; set; }
        public int QuoteID { get; set; }
        public int FilterID { get; set; }
        [Required(ErrorMessage = "Please enter the referral source.")]
        public int? RefferalId { get; set; }
        public int OpportunityID { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public string Viewed { get; set; }
        public string ClientNotes { get; set; }
        public string Amount { get; set; }
        public virtual List<BlitzerCore.Models.UI.UIContact> Contacts { get; set; } 
        public virtual List<UIQuote> Quotes { get; set; }
        public string AgentId { get; set; }
        public UIContact Agent { get; set; }
        public int QuoteType { get; set; }
        public string AgentPhone { get; set; }
        public string DepartureCityCode { get; set; }
        public string DestinationCityCode { get; set; }
        public string DepartureCityCode1 { get; set; }
        public string DepartureCityCode2 { get; set; }
        public string NumberOfAdults { get; set; }
        public string NumberOfRooms { get; set; }
        public string Child1Age { get; set; }
        public string Child2Age { get; set; }
        public string Child3Age { get; set; }
        public bool SendQuote { get; set; }
        public bool EnableSendQuoteBtn { get; set; }
        public string SentQuote { get; set; }
        public bool SendInsurance { get; set; }
        public string QuoteDueDate { get; set; }

        public List<int> AgesOfKids { get; set; }
        public List<int> DOBsOfKids { get; set; }
        public string When { get; set; }
        public string Finished { get; set; }
        [Required(ErrorMessage = "Please enter Start Date.")]
        [Display(Name = "Start Date")]
        public string StartDate { get; set; }
        [Required(ErrorMessage = "Please enter End Date.")]
        [Display(Name = "End Date")]
        public string EndDate { get; set; }
        public List<SelectListItem> RelationShips { get; set; }
        public List<SelectListItem> AirPortCodes { get; set; }
        public IEnumerable<UIQuoteGroup> ActiveQuoteGroups { get; set; }
        public string DepartureCityID { get; set; }
        public string DestinationCityID { get; set; }
        public List<ErrorMsg> ErrorMsgs { get; set; }
        public bool QuoteButtonDisabled { get; set; }
    }
}
