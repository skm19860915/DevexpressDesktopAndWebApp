using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BlitzerCore.Models;
using Microsoft.EntityFrameworkCore;
using BlitzerCore.Utilities;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Security.Cryptography.X509Certificates;

namespace BlitzerCore.DataAccess
{
    public class QuoteDataAccess
    {
        const string ClassName = "QuoteDataAccess::";
        IDbContext mContext;
        public QuoteDataAccess(IDbContext aContext)
        {
            mContext = aContext;
        }

        public int Save(QuoteRequestResort aQuote)
        {
            string FuncName = ClassName + $"Save (QuoteRequestResort = {aQuote.QuoteRequestResortID})";
            try
            {
                mContext.QuoteRequestResorts.Update(aQuote);
                var lCnt = mContext.SaveChanges();
                Logger.LogInfo($"{FuncName} Updated {lCnt} Records");
                return lCnt;
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName, e);
            }

            return 0;
        }


        public int Save(Quote aQuote, bool aSaveChanges = true)
        {
            string FuncName = ClassName + $"Save (Quote = {aQuote.QuoteID}, Commit = {aSaveChanges}) - ";
            string lText = null;

            if (aQuote.QuoteID > 0)
            {
                mContext.Quotes.Update(aQuote);
                lText = $"{ClassName}Save  Updated - (Quote = {aQuote.QuoteID}, Commit = {aSaveChanges}) ";
            }
            else
                mContext.Quotes.Add(aQuote);

            try
            {
                if (aSaveChanges == false)
                    return 0;

                var lCnt = mContext.SaveChanges();
                if (lText != null)
                    Logger.LogInfo(lText);
                else
                    Logger.LogInfo($"{ClassName}Save - Created new quote with id = {aQuote.QuoteID}");
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + "Failed to save Quote ", e);
                throw e;
            }
            return 0;
        }

        public IEnumerable<SKU> GetRoomTypes(QuoteGroup aQuoteGroup)
        {
            return mContext.QuoteRequestResorts
                .Include(l => l.ResortRoomType).ThenInclude(l1 => l1.Provider)
                .Where(quote => quote.QuoteGroupId == aQuoteGroup.Id).Select(x1 => x1.ResortRoomType).Distinct();
        }

        public IEnumerable<SKU> GetSubRoomTypes(QuoteGroup aQuoteGroup)
        {
            return mContext.QuoteToResultsMappers
                .Include(l => l.QuoteRequestResort).ThenInclude(l => l.ResortRoomType).ThenInclude(l1 => l1.Provider)
                .Where(quote => quote.QuoteGroupID == aQuoteGroup.Id && quote.QuoteRequestResort != null).Select(x1 => x1.QuoteRequestResort.ResortRoomType).Distinct();
        }

        public List<Flight> GetFlights(QuoteGroup aQuoteGroup)
        {
            var lOutput = mContext.RequestedFlights
            .Where(x => x.QuoteGroupId == aQuoteGroup.Id)
            .Include(x => x.DepartAirPort)
            .ToList();
            return lOutput;
        }

        //public int Save(List<QuoteRequestTicket> aTickets)
        //{
        //    int lOutput = 0;
        //    string FuncName = ClassName + $"Save - (List<QuoteRequestTicket>) ";

        //    mContext.QuoteRequestTickets.AddRange(aTickets);
        //    var lQuoteIds = aTickets.Select(m => m.QuoteRequestID).Distinct().ToList();
        //    var lQR = new QuoteRequestDataAccess(mContext).Get(lQuoteIds[0]);
        //    try
        //    {
        //        lOutput = mContext.SaveChanges();
        //        Logger.LogInfo(FuncName + $" update {lOutput} QuoteRequestTickets");
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.LogException(FuncName + " - Failed save QuoteRequestTickets", e);
        //    }

        //    return lOutput;
        //}

        public int Save(List<FlightItinerary> aTickets)
        {
            int lOutput = 0;
            string FuncName = ClassName + $"Save - (List<QuoteRequestTicket>) ";

            try
            {
                foreach (var lTicket in aTickets)
                {
                    if (lTicket.FlightItineraryId == 0)
                        mContext.FlightItineraries.Add(lTicket);
                    else
                        mContext.FlightItineraries.Update(lTicket);

                    lOutput = mContext.SaveChanges();
                }

                Logger.LogInfo(FuncName + $" update {lOutput} QuoteRequestTickets");
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + " - Failed save QuoteRequestTickets", e);
            }

            return 0;
        }


        public List<QuoteToResultsMapper> Get(QuoteGroup aQuoteGroup)
        {
            return mContext.QuoteToResultsMappers
                .Include(sub => sub.FlightItinerary).ThenInclude(sub1 => sub1.InBound).ThenInclude(sub2 => sub2.Flights).ThenInclude(sub3 => sub3.ArrivalAirPort)
                .Include(sub => sub.FlightItinerary).ThenInclude(sub1 => sub1.InBound).ThenInclude(sub2 => sub2.Flights).ThenInclude(sub3 => sub3.DepartAirPort)
                .Include(sub => sub.FlightItinerary).ThenInclude(sub1 => sub1.OutBound).ThenInclude(sub2 => sub2.Flights).ThenInclude(sub3 => sub3.ArrivalAirPort)
                .Include(sub => sub.FlightItinerary).ThenInclude(sub1 => sub1.OutBound).ThenInclude(sub2 => sub2.Flights).ThenInclude(sub3 => sub3.DepartAirPort)
                .Include(x => x.QuoteRequestResort).ThenInclude(sub1 => sub1.ResortRoomType)
                .Include(x => x.QuoteRequestResort).ThenInclude(sub1 => sub1.Resort)
                .Include(x => x.QuoteGroup).ThenInclude(sub1 => sub1.QuoteRequest)
                .Where(x => x.QuoteGroupID == aQuoteGroup.Id).ToList();
        }

        public List<Quote> Get(Contact aContact)
        {
            var lResult = mContext.Opportunities.Where(x => x.Stage != OpportunityStages.Won && x.Stage != OpportunityStages.Loss)
                .SelectMany(y => y.Travelers)
                .Where(u => u.UserID == aContact.Id)
                .SelectMany(x => x.Opportunity.QuoteRequests)
                .SelectMany(u1 => u1.QuoteGroups)
                .SelectMany(x1 => x1.Quotes).Include(x2 => x2.QuoteRequest).ThenInclude(sub => sub.Opportunity).ThenInclude(sub1 => sub1.Travelers);

            if (lResult == null)
                return new List<Quote>();

            return lResult.ToList();
        }

        public Quote GetQuoteLite(int aQuoteId)
        {
            return mContext.Quotes
                .Where(x => x.QuoteID == aQuoteId).FirstOrDefault();
        }

        public Quote Get(int aQuoteID)
        {
            var lOutput = mContext.Quotes
                .Include(x => x.QuoteRequest).ThenInclude(sub => sub.Agent).ThenInclude(sub1 => sub1.Emails)
                .Include(x => x.QuoteRequest).ThenInclude(sub => sub.Agent).ThenInclude(sub1 => sub1.PhoneNumbers)
                .Include(x => x.QuoteRequest).ThenInclude(sub => sub.DepartureAirPort)
                .Include(x => x.QuoteRequest).ThenInclude(sub => sub.DestinationAirPort)
                .Include(sub1 => sub1.Accommodation).ThenInclude(t1 => t1.Amenities)
                .Include(sub1 => sub1.AccommodationRoomType)
                .Include(sub1 => sub1.Flights).ThenInclude(sub2 => sub2.InBound)
                .Include(sub1 => sub1.Flights).ThenInclude(sub2 => sub2.OutBound)
                .Include(x => x.QuoteGroup)
                //.Include(x => x.QToRMapper).ThenInclude(sub => sub.QuoteRequestTicket).ThenInclude(sub1 => sub1.InBound).ThenInclude(sub2 => sub2.Flights).ThenInclude(sub3 => sub3.ArrivalAirPort)
                //.Include(x => x.QToRMapper).ThenInclude(sub => sub.QuoteRequestTicket).ThenInclude(sub1 => sub1.InBound).ThenInclude(sub2 => sub2.Flights).ThenInclude(sub3 => sub3.DepartAirPort)
                //.Include(x => x.QToRMapper).ThenInclude(sub => sub.QuoteRequestTicket).ThenInclude(sub1 => sub1.OutBound).ThenInclude(sub2 => sub2.Flights).ThenInclude(sub3 => sub3.ArrivalAirPort)
                //.Include(x => x.QToRMapper).ThenInclude(sub => sub.QuoteRequestTicket).ThenInclude(sub1 => sub1.OutBound).ThenInclude(sub2 => sub2.Flights).ThenInclude(sub3 => sub3.DepartAirPort)
                .Include(x => x.QuoteRequest).ThenInclude(sub => sub.Opportunity).ThenInclude(sub1 => sub1.Travelers).ThenInclude(sub2 => sub2.User).ThenInclude(sub3 => sub3.Emails)
                //.Include(x => x.QuoteRequest).ThenInclude(sub => sub.Opportunity).ThenInclude(sub1 => sub1.Travelers).ThenInclude(sub2 => sub2.User).ThenInclude(sub3 => sub3.Relationship)
                .Where(x => x.QuoteID == aQuoteID).FirstOrDefault();
            return lOutput;
        }

        public QuoteRequestResort GetBot(int aQuoteID)
        {
            var lOutput = mContext.QuoteRequestResorts
                .Include(x => x.QuoteGroup).ThenInclude(sub0 => sub0.QuoteRequest).ThenInclude(sub => sub.Agent).ThenInclude(sub1 => sub1.Emails)
                .Include(x => x.QuoteGroup).ThenInclude(sub0 => sub0.QuoteRequest).ThenInclude(sub => sub.Agent).ThenInclude(sub1 => sub1.PhoneNumbers)
                .Include(x => x.QuoteGroup).ThenInclude(sub0 => sub0.QuoteRequest).ThenInclude(sub => sub.DepartureAirPort)
                .Include(x => x.QuoteGroup).ThenInclude(sub0 => sub0.QuoteRequest).ThenInclude(sub => sub.DestinationAirPort)
                .Include(sub1 => sub1.Resort)
                .Include(sub1 => sub1.ResortRoomType)
                .Where(x => x.QuoteRequestResortID == aQuoteID).FirstOrDefault();
            return lOutput;
        }

        public int GetOpportunityIdFromQuote(int aQuoteMapperId)
        {
            try
            {
                var lQuoteToResult = mContext.QuoteToResultsMappers
                    .Include(x=>x.QuoteGroup).ThenInclude(y=>y.QuoteRequest)
                    .First(x => x.QRToQMapID == aQuoteMapperId);
                return lQuoteToResult.QuoteGroup.QuoteRequest.OpportunityID;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public List<Quote> GetQuotesFromQuoteRequest(QuoteRequest aQuoteRequest)
        {
            if (aQuoteRequest == null)
                return new List<Quote>();

            return mContext.Quotes.Where(x => x.QuoteRequestID == aQuoteRequest.QuoteRequestID).ToList();
        }
        public AirPort GetAirPortCode(int aAirPortID)
        {
            return mContext.AirPorts.FirstOrDefault(x => x.AirPortID == aAirPortID);
        }


        public AirPort GetAirPortCode(string aAirPortCode)
        {
            return mContext.AirPorts.Where(x => x.Code == aAirPortCode).FirstOrDefault();
        }

        public FlightItinerary GetTicket(int aTicketId)
        {
            return mContext.FlightItineraries
                .Include(quote => quote.InBound).ThenInclude(Sub => Sub.Flights).ThenInclude(Flt => Flt.ArrivalAirPort)
                .Include(quote => quote.InBound).ThenInclude(Sub => Sub.Flights).ThenInclude(Flt => Flt.DepartAirPort)
                .Include(quote => quote.OutBound).ThenInclude(Sub => Sub.Flights).ThenInclude(Flt => Flt.ArrivalAirPort)
                .Include(quote => quote.OutBound).ThenInclude(Sub => Sub.Flights).ThenInclude(Flt => Flt.DepartAirPort)
                .Include(quote => quote.QuoteRequest)
                .FirstOrDefault(x => x.FlightItineraryId == aTicketId);
        }

        public IEnumerable<FlightItinerary> GetTicketsByQRID(int aQuoteRequestID)
        {
            //var lFilteredData = mContext.QuoteToResultsMappers.Where(x => x.QuoteID == aQuoteID && x.QuoteRequestTicketID != null).Select(x => x.QuoteRequestTicketID);

            return mContext.FlightItineraries
                .Include(quote => quote.InBound).ThenInclude(Sub => Sub.Flights).ThenInclude(Flt => Flt.ArrivalAirPort)
                .Include(quote => quote.InBound).ThenInclude(Sub => Sub.Flights).ThenInclude(Flt => Flt.DepartAirPort)
                .Include(quote => quote.OutBound).ThenInclude(Sub => Sub.Flights).ThenInclude(Flt => Flt.ArrivalAirPort)
                .Include(quote => quote.OutBound).ThenInclude(Sub => Sub.Flights).ThenInclude(Flt => Flt.DepartAirPort)
                .Include(quote => quote.QuoteRequest)
                .Where(quote => quote.QuoteRequestID == aQuoteRequestID);
            //.Where(x => lFilteredData.Contains(x.TicketID));
        }

        public Hotel GetHotelByQuoteRequestResortId(int aId)
        {
            return mContext.QuoteRequestResorts.Where(x => x.QuoteRequestResortID == aId).Select(y => y.Resort).FirstOrDefault();
        }

        public List<QuoteRequestResort> GetAccommondations(QuoteGroup aQuoteGroup)
        {
            string FuncName = ClassName + $"GetAccommondationsByQGID - (int = {aQuoteGroup.Id})";
            Logger.EnterFunction(FuncName);
            var lResortDA = new AccommodationDataAccess(mContext);
            Logger.LogDebug($"{mContext.QuoteRequestResorts.Count()} Quote Request Resorts Pre");
            Logger.LogDebug($"{mContext.QuoteRequestResorts.Count(quote => quote.QuoteGroupId == aQuoteGroup.Id)} Filtered");
            var lOutput = mContext.QuoteRequestResorts
                .Include(qg => qg.QuoteGroup).ThenInclude(quote => quote.QuoteRequest)
                .Include(quote => quote.TourOperator)
                .Include(quote => quote.Resort).ThenInclude(sub => sub.Amenities)
                .Where(quote => quote.QuoteGroupId == aQuoteGroup.Id).OrderBy(x => x.Price).ToList();

            Logger.LogDebug($"{lOutput.Count()} Quote Request Resorts");
            Logger.LogDebug($"{lOutput.Count(x => x.Resort.Rating > 4.9)} Resorts have 5 star rating ");
            Logger.LogDebug($"{lOutput.Count(x => x.Resort.Rating > 3.9 && x.Resort.Rating < 4.7)} Resorts have 4+ star rating ");
            Logger.LogDebug($"{lResortDA.GetAdultsOnly()} Adults only Resorts");
            Logger.LogDebug($"{lResortDA.GetAllInclusive()} All Inclusive");
            if (lOutput.Count(x => x.Resort.Rating < 1) > 5)
                Logger.LogWarning(FuncName + $" {lOutput.Count(x => x.Resort.Rating < 1)} Resorts have 0 star rating ");
            if (lOutput.Count(x => x.Resort.Rating == null) > 5)
                Logger.LogWarning(FuncName + $" {lOutput.Count(x => x.Resort.Rating == null)} Resorts have null star rating ");
            Logger.LeaveFunction(FuncName);

            return lOutput;
        }

        public List<Quote> GetQuotesFromBots(QuoteGroup aQuoteGroup)
        {
            if (aQuoteGroup == null)
                return new List<Quote>();

            return mContext
                .QuoteRequestResorts
                .Include(qg => qg.QuoteGroup).ThenInclude(x => x.QuoteRequest).ThenInclude(x1 => x1.Opportunity).ThenInclude(x2 => x2.Travelers)
                .Include(x => x.ResortRoomType)
                .Where(x => x.QuoteGroupId == aQuoteGroup.Id)
                .Select(y => new Quote
                {
                    QuoteGroupID = y.QuoteGroupId,
                    AccommodationRoomTypeID = y.ResortRoomTypeID,
                    AccommodationRoomType = y.ResortRoomType,
                    QuoteGroup = y.QuoteGroup,
                    PackagePrice = y.LandOnly == false ? y.Price : 0,
                    ResortPrice = y.LandOnly == false ? 0 : y.Price,
                    TourOperatorID = y.TourOperatorID,
                    SupplierId = y.ResortId
                }).ToList();
        }

        internal List<QuoteGroup> GetQuoteGroups(Contact aContact)
        {
            return (from O in mContext.Opportunities
                    join U in mContext.UserMaps on O.ID equals U.OpportunityID
                    join QR in mContext.QuoteRequests on O.ID equals QR.OpportunityID
                    join QG in mContext.QuoteGroups on QR.QuoteRequestID equals QG.QuoteRequestID
                    where U.User.Id == aContact.Id
                    && O.Stage != OpportunityStages.Loss
                    select QG).ToList();
        }

        public QuoteToResultsMapper GetMapper(int aID)
        {
            return mContext.QuoteToResultsMappers
                .Include(x => x.QuoteGroup).ThenInclude(x1 => x1.QuoteRequest).ThenInclude(x2 => x2.Opportunity)
                .Include(x => x.QuoteRequestResort).ThenInclude(x1 => x1.QuoteGroup)
                .Include(x => x.QuoteRequestResort).ThenInclude(x1 => x1.Resort)
                .Include(x => x.QuoteRequestResort).ThenInclude(x1 => x1.ResortRoomType)
                .Where(x => x.QRToQMapID == aID).First();

        }

        public void ExcludeQuotes(List<int> aQRTOQMapIds)
        {
            var lMappers = mContext.QuoteToResultsMappers
                .Include(x => x.QuoteRequestResort)
                .Where(x => aQRTOQMapIds.Contains(x.QRToQMapID))
                .ToList();
            foreach (var lQuoteToResultsMapper in lMappers)
                lQuoteToResultsMapper.QuoteRequestResort.Exclude = true;

            mContext.SaveChanges();
        }

        public void IncludeQuotes(List<int> aQRTOQMapIds)
        {
            var lMappers = mContext.QuoteToResultsMappers
                .Include(x => x.QuoteRequestResort)
                .Where(x => aQRTOQMapIds.Contains(x.QRToQMapID))
                .ToList();
            foreach (var lQuoteToResultsMapper in lMappers)
                lQuoteToResultsMapper.QuoteRequestResort.Exclude = false;

            mContext.SaveChanges();
        }

        public void Save(QuoteToResultsMapper aQuoteToResultsMapper, bool aSaveChanges = true)
        {
            if (aQuoteToResultsMapper.QRToQMapID == 0)
                mContext.QuoteToResultsMappers.Add(aQuoteToResultsMapper);
            else
                mContext.QuoteToResultsMappers.Update(aQuoteToResultsMapper);

            if (aSaveChanges == true)
                mContext.SaveChanges();
        }

        public int SaveExcludedRoomTypes(IEnumerable<int> aQRRIDs)
        {
            var lQRR = mContext.QuoteRequestResorts.Where(x => aQRRIDs.Contains(x.QuoteRequestResortID)).ToList();
            lQRR.ForEach(x => x.Exclude = true);
            return mContext.SaveChanges();
        }

        public int SaveBookedHotels(IEnumerable<int> lHotelIDs, string aCurrentUserID)
        {
            //var lQuotes = mContext.QuoteRequestResorts.Where(x => lHotelIDs.Contains(x.QuoteRequestResortID)).ToList();
            //lQuotes.ForEach(x => x.BookedById = aCurrentUserID);
            //lQuotes.ForEach(x => x.BookedOn = DateTime.Now);
            //return mContext.SaveChanges();
            throw new NotImplementedException();
        }
    }
}
