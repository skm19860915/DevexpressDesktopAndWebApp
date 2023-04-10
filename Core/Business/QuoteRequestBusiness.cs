using System;
using System.Collections.Generic;
using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using BlitzerCore.DataAccess;
using BlitzerCore.Helpers;
using BlitzerCore.UIHelpers;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.Xml;
using System.Threading;

namespace BlitzerCore.Business
{
    public class QuoteRequestBusiness
    {
        const string ClassName = "QuoteRequestBusiness::";
        IDbContext DbContext { get; }
        QuoteRequestDataAccess mDataAccess = null;
        private readonly IConfiguration mConfig;
        // Used to drive unit testi

        public QuoteRequestBusiness(IDbContext aContext, IConfiguration aConfig = null)
        {
            DbContext = aContext;
            mConfig = aConfig;
            mDataAccess = new QuoteRequestDataAccess(DbContext);
        }
        public List<TourOperator> GetTourOperators()
        {
            var lTourOperators = new List<TourOperator>();
            lTourOperators.Add(new TourOperatorDataAccess(DbContext).Get(TourOperator.VACATION_EXPRESS));
            lTourOperators.Add(new TourOperatorDataAccess(DbContext).Get(TourOperator.DELTA_VACATIONS));
            //lTourOperators.Add(new TourOperatorDataAccess(DbContext).Get(TourOperator.JOURNESE));
            return lTourOperators;
        }

        /// <summary>
        /// The is primaryaly used when a user requests a quote from the web
        /// </summary>
        /// <param name="aRequest"></param>
        /// <param name="aClients"></param>
        /// <returns></returns>
        protected int SaveWithClients(QuoteRequest aRequest, Agent aAgent, List<UIContact> aClients = null)
        {
            if (aClients != null)
            {
                BlitzerCore.Models.Opportunity lOpp = new OpportunityBusiness(DbContext).GetOpportunity(aRequest, aClients, aAgent);
                if (lOpp != null)
                {
                    aRequest.Opportunity = lOpp;
                    aRequest.OpportunityID = lOpp.ID;
                    aRequest.Opportunity.Notes = aRequest.Notes;
                }
                else
                    Logger.LogError("Critical Error ----------- GetOpporotunity.GetOpporunity failed to find a Opportunity");
            }
            GetOpenQuoteGroup(aRequest);
            AddChildren(aRequest, aAgent);

            return mDataAccess.Save(aRequest);
        }

        public UIQuoteRequest GetNewQuoteRequest(Agent aAgent)
        {
            UIQuoteRequest lRequest = new UIQuoteRequest();
            lRequest.Contacts = GetDefaultClients();
            lRequest.AgentId = aAgent.Id;
            return lRequest;
        }

        public bool IsReadyToSend(QuoteRequest aQR)
        {
            foreach (var lQuoteGroup in aQR.QuoteGroups)
            {
                var lReadyBotQuotes = lQuoteGroup.BotQuotes.Count(x => x.Exclude == false) > 0;
                var lReadyManQuotes = lQuoteGroup.Quotes.Count(x => x.Status == QuoteStatus.Ready || x.Status == QuoteStatus.Sent) > 0;

                if (lReadyBotQuotes == false && lReadyManQuotes == false)
                    return false;
            }

            return true;
        }

        //public Quote CreateQuote(QuoteRequest aQuoteRequest)
        //{
        //    var lQBiz = new QuoteBusiness(DbContext);
        //    var lQuote = new QuoteGroupBusiness (aQuoteRequest);
        //    var lFilter = new FilterBusiness(DbContext).GetDefaultFilter(aQuoteRequest.DestinationAirPort, aQuoteRequest.Agent);
        //    lFilter.QuoteRequestID = aQuoteRequest.QuoteRequestID;
        //    lQBiz.Save(lQuote, lFilter);
        //    return lQuote;
        //}

