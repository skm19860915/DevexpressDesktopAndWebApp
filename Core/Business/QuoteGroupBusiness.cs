using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using BlitzerCore.DataAccess;
using BlitzerCore.Helpers;
using BlitzerCore.UIHelpers;
using Microsoft.Extensions.Configuration;

namespace BlitzerCore.Business
{
    public class QuoteGroupBusiness
    {
        public class Diff
        {
            public int TOId { get; set; }
            public double Amount { get; set; }
            public int PID { get; set; }
        }

        const string ClassName = "QuoteGroupBusiness::";
        IDbContext DbContext { get; }
        private readonly IConfiguration mConfig;

        public QuoteGroupBusiness(IDbContext aContext, IConfiguration aConfig = null)
        {
            DbContext = aContext;
            mConfig = aConfig;
        }
        public bool ValidTicket(int id, string tck)
        {
            string FuncName = ClassName + "ValidTicket (id,tck)";
            var lValid = new QuoteGroupDataAccess(DbContext).Exists(id, tck);
            if (lValid)
                Logger.LogDebug(FuncName + " Ticket was valid");
            else
                Logger.LogDebug(FuncName + " Ticket was NOT valid");
            return lValid;
        }

        public QuoteGroup Get(int id)
        {
            string FuncName = ClassName + $"Get (id={id})";
            return new QuoteGroupDataAccess(DbContext).Get(id);
        }
        public QuoteGroup Get(string aGUID)
        {
            string FuncName = ClassName + $"Get (id={aGUID})";
            return new QuoteGroupDataAccess(DbContext).Get(aGUID);
        }
        public UIQuoteGroup GetBotResults(QuoteGroup aQuoteGroup)
        {
            var lUIQR = QuoteGroupUIHelper.Convert(DbContext, aQuoteGroup);
            var lQuotes = new QuoteDataAccess(DbContext).GetQuotesFromBots(aQuoteGroup);
            //lUIQR.Quotes = QuoteUIHelper.Convert(DbContext, lQuotes).OrderBy(x => x.ResortName).ThenBy(x1 => x1.SKU).ToList();
            return lUIQR;
        }
        public IEnumerable<Flight> SaveStagingData(Staging.FlightHotelInformation aData, int aTourOperatorID, QuoteGroup aQuoteGroup)
        {
            var lTripBiz = new TripBusiness(DbContext);
            List<Flight> lResults = new List<Flight>();

            if (aData == null)
                return lResults;

            aData.Flights.ForEach(x => x.TourOperatorID = aTourOperatorID);
            aData.Hotels.ForEach(x => x.TourOperatorID = aTourOperatorID);
            aData.Hotels.ForEach(x => x.QuoteGroupId = aQuoteGroup.Id);

            var lTO = new CompanyBusiness(DbContext).Get(aTourOperatorID);
            aData.Flights.Where(x => x.Carrier == null).ToList().ForEach(x => x.Carrier = lTO.Name);

            // Save all Flights First
            if (aData != null && aData.Flights != null)
                new StagingDataAccess(DbContext).Save(aData.Flights);

            if (aData != null && aData.Hotels != null)
                new StagingDataAccess(DbContext).Save(aData.Hotels);

            DbContext.SaveChanges();

            return lResults;
        }


        //public Quote GetQuoteWithDefaultBLFilter(QuoteRequest aQuoteRequest)
        //{
        //    var lQBiz = new QuoteBusiness(DbContext);
        //    var lQuote = new Quote(aQuoteRequest);
        //    var lFilter = new FilterBusiness(DbContext).GetDefaultFilter(aQuoteRequest.DestinationAirPort, aQuoteRequest.Agent);
        //    lFilter.Accommodations = new List<FilteredAccommodation>();
        //    lQBiz.Save(lQuote, lFilter);
        //    ApplyFilter(lFilter);
        //    return lQuote;
        //}
        public QuoteGroup Create(QuoteRequest aQuoteRequest )
        {
            var lQuoteGroups = aQuoteRequest.QuoteGroups;
            if (lQuoteGroups.Count == 0 || lQuoteGroups.Last().SentDate != null)
            {
                QuoteGroup lOutput = new QuoteGroup();
                lOutput.GUID = Guid.NewGuid().ToString();
                lOutput.QuoteRequestID = aQuoteRequest.QuoteRequestID;
                lOutput.QuoteRequest = aQuoteRequest;
                var lFilter = new FilterBusiness(DbContext).GetDefaultFilter(aQuoteRequest);
                lFilter.QuoteGroup = lOutput;
                //new FilterBusiness(DbContext).Save(lFilter);
                ApplySubFilter(lFilter);

                return lOutput;
            }

            return lQuoteGroups.Last();
        }


