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
using BlitzerCore.Models;
using BlitzerCore.UIHelpers;
using WebApp.DataServices;
using WebApp.Controllers;
using BlitzerCore.WebBots;
using NUnitTests.ServiceStubs;

namespace NUnitTests.Business
{
    class QuoteRequest
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
        /*
         *  Verify Resort Filter returns all data if no filter specified
         */
        public void ConvertBulkHotelStagingDataToProd()
        {
            DataLake.ClearStagingData(mContext);
            var lTourOperators = new List<TourOperator>() { new CompanyBusiness(mContext).Get(DataLake.DELTA_AIRLINESID) as TourOperator };
            var lWebBots = new List<IWebTravelSrv>() { new WADStaticFileBot(mContext) };
            var lRates = StagingHotelRateLoader.Load();
            var lHotels = StagingHotelLoader.Load().OrderBy(x => x.HotelStagingID).ToList();
            lHotels.ForEach(x => x.HotelRateTypes = lRates.Where(y => y.HotelStagingID == x.HotelStagingID).ToList());
            lHotels.ForEach(x => x.HotelStagingID = 0);
            ((WADStaticFileBot)lWebBots[0]).SetData(lHotels);
            ((WADStaticFileBot)lWebBots[0]).SetData(StagingFlightLoader.Load());

            var lQuoteBiz = new QuoteRequestBusiness(mContext,  null);
            AirPort lDepart = DataLake.GetAirPorts().ElementAt(5);
            AirPort lDest = DataLake.GetAirPorts().ElementAt(4);
            var lAgent = DataLake.GetAgents()[0];

            var lUIRequest = DataLake.CreateUIQuoteRequest(REQUEST_ID, lDepart.Code, lDest.Code);
            var lRequest = lQuoteBiz.Save(lUIRequest, lAgent);
            var lQuoteGroup = lQuoteBiz.GetOpenQuoteGroup(lRequest);
            lQuoteBiz.Execute(lQuoteGroup, lAgent, lTourOperators);

            Assert.AreEqual(589, mContext.QuoteRequestResorts.Count());
            var lCnt = mContext.FlightItineraries.Count();
            Assert.Greater(mContext.FlightItineraries.Count(), 1, "No Flights where returned");
        }

        [Test]
        /*
         *  Verify Resort Filter returns all data if no filter specified
         */
        public void ConvertDeltaVacationsBulkFlightStagingDataToProd()
        {
            DataLake.ClearStagingData(mContext);
            var lWebBots = new List<IWebTravelSrv>() { new WADStaticFileBot(mContext) };
            var lFlights = StagingFlightLoader.Load();
            ((WADStaticFileBot)lWebBots[0]).SetData(lFlights);

            var lQRBiz = new QuoteRequestBusiness(mContext, null);
            var lQBiz = new QuoteBusiness(mContext, null);
            AirPort lDepart = DataLake.GetAirPorts().ElementAt(5);
            AirPort lDest = DataLake.GetAirPorts().ElementAt(6);
            var lAgent = DataLake.GetAgents()[0];

            var lUIRequest = DataLake.CreateUIQuoteRequest(REQUEST_ID, lDepart.Code, lDest.Code);
            var lRequest = lQRBiz.Save(lUIRequest, lAgent);
            var lQuoteGroup = lQRBiz.GetOpenQuoteGroup(lRequest);
            lQRBiz.Search(lQuoteGroup, lAgent, lWebBots);
            Assert.AreEqual(114, mContext.FlightItineraries.Count());
        }

        [Test]
        public void BulkLoadHotelStagging()
        {
            List<SKU> lRoomTypes = mContext.SKUs.ToList();

            mContext.Staging_Hotels.AddRange(StagingHotelLoader.Load());
            mContext.SaveChanges();
            mContext.Staging_HotelRates.AddRange(StagingHotelRateLoader.Load());
            mContext.SaveChanges();


            Assert.AreEqual(128, mContext.Staging_Hotels.Count());
            Assert.AreEqual(1081, mContext.Staging_HotelRates.Count());
        }

        [Test]
        public void BulkLoadFlightStagging()
        {

            List<SKU> lRoomTypes = mContext.SKUs.ToList();

            mContext.Staging_Flights.AddRange(StagingFlightLoader.Load());
            mContext.SaveChanges();

            Assert.AreEqual(114, mContext.Staging_Flights.Count());
        }


        [Test]
        public void ParsePrice()
        {
            string lsprice = @"

< !--BEGIN: / tags / pkg / shared / common / helper / displayReferencePrice.tag-- >







+$136.73
=< br >
$1,351.77



< !--END: / tags / pkg / shared / common / helper / displayReferencePrice.tag-- >
 total";
            string lPrice = new BlitzerCore.WebBots.AAVacationBot(mContext).ParsePrice(lsprice);
            Assert.AreEqual(lPrice, "$1,351.77");
        }

    }
}