        public QuoteRequest New(Contact aContact, Agent aAgent)
        {
            QuoteRequest lQR = null;
            var lOpp = new OpportunityBusiness(DbContext).CreateNewOpportunity(aContact, aAgent);
            if (aAgent != null && aAgent.Profile != null && aAgent.Profile.DefaultAirPort != null)
            {
                lQR = Create(aAgent, aAgent.Profile.DefaultAirPort.AirPortID, new AirBusiness(DbContext).GetDefaultAirPort().AirPortID, lOpp);
            }
            else
            {
                lQR = Create(aAgent, new AirBusiness(DbContext).GetDefaultAirPort().AirPortID, new AirBusiness(DbContext).GetDefaultAirPort().AirPortID, lOpp);
            }

            lQR.Agent = aAgent;
            lQR.AgentId = aAgent.Id;
            lQR.DepartureDate = DateTime.Now.AddDays(45);
            lQR.ReturnDate = DateTime.Now.AddDays(45 + 7);
            lQR.NumberOfAdults = 2;
            return lQR;
        }
        public List<Quote> GetQuotes(QuoteRequest lQR)
        {
            return new QuoteBusiness(DbContext).GetQuotes(lQR);
        }

        public List<QuoteRequest> Get(Contact aUser)
        {
            return new QuoteRequestDataAccess(DbContext).Get(aUser).Where(x => x.Opportunity.Status != Opportunity.OpportunityStatus.Inactive).ToList();
        }

        public QuoteRequest Get(int aQuoteRequestId)
        {
            return new QuoteRequestDataAccess(DbContext).Get(aQuoteRequestId);
        }

        public UIQuoteRequestWrapper GetBackGroundData(UIQuoteRequestWrapper aUI, Hotel aHotel)
        {
            string Func = $"[ClassName]:GetBackGroundData";
            Country lCountry = null;

            if (aHotel.AirPort.CountryId != null)
                lCountry = new CountryDataAccess(DbContext).Get(aHotel.AirPort.CountryId.Value);

            if (aHotel.AirPort != null && lCountry != null)
            {
                aUI.BackgroundImage = lCountry.ImageLocation;
                aUI.BackgroundCaption = "Your Aventure Awaits";
                if (aHotel.City != null)
                    aUI.BackgroundTitle = $"{aHotel.City}, {lCountry.Name}";
                else
                    aUI.BackgroundTitle = $"{lCountry.Name}";
            }
            else
            {
                Logger.LogWarning($"[Func] There Country was not set for Hotel[lHotel.Name]");
                aUI.BackgroundImage = "/images/DominicanRepublic.png";
                aUI.BackgroundCaption = "DISCOVER AN EXTRAORDINARY JOURNEY";
                aUI.BackgroundTitle = "";
            }

            return aUI; ;
        }
        public List<BlitzerCore.Models.UI.UIContact> GetDefaultClients()
        {
            var lOutput = new List<BlitzerCore.Models.UI.UIContact>();
            for (int i = 1; i <= 4; i++)
                lOutput.Add(new BlitzerCore.Models.UI.UIContact() { RelationshipID = i, First = "", Last = "" });

            return lOutput;
        }

        public static int GetNumberOfChildren(QuoteRequest aQuoteRequest)
        {
            return IsKid(aQuoteRequest.Child1Age) + IsKid(aQuoteRequest.Child2Age) + IsKid(aQuoteRequest.Child3Age);
        }

        public UIQuoteRequestWrapper GetQuoteInfo(QuoteGroup aQuoteGroup, Agent aAgent)
        {
            UIQuoteRequestWrapper lWrapper = new UIQuoteRequestWrapper()
            {
                Start = DataHelper.GetLongDateString(aQuoteGroup.QuoteRequest.DepartureDate),
                End = DataHelper.GetLongDateString(aQuoteGroup.QuoteRequest.ReturnDate),
                DepartureCityCode = aQuoteGroup.QuoteRequest.DepartureAirPort.Code,
                DestinationCityCode = aQuoteGroup.QuoteRequest.DestinationAirPort.Code,
                People = (aQuoteGroup.QuoteRequest.NumberOfChildren + aQuoteGroup.QuoteRequest.NumberOfAdults).ToString(),
                Rooms = "1"
            };

            // Must have status of Ready to show quote before sent to client
            // Must have status of Send to show quotes to clients after sent to client
            if (aQuoteGroup.BotQuotes.Count(x=>x.Exclude == false)  > 0 || aQuoteGroup.Quotes.Count(x=>x.Status == QuoteStatus.Ready || x.Status == QuoteStatus.Sent) > 0)
            {
                if (aQuoteGroup.BotQuotes.Any(x => x.QuoteRequestResort != null))
                {
                    var lQuote = aQuoteGroup.BotQuotes.Where(x => x.QuoteRequestResort != null).First();
                    var lQR = lQuote.QuoteRequestResort;
                    var lResort = lQR.Resort;

                    lWrapper = GetBackGroundData(lWrapper, lResort);
                }
                else
                    lWrapper = GetBackGroundData(lWrapper, aQuoteGroup.Quotes.First().Accommodation);

                lWrapper.isAgent = aAgent != null;
                return lWrapper;
            }
            
            throw new Exception("Invalid Quote");
        }

