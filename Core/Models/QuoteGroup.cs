using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
namespace BlitzerCore.Models
{
    public enum QuoteGroupStatus { Open, Sent, Deleted }
    public enum QuoteGroupFilter { All, Active, Deleted}
    public class QuoteGroup
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public QuoteGroup()
        {
            InitObjects();
        }

        public QuoteGroup (QuoteRequest aRequest )
        {
            QuoteRequest = aRequest;
            InitObjects();
        }

        void InitObjects()
        {
            Quotes = new List<Quote>();
            BotQuotes = new List<QuoteToResultsMapper>();
        }

        public int QuoteRequestID { get; set; }
        [ForeignKey("QuoteRequestID")]
        public virtual QuoteRequest QuoteRequest { get; set; }
        public int? SelectedQuoteRequestTicketId { get; set; }
        [ForeignKey("QuoteRequestTicketId")]
        public virtual FlightItinerary SelectedQuoteRequestTicket { get; set; }
        public bool Locked { get; set; }
        public string GUID { get; set; }  // Used by the client to view quote witout account
        public DateTime? Viewed { get; set; }
        public DateTime? SentDate { get; set; }
        public QuoteGroupStatus Status { get; set; }
        // This holds all them mapps to the quotes returned from the bot in the QuoteRequestResults
        public List<QuoteToResultsMapper> BotQuotes { get; set; }
        // These would be manual quotes        
        public List<Quote> Quotes { get; set; }
        public List<FlightItinerary> Flights { get; set; }     
        public List<ClientView> ClientViews { get; set; }
        public string Note { get; set; }
    }
}
