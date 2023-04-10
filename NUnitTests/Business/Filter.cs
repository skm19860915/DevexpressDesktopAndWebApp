using System.Collections.Generic;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApp.DataServices;
using BlitzerCore.DataAccess;
using NUnitTests.Helpers;
using BlitzerCore.Business;
using BlitzerCore.Models;
using WebApp.Controllers;
using BlitzerCore.WebBots;
using BlitzerCore.Utilities;

namespace NUnitTests.Business
{
    [TestFixture]
    public class Filter
    {
        RepositoryContext mContext = null;
        DbContextOptions<WebApp.DataServices.RepositoryContext> mDBOptions = null;
        const int REQUEST_ID = 100;

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

        [Test]

        public void AdultsOnlyFilter()
        {
            int REQUEST_ID = 345;
            var lWebBots = new List<IWebTravelSrv>() { new NUnitTests.ServiceStubs.WorldAgentDirectStub(mContext) };
            var lQRBiz = new BlitzerCore.Business.QuoteRequestBusiness(mContext);
            AirPort lDepart = DataLake.GetAirPorts().ElementAt(2);
            AirPort lDest = DataLake.GetAirPorts().ElementAt(5);
            var lAgent = new ContactBusiness(mContext).Get(DataLake.GetAgents()[0].Id) as Agent;
            var lUIRequest = DataLake.CreateUIQuoteRequest(REQUEST_ID, lDepart.Code, lDest.Code);
            var lRequest = lQRBiz.Save(lUIRequest, lAgent);

            AgentAirPortPreference lFilter = new FilterDataAccess(mContext).GetAgentDefaultFilter(lAgent, lDest);
            //lFilter.AllInclusive = true;
            lFilter.AdultOnly = true;
            new FilterDataAccess(mContext).Save(lFilter);

            // Creates Quote Request & Executes Query
            var lQuoteGroup = lQRBiz.Search(lRequest.QuoteGroups.First(), lAgent, lWebBots);
            mContext.SaveChanges();
            Assert.AreEqual(353, lQuoteGroup.BotQuotes.Count);
        }

        [Test]
        public void ApplyDefaultFilter()
        {
            DataLake.ClearStagingData(mContext);
            var lWebBots = new List<IWebTravelSrv>() { new WADStaticFileBot(mContext) };
            ((WADStaticFileBot)lWebBots[0]).LoadBulk();

            DataLake.SetAdultsOnlyResorts(mContext);
            Assert.AreEqual(3, new AccommodationDataAccess(mContext).GetByAmentity(Amenity.AmenityTypes.AdultsOnly).Count(), "Adults only filter is not working");

            // After Hotels loaded to Prod, Update the Hotel Attributes
            var lQRBiz = new QuoteRequestBusiness(mContext);
            var lQBiz = new QuoteBusiness(mContext, null);
            AirPort lDepart = DataLake.GetAirPorts().ElementAt(2);
            AirPort lDest = DataLake.GetAirPorts().ElementAt(5);
            var lAgent = DataLake.GetAgents()[0];

            var lUIRequest = DataLake.CreateUIQuoteRequest(REQUEST_ID, lDepart.Code, lDest.Code);
            var lRequest = lQRBiz.Save(lUIRequest, DataLake.GetAgents()[0]);
            // Creates Quote Request & Executes Query
            var lQuote = lQRBiz.Search(lRequest.QuoteGroups.First(), lAgent, lWebBots);
            var lQuoteGroup = lRequest.QuoteGroups.First();

            var lQtoRMapper = mContext.QuoteToResultsMappers.Where(x => x.QuoteGroup == lQuoteGroup);
            Assert.AreEqual(114, lQtoRMapper.Count(x => x.FlightItinerary != null), "Default Filter didn't return correct number of flights");
            Assert.AreEqual(239, lQtoRMapper.Count(x => x.QuoteRequestResort != null), "Default Filter didn't return the correct number of Hotels");
        }

        [Test]
        /*
         *  Verify Resort Filter returns all data if no filter specified
         */
        public void ValidateNonStopFilter()
        {
            var lQuoteBiz = new QuoteBusiness(mContext, null);
            var lQRBiz = new QuoteRequestBusiness(mContext);
            var lQRController = new QuoteRequestController(mContext, null, null, null);

            var lUIRequest = DataLake.CreateUIQuoteRequest(REQUEST_ID);
            var lRequest = lQRBiz.Save(lUIRequest, DataLake.GetAgents()[0]);
            lQRController.Search(lRequest.QuoteRequestID, new List<IWebTravelSrv>());
            var lNonStops = lRequest.QuoteGroups.First().BotQuotes.Count(x => x.FlightItinerary != null && x.FlightItinerary.InBound.Stops == 0);

            Assert.AreEqual(74, lNonStops);
        }
    }
}
