using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.IO;
using System.Text;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using System.Threading;
using BlitzerCore.Models;
using BlitzerCore.Utilities;
using BlitzerCore.DataAccess;
using BlitzerCore.Business;
using BlitzerCore.Helpers;
using BlitzerCore.Business.DBConverters;

namespace BlitzerCore.WebBots
{
    public class AAVacationBot : WebBotBase, IAAVacationSrv
    {
        const string ClassName = "AAVacationBot::";

        private readonly string AA_PREFERRED = "Preferred Hotel";
        const string CONNECTINGFLIGHT = "Connecting Flight";
        const string OUTBOUNDFLIGHT = "Outbound Flight";
        const string LEGHEADER = "prod-subheader";
        const string FLIGHERHEADER = "prod-header";

        const string WEBSITE = "http://www.aavacations.com/agent";

        public AAVacationBot(IDbContext aContext, IWebDriver aDriver = null ) : base (aDriver)
        {
            mContext = aContext;
            if (aContext != null)
                TourOperatorID = new TourOperatorDataAccess(aContext).Get(TourOperator.AA_VACATIONS).Id;
        }

        public override string GetTourOperatorName()
        {
            return TourOperator.AA_VACATIONS;
        }
        public override ITourOperatorDBConverter GetDBConverter()
        {
            return new AAConverter();
        }
        private void init()
        {
            try
            {
                // TODO Auto-generated method stub
                ChromeOptions lOptions = new ChromeOptions();
                //lOptions.AddArgument("--headless");
                mChrome = new ChromeDriver(lOptions);
                var lWhat = DateTime.Now;
                //mChrome = new OpenQA.Selenium.Remote.();
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to create ChromeDrive", e);
            }
        }

        public override bool Login(string aUserName, string aPassword)
        {
            init();
            mChrome.Navigate().GoToUrl(WEBSITE);
            mChrome.Manage().Window.Maximize();
            mChrome.FindElement(By.Name("email")).SendKeys(aUserName);
            mChrome.FindElement(By.Id("password")).SendKeys(aPassword);
            mChrome.FindElement(By.XPath("//*[@id='loginbutton']")).Click();
            return true;
        }

        public override Staging.FlightHotelInformation FindTrips(QuoteGroup aQuoteGroup, string aStartLocation, string aEndLocation, DateTime aDepartDate, DateTime aReturnDate, Quote aBookTrip = null)
        {
            string FuncName = ClassName + "FindTrips - ";
            Logger.EnterFunction(FuncName);
            int lBreakCnt = 0;
            IWebElement lFromField = null;
            try
            {
                // Find the first element on the page

                try
                {
                    lFromField = mChrome.FindElement(By.Id("flight-hotel_from_city"));
                }
                catch (Exception) { }
                while (true)
                {
                    if (lFromField == null)
                    {
                        Thread.Sleep(1000);
                        try
                        {
                            lFromField = mChrome.FindElement(By.Id("flight-hotel_from_city"));
                        }
                        catch (Exception) { }

                        if (lFromField != null)
                        {
                            break;
                        }
                        lBreakCnt++;
                        if (lBreakCnt > 5)
                            throw new InvalidDataException($"{FuncName} Failed to find the From Field on the Search Page");
                    }
                }
                lFromField.Clear();

                mChrome.FindElement(By.Id("flight-hotel_from_city")).SendKeys(aStartLocation);
                mChrome.FindElement(By.Id("flight-hotel_to_city")).SendKeys(aEndLocation);

                mChrome.FindElement(By.Id("flight-hotel_from_date")).Clear();
                mChrome.FindElement(By.Id("flight-hotel_from_date")).SendKeys(FormatDate(aDepartDate));
                mChrome.FindElement(By.Id("flight-hotel_to_date")).Clear();
                mChrome.FindElement(By.Id("flight-hotel_to_date")).SendKeys(FormatDate(aReturnDate));
                mChrome.FindElement(By.Id("flight-hotel_to_date")).SendKeys("\t");

                mChrome.FindElement(By.Id("searchButton")).Click();

                string lHTML = "";

                // Wait for the full page to load
                while (true)
                {
                    Thread.Sleep(1000);
                    lHTML = mChrome.PageSource;
                    if (lHTML.Contains("</body>"))
                    {
                        break;
                    }
                }

                System.IO.File.WriteAllText(GetFilePath(), lHTML); ;

                try
                {
                    mChrome.FindElement(By.Id("amenities_filter_adults_only")).Click();
                }
                catch (Exception e)
                {
                    Logger.LogException($"{FuncName} Failed to click the Adults Ony filter number", e);
                }
                Thread.Sleep(2000);

                try
                {
                    mChrome.FindElement(By.Id("amenities_filter_all_inclusive")).Click();
                }
                catch (Exception)
                {
                    try
                    {
                        Thread.Sleep(1500);
                        mChrome.FindElement(By.Id("amenities_filter_all_inclusive")).Click();
                    }
                    catch (Exception)
                    {
                        try
                        {
                            Thread.Sleep(1500);
                            mChrome.FindElement(By.Id("amenities_filter_all_inclusive")).Click();
                        }
                        catch (Exception e1)
                        {
                            Logger.LogException($"{FuncName} Failed to click the all inclusive Only filter number", e1);
                        }
                    }
                }

                Thread.Sleep(2500);
                var lSelected = "";
                // Sort By Price
                try {
                    //while (lSelected != "Price")
                    {
                        mChrome.FindElement(By.Id("avail_sort_select")).SendKeys("Price" + Keys.Enter);
                        var lSortyByCtrl = mChrome.FindElement(By.Id("avail_sort_select"));
                        lSelected = lSortyByCtrl.ToString();

                        Thread.Sleep(2500);
                    }
                }
                catch ( Exception e3 )
                {
                    Logger.LogException($"{FuncName} Failed to sort by price", e3);
                }
    
                // Wait for the full page to load
                while (true)
                {
                    Thread.Sleep(2000);
                    lHTML = mChrome.PageSource;
                    if (lHTML.Contains("</body>"))
                    {
                        break;
                    }
                }

                System.IO.File.WriteAllText(GetFilePath(), lHTML);


                return PageParse(lHTML, aQuoteGroup);
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + "Exception executing pulling AA Vacation Quote", e);
                throw e;
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }


        public List<Staging.HotelRate> ParsePricePage(HtmlNode aDocNode)
        {
            var lOutput = new List<Staging.HotelRate>();

            if (aDocNode == null)
                return lOutput;

            var lResortNodes = aDocNode.SelectNodes("//fieldset[@class='room-types-fieldset']")[0].ChildNodes;
            bool lFirstNode = true;
            foreach (var lRoomTypeNode in lResortNodes)
            {
                if (lRoomTypeNode.Name != "div")
                    continue;

                if (lFirstNode)
                {
                    lFirstNode = false;
                    continue;
                }
                var lHotelRate = new Staging.HotelRate();
                lHotelRate.RoomType = Trim(lRoomTypeNode.SelectSingleNode("./span[1]/div[1]/span[2]"));
                lHotelRate.Price = Trim(lRoomTypeNode.SelectSingleNode("./span[2]/span[1]/span[1]"));
                lOutput.Add(lHotelRate);
            }

            return lOutput;
        }


        public override void ConvertFlightsFromStagingToProd(BlitzerCore.Models.QuoteGroup aQuoteGroup)
        {
            try
            {
                var lStaggedFlights = mContext.Staging_Flights.Where(x => x.QuoteGroupId == aQuoteGroup.Id).OrderBy(x => x.LegGUID).ToList();
                if (lStaggedFlights == null || lStaggedFlights.Count() == 0)
                    return;

                var lFlights = new AirBusiness(mContext, null).Convert(lStaggedFlights, aQuoteGroup);
                foreach (var lFlight in lFlights)
                {
                    lFlight.QuoteGroupId = aQuoteGroup.Id;
                    lFlight.QuoteRequestID = aQuoteGroup.QuoteRequest.QuoteRequestID;
                    lFlight.InBound.Flights.ForEach(x => x.QuoteGroupId = aQuoteGroup.Id);
                    lFlight.OutBound.Flights.ForEach(x => x.QuoteGroupId = aQuoteGroup.Id);
                }

                var lCount = new QuoteDataAccess(mContext).Save(lFlights);
                foreach (var lQRFlight in lFlights)
                {
                    lQRFlight.InBound.TripTicketId = lQRFlight.FlightItineraryId;
                    lQRFlight.OutBound.TripTicketId = lQRFlight.FlightItineraryId;
                }
                lCount = new QuoteDataAccess(mContext).Save(lFlights);
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to convert AA Flights from Staging to Prod", e);
                throw e;
            }
        }

