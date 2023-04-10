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
    public class NewOppFromContact
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
        public void AgentCreatesNewOpportunity()
        {
            var lContactBiz = new ContactBusiness(DbContext);
            var lAgent = lContactBiz.Get(DataLake.GetAgents()[0].Id) as Agent;

            /************************ Call New Opp(Get)     *********************************/
            var lNewQR = new QuoteRequestBusiness(DbContext).New(lContactBiz.Get(""), lAgent);
            /********************************************************************************/

            var lUIQR = QuoteRequestUIHelper.Convert(DbContext, lNewQR);
            lUIQR.Contacts[0].First = "Test";
            lUIQR.Contacts[0].Middle = "Middle";
            lUIQR.Contacts[0].Last = "LastName";
            lUIQR.Contacts[0].DOB = "8/8/1998";
            lUIQR.Contacts[0].PrimaryEmail = "Test@TestUser.com";
            lUIQR.Contacts[0].PrimaryPhone = "(888)333-7700";
            lUIQR.StartDate = "3/1/2020";
            lUIQR.EndDate = "3/10/2020";
            lUIQR.DepartureCityCode = "RDU";
            lUIQR.DestinationCityCode = "CUN";

            /************************ Call Save New Opp(Post)    ****************************/
            var lQR = new QuoteRequestBusiness(DbContext).Save(lUIQR, DataLake.GetAgents()[0]);
            /********************************************************************************/

            Assert.AreEqual(lQR.Opportunity.Travelers[0].User.First, "Test");
            Assert.AreEqual(lQR.Opportunity.Travelers[0].User.Middle, "Middle");
            Assert.AreEqual(lQR.Opportunity.Travelers[0].User.Last, "LastName");
            Assert.AreEqual(lQR.Opportunity.Travelers[0].User.DOB, new DateTime(1998, 8,8));
            Assert.AreEqual(lQR.Opportunity.Travelers[0].User.Emails[0].Address, "Test@TestUser.com");
        }

        [Test]
        public void AgentCreatesNewOpportunityFromContact()
        {
            var lContactBiz = new ContactBusiness(DbContext);
            var lAgent = lContactBiz.Get(DataLake.GetAgents()[0].Id) as Agent;
            var lContactCnt = DbContext.Contacts.Count();
            var lContact = DataLake.GetClients()[0];
            /************************ Call New Opp(Get)     *********************************/
            var lNewQR = new QuoteRequestBusiness(DbContext).New(lContactBiz.Get(lContact), lAgent);
            /********************************************************************************/

            var lUIQR = QuoteRequestUIHelper.Convert(DbContext, lNewQR);
            lUIQR.StartDate = "3/1/2020";
            lUIQR.EndDate = "3/10/2020";
            lUIQR.DepartureCityCode = "RDU";
            lUIQR.DestinationCityCode = "CUN";

            /************************ Call Save New Opp(Post)    ****************************/
            var lQR = new QuoteRequestBusiness(DbContext).Save(lUIQR, DataLake.GetAgents()[0]);
            /********************************************************************************/

            Assert.AreEqual(lContactCnt, DbContext.Contacts.Count());
            Assert.AreEqual(lQR.Opportunity.Travelers[0].User.First, lContact.First);
            Assert.AreEqual(lQR.Opportunity.Travelers[0].User.Middle, lContact.Middle);
            Assert.AreEqual(lQR.Opportunity.Travelers[0].User.Last, lContact.Last);
            Assert.AreEqual(lQR.Opportunity.Travelers[0].User.DOB, lContact.DOB);
            Assert.AreEqual(lQR.Opportunity.Travelers[0].User.Emails[0].Address, lContact.PrimaryEmail);
        }

        [Test]
        public void AgentSelectsContacts()
        {
            var lConBiz = new ContactBusiness(DbContext);
            var lQRBiz = new QuoteRequestBusiness(DbContext);
            Contact lPrimeContact =lConBiz.Get(DataLake.GetClients()[0].Id);
            Agent lAgent = lConBiz.Get(DataLake.GetAgents()[0].Id) as Agent;
            lPrimeContact.PreferredAirPorts = new List<PreferredAirPort>();
            var lAirPort = new AirPortDataAccess(DbContext).Get(DataLake.GetAirPorts()[0].Code);
            lPrimeContact.PreferredAirPorts.Add(new PreferredAirPort() { AirPortID = lAirPort.AirPortID, AirPort = lAirPort, ContactId = lPrimeContact.Id });
            lConBiz.Save(lPrimeContact, lAgent);
            Contact lWife = lConBiz.Get(DataLake.GetClients()[1].Id);
            Contact lSon = lConBiz.Get(DataLake.GetClients()[2].Id);
            Contact lDaughter = lConBiz.Get(DataLake.GetClients()[3].Id);
            lConBiz.AddHouseHoldMember(lPrimeContact, lWife);
            lConBiz.AddHouseHoldMember(lPrimeContact, lSon);
            lConBiz.Save(lPrimeContact, lAgent);
            lPrimeContact = lConBiz.Get(DataLake.GetClients()[0].Id);
            /********************************************************/
            var lQuoteRequest = lQRBiz.New(lPrimeContact, lAgent);
            /********************************************************/
            // Set Preferred AirPort
            Assert.AreEqual(DataLake.GetAirPorts()[0].AirPortID, lQuoteRequest.DepartureAirPort.AirPortID);
            // Populate all the traverls in the primaries household
            Assert.AreEqual(3, lQuoteRequest.Opportunity.Travelers.Count());
            Assert.AreEqual(lPrimeContact.First, lQuoteRequest.Opportunity.Travelers[0].User.First);
            var lUI = QuoteRequestUIHelper.Convert(DbContext, lQuoteRequest);
            Assert.IsNotNull(lUI);
            Assert.AreEqual(4, lUI.Contacts.Count());
            lUI.DepartureCityCode = DataLake.GetAirPorts()[0].Code;
            lUI.DestinationCityCode = DataLake.GetAirPorts()[1].Code;
            /********************************************************/
            var lQRResult = lQRBiz.Save(lUI, lAgent);
            /********************************************************/
            Assert.AreEqual(1, DbContext.Opportunities.Count());
        }
    }
}