        private static int IsKid(int? aKid)
        {
            if (aKid == null)
                return 0;
            if (aKid.HasValue == false)
                return 0;
            if (aKid.Value == 0)
                return 0;
            return 1;
        }

        public QuoteRequest Create(Agent aAgent, string aDepartureCityCode, string aDestinationCityCode, Opportunity aOpp)
        {

            var lQuoteDataAccess = new QuoteDataAccess(DbContext);
            var lOutput = new QuoteRequest();
            lOutput.Opportunity = aOpp;
            lOutput.DepartureAirPort = lQuoteDataAccess.GetAirPortCode(aDepartureCityCode);
            lOutput.DepartureAirPortID = lOutput.DepartureAirPort.AirPortID;
            lOutput.DestinationAirPort = lQuoteDataAccess.GetAirPortCode(aDestinationCityCode);
            lOutput.DestinationAirPortID = lOutput.DestinationAirPort.AirPortID;
            lOutput.AgentId = aAgent.Id;
            lOutput.Agent = aAgent;
            lOutput.When = DateTime.Now;

            if (aAgent != null)
                lOutput.AgentId = aAgent.Id;
            GetOpenQuoteGroup(lOutput);
            Save(lOutput, aAgent);
            return lOutput;
        }

        public QuoteRequest Create(string aAgentID, string aDepartureCityCode, string aDestinationCityCode)
        {
            var lQuoteDataAccess = new QuoteDataAccess(DbContext);
            var lOutput = new QuoteRequest();
            //lOutput.Opportunity = aOpp;
            lOutput.DepartureAirPort = lQuoteDataAccess.GetAirPortCode(aDepartureCityCode);
            lOutput.DepartureAirPortID = lOutput.DepartureAirPort.AirPortID;
            lOutput.DestinationAirPort = lQuoteDataAccess.GetAirPortCode(aDestinationCityCode);
            lOutput.DestinationAirPortID = lOutput.DestinationAirPort.AirPortID;
            lOutput.AgentId = aAgentID;
            lOutput.Agent = new ContactBusiness(DbContext).GetAgent(aAgentID);
            GetOpenQuoteGroup(lOutput);
            return lOutput;
        }

        public QuoteRequest Create(Agent aAgent, int aDepartureCityID, int aDestinationCityID, Opportunity aOpp)
        {
            aOpp.InboundAirPortID = aDepartureCityID;
            aOpp.OutboundAirPortID = aDepartureCityID;

            var lQuoteRequest = Create(aAgent.Id, aDepartureCityID, aDestinationCityID, aOpp);
            lQuoteRequest.Agent = aAgent;
            GetOpenQuoteGroup(lQuoteRequest);
            return lQuoteRequest;
        }

        public QuoteRequest Create(string aAgentId, int aDepartureCityID, int aDestinationCityID, Opportunity aOpp)
        {
            var lQuoteDataAccess = new QuoteDataAccess(DbContext);
            var lOutput = new QuoteRequest();
            lOutput.Opportunity = aOpp;
            lOutput.DepartureAirPort = lQuoteDataAccess.GetAirPortCode(aDepartureCityID);
            lOutput.DepartureAirPortID = aDepartureCityID;
            lOutput.DestinationAirPort = lQuoteDataAccess.GetAirPortCode(aDestinationCityID);
            lOutput.DestinationAirPortID = aDestinationCityID;
            lOutput.AgentId = aAgentId;
            return lOutput;
        }