        public override void ConvertResortsFromStagingToProd(QuoteGroup aQuoteGroup, QuoteRequestBusiness aQRBiz)
        {
            try
            {
                var lHotelBiz = new HotelBusiness(mContext);

                // Get the stagged resorts
                var lStaggedResortQuotes = mContext.Staging_HotelRates
                    .Include(b => b.HotelStaging).ThenInclude(sub => sub.Amenities)
                    .Where(x => x.HotelStaging != null && x.HotelStaging.QuoteGroupId == aQuoteGroup.Id)
                    .ToList();
                int lStaggedCount = lStaggedResortQuotes.Count();

                lHotelBiz.CreateMissingAccommodations(aQuoteGroup, new StagingDataAccess(mContext).GetAccommodations(aQuoteGroup), this);
                lHotelBiz.CreateMissingRoomTypes(lStaggedResortQuotes, this, aQuoteGroup);
                aQRBiz.DeleteQuoteRequestResorts(aQuoteGroup, TourOperatorID);

                var lHotels = mContext.Accommodations.ToList();
                var lRoomTypes = mContext.RoomTypes.ToList();
                var lStopWatch = Logger.StartStopWatch();
                aQRBiz.CreateResortResults(aQuoteGroup, lHotels, lRoomTypes, lStaggedResortQuotes, this);
                Logger.StopStopWatch(lStopWatch, "CreateResortResults");

                var lCount = mContext.SaveChanges();
                Logger.LogInfo("Saved " + lCount + "QuoteRequestResort records");
            }
            catch (Exception e)
            {
                Logger.LogException("Exception convert Staging Resorts to Prod", e);
                throw e;
            }

        }

        public override double getPriceMultiplier(QuoteGroup aQuoteGroup)
        {
            return 1;
        }
        public override double getChildPriceMultiplier(QuoteGroup aQuoteGroup)
        {
            return 0;
        }

        private string Trim(HtmlNode aNode)
        {
            if (aNode == null)
                return "";
            string lOutput = aNode.InnerText.Replace("\r\n", "").Trim();

            return lOutput;
        }

        private string FormatDate(DateTime aDate)
        {
            string lMonth = aDate.Month.ToString();
            string lDate = aDate.Day.ToString();
            string lYear = aDate.Year.ToString();
            return Pad(lMonth) + "/" + Pad(lDate) + "/" + lYear;
        }

        private string Pad(string aString)
        {
            if (aString.Length > 1)
                return aString;

            return "0" + aString;
        }

        public Staging.FlightHotelInformation PageParse(string aHTML, QuoteGroup aQuoteGroup)
        {
            string FuncName = ClassName + "PageParse()";
            try
            {
                Staging.FlightHotelInformation flightHotelInformation = new Staging.FlightHotelInformation();

                HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
                //htmlDoc.Encoding = Encoding.win
                // There are various options, set as needed
                htmlDoc.OptionFixNestedTags = true;

                // filePath is a path to a file containing the html
                htmlDoc.Load(new MemoryStream(Encoding.UTF8.GetBytes(aHTML)));

                // Use:  htmlDoc.LoadHtml(xmlString);  to load from a string (was htmlDoc.LoadXML(xmlString)


                // ParseErrors is an ArrayList containing any errors from the Load statement
                if (htmlDoc.ParseErrors != null && htmlDoc.ParseErrors.Count() > 1)
                {
                    Logger.LogError($"{FuncName} failed to parse request");

                }
                else
                {
                    if (htmlDoc.DocumentNode != null)
                    {
                        HtmlAgilityPack.HtmlNode lBodyNode = htmlDoc.DocumentNode.SelectSingleNode("//body");
                        if (lBodyNode != null)
                        {
                            flightHotelInformation.Flights = ParseFlights(lBodyNode, aQuoteGroup);
                            flightHotelInformation.Hotels = ParseHotels(lBodyNode);
                        }
                    }
                }

                return flightHotelInformation;
            }
            catch (Exception ex)
            {
                Logger.LogException($"{FuncName} Error Parsing AA Vacation Page : ", ex);
            }

            return null;
        }

        public List<Staging.Hotel> ParseHotels(HtmlNode lBodyNode)
        {
            string FuncName = ClassName + "PageParse()";
            var lOutput = new List<Staging.Hotel>();

            var lResortNodes = lBodyNode.SelectNodes("//div[@class='supplier-info']");
            foreach (var lResortNode in lResortNodes)
            {
                try
                {
                    var lHotel = new Staging.Hotel();
                    ParseHotelDetails(lHotel, lResortNode.SelectSingleNode("./div[1]"));
                    lHotel.HotelRateTypes = ParseHotelRoomTypes(lHotel, lResortNode.SelectSingleNode("./div[2]"));
                    lOutput.Add(lHotel);
                } catch( Exception e )
                {
                    Logger.LogException($"{FuncName} Failed to parse hotel room types", e);
                }
            }

            return lOutput;
        }

