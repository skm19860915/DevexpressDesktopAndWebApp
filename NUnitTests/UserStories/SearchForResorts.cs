using System.Collections.Generic;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApp.DataServices;
using System.IO;
using WebApp.Controllers;
using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NUnitTests.Helpers;
using NUnitTests.ServiceStubs;
using Microsoft.AspNetCore.Mvc;
using BlitzerCore.DataAccess;
using BlitzerCore.Business;
using BlitzerCore.Models;
using BlitzerCore.UIHelpers;
using BlitzerCore.WebBots;
using BlitzerCore.Utilities;

namespace NUnitTests.UserStories
{
    class SearchForResorts
    {
        const string ClassName = "SearchForResorts::";
        const int REQUEST_ID = 100;
        RepositoryContext DbContext { get; set; }
        DbContextOptions<RepositoryContext> mDBOptions = null;

        [SetUp]
        public void CreateInMemoryContext()
        {
            mDBOptions = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            DbContext = new RepositoryContext(mDBOptions);
            if (DbContext != null)
            {
                DbContext.Database.EnsureDeleted();
                DbContext.Database.EnsureCreated();
                DataLake.Init(DbContext);
            }
        }

        [Test]
        public void ExecuteSearchForQuoteRequest()
        {

            DataLake.ClearStagingData(DbContext);
            var lDataLoaders = new List<IWebTravelSrv>() { new WADStaticFileBot(DbContext) };
            ((WADStaticFileBot)lDataLoaders[0]).LoadBulk();

            DataLake.SetAdultsOnlyResorts(DbContext);
            Assert.AreEqual(3, new AccommodationDataAccess(DbContext).GetByAmentity(Amenity.AmenityTypes.AdultsOnly).Count(), "Adults only filter is not working");

            // After Hotels loaded to Prod, Update the Hotel Attributes
            var lQRBiz = new QuoteRequestBusiness(DbContext, null);
            var lQBiz = new QuoteBusiness(DbContext, null);
            AirPort lDepart = DataLake.GetAirPorts().ElementAt(2);
            AirPort lDest = DataLake.GetAirPorts().ElementAt(5);
            var lAgent = DataLake.GetAgents()[0];

            var lUIRequest = DataLake.CreateUIQuoteRequest(REQUEST_ID, lDepart.Code, lDest.Code);
            var lRequest = lQRBiz.Save(lUIRequest, DataLake.GetAgents()[0]);
            // Creates Quote Request & Executes Query
            var lInputQuoteGroup = lQRBiz.GetOpenQuoteGroup(lRequest);

            var lQuoteGroup = lQRBiz.Search(lInputQuoteGroup, lAgent, lDataLoaders);

            var lFlightCnt = DbContext.FlightItineraries.Where(x => x.QuoteGroupId == lRequest.QuoteRequestID).Count();
            var lResortsCnt = DbContext.QuoteRequestResorts.Where(x => x.QuoteGroupId == lRequest.QuoteRequestID).Count();
            Assert.AreEqual(114, lFlightCnt, "Default Filter didn't return correct number of flights");
            Assert.AreEqual(701, lResortsCnt, "Default Filter didn't return the correct number of Hotels");

            var lOutputQR = lQRBiz.Get(lRequest.QuoteRequestID);
            Assert.AreEqual(353, lOutputQR.QuoteGroups.First().BotQuotes.Count());
        }

        [Test]
        public void CreateHotels()
        {

            DataLake.ClearStagingData(DbContext);
            var lDataLoaders = new List<IWebTravelSrv>() { new WADStaticFileBot(DbContext) };
            ((WADStaticFileBot)lDataLoaders[0]).LoadBulk();
            var lWebSrv =
                new BlitzerBusiness(DbContext, null).GetWebService(
                    new TourOperatorBusiness(DbContext).Get(TourOperator.VACATION_EXPRESS));

            //DataLake.SetAdultsOnlyResorts(DbContext);
            //Assert.AreEqual(3, new AccommodationDataAccess(DbContext).GetByAmentity(Amenity.AmenityTypes.AdultsOnly).Count(), "Adults only filter is not working");

            // After Hotels loaded to Prod, Update the Hotel Attributes
            var lQRBiz = new QuoteRequestBusiness(DbContext, null);
            var lQBiz = new QuoteBusiness(DbContext, null);
            AirPort lDepart = DataLake.GetAirPorts().ElementAt(2);
            AirPort lDest = DataLake.GetAirPorts().ElementAt(5);
            var lAgent = DataLake.GetAgents()[0];

            var lUIRequest = DataLake.CreateUIQuoteRequest(REQUEST_ID, lDepart.Code, lDest.Code);
            var lRequest = lQRBiz.Save(lUIRequest, DataLake.GetAgents()[0]);
            // Creates Quote Request & Executes Query
            var lInputQuoteGroup = lQRBiz.GetOpenQuoteGroup(lRequest);

            var lHBiz = new HotelBusiness(DbContext);
            var lStagingHotels = new List<Staging.Hotel>();
            var lHDA = new HotelDataAccess(DbContext);
            for (int i = 0; i < 100; i++)
            {
                var lStagHotel = new Staging.Hotel()
                { Name = $"Staging Hotel-{i}", QuoteGroup = lInputQuoteGroup, TourOperatorID = 115 };
                lStagingHotels.Add(lStagHotel);
                //var lHotel = lHBiz.CreateHotel(lStagHotel, lInputQuoteGroup, lWebSrv);
                var lHotel = new Hotel() {Name = $"Hotel-{i}", QuoteRequestID = lRequest.QuoteRequestID};
                lHDA.Save(lHotel);
            }
            //lHBiz.CreateMissingAccommodations(lInputQuoteGroup, lStagingHotels, lWebSrv);



        }

        [Test]
        public void CleanUpAfterBotSearch()
        {

            DataLake.ClearStagingData(DbContext);
            var lDataLoaders = new List<IWebTravelSrv>() { new WADStaticFileBot(DbContext) };
            ((WADStaticFileBot)lDataLoaders[0]).FilterHotels = false;
            ((WADStaticFileBot)lDataLoaders[0]).LoadBulk();

            // After Hotels loaded to Prod, Update the Hotel Attributes
            var lQRBiz = new QuoteRequestBusiness(DbContext, null);
            var lQBiz = new QuoteBusiness(DbContext, null);
            AirPort lDepart = DataLake.GetAirPorts().ElementAt(2);
            AirPort lDest = DataLake.GetAirPorts().ElementAt(5);
            var lAgent = DataLake.GetAgents()[0];

            var lUIRequest = DataLake.CreateUIQuoteRequest(REQUEST_ID, lDepart.Code, lDest.Code);
            var lRequest = lQRBiz.Save(lUIRequest, DataLake.GetAgents()[0]);
            var lInputQG = lQRBiz.GetOpenQuoteGroup(lRequest);
            // Creates Quote Request & Executes Query
            var lQuoteGroup = lQRBiz.Search(lInputQG, lAgent, lDataLoaders);
            var lRoomTypeCnt_Pre = DbContext.SKUs.Count();
            lQuoteGroup = lQRBiz.Search(lInputQG, lAgent, lDataLoaders);
            var lRoomTypeCnt_Post = DbContext.SKUs.Count();
            Assert.AreEqual(lRoomTypeCnt_Pre, lRoomTypeCnt_Post);
        }

    }
}
