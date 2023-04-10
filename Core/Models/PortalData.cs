using System;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models.UI;

namespace BlitzerCore.Models
{
    public class PortalData
    {
        public List<UIOpportunity> Opportunities { get; set; }
        public List<UITrip> Trips { get; set; }
        public List<UITask> Tasks { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsClient { get; set; }
        public string SearchText { get; set; }
        public int NewBookings { get; set; }
        public int WonBookings { get; set; }
    }
}
