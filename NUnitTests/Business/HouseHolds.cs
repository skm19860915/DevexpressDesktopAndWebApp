using System.Collections.Generic;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApp.DataServices;
using System.Security.Policy;
using Microsoft.Exchange.WebServices.Data;
using WebApp.Controllers;
using System;
using BlitzerCore.DataAccess;
using NUnitTests.Helpers;
using BlitzerCore.Business;
using BlitzerCore.Models;
using BlitzerCore.UIHelpers;

namespace NUnitTests.Business
{
    public class HouseHolds
    {
        RepositoryContext DbContext { get; set; }
        DbContextOptions<WebApp.DataServices.RepositoryContext> mDBOptions = null;

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
        public void AddWife()
        {
            var lAgent = DataLake.GetAgents()[0];
            var ContactBiz = new ContactBusiness(DbContext);
            
            var lHusband = ContactBiz.Get(DataLake.GetClients()[0].Id);
            lHusband.MaritalStatus = MaritalStatuses.Married;
            lHusband.Gender = Gender.Male;
            ContactBiz.Save(lHusband, lAgent);

            var lWife = ContactBiz.Get(DataLake.GetClients()[1].Id);
            lWife.MaritalStatus = MaritalStatuses.Married;
            lWife.Gender = Gender.Female;
            ContactBiz.Save(lWife, lAgent);

            new ContactBusiness(DbContext).AddHouseHoldMember(lHusband, lWife);
            var lMembers = ContactBiz.GetHouseHoldMembers(lHusband);
            Assert.AreEqual(2,lMembers.Count());
            var lHHusband = lMembers.FirstOrDefault(x => x.Member.Id == lHusband.Id);
            var lHWife = lMembers.FirstOrDefault(x => x.Member.Id == lWife.Id);
            Assert.AreEqual(lHusband.Id, lHHusband.Member.Id);
            Assert.AreEqual(RelationShips.Husband, lHHusband.Relationship);
            Assert.AreEqual(RelationShips.Wife, lHWife.Relationship);
        }
        [Test]
        public void AddDaughter()
        {
            var ContactBiz = new ContactBusiness(DbContext);
            var lAgent = DataLake.GetAgents()[0]; 
            var lHusband = ContactBiz.Get(DataLake.GetClients()[0].Id);
            lHusband.MaritalStatus = MaritalStatuses.Seperated;
            lHusband.Gender = Gender.Male;
            ContactBiz.Save(lHusband, lAgent);

            var lWife = ContactBiz.Get(DataLake.GetClients()[1].Id);
            lWife.MaritalStatus = MaritalStatuses.Single;
            lWife.Gender = Gender.Female;
            lWife.Age = 13;
            ContactBiz.Save(lWife, lAgent);

            new ContactBusiness(DbContext).AddHouseHoldMember(lHusband, lWife);
            var lMembers = ContactBiz.GetHouseHoldMembers(lHusband);
            Assert.AreEqual(2, lHusband.HouseHold.Members.Count());
            Assert.AreEqual(2, lMembers.Count());
            var lHHusband = lMembers.FirstOrDefault(x => x.Member.Id == lHusband.Id);
            var lHWife = lMembers.FirstOrDefault(x => x.Member.Id == lWife.Id);
            Assert.AreEqual(lHusband.Id, lHHusband.Member.Id);
            Assert.AreEqual(RelationShips.Father, lHHusband.Relationship);
            Assert.AreEqual(RelationShips.Daughter, lHWife.Relationship);
        }
        [Test]
        public void AddSon()
        {
            var ContactBiz = new ContactBusiness(DbContext);
            var lAgent = DataLake.GetAgents()[0];

            var lHusband = ContactBiz.Get(DataLake.GetClients()[0].Id);
            lHusband.MaritalStatus = MaritalStatuses.Divoiced;
            lHusband.Gender = Gender.Male;
            ContactBiz.Save(lHusband, lAgent);

            var lWife = ContactBiz.Get(DataLake.GetClients()[1].Id);
            lWife.MaritalStatus = MaritalStatuses.Single;
            lWife.Gender = Gender.Male;
            lWife.Age = 13;
            ContactBiz.Save(lWife, lAgent);

            new ContactBusiness(DbContext).AddHouseHoldMember(lHusband, lWife);
            var lMembers = ContactBiz.GetHouseHoldMembers(lHusband);
            Assert.AreEqual(2, lMembers.Count());
            var lHHusband = lMembers.FirstOrDefault(x => x.Member.Id == lHusband.Id);
            var lHWife = lMembers.FirstOrDefault(x => x.Member.Id == lWife.Id);
            Assert.AreEqual(lHusband.Id, lHHusband.Member.Id);
            Assert.AreEqual(RelationShips.Father, lHHusband.Relationship);
            Assert.AreEqual(RelationShips.Son, lHWife.Relationship);
        }

        [Test]
        public void FatherWifeSonDaughter()
        {
            var lAgent = DataLake.GetAgents()[0];

            var ContactBiz = new ContactBusiness(DbContext);
            var lHusband = ContactBiz.Get(DataLake.GetClients()[0].Id);
            lHusband.MaritalStatus = MaritalStatuses.Divoiced;
            lHusband.Gender = Gender.Male;
            ContactBiz.Save(lHusband, lAgent);
            var lWife = DataLake.GetClients()[1];
            lWife.Gender = Gender.Female;
            var lSon = DataLake.GetClients()[2];
            lSon.Gender = Gender.Male;
            var lDaughter = DataLake.GetClients()[3];
            lDaughter.Gender = Gender.Female;

            ContactBiz.Save(lWife, lAgent);
            ContactBiz.Save(lSon, lAgent);
            ContactBiz.Save(lDaughter, lAgent);


            ContactBiz.AddHouseHoldMember(lHusband, lWife);
            ContactBiz.AddHouseHoldMember(lHusband, lSon);
            ContactBiz.AddHouseHoldMember(lHusband, lDaughter);

            Assert.AreEqual(4, lSon.HouseHold.Members.Count());
        }
    }
}
