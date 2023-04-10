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
    class AAVacationTests
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


        public void LogOut()
        {
            //"/ adm / agent_tools / logout.cfm";
        }

        [Test]
        [Ignore("Not in this Quarter")]
        public void ParseHotels()
        {
            var lQR = new QuoteRequest() { QuoteRequestID = 492, DepartureAirPortID = DataLake.GetAirPorts()[5].AirPortID, DestinationAirPortID = DataLake.GetAirPorts()[6].AirPortID };
            var lAABot = new AAVacationBot(mContext);
            var lQRBiz = new QuoteRequestBusiness(mContext);
            var lHotels = GetBulkHotels(mContext);
            Assert.AreEqual(25, lHotels.Count);
            ValidateHotel1(lHotels[0]);
            ValidateHotel13(lHotels[12]);
            lHotels[0].HotelRateTypes = GetBulkPrices(mContext);
            var lData = new Staging.FlightHotelInformation() { Hotels = lHotels };
            //lQRBiz.SaveStagingData(lData, lAABot.OperatorID, lQR.QuoteGroups.First());
            Assert.AreEqual(25, mContext.Staging_Hotels.Count(x=>x.RequestID == lQR.QuoteRequestID));
            Assert.AreEqual(10, mContext.Staging_HotelRates.Count());
            lAABot.ConvertResortsFromStagingToProd(lQR.QuoteGroups.First(), lQRBiz);
            Assert.AreEqual(10, mContext.QuoteRequestResorts.Count(x => x.QuoteGroupId == lQR.QuoteGroups.First().Id));
        }

        [Test]
        [Ignore("Not in this Quarter")]
        public void RunQRTwicewithsameCriteria()
        {
            var lQR = new QuoteRequest() { QuoteRequestID = 492, DepartureAirPortID = DataLake.GetAirPorts()[5].AirPortID, DestinationAirPortID = DataLake.GetAirPorts()[6].AirPortID };
            var lAABot = new AAVacationBot(mContext);
            var lQRBiz = new QuoteRequestBusiness(mContext);
            var lHotels = DataLake.GetStagingHotels(lQR.QuoteGroups.First());
            lHotels[0].HotelRateTypes = GetBulkPrices(mContext);
            var lData = new Staging.FlightHotelInformation() { Hotels = lHotels };
            //lQRBiz.SaveStagingData(lData, lAABot.OperatorID, lQR);
            lAABot.ConvertResortsFromStagingToProd(lQR.QuoteGroups.First(), lQRBiz);

            // Verify no issue with running a second time
            lQR = new QuoteRequest() { QuoteRequestID = 493, DepartureAirPortID = DataLake.GetAirPorts()[5].AirPortID, DestinationAirPortID = DataLake.GetAirPorts()[6].AirPortID };
            lHotels = DataLake.GetStagingHotels(lQR.QuoteGroups.First(), 10);
            lData = new Staging.FlightHotelInformation() { Hotels = lHotels };
            //lQRBiz.SaveStagingData(lData, lAABot.OperatorID, lQR);
            lAABot.ConvertResortsFromStagingToProd(lQR.QuoteGroups.First(), lQRBiz);
            Assert.AreEqual(8, mContext.Staging_Hotels.Count());
        }

        [Test]
        [Ignore("Not in this Quarter")]
        public void ParseHotelPrices()
        {
            var lRoomTypes = GetBulkPrices(mContext);
            Assert.AreEqual(10, lRoomTypes.Count);
            Assert.AreEqual("Premium Garden View, 1 King", lRoomTypes[0].RoomType);
            Assert.AreEqual("+$0.00", lRoomTypes[0].Price);
            Assert.AreEqual("Premium Ocean Front with Terrace, 1 King", lRoomTypes[9].RoomType);
            Assert.AreEqual("+$3,494.73", lRoomTypes[9].Price);
        }

        public List<Staging.Hotel> GetBulkHotels(IDbContext aContext)
        {
            string lFilePath = @"C:\Users\redop\source\repos\Blitzer\Data\AAVacation_VacationPage.html";
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument
            {
                Text = null,
                BackwardCompatibility = false,
                OptionAddDebuggingAttributes = false,
                OptionAutoCloseOnEnd = false,
                OptionCheckSyntax = false,
                OptionComputeChecksum = false,
                OptionEmptyCollection = false,
                DisableServerSideCode = false,
                OptionDefaultStreamEncoding = null,
                OptionXmlForceOriginalComment = false,
                OptionExtractErrorSourceText = false,
                OptionExtractErrorSourceTextMaxLength = 0,
                OptionFixNestedTags = false,
                OptionOutputAsXml = false,
                OptionPreserveXmlNamespaces = false,
                OptionOutputOptimizeAttributeValues = false,
                GlobalAttributeValueQuote = null,
                OptionOutputOriginalCase = false,
                OptionOutputUpperCase = false,
                OptionReadEncoding = false,
                OptionStopperNodeName = null,
                OptionDefaultUseOriginalName = false,
                OptionUseIdAttribute = false,
                OptionWriteEmptyNodes = false,
                OptionMaxNestedChildNodes = 0,
                ParseExecuting = null
            };
            htmlDoc.OptionFixNestedTags = true;
            string lHTML = System.IO.File.ReadAllText(lFilePath);
            htmlDoc.Load(new MemoryStream(Encoding.UTF8.GetBytes(lHTML)));
            var lAABot = new AAVacationBot(aContext);
            return lAABot.ParseHotels(htmlDoc.DocumentNode);
        }

        public List<Staging.HotelRate> GetBulkPrices(IDbContext aContext)
        {
            string lFilePath = @"C:\Users\redop\source\repos\Blitzer\Data\AAVacation_PricePage.html";
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.OptionFixNestedTags = true;
            string lHTML = System.IO.File.ReadAllText(lFilePath);
            htmlDoc.Load(new MemoryStream(Encoding.UTF8.GetBytes(lHTML)));
            var lAABot = new AAVacationBot(aContext);
            return lAABot.ParsePricePage(htmlDoc.DocumentNode);
        }

        public List<Staging.Flight> GetBulkFlights(IDbContext aContext)
        {
            var lRequest = new QuoteRequest()
            {
                DestinationAirPort = DataLake.GetAirPorts()[5],
                DepartureAirPort = DataLake.GetAirPorts()[6]
            };
            string lFilePath = @"C:\Users\redop\source\repos\Blitzer\Data\AAVacation_VacationPage.html";
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.OptionFixNestedTags = true;
            string lHTML = System.IO.File.ReadAllText(lFilePath);
            htmlDoc.Load(new MemoryStream(Encoding.UTF8.GetBytes(lHTML)));
            var lAABot = new AAVacationBot(aContext);
            return lAABot.ParseFlights(htmlDoc.DocumentNode, lRequest.QuoteGroups.First());

        }

        [Test]
        [Ignore("Not in this Quarter")]
        public void LIVE_ParseHotels()
        {
            var lAAVacationBot = new AAVacationBot(mContext);
            lAAVacationBot.Login("sales@eze2travel.com", "AAVac!6315");
            var lRequest = new QuoteGroup() {
                //DestinationAirPort = DataLake.GetAirPorts()[5],
                //DepartureAirPort = DataLake.GetAirPorts()[6]
            };

            var lResorts = lAAVacationBot.FindTrips(lRequest, "RDU", "CUN", new DateTime(2021, 5, 1), new DateTime(2020, 5, 7));
            Assert.AreEqual(25, lResorts.Hotels.Count());
            lAAVacationBot.Close();
        }

        [Test]
        [Ignore("Not in this Quarter")]
        public void ParseFlights()
        {
            var lFlights = GetBulkFlights(mContext);
            Assert.AreEqual(4, lFlights.Count);
            ValidateDepart(lFlights);

            var lQR = new QuoteRequest() { QuoteRequestID = 492, DepartureAirPortID = DataLake.GetAirPorts()[5].AirPortID, DestinationAirPortID = DataLake.GetAirPorts()[6].AirPortID };
            var lAABot = new AAVacationBot(mContext);
            var lQRBiz = new QuoteRequestBusiness(mContext);
            var lData = new Staging.FlightHotelInformation() { Flights = lFlights };
            //lQRBiz.SaveStagingData(lData, lAABot.OperatorID, lQR);
            Assert.AreEqual(4, mContext.Staging_Flights.Count());
            lAABot.ConvertFlightsFromStagingToProd(lQR.QuoteGroups.First());
            Assert.AreEqual(1, mContext.FlightItineraries.Count(x=>x.QuoteRequestID == lQR.QuoteRequestID));
        }

        private void ValidateReturn(List<Staging.Flight> lFlights)
        {
            throw new NotImplementedException();
        }

        private void ValidateDepart(List<Staging.Flight> aFlights)
        {
            var lFlights = aFlights.Where(x => x.Side == Staging.Flight.SIDES.DEPARTURE);
            Assert.AreEqual("5:58PM", lFlights.ElementAt(0).ArrivalTime);
            Assert.AreEqual("3:55PM", lFlights.ElementAt(0).DepartTime);
            Assert.AreEqual("Raleigh / Durham Intl - (RDU)", lFlights.ElementAt(0).DepartLocation);
            Assert.AreEqual("Dallas Fort Worth Intl - (DFW)", lFlights.ElementAt(0).ArrivalLocation);
            Assert.AreEqual("2627", lFlights.ElementAt(0).Aircraft);

            Assert.AreEqual("10:50PM", lFlights.ElementAt(1).ArrivalTime);
            Assert.AreEqual("7:05PM", lFlights.ElementAt(1).DepartTime);
            Assert.AreEqual("Cancun - (CUN)", lFlights.ElementAt(1).ArrivalLocation);
            Assert.AreEqual("Dallas Fort Worth Intl - (DFW)", lFlights.ElementAt(1).DepartLocation);
            Assert.AreEqual("1547", lFlights.ElementAt(1).Aircraft);
        }

        private void ValidateHotel1(Staging.Hotel aHotel)
        {
            Assert.AreEqual("Live Aqua Beach Resort Cancun", aHotel.Name);
            Assert.AreEqual(true, aHotel.AAPreferred);
            Assert.AreEqual(13, aHotel.Amenities.Count);
            Assert.AreEqual("Adults Only", aHotel.Amenities[12].Amenity.Type);
            Assert.AreEqual("All Inclusive", aHotel.Amenities[0].Amenity.Type);
            Assert.AreEqual("Cancun", aHotel.Location);
            Assert.AreEqual("Star Rating: 4.5", aHotel.Stars);
            Assert.AreEqual(DataLake.AA_AIRLINESID, aHotel.TourOperatorID);

        }
        private void ValidateHotel13(Staging.Hotel aHotel)
        {
            Assert.AreEqual("La Reina Roja Hotel Boutique", aHotel.Name);
            Assert.AreEqual(false, aHotel.AAPreferred);
            Assert.AreEqual(7, aHotel.Amenities.Count);
            Assert.AreEqual("Parking", aHotel.Amenities[1].Amenity.Type);
            Assert.AreEqual("Room Service", aHotel.Amenities[5].Amenity.Type);
            Assert.AreEqual("PLAYA DEL CARMEN", aHotel.Location);
            Assert.AreEqual("Star Rating: 4.0", aHotel.Stars);
            Assert.AreEqual(DataLake.AA_AIRLINESID, aHotel.TourOperatorID);
        }
    }
}
