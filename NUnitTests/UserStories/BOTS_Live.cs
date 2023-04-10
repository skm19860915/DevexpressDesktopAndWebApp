using System.Collections.Generic;
using NUnit.Framework;
using BlitzerCore.Business;
using BlitzerCore.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApp.DataServices;
using System.IO;
using WebApp.Controllers;
using BlitzerCore.DataAccess;
using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NUnitTests.Helpers;
using Microsoft.Extensions.Configuration;
using BlitzerCore.WebBots;
using NUnitTests.ServiceStubs;
using Microsoft.AspNetCore.Mvc;
using BlitzerCore.Utilities;


namespace NUnitTests.UserStories
{
    public class BOTS
    {
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
            var lDataLoaders = new List<IWebTravelSrv> { new WADStaticFileBot(DbContext) };
            ((WADStaticFileBot)lDataLoaders[0]).LoadBulk();

            // After Hotels loaded to Prod, Update the Hotel Attributes
            var lQRBiz = new QuoteRequestBusiness(DbContext, null);
            var lQBiz = new QuoteBusiness(DbContext, null);
            string lDepartCode = "RDU";
            string lDestCode = "CUN";
            var lAgent = new ContactDataAccess(DbContext).GetAgents().First();

            var lUIRequest = DataLake.CreateUIQuoteRequest(REQUEST_ID, lDepartCode, lDestCode);
            var lRequest = lQRBiz.Save(lUIRequest, lAgent);
            // Creates Quote Request & Executes Query
            var lQuote = lQRBiz.Search(lRequest.QuoteGroups.First(), lAgent, lDataLoaders);

            var lFlightCnt = DbContext.FlightItineraries.Where(x => x.QuoteRequestID == lRequest.QuoteRequestID).Count();
            var lResortsCnt = DbContext.QuoteRequestResorts.Where(x => x.QuoteGroupId == lRequest.QuoteGroups.First().Id).Count();
            Assert.AreEqual(114, lFlightCnt, "Default Filter didn't return correct number of flights");
            Assert.AreEqual(701, lResortsCnt, "Default Filter didn't return the correct number of Hotels");
        }
    }
}
