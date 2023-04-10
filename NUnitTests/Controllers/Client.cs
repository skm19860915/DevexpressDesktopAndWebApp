using System.Collections.Generic;
using NUnit.Framework;
using BlitzerCore.Business;
using BlitzerCore.DataAccess;
using BlitzerCore.Models;
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
using BlitzerCore.Models.UI;

namespace NUnitTests.Controllers
{
    public class Client
    {
        const int REQUEST_ID = 100;
        const string ClassName = "NUnitTests.Controllers.QuoteRequest::";
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
                DataLake.LoadHotels(mContext);
                DataLake.LoadRoomTypes(mContext);
            }
        }

        /// <summary>
        /// Insurance that after a search that a user can view the quote
        /// </summary>
        [Test]
        public void ViewBotQuote()
        {
            var lTravelServices = new List<IWebTravelSrv>() { new WADStaticFileBot(mContext) };
            var lQuoteBiz = new QuoteRequestBusiness(mContext, null);
            AirPort lDepart = DataLake.GetAirPorts().ElementAt(2);
            AirPort lDest = DataLake.GetAirPorts().ElementAt(5);
            var lAgent = mContext.Agents.Where(x => x.Id == DataLake.GetAgents()[0].Id).First();

            //Search for resorts
            var lUIRequest = DataLake.CreateUIQuoteRequest(REQUEST_ID, lDepart.Code, lDest.Code);
            var lQuoteRequest = lQuoteBiz.Save(lUIRequest, lAgent);
            RedirectToActionResult lRequest = (RedirectToActionResult)new QuoteRequestController(mContext, null, null, null).Search(lQuoteRequest.QuoteRequestID, lTravelServices);
            
            // Send Quote
            var lQGBiz = new QuoteGroupBusiness(mContext, null);
            lQGBiz.SendQuoteGroup(lAgent, lQuoteRequest.QuoteGroups.First());

            // Click Portal View Quote button
            var lActionResult = new ClientController(mContext, null, null).Quote(lQuoteRequest.QuoteGroups.First().GUID);
            Assert.IsNotNull((lActionResult));
            var lActionResult2 = new ClientController(mContext, null, null).Quote(lQuoteRequest.QuoteGroups.First().GUID);
        }

        /// <summary>
        /// Insurance that after a search that a user can view the manual quote
        /// </summary>
        [Test]
        public void ViewManualQuote()
        {
            var lTravelServices = new List<IWebTravelSrv>() { new WADStaticFileBot(mContext) };
            var lQuoteBiz = new QuoteRequestBusiness(mContext, null);
            AirPort lDepart = DataLake.GetAirPorts().ElementAt(2);
            AirPort lDest = DataLake.GetAirPorts().ElementAt(5);
            var lAgent = mContext.Agents.Where(x => x.Id == DataLake.GetAgents()[0].Id).First();

            //Search for resorts
            var lUIRequest = DataLake.CreateUIQuoteRequest(REQUEST_ID, lDepart.Code, lDest.Code);
            var lQuoteRequest = lQuoteBiz.Save(lUIRequest, lAgent);

            var lHotel = mContext.Accommodations.First(x => x.Name == DataLake.GetHotels().First().Name);

            // Add Manual Quote
            UIQuote lForm = new UIQuote();
            lForm.SupplierId = lHotel.Id;
            lForm.TourOperatorID = DataLake.VE_AIRLINESID;
            lForm.SKUID = mContext.SKUs.FirstOrDefault(x => x.ProviderID == lHotel.Id).SKUID;
            lForm.QuoteRequest = lUIRequest;
            lForm.QuoteRequestID = lQuoteRequest.QuoteRequestID;
            lForm.Out_Leg1_DepartTime = "1130";
            lForm.Out_Leg1_ArriveTime = "1245";
            lForm.Out_ConnectionAirport = "LAX";
            lForm.Out_Leg2_DepartTime = "1522";
            lForm.Out_Leg2_ArriveTime = "1711";
            lForm.Out_Leg2_Flight = "139A";
            lForm.In_Leg1_DepartTime = "1530";
            lForm.In_Leg1_ArriveTime = "1922";
            lForm.In_Leg1_Flight = "499Z";
            lForm.PackagePrice = "16,123.45";
            // Set the status to ready so that it will send to the client
            lForm.Status = QuoteStatus.Ready;
            RedirectToActionResult lRequest = (RedirectToActionResult)new QuoteController(mContext, null).New(lForm);

            // Send Quote
            var lQGBiz = new QuoteGroupBusiness(mContext, null);
            lQGBiz.SendQuoteGroup(lAgent, lQuoteRequest.QuoteGroups.First());

            // View Quote
            var lActionResult = (ViewResult)new ClientController(mContext, null, null).Quote(lQuoteRequest.QuoteGroups.First().GUID);
            // If the quote is not sent, return model will be for a UIContact which catches all exceptions
            Assert.IsNotNull(lActionResult.Model as UIQuoteRequestWrapper);
            Assert.AreEqual(((UIQuoteRequestWrapper)lActionResult.Model).DestinationCityCode, lDest.Code);
            Assert.AreEqual(((UIQuoteRequestWrapper)lActionResult.Model).DepartureCityCode, lDepart.Code);
            Assert.AreEqual(((UIQuoteRequestWrapper)lActionResult.Model).BackgroundTitle , "US");
            Assert.AreEqual(((UIQuoteRequestWrapper)lActionResult.Model).Quotes[0].Flight_In_Arrive, "7:22 PM");
            Assert.AreEqual(((UIQuoteRequestWrapper)lActionResult.Model).Quotes[0].Flight_Out_Arrive, "5:11 PM");
            Assert.AreEqual(((UIQuoteRequestWrapper)lActionResult.Model).Quotes[0].Flight_Out_Depart, "11:30 AM");
            Assert.AreEqual(((UIQuoteRequestWrapper)lActionResult.Model).Quotes[0].Flight_Out_Layover, "LAX 2h 37m");
            Assert.AreEqual(((UIQuoteRequestWrapper)lActionResult.Model).Quotes[0].Total, "$16,123.45");
            Assert.AreEqual(((UIQuoteRequestWrapper)lActionResult.Model).Quotes[0].SupplierId, lHotel.Id);
            Assert.AreEqual(((UIQuoteRequestWrapper)lActionResult.Model).Quotes[0].RoomType, mContext.SKUs.FirstOrDefault(x => x.ProviderID == lHotel.Id).Name);
        }
    }
}
