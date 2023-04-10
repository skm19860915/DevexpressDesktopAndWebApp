using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Extensions.Configuration;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.UIHelpers;
using BlitzerCore.Utilities;
using BlitzerCore.DataAccess;
using BlitzerCore.Helpers;

namespace BlitzerCore.Business
{
    public class QuoteBusiness
    {
        const string ClassName = "QuoteBusiness::";
        IDbContext DbContext { get; set; }
        QuoteDataAccess mDataAccess = null;
        private readonly IConfiguration mConfig;

        public QuoteBusiness(IDbContext aContext, IConfiguration aConfig = null)
        {
            DbContext = aContext;
            mConfig = aConfig;
            mDataAccess = new QuoteDataAccess(DbContext);
        }

        public Quote Save(UIQuote aQuote)
        {
            var lQuote = QuoteUIHelper.Convert(DbContext, aQuote);
            Save(lQuote);
            return lQuote;
        }

        public Quote Save(Quote aQuote)
        {
            ComputeCalcs(aQuote);
            if (aQuote.Flights != null)
            {
                foreach (var lTicket in aQuote.Flights)
                {
                    lTicket.QuoteRequestID = aQuote.QuoteRequestID;
                    if (lTicket.InBound.Flights != null)
                    {
                        lTicket.InBound.Flights.ForEach(x => x.QuoteGroupId = aQuote.QuoteGroup.Id);
                        lTicket.InBound.Flights.ForEach(x => x.Quote = aQuote);
                    }
                    if (lTicket.OutBound.Flights != null)
                    {
                        lTicket.OutBound.Flights.ForEach(x => x.QuoteGroupId = aQuote.QuoteGroup.Id);
                        lTicket.OutBound.Flights.ForEach(x => x.Quote = aQuote);
                    }
                }
            }
            mDataAccess.Save(aQuote);
            return aQuote;
        }

        private void ComputeCalcs(Quote aQuote)
        {
            aQuote.SubTotal = aQuote.PackagePrice + aQuote.ResortPrice + aQuote.FlightPrice;
            aQuote.Total = aQuote.SubTotal + aQuote.Adjustment;
        }
        public Quote Get(int aQuoteID)
        {
            var lQuote = mDataAccess.Get(aQuoteID);
            return lQuote;
        }

        public List<Quote> GetQuotes(QuoteRequest aQuoteRequest)
        {
            string FuncName = $"{ClassName}GetQuoteRequest (QuoteRequest)-";
            if (aQuoteRequest == null)
            {
                Logger.LogWarning($"{FuncName} Null Quote Passed into request");
                return new List<Quote>();
            }

            var lQR = new QuoteRequestBusiness(DbContext, null).GetQuoteRequest(aQuoteRequest.QuoteRequestID);
            return new QuoteDataAccess(DbContext).GetQuotesFromQuoteRequest(lQR);
        }

        public Quote GetQuoteFromQuoteRequest(int aQuoteRequestID)
        {
            var lQR = new QuoteRequestBusiness(DbContext, null).GetQuoteRequest(aQuoteRequestID);
            var lQuotes = new QuoteDataAccess(DbContext).GetQuotesFromQuoteRequest(lQR);
            if (lQuotes == null || lQuotes.Count() == 0)
                return null;

            var lQuote = lQuotes[0];
            return lQuote;
        }

        //public Quote GetQuoteWithDefaultFilter(int aQuoteRequestID)
        //{
        //    var lQuoteRqtBiz = new QuoteRequestBusiness(DbContext, null);
        //    QuoteRequest lRequest = lQuoteRqtBiz.GetQuoteRequest(aQuoteRequestID);
        //    return GetQuoteWithDefaultBLFilter(lRequest);
        //}