        public BlitzerCore.Models.QuoteRequest GetQuoteRequest(int aQuoteRequestID)
        {
            return mDataAccess.Get(aQuoteRequestID);
        }

        public List<QuoteRequest> GetQuoteRequestByOppId(int aOpportunityID)
        {
            return mDataAccess.GetByOppId(aOpportunityID);
        }

        public void Execute(QuoteGroup aQuoteGroup, Agent aAgent, List<TourOperator> aTourOperators)
        {
            string FuncName = ClassName + "Execute - ";
            Logger.EnterFunction(FuncName);
            IWebTravelSrv lWebBot = null;
            QuoteRequest.QuoteTypes lQuoteType = aQuoteGroup.QuoteRequest.QuoteType;
            int lNumOfLoops = aQuoteGroup.QuoteRequest.QuoteType == QuoteRequest.QuoteTypes.Both ? 2 : 1;
            int lStartIndex = 0;
            Logger.LogInfo($"The number of loops to execute is {lNumOfLoops} for QuoteType : {lQuoteType.ToString()}");
            try
            {
                Staging.FlightHotelInformation lResults = null;
                new QuoteGroupDataAccess(DbContext).DeleteData(aQuoteGroup);
                new QuoteRequestDataAccess(DbContext).DeleteStagingData(aQuoteGroup);

                foreach (var lTourOperator in aTourOperators)
                {
                    for (int i = lStartIndex; i < lNumOfLoops; i++)
                    {
                        Logger.LogDebug($"Loop Index {i}");
                        if (lNumOfLoops == 2 && i == 0)
                            aQuoteGroup.QuoteRequest.QuoteType = QuoteRequest.QuoteTypes.LandOnly;
                        else if (lNumOfLoops == 2 && i == 1)
                            aQuoteGroup.QuoteRequest.QuoteType = QuoteRequest.QuoteTypes.Package;

                        int lExecutions = 0;
                        do
                        {
                            // Search
                            try
                            {
                                lWebBot = new BlitzerBusiness(DbContext, mConfig).GetWebService(lTourOperator);
                                Logger.LogInfo($"Execution {lExecutions} for {lTourOperator.Name}");
                                var lAgentLogin = new ContactDataAccess(DbContext).GetLogin(lWebBot.TourOperatorID, aAgent.Id);
                                if (lAgentLogin == null)
                                {
                                    Logger.LogError(FuncName + "Travel Agent Login required to execute search for OperatorID = [" + lWebBot.TourOperatorID + "] Agent ID = [" + aQuoteGroup.QuoteRequest.AgentId + "]");
                                    throw new InvalidOperationException(FuncName + "Missing User Login data");
                                }
                                lWebBot.Login(lAgentLogin.UserName, lAgentLogin.Password);
                                Logger.LogInfo("Calling " + lWebBot.GetType().Name + $".FindTrips({aQuoteGroup.QuoteRequest.QuoteRequestID}, {aQuoteGroup.QuoteRequest.DepartureAirPort.Code}, {aQuoteGroup.QuoteRequest.DestinationAirPort.Code})");

                                // There seams to be an issue with advancing to fast
                                int lWaitTime = new Random().Next(1500, 3500);
                                System.Threading.Thread.Sleep(lWaitTime);

                                lResults = lWebBot.FindTrips(aQuoteGroup, aQuoteGroup.QuoteRequest.DepartureAirPort.Code, aQuoteGroup.QuoteRequest.DestinationAirPort.Code, aQuoteGroup.QuoteRequest.DepartureDate, aQuoteGroup.QuoteRequest.ReturnDate);
                                if (lResults != null)
                                {
                                    Logger.LogInfo($"{FuncName}FindTrips returned with {lResults.Hotels.Count()} resorts and {lResults.Flights.Count()} Flights");

                                    // Save
                                    {
                                        Staging.FlightHotelInformation lInjectedData = new Staging.FlightHotelInformation();
                                        lInjectedData.Flights = lWebBot.ProcessStagingFlights(lResults.Flights);
                                        lInjectedData.Hotels = lWebBot.ProcessStagingHotels(lResults.Hotels);
                                        new QuoteGroupBusiness(DbContext).SaveStagingData(lInjectedData, lWebBot.TourOperatorID, aQuoteGroup);
                                    }

                                    // Process
                                    {
                                        if (aQuoteGroup.QuoteRequest.QuoteType != QuoteRequest.QuoteTypes.LandOnly)
                                            lWebBot.ConvertFlightsFromStagingToProd(aQuoteGroup);
                                        lWebBot.ConvertResortsFromStagingToProd(aQuoteGroup, this);
                                    }
                                }
                                else
                                {
                                    Logger.LogWarning($"{FuncName} FindTrip returns nothing");
                                }
                                // If we reach this point, it means the Web bot executed successful and we can exit the loop
                                break;
                            }
                            catch (Exception e1)
                            {
                                Logger.LogException($"WebBot Exception with {lWebBot.ToString()}", e1);
                            }
                            finally
                            {
                                lWebBot.Close();
                            }
                            lExecutions++;
                        } while (lExecutions < 5);
                    }
                }

                aQuoteGroup.QuoteRequest.QuoteType = lQuoteType;
                new QuoteRequestBusiness(DbContext).Save(aQuoteGroup.QuoteRequest, aQuoteGroup.QuoteRequest.Agent);
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + "Exception executing quote request", e);
                throw e;
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }

