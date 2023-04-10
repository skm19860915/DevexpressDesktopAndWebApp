using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using NUnitTests.Helpers;
using NUnitTests.ServiceStubs;
using NUnit.Framework;
using BlitzerCore.DataAccess;
using BlitzerCore.Business;
using BlitzerCore.Models;
using BlitzerCore.UIHelpers;
using BlitzerCore.Models.UI;
using BlitzerCore.WebBots;
using BlitzerCore.Helpers;
using WebApp.DataServices;

namespace NUnitTests.UserStories
{
    public class UserSelectsQuote
    {
        public RepositoryContext DbContext { get; set; }
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
        public void RejectBookingBecausenoQuoteSelected()
        {
            var lStub = new NUnitTests.ServiceStubs.WorldAgentDirectStub(DbContext);
            var lQBiz = new QuoteBusiness(DbContext);
            var lQRBiz = new BlitzerCore.Business.QuoteRequestBusiness(DbContext);

            var lAgent = new ContactBusiness(DbContext).Get(DataLake.GetAgents()[0].Id) as Agent;
            var lQR = new QuoteRequestBusiness(DbContext).New(null, lAgent);
            var lUIQR = QuoteRequestUIHelper.Convert(DbContext, lQR);

            AirPort lDepart = DataLake.GetAirPorts().ElementAt(2);
            AirPort lDest = DataLake.GetAirPorts().ElementAt(5);

            var lCaputeTC = new GenerateQuoteForTrip();
            lCaputeTC.DbContext = DbContext;
            lCaputeTC.InputQuoteData();

            // Now Book With Trip Business
            var lQuoteGroup = new QuoteGroupBusiness(DbContext).Get(DbContext.QuoteGroups.FirstOrDefault().Id);
            var lUIQuoteGroup = QuoteGroupUIHelper.Convert(DbContext, lQuoteGroup);

            //Assert.Throws<InvalidDataException>(() => new TripBusiness(DbContext).Book(lUIQuoteGroup, DataLake.GetAgents()[0]));
        }

        [Test]
        public void RejectBookingBecausenoMultipleQuotesSelected()
        {
            var lStub = new NUnitTests.ServiceStubs.WorldAgentDirectStub(DbContext);
            var lQBiz = new QuoteBusiness(DbContext);
            var lQRBiz = new BlitzerCore.Business.QuoteRequestBusiness(DbContext);

            var lAgent = new ContactBusiness(DbContext).Get(DataLake.GetAgents()[0].Id) as Agent;
            var lQR = new QuoteRequestBusiness(DbContext).New(null, lAgent);
            var lUIQR = QuoteRequestUIHelper.Convert(DbContext, lQR);

            AirPort lDepart = DataLake.GetAirPorts().ElementAt(2);
            AirPort lDest = DataLake.GetAirPorts().ElementAt(5);

            var lCaputeTC = new GenerateQuoteForTrip();
            lCaputeTC.DbContext = DbContext;
            lCaputeTC.InputQuoteData();

            // Now Book With Trip Business
            var lQuoteGroup = new QuoteGroupBusiness(DbContext).Get(DbContext.QuoteGroups.FirstOrDefault().Id);
            lQuoteGroup.Quotes.ForEach(x => x.Booked = true);
            var lUIQuoteGroup = QuoteGroupUIHelper.Convert(DbContext, lQuoteGroup);

            //Assert.Throws<InvalidDataException>(() => new TripBusiness(DbContext).Book(lUIQuoteGroup, DataLake.GetAgents()[0].Id));
        }

        [Test]
        public void UserBooksATripFromQuote()
        {
            var lStub = new NUnitTests.ServiceStubs.WorldAgentDirectStub(DbContext);
            var lQBiz = new QuoteBusiness(DbContext);
            var lQRBiz = new BlitzerCore.Business.QuoteRequestBusiness(DbContext);

            var lAgent = new ContactBusiness(DbContext).Get(DataLake.GetAgents()[0].Id) as Agent;
            var lQR = new QuoteRequestBusiness(DbContext).New(null, lAgent);
            var lUIQR = QuoteRequestUIHelper.Convert(DbContext, lQR);

            AirPort lDepart = DataLake.GetAirPorts().ElementAt(2);
            AirPort lDest = DataLake.GetAirPorts().ElementAt(5);

            var lCaputeTC = new GenerateQuoteForTrip();
            lCaputeTC.DbContext = DbContext;
            lCaputeTC.InputQuoteData();

            // Now Book With Trip Business
            var lQuoteGroup = new QuoteGroupBusiness(DbContext).Get(DbContext.QuoteGroups.FirstOrDefault().Id);
            lQuoteGroup.Quotes[1].Booked = true;
            var lUIQuoteGroup = QuoteGroupUIHelper.Convert(DbContext, lQuoteGroup);
            lUIQuoteGroup.QuoteRequest = null;

            // Clean up Opportunity for other test cases
            DbContext.UnTrack(lQR.Opportunity);
            DbContext.UnTrack(lQuoteGroup.QuoteRequest.Opportunity);

            /******************************** Book The Quote *****************************************/
            var lTrip = new TripBusiness(DbContext).Book(QuoteUIHelper.Convert(DbContext, lQuoteGroup.Quotes[1]),
                lAgent);
            /*****************************************************************************************/

            Assert.AreEqual(1, lTrip.ID);
            //Assert.AreEqual(1, lTrip.Bookings.Count);
            //Assert.AreEqual(231.98, lTrip.Balance);

        }
    }
}
