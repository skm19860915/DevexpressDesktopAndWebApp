using System.Collections.Generic;
using NUnit.Framework;
using BlitzerCore.Business;
using BlitzerCore.DataAccess;
using BlitzerCore.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApp.DataServices;
using WebApp.Controllers;
using NUnitTests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using NUnitTests.Bots;
using BlitzerCore.WebBots;
using BlitzerCore.UIHelpers;
using BlitzerCore.Utilities;

namespace NUnitTests.Controllers
{
    public class QuoteRequest
    {
        const int REQUEST_ID = 100;
        const string ClassName = "NUnitTests.Controllers.QuoteRequest::";
        RepositoryContext mContext = null;
        DbContextOptions<RepositoryContext> mDBOptions = null;

        [SetUp]
        public void CreateInMemoryContext()
        {
            mDBOptions = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            mContext = new RepositoryContext(mDBOptions);
            if (mContext != null)
            {
                mContext.Database.EnsureDeleted();
                mContext.Database.EnsureCreated();
                DataLake.Init(mContext);
            }
        }

        Dictionary<int, double> CreateQuoteData()
        {
            Dictionary<int, double> lResortQuotes = new Dictionary<int, double>();
            lResortQuotes[0] = 123456.00;
            lResortQuotes[1] = 789123.00;
            lResortQuotes[2] = 11111.00;
            lResortQuotes[3] = 222.34;
            lResortQuotes[4] = 3333.55;
            lResortQuotes[5] = 445678.66;
            lResortQuotes[6] = 99999.99;
            lResortQuotes[7] = 34.56;
            return lResortQuotes;
        }

        [Test]
        public void Search_StaticLoad()
        {
            var lTravelServices = new List<IWebTravelSrv>() { new WADStaticFileBot(mContext) };
            var lQuoteBiz = new QuoteRequestBusiness(mContext,null);
            AirPort lDepart = DataLake.GetAirPorts().ElementAt(2);
            AirPort lDest = DataLake.GetAirPorts().ElementAt(5);
            var lAgent = DataLake.GetAgents()[0];

            var lUIRequest = DataLake.CreateUIQuoteRequest(REQUEST_ID, lDepart.Code, lDest.Code);
            var lQuoteRequest = lQuoteBiz.Save(lUIRequest, lAgent);
            RedirectToActionResult lRequest = (RedirectToActionResult)new QuoteRequestController(mContext, null, null, null).Search(lQuoteRequest.QuoteRequestID, lTravelServices);

        }

