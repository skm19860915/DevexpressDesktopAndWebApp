using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Helpers;
using BlitzerCore.Business;
using BlitzerCore.Utilities;
using BlitzerCore.DataAccess;
using BlitzerCore.Models.ASP;

namespace BlitzerCore.UIHelpers
{
    public class QuoteUIHelper
    {
        const string ClassName = "QuoteBusiness::";

        public static UIQuote Convert(IDbContext aContext, Quote aQuote, ASPQuote aUI = null)
        {
            UIQuote lQuote = new BlitzerCore.Models.UI.UIQuote();
            if (aUI != null)
                lQuote = aUI;

            if (aQuote == null)
                return lQuote;

            lQuote.Primary = ContactUIHelper.Convert(aQuote.QuoteRequest.Opportunity.Travelers.Where(x => x.Primary == true).Select(y => y.User).FirstOrDefault());
            lQuote.QuoteID = aQuote.QuoteID;
            lQuote.PackagePrice = DataHelper.ConvertToCurrency(aQuote.PackagePrice);
            lQuote.ResortPrice = DataHelper.ConvertToCurrency(aQuote.ResortPrice);
            lQuote.FlightPrice = DataHelper.ConvertToCurrency(aQuote.FlightPrice);
            lQuote.Adjustment = DataHelper.ConvertToCurrency(aQuote.Adjustment);
            lQuote.Total = aQuote.Total;
            lQuote.SupplierId = aQuote.SupplierId;
            if (aQuote.SupplierId != null)
            {
                lQuote.Supplier = CompanyUIHelper.Convert(aContext, new CompanyBusiness(aContext).Get(aQuote.SupplierId.Value));
                lQuote.SupplierName = lQuote.Supplier.Name;
                lQuote.SupplerRank = lQuote.Supplier.Rating;
            }
            lQuote.Booked = aQuote.Booked;
            lQuote.TourOperatorID = aQuote.TourOperatorID;
            if (aQuote.TourOperatorID != null)
                lQuote.TourOperator = new CompanyBusiness(aContext).Get(aQuote.TourOperatorID.Value)?.Name;
            lQuote.SKUID = aQuote.AccommodationRoomTypeID;
            if ( aQuote.AccommodationRoomType != null )
                lQuote.SortOrder = aQuote.AccommodationRoomType.SortOrder; 
            lQuote.QuoteRequestID = aQuote.QuoteRequestID;
            lQuote.Status = aQuote.Status;
            lQuote.Insurance = aQuote.Insurance;
            lQuote.Transfer = aQuote.Transfer;

            if (aQuote.AccommodationRoomType != null)
                lQuote.SKU = aQuote.AccommodationRoomType.Name;
            if (aQuote.SupplierId > 0)
                lQuote.ResortName = new CompanyDataAccess(aContext).Get(aQuote.SupplierId.Value).Name;
            var lFlights = aQuote.Flights;

            ConvertFlights(aContext, lQuote, aQuote);

            return lQuote;
        }