        public List<Staging.HotelRate> ParseHotelRoomTypes(Staging.Hotel lHotel, HtmlNode aResortNode)
        {
            lHotel.Price = Trim(aResortNode.SelectSingleNode("./div[1]/div[2]/div[1]/div[3]/span[2]"));

            var lXPathLink = aResortNode.SelectSingleNode("./div[2]/a[2]").XPath;
            HtmlNode lPageHtmlNode = null;
            if (mChrome != null)
            {
                mChrome.FindElement(By.XPath(lXPathLink)).Click();
                lPageHtmlNode = WaitForPageLoad();
            }
            else
            {
                // When working with a static page Load for testing
                List<Staging.HotelRate> lTestingData = new List<Staging.HotelRate>();
                lTestingData.Add(new Staging.HotelRate() { HotelStaging = lHotel, RoomType = "Cheapest Test", Price = lHotel.Price });
                return lTestingData;
            }

            var lOutput = ParsePricePage();
            if (mChrome != null)
            {
                // Go Back to Previous Page
                var lReturnElement = lPageHtmlNode.SelectSingleNode("//div[@class='hotelChangeLink']/a[1]");
                lXPathLink = lReturnElement.XPath;
                mChrome.FindElement(By.XPath(lXPathLink)).Click();
            }
            return lOutput;
        }
        public HtmlNode WaitForPageLoad()
        {
            if (mChrome == null)
                return null;

            string lHTML = "";
            while (true)
            {
                Thread.Sleep(1000);
                lHTML = mChrome.PageSource;
                if (lHTML.Contains("</body>"))
                {
                    break;
                }
            }

            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            //htmlDoc.Encoding = Encoding.win
            // There are various options, set as needed
            htmlDoc.OptionFixNestedTags = true;

            // filePath is a path to a file containing the html
            htmlDoc.Load(new MemoryStream(Encoding.UTF8.GetBytes(lHTML)));

            // ParseErrors is an ArrayList containing any errors from the Load statement
            if (htmlDoc.ParseErrors != null && htmlDoc.ParseErrors.Count() > 1)
            {
                // Handle any parse errors as required

            }
            else
            {

                if (htmlDoc.DocumentNode != null)
                {
                    HtmlAgilityPack.HtmlNode bodyNode = htmlDoc.DocumentNode.SelectSingleNode("//body");

                    if (bodyNode != null)
                    {
                        return bodyNode;
                    }
                }
            }

            return null;
        }

        public List<Staging.HotelRate> ParsePricePage()
        {
            return ParsePricePage(WaitForPageLoad());
        }

        private void ParseHotelDetails(Staging.Hotel aHotel, HtmlNode aResortNode)
        {
            string FuncName = ClassName + $"ParseHotelDetails({aHotel.Name})";

            try
            {
                aHotel.TourOperatorID = TourOperatorID;
                aHotel.Name = ParseName(aResortNode.SelectNodes("./h3[2]/span[2]")[0].InnerText);
                Logger.LogInfo("Hotel Name = " + aHotel.Name);
                var lNode = aResortNode.SelectNodes("./div[1]/span[1]");
                if ( lNode != null && lNode.Count() > 0 )
                    aHotel.Stars = lNode[0].InnerText;
                var lLocationRoot = aResortNode.SelectSingleNode("./ul[1]/li[3]/span[1]");
                if (lLocationRoot != null)
                    aHotel.Location = lLocationRoot.InnerText.Split(',')[0];
                else
                    Logger.LogWarning("Hotel " + aHotel.Name + " didn't have a location");
                aHotel.Amenities = ParseAmenities(aResortNode.SelectNodes(".//ul[@class='amenities_filter']/li/span"), aHotel);
                aHotel.AAPreferred = ParsePreffered(aResortNode.SelectSingleNode(".//span[@class='malibu_special_notices']//b"));
            } catch ( Exception e )
            {
                Logger.LogException($"{FuncName} Failed to parse hotel", e);
            }
        }

        private string ParseName(string innerText)
        {
            string ADULTS_ONLY = "Adults Only";
            if (innerText.Contains(ADULTS_ONLY) == false)
                return innerText;

            return innerText.Replace(ADULTS_ONLY, "").Trim();
        }

        private bool ParsePreffered(HtmlNode htmlNode)
        {
            if (htmlNode == null)
                return false;

            return htmlNode.InnerHtml.Split(',')[0].Trim() == AA_PREFERRED;
        }

