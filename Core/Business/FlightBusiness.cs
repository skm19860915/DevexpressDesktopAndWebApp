using System;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models;

namespace BlitzerCore.Business
{
    public class FlightBusiness
    {
        public static List<Flight> Convert (List<FlightItinerary> aTickets)
        {
            var lOutput = new List<Flight>();
            foreach (var lTicket in aTickets)
                lOutput.Add(Convert(lTicket));

            return lOutput;
        }

        public static Flight Convert(FlightItinerary aTicket)
        {
            var lArriveFlight = GetArrivalAirPort(aTicket);
            var lDepartFlight = GetDepartAirPort(aTicket);
            var lFlight = new Flight() { ArrivalAirPort = lArriveFlight.ArrivalAirPort,
             Arrive = lArriveFlight.Arrive,
             ArrivalAirPortID = lArriveFlight.ArrivalAirPortID,
             Carrier = lArriveFlight.Carrier,
             Depart = lDepartFlight.Depart,
             DepartAirPort = lDepartFlight.DepartAirPort,
             DepartAirPortID = lDepartFlight.DepartAirPortID,
             Identifer = ""};
            return lFlight;
        }

        private static Flight GetArrivalAirPort (FlightItinerary aTicket )
        {
            return aTicket.InBound.Flights[aTicket.InBound.Flights.Count - 1];
        }
        private static Flight GetDepartAirPort(FlightItinerary aTicket)
        {
            return aTicket.InBound.Flights[0];
        }
    }
}