        public void AddResort(QuoteGroup aQuoteGroup, Hotel aHotel, SKU aRoomType, double aPrice, double aAdjustment)
        {
            string FuncName = $"{ClassName}AddResort - ";
            if (aHotel == null)
            {
                Logger.LogWarning($"{FuncName} Null Hotel passed into method.  Returning");
                return;
            }
            if (aRoomType == null)
            {
                Logger.LogWarning($"{FuncName} Null Hotel Room Type passed into method.  Returning");
                return;
            }

            var lTourOp = new TourOperatorBusiness(DbContext).Get(TourOperator.VACATION_EXPRESS);

            var lQuote = CreateQuote(aQuoteGroup);
            lQuote.SupplierId = aHotel.Id;
            lQuote.AccommodationRoomTypeID = aRoomType.SKUID;
            lQuote.ResortPrice = aPrice;
            lQuote.TourOperator = lTourOp;
            lQuote.TourOperatorID = lTourOp.Id;
            lQuote.Adjustment = aAdjustment;
            aQuoteGroup.Quotes.Add(lQuote);
            Save(lQuote);
        }

        public void Save(QuoteRequestResort aQuote)
        {
            new QuoteDataAccess(DbContext).Save(aQuote);
        }

        public Quote CreateQuote(QuoteGroup aQuoteGroup)
        {
            string FuncName = $"{ClassName}CreateQuote";
            return new Quote(aQuoteGroup);
        }

        public Quote Create(QuoteToResultsMapper aMap, Contact aUser)
        {
            var lQuote = new Quote()
            {
                AccommodationRoomTypeID = aMap.QuoteRequestResort.ResortRoomTypeID,
                TourOperatorID = aMap.QuoteRequestResort.TourOperatorID,
                QuoteRequestID = aMap.QuoteGroup.QuoteRequestID,
                QuoteGroupID = aMap.QuoteGroupID,
                SupplierId = aMap.QuoteRequestResort.ResortId,
                PackagePrice = aMap.QuoteRequestResort.Price,
                Status = QuoteStatus.Booked,
                BookedBy = new ContactBusiness(DbContext).Get(aUser.Id),
                BookedOn = DateTime.Now,
                BookedById = aUser.Id,
                Booked = true
            };

            return lQuote;
        }

        public double ComputePrice(QuoteGroup aQuoteGroup, QuoteToResultsMapper aMap)
        {            
            var lNumOfChildren = QuoteRequestBusiness.GetNumberOfChildren(aQuoteGroup.QuoteRequest);
            var lHotel = aMap.QuoteRequestResort.Price;

            if (aQuoteGroup.SelectedQuoteRequestTicket == null)
                return lHotel;

            var lAir = aQuoteGroup.SelectedQuoteRequestTicket.ExtraCost * aQuoteGroup.QuoteRequest.NumberOfAdults + aQuoteGroup.SelectedQuoteRequestTicket.ExtraCost * lNumOfChildren;
            return lHotel + lAir;
        }

        /// <summary>
        /// Get the QuoteGroup which contains all the quotes for the Contact/Client passed
        /// </summary>
        /// <param name="aContact"></param>
        /// <returns></returns>
        public List<QuoteGroup> GetQuoteGroups(Contact aContact)
        {
            return mDataAccess.GetQuoteGroups(aContact);
        }

        internal static string GetTransferText(Quote.TransferTypes? aTransferType)
        {
            switch (aTransferType)
            {
                case Quote.TransferTypes.Basic: return "Shared Transfers";
                case Quote.TransferTypes.NonStop: return "Non-Stop Shared Transfers";
                case Quote.TransferTypes.Private: return "Private Transfers";
                case Quote.TransferTypes.Premium: return "Delux Private Transfers";
                default: return "No Transfers";
            }
        }

        internal static string GetInsuranceText(Quote.InsuranceTypes? aInsurance)
        {
            switch (aInsurance)
            {
                case Quote.InsuranceTypes.Basic: return "Travel Protection ( Cancel for any reason, Refund in form of travel voucher)";
                case Quote.InsuranceTypes.Plus: return "Travel Protection Plus( Cancel for any reason, Refund in orignal form of payment)";
                case Quote.InsuranceTypes.PreDepartureWaiver: return "Cancel for any reason";
                default: return "No Travel Insurance";
            }
        }


        //public void Save(Quote lQuote, Filter lFilter)
        //{
        //    if (lQuote != null)
        //    {
        //        Save(lQuote);
        //        if (lFilter == null)
        //            throw new InvalidDataException("Null Filter Obj passed to Quote.Save");
        //        lFilter.QuoteGroupID = lQuote.QuoteID;
        //    }
        //    new FilterDataAccess(DbContext).Save(lFilter);
        //}