        /// <summary>
        /// Determine if there are any issues before a user can run a quote
        /// </summary>
        /// <param name="lRequest"></param>
        /// <returns>Error Messages</returns>
        internal List<ErrorMsg> CheckForExeErrors(BlitzerCore.Models.UI.UIQuoteRequest aQuoteRequest, Agent aAgent)
        {
            var lOutput = new List<ErrorMsg>();
            // determine if the agent has a user and password for each of the WebBots
            foreach (var lWebBot in TourOperatorRegistry.GetAllWebBots(DbContext))
            {
                if (lWebBot.TourOperatorID != new TourOperatorDataAccess(DbContext).Get(TourOperator.DELTA_VACATIONS).Id)
                    continue;

                var lAgentLogin = new ContactDataAccess(DbContext).GetLogin(lWebBot.TourOperatorID, aAgent.Id);
                if (lAgentLogin == null)
                    lOutput.Add(new BlitzerDataAccess(DbContext).GetErrorMsg(GetErrorCode(lWebBot)));
            }

            return lOutput;
        }

        internal void CreateMissingLocations(List<Staging.HotelRate> lStaggedResortQuotes)
        {
            throw new NotImplementedException();
        }

        private DataHelper.ErrorCodes GetErrorCode(WebBots.WebBotBase aWebBot)
        {
            if (aWebBot.TourOperatorID == new TourOperatorDataAccess(DbContext).Get(TourOperator.DELTA_VACATIONS).Id)
                return DataHelper.ErrorCodes.DeltaVacationsNotSetup;
            else if (aWebBot.TourOperatorID == new TourOperatorDataAccess(DbContext).Get(TourOperator.AA_VACATIONS).Id)
                return DataHelper.ErrorCodes.AAVacationsNotSetup;

            var lErrMsg = "Unable to find ErrorCode for WebBot with ID = " + aWebBot.TourOperatorID;
            Logger.LogError(lErrMsg);
            throw new NotImplementedException(lErrMsg);
        }

        /// <summary>
        /// Call from the Edit Page when creating
        /// </summary>
        /// <param name="aQuoteRequest"></param>
        /// <param name="aAgent"></param>
        /// <returns></returns>
        public QuoteRequest Save(UIQuoteRequest aQuoteRequest, Agent aAgent)
        {
            string FuncName = ClassName + $"Save (UIQuoteRequest = {aQuoteRequest.QuoteID})";
            Logger.EnterFunction(FuncName);
            try
            {
                aQuoteRequest.AgentId = aAgent.Id;
                ProcessGUI(aQuoteRequest);
                var lQuoteRequest = QuoteRequestUIHelper.Convert(DbContext, aQuoteRequest);
                SaveWithClients(lQuoteRequest, aAgent, aQuoteRequest.Contacts);
                return lQuoteRequest;
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + "Failed to save quote request", e);
                throw e;
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }

