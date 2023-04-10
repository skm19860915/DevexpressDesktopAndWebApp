using BlitzerCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlitzerCore.Business.AIFilters
{
    public class FlightDiffs
    {
        public int FlightID { get; set; }
        public double Duration { get; set; }
    }
    public class Return3ShortestFlights : AIFilter
    {
        public Return3ShortestFlights()
        {
            _AIFilterID = 2;
            _AIName = "Return 3 Shortest Flights";
            Description = "Examine all flights and return the 3 with the shortest duration";
        }
    
        public override IEnumerable<FlightItinerary> Apply(IEnumerable<FlightItinerary> aInput)
        {
            List<FlightDiffs> lDiffs = new List<FlightDiffs>();
            foreach ( var lTicket in aInput)
            {
                var lOutDiff = lTicket.OutBound.End.Subtract(lTicket.OutBound.Start).TotalMinutes;
                var lInDiff = lTicket.InBound.End.Subtract(lTicket.InBound.Start).TotalMinutes;
                lDiffs.Add(new FlightDiffs() { FlightID = lTicket.FlightItineraryId, Duration = lOutDiff + lInDiff });
            }
            var lFilteredIDs = lDiffs.OrderBy(x => x.Duration).Take(3).Select(x => x.FlightID);
            return aInput.Where(x => lFilteredIDs.Contains(x.FlightItineraryId)).ToList();
        }
    }
}