        private IEnumerable<FlightItinerary> FilterTickets(QuoteRequest aQuoteRequest, List<FilteredTicket> aTickets)
        {
            var lTickets = new QuoteRequestDataAccess(DbContext).GetTicketsByQRID(aQuoteRequest.QuoteRequestID);
            return lTickets;
        }

        private IEnumerable<QuoteRequestResort> FilterResorts(QuoteRequest aQuoteRequest, List<FilteredAccommodation> aAccommodations)
        {
            //var lResorts = new QuoteRequestDataAccess(DbContext).GetAccommondationsByQRID(aQuoteRequest.QuoteRequestID);
            //var lFilterKeys = aAccommodations.Select(A => A.AccommodationID);

            //if (lFilterKeys.Count() == 0)
            //    return lResorts;

            //return lResorts.Where(x => lFilterKeys.Contains(x.AccommodationID));
            return null;
        }

        public Filter Convert(BlitzerCore.Models.UI.UIFilter aFilter)
        {
            var lOutput = new Filter();
            lOutput.Accommodations = new List<FilteredAccommodation>();
            foreach (var lResortID in aFilter.SelectedAccommondations)
                lOutput.Accommodations.Add(new FilteredAccommodation() { AccommodationID = lResortID, Filter = lOutput });

            lOutput.Stops_Equals_0 = aFilter.Stops_Equals_0;
            lOutput.Stops_Equals_1 = aFilter.Stops_Equals_1;
            lOutput.AdultOnly = aFilter.AdultOnly;
            lOutput.AdultsOnlySection = aFilter.AdultsOnlySection;
            lOutput.AllInclusive = aFilter.AllInclusive;
            lOutput.PerPersonBudget = DataHelper.ConvertFromCurrency(aFilter.PerPersonBudget);
            lOutput.TripBudget = DataHelper.ConvertFromCurrency(aFilter.TripBudget);
            lOutput.TripMinBudget = DataHelper.ConvertFromCurrency(aFilter.TripMinBudget);
            lOutput.FilterID = aFilter.FilterID;
            lOutput.QuoteGroupID = aFilter.QuoteGroupID;
            lOutput.SelectedLocation = aFilter.SelectedLocation;
            lOutput.QuoteRequestID = aFilter.QuoteRequestID;
            lOutput.Stars = DataHelper.GetInt(aFilter.SelectedStars);
            return lOutput;
        }

        public string GetQuoteName(Quote lQuote)
        {
            string lOutput = "";
            AirPort lDepartFrom = lQuote.QuoteRequest.DepartureAirPort;
            if (lDepartFrom.CountryName == "US")
                lOutput = lDepartFrom.City + ", " + lDepartFrom.State;
            else
                lOutput = lDepartFrom.City + ", " + lDepartFrom.CountryName;


            AirPort lDestination = lQuote.QuoteRequest.DestinationAirPort;

            var lCountry = "";
            lOutput += " to ";

            switch (lDestination.CountryName)
            {
                case "AU":
                    lCountry = "Austria";
                    break;
                default:
                    lCountry = "Mexico";
                    break;
            }

            if (lDestination.CountryName == "US")
                lOutput += lDestination.City + ", " + lDepartFrom.State;
            else
                lOutput += lDestination.City + ", " + lCountry;

            return lOutput;
        }

        public Quote GetQuote(int id, string aAgentID = null)
        {
            BlitzerCore.Models.Quote lQuote = new QuoteDataAccess(DbContext).Get(id);
            if (lQuote == null)
            {
                Logger.LogWarning("Failed to find Quote with ID = " + id);
                return null;
            }
            return lQuote;

        }

        public QuoteRequestResort GetBotQuote(int id)
        {
            BlitzerCore.Models.QuoteRequestResort lQuote = new QuoteDataAccess(DbContext).GetBot(id);
            if (lQuote == null)
            {
                Logger.LogWarning("Failed to find Quote with ID = " + id);
                return null;
            }
            return lQuote;
        }

        public List<Quote> GetQuotes(Contact aContact)
        {
            var lQuotes = new QuoteDataAccess(DbContext).Get(aContact);
            return lQuotes.Where(x => x.Status == QuoteStatus.Ready || x.Status == QuoteStatus.Sent).ToList();
        }
    }
}
