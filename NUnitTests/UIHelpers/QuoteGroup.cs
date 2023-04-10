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

namespace NUnitTests.UIHelpers
{
    class QuoteGroup
    {
        const int REQUEST_ID = 100;
        RepositoryContext mContext = null;
        DbContextOptions<RepositoryContext> mDBOptions = null;
        enum QuoteType { Package =1, LandOnly = 2, Both = 3}

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
        //**************************************************************************
        // Summary : Calls Make to QuoteRequest/Edit To View results of Pull Quotes
        //**************************************************************************
        public void DisplayCheapestPackageBotQuotes() {
            // set the input data
            BlitzerCore.Models.QuoteGroup lQuoteGroup = GetTestQuoteGroup(QuoteType.Package);
            var lResult = QuoteGroupUIHelper.Convert(mContext, lQuoteGroup);
            Assert.AreEqual(2, lResult.QuoteList.Count(), "Should only return 2 UIQuotes");
            Assert.AreEqual("$90.00", lResult.QuoteList.Where(x => x.SKUID == 1).FirstOrDefault().PackagePrice);
            Assert.AreEqual(2, lResult.QuoteList.Where(x => x.SKUID == 1).FirstOrDefault().TourOperatorID);
            Assert.AreEqual("$200.00", lResult.QuoteList.Where(x => x.SKUID == 2).FirstOrDefault().PackagePrice);
            Assert.AreEqual(1, lResult.QuoteList.Where(x => x.SKUID == 2).FirstOrDefault().TourOperatorID);
        }

        [Test]
        //**************************************************************************
        // Summary : Calls Make to QuoteRequest/Edit To View results of Pull Quotes
        //**************************************************************************
        public void DisplayCheapestLandBotQuotes()
        {
            // set the input data
            BlitzerCore.Models.QuoteGroup lQuoteGroup = GetTestQuoteGroup(QuoteType.LandOnly);
            var lResult = QuoteGroupUIHelper.Convert(mContext, lQuoteGroup);
            Assert.AreEqual(2, lResult.QuoteList.Count(), "Should only return 2 UIQuotes");
            Assert.AreEqual("$45.00", lResult.QuoteList.Where(x => x.SKUID == 1).FirstOrDefault().PackagePrice);
            Assert.AreEqual(2, lResult.QuoteList.Where(x => x.SKUID == 1).FirstOrDefault().TourOperatorID);
            Assert.AreEqual("$100.00", lResult.QuoteList.Where(x => x.SKUID == 2).FirstOrDefault().PackagePrice);
            Assert.AreEqual(1, lResult.QuoteList.Where(x => x.SKUID == 2).FirstOrDefault().TourOperatorID);
        }

        [Test]
        //**************************************************************************
        // Summary : Calls Make to QuoteRequest/Edit To View results of Pull Quotes
        //**************************************************************************
        public void DisplayCheapestLandAndPackageBotQuotes()
        {
            // set the input data
            BlitzerCore.Models.QuoteGroup lQuoteGroup = GetTestQuoteGroup(QuoteType.Both);
            var lResult = QuoteGroupUIHelper.Convert(mContext, lQuoteGroup);
            Assert.AreEqual(4, lResult.QuoteList.Count(), "Should return 4 UIQuotes");
            Assert.AreEqual("$45.00", lResult.QuoteList.Where(x => x.SKUID == 1).OrderBy(x=>x.Total).FirstOrDefault().PackagePrice);
            Assert.AreEqual("$90.00", lResult.QuoteList.Where(x => x.SKUID == 1).OrderBy(x => x.Total).LastOrDefault().PackagePrice);
            Assert.AreEqual("$100.00", lResult.QuoteList.Where(x => x.SKUID == 2).OrderBy(x => x.Total).FirstOrDefault().PackagePrice);
            Assert.AreEqual("$200.00", lResult.QuoteList.Where(x => x.SKUID == 2).OrderBy(x => x.Total).LastOrDefault().PackagePrice);
            //Assert.AreEqual(2, lResult.QuoteList.Where(x => x.SKUID == 1).FirstOrDefault().TourOperatorID);
            //Assert.AreEqual("$100.00", lResult.QuoteList.Where(x => x.SKUID == 2).FirstOrDefault().PackagePrice);
            //Assert.AreEqual(1, lResult.QuoteList.Where(x => x.SKUID == 2).FirstOrDefault().TourOperatorID);
        }