        protected static void ConvertFlights(IDbContext aContext, UIQuote aOutput, Quote aInput)
        {
            List<Flight> lFlights = new List<Flight>();
            if (aInput.Flights != null)
            {
                var lInBoundFl = aInput.Flights.Where(x=>x.InBound != null && x.InBound.Flights.Count() > 0 ). SelectMany(x =>x.InBound.Flights);
                var lOutBoundFl = aInput.Flights.Where(x => x.OutBound != null && x.OutBound.Flights.Count() > 0).SelectMany(x => x.OutBound.Flights);
                try
                {
                    if (lInBoundFl.Any() )
                            lFlights.AddRange(aInput.Flights.Where(x => x.InBound != null)
                            .SelectMany(x => x.InBound.Flights));
                    if (lOutBoundFl.Any())
                        lFlights.AddRange(aInput.Flights.Where(x => x.OutBound != null)
                            .SelectMany(x => x.OutBound.Flights));
                }
                catch (Exception)
                {
                }
            }

            var lOutBounds = lFlights.Where(x => x.Side == Flight.SIDES.OUTBOUND).OrderBy(x=>x.Depart).ToList();
            if (lOutBounds.Any() )
            {
                var lOutBound = lOutBounds[0];
                aOutput.Out_Leg1_Id = lOutBound.TransportationID;
                aOutput.Out_Leg1_ArriveTime = DataHelper.GetTimeString(lOutBound.Arrive);
                aOutput.Out_Leg1_DepartTime = DataHelper.GetTimeString(lOutBound.Depart);
                aOutput.Out_Leg1_Flight = lOutBound.Identifer;
            }

            if (lOutBounds.Count() > 1)
            {
                var lOutBound = lOutBounds[1];
                aOutput.Out_Leg2_Id = lOutBound.TransportationID;
                aOutput.Out_Leg2_ArriveTime = DataHelper.GetTimeString(lOutBound.Arrive);
                aOutput.Out_Leg2_DepartTime = DataHelper.GetTimeString(lOutBound.Depart);
                aOutput.Out_Leg2_Flight = lOutBound.Identifer;
            }

            var lInBounds = lFlights.Where(x => x.Side == Flight.SIDES.INBOUND).OrderBy(x => x.Depart).ToList();
            if (lInBounds.Any())
            {
                var lInBound = lInBounds[0];
                aOutput.In_Leg1_Id = lInBound.TransportationID;
                aOutput.In_Leg1_ArriveTime = DataHelper.GetTimeString(lInBound.Arrive);
                aOutput.In_Leg1_DepartTime = DataHelper.GetTimeString(lInBound.Depart);
                aOutput.In_Leg1_Flight = lInBound.Identifer;
            }
            if (lInBounds.Count() > 1)
            {
                var lInBound = lInBounds[1];
                aOutput.In_Leg2_Id = lInBound.TransportationID;
                aOutput.In_Leg2_ArriveTime = DataHelper.GetTimeString(lInBound.Arrive);
                aOutput.In_Leg2_DepartTime = DataHelper.GetTimeString(lInBound.Depart);
                aOutput.In_Leg2_Flight = lInBound.Identifer;
            }
        }

        public static IEnumerable<UIQuote> Convert(IDbContext aContext, List<QuoteToResultsMapper> aMap, QuoteGroup aQuoteGroup)
        {
            var lOutput = new List<UIQuote>();
            if (aMap == null || aMap.Count() == 0)
                return lOutput;

            List<QuoteRequestResort> lRequestHotels = new QuoteRequestDataAccess(aContext).GetAll(aMap.First().QuoteGroup);
            List<Company> lCompanies = new CompanyDataAccess(aContext).GetAll();
            foreach (var lMap in aMap)
                lOutput.Add(Convert(aContext, lMap, lRequestHotels, lCompanies, aQuoteGroup));

            return lOutput;
        }

