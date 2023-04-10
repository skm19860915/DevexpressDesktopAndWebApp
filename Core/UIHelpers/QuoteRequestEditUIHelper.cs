using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlitzerCore.Models.UI;
using BlitzerCore.Models;
using BlitzerCore.Helpers;
using BlitzerCore.DataAccess;
using BlitzerCore.Business;
using BlitzerCore.Utilities;

namespace BlitzerCore.UIHelpers
{
    public class QuoteRequestEditUIHelper
    {
        public static List<UIQuoteRequestEdit> Convert(IDbContext aContext, IEnumerable<Quote> aQuotes, Contact aAgent, bool aIncludeExcluded)
        {
            var lOutput = new List<UIQuoteRequestEdit>();
            var lSupplier = "";

            if (aQuotes == null || aQuotes.Count() == 0)
                return new List<UIQuoteRequestEdit>();

            foreach (var lQuote in aQuotes.OrderByDescending (x=>x.Accommodation.Rating).ThenBy(x=>x.Accommodation.Name).ThenBy(x => x.AccommodationRoomType.SortOrder).ThenBy(x => x.Total))
            {
                if (lQuote.Total < 1)
                    continue;

                var lElement = NewQuote(lQuote);
                PopulateFlights(aContext, lQuote, lElement);
                lOutput.Add(lElement);
                if (lSupplier != lElement.SupplierName)
                {
                    lElement.Break = true;
                    if (lQuote.Accommodation != null)
                    {
                        var lHotel = new HotelBusiness(aContext).Get(lQuote.Accommodation.Id);
                        lElement.Supplier = HotelUIHelper.Convert(aContext, lHotel);
                        lElement.SupplierId = lHotel.Id;
                        if (lHotel.CountryId != null)
                            lElement.CountryId = lHotel.CountryId.Value;
                    }
                }
                lSupplier = lElement.SupplierName;
                if (lElement.Supplier != null)
                {
                    if (lElement.Supplier.Page as UIResortPage != null)
                    {
                        if (lElement.Supplier != null && ((UIResortPage)lElement.Supplier.Page).Comparables.Count == 0)
                            ((UIResortPage)lElement.Supplier.Page).OverRideUrl = lElement.Supplier.WebSite;
                    }
                }
            }

            foreach (var lItem in lOutput)
                lItem.Count = lOutput.Count(x => x.SupplierName == lItem.SupplierName);

            return lOutput.ToList();
        }

        private static void PopulateFlights(IDbContext aContext, Quote lQuote, UIQuoteRequestEdit lElement)
        {
            if (lQuote.ResortPrice > 0)
                return;

            List<Flight> lFilteredFlights = null;
            List<Flight> lAllFlights = new QuoteDataAccess(aContext).GetFlights(lQuote.QuoteGroup);
            if (lAllFlights != null && lAllFlights.Count() > 0 && lQuote.QuoteGroup.SelectedQuoteRequestTicketId != null)
                lFilteredFlights = lAllFlights.Where(x => x.Leg.TripTicketId == lQuote.QuoteGroup.SelectedQuoteRequestTicketId).ToList();
            else
                lFilteredFlights = lAllFlights;

            var lOutBounds = lFilteredFlights.Where(x => x.Side == Flight.SIDES.OUTBOUND).OrderBy(x => x.Depart);
            var lBestLeg = SelectBestOutBoundLeg(lOutBounds);
            if (lBestLeg != null)
                ProcessOutboundFlights(lElement, lBestLeg.Flights);
            //ProcessOutboundFlights(lElement, ConvertToFlights(lQuote.Flights.Select(x=>x.OutBound)));

            var lInBounds = lFilteredFlights.Where(x => x.Side == Flight.SIDES.INBOUND).OrderBy(x => x.Depart);
            lBestLeg = SelectBestInBoundLeg(lInBounds);
            if (lBestLeg != null)
                ProcessInboundFlights(lElement, lBestLeg.Flights);
            //ProcessInboundFlights(lElement, ConvertToFlights(lQuote.Flights.Select(x => x.InBound)));
            lElement.QuoteRequest = QuoteRequestUIHelper.Convert(aContext, lQuote.QuoteRequest);
        }

        private static Leg SelectBestOutBoundLeg(IEnumerable<Flight> aOutBounds)
        {
            var lLegs = aOutBounds.Select(x => x.Leg).Distinct().ToList();
            if (lLegs.Count() == 0)
                return null;

            // get the least amount of stops
            var lBestLegs = lLegs.Where(x => x.Stops == 0);
            if (lBestLegs.Count() == 0)
                lBestLegs = lLegs.Where(x => x.Stops == 1);
            if (lBestLegs.Count() == 0)
                lBestLegs = lLegs.Where(x => x.Stops == 2);
            if (lBestLegs.Count() == 0)
                lBestLegs = lLegs;

            var lBestSpan = lBestLegs.Min(x => x.End.Subtract(x.Start));
            var lBestLeg = lLegs.Where(x => x.End.Subtract(x.Start) == lBestSpan).First();
            return lBestLeg;
        }

