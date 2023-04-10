using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
using System.Collections.Specialized;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlitzerCore.Models.UI
{
    public class UIQuoteGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int QuoteRequestID { get; set; }
        [ForeignKey("QuoteRequestID")]
        public virtual UIQuoteRequest QuoteRequest { get; set; }
        public int FilterId { get; set; }
        public int OpportunityID { get; set; }
        [ForeignKey("OpportunityID")]
        public virtual Opportunity Opportunity { get; set; }
        public bool Locked { get; set; }
        public string GUID { get; set; }  // Used by the client to view quote witout account
        public string Viewed { get; set; }
        public string SentDate { get; set; }
        public string Note { get; set; }
        public bool NoData { get; set; }
        public Dictionary<UICompany, List<UIQuote>> Quotes { get; set; }
        public List<UIQuote> QuoteList { get; set; }
        public List<UIContact> Contacts { get; set; }
        public UIContact Agent { get; set; }
        public List<FlightItinerary> Flights { get; set; }
        public List<UIFlight> UIFlights { get; set; }
        public QuoteGroupStatus Status { get; set; }
        public List<ErrorMsg> ErrorMsgs { get; set; }
        public UICountry CountryData { get; set; }
        public bool WarningNoEmail { get; set; }
    }
}