        public QuoteGroup Preview(QuoteRequest aQuoteRequest)
        {
            //QuoteGroup lOutput = new QuoteGroup();
            //lOutput.Locked = true;
            //lOutput.GUID = Guid.NewGuid().ToString();
            //lOutput.SentDate = DateTime.Now;
            //lOutput.Status = QuoteGroupStatus.Preview;
            //lOutput.QuoteRequestID = aQuoteRequest.QuoteRequestID;
            //DeleteOldPreviews(aQuoteRequest);
            //foreach (var lQuote in aQuoteRequest.Quotes.Where(x => x.Status == QuoteStatus.Ready || x.Status == QuoteStatus.NotReady))
            //    PrepareToPreview(lOutput, lQuote);

            //new QuoteGroupDataAccess(DbContext).Save(lOutput);

            //return lOutput;
            throw new NotImplementedException();
        }

        private void DeleteOldPreviews(QuoteRequest aQuoteRequest)
        {
            new QuoteGroupDataAccess(DbContext).DeletePreviews(aQuoteRequest);
        }

        public IEnumerable<QuoteGroup> Get(QuoteRequest aQR, QuoteGroupFilter aFilter)
        {
            return new QuoteGroupDataAccess(DbContext).Get(aQR, aFilter);
        }
        private void PrepareToSend(QuoteGroup lOutput, Quote lQuote)
        {
            lQuote.QuoteGroup = lOutput;
            lQuote.Status = QuoteStatus.Sent;
            if ( lOutput.Quotes.Count(x=>x.QuoteID == lQuote.QuoteID) == 0 )
                lOutput.Quotes.Add(lQuote);
        }
        private void PrepareToPreview(QuoteGroup lOutput, Quote lQuote)
        {
            lQuote.QuoteGroup = lOutput;
        }


        public Filter GetFilter(QuoteGroup aQuoteGroup)
        {
            if (aQuoteGroup == null)
                throw new InvalidDataException("QuoteBiz.GetFilterFromQuote can't accept null Quote obj");
            return new QuoteGroupDataAccess(DbContext).GetFilter(aQuoteGroup);
        }

        public QuoteGroup GetQuoteGroup(Filter aFilter)
        {
            if (aFilter.QuoteGroup != null)
                return aFilter.QuoteGroup;

            var lExistingFilter = new FilterBusiness(DbContext).Get(aFilter.FilterID);
            return lExistingFilter.QuoteGroup;
        }



        public IEnumerable<Quote> GetTop5Resorts ( IEnumerable<Quote> aQuotes)
        {
            int lCnt = 5;
            var lAllResorts = aQuotes.Select(x => x.Accommodation).Distinct().OrderByDescending(y => y.Rating);
            if (lAllResorts.Count() < 5)
                lCnt = lAllResorts.Count();
            var lFilterResorts = lAllResorts.Take(lCnt).Select(x => x.Id);
            var lFilteredQuotes = aQuotes.Where(x => lFilterResorts.Contains(x.Accommodation.Id));
            return lFilteredQuotes;
        }

