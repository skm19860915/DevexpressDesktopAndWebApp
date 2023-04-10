using System;
using System.Collections.Generic;
using NUnit.Framework;
using BlitzerCore.Business;
using BlitzerCore.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using NUnitTests.Helpers;
using WebApp.DataServices;

namespace NUnitTests.Business
{
    public class TripBusiness
    {
        RepositoryContext mContext = null;
        DbContextOptions<RepositoryContext> mDBOptions = null;
        //string mUserID = "0654d61e-6df8-4e1c-83fd-77efe85b718e";

        const string TRIP1NAME = "Trip1 Name";
        const string BOOK1NAME = "Booking 1";

        public TripBusiness(IDbContext aContext)
        {
            mContext = (RepositoryContext)aContext;
        }

        public TripBusiness()
        {
        }
        [SetUp]
        public void Setup()
        {
            CreateInMemoryContext();
        }
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
                DataLake.Init(mContext);
                GetTrip();
            }

            return mContext;
        }

        public Trip GetTrip()
        {
            AirPort lDepart = mContext.AirPorts.First(x => x.Code == "RDU");
            AirPort lDest = mContext.AirPorts.First(x => x.Code == "CUN");
            Trip lTrip = new Trip() { Name = TRIP1NAME, StartDate = new System.DateTime(2020, 5, 1), EndDate = new System.DateTime(2020, 5, 7), OutboundAirPort = lDepart, InboundAirPort = lDest };
            lTrip.Travelers.Add(new UserMap() { Opportunity = lTrip, UserID = DataLake.GetClients().First().Id });
            lTrip.Travelers.Add(new UserMap() { Opportunity = lTrip, UserID = DataLake.GetClients().Skip(1).First().Id });
            lTrip.Travelers.Add(new UserMap() { Opportunity = lTrip, UserID = DataLake.GetClients().Skip(2).First().Id });
            lTrip.Agent = mContext.Agents.First();
            lTrip.AgentId = lTrip.Agent.Id;
            //Booking lBook = new Booking() { BookingNumber = BOOK1NAME, Amount = 400.00, FinalPayment = new DateTime(2020, 4, 20), Trip = lTrip };
            mContext.Trips.Add(lTrip);
            mContext.SaveChanges();
            return lTrip;
        }

        [Test]
        public void ValidateTrip()
        {
            var lTripBiz = new BlitzerCore.Business.TripBusiness(mContext);
            var lTrip = lTripBiz.Get(1);

            Assert.AreEqual(TRIP1NAME, lTrip.Name);
            //.AreEqual(lTrip.Bookings[0].BookingNumber, BOOK1NAME);
        }

        [Test]
        public void ConvertDate()
        {
            string lsDate = "Sun 08/16/2020 7:50am";
            DateTime lDT = DateTime.Parse(lsDate);
            DateTime lDT2 = lDT;
        }
    }
}