        private static Leg SelectBestInBoundLeg(IEnumerable<Flight> aInBounds)
        {
            //var lFlights = GetBestOutBoundFlights(aOutBounds);
            var lLegs = aInBounds.Select(x => x.Leg).Distinct();
            if (lLegs.Count() == 0)
                return null;

            // get the least amount of stops
            var lBestLegs = lLegs.Where(x => x.Stops == 0);
            if (lBestLegs.Count() == 0)
                lBestLegs = lLegs.Where(x => x.Stops == 1);
            if (lBestLegs.Count() == 0)
                lBestLegs = lLegs.Where(x => x.Stops == 2);
            if (lBestLegs.Count() == 0)
                lBestLegs = lLegs;

            lBestLegs = GetBestInBoundLegs(lBestLegs.SelectMany(x => x.Flights));
            // Find the Shortest Span
            var lBestSpan = lBestLegs.Min(x => x.End.Subtract(x.Start));
            var lBestLeg = lLegs.Where(x => x.End.Subtract(x.Start) == lBestSpan).First();
            return lBestLeg;
        }

        /// <summary>
        /// Find the Best Leg based on time of day
        /// </summary>
        /// <param name="aOutBounds"></param>
        /// <returns></returns>
        private static IEnumerable<Leg> GetBestInBoundLegs(IEnumerable<Flight> aOutBounds)
        {
            if (aOutBounds.Count() == 0)
                return new List<Leg>();
            var lDate = aOutBounds.First().Arrive;

            DateTime lDesiredTime = new DateTime(lDate.Year, lDate.Month, lDate.Day, 18, 0, 0);
            var lList = aOutBounds.Where(x => x.Arrive > lDesiredTime);
            if (lList.Count() > 0)
                return lList.Select(x => x.Leg).Distinct();

            lDesiredTime = new DateTime(lDate.Year, lDate.Month, lDate.Day, 14, 0, 0);
            lList = aOutBounds.Where(x => x.Arrive > lDesiredTime);
            if (lList.Count() > 0)
                return lList.Select(x => x.Leg).Distinct();

            lDesiredTime = new DateTime(lDate.Year, lDate.Month, lDate.Day, 12, 0, 0);
            lList = aOutBounds.Where(x => x.Arrive > lDesiredTime);
            if (lList.Count() > 0)
                return lList.Select(x => x.Leg).Distinct();

            return aOutBounds.Select(x => x.Leg).Distinct();
        }

        private static List<Flight> ConvertToFlights(IEnumerable<Leg> aLeg)
        {
            List<Flight> lOutput = new List<Flight>();
            foreach (var lLeg in aLeg)
            {
                if (lLeg.Flights == null)
                    continue;

                lOutput.AddRange(lLeg.Flights);
            }
            return lOutput;
        }

        public static void ProcessOutboundFlights(UIQuoteRequestEdit aElement, IEnumerable<Flight> aOutBounds)
        {
            var lFlights = aOutBounds.OrderBy(x => x.Arrive).ToList();
            if (lFlights.Count() == 1)
                ProcessOutboundFlight(aElement, lFlights[0]);
            else if (lFlights.Count() > 1)
                ProcessOutboundFlights(aElement, lFlights[0], lFlights[1]);
        }

        public static void ProcessInboundFlights(UIQuoteRequestEdit aElement, IEnumerable<Flight> aOutBounds)
        {
            var lFlights = aOutBounds.OrderBy(x => x.Arrive).ToList();
            if (lFlights.Count() == 1)
                ProcessInboundFlight(aElement, lFlights[0]);
            else if (lFlights.Count() > 1)
                ProcessInboundFlights(aElement, lFlights[0], lFlights[1]);
        }