        public List<Quote> GetBestQuotesByTourOperator(QuoteGroup aQuoteGroup, Agent aAgent)
        {
            string FuncName = ClassName + $"GetBestQuotesByTourOperator (aQuoteGroupId = {aQuoteGroup.Id},tck)";
            var lResults = new List<Quote>();
            try
            {
                Logger.EnterFunction(FuncName);
                var lAllQuotes = GetAllQuotes(aQuoteGroup);
                Logger.LogDebug($"Quote Cnt = {lAllQuotes.Count()}");
                var lRoomTypeIDs = lAllQuotes.Where(x => x.AccommodationRoomType != null).Select(x => x.AccommodationRoomTypeID).Distinct();
                Logger.LogDebug($"RoomType Cnt = {lRoomTypeIDs.Count()}");
                var lTOIds = lAllQuotes.Select(x => x.TourOperatorID);
                var lDiffs = new List<Diff>();
                Quote lPackageQuote = null;
                Quote lLandOnlyQuote = null;
                double lPackageMin = 0;
                double lLandOnlyMin = 0;

                foreach (var lRoomTypeID in lRoomTypeIDs)
                {
                    lPackageQuote = null;
                    lLandOnlyQuote = null;

                    var lPackageQuotes = lAllQuotes.Where(x => x.AccommodationRoomTypeID == lRoomTypeID && x.PackagePrice > 0);
                    if (lPackageQuotes.Count() > 0)
                    {
                        lPackageMin = lPackageQuotes.Min(x => x.PackagePrice);
                        //Logger.LogDebug($"Package Cnt = {lPackageQuotes.Count()}");
                    }

                    var lLandOnlyQuotes = lAllQuotes.Where(x => x.AccommodationRoomTypeID == lRoomTypeID && x.ResortPrice > 0);
                    if (lLandOnlyQuotes.Count() > 0)
                    {
                        lLandOnlyMin = lLandOnlyQuotes.Min(x => x.ResortPrice);
                        //Logger.LogDebug($"LandOnly Cnt = {lLandOnlyQuotes.Count()}");
                    }

                    if (lPackageMin > 0)
                    {
                        if (aAgent == null) // Clients on see quotes that are ready
                            lPackageQuote = lAllQuotes.Where(x => x.PackagePrice == lPackageMin && (x.Status == QuoteStatus.Ready || x.Status == QuoteStatus.Sent)).FirstOrDefault();
                        else // Agent can see all the quotes
                            lPackageQuote = lAllQuotes.Where(x => x.PackagePrice == lPackageMin && x.Status != QuoteStatus.Excluded).FirstOrDefault();
                    }

                    if (lLandOnlyMin > 0)
                    {
                        if (aAgent == null) // Clients on see quotes that are ready
                            lLandOnlyQuote = lAllQuotes.Where(x => x.ResortPrice == lLandOnlyMin && (x.Status == QuoteStatus.Ready || x.Status == QuoteStatus.Sent)).FirstOrDefault();
                        else // Agent can see all the quotes
                            lLandOnlyQuote = lAllQuotes.Where(x => x.ResortPrice == lLandOnlyMin && x.Status != QuoteStatus.Excluded).FirstOrDefault();
                    }

                    if (lPackageQuote != null && lPackageMin > 0)
                        lResults.Add(lPackageQuote);

                    if (lLandOnlyQuote != null && lLandOnlyMin > 0)
                        lResults.Add(lLandOnlyQuote);
                }

            } finally
            {
                Logger.LeaveFunction(FuncName);
            }
            return lResults.Distinct().ToList();
        }

        private List<Quote> GetAllQuotes(QuoteGroup aQuoteGroup)
        {
            var lOutput = new List<Quote>();
            lOutput.AddRange(aQuoteGroup.Quotes);
            FlightItinerary lFilterTicket = null;
            if (aQuoteGroup.SelectedQuoteRequestTicketId != null)
            {
                lFilterTicket = new QuoteDataAccess(DbContext).GetTicket(aQuoteGroup.SelectedQuoteRequestTicketId.Value);
                aQuoteGroup.SelectedQuoteRequestTicket = lFilterTicket;
            }
            List<QuoteToResultsMapper> lQTRMapper = new QuoteDataAccess(DbContext).Get(aQuoteGroup);
            foreach (var lBotQuote in aQuoteGroup.BotQuotes.Where(x=>x.Exclude == false))
                if (lBotQuote.QuoteRequestResortID != null)
                {
                    if (lFilterTicket != null && lBotQuote.QuoteRequestResort != null && lBotQuote.QuoteRequestResort.TourOperatorID != lFilterTicket.TourOperatorId)
                        continue;

                    var lQuote = Convert(aQuoteGroup, lBotQuote);
                    if ( aQuoteGroup.Flights != null ) 
                        lQuote.Flights.AddRange(aQuoteGroup.Flights);
                    lOutput.Add(lQuote);

                }
            return lOutput.OrderBy(x=>x.AccommodationRoomType.SortOrder).ToList();
        }