        public static UIQuote Convert(IDbContext aContext, QuoteToResultsMapper aMap, List<QuoteRequestResort> aResorts, List<Company> aCompanies, QuoteGroup aQuoteGroup)
        {
            var lOutput = new UIQuote();
            var lResort = aMap.QuoteRequestResort;
            if (lResort == null)
                return null;

            lOutput.QuoteID = aMap.QuoteRequestResortID.Value * -1;
            lOutput.PackagePrice = DataHelper.ConvertToCurrency(aMap.QuoteRequestResort.Price);
            lOutput.Total = new QuoteBusiness(aContext).ComputePrice( aQuoteGroup, aMap );
            lOutput.SupplierId = aMap.QuoteRequestResort.ResortId;
            lOutput.SupplerRank = aMap.QuoteRequestResort.Resort.Rating;
            lOutput.SupplierName = aMap.QuoteRequestResort.Resort.Name;
            lOutput.SKU = aMap.QuoteRequestResort.ResortRoomType.Name;
            lOutput.SKUID = aMap.QuoteRequestResort.ResortRoomType.SKUID;
            lOutput.SortOrder = aMap.QuoteRequestResort.ResortRoomType.SortOrder;
            if (aMap.QuoteRequestResort.LandOnly)
                lOutput.QuoteType = "Land Only";
            else 
                lOutput.QuoteType = "Package";
            lOutput.ResortName = aMap.QuoteRequestResort.Resort.Name;
            lOutput.Status = Status(aMap.QuoteRequestResort);
            int lResortRID = aMap.QuoteRequestResortID.Value;
            var lQRResort = aResorts.Where(x => x.QuoteRequestResortID == lResortRID);
            if (lQRResort != null && lQRResort.Count() > 0)
            {
                //var lHotel = new QuoteDataAccess(aContext).GetHotelByQuoteRequestResortId(lResortRID);
                //Company lCompany = new CompanyBusiness(aContext).Get(lQRResort.First().Resort.Id);
                var lCompany = aCompanies.Where(x => x.Id == lQRResort.First().Resort.Id);
                if (lCompany == null && lCompany.Count() == 0)
                    Logger.LogWarning("UIQuoteHelper::Convert - Unable to get Resort Page Info for : " + aMap.QuoteRequestResortID.Value);
                else
                {
                    lOutput.Supplier = CompanyUIHelper.Convert(aContext, lCompany.First());
                    lOutput.SupplierName = lOutput.Supplier.Name;
                }
            }
            lOutput.TourOperator = aMap.QuoteRequestResort.TourOperator.Name;
            lOutput.TourOperatorID = aMap.QuoteRequestResort.TourOperatorID;

            return lOutput;
        }

        private static QuoteStatus Status(QuoteRequestResort aQuoteResort)
        {
            if (aQuoteResort.Exclude == true)
                return QuoteStatus.Excluded;

            return QuoteStatus.Ready;
        }

        public static Quote Convert(IDbContext aContext, UIQuote aQuote)
        {
            string FuncName = ClassName + "Convert (UIQuote)";

            if (aQuote == null)
            {
                Logger.LogError(FuncName + " Null UIQuote passed into method");
                return null;
            }

            Quote lQuote = new QuoteBusiness(aContext).Get(aQuote.QuoteID);
            if (lQuote == null)
                lQuote = new Quote();

            lQuote.Status = aQuote.Status;
            lQuote.SupplierId = aQuote.SupplierId;
            lQuote.AccommodationRoomTypeID = aQuote.SKUID;
            lQuote.TourOperatorID = aQuote.TourOperatorID;
            lQuote.Booked = aQuote.Booked;
            // Have to be carefull here.  If no value come back from the GUI, don't overwrite
            // with 0 when there was no value sent from the GUI
            if (aQuote.PackagePrice != null)
                lQuote.PackagePrice = DataHelper.ConvertFromCurrency(aQuote.PackagePrice);
            if (aQuote.ResortPrice != null)
                lQuote.ResortPrice = DataHelper.ConvertFromCurrency(aQuote.ResortPrice);
            if (aQuote.FlightPrice != null)
                lQuote.FlightPrice = DataHelper.ConvertFromCurrency(aQuote.FlightPrice);
            if (aQuote.Adjustment != null)
                lQuote.Adjustment = DataHelper.ConvertFromCurrency(aQuote.Adjustment);
            lQuote.QuoteRequestID = aQuote.QuoteRequestID;
            lQuote.Insurance = aQuote.Insurance;
            lQuote.Transfer = aQuote.Transfer;

            lQuote.Flights = ConvertFlights(aContext, aQuote);

            return lQuote;
        }

