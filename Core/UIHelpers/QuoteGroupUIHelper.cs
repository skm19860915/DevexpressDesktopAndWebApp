using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Helpers;
using BlitzerCore.Business;
using BlitzerCore.DataAccess;
using BlitzerCore.Utilities;
using System.IO;

namespace BlitzerCore.UIHelpers
{
    public class QuoteGroupUIHelper
    {
        public static UIQuoteGroup Convert(IDbContext aContext, QuoteGroup aQuoteGroup, bool aDeepCopy = false)
        {
            var lOutput = new UIQuoteGroup();
            lOutput.Id = aQuoteGroup.Id;
            lOutput.GUID = aQuoteGroup.GUID;
            if (aDeepCopy)
                lOutput.QuoteRequest = QuoteRequestUIHelper.Convert(aContext, aQuoteGroup.QuoteRequest, aDeepCopy);
            else
                lOutput.QuoteRequest = QuoteRequestUIHelper.Convert(aContext, aQuoteGroup.QuoteRequest, false);
            lOutput.QuoteRequestID = aQuoteGroup.QuoteRequestID;
            lOutput.OpportunityID = aQuoteGroup.QuoteRequest.OpportunityID;
            var lFilter = new QuoteGroupDataAccess(aContext).GetFilter(aQuoteGroup);
            if (lFilter != null)
                lOutput.FilterId = lFilter.FilterID;
            lOutput.Contacts = lOutput.QuoteRequest.Contacts;
            lOutput.Note = aQuoteGroup.Note;
            lOutput.SentDate = DataHelper.GetDateTimeString(aQuoteGroup.SentDate);
            lOutput.Status = aQuoteGroup.Status;
            lOutput.Agent = ContactUIHelper.Convert(aQuoteGroup.QuoteRequest.Agent);
            var lFilterQuotes = aQuoteGroup.Quotes.GroupBy(x => x.AccommodationRoomType).Select(x => x.OrderBy(y => y.Total)).Select(x => x.First()).ToList();
            var lRawQuote = QuoteUIHelper.Convert(aContext, lFilterQuotes).OrderBy(x => x.Supplier).ToList();
            var lBotResorts = aQuoteGroup.BotQuotes.Where(x => x.QuoteRequestResort != null)
                .OrderBy(x => x.QuoteRequestResort.Price).ToList();
            if (aQuoteGroup.SelectedQuoteRequestTicketId != null)
                aQuoteGroup.SelectedQuoteRequestTicket = new QuoteDataAccess(aContext).GetTicket(aQuoteGroup.SelectedQuoteRequestTicketId.Value);
            var lBotFilterQuotes = lBotResorts.GroupBy(x => new { x.QuoteRequestResort.ResortRoomTypeID, x.QuoteRequestResort.LandOnly } ).Select(x => x.OrderBy(y => y.QuoteRequestResort.Price)).Select(x => x.First()).ToList();
            lRawQuote.AddRange(QuoteUIHelper.Convert(aContext, lBotFilterQuotes, aQuoteGroup));
            lOutput.Quotes = BuildDic(lRawQuote);
            var lConvertedQuotes = lOutput.Quotes.SelectMany(x => x.Value).OrderByDescending(x => x.SupplerRank).ThenBy(x1 => x1.SupplierName).ThenBy(x4=>x4.Total);
            lOutput.NoData = aQuoteGroup.Quotes.Any() == false && lRawQuote.Count() == 0 ;

            // Filter the Quotes for the selectedFlight
            if ( aQuoteGroup.SelectedQuoteRequestTicketId != null )
            {
                var lSelectedTicket = new QuoteDataAccess(aContext).GetTicket(aQuoteGroup.SelectedQuoteRequestTicketId.Value);
                if (lSelectedTicket != null)
                    lOutput.QuoteList = lConvertedQuotes.Where(x => x.TourOperatorID == lSelectedTicket.TourOperatorId).ToList();
                else
                {
                    aQuoteGroup.SelectedQuoteRequestTicketId = null;
                    lOutput.QuoteList = lConvertedQuotes.ToList();
                }
            } else
                lOutput.QuoteList = lConvertedQuotes.ToList();

            return lOutput;
        }

        private static Dictionary<UICompany, List<UIQuote>> BuildDic(List<UIQuote> aQuotes)
        {
            List<UIQuote> lQuotes = null;
            if (aQuotes.Any(x => x.Supplier == null) == false)
                lQuotes = aQuotes.OrderBy(x => x.Supplier.Name).ThenBy(x => x.SortOrder).ToList();
            else
                lQuotes = aQuotes;
            Dictionary<UICompany, List<UIQuote>> lOutput = new Dictionary<UICompany, List<UIQuote>>();

            if (aQuotes == null || aQuotes.Count() == 0)
                return lOutput;

            int lSupplier = 0;
            List<UIQuote> lQuoteList = null;
            foreach (var lQuote in lQuotes)
            {
                if (lQuote.Supplier == null || lSupplier != lQuote.Supplier.Id)
                {
                    lQuoteList = new List<UIQuote>();
                    if (lQuote.Supplier == null)
                        lQuote.Supplier = new UICompany() { Id = 0, Name = "No Supplier" };
                    lOutput.Add(lQuote.Supplier, lQuoteList);
                }

                lQuoteList.Add(lQuote);
                lSupplier = lQuote.Supplier.Id;
            }
            return lOutput;
        }