        private List<AmenityMap> ParseAmenities(HtmlNodeCollection aHtmlNodes, Staging.Hotel aResort)
        {
            var lOutput = new List<AmenityMap>();
            if (aHtmlNodes == null)
                return lOutput;

            var lDA = new AccommodationDataAccess(mContext);

            foreach (var lNode in aHtmlNodes)
            {
                var lMap = new ResortPageBusiness(mContext).GetStagingHotelAmenityMap(aResort, lNode.InnerText.Split(',')[0]);
                if (lMap != null)
                    lOutput.Add(lMap);
            }
            return lOutput;
        }

        public List<Staging.Flight> ParseFlights(HtmlNode lBodyNode, QuoteGroup aQuoteGroup)
        {
            var lOutput = new List<Staging.Flight>();
            var lFlightNodes = lBodyNode.SelectNodes(".//table[@class='air_details']//tr");

            Guid lLegGUID = Guid.NewGuid();
            var lSide = Staging.Flight.SIDES.DEPARTURE;

            for (int i = 0; i < lFlightNodes.Count; i += 3)
            {
                var lSFlight = new Staging.Flight();
                ParseDepartLeg(lSFlight, i, lFlightNodes, lLegGUID, lSide);
                ParseArriveLeg(lSFlight, (i + 1), lFlightNodes);
                if (lSide == Staging.Flight.SIDES.DEPARTURE && Match(lSFlight.ArrivalLocation, aQuoteGroup.QuoteRequest.DestinationAirPort.Code))
                {
                    lSide = Staging.Flight.SIDES.RETURN;
                    lLegGUID = Guid.NewGuid();
                }
                else if (lSide == Staging.Flight.SIDES.RETURN && Match(lSFlight.ArrivalLocation, aQuoteGroup.QuoteRequest.DepartureAirPort.Code))
                {
                    lSide = Staging.Flight.SIDES.DEPARTURE;
                    lLegGUID = Guid.NewGuid();
                }
                lOutput.Add(lSFlight);
            }
            return lOutput;
        }

        private bool Match(string aAirPort, string code)
        {

            string lTarget = String.Concat(aAirPort.Where(c => !Char.IsWhiteSpace(c)));
            string lAirPortCode = lTarget;
            if (lAirPortCode.Length > 3)
            {
                int lStart = lTarget.IndexOf('(');
                if (lStart < 0)
                    return false;
                lStart += 1;
                int lLen = 3;
                lAirPortCode = lTarget.Substring(lStart, lLen);
            }

            return lAirPortCode.Trim().ToUpper() == code.ToUpper();
        }

        private void ParseArriveLeg(Staging.Flight aSFlight, int aIndex, HtmlNodeCollection aFlightNodes)
        {
            aSFlight.ArrivalTime = aFlightNodes[aIndex].SelectSingleNode("./td[2]/b").InnerText;
            aSFlight.ArrivalLocation = aFlightNodes[aIndex].SelectSingleNode("./td[3]/span").InnerText;
            aSFlight.ArrivalDate = aFlightNodes[aIndex].SelectSingleNode("./td[4]/b").InnerText;
        }

        private void ParseDepartLeg(Staging.Flight aSFlight, int aIndex, HtmlNodeCollection aFlightNodes, Guid aGuid, Staging.Flight.SIDES aSide)
        {
            aSFlight.DepartTime = aFlightNodes[aIndex].SelectSingleNode("./td[2]/b").InnerText;
            aSFlight.DepartLocation = aFlightNodes[aIndex].SelectSingleNode("./td[3]/span").InnerText;
            aSFlight.DepartDate = aFlightNodes[aIndex].SelectSingleNode("./td[4]/b").InnerText;
            var lCarrierNode = aFlightNodes[aIndex].SelectSingleNode("./td[6]/b");
            if (lCarrierNode != null)
                aSFlight.Carrier = lCarrierNode.InnerText;
            aSFlight.Aircraft = aFlightNodes[aIndex].SelectSingleNode("./td[6]/span[3]").InnerText;
            aSFlight.LegGUID = aGuid;
            aSFlight.Side = aSide;
        }

        public string ParsePrice(string aInput)
        {
            try
            {
                // Find the =<br> tag first
                aInput = aInput.Replace(" ", "");
                int lStart = aInput.IndexOf("=");
                if (lStart < 0)
                    return "$0.00";
                lStart = aInput.IndexOf("$", lStart);
                int lEnd = aInput.IndexOf("\r", lStart);
                string lPrice = aInput.Substring(lStart, lEnd - lStart);
                return lPrice;
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to parse price for hotel", e);
                return "";
            }
        }
    }
}
