using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class Filter
    {
        public Filter()
        {
            Name = "";
            Accommodations = new List<FilteredAccommodation>();
            Stars = 0;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FilterID { get; set; }
        public virtual List<FilteredAccommodation> Accommodations { get; set; }
        public int QuoteGroupID { get; set; }
        [ForeignKey("QuoteGroupID")]
        public virtual QuoteGroup QuoteGroup { get; set; }
        public int QuoteRequestID { get; set; }
        [ForeignKey("QuoteRequestID")]
        public virtual QuoteRequest QuoteRequest { get; set; }
        public void Copy(AgentAirPortPreference aFilter)
        {
            if (aFilter == null)
                return;

            Stops_Equals_0 = aFilter.Stops_Equals_0;
            Stops_Equals_1 = aFilter.Stops_Equals_1;
            AdultOnly = aFilter.AdultOnly;
            AllInclusive = aFilter.AllInclusive;
            Stars = aFilter.StarRating;
            TripMinBudget = aFilter.TripMinBudget;
            this.TripBudget = aFilter.TripBudget;
            foreach (var lHotel in aFilter.PreferredHotels)
                Accommodations.Add(lHotel);
        }
        public int Stars { get; set; }
        public string Name { get; set; }
        public bool isDefault { get; set; }
        public bool Stops_Equals_0 { get; set; }
        public bool Stops_Equals_1 { get; set; }
        public bool AdultOnly { get; set; }
        public bool AdultsOnlySection { get; set; }
        public bool AllInclusive { get; set; }
        public double PerPersonBudget { get; set; }
        public double TripBudget { get; set; }
        public double TripMinBudget { get; set; }
        public ICollection<AIFilterMAP> OutboundFilters { get; set; }
        public ICollection<AIFilterMAP> InboundFilters { get; set; }
        public ICollection<AIFilterMAP> AirFilters { get; set; }
        public string SelectedLocation { get; set; }

        public void Copy(Filter aFilter)
        {
            Stops_Equals_0 = aFilter.Stops_Equals_0;
            Stops_Equals_1 = aFilter.Stops_Equals_1;
            AdultOnly = aFilter.AdultOnly;
            AllInclusive = aFilter.AllInclusive;
            this.PerPersonBudget = aFilter.PerPersonBudget;
            this.TripBudget = aFilter.TripBudget;
            this.TripMinBudget = aFilter.TripMinBudget;
            AirFilters = aFilter.AirFilters;
            OutboundFilters = aFilter.OutboundFilters;
            InboundFilters = aFilter.InboundFilters;
            SelectedLocation = aFilter.SelectedLocation;
            this.Accommodations = aFilter.Accommodations;
        }
    }
}