        private Quote Convert(QuoteGroup aQuoteGroup, QuoteToResultsMapper aBotQuote)
        {
            var lQuote = new Quote()
            {
                QuoteID = aBotQuote.QRToQMapID * -1,
                Accommodation = aBotQuote.QuoteRequestResort.Resort,
                AccommodationRoomType = aBotQuote.QuoteRequestResort.ResortRoomType,
                AccommodationRoomTypeID = aBotQuote.QuoteRequestResort.ResortRoomTypeID,
                Total = new QuoteBusiness(null).ComputePrice( aQuoteGroup, aBotQuote), 
                PackagePrice = aBotQuote.QuoteRequestResort.LandOnly ? 0 : aBotQuote.QuoteRequestResort.Price, 
               ResortPrice = aBotQuote.QuoteRequestResort.LandOnly ? aBotQuote.QuoteRequestResort.Price : 0,
                QuoteRequestID = aBotQuote.QuoteGroup.QuoteRequestID,
                QuoteRequest = aBotQuote.QuoteGroup.QuoteRequest,
                QuoteGroup = aBotQuote.QuoteGroup,
                QuoteGroupID = aBotQuote.QuoteGroup.Id,
                Flights = new List<FlightItinerary>()
            };

            if (aBotQuote.Exclude == false && aBotQuote.QuoteRequestResort.Exclude == false)
                lQuote.Status = QuoteStatus.Ready;
            else
                lQuote.Status = QuoteStatus.Excluded;

            return lQuote;
        }
        public QuoteGroup ApplyFilter(Filter aFilter)
        {
            string FuncName = ClassName + $"ApplyFilter ( FilterID = {aFilter.FilterID})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lFilter = aFilter;
                var lQDA = new QuoteDataAccess(DbContext);
                var lQuoteGroup = GetQuoteGroup(lFilter);
                var lQTRBiz = new QTRBusiness(DbContext);
                new QTRBusiness(DbContext).DeleteMappings(lQuoteGroup);
                var lQR = lQuoteGroup.QuoteRequest;
                if (lQR == null)
                    throw new InvalidDataException("QuoteBiz.ApplyFilter requires the Filter to have a QuoteRequest");

                var lResorts = lQDA.GetAccommondations(lQuoteGroup);
                var lMissCnt = lResorts.Count(x => x.Price == 0);
                if (lMissCnt > 0)
                {
                    Logger.LogWarning($"Removing {lMissCnt} resorts because of missing prices");
                    lResorts = lResorts.Where(x => x.Price > 0).ToList();
                }
                var lResortCnt = lResorts.Count();
                var lUniqueRst = lResorts.Select(x => x.Resort).Distinct();
                var lTickets = lQDA.GetTicketsByQRID(lQR.QuoteRequestID);
                var lTicketCnt = lTickets.Count();

                Logger.LogInfo("QRID=" + lQR.QuoteRequestID + " there were " + lResortCnt + " unfiltered resorts");
                Logger.LogInfo("QRID=" + lQR.QuoteRequestID + " there were " + lTicketCnt + " unfiltered tickets");

                if (lFilter.Accommodations != null
                    && lFilter.Accommodations.Count() > 0)
                {
                    var lCount = lResorts.Count();
                    var lSKUs = lFilter.Accommodations.Where(x => x.IncludedSKUs != null).SelectMany(x => x.IncludedSKUs);
                    if (lSKUs.Count() > 0 && lSKUs.Select(x => x.SKUId).Count() > 0)
                        lResorts = lResorts.Where(x => lSKUs.Select(x1 => x1.SKUId).Contains(x.ResortRoomTypeID)).ToList();
                    Logger.LogInfo($" Dropped {lCount - lResorts.Count()} after Accommodation Type filter => {lResorts.Count()}");
                }

                var lResortIds = new List<int>();
                var lPullRecCnt = 0;
                for (lPullRecCnt = 0; lPullRecCnt < lResorts.Count(); lPullRecCnt++)
                {
                    var lResort = lResorts.ElementAt(lPullRecCnt);
                    if (lResortIds.Contains(lResort.Resort.Id) == false)
                        lResortIds.Add(lResort.Resort.Id);
                }

                Logger.LogInfo($"Pulling {lPullRecCnt} Resort Records");
                foreach (var lResortId in lResortIds)
                {
                    foreach (var lSelectedResort in lResorts.Where(x => x.ResortId == lResortId))
                        lQTRBiz.CreateMap(lFilter.QuoteGroup.BotQuotes, lQuoteGroup, lSelectedResort);
                }
                Logger.LogValue("BotCount after adding Resorts", lFilter.QuoteGroup.BotQuotes.Count());

                foreach (var lTicket in lTickets)
                {
                    lQTRBiz.CreateMap(lFilter.QuoteGroup.BotQuotes, lQuoteGroup, lTicket);
                }

                Logger.LogInfo("For QRID=" + lQR.QuoteRequestID + " there were " + lResorts.Count() + " Filtered resorts");
                Logger.LogInfo("For QRID=" + lQR.QuoteRequestID + " there were " + lTickets.Count() + " Filtered tickets");
                Logger.LogValue("BotCount after adding tickets", lFilter.QuoteGroup.BotQuotes.Count());
                return lFilter.QuoteGroup;
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }

        /// <summary>
        /// Filter the data from the QuoteResultsResorts and Maps down into 
        /// The quoteToResultsMap
        /// </summary>
        /// <param name="aFilter"></param>
        /// <returns></returns>
        public QuoteGroup ApplySubFilter(Filter aFilter)
        {
            string FuncName = ClassName + $"ApplySubFilter ( FilterID = {aFilter.FilterID})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lFilter = aFilter;
                var lQDA = new QuoteDataAccess(DbContext);
                var lQuoteGroup = GetQuoteGroup(lFilter);
                var lQTRBiz = new QTRBusiness(DbContext);
                new QTRBusiness(DbContext).DeleteMappings(lQuoteGroup);
                var lQR = lQuoteGroup.QuoteRequest;
                if (lQR == null)
                    throw new InvalidDataException("QuoteBiz.ApplyFilter requires the Filter to have a QuoteRequest");

                var lResorts = lQDA.GetAccommondations(lQuoteGroup);
                var lMissCnt = lResorts.Count(x => x.Price == 0);
                if (lMissCnt > 0)
                {
                    Logger.LogWarning($"Removing {lMissCnt} resorts because of missing prices");
                    lResorts = lResorts.Where(x => x.Price > 0).ToList();
                }
                var lResortCnt = lResorts.Count();
                var lUniqueRst = lResorts.Select(x => x.Resort).Distinct();
                var lTickets = lQDA.GetTicketsByQRID(lQR.QuoteRequestID);
                var lTicketCnt = lTickets.Count();

                Logger.LogInfo("QRID=" + lQR.QuoteRequestID + " there were " + lResortCnt + " unfiltered resorts");
                Logger.LogInfo("QRID=" + lQR.QuoteRequestID + " there were " + lTicketCnt + " unfiltered tickets");

                if (lFilter.Stops_Equals_1 == true)
                    lTickets = lTickets.Where(x => x.OutBound.Flights.Count <= 2 || x.InBound.Flights.Count <= 2);

                if (lFilter.Stops_Equals_0 == true && lFilter.Stops_Equals_1 == false)
                    lTickets = lTickets.Where(x => x.OutBound.Flights.Count == 1 || x.InBound.Flights.Count == 1);

                var lCount = lResorts.Count();
                if (lFilter.TripBudget > 0)
                    lResorts = lResorts.Where(x => x.Price < lFilter.TripBudget).ToList();
                Logger.LogInfo($" Dropped {lCount - lResorts.Count()} after budget filter {lFilter.TripBudget} => {lResorts.Count()}");

                lCount = lResorts.Count();
                if (lFilter.TripMinBudget > 0)
                    lResorts = lResorts.Where(x => x.Price > lFilter.TripMinBudget).ToList();
                Logger.LogInfo($" Dropped {lCount - lResorts.Count()} after Min budget filter {lFilter.TripMinBudget} => {lResorts.Count()}");

                lCount = lResorts.Count();
                if (lFilter.SelectedLocation != null)
                    lResorts = lResorts.Where(x => x.Resort.Area == lFilter.SelectedLocation).ToList();
                Logger.LogInfo($" Dropped {lCount - lResorts.Count()} after location filter {lFilter.SelectedLocation} => {lResorts.Count()}");

                lCount = lResorts.Count();
                // Filter for Stars
                if (lFilter.Stars > 1)
                    lResorts = lResorts.Where(x => lFilter.Stars <= x.Resort.Rating).ToList(); ;
                Logger.LogInfo($" Dropped {lCount - lResorts.Count()} after star filter of {lFilter.Stars} => {lResorts.Count()}");

                if (lFilter.AdultOnly)
                {
                    lCount = lResorts.Count();
                    var lResortIDs = lResorts.Select(x => x.Resort.Amenities.Where(y => y.AmenityID == (int)Amenity.AmenityTypes.AdultsOnly)).SelectMany(p => p).Select(u => u.AccommodationID).Distinct();
                    lResorts = lResorts.Where(x => lResortIDs.Contains(x.ResortId)).ToList();
                    Logger.LogInfo($" Dropped {lCount - lResorts.Count()} after Adults Only filter => {lResorts.Count()}");
                }

                if (lFilter.AllInclusive)
                {
                    lCount = lResorts.Count();
                    var lResortIDs = lResorts.Select(x => x.Resort.Amenities.Where(y => y.AmenityID == (int)Amenity.AmenityTypes.AllInclusive)).SelectMany(p => p).Select(u => u.AccommodationID).Distinct();
                    lResorts = lResorts.Where(x => lResortIDs.Contains(x.ResortId)).ToList();
                    Logger.LogInfo($" Dropped {lCount - lResorts.Count()} after All Inclusive filter => {lResorts.Count()}");
                }

                if (lFilter.Accommodations != null && lFilter.Accommodations.Count() > 0)
                {
                    lCount = lResorts.Count();
                    var lResortIDs = lFilter.Accommodations.Select(x => x.AccommodationID);
                    lResorts = lResorts.Where(x => lResortIDs.Contains(x.ResortId)).ToList();
                    Logger.LogInfo($" Dropped {lCount - lResorts.Count()} after Accommodations filter => {lResorts.Count()}");
                }

                if (lFilter.Accommodations != null 
                    && lFilter.Accommodations.Count() > 0)
                {
                    lCount = lResorts.Count();
                    var lSKUs = lFilter.Accommodations.Where(x=>x.IncludedSKUs != null).SelectMany(x => x.IncludedSKUs);
                    if ( lSKUs.Count() > 0 && lSKUs.Select(x => x.SKUId).Count() > 0 )
                        lResorts = lResorts.Where(x => lSKUs.Select(x1 => x1.SKUId).Contains(x.ResortRoomTypeID)).ToList();
                    Logger.LogInfo($" Dropped {lCount - lResorts.Count()} after Accommodation Type filter => {lResorts.Count()}");
                }

                var lResortIds = new List<int>();
                var lPullRecCnt = 0;
                for ( lPullRecCnt = 0; lPullRecCnt < lResorts.Count(); lPullRecCnt++)
                {
                    var lResort = lResorts.ElementAt(lPullRecCnt);
                    if (lResortIds.Contains(lResort.Resort.Id) == false)
                        lResortIds.Add(lResort.Resort.Id);
                    //if (lResortIds.Count() > 5)
                    //    break;
                }

                Logger.LogInfo($"Pulling {lPullRecCnt} Resort Records");
                foreach (var lResortId in lResortIds)
                {
                    foreach ( var lSelectedResort in lResorts.Where(x=>x.ResortId == lResortId))
                        lQTRBiz.CreateMap(lFilter.QuoteGroup.BotQuotes, lQuoteGroup, lSelectedResort);
                }
                Logger.LogValue("BotCount after adding Resorts", lFilter.QuoteGroup.BotQuotes.Count());

                //if (lFilter.AirFilters != null && lFilter.AirFilters.Count() > 0)
                //{
                //    foreach (var lTempFilter in lFilter.AirFilters)
                //    {
                //        var lAirFilter = AIFilterRegistry.GetFilter(lTempFilter.AIFilterID);
                //        lTickets = lAirFilter.Apply(lTickets);
                //    }
                //}

                foreach (var lTicket in lTickets)
                {
                    lQTRBiz.CreateMap(lFilter.QuoteGroup.BotQuotes, lQuoteGroup, lTicket);
                }

                Logger.LogInfo("For QRID=" + lQR.QuoteRequestID + " there were " + lResorts.Count() + " Filtered resorts");
                Logger.LogInfo("For QRID=" + lQR.QuoteRequestID + " there were " + lTickets.Count() + " Filtered tickets");
                Logger.LogValue("BotCount after adding tickets", lFilter.QuoteGroup.BotQuotes.Count());
                return lFilter.QuoteGroup;
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }
        public bool SendQuoteGroup(Agent aAgent, QuoteGroup aGroup, bool aSendEmail = true)
        {
            string FuncName = ClassName + $"SendQuoteGroup (Agent ={aAgent.Id}, QuoteGroup ={aGroup.Id})";
            if ( aSendEmail)
                SendQuoteGroupEmail(aGroup);
            new OpportunityBusiness(DbContext).QuoteSent(aGroup.QuoteRequest, aAgent);
            aGroup.Status = QuoteGroupStatus.Sent;
            aGroup.Quotes.Where(x=>x.Status == QuoteStatus.Ready).ToList().ForEach(x=>x.Status = QuoteStatus.Sent);
            aGroup.SentDate = DateTime.Now;
            new QuoteRequestBusiness(DbContext).GetOpenQuoteGroup(aGroup.QuoteRequest);
            new QuoteGroupBusiness(DbContext).Save(aGroup);
            return true;
        }