        /// <summary>
        /// Called From the update page when only a subset of the inof is updated
        /// </summary>
        /// <param name="aQuoteRequest"></param>
        /// <param name="aAgent"></param>
        /// <returns></returns>
        public int Save(QuoteRequest aQuoteRequest, Agent aAgent)
        {
            string FuncName = ClassName + $"Save (QuoteRequest = {aQuoteRequest.QuoteRequestID})";
            Logger.EnterFunction(FuncName);
            try
            {
                aQuoteRequest.AgentId = aAgent.Id;
                return mDataAccess.Save(aQuoteRequest);
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + "Failed to save quote request", e);
                throw e;
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }

        private void ProcessGUI(UIQuoteRequest aQuoteRequest)
        {
            //if (aQuoteRequest.RefferalId == 0)
            //     aQuoteRequest.RefferalId = null;
        }

        public void CreateTasks(QuoteRequest lQuoteRequest, UIQuoteRequest aQuoteRequest)
        {
            if (aQuoteRequest.SendQuote)
                new TaskBusiness(DbContext).Create(lQuoteRequest, "Create Quote", DateTime.Now.AddDays(2));
            if (aQuoteRequest.SendInsurance)
                new TaskBusiness(DbContext).Create(lQuoteRequest, "Create Insurance Quote", DateTime.Now.AddDays(2));
        }

