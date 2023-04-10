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
    public class GenerateQuoteForTrip
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
        public void RetrieveQuoteRequestforUser()
        {

            var lQBiz = new QuoteBusiness(DbContext);
            var lQRBiz = new QuoteRequestBusiness(DbContext);
            // Retrieve the created QuoteRequest
            var lCaputeTC = new CaptureQuickQuoteRequest();
            lCaputeTC.DbContext = DbContext;
            lCaputeTC.FatherWifeandAgeForChild();

            var lOppDA = new OpportunityDataAccess(DbContext);
            var lOpps = lOppDA.GetActiveOpportunities(DataLake.GetAgents()[0]).Where(x => x.Stage != OpportunityStages.Won && x.Stage != OpportunityStages.Loss).ToList();
            Assert.AreEqual(1, lOpps.Count());
            var lQRs = lOpps[0].QuoteRequests;
            Assert.NotNull(lQRs);
            Assert.AreEqual(1, lQRs.Count);
        }

        [Test]
        public void InputQuoteData() {
            
            var lQBiz = new QuoteBusiness(DbContext);
            var lQRBiz = new QuoteRequestBusiness(DbContext);

            // Retrieve the created QuoteRequest
            var lCaputeTC = new CaptureQuickQuoteRequest();
            lCaputeTC.DbContext = DbContext;
            lCaputeTC.FatherWifeandAgeForChild();

            DataLake.LoadHotels(DbContext);
            DataLake.LoadRoomTypes(DbContext);
            var lQR = lQRBiz.GetQuoteRequest(1);
            var lQuotes = lQR.QuoteGroups.SelectMany(x => x.Quotes);
            Assert.NotNull(lQuotes);
            Assert.AreEqual(0, lQuotes.Count());

            var lPrice1 = 137.00;
            var lAdjustment1 = 34.50;
            var lPrice2 = 217.00;
            var lAdjustment2 = 14.980;
            var lPrice3 = 391.00;
            var lAdjustment3 = 140.00;

            var lQuoteGroup = new QuoteRequestBusiness(DbContext).GetOpenQuoteGroup(lQR);
            Assert.IsNotNull(lQuoteGroup);

            //Add the first resort to the quote
            lQBiz.AddResort(lQuoteGroup, DbContext.Accommodations.ToList()[0], DbContext.SKUs.ToList()[0], lPrice1, lAdjustment1);
            lQBiz.AddResort(lQuoteGroup, DbContext.Accommodations.ToList()[1], DbContext.SKUs.ToList()[1], lPrice2, lAdjustment2);
            lQBiz.AddResort(lQuoteGroup, DbContext.Accommodations.ToList()[2], DbContext.SKUs.ToList()[2], lPrice3, lAdjustment3);

            lQuoteGroup = new QuoteGroupDataAccess(DbContext).Get(1);
            Assert.AreEqual(3, lQuoteGroup.Quotes.Count());
            Assert.AreEqual(lPrice1, lQuoteGroup.Quotes[0].SubTotal);
            Assert.AreEqual(lAdjustment1, lQuoteGroup.Quotes[0].Adjustment);
            Assert.AreEqual(lPrice2, lQuoteGroup.Quotes[1].SubTotal);
            Assert.AreEqual(lAdjustment2, lQuoteGroup.Quotes[1].Adjustment);
            Assert.AreEqual(lPrice3, lQuoteGroup.Quotes[2].SubTotal);
            Assert.AreEqual(lAdjustment3, lQuoteGroup.Quotes[2].Adjustment);

            DbContext.UnTrack(lQR.Opportunity);
        }
        [Test]
        public void UserAcceptsQuote()
        {

            var lQBiz = new QuoteBusiness(DbContext);
            var lQRBiz = new QuoteRequestBusiness(DbContext);
            var lQDA = new QuoteDataAccess(DbContext);
            // Retrieve the created QuoteRequest
            var lCaputeTC = new CaptureQuickQuoteRequest();
            lCaputeTC.DbContext = DbContext;
            InputQuoteData();

            var lOppDA = new OpportunityDataAccess(DbContext);
            var lOpps = lOppDA.GetActiveOpportunities(DataLake.GetAgents()[0]).Where(x => x.Stage != OpportunityStages.Won && x.Stage != OpportunityStages.Loss).ToList();
            Assert.AreEqual(1, lOpps.Count());
            var lQRs = lOpps[0].QuoteRequests;
            Assert.NotNull(lQRs);
            Assert.AreEqual(1, lQRs.Count);
            var lQuoteGroups = lQBiz.GetQuoteGroups(lOpps[0].Travelers[0].User);



            /****************** User has to mark each quote as Ready to Send **********************/
            for (int i = 1; i <= 3; i++)
                lQDA.Get(i).Status = QuoteStatus.Ready;

            /**************************************************************************************/
            new QuoteGroupBusiness(DbContext).SendQuoteGroup(lQuoteGroups[0].QuoteRequest.Agent, lQuoteGroups[0]);
            /**************************************************************************************/

            var lOpp = lOppDA.Get(1);
            lQuoteGroups = lQBiz.GetQuoteGroups(lOpp.Travelers[0].User);
            Assert.AreEqual(2, lQuoteGroups.Count());
            var lQG1 = new QuoteGroupBusiness(DbContext).Get(lQuoteGroups[0].Id);

            Assert.NotNull(lQRs);
            Assert.AreEqual(1, lQRs.Count);
            Assert.AreEqual(3, lQG1.Quotes.Count());
            Assert.AreEqual(QuoteStatus.Sent, lQG1.Quotes[0].Status);
            Assert.AreEqual(QuoteStatus.Sent, lQG1.Quotes[1].Status);
            Assert.AreEqual(QuoteStatus.Sent, lQG1.Quotes[2].Status);

            Assert.AreEqual(3, lQG1.Quotes.Count());

            lOpp.Stage = OpportunityStages.Won;
            new OpportunityDataAccess(DbContext).Save(lOpp);
            lOpp = lOppDA.Get(1);
            lQuoteGroups = lQBiz.GetQuoteGroups(lOpp.Travelers[0].User);
            Assert.AreEqual(2, lQuoteGroups.Count());
            lQG1 = new QuoteGroupBusiness(DbContext).Get(lQuoteGroups[0].Id);
            Assert.AreEqual(3, lQuoteGroups[0].Quotes.Count());

            DbContext.UnTrack(lOpp);

            /***************************************************************************************/
            //                               User Books a Quote
            /***************************************************************************************/
            var  lQuote = new QuoteDataAccess(DbContext).Get(1);
            var lUIQuote = QuoteUIHelper.Convert(DbContext, lQuote);
            DbContext.UnTrack(lQuote.QuoteRequest.Opportunity);
            new TripBusiness(DbContext).Book(lUIQuote, lOpps[0].Travelers[0].User);
            /***************************************************************************************/

            Assert.AreEqual(QuoteStatus.Sent, lQG1.Quotes[0].Status);
            Assert.AreEqual(QuoteStatus.Sent, lQG1.Quotes[2].Status);
            Assert.AreEqual(QuoteStatus.Booked, lQDA.Get(lUIQuote.QuoteID).Status);
            Assert.AreEqual(null, lQG1.Quotes[0].BookedById);
            Assert.AreEqual(lOpps[0].Travelers[0].User.Id, lQDA.Get(lUIQuote.QuoteID).BookedById);
            Assert.AreEqual(null, lQG1.Quotes[2].BookedById);
            lOpp = lOppDA.Get(1);
            Assert.AreEqual(lOpp.Stage, OpportunityStages.Won);

        }
    }
}
