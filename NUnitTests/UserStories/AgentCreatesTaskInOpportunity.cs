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
    class AgentCreatesTaskInOpportunity
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
        public void CreateTask()
        {
            const string lOppNote = "Task Note Number 1";
            var lTaskBiz = new TaskBusiness(DbContext);
            var lUserStory = new NewOppFromContact();
            var lAgent = new ContactBusiness(DbContext).GetAgent(DataLake.GetAgents()[0].Id);
            var lOppBiz = new OpportunityBusiness(DbContext);
            lUserStory.DbContext = DbContext;
            lUserStory.AgentCreatesNewOpportunity();
            var lOpp = new OpportunityBusiness(DbContext).GetAll().FirstOrDefault();
            lOpp.Notes = lOppNote;
            lOppBiz.Save(lOpp, lAgent);
            Assert.IsNotNull(lOpp);
            var lTask = lTaskBiz.Create(lOpp, lAgent); 
            lTaskBiz.Save(lTask);
            var lTestTask = lTaskBiz.Get(1);
            Assert.IsNotNull(lTestTask);
            Assert.AreEqual(lOpp.ID, lTestTask.Opportunity.ID);
            Assert.AreEqual(lAgent.Id, lTestTask.Owner.Id);
            Assert.AreEqual(lOppNote, lOpp.Notes);
        }
    }
}