        /// <summary>
        /// Return a new Quote from a Quote Request
        /// <PostCondition>
        /// Created a Filter
        /// </PostCondition>
        /// </summary>
        /// <param name="lQuoteRequest"></param>
        /// <returns>Quote</returns>
        public QuoteGroup Search(QuoteGroup aQuoteGroup, Agent aAgent, List<IWebTravelSrv> aWebBots)
        {
            string FuncName = ClassName + "Search";
            Logger.EnterFunction(FuncName);
            try
            {
                var lQGBiz = new QuoteGroupBusiness(DbContext);
                var lQBiz = new QuoteBusiness(DbContext);
                // Quote Results is not populated
                Execute(aQuoteGroup, aAgent, GetTourOperators());
                var lFilter = new QuoteGroupDataAccess(DbContext).GetFilter(aQuoteGroup);
                if (lFilter == null)
                {
                    Logger.LogDebug("Pulling Default Filter");
                    lFilter = new FilterBusiness(DbContext).GetDefaultFilter(aQuoteGroup.QuoteRequest);
                } else
                {
                    Logger.LogDebug("Using Existing Filter");
                }
                lFilter.QuoteGroup = aQuoteGroup;
                // this will map the results to the Quote Group
                lQGBiz.ApplySubFilter(lFilter);
                new FilterBusiness(DbContext).Save(lFilter);
                aQuoteGroup.QuoteRequest.Finished = DateTime.Now;
                lQGBiz.Save(aQuoteGroup);
                Logger.LogInfo($"Quote Group has {aQuoteGroup.Quotes.Count()} manual quotes");
                Logger.LogInfo($"Quote Group has {aQuoteGroup.BotQuotes.Count()} Bot quotes");
                return aQuoteGroup;
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }

        public QuoteGroup GetOpenQuoteGroup(QuoteRequest aQuoteRequest)
        {
            var lQuoteGroups = aQuoteRequest.QuoteGroups.Where(x => x.Status == QuoteGroupStatus.Open);
            if (lQuoteGroups.Count() == 0)
            {
                var lQuoteGroup = new QuoteGroupBusiness(DbContext).Create(aQuoteRequest);
                aQuoteRequest.QuoteGroups.Add(lQuoteGroup);
                // Can't call save at this point.  Method is too generic and used in many places
                //Save(aQuoteRequest, aQuoteRequest.Agent);
                return lQuoteGroup;
            }
            return lQuoteGroups.First();
        }

        private void AddChildren(QuoteRequest aQuoteRequest, Agent aAgent)
        {
            var ContactBiz = new ContactBusiness(DbContext);
            if (aQuoteRequest.AgesOfKids != null && aQuoteRequest.AgesOfKids.Count > 0)
            {
                foreach (var lAge in aQuoteRequest.AgesOfKids)
                {
                    var lChild = ContactBiz.CreateChild(lAge, aAgent);
                    // Need to add kids to household
                    ContactBiz.AddHouseHoldMember(aQuoteRequest.Opportunity.Travelers[0].User, lChild);
                    aQuoteRequest.Opportunity.Travelers.Add(new UserMap() { UserID = lChild.Id, User = lChild, OpportunityID = aQuoteRequest.Opportunity.ID });
                }
            }
        }



        private void Save(Hotel aNewHotel, AccommodationDataAccess aAccommodationDA = null, bool aSaveChange = true)
        {
            AccommodationDataAccess lDA = aAccommodationDA;
            if (lDA == null)
                lDA = new AccommodationDataAccess(DbContext);

            lDA.AddAccommodation(aNewHotel);
            if (aSaveChange == true)
                DbContext.SaveChanges();
        }


        public void CreateResortResults(BlitzerCore.Models.QuoteGroup aQuoteGroup, List<Hotel> aHotels, List<RoomType> aRoomTypes, List<Staging.HotelRate> lStaggedResortQuotes, IWebTravelSrv aWebSrv)
        {
            string FuncName = ClassName + $"CreateResortResults (int = {aQuoteGroup.Id}, QuoteRequest) - ";
            List<QuoteRequestResort> lExistingResorts = new QuoteRequestDataAccess(DbContext).GetAll(aQuoteGroup);
            foreach (var lResortStagging in lStaggedResortQuotes)
            {
                //Stopwatch lSW = Logger.StartStopWatch();
                QuoteRequestResort lResort = new QuoteRequestResort();
                //Logger.StopWatchElapsedTime(lSW, "Create Resort");
                var lHotel = aHotels.Where(x => x.Name == aWebSrv.GetDBConverter().GetName(lResortStagging.HotelStaging.Name)).FirstOrDefault();
                //Logger.StopWatchElapsedTime(lSW, "Find Hotel");
                if (lHotel != null)
                {
                    lResort.ResortId = lHotel.Id;
                    lResort.QuoteGroupId = aQuoteGroup.Id;
                    lResort.LandOnly = lResortStagging.LandOnly;
                    lResort.MealPlan = GetMealPlan(lHotel, lResortStagging);
                    //Logger.StopWatchElapsedTime(lSW, "Create Meal Plan");
                    lResort.TourOperatorID = lResortStagging.HotelStaging.TourOperatorID;
                    var lRoomType = new HotelBusiness(DbContext).GetRoomType(DbContext, aQuoteGroup, aRoomTypes, lResortStagging, lResort.ResortId);
                    if (lRoomType.SKUID > 0)
                        lResort.ResortRoomTypeID = lRoomType.SKUID;
                    else
                        lResort.ResortRoomType = lRoomType;
                    lResort.RoomURL = lRoomType.URL;
                    //Logger.StopWatchElapsedTime(lSW, "GetRoom Type");
                    if (lResortStagging.Price != null)
                    {
                        string lsPrice = lResortStagging.Price.Replace("\"", "");
                        string lsChildPrice = null;
                        if (lResortStagging.ChildPrice != null)
                            lsChildPrice = lResortStagging.ChildPrice.Replace("\"", "");
                        double lPrice = 0;
                        double lChildPrice = 0;
                        if (lResortStagging.HotelStaging.Price != null)
                        {
                            lPrice = DataHelper.ConvertFromCurrency(lsPrice) + DataHelper.ConvertFromCurrency(lResortStagging.HotelStaging.Price);
                            lChildPrice = DataHelper.ConvertFromCurrency(lsChildPrice) + DataHelper.ConvertFromCurrency(lResortStagging.HotelStaging.ChildPrice);
                        }
                        else
                        {
                            lPrice = DataHelper.ConvertFromCurrency(lsPrice);
                            lChildPrice = DataHelper.ConvertFromCurrency(lsChildPrice);
                        }

                        lResort.Price = ComputePrice(lPrice, lChildPrice, aQuoteGroup, aWebSrv);
                    }
                    else
                        lResort.Price = -1;


                    //Logger.StopWatchElapsedTime(lSW, "Parse");
                    if (DoesRequestResortExist(lExistingResorts, lResort) == false)
                        new QuoteRequestDataAccess(DbContext).Save(lResort, false);
                }
                else
                {
                    Logger.LogWarning(FuncName + "Failed to Merging Staging Hoels into existing hotel hotel named [" + DataHelper.ConvertWebString(lResortStagging.HotelStaging.Name) + "]");
                }
            }

            try
            {
                var lCnt = DbContext.SaveChanges();
                Logger.LogInfo(FuncName + $" - Updated {lCnt} QuoteRequestResorts rows");
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + " - Failed to Save QuoteRequestResorts", e);
            }
        }

