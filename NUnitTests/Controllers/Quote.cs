using System.Collections.Generic;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApp.DataServices;
using WebApp.Controllers;
using NUnitTests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using NUnitTests.Bots;
using BlitzerCore.WebBots;
using BlitzerCore.UIHelpers;
using BlitzerCore.Utilities;
using BlitzerCore.Business;
using BlitzerCore.DataAccess;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;

namespace NUnitTests.Controllers
{
    public class Quote
    {
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
        public void CreateQuoteWithFlights ()
        {
            var lQuoteBiz = new QuoteRequestBusiness(mContext, null);
            var lAirBz = new AirBusiness(mContext, null);
            AirPort lDepart = lAirBz.GetAirPort("RDU");
            AirPort lDest = lAirBz.GetAirPort("CUN");
            var lAgent = new ContactDataAccess(mContext).GetAgents().FirstOrDefault();

            var lUIRequest = DataLake.CreateUIQuoteRequest(100, lDepart.Code, lDest.Code);
            var lQuoteRequest = lQuoteBiz.Save(lUIRequest, lAgent);

            //var lUIRequest = DataLake.CreateUIQuoteRequest(REQUEST_ID, lDepart.Code, lDest.Code);
            UIQuote lForm = new UIQuote();
            lForm.TourOperatorID = DataLake.VE_AIRLINESID;
            lForm.QuoteRequest = lUIRequest;
            lForm.QuoteRequestID = lQuoteRequest.QuoteRequestID;
            lForm.Out_Leg1_DepartTime = "1130";
            lForm.Out_Leg1_ArriveTime = "1245";
            lForm.Out_Leg1_Flight = "139A";
            lForm.In_Leg1_DepartTime = "1530";
            lForm.In_Leg1_ArriveTime = "1922";
            lForm.In_Leg1_Flight = "499Z";
            RedirectToActionResult lRequest = (RedirectToActionResult)new QuoteController(mContext, null).New(lForm);

            var lQuote = new QuoteDataAccess(mContext).Get(1);
            Assert.IsNotNull(lQuote);
            Assert.AreEqual(1, lQuote.QuoteID);
            Assert.AreEqual(1, lQuote.Flights.Count);
            var lUIQuote = QuoteUIHelper.Convert(mContext, lQuote);
            Assert.AreEqual("11:30 AM", lUIQuote.Out_Leg1_DepartTime);
            Assert.AreEqual("12:45 PM", lUIQuote.Out_Leg1_ArriveTime);
            Assert.AreEqual("139A", lUIQuote.Out_Leg1_Flight);
            Assert.AreEqual("3:30 PM", lUIQuote.In_Leg1_DepartTime);
            Assert.AreEqual("7:22 PM", lUIQuote.In_Leg1_ArriveTime);
            Assert.AreEqual("499Z", lUIQuote.In_Leg1_Flight);
        }

        [Test]
        public void CreateQuoteWithOutFlights()
        {
            var lQuoteBiz = new QuoteRequestBusiness(mContext, null);
            var lAirBz = new AirBusiness(mContext, null);
            AirPort lDepart = lAirBz.GetAirPort("RDU");
            AirPort lDest = lAirBz.GetAirPort("CUN");
            var lAgent = new ContactDataAccess(mContext).GetAgents().FirstOrDefault();

            var lUIRequest = DataLake.CreateUIQuoteRequest(100, lDepart.Code, lDest.Code);
            var lQuoteRequest = lQuoteBiz.Save(lUIRequest, lAgent);

            //var lUIRequest = DataLake.CreateUIQuoteRequest(REQUEST_ID, lDepart.Code, lDest.Code);
            UIQuote lForm = new UIQuote();
            lForm.TourOperatorID = DataLake.VE_AIRLINESID;
            lForm.QuoteRequest = lUIRequest;
            lForm.QuoteRequestID = lQuoteRequest.QuoteRequestID;
            lForm.Adjustment = "1000";
            lForm.PackagePrice = "2000";
            lForm.Status = QuoteStatus.Ready;
            RedirectToActionResult lRequest = (RedirectToActionResult)new QuoteController(mContext, null).New(lForm);

            var lQuote = new QuoteDataAccess(mContext).Get(1);
            Assert.IsNotNull(lQuote);
            Assert.AreEqual(1, lQuote.QuoteID);
            Assert.AreEqual(1, lQuote.Flights.Count);
            var lUIQuote = QuoteUIHelper.Convert(mContext, lQuote);
            Assert.AreEqual(3000.00, lUIQuote.Total);
            Assert.AreEqual(QuoteStatus.Ready, lUIQuote.Status);
        }

        
        public void BookManualQuote()
        {
            var lQuoteBiz = new QuoteRequestBusiness(mContext, null);
            var lAirBz = new AirBusiness(mContext, null);
            AirPort lDepart = lAirBz.GetAirPort("RDU");
            AirPort lDest = lAirBz.GetAirPort("CUN");
            var lAgent = new ContactDataAccess(mContext).GetAgents().FirstOrDefault();

            var lUIRequest = DataLake.CreateUIQuoteRequest(100, lDepart.Code, lDest.Code);
            var lQuoteRequest = lQuoteBiz.Save(lUIRequest, lAgent);

            //var lUIRequest = DataLake.CreateUIQuoteRequest(REQUEST_ID, lDepart.Code, lDest.Code);
            UIQuote lForm = new UIQuote();
            lForm.TourOperatorID = DataLake.VE_AIRLINESID;
            lForm.QuoteRequest = lUIRequest;
            lForm.QuoteRequestID = lQuoteRequest.QuoteRequestID;
            lForm.Adjustment = "1000";
            lForm.PackagePrice = "2000";
            lForm.Out_Leg1_DepartTime = "1130";
            lForm.Out_Leg1_ArriveTime = "1245";
            lForm.Out_Leg1_Flight = "139A";
            lForm.In_Leg1_DepartTime = "1530";
            lForm.In_Leg1_ArriveTime = "1922";
            lForm.In_Leg1_Flight = "499Z";
            lForm.Status = QuoteStatus.Booked;
            RedirectToActionResult lRequest = (RedirectToActionResult)new QuoteController(mContext, null).New(lForm);

            var lTrip = new TripBusiness(mContext).Get(1);
            Assert.IsNotNull(lTrip);
        }

    }
}