        private static List<FlightItinerary> ConvertFlights(IDbContext aContext, UIQuote aQuote)
        {
            QuoteRequest lQuoteRequest = new QuoteRequestBusiness(aContext).Get(aQuote.QuoteRequestID);
            var lQuoteGroup = new QuoteRequestBusiness(aContext).GetOpenQuoteGroup(lQuoteRequest);
            List<FlightItinerary> lOutput = new List<FlightItinerary>();
            List<Staging.Flight> lInput = new List<Staging.Flight>();

            var lOutConnection = lQuoteRequest.DestinationAirPort.Code;
            if (aQuote.Out_ConnectionAirport != null && aQuote.Out_ConnectionAirport.Length > 0)
            {
                var lAirPort = new AirBusiness(aContext).GetAirPort(aQuote.Out_ConnectionAirport);
                if (lAirPort != null)
                    lOutConnection = lAirPort.Code;
            }

            int? lTOID = null;
            if (aQuote.TourOperatorID != null)
            {
                lTOID = aQuote.TourOperatorID.Value;

                Staging.Flight lOutbound = new Staging.Flight()
                {
                    LegGUID = Guid.NewGuid(),
                    QuoteGroupId = lQuoteGroup.Id,
                    DepartDate = lQuoteRequest.DepartureDate.ToShortDateString(),
                    DepartLocation = lQuoteRequest.DepartureAirPort.Code,
                    ArrivalLocation = lOutConnection,
                    ArrivalDate = lQuoteRequest.DepartureDate.ToShortDateString(),
                    TourOperatorID = lTOID.Value,
                    PullType = Staging.Flight.PullTypes.Manual
                };
                lOutbound.Aircraft = aQuote.Out_Leg1_Flight;
                lOutbound.FlightStagingID = aQuote.Out_Leg1_Id;
                lOutbound.DepartTime = GetTime(aQuote.Out_Leg1_DepartTime);
                lOutbound.ArrivalTime = GetTime(aQuote.Out_Leg1_ArriveTime);
                lOutbound.FlightStagingID = aQuote.Out_Leg1_Id;
                lOutbound.TourOperatorID = aQuote.TourOperatorID.Value;
                lOutbound.Side = Staging.Flight.SIDES.DEPARTURE;
                lInput.Add(lOutbound);
                if (aQuote.Out_ConnectionAirport != null && aQuote.Out_ConnectionAirport.Length > 0
                    && aQuote.Out_Leg2_DepartTime != null && aQuote.Out_Leg2_DepartTime.Length > 0
                    && aQuote.Out_Leg2_ArriveTime != null && aQuote.Out_Leg2_ArriveTime.Length > 0)
                {
                    lOutbound = new Staging.Flight()
                    {
                        LegGUID = Guid.NewGuid(),
                        QuoteGroupId = aQuote.QuoteRequestID,
                        DepartDate = lQuoteRequest.DepartureDate.ToShortDateString(),
                        DepartLocation = lOutConnection,
                        ArrivalLocation = lQuoteRequest.DestinationAirPort.Code,
                        ArrivalDate = lQuoteRequest.DepartureDate.ToShortDateString(),
                        TourOperatorID = lTOID.Value,
                        PullType = Staging.Flight.PullTypes.Manual
                    };
                    lOutbound.Aircraft = aQuote.Out_Leg2_Flight;
                    lOutbound.FlightStagingID = aQuote.Out_Leg2_Id;
                    lOutbound.DepartTime = GetTime(aQuote.Out_Leg2_DepartTime);
                    lOutbound.ArrivalTime = GetTime(aQuote.Out_Leg2_ArriveTime);
                    lOutbound.FlightStagingID = aQuote.Out_Leg2_Id;
                    lOutbound.Side = Staging.Flight.SIDES.DEPARTURE;
                    lInput.Add(lOutbound);
                }

                Staging.Flight lInbound = new Staging.Flight()
                {
                    LegGUID = Guid.NewGuid(),
                    QuoteGroupId = lQuoteGroup.Id,
                    QuoteGroup = lQuoteGroup,
                    DepartDate = lQuoteRequest.ReturnDate.ToShortDateString(),
                    DepartLocation = lQuoteRequest.DestinationAirPort.Code,
                    ArrivalLocation = lQuoteRequest.DepartureAirPort.Code,
                    ArrivalDate = lQuoteRequest.ReturnDate.ToShortDateString(),
                    TourOperatorID = lTOID.Value,
                    PullType = Staging.Flight.PullTypes.Manual
                };
                lInbound.Aircraft = aQuote.In_Leg1_Flight;
                lInbound.FlightStagingID = aQuote.In_Leg1_Id;
                lInbound.DepartTime = GetTime(aQuote.In_Leg1_DepartTime);
                lInbound.ArrivalTime = GetTime(aQuote.In_Leg1_ArriveTime);
                lInbound.FlightStagingID = aQuote.In_Leg1_Id;
                lInbound.TourOperatorID = aQuote.TourOperatorID.Value;
                lInbound.Side = Staging.Flight.SIDES.RETURN;
                lInput.Add(lInbound);
                if (aQuote.In_ConnectionAirport != null && aQuote.In_ConnectionAirport.Length > 0
                    && aQuote.In_Leg2_DepartTime != null && aQuote.In_Leg2_DepartTime.Length > 0
                    && aQuote.In_Leg2_ArriveTime != null && aQuote.In_Leg2_ArriveTime.Length > 0)
                {
                    lInbound = new Staging.Flight()
                    {
                        LegGUID = Guid.NewGuid(),
                        QuoteGroupId = lQuoteGroup.Id,
                        QuoteGroup = lQuoteGroup,
                        DepartDate = lQuoteRequest.ReturnDate.ToShortDateString(),
                        DepartLocation = lQuoteRequest.DestinationAirPort.Code,
                        ArrivalLocation = lQuoteRequest.DepartureAirPort.Code,
                        ArrivalDate = lQuoteRequest.ReturnDate.ToShortDateString(),
                        TourOperatorID = lTOID.Value,
                        PullType = Staging.Flight.PullTypes.Manual
                    };
                    lInbound.Aircraft = aQuote.In_Leg2_Flight;
                    lInbound.DepartTime = GetTime(aQuote.In_Leg2_DepartTime);
                    lInbound.ArrivalTime = GetTime(aQuote.In_Leg2_ArriveTime);
                    lInbound.FlightStagingID = aQuote.In_Leg2_Id;
                    lInbound.Side = Staging.Flight.SIDES.RETURN;
                    lInput.Add(lInbound);
                }
            }
            return new AirBusiness(aContext).Convert(lInput, lQuoteGroup);
        }