        private double ComputePrice(double aBasePrice, double aChildPrice, QuoteGroup aQuoteGroup, IWebTravelSrv aWebSrv)
        {
            var lNumOfChildren = GetNumberOfChildren(aQuoteGroup.QuoteRequest);
            var lHotel = aBasePrice * aWebSrv.getPriceMultiplier(aQuoteGroup);
            if (aChildPrice > 1)
                lHotel += aChildPrice * aWebSrv.getChildPriceMultiplier(aQuoteGroup);
            else
                lHotel += aBasePrice * aWebSrv.getChildPriceMultiplier(aQuoteGroup);
            if (aQuoteGroup.SelectedQuoteRequestTicket == null)
                return lHotel;

            var lAir = aQuoteGroup.SelectedQuoteRequestTicket.ExtraCost * aQuoteGroup.QuoteRequest.NumberOfAdults + aQuoteGroup.SelectedQuoteRequestTicket.ExtraCost * lNumOfChildren;
            return lHotel + lAir;
        }
        private bool DoesRequestResortExist(List<QuoteRequestResort> aExistingResorts, QuoteRequestResort aResort)
        {
            var lExist = aExistingResorts.Count(x => x.ResortId == aResort.ResortId
            && x.ResortRoomTypeID == aResort.ResortRoomTypeID
            && x.TourOperatorID == aResort.TourOperatorID) > 0;

            if (lExist == false)
            {
                var lCnt = aExistingResorts.Count(x => x.ResortId == aResort.ResortId
           && x.TourOperatorID == aResort.TourOperatorID);

            }

            return lExist;
        }

        public int SaveExcludedRoomTypes(IEnumerable<int> lQRRIDs)
        {
            return new QuoteDataAccess(DbContext).SaveExcludedRoomTypes(lQRRIDs);
        }

        private string GetMealPlan(Hotel aResort, Staging.HotelRate aStagingRate)
        {
            if (aStagingRate.RateType != null)
                return new MealPlan(aStagingRate.RateType).Name;

            return new MealPlan(aStagingRate.HotelStaging).Name;
        }

        private void UpdateAccommodationsFromStagging(int lQuoteRequestID)
        {
            var lHotels = DbContext.Accommodations;
            var lStaggedHotels = DbContext.Staging_Hotels;
            foreach (var lStaggedHotel in lStaggedHotels)
            {
                var lHotel = lHotels.Where(x => x.Name == lStaggedHotel.Name.Trim()).FirstOrDefault();
                if (lHotel != null && (lHotel.Area == null || lHotel.Area.Length == 0))
                {
                    lHotel.Area = lStaggedHotel.Location;
                }
            }
            int lUpdatedRows = DbContext.SaveChanges();
            Logger.LogInfo("Updated " + lUpdatedRows + " Hotels with new locations");
        }

        public void DeleteQuoteRequestResorts(QuoteGroup aQuoteGroup, int aOperatorID)
        {
            try
            {
                var lQuotes = DbContext.QuoteRequestResorts.Where(x => x.QuoteGroupId == aQuoteGroup.Id && x.TourOperatorID == aOperatorID);
                DbContext.QuoteRequestResorts.RemoveRange(lQuotes);
                var lCount = DbContext.SaveChanges();
                Logger.LogInfo("Removing Previous Query Results : Removed " + lCount + " QuoteRequestResorts for QuoteGroupID = " + aQuoteGroup.Id);
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to DeleteQuoteRequestResorts for QuoteID = " + aQuoteGroup.Id, e);
            }
        }

    }
}
