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
    class ContactManagement
    {
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
        public void SaveContactWithoutDuplicateEntries()
        {
            var lAgent = DataLake.GetAgents()[0];
            var lCBiz = new ContactBusiness(DbContext);
            var lUIContact = DataLake.GetUIContacts()[0];
            lUIContact.PrimaryPhone = "(818) 567-8100";
            lUIContact.Id = "RedDawn";
            var lContact = lCBiz.Save(lUIContact, lAgent);
            lContact = lCBiz.Save(lUIContact, lAgent);
            // Verify we don't continue to add phonenumbers
            Assert.AreEqual(1, lCBiz.Get(lUIContact.Id).PhoneNumbers.Count());
            Assert.AreEqual(1, lCBiz.Get(lUIContact.Id).Emails.Count());

            Assert.AreEqual(lCBiz.Get(lUIContact.Id).PhoneNumbers[0].PhoneNumber, "8185678100");
        }
        [Test]
        public void SaveContactUpdatePrimaries()
        {
            var lAgent = DataLake.GetAgents()[0];

            var lCBiz = new ContactBusiness(DbContext);
            var lUIContact = DataLake.GetUIContacts()[0];
            lUIContact.PrimaryPhone = "(818) 567-8100";
            lUIContact.Id = "RedDawn";
            var lContact = lCBiz.Save(lUIContact, lAgent);

            // UPdate Primary PHone
            lUIContact.PrimaryPhone = "(411) 911-0000";
            lContact = lCBiz.Save(lUIContact, lAgent);

            // Verify we don't continue to add phonenumbers
            Assert.AreEqual(1, lCBiz.Get(lUIContact.Id).PhoneNumbers.Count());
            Assert.AreEqual(1, lCBiz.Get(lUIContact.Id).PhoneNumbers.Count(x => x.Defaut == true));
            Assert.AreEqual(lCBiz.Get(lUIContact.Id).PhoneNumbers[0].PhoneNumber, "4119110000");
        }
    }
}
