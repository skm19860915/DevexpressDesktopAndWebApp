using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BlitzerCore.Models;
using WebApp.DataServices;
using NUnitTests.Helpers;
using NUnit.Framework;
using System.IO;
using Microsoft.EntityFrameworkCore;
using BlitzerCore.WebBots;
using BlitzerCore.Business;

namespace NUnitTests.Bots
{
    class WorldAgentDirectTests
    {
        RepositoryContext mContext = null;
        DbContextOptions<WebApp.DataServices.RepositoryContext> mDBOptions = null;

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
        public void ParseErrorPage()
        {
            string lFilePath = @"C:\Users\idewatson\source\repos\Blitzer\Data\WADErrors.html";
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.OptionFixNestedTags = true;
            string lHTML = System.IO.File.ReadAllText(lFilePath);
            var lWADBot = new WorldAgentDirectBot(mContext);
            lWADBot.PageParse(lHTML, new QuoteGroup());
        }

        [Test]
        public void VerifyAdultsOnlyAmenityMap()
        {

        }

        [Test]
        public void ParseHotels()
        {
            var lWebBot = new WADStaticFileBot(mContext, false);
            var lQuoteRequest = new QuoteRequest()
            {

                DestinationAirPort = DataLake.GetAirPorts()[5],
                DepartureAirPort = DataLake.GetAirPorts()[6],
                QuoteType = QuoteRequest.QuoteTypes.Package
            };
            var lQuoteGroup = new QuoteGroup() { QuoteRequest = lQuoteRequest };

            var lResorts = lWebBot.FindTrips(lQuoteGroup, "RDU", "CUN", new DateTime(2021, 5, 1), new DateTime(2021, 5, 7));
            Assert.AreEqual(93, lResorts.Hotels.Count());
            lWebBot.Close();
        }

    }
}
