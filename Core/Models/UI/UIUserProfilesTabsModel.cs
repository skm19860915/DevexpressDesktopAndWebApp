using System;
using System.Collections.Generic;
using System.Text;

namespace BlitzerCore.Models.UI
{
    [System.ComponentModel.DataAnnotations.Schema.NotMapped]
    public class UIUserProfilesTabsModel
    {
        public UIUserProfilesTabsModel()
        {
            TravelerIDs = new List<string>();
            TravelerNames = new List<string>();
        }
        public int TripID { get; set; }
        public string ActiveClient { get; set; }
        public List<string> TravelerIDs { get; set; }
        public List<string> TravelerNames { get; set; }
    }
}
