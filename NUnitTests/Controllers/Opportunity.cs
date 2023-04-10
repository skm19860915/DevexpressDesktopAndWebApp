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
using System.Globalization;

namespace NUnitTests.Controllers
{
    public class Opportunity
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
            }
        }

        [Test]
        public void CreateNewOpp()
        {
            var lUIQuoteRequest = CreateUIQuoteRequest();

            var lAgent = DataLake.GetAgents()[0];

            string FuncName = $"{ClassName}New (QuoteRequest)";
            Logger.EnterFunction(FuncName);

            var lCBiz = new ContactBusiness(mContext);
            var lUserExist = lCBiz.GetByEmail(lUIQuoteRequest.Contacts[0].PrimaryEmail);
            var lQuoteRequest = new QuoteRequestBusiness(mContext).Save(lUIQuoteRequest, lAgent);

            // Verify QuoteGroup Created
            Assert.AreEqual(1, lQuoteRequest.QuoteGroups.Count());
            // Verify Filter Created
            var lQuoteGroup = lQuoteRequest.QuoteGroups.First();
            Assert.IsNotNull(lQuoteGroup);
            // Verify Opportunity Created
            Assert.IsNotNull(lQuoteRequest.Opportunity);
        }

        public UIQuoteRequest CreateUIQuoteRequest()
        {
            //var lQuoteRequest = new BlitzerCore.Models.UI.UIQuoteRequest() { QuoteID = 39, StartDate = "10/1/2020", EndDate = "10/10/2020", When = "8/1/2020" };
            AirPort lDepart = DataLake.GetAirPorts().ElementAt(2);
            AirPort lDest = DataLake.GetAirPorts().Where(x => x.Code == "LAX").First();
            var lAgent = DataLake.GetAgents()[0];

            var lUIRequest = DataLake.CreateUIQuoteRequest(REQUEST_ID, lDepart.Code, lDest.Code);
            lUIRequest.NumberOfAdults = "2";
            var lQuoteRequest = new QuoteRequestBusiness(mContext).Save(lUIRequest, lAgent);
            var lsStartDate = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DataLake.START_DATE.Month);

            Assert.IsNotNull(lUIRequest, "UI Request is Null");
            Assert.IsNotNull(lQuoteRequest, "Quote Request is Null");
            Assert.IsNotNull(lQuoteRequest.Opportunity, "Opportunity is null");
            Assert.AreEqual($"Tester {lDest.City} {lsStartDate} 2021", lQuoteRequest.Opportunity.Name);
            Assert.AreEqual(1, lQuoteRequest.Opportunity.QuoteRequests.Count());
            Assert.AreEqual(1, lQuoteRequest.QuoteGroups.Count(), "Quote Group should of had 1 Element");

            return lUIRequest;
        }

    }
}
