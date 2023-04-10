using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace BlitzerCore.Models.UI
{
    [NotMapped]
    public class UIFilter
    {
        public UIFilter ()
        {
            SelectedAccommondations = new List<int>();
        }
        public int FilterID { get; set; }
        public int QuoteGroupID { get; set; }
        public int QuoteRequestID { get; set; }
        public List<Lookup> Accommondations { get; set; }
        public string SelectedStars { get; set; }
        public List<SelectListItem> Stars { get; set; }
        public string SelectedLocation { get; set; }
        public List<SelectListItem> Locations { get; set; }
        public List<int> SelectedAccommondations { get; set; }
        public bool Stops_Equals_0 { get; set; }
        public bool Stops_Equals_1 { get; set; }
        public bool AdultOnly { get; set; }
        public bool AdultsOnlySection { get; set; }
        public bool AllInclusive { get; set; }
        public string PerPersonBudget { get; set; }
        public string TripMinBudget { get; set; }
        public string TripBudget { get; set; }
    }
}
