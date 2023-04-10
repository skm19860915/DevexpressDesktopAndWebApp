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
    class OpportunityAccess
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

        public Team CreateTeam()
        {
            var lTeamBiz = new TeamBusiness(DbContext);
            Team lTeam = new Team() { Name = "Test Team" };
            new TeamDataAccess(DbContext).Save(lTeam);
            Agent A1 = DataLake.GetAgents()[0];
            lTeamBiz.AddMember(lTeam, A1);
            Agent A2 = DataLake.GetAgents()[1];
            lTeamBiz.AddMember(lTeam, A2);
            return lTeam;

        }
        public void CreateOpp(Agent aOwner)
        {
            var lContactBiz = new ContactBusiness(DbContext);

            /************************ Call New Opp(Get)     *********************************/
            var lNewQR = new QuoteRequestBusiness(DbContext).New(lContactBiz.Get(""), aOwner);
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

        }

        [Test]
        public void AccessTeamMembersOpps()
        {
            CreateTeam();
            Agent lOwner = DataLake.GetAgents()[0];
            Agent lTeamMate = DataLake.GetAgents()[1];

            var lTeam = new TeamDataAccess(DbContext).Get(1);
            Assert.IsNotNull(lTeam);
            CreateOpp(lOwner);
            var lBaseOpp = new OpportunityDataAccess(DbContext).Get(lOwner);
            Assert.IsNotNull(lBaseOpp);

            var lOpp = new OpportunityDataAccess(DbContext).GetActiveOpportunities(lTeamMate);
            // Team Mate shold have access
            Assert.IsNotNull(lOpp);
            Assert.AreEqual(1, lOpp.Count());
        }
    }
}