        private static string GetDate(string aInput)
        {
            return "Dec 12, 2010";
        }

        public static string GetTime(string aInput)
        {
            if (aInput == null)
                return "";

            var lInput = aInput.Replace(":", "");
            int lMins = 0;
            int lHours = 0;
            string lSuffix = "AM";

            if (aInput.ToUpper().Contains("AM") || aInput.ToUpper().Contains("PM"))
            {
                lInput = lInput.Substring(0, lInput.Length - 2);
                lInput = lInput.Trim();
            }

            if (lInput.Length == 3)
            {

                lMins = int.Parse(lInput.Substring(1, 2));
                lHours = int.Parse(lInput.Substring(0, 1));
                lSuffix = "AM";
                if (aInput.ToLower().Contains("pm"))
                    lSuffix = "PM";
            }
            else
            {

                lMins = int.Parse(lInput.Substring(2, 2));
                lHours = int.Parse(lInput.Substring(0, 2));
                if (lHours > 11)
                {
                    lSuffix = "PM";
                    lHours -= 12;
                }
            }

            var lResult = $"{lHours}:{lMins} {lSuffix}";
            return lResult;
        }

        private static List<BlitzerCore.Models.UI.AirQuote> GetAirLineTickets(List<BlitzerCore.Models.QuoteToResultsMapper> aData)
        {
            var lTickets = aData.Where(x => x.FlightItineraryId != null).Select(x => x.FlightItinerary);
            return Convert(lTickets);
        }