        [Test]
        public void Search_AllInclusive()
        {
            string FuncName = ClassName + $"Search_AllInclusive - ";
            try
            {
                Logger.EnterFunction(FuncName);
                var lTravelServices = new List<IWebTravelSrv>() { new NUnitTests.ServiceStubs.WorldAgentDirectStub(mContext) };
                var lQuoteBiz = new QuoteRequestBusiness(mContext, null);
                AirPort lDepart = DataLake.GetAirPorts().ElementAt(2);
                AirPort lDest = DataLake.GetAirPorts().ElementAt(5);
                var lAgent = DataLake.GetAgents()[0];

                // Need to Create Another Default filter for the current agent with All Inclusive
                AgentAirPortPreference lPref = new AgentAirPortPreference()
                {
                    AirportID = lDest.AirPortID,
                    AgentId = lAgent.Id,
                    AllInclusive = true
                };
                new FilterDataAccess(mContext).Save(lPref);

                // INit Proces
                Logger.LogInfo(FuncName + $"There are {mContext.Accommodations.Count()} Hotels to start with");

                var lUIRequest = DataLake.CreateUIQuoteRequest(REQUEST_ID, lDepart.Code, lDest.Code);
                new OpportunityController(mContext, null, null, null).New(lUIRequest);
                RedirectToActionResult lRequest = (RedirectToActionResult)new QuoteRequestController(mContext, null, null, null).Search(mContext.QuoteRequests.First().QuoteRequestID, lTravelServices);

                // Verify 50 Tickets with 4 flights Created from service provider
                var lTicket = mContext.FlightItineraries;
                Assert.AreEqual(114, lTicket.Count());

                // Verify 50 Hotels Created
                var lHotels = mContext.QuoteRequestResorts.ToList();
                Assert.AreEqual(701, lHotels.Count());
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }

        [Test]
        /*
         *  Verify Resort Filter returns all data if no filter specified
         */
        public void ExecuteQueryAndReturnRawData()
        {
            var lContext = mContext;
            var lTourOperators = new List<TourOperator>() { new CompanyBusiness(mContext).Get(DataLake.VE_AIRLINESID) as TourOperator };

            var lQuoteBiz = new QuoteRequestBusiness(lContext, null);
            AirPort lDepart = DataLake.GetAirPorts().ElementAt(2);
            AirPort lDest = DataLake.GetAirPorts().ElementAt(5);
            var lAgent = DataLake.GetAgents()[0];

            var lUIRequest = DataLake.CreateUIQuoteRequest(REQUEST_ID, lDepart.Code, lDest.Code);
            var lRequest = lQuoteBiz.Save(lUIRequest, DataLake.GetAgents()[0]);
            var lQuoteGroup = lQuoteBiz.GetOpenQuoteGroup(lRequest);
            lQuoteBiz.Execute(lQuoteGroup, lAgent, lTourOperators);

            // Verify Quote Created
            var lQuoteRequest = mContext.QuoteRequests.FirstOrDefault();
            Assert.IsNotNull(lQuoteRequest);
            Assert.AreEqual(lDepart.AirPortID, lQuoteRequest.DepartureAirPortID);
            Assert.AreEqual(lDest.AirPortID, lQuoteRequest.DestinationAirPortID);

            // Verify 2 AirLine Tickets Created
            var lTicket = mContext.FlightItineraries;
            Assert.AreEqual(64, lTicket.Count());

            // Verify 4 Hotels Created
            var lQuotes = mContext.QuoteGroups.ToList()[0].Quotes;
            Assert.AreEqual(112, mContext.QuoteRequestResorts.Count());
            //var lHotelQuote0 = lQuotes.Where(x => x.AccommodationRoomType.Name == (DataLake.GetRoomTypes()[0].RoomType)).FirstOrDefault();
            //var lHotelQuote1 = lQuotes.Where(x => x.AccommodationRoomType.Name == (DataLake.GetRoomTypes()[1].RoomType)).FirstOrDefault();
            //var lHotelQuote2 = lQuotes.Where(x => x.AccommodationRoomType.Name == (DataLake.GetRoomTypes()[2].RoomType)).FirstOrDefault();
            //var lHotelQuote3 = lQuotes.Where(x => x.AccommodationRoomType.Name == (DataLake.GetRoomTypes()[3].RoomType)).FirstOrDefault();
            //var lHotelQuote4 = lQuotes.Where(x => x.AccommodationRoomType.Name == (DataLake.GetRoomTypes()[4].RoomType)).FirstOrDefault();

            //Assert.AreEqual(DataLake.GetRoomTypes()[0].RoomType, lHotelQuote0.AccommodationRoomType.Name);
            //Assert.AreEqual(DataLake.GetRoomTypes()[1].RoomType, lHotelQuote1.AccommodationRoomType.Name);
            //Assert.AreEqual(decimal.Parse(DataLake.GetRoomTypes()[0].Price, System.Globalization.NumberStyles.Currency), lHotelQuote0.SubTotal);
            //Assert.AreEqual(decimal.Parse(DataLake.GetRoomTypes()[1].Price, System.Globalization.NumberStyles.Currency), lHotelQuote1.SubTotal);
            //Assert.AreEqual(442, lHotelQuote2.SubTotal);
            //Assert.AreEqual(decimal.Parse(DataLake.GetRoomTypes()[3].Price, System.Globalization.NumberStyles.Currency), lHotelQuote3.SubTotal);

            Assert.AreEqual(lUIRequest.DepartureCityCode, lQuoteRequest.DepartureAirPort.Code);
            Assert.AreEqual(lUIRequest.DestinationCityCode, lQuoteRequest.DestinationAirPort.Code);
            Assert.AreEqual(BlitzerCore.Helpers.DateHelper.ConvertDate(lUIRequest.StartDate).Value.ToShortDateString(), lQuoteRequest.DepartureDate.ToShortDateString());
            Assert.AreEqual(BlitzerCore.Helpers.DateHelper.ConvertDate(lUIRequest.EndDate).Value.ToShortDateString(), lQuoteRequest.ReturnDate.ToShortDateString());
            Assert.AreEqual(lQuoteRequest.AgentId, lQuoteRequest.AgentId);
            Assert.AreEqual(lUIRequest.Contacts[0].First, lQuoteRequest.Opportunity.Travelers[0].User.First);
            Assert.AreEqual(lUIRequest.Contacts[0].Last, lQuoteRequest.Opportunity.Travelers[0].User.Last);

        }


        /// <summary>
        /// Must Locate existing Opportunity for the same primary lead if within 60 day window 
        /// and NOT create a new Opportunity
        /// </summary>
        [Test]
        public void FindExistingOppOnQuoteRequestSave()
        {
            var lWebBots = new List<IWebTravelSrv>() { new NUnitTests.ServiceStubs.WorldAgentDirectStub(mContext) };
            var lQuoteBiz = new QuoteRequestBusiness(mContext, null);

            // Add First Quote
            AirPort lDepart = DataLake.GetAirPorts().ElementAt(2);
            AirPort lDest = DataLake.GetAirPorts().ElementAt(5);
            var lAgent = DataLake.GetAgents()[0];

            var lUIRequest = DataLake.CreateUIQuoteRequest(REQUEST_ID, lDepart.Code, lDest.Code);
            var lQuoteController = new QuoteController(mContext,null);
            var lQuoteRequestController = new QuoteRequestController(mContext, null, null, null);
            var lQuoteRequest = lQuoteBiz.Save(lUIRequest, lAgent);
            var lQuoteGroup = lQuoteBiz.GetOpenQuoteGroup(lQuoteRequest);
            //lQuoteBiz.Execute(lQuoteGroup, lAgent, lWebBots);

            var lSavedQuoteRequest = lQuoteBiz.GetQuoteRequest(lQuoteRequest.QuoteRequestID);
            var lFirstOpportunity = lSavedQuoteRequest.Opportunity;

            // Execute Second Quote with Different Dates
            var lUIQuoteRequest = DataLake.CreateUIQuoteRequest();
            lUIQuoteRequest.StartDate = "10/5/2021";
            lUIQuoteRequest.EndDate = "10/15/2021";
            lUIQuoteRequest.Contacts.AddRange(DataLake.CreateUIContacts());
            var lActionResult = lQuoteRequestController.Search(lQuoteRequest.QuoteRequestID, lWebBots);

            // Validate the QuoteRequest Saved
            var lRedirectResult = lActionResult as RedirectToActionResult;
            Assert.NotNull(lRedirectResult, "Failed to save Quote Request");

            //Get New Opportuity ID
            var lRequestQuoteID = (int)lRedirectResult.RouteValues.ElementAt(0).Value;
            var lSecondSavedQuoteRequest = lQuoteBiz.GetQuoteRequest(lRequestQuoteID);
            var lSecondOpportunity = lSavedQuoteRequest.Opportunity;

            Assert.IsNotNull(lSecondOpportunity, "Failed to find Opportunity for saved QuoteRequest");
            Assert.AreEqual(lSavedQuoteRequest.Opportunity.Travelers.Count(), DataLake.CreateUIContacts().Count);
            Assert.AreEqual(lFirstOpportunity.ID, lSecondOpportunity.ID);
            Assert.AreEqual(lSavedQuoteRequest.AgentId, lSecondOpportunity.AgentId);
            Assert.AreEqual(lSavedQuoteRequest.DepartureAirPortID, lSecondOpportunity.OutboundAirPortID);
            Assert.AreEqual(lSavedQuoteRequest.DestinationAirPortID, lSecondOpportunity.InboundAirPortID);
        }
    }
}