        public static QuoteGroup Convert(IDbContext aContext, UIQuoteGroup aQuoteGroup)
        {
            var lOutput = new QuoteGroupBusiness(aContext).Get(aQuoteGroup.Id);
            lOutput.Id = aQuoteGroup.Id;
            lOutput.QuoteRequestID = aQuoteGroup.QuoteRequestID;
            lOutput.GUID = aQuoteGroup.GUID;
            lOutput.Note = aQuoteGroup.Note;
            lOutput.Quotes = QuoteUIHelper.Convert(aContext, aQuoteGroup.Quotes.SelectMany(x => x.Value).ToList());
            return lOutput;
        }

        public static List<UIQuoteGroup> Convert(IDbContext aContext, IEnumerable<QuoteGroup> aGroups, bool aDeepCopy = false)
        {
            var lOutput = new List<UIQuoteGroup>();
            foreach (var lGroup in aGroups.ToList())
            {
                if (lGroup.SelectedQuoteRequestTicketId != null)
                    lGroup.SelectedQuoteRequestTicket = new QuoteDataAccess(aContext).GetTicket(lGroup.SelectedQuoteRequestTicketId.Value); 
                lOutput.Add(Convert(aContext, lGroup, aDeepCopy));
            }
            return lOutput;
        }

        public static List<UIQuote> ConvertToQuotes(IDbContext aContext, ICollection<QuoteGroup> aGroups, bool aDeepCopy = false)
        {
            List<UIQuote> lOutput = new List<UIQuote>();
            List<UIQuoteGroup> lQuoteGroups = new List<UIQuoteGroup>();
            //foreach (var lGroup in aGroups.ToList()) 
            //    lQuoteGroups.Add(Convert(aContext, lGroup, aDeepCopy));


            foreach (var lGroup in aGroups)
            {
                lOutput.AddRange(QuoteUIHelper.Convert(aContext, lGroup.Quotes));
                lOutput.AddRange(QuoteUIHelper.Convert(aContext, lGroup.BotQuotes.Where(x => x.QuoteRequestResortID != null).ToList(), lGroup));
            }

            return lOutput;
        }

        public static List<UIFlight> ConvertFlights(IDbContext mContext, QuoteGroup lQG)
        {
            var lOutput = new List<UIFlight>();
            var lOutAirLine = "";
            var lOutTime = "";
            var lInTime = "";
            var lInAirLien = "";

            foreach (var lFlight in lQG.Flights.OrderBy(x=>x.OutBound.Flights.OrderBy(y=>y.Depart).First().Depart)
                .ThenBy(x => x.OutBound.Flights.OrderBy(y => y.Arrive).First().Arrive)
                .ThenByDescending(x => x.InBound.Flights.OrderByDescending(y => y.Depart).Last().Depart)
                )
            {
                //if (lFlight.OutBound.Stops > 1 || lFlight.InBound.Stops > 1)
                //    continue;

                UIQuoteRequestEdit lOutUI = new UIQuoteRequestEdit();
                UIQuoteRequestEdit lInUI = new UIQuoteRequestEdit();
                QuoteRequestEditUIHelper.ProcessOutboundFlights(lOutUI, lFlight.OutBound.Flights);
                QuoteRequestEditUIHelper.ProcessInboundFlights(lInUI, lFlight.InBound.Flights);
                var lUIFlight = new UIFlight()
                {
                    Id = lFlight.FlightItineraryId,
                    Out_DepartLocation = $"{lQG.QuoteRequest.DepartureAirPort.Name}",
                    Out_ArriveLocation = $"{lQG.QuoteRequest.DepartureAirPort.Name}",
                    Out_ConnectionAirport = lOutUI.Flight_Out_Layover,
                    ExtraCost = DataHelper.ConvertToCurrency(lFlight.ExtraCost),
                    Out_DepartTime = lOutUI.Flight_Out_Depart,
                    Out_ArriveTime = lOutUI.Flight_Out_Arrive,
                    Out_Carrier = lFlight.OutBound.Flights.First().Carrier,
                    In_Carrier = lFlight.InBound.Flights.First().Carrier,
                    In_DepartLocation = $"{lQG.QuoteRequest.DestinationAirPort.Name}",
                    In_ArriveLocation = $"{lQG.QuoteRequest.DestinationAirPort.Name}",
                    In_ConnectionAirport = lInUI.Flight_In_Layover,
                    In_DepartTime = lInUI.Flight_In_Depart,
                    In_Flight = lInUI.Flight_In_Numbers,
                    In_ArriveTime = lInUI.Flight_In_Arrive,
                };

                lOutput.Add(lUIFlight);
                if (lOutAirLine == lFlight.OutBound.Flights.First().Carrier)
                    lUIFlight.Out_Carrier = "";
                if (lOutTime == lOutUI.Flight_Out_Depart)
                    lUIFlight.Out_DepartTime = "";
                if (lInAirLien == lFlight.InBound.Flights.First().Carrier)
                    lUIFlight.In_Carrier = "";
                if (lInTime == lOutUI.Flight_Out_Arrive)
                    lUIFlight.Out_ArriveTime = "";

                lOutAirLine = lFlight.OutBound.Flights.First().Carrier;
                lOutTime = lOutUI.Flight_Out_Depart;
                lInAirLien = lFlight.InBound.Flights.First().Carrier;
                lInTime = lOutUI.Flight_Out_Arrive;
            }
            return lOutput;
        }
    }
}
