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
    class CaptureQuickQuoteRequest
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

        public UIQuoteRequest CaptureQuoteRequest()
        {
            DataLake.ClearStagingData(DbContext);
            var lAgent = DataLake.GetAgents()[0];

            var lQRBiz = new QuoteRequestBusiness(DbContext, null);
            var lUIQuoteRequest = lQRBiz.GetNewQuoteRequest(lAgent);
            Assert.IsNotNull(lUIQuoteRequest);
            Assert.AreEqual(4, lUIQuoteRequest.Contacts.Count());
            return lUIQuoteRequest;
        }

        [Test]
        public void CustomerOnlyProvidesCell()
        {
            var lTravelService = new WADStaticFileBot(DbContext);

            const string FirstName = "Jack";
            const string Cell = "(919)777-8888";
            const string DepartCode = "RDU";
            const string DestCode = "CUN";
            const string Start = "4/1/2021";
            const string End = "4/7/2021";


            var lUIQuoteRequest = CaptureQuoteRequest();
            lUIQuoteRequest.Contacts[0].First = FirstName;
            lUIQuoteRequest.Contacts[0].Cell = Cell;
            lUIQuoteRequest.Contacts[0].RelationshipID = 2;
            lUIQuoteRequest.DepartureCityCode = DepartCode;
            lUIQuoteRequest.DestinationCityCode = DestCode;
            lUIQuoteRequest.StartDate = Start;
            lUIQuoteRequest.EndDate = End;

            var lQRBiz = new QuoteRequestBusiness(DbContext,  null);
            /**************************************************************************/
            var lQuoteRequest = lQRBiz.Save(lUIQuoteRequest, DataLake.GetAgents()[0]);
            var lOpp = new OpportunityBusiness(DbContext).GetOpportunity(1);
            /**************************************************************************/

            Assert.IsNotNull(lQuoteRequest);
            Assert.AreEqual(1, lQuoteRequest.Opportunity.Travelers.Count());
            Assert.AreEqual(FirstName, lQuoteRequest.Opportunity.Travelers[0].User.First);
            Assert.AreEqual("9197778888", lQuoteRequest.Opportunity.Travelers[0].User.PhoneNumbers[0].PhoneNumber);
            Assert.AreEqual(1, HouseHoldHelper.GetMembers(DbContext, lQuoteRequest.Opportunity.Travelers[0].User).Count);
            Assert.AreEqual(1, lOpp.Travelers[0].User.HouseHoldId);
            Assert.AreEqual(DepartCode, lQuoteRequest.DepartureAirPort.Code);
            Assert.AreEqual(DestCode, lQuoteRequest.DestinationAirPort.Code);
            Assert.AreEqual(DataHelper.GetDate(Start) , lQuoteRequest.DepartureDate);
            Assert.AreEqual(DataHelper.GetDate(End), lQuoteRequest.ReturnDate);
        }
        [Test]
        public void FatherProvideAgeForChild()
        {
            var lTravelService = new WADStaticFileBot(DbContext);

            const string FirstName = "Jack";
            const string Cell = "(919)777-8888";
            const string DepartCode = "RDU";
            const string DestCode = "CUN";
            const string Start = "4/1/2021";
            const string End = "4/7/2021";
            const int KidAge = 12;

            var lUIQuoteRequest = CaptureQuoteRequest();
            lUIQuoteRequest.Contacts[0].First = FirstName;
            lUIQuoteRequest.Contacts[0].Cell = Cell;
            lUIQuoteRequest.DepartureCityCode = DepartCode;
            lUIQuoteRequest.DestinationCityCode = DestCode;
            lUIQuoteRequest.StartDate = Start;
            lUIQuoteRequest.EndDate = End;
            lUIQuoteRequest.AgesOfKids = new List<int>() { KidAge };

            var lQRBiz = new QuoteRequestBusiness(DbContext, null);
            var lQuoteRequest = lQRBiz.Save(lUIQuoteRequest, DataLake.GetAgents()[0]);

            Assert.IsNotNull(lQuoteRequest);
            Assert.Greater(lQuoteRequest.QuoteRequestID, 0);
            Assert.AreEqual(2, lQuoteRequest.Opportunity.Travelers.Count());
            Assert.AreEqual(FirstName, lQuoteRequest.Opportunity.Travelers[0].User.First);
            Assert.AreEqual("9197778888", lQuoteRequest.Opportunity.Travelers[0].User.PhoneNumbers[0].PhoneNumber);
            Assert.AreEqual(KidAge, lQuoteRequest.Opportunity.Travelers[1].User.Age);
            Assert.AreEqual(2,  HouseHoldHelper.GetMembers(DbContext, lQuoteRequest.Opportunity.Travelers[0].User).Count );
            Assert.AreEqual(DepartCode, lQuoteRequest.DepartureAirPort.Code);
            Assert.AreEqual(DestCode, lQuoteRequest.DestinationAirPort.Code);
            Assert.AreEqual(DataHelper.GetDate(Start), lQuoteRequest.DepartureDate);
            Assert.AreEqual(DataHelper.GetDate(End), lQuoteRequest.ReturnDate);
        }

        [Test]
        public void SaveQuoteRequestWithoutAgent()
        {
            int lRequestID = 120;
            var lQuoteBiz = new QuoteRequestBusiness(DbContext, null);
            BlitzerCore.Models.UI.UIQuoteRequest lRequest = new BlitzerCore.Models.UI.UIQuoteRequest() { QuoteID = lRequestID };
            Assert.Throws<InvalidDataException>(() => QuoteRequestUIHelper.Validate(DbContext, lRequest));
        }

        [Test]
        public void SaveQuoteRequestWithoutClient()
        {
            int lRequestID = 120;
            var lQuoteBiz = new QuoteRequestBusiness(DbContext, null);
            BlitzerCore.Models.UI.UIQuoteRequest lRequest = new BlitzerCore.Models.UI.UIQuoteRequest() { QuoteID = lRequestID };
            lRequest.Agent = ContactUIHelper.Convert( DbContext.Agents.FirstOrDefault());
            Assert.Throws<InvalidDataException>(() => QuoteRequestUIHelper.Validate(DbContext, lRequest));
        }


        [Test]
        public void FatherWifeandAgeForChild()
        {
            var lTravelService = new WADStaticFileBot(DbContext);

            const string HusbandName = "Jack";
            const string WifeName = "Linda";
            const string Cell = "(919)777-8888";
            const string DepartCode = "RDU";
            const string DestCode = "CUN";
            const string Start = "4/1/2021";
            const string End = "4/7/2021";
            const int KidAge = 12;

            var lUIQuoteRequest = CaptureQuoteRequest();
            lUIQuoteRequest.Contacts[1].First = HusbandName;
            lUIQuoteRequest.Contacts[1].RelationshipID = 2;
            lUIQuoteRequest.Contacts[1].Gender = Gender.Male;
            lUIQuoteRequest.Contacts[0].First = WifeName;
            lUIQuoteRequest.Contacts[0].RelationshipID = 1;
            lUIQuoteRequest.Contacts[0].Gender = Gender.Female;
            lUIQuoteRequest.Contacts[0].Cell = Cell;
            lUIQuoteRequest.DepartureCityCode = DepartCode;
            lUIQuoteRequest.DestinationCityCode = DestCode;
            lUIQuoteRequest.StartDate = Start;
            lUIQuoteRequest.EndDate = End;
            lUIQuoteRequest.AgesOfKids = new List<int>() { KidAge };

            var lContactsBeforeExe = DbContext.Contacts.Count();
            var lQRBiz = new QuoteRequestBusiness(DbContext);
            var lQuoteRequest = lQRBiz.Save(lUIQuoteRequest, DataLake.GetAgents()[0]);
            var lOpp = new OpportunityBusiness(DbContext).GetOpportunity(1);
            Assert.IsNotNull(lQuoteRequest);
            Assert.Greater(lQuoteRequest.QuoteRequestID, 0);
            Assert.AreEqual(3, lOpp.Travelers.Count());
            Assert.AreEqual(lContactsBeforeExe + 3, DbContext.Contacts.Count());
            Assert.AreEqual(HusbandName, lOpp.Travelers[1].User.First);
            Assert.AreEqual(WifeName, lOpp.Travelers[0].User.First);
            Assert.AreEqual("9197778888", lOpp.Travelers[0].User.PhoneNumbers[0].PhoneNumber);
            Assert.AreEqual(KidAge, lOpp.Travelers[2].User.Age);
            Assert.AreEqual(3, HouseHoldHelper.GetMembers(DbContext, lOpp.Travelers[0].User).Count);
            Assert.AreEqual(RelationShips.Wife, HouseHoldHelper.GetMembers(DbContext, lOpp.Travelers[0].User).First(x=>x.Member.First == WifeName).Relationship);
            Assert.AreEqual(RelationShips.Husband, HouseHoldHelper.GetMembers(DbContext, lOpp.Travelers[0].User).First(x => x.Member.First == HusbandName).Relationship);
            Assert.AreEqual(DepartCode, lQuoteRequest.DepartureAirPort.Code);
            Assert.AreEqual(DestCode, lQuoteRequest.DestinationAirPort.Code);
            Assert.AreEqual(DataHelper.GetDate(Start), lQuoteRequest.DepartureDate);
            Assert.AreEqual(DataHelper.GetDate(End), lQuoteRequest.ReturnDate);

            DbContext.UnTrack(lQuoteRequest.Opportunity);
            DbContext.UnTrack(lOpp);
        }
    }
}