using System;
using System.Collections.Generic;
using System.Text;

namespace BlitzerCore.Models.UI
{
    public class QuoteRequestResults
    {
        public int Id { get; set; }
        public UIQuoteRequest QuoteRequest { get; set; }
        public List<AirQuote> AirLineTickets { get; set; }
        public List<ResortQuote> Hotels { get; set; }
    }
}
