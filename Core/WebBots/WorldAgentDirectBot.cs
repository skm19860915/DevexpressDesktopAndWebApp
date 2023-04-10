using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System.IO;
using System.Text;
using BlitzerCore.Models;
using BlitzerCore.Utilities;
using BlitzerCore.DataAccess;
using BlitzerCore.Business;
using BlitzerCore.Business.DBConverters;

namespace BlitzerCore.WebBots
{
    public class WorldAgentDirectBot : WebBotBase, IDeltaVacationSrv
    {

        const string ClassName = "WorldAgentDirectBot::";
        const string CONNECTINGFLIGHT = "Connecting Flight";
        const string OUTBOUNDFLIGHT = "Outbound Flight";
        const string LEGHEADER = "prod-subheader";
        const string FLIGHERHEADER = "prod-header";

        const string WEBSITE = "http://www.worldagentdirect.com";
        public WorldAgentDirectBot(IDbContext aContext) : base (null)
        {
            mContext = aContext;
            if (aContext != null)
                TourOperatorID = new TourOperatorDataAccess(aContext).Get(TourOperator.DELTA_VACATIONS).Id;
        }
        public override string GetTourOperatorName()
        {
            return TourOperator.DELTA_VACATIONS;
        }

        public override ITourOperatorDBConverter GetDBConverter()
        {
            return new WorldAgentDirectConverter();
        }