        //private static List<BlitzerCore.Models.UI.ResortQuote> GetHotels(List<BlitzerCore.Models.QuoteToResultsMapper> aData)
        //{
        //    var lHotels = aData.Where(x => x.QuoteRequestResortID != null).Select(x => x.QuoteRequestResort).OrderBy(x => x.Accommodation.Name).ThenBy(x => x.Price);
        //    return Convert(lHotels);
        //}

        public static string DisplayDateTime(DateTime aDT)
        {
            return aDT.ToString("F");
        }

        public static string DisplayDate(DateTime aDT)
        {
            return aDT.ToString("MMM dd");
        }

        public static string DisplayTime(DateTime aDT)
        {
            return aDT.ToString("h:mm tt");
        }

        public static List<BlitzerCore.Models.UI.AirQuote> Convert(IEnumerable<FlightItinerary> aTickets)
        {
            var lResults = new List<BlitzerCore.Models.UI.AirQuote>();

            foreach (var lTicket in aTickets)
            {
                var lAirQuote = new BlitzerCore.Models.UI.AirQuote();
                lAirQuote.TicketID = lTicket.FlightItineraryId;
                if (lTicket.OutBound.Start != Defines.DATETIME_ERROR)
                    lAirQuote.OutBoundDepartDate = DisplayDate(lTicket.OutBound.Start);
                else
                    lAirQuote.OutBoundDepartDate = "Error";

                if (lTicket.OutBound.Start != Defines.DATETIME_ERROR)
                    lAirQuote.OutBoundDepartTime = DisplayTime(lTicket.OutBound.Start);
                else
                    lAirQuote.OutBoundDepartTime = "Error";

                if (lTicket.InBound.Start != Defines.DATETIME_ERROR)
                    lAirQuote.InBoundDepartDate = DisplayDate(lTicket.InBound.Start);
                else
                    lAirQuote.InBoundDepartDate = "Error";

                if (lTicket.InBound.Start != Defines.DATETIME_ERROR)
                    lAirQuote.InBoundDepartTime = DisplayTime(lTicket.InBound.Start);
                else
                    lAirQuote.InBoundDepartTime = "Error";

                if (lTicket.OutBound.Stops > 0 && lTicket.OutBound.Flights[0].DepartAirPortID != lTicket.OutBound.Flights[1].ArrivalAirPortID)
                {
                    if (lTicket.OutBound.Flights[0].ArrivalAirPort != null)
                        if (lTicket.OutBound.Flights[0].ArrivalAirPort.City != null && lTicket.OutBound.Flights[0].ArrivalAirPort.City.Length > 0)
                            lAirQuote.OutBoundConnectionCity = lTicket.OutBound.Flights[0].ArrivalAirPort.City;
                        else
                            lAirQuote.OutBoundConnectionCity = lTicket.OutBound.Flights[0].ArrivalAirPort.Name;
                    else
                    {
                        lAirQuote.OutBoundConnectionCity = "Error";
                        Logger.LogError("Failed to find OutboundConnection city for AirPortID" + lTicket.OutBound.Flights[0].ArrivalAirPortID);
                    }
                    lAirQuote.OutBoundConnectionArrivalTime = DisplayTime(lTicket.OutBound.Flights[0].Arrive);

                    var lOutDiff = lTicket.OutBound.Flights[1].Depart.Subtract(lTicket.OutBound.Flights[0].Arrive);
                    lAirQuote.OutBoundLayOverDuration = lOutDiff.Hours + " Hours " + lOutDiff.Minutes + " Minutes";

                }
                if (lTicket.InBound.Stops > 0 && lTicket.InBound.Flights[0].DepartAirPortID != lTicket.InBound.Flights[1].ArrivalAirPortID)
                {
                    if (lTicket.InBound.Flights[0].ArrivalAirPort.City != null && lTicket.InBound.Flights[0].ArrivalAirPort.City.Length > 0)
                        lAirQuote.InBoundConnectionCity = lTicket.InBound.Flights[0].ArrivalAirPort.City;
                    else
                        lAirQuote.InBoundConnectionCity = lTicket.InBound.Flights[0].ArrivalAirPort.Name;
                    lAirQuote.InBoundConnectionArrivalTime = DisplayTime(lTicket.InBound.Flights[0].Arrive);

                    var lInDiff = lTicket.InBound.Flights[1].Depart.Subtract(lTicket.InBound.Flights[0].Arrive);
                    lAirQuote.InBoundLayOverDuration = lInDiff.Hours + " Hours " + lInDiff.Minutes + " Minutes";
                }

                if (lTicket.OutBound.Start != Defines.DATETIME_ERROR)
                    lAirQuote.OutBoundArriveTime = DisplayTime(lTicket.OutBound.End);
                else
                    lAirQuote.OutBoundArriveTime = "Error";

                if (lTicket.InBound.End != Defines.DATETIME_ERROR)
                    lAirQuote.InBoundArriveTime = DisplayTime(lTicket.InBound.End);
                else
                    lAirQuote.InBoundArriveTime = "Error";

                if (lAirQuote.OutBoundConnectionCity == null || lAirQuote.OutBoundConnectionCity.Length == 0)
                    lAirQuote.OutBoundConnectionCity = "Non-Stop";
                if (lAirQuote.InBoundConnectionCity == null || lAirQuote.InBoundConnectionCity.Length == 0)
                    lAirQuote.InBoundConnectionCity = "Non-Stop";

                //lAirQuote.OutBoundDetails = lTicket.OutBound.Stops +  " Stops Length "+ lOutDiff.Hours + " Hours "+lOutDiff.Minutes+" Minutes";
                //lAirQuote.InBoundDetails = lTicket.InBound.Stops + "Stops Length " + lInDiff.Hours + " Hours " + lInDiff.Minutes + " Minutes"; 
                lResults.Add(lAirQuote);
            }

            return lResults;
        }

