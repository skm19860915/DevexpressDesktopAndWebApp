using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class FilteredTicket
    {
        [Key]
        public int FilterTicketID { get; set; }
        public int TicketID { get; set; }
        [ForeignKey("QuoteRequestTicket")]
        public virtual FlightItinerary Ticket { get; set; }
        public int FilterID { get; set; }
        [ForeignKey("FilterID")]
        public virtual Filter Filter { get; set; }
    }
}
