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
    class MakePaymensOnTrip
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
        public void CreatePayment()
        {
            var lBookTrip = new UserSelectsQuote();
            lBookTrip.DbContext = DbContext;
            lBookTrip.UserBooksATripFromQuote();
            var lBooking = DbContext.Bookings.FirstOrDefault();
            Assert.IsNotNull(lBooking);
            UIPayment lUIPayment = new PaymentBusiness(DbContext, null).Create(lBooking);
            lUIPayment.Amount = "50.35";
            /*************************************************************************/
            new PaymentBusiness(DbContext, null).Save(lUIPayment, DataLake.GetAgents()[0]);
            /*************************************************************************/
            var lPayment = DbContext.Payments.FirstOrDefault();
            Assert.IsNotNull(lPayment);
            Assert.AreEqual(50.35, lPayment.Amount);
        }

        [Test]
        public void MakeNewCreditCardPayment()
        {
            var lBookTrip = new UserSelectsQuote();
            lBookTrip.DbContext = DbContext;
            lBookTrip.UserBooksATripFromQuote();
            var lBooking = DbContext.Bookings.FirstOrDefault();
            Assert.IsNotNull(lBooking);
            UIPayment lUIPayment = new PaymentBusiness(DbContext, null).Create(lBooking);
            lUIPayment.Amount = "50.35";
            lUIPayment.CreditCard = "111122223334789";
            lUIPayment.Expiration = "12/28";
            lUIPayment.Code = "1236";
            /*************************************************************************/
            new PaymentBusiness(DbContext, null).Save(lUIPayment, DataLake.GetAgents()[0]);
            var lPayment = new PaymentBusiness(DbContext, null).Get(1);
            /*************************************************************************/
            Assert.IsNotNull(lPayment);
            Assert.AreEqual(50.35, lPayment.Amount);
        }
    }
}