        protected void init()
        {
            try
            {
                // TODO Auto-generated method stub
                ChromeOptions lOptions = new ChromeOptions();
                // This is required to run on the Web Server
                lOptions.AddArgument("--headless");
                mChrome = new ChromeDriver(lOptions);
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
            //mChrome.Manage().Window.Maximize();
            mChrome.FindElement(By.Name("arcNumber")).SendKeys(IATA_Number);
            mChrome.FindElement(By.Name("userId")).SendKeys(aUserName);
            mChrome.FindElement(By.Name("password")).SendKeys(aPassword);
            mChrome.FindElement(By.XPath("//*[@id='commonLoginForm']/table/tbody/tr[4]/td/input")).Click();
            return true;
        }

        public override Staging.FlightHotelInformation FindTrips(QuoteGroup aQuoteGroup, string aStartLocation, string aEndLocation, DateTime aDepartDate, DateTime aReturnDate, Quote aBookTrip = null)
        {
            string FuncName = ClassName + $"FindTrips ({aStartLocation}, {aEndLocation}, {aDepartDate}) ";
            try
            {
                Logger.EnterFunction(FuncName);
                mChrome.FindElement(By.Name("originAirportCode")).Clear();
                var lOrigin = mChrome.FindElement(By.Id("originAirportCode"));
                if (lOrigin != null)
                    lOrigin.SendKeys(aStartLocation);
                else
                {
                    string lErrMsg = $"{FuncName} - Unable to find the Origin Airport Field";
                    Logger.LogError(lErrMsg);
                    throw new InvalidOperationException(lErrMsg);
                }
                mChrome.FindElement(By.Name("destinationAirportCode")).SendKeys(aEndLocation);
                mChrome.FindElement(By.Name("departureDate")).SendKeys(aDepartDate.ToShortDateString());
                mChrome.FindElement(By.Name("returnDate")).SendKeys(aReturnDate.ToShortDateString());
                mChrome.FindElement(By.Name("partyComposition.adultCount")).Clear();
                mChrome.FindElement(By.Name("partyComposition.adultCount")).SendKeys(aQuoteGroup.QuoteRequest.NumberOfAdults.ToString());
                Logger.LogInfo($"Set the number of Adults to {aQuoteGroup.QuoteRequest.NumberOfAdults}");

                if (aQuoteGroup.QuoteRequest.QuoteType == QuoteRequest.QuoteTypes.LandOnly)
                {
                    // Click the Hotel Only Radio Button
                    mChrome.FindElement(By.XPath("//*[@id='pkgSharedBookingSchedAirQuickBookForm']/table[1]/tbody/tr[4]/td/table/tbody/tr[2]/td[3]/input")).Click();
                }

                // Send Age of Child 1
                if (aQuoteGroup.QuoteRequest.Child1Age != null && aQuoteGroup.QuoteRequest.Child1Age > 0)
                    mChrome.FindElement(By.XPath("//*[@id='pkgSharedBookingSchedAirQuickBookForm']/table[4]/tbody/tr[4]/td/table/tbody/tr/td[2]/input[1]")).SendKeys(aQuoteGroup.QuoteRequest.Child1Age.ToString());

                // Send age of child 2
                if (aQuoteGroup.QuoteRequest.Child2Age != null && aQuoteGroup.QuoteRequest.Child2Age > 0)
                    mChrome.FindElement(By.XPath("//*[@id='pkgSharedBookingSchedAirQuickBookForm']/table[4]/tbody/tr[4]/td/table/tbody/tr/td[2]/input[2]")).SendKeys(aQuoteGroup.QuoteRequest.Child2Age.ToString());

                // Click key to input all data
                mChrome.FindElement(By.XPath("//*[@id='pkgSharedBookingSchedAirQuickBookForm']/table[4]/tbody/tr[11]/td/input")).Click();

                string lHTML = "";
                while (true)
                {
                    lHTML = mChrome.PageSource;
                    Thread.Sleep(1000);
                    if (lHTML.Contains("</body>"))
                        break;
                }

                System.IO.File.WriteAllText(GetFilePath(), lHTML);

                return PageParse(lHTML, aQuoteGroup);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }

        public override void ConvertFlightsFromStagingToProd(QuoteGroup aQuoteGroup)
        {
            int aInputRequestID = aQuoteGroup.Id;

            try
            {
                var lStaggedFlights = mContext.Staging_Flights.Where(x => x.QuoteGroupId == aInputRequestID && x.TourOperatorID == TourOperatorID).OrderBy(x => x.LegGUID).ToList();
                var lFlights = new AirBusiness(mContext, null).Convert(lStaggedFlights, aQuoteGroup);
                if (aQuoteGroup.QuoteRequest.QuoteType != QuoteRequest.QuoteTypes.LandOnly)
                {
                    foreach (var lFlight in lFlights)
                    {
                        lFlight.QuoteGroup = aQuoteGroup;
                        lFlight.QuoteRequestID = aQuoteGroup.QuoteRequest.QuoteRequestID;
                        lFlight.QuoteGroupId = aQuoteGroup.Id;
                        lFlight.InBound.Flights.ForEach(x => x.QuoteGroup = aQuoteGroup);
                        lFlight.OutBound.Flights.ForEach(x => x.QuoteGroup = aQuoteGroup);
                    }
                    var lCount = new QuoteDataAccess(mContext).Save(lFlights);

                    foreach (var lQRFlight in lFlights )
                    {
                        lQRFlight.InBound.TripTicketId = lQRFlight.FlightItineraryId;
                        lQRFlight.OutBound.TripTicketId = lQRFlight.FlightItineraryId;
                    }
                    lCount = new QuoteDataAccess(mContext).Save(lFlights);
                }
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to convert flights from Staging to Prod", e);
                throw e;
            }
        }

        public override double getPriceMultiplier(QuoteGroup aQuoteGroup)
        {
            return 1;
        }
        protected override string GetFilePath()
        {
            return @"..\Downloads\WAD_WebHTML.html";
        }

        public override void ConvertResortsFromStagingToProd(QuoteGroup aQuoteGroup, QuoteRequestBusiness aQRBiz)
        {
            string FuncName = ClassName + "ConvertResortsFromStagingToProd (QuoteRequest, QuoteRequestBusiness) - ";
            // Update Resort Locations if necessary
            //UpdateAccommodationsFromStagging(aQuoteRequest.QuoteRequestID);
            var lHotelBiz = new HotelBusiness(mContext);

            int aInputQueryID = aQuoteGroup.Id;
            int lOutputQueryID = aQuoteGroup.Id;

            try
            {
                lHotelBiz.CreateMissingAccommodations(aQuoteGroup, new StagingDataAccess(mContext).GetAccommodations(aQuoteGroup).Where(x=>x.TourOperatorID == TourOperatorID) , this);

                // Get the stagged resorts
                var lStaggedResortQuotes = mContext.Staging_HotelRates
                    .Include(b => b.HotelStaging)
                    .Where(x => x.HotelStaging != null && x.HotelStaging.QuoteGroupId == aQuoteGroup.Id && x.HotelStaging.TourOperatorID == TourOperatorID)
                    .ToList();
                int lStaggedCount = lStaggedResortQuotes.Count();

                lHotelBiz.CreateMissingRoomTypes(lStaggedResortQuotes, this, aQuoteGroup);
                aQRBiz.DeleteQuoteRequestResorts(aQuoteGroup, TourOperatorID);

                var lHotels = mContext.Accommodations.ToList();
                var lRoomTypes = mContext.RoomTypes.ToList();
                var lStopWatch = Logger.StartStopWatch();
                aQRBiz.CreateResortResults(aQuoteGroup, lHotels, lRoomTypes, lStaggedResortQuotes, this);
                Logger.StopStopWatch(lStopWatch, "CreateResortResults");

                var lCount = mContext.SaveChanges();
                Logger.LogInfo(FuncName + "Saved " + lCount + "QuoteRequestResort records");
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + "Exception convert Staging Resorts to Prod", e);
                throw e;
            }
        }

        public Staging.FlightHotelInformation PageParse(string aHTML, QuoteGroup aQuoteGroup)
        {
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
                    // Handle any parse errors as required

                }
                else
                {
                    if (CheckForErrorResponse(out flightHotelInformation))
                        return flightHotelInformation;

                    if (htmlDoc.DocumentNode != null)
                    {
                        HtmlAgilityPack.HtmlNode bodyNode = htmlDoc.DocumentNode.SelectSingleNode("//body");

                        if (bodyNode != null)
                        {
                            var flights = bodyNode.SelectNodes("//div[@class='selected_flights']//table//tr");

                            var viewMoreFlights = bodyNode.SelectNodes("//div[@id='viewMoreFlights']//table//tr");

                            List<Staging.Flight> airList = new List<Staging.Flight>();

                            if ( aQuoteGroup.QuoteRequest.QuoteType != QuoteRequest.QuoteTypes.LandOnly )
                            {
                                var flight = flights.Skip(1).ToList();
                                Guid lLegGUID = Guid.NewGuid();
                                Guid lItinGUID = Guid.NewGuid();
                                Staging.Flight.SIDES lSide = Staging.Flight.SIDES.RETURN;
                                string lPrice = "";

                                for (int i = 0; i < flight.Count(); i++)
                                {

                                    Staging.Flight flt = new Staging.Flight();
                                    var lFlightHTML = flight[i];
                                    flt.ItineraryGUID = lItinGUID;
                                    HtmlAgilityPack.HtmlNode lPreviousHTML = null;
                                    if (i > 0)
                                        lPreviousHTML = flight[i - 1];

                                    // Get the Price from the Header which contains the price
                                    if (lFlightHTML.OuterHtml.Contains(FLIGHERHEADER))
                                    {
                                        lPrice = lFlightHTML.SelectSingleNode("<span>").InnerText.Trim();
                                    }
                                    // It hits this section for the start of every new flight
                                    if (lFlightHTML.OuterHtml.Contains(LEGHEADER))
                                    {
                                        flt.Carrier = lFlightHTML.SelectSingleNode("td[1]").InnerText;

                                        if (flt.Carrier.Contains(OUTBOUNDFLIGHT))
                                        {
                                            lSide = Staging.Flight.SIDES.DEPARTURE;
                                        }
                                        else
                                        {
                                            lSide = Staging.Flight.SIDES.RETURN;
                                            lLegGUID = Guid.NewGuid();
                                        }
                                    }
                                    else if (i > 0 && (lPreviousHTML.OuterHtml.Contains(LEGHEADER) ||
                                        lPreviousHTML.OuterHtml.Contains(CONNECTINGFLIGHT)))
                                    {
                                        flt.QuoteGroupId = aQuoteGroup.Id;
                                        flt.DepartDate = lFlightHTML.SelectSingleNode("td[2]").InnerText.Trim();
                                        string[] route = lFlightHTML.SelectSingleNode("td[3]").InnerText.Replace("\r\n\t", ";").Split(';');
                                        flt.DepartLocation = route.Length > 0 ? route[0].Trim().Replace("to", "") : string.Empty;
                                        flt.ArrivalLocation = route.Length > 0 ? route[1].Trim() : string.Empty;
                                        string[] time = lFlightHTML.SelectSingleNode("td[4]").InnerText.Replace("\r\n\t", ";").Split(';');
                                        flt.DepartTime = time.Length > 0 ? time[0].Trim().Replace("departs", "") : string.Empty;
                                        flt.ArrivalTime = time.Length > 0 ? time[1].Trim().Replace("arrives", "") : string.Empty;
                                        string craftstop = lFlightHTML.SelectSingleNode("td[5]").InnerText;
                                        flt.Aircraft = craftstop.Substring(0, craftstop.LastIndexOf("\r\n\t") + 1).Trim();
                                        flt.NumberOfStop = craftstop.Substring(craftstop.LastIndexOf("\r\n\t") + 1).Trim();
                                        flt.LegGUID = lLegGUID;
                                        flt.ItineraryGUID = lItinGUID;
                                        flt.Side = lSide;
                                        flt.TourOperatorID = this.TourOperatorID;
                                        airList.Add(flt);
                                    }
                                    flt.Carrier = "Delta Airlines";
                                }

                                var viewMorFlights = viewMoreFlights.Skip(1).ToList();
                                for (int i = 0; i < viewMorFlights.Count(); i++)
                                {

                                    Staging.Flight flt = new Staging.Flight();
                                    flt.ItineraryGUID = lItinGUID;
                                    // Get the Price from the Header which contains the price
                                    if (viewMorFlights[i].OuterHtml.Contains(FLIGHERHEADER))
                                    {
                                        var lData = viewMorFlights[i];
                                        try
                                        {
                                            var lTempNode = lData.SelectSingleNode(".//span");
                                            var lNode = lData.SelectSingleNode(".//span");
                                            if (lNode != null)
                                            {
                                                var lInnerText = lNode.InnerText;
                                                if (lInnerText != null)
                                                    lPrice = lInnerText.Trim();
                                                else
                                                    Logger.LogError("Flight Inner Text is null");
                                            }
                                            else
                                                Logger.LogError("Failed to find Span node in flight element");
                                        }
                                        catch (Exception e2)
                                        {
                                            Logger.LogException("Failed to find <span> node in " + lData, e2);
                                        }
                                    }

                                    if (viewMorFlights[i].OuterHtml.Contains(LEGHEADER))
                                    {
                                        flt.Carrier = viewMorFlights[i].SelectSingleNode("td[1]").InnerText;

                                        if (flt.Carrier.Contains(OUTBOUNDFLIGHT))
                                        {
                                            lSide = Staging.Flight.SIDES.DEPARTURE;
                                            lItinGUID = Guid.NewGuid();
                                            lLegGUID = Guid.NewGuid();
                                        }
                                        else
                                        {
                                            lSide = Staging.Flight.SIDES.RETURN;
                                            lLegGUID = Guid.NewGuid();
                                        }
                                    }
                                    else if (i > 0 && (viewMorFlights[i - 1].OuterHtml.Contains(LEGHEADER) ||
                                      viewMorFlights[i - 1].OuterHtml.Contains(CONNECTINGFLIGHT)))
                                    {
                                        try
                                        {
                                            flt.ExtraCost = ExtractExtraPrice(viewMoreFlights[i]);
                                            flt.QuoteGroupId = aQuoteGroup.Id;
                                            flt.DepartDate = viewMorFlights[i].SelectSingleNode("td[2]").InnerText.Trim();
                                            string[] route = viewMorFlights[i].SelectSingleNode("td[3]").InnerText.Replace("\r\n\t", ";").Split(';');
                                            flt.DepartLocation = route.Length > 0 ? route[0].Trim().Replace("to", "") : string.Empty;
                                            flt.ArrivalLocation = route.Length > 0 ? route[1].Trim() : string.Empty;
                                            string[] time = viewMorFlights[i].SelectSingleNode("td[4]").InnerText.Replace("\r\n\t", ";").Split(';');
                                            flt.DepartTime = time.Length > 0 ? time[0].Trim().Replace("departs", "") : string.Empty;
                                            flt.ArrivalTime = time.Length > 0 ? time[1].Trim().Replace("arrives", "") : string.Empty;
                                            string craftstop = viewMorFlights[i].SelectSingleNode("td[5]").InnerText;
                                            flt.Aircraft = craftstop.Substring(0, craftstop.LastIndexOf("\r\n\t") + 1).Trim();
                                            flt.NumberOfStop = craftstop.Substring(craftstop.LastIndexOf("\r\n\t") + 1).Trim();
                                            flt.LegGUID = lLegGUID;
                                            flt.Side = lSide;
                                            airList.Add(flt);
                                        }
                                        catch (Exception e1)
                                        {
                                            Logger.LogException("Failed to process Additional flights", e1);
                                        }
                                    }

                                }
                            } // End of Process Flight
                            //*******************hotel

                            var hotelList = bodyNode.SelectNodes("//div[@class='item']//ul//li");

                            List<Staging.Hotel> hotels = new List<Staging.Hotel>();

                            for (int j = 0; j < hotelList.Count(); j++)
                            {
                                Staging.Hotel hotel = new Staging.Hotel();

                                hotel.Stars = hotelList[j].SelectSingleNode("table/tbody/tr/td[2]/table/tbody/tr[1]/td/table/tbody/tr/td[1]/img").Attributes["alt"].Value;
                                var lStep1 = hotelList[j].SelectSingleNode("table/tbody/tr/td[2]/table/tbody/tr[1]/td/table/tbody/tr/td[2]/a");
                                var lStep2 = lStep1.InnerText;
                                hotel.Name = lStep2.Trim();
                                hotel.Location = hotelList[j].SelectSingleNode("table/tbody/tr/td[2]/table/tbody/tr[3]/td").InnerText.Trim();
                                var roomTypeChildNodes = (hotelList[j].SelectNodes("table/tbody/tr/td[2]/table/tbody/tr[4]/td/div/table/tbody/tr"));
                                hotel.QuoteGroupId = aQuoteGroup.Id;
                                if (roomTypeChildNodes != null)
                                {
                                    for (int k = 0; k < roomTypeChildNodes.Count(); k++)
                                    {
                                        Staging.HotelRate hoteltype = new Staging.HotelRate() { LandOnly = aQuoteGroup.QuoteRequest.QuoteType == QuoteRequest.QuoteTypes.LandOnly }; ;
                                        hoteltype.RateType = roomTypeChildNodes[k].SelectSingleNode("td[1]").InnerText.Trim();
                                        hoteltype.RoomType = roomTypeChildNodes[k].SelectSingleNode("td[2]").InnerText.Trim();
                                        hoteltype.Price = ParsePrice(roomTypeChildNodes[k].SelectSingleNode("td[4]").InnerText.Trim());

                                        //if (hoteltype.RoomType != "Family Room")
                                        //    continue;

                                        hotel.HotelRateTypes.Add(hoteltype);
                                    }
                                }

                                //if (hotel.Name != "Barcelo Maya Beach Resort")
                                //    continue;

                                hotels.Add(hotel);
                            }

                            flightHotelInformation.Flights = airList;
                            flightHotelInformation.Hotels = hotels;
                        }

                    }

                }

                return flightHotelInformation;
            }
            catch (Exception ex)
            {
                Logger.LogException("Error Parsing Fligh Data from WorldAgentDiret : ", ex);
            }

            return null;
        }

        private string ExtractExtraPrice(HtmlAgilityPack.HtmlNode aNode)
        {
            var lTextNode = aNode.SelectSingleNode("./td/span");

            if (lTextNode == null)
                return "";
            var lPrice = lTextNode.InnerHtml;
            lPrice = lPrice.Replace(" ", "");
            lPrice = lPrice.Replace(Environment.NewLine, "");
            int lStart = lPrice.IndexOf("-->") + 3;
            int lEnd = lPrice.IndexOf("=", lStart);
            if (lEnd < lStart)
                return "";
            lPrice = lPrice.Substring(lStart, lEnd - lStart);
            return lPrice;
        }

        private bool CheckForErrorResponse(out Staging.FlightHotelInformation aFlightHotelInformation)
        {
            aFlightHotelInformation = new Staging.FlightHotelInformation();
            return false;
        }

        public override List<Staging.Flight> ProcessStagingFlights(List<Staging.Flight> aData)
        {
            aData.Where(x=>x.Carrier == null || x.Carrier.Trim() == "").ToList().ForEach(x => x.Carrier = TourOperator.DELTA_VACATIONS);
            return aData;
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
