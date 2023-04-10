using System.Threading.Tasks;
using BlitzerCore.Models;
using BlitzerCore.Utilities;
using BlitzerCore.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace BlitzerCore.Business
{
    public class AirBusiness
    {
        readonly IDbContext mContext;
        readonly IReportEmailer mReportEmailer;
        List<AirPort> AirPorts { get; set; }
        const string ClassName = "AirBusiness::";


        public AirBusiness(IDbContext aContext, IReportEmailer aReportEmailer = null)
        {
            mContext = aContext;
            mReportEmailer = aReportEmailer;
        }

        public List<FlightItinerary> Convert (List<Staging.Flight> aFlights, QuoteGroup aQuoteGroup )
        {
            const string FUNCNAME = ClassName + "Convert(List<Staging.Flight>)";

            var lResults = new List<FlightItinerary>();
            var lTempTicket = new OneWayTicket();
            var lFlightItins = aFlights.Select(x => x.ItineraryGUID).Distinct();
            var lStagedFlights = aFlights.OrderBy(x => x.LegGUID);
            Guid NULLGUID = new Guid();
            var lPrevLegGUID = NULLGUID;
            AirPorts = new AirPortDataAccess(mContext).GetAll().ToList();

            // ReSharper disable once InconsistentNaming
            foreach ( var lFlightItinGUIDs in lFlightItins)
            {
                try
                {
                    var lItinFlights = aFlights.Where(x => x.ItineraryGUID == lFlightItinGUIDs);

                    // this is the first trip
                    var lFlightItin = new FlightItinerary
                    {
                        OutBound = ProcessLeg(Convert(lItinFlights.Where(x => x.Side == Staging.Flight.SIDES.DEPARTURE).ToList()), aQuoteGroup),
                        InBound = ProcessLeg(Convert(lItinFlights.Where(x => x.Side == Staging.Flight.SIDES.RETURN).ToList()), aQuoteGroup)
                    };

                    lFlightItin.TourOperatorId = lFlightItin.OutBound.Flights.Select(x => x.TourOperatorId).First();
                    lFlightItin.ExtraCost = ConvertPrice(lItinFlights.First().ExtraCost);
                    if (lFlightItin.OutBound.Flights != null && lFlightItin.OutBound.Flights.Any() && 
                        (lFlightItin.InBound.Flights == null || !lFlightItin.InBound.Flights.Any()))
                        Logger.LogError($"Outbound List has flights {lFlightItin.OutBound.Flights[0].Carrier}{lFlightItin.OutBound.Flights[0].Identifer} but inbound doesn't, should never happen");
                    else if (lFlightItin.InBound.Flights != null && lFlightItin.InBound.Flights.Any() && 
                        (lFlightItin.OutBound.Flights == null || !lFlightItin.OutBound.Flights.Any() ))
                        Logger.LogError($"Inbound List has flights{lFlightItin.InBound.Flights[0].Carrier}{lFlightItin.InBound.Flights[0].Identifer}  but outbound doesn't, should never happen");
                    else
                        lResults.Add(lFlightItin);
                } catch ( Exception e )
                {
                    Logger.LogException(FUNCNAME, e);
                }
            }

            return lResults;
        }

        public List<SKU> GetProducts(Company aAirLine)
        {
            return new SKUDataAccess(mContext).GetSKUs(aAirLine);
        }

        private double ConvertPrice(string aExtraCost)
        {
            if (aExtraCost == null)
                return 0;

            var lExtraCost = aExtraCost.Replace(" pp", "");
            if (double.TryParse(lExtraCost, out double lTempCost))
                return lTempCost;

            return 0;
        }

        private Leg ProcessLeg(List<Flight> aFlights, QuoteGroup aGroup)
        {
            Leg lLeg = new Leg();
            if (aFlights != null &&  aFlights.Count() > 0)
            {
                lLeg.Start = aFlights.Min(x => x.Depart);
                lLeg.End = aFlights.Max(x => x.Arrive);
                lLeg.Flights = aFlights;
                lLeg.Stops = lLeg.Flights.Count() - 1;
            } else
            {
                lLeg.Start = Defines.DATETIME_ERROR;
                lLeg.End = Defines.DATETIME_ERROR;
            }
            lLeg.QuoteGroup = aGroup;
            var lTourIDs = aFlights.Select(x => x.TourOperatorId);
            if (lTourIDs.Count() > 0)
                lLeg.TourOperatorId = lTourIDs.First();

            return lLeg;
        }

        private List<Flight> Convert(IEnumerable<Staging.Flight> aStagingFlights)
        {
            var lFlights = new List<Flight>();
            foreach (var lFlight in aStagingFlights)
                lFlights.Add(Convert(lFlight));
            return lFlights;
        }

        private Flight Convert(Staging.Flight lStagingFlight)
        {
            const string FuncName = ClassName + "Convert(Flight)";
            var lFlight = new Flight();
            try
            {
                // The Arrival Date comes in as null if the same as the DepartDate
                if (lStagingFlight.ArrivalDate == null)
                    lStagingFlight.ArrivalDate = lStagingFlight.DepartDate;

                string lsDepart = lStagingFlight.DepartDate.Trim() + " " + lStagingFlight.DepartTime.Trim();
                string lsArrive = lStagingFlight.ArrivalDate.Trim() + " " + lStagingFlight.ArrivalTime.Trim();

                DateTime lDate = DateTime.Now;
                bool lStatus = DateTime.TryParse(lsDepart, out lDate);
                if (lStatus)
                    lFlight.Depart = lDate;
                else
                {
                    Logger.LogError("Failed to Part DepartTime[" + lsDepart + "] for QuoteGroup[" + lStagingFlight.QuoteGroupId + "]");
                    return null;
                }

                lStatus = DateTime.TryParse(lsArrive, out lDate);
                if (lStatus)
                    lFlight.Arrive = lDate;
                else
                {
                    Logger.LogError("Failed to Part ArrivalTime[" + lsArrive + "] for QuoteGroup[" + lStagingFlight.QuoteGroupId + "]");
                    return null;
                }

                if ( lStagingFlight.PullType == Staging.Flight.PullTypes.Manual)
                    lFlight.TransportationID = lStagingFlight.FlightStagingID;

                if (lStagingFlight.Side == Staging.Flight.SIDES.DEPARTURE)
                    lFlight.Side = Flight.SIDES.OUTBOUND;
                else
                    lFlight.Side = Flight.SIDES.INBOUND;

                lFlight.ArrivalAirPort = GetAirPort(lStagingFlight.ArrivalLocation);
                if ( lFlight.ArrivalAirPort == null )
                {
                    string lError = $"{FuncName}Arrival Airport must be supplied to create quote";
                    Logger.LogError(lError);
                    throw new InvalidDataException(lError);
                }
                lFlight.ArrivalAirPortID = lFlight.ArrivalAirPort.AirPortID;
                if (lFlight.ArrivalAirPortID == null)
                    Logger.LogError("Arrival Airport ID can't be null");

                lFlight.DepartAirPort = GetAirPort(lStagingFlight.DepartLocation);
                if (lFlight.DepartAirPort == null)
                {
                    string lError = $"{FuncName}Depart Airport must be supplied to create quote";
                    Logger.LogError(lError);
                    throw new InvalidDataException(lError);
                }
                lFlight.DepartAirPortID = lFlight.DepartAirPort.AirPortID;
                lFlight.Carrier = lStagingFlight.Carrier;
                lFlight.Identifer = lStagingFlight.Aircraft;
                lFlight.TourOperatorId = lStagingFlight.TourOperatorID;
            }
            catch ( Exception e )
            {
                Logger.LogException("Error converting StagingFlight to Prod " + lFlight, e);
                throw e;
            }
            
            return lFlight;
        }


        public AirPort GetDefaultAirPort()
        {
            var lDA = new AirPortDataAccess(mContext);
            return lDA.GetAll().FirstOrDefault(x => x.Default == true);
        }

        public AirPort GetAirPort(string aAirPort)
        {
            if (aAirPort == null)
                return null;

            var lDA = new AirPortDataAccess(mContext);
            int lStart = aAirPort.IndexOf("(") + 1;
            const int lLength = 3;
            string lAirPortCode = aAirPort.Substring(lStart, lLength);
            if (AirPorts == null)
                AirPorts = lDA.GetAll().ToList();
            var lAirPort = AirPorts.Where(x => x.Code != null && x.Code.ToUpper() == lAirPortCode.ToUpper()).FirstOrDefault();
            if (lAirPort == null)
            {
                lAirPort = new AirPort() { Code = lAirPortCode, Name = aAirPort };
                lDA.Save(lAirPort);
            }

            return lAirPort;
        }
    }
}
