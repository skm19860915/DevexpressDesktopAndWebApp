using System;
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
using BlitzerCore.Models.UI;
using BlitzerCore.UIHelpers;
using NUnitTests.Bots;
using BlitzerCore.WebBots;

namespace NUnitTests.Controllers
{
    [TestFixture]
    public class Trip
    {
        const int REQUEST_ID = 462;
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

        [Test]

        public void BookTrip()
        {
            // Save New Quote
            var lWebBots = new List<IWebTravelSrv>() {new NUnitTests.ServiceStubs.WorldAgentDirectStub(mContext)};
            ((NUnitTests.ServiceStubs.WorldAgentDirectStub) lWebBots[0]).LoadBulk();
            var lQRBiz = new QuoteRequestBusiness(mContext, null);
            var lQGBiz = new QuoteGroupBusiness(mContext);
            AirPort lDepart = DataLake.GetAirPorts().ElementAt(2);
            AirPort lDest = DataLake.GetAirPorts().ElementAt(5);
            var lAgent = DataLake.GetAgents()[0];

            /********************* Search by Quote Request *******************************/
            var lUIRequest = DataLake.CreateUIQuoteRequest(0, lDepart.Code, lDest.Code);
            var lQuoteRequest = lQRBiz.Save(lUIRequest, lAgent);
            new QuoteRequestController(mContext, null, null, null).Search(lQuoteRequest.QuoteRequestID, lWebBots);

            // Get the UIQuote
            var lActionResult = new QuoteController(mContext, null).View(1);
            var lUIQG = QuoteGroupUIHelper.Convert(mContext, lQuoteRequest.QuoteGroups.First());
            var lQRE = QuoteRequestEditUIHelper.Convert(mContext, lQuoteRequest.QuoteGroups.First().Quotes,
                lQuoteRequest.Opportunity.Travelers.First().User, true);

            mContext.UnTrack(lQuoteRequest.Opportunity);
            var lQuotes = lQGBiz.GetBestQuotesByTourOperator(lQuoteRequest.QuoteGroups.First(), lAgent);
            var lTopQuotes = lQGBiz.GetTop5Resorts(lQuotes);
            var lUIQRE = QuoteRequestEditUIHelper
                .Convert(mContext, lTopQuotes, lQuoteRequest.Opportunity.Travelers.First().User, false).ToList();

            lUIQRE.FirstOrDefault().Booked = true;
            new TripBusiness(mContext).Book(lUIQRE.FirstOrDefault(), lQuoteRequest.Opportunity.Travelers.First().User);
            Assert.AreEqual(1, mContext.Opportunities.Count(x => x.Stage == OpportunityStages.Won));
            var lTripID = 1;

            var lOpp = new OpportunityBusiness(mContext).GetOpportunity(1);
            var lTrip = new TripBusiness(mContext).Get(lTripID);
            Assert.AreEqual(lTripID, lTrip.ID);
            Assert.AreEqual(lOpp.Name, lTrip.Name);
            Assert.AreEqual(lOpp.InboundAirPortID, lTrip.InboundAirPortID);
            Assert.AreEqual(lOpp.OutboundAirPortID, lTrip.OutboundAirPortID);
            Assert.AreEqual(lOpp.QuoteRequests.Count(), lTrip.QuoteRequests.Count());
            Assert.AreEqual(OpportunityStages.Won, lTrip.Stage);
            Assert.AreEqual(lOpp.Travelers.Count(), lTrip.Travelers.Count());
            Assert.AreEqual(BlitzerCore.Models.Trip.Statuses.Active, lTrip.TripStatus);

        }

        [Test]

        public void TripMarkedAsTraveled()
        {
            var lTrip = new BlitzerCore.Models.Trip()
            {
                StartDate = DateTime.Now.Add(new TimeSpan(60, 0, 0, 0)),
                EndDate = DateTime.Now.Add(new TimeSpan(55, 0, 0, 0)),
                Bookings = new List<Booking>() {new Booking() {Status = BookingStatus.PaidInFull}}
            };
            Assert.AreEqual(TripStage.CompleteProfile, new BlitzerCore.Business.TripBusiness(null).GetStage(lTrip));
            lTrip.StartDate = lTrip.StartDate.AddDays(-60);
            lTrip.EndDate = lTrip.EndDate.AddDays(-60);
            Assert.AreEqual(TripStage.Traveled, new BlitzerCore.Business.TripBusiness(null).GetStage(lTrip));
        }
    }
}
