using System;
using System.Collections.Generic;
using System.Text;

namespace BlitzerCore.Models.UI
{
    public class UIOpportunityGrid
    {
        public UIOpportunityGrid()
        {
            RelationShips = new List<Lookup>();
            AirPortCodes = new List<Lookup>();
        }
        public int OpportunityID { get; set; }
        public string OutBoundAirport { get; set; }
        public string InBoundAirPort { get; set; }
        public string OutBoundDate { get; set; }
        public string InBoundDate { get; set; }
        public string AgentID { get; set; }
        public string Notes { get; set; }
        public bool AllInclusive { get; set; }
        public bool HotelOnly { get; set; }
        public bool Cruise { get; set; }
        public string PerPersonBudget { get; set; }
        public string TripBudget { get; set; }
        public string Name { get; set; }
        public List<Lookup> RelationShips { get; set; }
        public List<Lookup> AirPortCodes { get; set; }
    }
}
