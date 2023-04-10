using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using NUnitTests.Helpers;
using NUnit.Framework;
using BlitzerCore.DataAccess;
using BlitzerCore.Business;
using BlitzerCore.Helpers;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.UIHelpers;
using WebApp.DataServices;
using WebApp.Controllers;
using BlitzerCore.WebBots;

namespace NUnitTests.Business
{
    public class CreditBusiness
    {

        const int REQUEST_ID = 100;
        RepositoryContext mContext = null;
        DbContextOptions<RepositoryContext> mDBOptions = null;


        [SetUp]
        public void CreateInMemoryContext()
        {
            mDBOptions = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            mContext = new RepositoryContext(mDBOptions);
            if (mContext != null)
            {
                mContext.Database.EnsureDeleted();
                mContext.Database.EnsureCreated();
                DataLake.Init(mContext);
            }
        }

        [Test]
        public void CreateCredit()
        {
            var lTrip = new TripBusiness(mContext).GetTrip();
            var lBooking = new BookingBusiness(mContext).CreateBooking(lTrip, 999);
            var lUICredit = new UICredit()
            {
                Amount = DataHelper.ConvertToCurrency(lBooking.Amount), OriginalBookingId = lBooking.BookingID,
                Traveler = lTrip.Travelers.First().UserID, When = DateTime.Now
            };

            new BlitzerCore.Business.CreditBusiness(mContext, null).Save(lUICredit);
            var lCredit = mContext.Credits.First();
            Assert.AreEqual(lBooking.BookingID, lCredit.OriginalBookingId);
            Assert.AreEqual(lBooking.Amount/lTrip.Travelers.Count(), lCredit.Amount);
            Assert.AreEqual(lTrip.Travelers.Count(), mContext.Credits.Count());
        }
    }
}