        public bool  SendQuoteGroupEmail(QuoteGroup aGroup)
        {
            string FuncName = ClassName + $"SendQuoteGroupEmail (QuoteGroup ={aGroup.Id})";
            const string NEWLINE = "<br/>";
            var lEmailAddrs = new List<string>();

            var lQuoteRequest = aGroup.QuoteRequest;
            var lAirPort = lQuoteRequest.DestinationAirPort;
            if ( lAirPort != null )
                lAirPort.Country = lAirPort.Country ?? new CountryDataAccess(DbContext).Get(lAirPort.CountryId.Value);
            var lClients = lQuoteRequest.Opportunity.Travelers;
            var lPrimary = new ContactBusiness(DbContext).Get(lClients[0].User.Id);

            // This is unit testing
            if (mConfig == null)
                return false;

            if (lPrimary.PrimaryEmail == null)
                return false;

            lEmailAddrs.AddRange(lClients.SelectMany(x => x.User.Emails)
                .Where(t => t.Address != null && t.Address != "")
                .Select(y => y.Address));

            IEmailHelper lEmailer = new CoreEmailHelper(mConfig);
            var lSubject = "Your " + lQuoteRequest.DestinationAirPort.CountryName + " Quote is Ready for " + DataHelper.GetShortDateString(lQuoteRequest.DepartureDate) + " to " + DataHelper.GetLongDateString(lQuoteRequest.ReturnDate);
            var lBody = "Hello " + lPrimary.Name + ", " + NEWLINE;
            lBody += NEWLINE;
            if (aGroup.Note != null && aGroup.Note.Trim().Length > 0 )
            {
                lBody += aGroup.Note;
                lBody += NEWLINE;
                lBody += NEWLINE;
            }
            lBody += "View your quote in your ";
            lBody += "<a href=\"" + mConfig["AppSettings:AppURL"] + "/Client/Quote/" + aGroup.GUID + " \"><i>Portal</i></a>";


            lBody += NEWLINE;
            lBody += NEWLINE;
            lBody += "All travelers are advised to consult the <a href=\"https://travel.state.gov/content/travel/en/international-travel.html\">US State Department</a> ";
            lBody += "and <a href=\"https://wwwnc.cdc.gov/travel\">CDC</a>  websites ";
            lBody += "for the latest information and advisories for destinations prior to booking travel.";
            lBody += NEWLINE;
            lBody += NEWLINE;
            lBody += "Valid passports are required for international travel for all US citizens.Passports should be valid for at least 6 months after travel.Information on obtaining or renewing a US passport can be found at the <a href=\" http://travel.state.gov/passport/passport_1738.html\">US State Department</a> website";
            lBody += NEWLINE;
            lBody += NEWLINE;
            lBody += "<b>Please be advised that prices and availability are subject to change at any time. Prices cannot be guaranteed until a booking is made and payment has been applied.</b>";
            lBody += NEWLINE;
            lBody += NEWLINE;
            lBody += "<div style=\"font-size:20px;color:Red\">Eze2Travel</div>";
            lBody += "Office : (919) 788-9885" + NEWLINE;
            lBody += "Cell : (919) 423-8588" + NEWLINE;
            lBody += "<img></img>";
            lEmailer.SendEmail(lEmailAddrs, new List<string>(), lSubject, lBody);

            Logger.LogInfo(FuncName + " Successfully called SendQuote");
            return true;
        }

        public void Save(QuoteGroup aQuoteGroup)
        {
            new QuoteGroupDataAccess(DbContext).Save(aQuoteGroup);
        }
    }
}
