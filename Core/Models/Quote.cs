using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace BlitzerCore.Models
{
    public enum QuoteStatus { 
        [Display(Name ="Not Ready")]
        NotReady, Ready, Sent, Booked, Excluded, Inactive }
    /// <summary>
    /// Stores the Results after a Filter has been applied
    /// Must Execute ApplyFilter to populate this object
    /// </summary>
    public class Quote
    {
        public enum TransferTypes { Basic, NonStop, Private, Premium };
        public enum InsuranceTypes { Basic, Plus, PreDepartureWaiver };

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuoteID { get; set; }
        public int QuoteRequestID { get; set; }
        [ForeignKey("QuoteRequestID")]
        public virtual QuoteRequest QuoteRequest { get; set; }
        public int QuoteGroupID { get; set; }
        [ForeignKey("QuoteGroupID")]
        public virtual QuoteGroup QuoteGroup { get; set; }
        public int? SupplierId { get; set; }
        [ForeignKey("SupplierId")]
        public virtual Hotel Accommodation { get; set; }
        public int? AccommodationRoomTypeID { get; set; }
        [ForeignKey("AccommodationRoomTypeID")]
        public virtual SKU AccommodationRoomType { get; set; }
        public int? TourOperatorID { get; set; }
        [ForeignKey("TourOperatorID")]
        public virtual TourOperator TourOperator { get; set; }
        public double PackagePrice { get; set; }
        public double ResortPrice { get; set; }
        public double FlightPrice { get; set; }
        public double SubTotal { get; set; }
        public double Adjustment { get; set; }
        public double Total { get; set; }
        public bool Booked { get; set; }
        public DateTime? BookedOn { get; set; }
        public string BookedById { get; set; }
        [ForeignKey("BookedById")]
        public Contact BookedBy { get; set; }
        public QuoteStatus Status { get; set; }
        public List<FlightItinerary> Flights { get; set; }
        public Quote.TransferTypes? Transfer { get; set; }
        public Quote.InsuranceTypes? Insurance { get; set; }

        public Quote(QuoteGroup aGroup)
        {
            QuoteGroup = aGroup;
            QuoteGroupID = aGroup.Id;
            QuoteRequestID = aGroup.QuoteRequestID;
        }
        
        public Quote()
        {
        }

     }
}
