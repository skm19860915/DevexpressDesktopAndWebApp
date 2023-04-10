using System.Collections.Generic;
using NUnit.Framework;
using BlitzerCore.Business;
using BlitzerCore.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApp.DataServices;
using System.Security.Policy;
using Microsoft.Exchange.WebServices.Data;
using WebApp.Controllers;
using System;
using BlitzerCore.DataAccess;
using NUnitTests.Helpers;

namespace NUnitTests.Business
{
    [TestFixture]
    class Opportunity
    {
        RepositoryContext mContext = null;
        DbContextOptions<WebApp.DataServices.RepositoryContext> mDBOptions = null;

        public RepositoryContext CreateInMemoryContext()
        {
            mDBOptions = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            mContext = new RepositoryContext(mDBOptions);
            if (mContext != null)
            {
                mContext.Database.EnsureDeleted();
                mContext.Database.EnsureCreated();

                //LoadHotels();
                //LoadRoomTypes();
            }

            return mContext;
        }

        [Test]
        public void TestCreating4Leads()
        {
            //var lLeads = new List<BlitzerCore.Models.UI.Lead>();
            using (var lContext = CreateInMemoryContext())
            {
                var lTravallers = NUnitTests.Helpers.DataLake.CreateUIContacts();
                var lLeads = new OpportunityBusiness(lContext).Convert(lTravallers);

                Assert.AreEqual(4, lLeads.Count());
                ValidateLead(DataLake.CreateUIContacts()[0], lLeads[0].User);
                Assert.IsTrue(lLeads[0].Primary);
                ValidateLead(DataLake.CreateUIContacts()[1], lLeads[1].User);
                ValidateLead(DataLake.CreateUIContacts()[2], lLeads[2].User);
                ValidateLead(DataLake.CreateUIContacts()[3], lLeads[3].User);
            }
        }

        void ValidateLead(BlitzerCore.Models.UI.UIContact aBase, BlitzerCore.Models.Contact aTester)
        {
            Assert.AreEqual(aBase.First, aTester.First);
            Assert.AreEqual(aBase.Middle, aTester.Middle);
            Assert.AreEqual(aBase.Last, aTester.Last);
            var lRawPhone = aTester.PhoneNumbers.FirstOrDefault(x => x.Defaut);
            if (lRawPhone == null)
                Assert.AreEqual(aBase.Cell, lRawPhone);
            else
                Assert.AreEqual(aBase.Cell, lRawPhone.PhoneNumber);
            Assert.AreEqual(BlitzerCore.Helpers.DateHelper.ConvertDate(aBase.DOB), aTester.DOB);
            if (aBase.PrimaryEmail != null && aBase.PrimaryEmail != null)
                Assert.AreEqual(aBase.PrimaryEmail, aTester.Emails[0].Address);
            else
                Assert.AreEqual(0, aTester.Emails.Count());
        }

        [Test]
        public void ValidateOpportunity()
        {
            //var lLeads = new List<BlitzerCore.Models.UI.Lead>();
            using (var lContext = CreateInMemoryContext())
            {
                var lTravallers = NUnitTests.Helpers.DataLake.CreateUIContacts();
                //var lAgent = CreateAgent();
                var lUIOpportunity = new BlitzerCore.Models.UI.UIOpportunity()
                {
                    AgentId = "Error.  Must Correct!",
                    AllInclusive = true,
                    OutBoundAirport = "Raleight (RDU)",
                    InBoundAirPort = "Cancun Mexico (CUN)",
                    OutBoundDate = "11/1/20",
                    InBoundDate = "11/8/20",
                    Travelers = lTravallers
                };
                BlitzerCore.Models.Opportunity lOpportunity = new OpportunityBusiness(lContext).Convert(lUIOpportunity);

            }
        }

        [TestCase("CanCun(CUN)")]
        [TestCase("  CanC um ( C U N )")]
        [TestCase("0")]
        [TestCase("CanCun")]
        public void GetAirPort(string lValue)
        {
            //var lLeads = new List<BlitzerCore.Models.UI.Lead>();
            using (var lContext = CreateInMemoryContext())
            {
                CreateAirPorts();

                var lTestAirPort = new TravelBusiness(lContext).GetAirPort(lValue);
                var lBaseAirPort = new AirPortDataAccess(lContext).Get("CUN");

                if (lValue.IndexOf('(') > 0)
                    Assert.AreEqual(lBaseAirPort.AirPortID, lTestAirPort.AirPortID);
                else
                    Assert.AreEqual(null, lTestAirPort);
            }
        }

        public List<Relationship> CreateRelationShips()
        {
            var lRLs = new List<Relationship>();
            lRLs.Add(new Relationship() { RelationshipID = 1, Description = "Father" });
            lRLs.Add(new Relationship() { RelationshipID = 2, Description = "Wife" });
            lRLs.Add(new Relationship() { RelationshipID = 3, Description = "Son" });
            lRLs.Add(new Relationship() { RelationshipID = 4, Description = "Daughter" });
            return lRLs;
        }


        private void CreateAirPorts()
        {
            mContext.Airports.Add(new AirPort() { AirPortID = 37, Name = "Cancun (CUN)", Code = "CUN", });
            mContext.Airports.Add(new AirPort() { AirPortID = 398, Name = "Raleight-Durham (RDU)", Code = "RDU", });
            mContext.SaveChanges();
        }
    }
}