        private static void ProcessOutboundFlight(UIQuoteRequestEdit aElement, Flight aOutBound)
        {
            if (aOutBound == null)
                return;

            aElement.Flight_Out_Arrive = DataHelper.GetTimeString(aOutBound.Arrive);
            aElement.Flight_Out_Depart = DataHelper.GetTimeString(aOutBound.Depart);
            aElement.Flight_Out_Date = DataHelper.GetLongDateString(aOutBound.Depart);
            TimeSpan lTS = aOutBound.Arrive.Subtract(aOutBound.Depart);
            if (lTS.Hours > 0 || lTS.Minutes > 0)
                aElement.Flight_Out_Duration = $"{lTS.Hours}h {lTS.Minutes}m";
            else
                aElement.Flight_Out_Duration = "";
            aElement.Flight_Out_Numbers = aOutBound.Carrier + " " + aOutBound.Identifer;
        }
        private static void ProcessInboundFlight(UIQuoteRequestEdit aElement, Flight aFlight)
        {
            if (aFlight == null)
                return;

            aElement.Flight_In_Arrive = DataHelper.GetTimeString(aFlight.Arrive);
            aElement.Flight_In_Depart = DataHelper.GetTimeString(aFlight.Depart);
            aElement.Flight_In_Date = DataHelper.GetLongDateString(aFlight.Depart);
            TimeSpan lTS = aFlight.Arrive.Subtract(aFlight.Depart);
            if (lTS.Hours > 0 || lTS.Minutes > 0)
                aElement.Flight_In_Duration = $"{lTS.Hours}h {lTS.Minutes}m";
            else
                aElement.Flight_In_Duration = "";
            aElement.Flight_In_Numbers = aFlight.Carrier + " " + aFlight.Identifer;
        }
        private static void ProcessOutboundFlights(UIQuoteRequestEdit aElement, Flight aLeg1, Flight aLeg2)
        {
            if (aLeg1 == null || aLeg2 == null)
                return;

            aElement.Flight_Out_Arrive = DataHelper.GetTimeString(aLeg2.Arrive);
            aElement.Flight_Out_Depart = DataHelper.GetTimeString(aLeg1.Depart);
            aElement.Flight_Out_Date = DataHelper.GetLongDateString(aLeg1.Depart);
            TimeSpan lTS = aLeg2.Arrive.Subtract(aLeg1.Depart);
            if (lTS.Hours > 0 || lTS.Minutes > 0)
                aElement.Flight_Out_Duration = $"{lTS.Hours}h {lTS.Minutes}m";
            else
                aElement.Flight_Out_Duration = "";
            lTS = aLeg2.Depart.Subtract(aLeg1.Arrive);
            var lAirPort = aLeg2.DepartAirPort?.Code;
            if (lTS.Hours > 0 || lTS.Minutes > 0)
                aElement.Flight_Out_Layover = $"{lAirPort} {lTS.Hours}h {lTS.Minutes}m";
            else
                aElement.Flight_Out_Layover = "";
            aElement.Flight_Out_Numbers = $"{aLeg1.Carrier} {aLeg1.Identifer},{aLeg2.Carrier} {aLeg2.Identifer}";
        }
        private static void ProcessInboundFlights(UIQuoteRequestEdit aElement, Flight aLeg1, Flight aLeg2)
        {
            if (aLeg1 == null || aLeg2 == null)
                return;

            aElement.Flight_In_Arrive = DataHelper.GetTimeString(aLeg2.Arrive);
            aElement.Flight_In_Depart = DataHelper.GetTimeString(aLeg1.Depart);
            aElement.Flight_In_Date = DataHelper.GetLongDateString(aLeg1.Depart);
            TimeSpan lTS = aLeg2.Arrive.Subtract(aLeg1.Depart);
            if (lTS.Hours > 0 || lTS.Minutes > 0)
                aElement.Flight_In_Duration = $"{lTS.Hours}h {lTS.Minutes}m";
            else
                aElement.Flight_In_Duration = "";
            lTS = aLeg2.Depart.Subtract(aLeg1.Arrive);
            var lAirPort = aLeg2.DepartAirPort?.Code;
            if (lTS.Hours > 0 || lTS.Minutes > 0)
                aElement.Flight_In_Layover = $"{lAirPort} {lTS.Hours}h {lTS.Minutes}m";
            else
                aElement.Flight_In_Layover = "";
            aElement.Flight_In_Numbers = $"{aLeg1.Carrier} {aLeg1.Identifer}, {aLeg2.Carrier} {aLeg2.Identifer}";
        }
        private static UIQuoteRequestEdit NewQuote(Quote aQuote)
        {

            UIQuoteRequestEdit lOutput = new UIQuoteRequestEdit()
            {
                Id = aQuote.QuoteID,
                TourOperator = aQuote.TourOperator?.Name,
                TO_Initials = CompanyBusiness.Initials(aQuote.TourOperator),
                SupplierName = aQuote.Accommodation?.Name,
                RoomType = aQuote.AccommodationRoomType?.Name,
                QuoteRequestId = aQuote.QuoteRequestID,
                Insurance = QuoteBusiness.GetInsuranceText(aQuote.Insurance),
                Transfer = QuoteBusiness.GetTransferText(aQuote.Transfer),
                Total = DataHelper.ConvertToCurrency(aQuote.Total)
            };

            if (aQuote.ResortPrice > 0)
                lOutput.IncludesAir = false;
            else
                lOutput.IncludesAir = true;

            if (aQuote.Accommodation as Hotel != null && ((RoomType)aQuote.AccommodationRoomType).KingOr2Queens)
            {
                lOutput.KingOrQueen = "King or 2 Queens beds can be requested";
                lOutput.WarningNotice = "Bedding type is not guranteed";
            }
            lOutput.HotelStay = $"{(aQuote.QuoteRequest.ReturnDate - aQuote.QuoteRequest.DepartureDate).Days} nights at the resort";

            return lOutput;
        }

    }
}