        private BlitzerCore.Models.QuoteGroup GetTestQuoteGroup(QuoteType aQuoteType)
        {
            AirPort lDepart = DataLake.GetAirPorts().ElementAt(2);
            AirPort lDest = DataLake.GetAirPorts().ElementAt(5);
            var lAgent = new ContactBusiness(mContext).Get(DataLake.GetAgents()[0].Id) as Agent;
            var lUIRequest = DataLake.CreateUIQuoteRequest(REQUEST_ID, lDepart.Code, lDest.Code);
            var lQuoteRequest = new QuoteRequestBusiness(mContext).Save(lUIRequest, lAgent);
            
            var lQuoteGroup = new BlitzerCore.Models.QuoteGroup() { QuoteRequest = lQuoteRequest };


            var lResort = new BlitzerCore.Models.Hotel() { Id = 1 };
            var lRoomType1 = new SKU() { SKUID = 1, Name = "Room Type 1" };
            var lRoomType2 = new SKU() { SKUID = 2, Name = "Room Type 2" };

            var lTO1 = new TourOperator() { Id = 1, Name = "TO 1" };
            var lTO2 = new TourOperator() { Id = 2, Name = "TO 2" };
            lQuoteGroup.BotQuotes = new List<QuoteToResultsMapper>();

            if (aQuoteType == QuoteType.Both || aQuoteType == QuoteType.Package)
            {
                // VE Room 1
                QuoteRequestResort lResort1 = new QuoteRequestResort() { LandOnly = false, QuoteGroup = lQuoteGroup, ResortId = lResort.Id, Resort = lResort, ResortRoomTypeID = lRoomType1.SKUID, Price = 100, ResortRoomType = lRoomType1, TourOperatorID = lTO1.Id, TourOperator = lTO1 };
                // VE Room 2
                QuoteRequestResort lResort2 = new QuoteRequestResort() { LandOnly = false, QuoteGroup = lQuoteGroup, ResortId = lResort.Id, Resort = lResort, ResortRoomTypeID = lRoomType2.SKUID, Price = 200, ResortRoomType = lRoomType2, TourOperatorID = lTO1.Id, TourOperator = lTO1 };
                // Delta Room 1
                QuoteRequestResort lResort3 = new QuoteRequestResort() { LandOnly = false, QuoteGroup = lQuoteGroup, ResortId = lResort.Id, Resort = lResort, ResortRoomTypeID = lRoomType1.SKUID, Price = 90, ResortRoomType = lRoomType1, TourOperatorID = lTO2.Id, TourOperator = lTO2 };
                // Delta Room 2
                QuoteRequestResort lResort4 = new QuoteRequestResort() { LandOnly = false, QuoteGroup = lQuoteGroup, ResortId = lResort.Id, Resort = lResort, ResortRoomTypeID = lRoomType2.SKUID, Price = 210, ResortRoomType = lRoomType2, TourOperatorID = lTO2.Id, TourOperator = lTO2 };

                lQuoteGroup.BotQuotes.Add(new QuoteToResultsMapper() { QuoteGroup = lQuoteGroup, QuoteGroupID = lQuoteGroup.Id, QuoteRequestResort = lResort1, QuoteRequestResortID = 1 });
                lQuoteGroup.BotQuotes.Add(new QuoteToResultsMapper() { QuoteGroup = lQuoteGroup, QuoteGroupID = lQuoteGroup.Id, QuoteRequestResort = lResort2, QuoteRequestResortID = 1 });
                lQuoteGroup.BotQuotes.Add(new QuoteToResultsMapper() { QuoteGroup = lQuoteGroup, QuoteGroupID = lQuoteGroup.Id, QuoteRequestResort = lResort3, QuoteRequestResortID = 1 });
                lQuoteGroup.BotQuotes.Add(new QuoteToResultsMapper() { QuoteGroup = lQuoteGroup, QuoteGroupID = lQuoteGroup.Id, QuoteRequestResort = lResort4, QuoteRequestResortID = 1 });
            }

            if (aQuoteType == QuoteType.Both || aQuoteType == QuoteType.LandOnly)
            {
                // VE Room 1
                QuoteRequestResort lResort1 = new QuoteRequestResort() { LandOnly = true, QuoteGroup = lQuoteGroup, ResortId = lResort.Id, Resort = lResort, ResortRoomTypeID = lRoomType1.SKUID, Price = 50, ResortRoomType = lRoomType1, TourOperatorID = lTO1.Id, TourOperator = lTO1 };
                // VE Room 2
                QuoteRequestResort lResort2 = new QuoteRequestResort() { LandOnly = true, QuoteGroup = lQuoteGroup, ResortId = lResort.Id, Resort = lResort, ResortRoomTypeID = lRoomType2.SKUID, Price = 100, ResortRoomType = lRoomType2, TourOperatorID = lTO1.Id, TourOperator = lTO1 };
                // Delta Room 1
                QuoteRequestResort lResort3 = new QuoteRequestResort() { LandOnly = true, QuoteGroup = lQuoteGroup, ResortId = lResort.Id, Resort = lResort, ResortRoomTypeID = lRoomType1.SKUID, Price = 45, ResortRoomType = lRoomType1, TourOperatorID = lTO2.Id, TourOperator = lTO2 };
                // Delta Room 2
                QuoteRequestResort lResort4 = new QuoteRequestResort() { LandOnly = true, QuoteGroup = lQuoteGroup, ResortId = lResort.Id, Resort = lResort, ResortRoomTypeID = lRoomType2.SKUID, Price = 105, ResortRoomType = lRoomType2, TourOperatorID = lTO2.Id, TourOperator = lTO2 };

                lQuoteGroup.BotQuotes.Add(new QuoteToResultsMapper() { QuoteGroup = lQuoteGroup, QuoteGroupID = lQuoteGroup.Id, QuoteRequestResort = lResort1, QuoteRequestResortID = 1 });
                lQuoteGroup.BotQuotes.Add(new QuoteToResultsMapper() { QuoteGroup = lQuoteGroup, QuoteGroupID = lQuoteGroup.Id, QuoteRequestResort = lResort2, QuoteRequestResortID = 1 });
                lQuoteGroup.BotQuotes.Add(new QuoteToResultsMapper() { QuoteGroup = lQuoteGroup, QuoteGroupID = lQuoteGroup.Id, QuoteRequestResort = lResort3, QuoteRequestResortID = 1 });
                lQuoteGroup.BotQuotes.Add(new QuoteToResultsMapper() { QuoteGroup = lQuoteGroup, QuoteGroupID = lQuoteGroup.Id, QuoteRequestResort = lResort4, QuoteRequestResortID = 1 });
            }


            return lQuoteGroup;
        }
    }
}