        //public static List<BlitzerCore.Models.UI.ResortQuote> Convert(IEnumerable<Quote> aQuote)
        //{
        //    List<BlitzerCore.Models.UI.ResortQuote> lQuotes = new List<BlitzerCore.Models.UI.ResortQuote>();
        //    BlitzerCore.Models.UI.ResortQuote lPreviousHotel = new BlitzerCore.Models.UI.ResortQuote();

        //    foreach (QuoteRequestResort lQuote in aQuote)
        //    {
        //        if (lPreviousHotel.AccommodiationID != lQuote.AccommodationID)
        //        {
        //            lPreviousHotel.RoomQuotes = lPreviousHotel.RoomQuotes.OrderBy(x => x.Price).ToList();
        //            lPreviousHotel = new BlitzerCore.Models.UI.ResortQuote();
        //            lPreviousHotel.Import(lQuote.Accommodation);
        //            lPreviousHotel.RoomQuotes.Clear();
        //            lQuotes.Add(lPreviousHotel);
        //        }

        //        if (lQuote.AccommodationID == lPreviousHotel.AccommodiationID)
        //            lPreviousHotel.RoomQuotes.Add(new BlitzerCore.Models.UI.RoomQuote(lQuote));
        //    }

        //    //return lQuotes;
        //}

        public static List<UIQuote> Convert(IDbContext aContext, ICollection<Quote> aQuotes)
        {
            var lOutput = new List<UIQuote>();
            if (aQuotes == null || aQuotes.Count() == 0)
                return lOutput;

            foreach (var lQuote in aQuotes)
                lOutput.Add(Convert(aContext, lQuote));
            return lOutput;
        }

        public static List<Quote> Convert(IDbContext aContext, ICollection<UIQuote> aQuotes)
        {
            var lOutput = new List<Quote>();
            if (aQuotes == null)
                return lOutput;

            foreach (var lQuote in aQuotes)
                lOutput.Add(Convert(aContext, lQuote));
            return lOutput;
        }

    }
}
