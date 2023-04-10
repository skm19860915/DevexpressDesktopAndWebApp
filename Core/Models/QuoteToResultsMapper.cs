using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;


namespace BlitzerCore.Models
{
    // 
    public class QuoteToResultsMapper
    {
        public QuoteToResultsMapper()
        {
            Booked = false;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QRToQMapID { get; set; }
        public int QuoteGroupID { get; set; }
        [ForeignKey("QuoteGroupID")]
        public virtual QuoteGroup QuoteGroup { get; set; }
        public int? QuoteRequestResortID { get; set; }
        [ForeignKey("QuoteRequestResortID")]
        public virtual QuoteRequestResort QuoteRequestResort { get; set; }
        public int? FlightItineraryId { get; set; }
        [ForeignKey("FlightItineraryId")]
        public virtual FlightItinerary FlightItinerary { get; set; }
        public bool Exclude { get; set; }
        public bool Booked { get; set; }
        public DateTime? BookedOn { get; set; }
    }
}
