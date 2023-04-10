using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
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
    public class JourneseBot : WebBotBase, IJourneseSrv
    {

        const string ClassName = "JourneseBot::";
        const string CONNECTINGFLIGHT = "Connecting Flight";
        const string OUTBOUNDFLIGHT = "Outbound Flight";
        const string LEGHEADER = "prod-subheader";
        const string FLIGHERHEADER = "prod-header";
        const string NOFLIGHTSAVAILABLE = "We are unable to find an airfare in our system";

        const string WEBSITE = "https://drhw.tpsww.com/Travel-Agent-Sign-In";
        public JourneseBot(IDbContext aContext) : base ( null )
        {
            mContext = aContext;
            if (aContext != null)
                TourOperatorID = new TourOperatorDataAccess(aContext).Get(TourOperator.JOURNESE).Id;
        }
        public override string GetTourOperatorName()
        {
            return TourOperator.JOURNESE;
        }

        public override ITourOperatorDBConverter GetDBConverter()
        {
            return new JourneseConverter();
        }

        protected void init()
        {
            try
            {
                // TODO Auto-generated method stub
                ChromeOptions lOptions = new ChromeOptions();
                //lOptions.AddArgument("--headless");
                mChrome = new ChromeDriver(lOptions);

                //mChrome = new OpenQA.Selenium.Remote.();
            }
            catch (Exception e)
            {
                string lException = "Failed to create ChromeDrive";
                Logger.LogException(lException, e);
                throw new Exception(lException);
            }
        }

        public override bool Login(string aUserName, string aPassword)
        {
            init();
            mChrome.Navigate().GoToUrl(WEBSITE);
            Logger.LogInfo($"Successfully Navigated to {WEBSITE}");
            mChrome.FindElement(By.Name("existing_account_username_border:existing_account_username_border_body:existing_account_username")).SendKeys(aUserName);
            Logger.LogInfo("Successfully send UserName to website");
            mChrome.FindElement(By.Name("existing_account_password_border:existing_account_password_border_body:existing_account_password")).SendKeys(aPassword);
            Logger.LogInfo("Successfully send password to website");
            ClickButtonUsingXPath("//*[@id='id45']");

            // Get pass teh We Value your Time Screen
            //var lText = mChrome.PageSource "/html/body/div[18]/div/div/div/div/div/div/form/div/div[1]/div/div/div/div[1]/p[2]/strong";
            //ClickLinkXPath("/html/body/div[18]/div/div/div/div/div/div/button/svg/path");
            //mChrome.FindElement(By.Name("existing_account_sign_in")).Click();
            Logger.LogInfo("Successfully clicked login button");
            return true;
        }
        public override double getPriceMultiplier(QuoteGroup aQuoteGroup)
        {
            return aQuoteGroup.QuoteRequest.NumberOfAdults;
        }

        public override double getChildPriceMultiplier(QuoteGroup aQuoteGroup)
        {
            return QuoteRequestBusiness.GetNumberOfChildren(aQuoteGroup.QuoteRequest);
        }
        public override Staging.FlightHotelInformation FindTrips(QuoteGroup aQuoteGroup, string aStartLocation, string aEndLocation, DateTime aDepartDate, DateTime aReturnDate, Quote aBookTrip = null)
        {
            string FuncName = ClassName + $"FindTrips ({aStartLocation}, {aEndLocation}, {aDepartDate}) ";
            try
            {
                int lNumKids = QuoteRequestBusiness.GetNumberOfChildren(aQuoteGroup.QuoteRequest);
                var lLookup = new BlitzerCore.TourOps.Journese(mContext);

                // Need to translate places like CUN -> Cancun
                aEndLocation = lLookup.GetDestinationAirport(aEndLocation);

                Logger.LogDebug($" Depart Location = {aStartLocation}");
                Logger.LogDebug($" Destination Location = {aEndLocation}");

                var lHTML = "";
                for (int lNumberOfStops = 0; lNumberOfStops < 2; lNumberOfStops++)
                {

                    lHTML = ExecuteSearch(aQuoteGroup, aStartLocation, aEndLocation, aDepartDate, aReturnDate, lNumberOfStops);
                    bool lHaveFoundTrips = TestForError(lHTML) == false;
                    if (lHaveFoundTrips)
                        break;
                }
                return PageParse(lHTML, aQuoteGroup);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }
        bool TestForError(string aHTML)
        {
            return aHTML.Contains(NOFLIGHTSAVAILABLE);
        }
        private string ExecuteSearch(QuoteGroup aQuoteGroup, string aStartLocation, string aEndLocation, DateTime aDepartDate, DateTime aReturnDate, int aNumberOfStops)
        {
            string FuncName = ClassName + $"ExecuteSearch";
            int lNumKids = QuoteRequestBusiness.GetNumberOfChildren(aQuoteGroup.QuoteRequest);
            var lVELookup = new BlitzerCore.TourOps.VacationExpress(mContext);

            if (aQuoteGroup.QuoteRequest.QuoteType == QuoteRequest.QuoteTypes.LandOnly)
                mChrome.FindElement(By.Name("packageTypePanelContainer:itineraryOptionPanel:bookingPackageType")).Click();

            // Verify we are on the Query Screen
            Logger.EnterFunction(FuncName);

            if (aQuoteGroup.QuoteRequest.QuoteType != QuoteRequest.QuoteTypes.LandOnly)
            {
                InputTextByName("departureDestinationPanel:departurePanel:feedbackBorder:feedbackBorder_body:searchTerm", aStartLocation);
                // Select non Stop Flights
                ClickCheckBox("travelAgentAdditionalOptionsPanel:airTravelOptionsPanel:mainWrapper:nonStopFlights");
            }
            // Find Destination and input text
            InputTextByName("departureDestinationPanel:destinationPanel:feedbackBorder:feedbackBorder_body:searchTerm", aEndLocation);

            // Set the Start Date
            InputTextByName("primaryTravelDatesPanel:flightDepartureDate:fieldset:feedbackBorder:feedbackBorder_body:date", aDepartDate.ToShortDateString());
            // Set the End DAte
            InputTextByName("primaryTravelDatesPanel:flightReturnDate:fieldset:feedbackBorder:feedbackBorder_body:date", aReturnDate.ToShortDateString());

            // Set the Number of Adults
            var lNumOfAdults = $"{aQuoteGroup.QuoteRequest.NumberOfAdults}";
            SelectTextById("room1", lNumOfAdults);

            // If they have kids, enter info
            SelectTextById("roomId", lNumKids.ToString());
            if (lNumKids > 0)
            {
                // Set the Age of Child 1 
                SelectTextByXPath("//*[@id='travelerAssignSectionId']/div[2]/fieldset/div[2]/div/div[3]/div[1]/select", aQuoteGroup.QuoteRequest.Child1Age.Value.ToString());
                // Set the Age of Child 2 
                if (lNumKids > 1)
                    SelectTextByXPath("//*[@id='travelerAssignSectionId']/div[2]/fieldset/div[2]/div/div[3]/div[2]/select", aQuoteGroup.QuoteRequest.Child2Age.Value.ToString());
            }

            // Click the continue button to start Query
            ClickButton("Search for Flights + Hotels");

            string lHTML = "";
            while (true)
            {
                lHTML = mChrome.PageSource;
                Thread.Sleep(1000);
                if (lHTML.Contains("</body>"))
                    break;
            }

            System.IO.File.WriteAllText(GetFilePath(), lHTML);
            return lHTML;
        }
        protected override string GetFilePath()
        {
            return @"..\Downloads\Journese_WebHTML.html";
        }

        public override void ConvertFlightsFromStagingToProd(QuoteGroup aQuoteGroup)
        {
            try
            {
                var lStaggedFlights = mContext.Staging_Flights.Where(x => x.QuoteGroupId == aQuoteGroup.Id && x.TourOperatorID == TourOperatorID).OrderBy(x => x.LegGUID).ToList();
                var lFlights = new AirBusiness(mContext, null).Convert(lStaggedFlights, aQuoteGroup);
                foreach (var lFlight in lFlights)
                {
                    lFlight.QuoteRequestID = aQuoteGroup.QuoteRequest.QuoteRequestID;
                    lFlight.QuoteGroupId = aQuoteGroup.Id;
                    lFlight.QuoteGroup = aQuoteGroup;
                    lFlight.InBound.Flights.ForEach(x => x.QuoteGroup = aQuoteGroup);
                    lFlight.OutBound.Flights.ForEach(x => x.QuoteGroup = aQuoteGroup);
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
                Logger.LogException("Failed to convert flights from Staging to Prod", e);
                throw e;
            }
        }

        public override void ConvertResortsFromStagingToProd(QuoteGroup aQuoteGroup, QuoteRequestBusiness aQRBiz)
        {
            string FuncName = ClassName + "ConvertResortsFromStagingToProd (QuoteRequest, QuoteRequestBusiness) - ";
            // Update Resort Locations if necessary
            //UpdateAccommodationsFromStagging(aQuoteRequest.QuoteRequestID);

            try
            {
                var lHotelBiz = new HotelBusiness(mContext);
                lHotelBiz.CreateMissingAccommodations(aQuoteGroup, new StagingDataAccess(mContext).GetAccommodations(aQuoteGroup), this);

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
                //htmlDoc.Load(new MemoryStream(Encoding.UTF8.GetBytes(aHTML)));
                //htmlDoc.Load(new MemoryStream(Encoding. GetBytes(aHTML)));
                htmlDoc.LoadHtml(aHTML);
                // Use:  htmlDoc.LoadHtml(xmlString);  to load from a string (was htmlDoc.LoadXML(xmlString)


                // ParseErrors is an ArrayList containing any errors from the Load statement
                if (htmlDoc.ParseErrors != null && htmlDoc.ParseErrors.Count() > 1)
                {
                    Logger.LogError($"Loaded Document with {htmlDoc.ParseErrors.Count()} Error");
                }

                {
                    if (CheckForErrorResponse(out flightHotelInformation))
                        return flightHotelInformation;

                    if (htmlDoc.DocumentNode != null)
                    {
                        HtmlAgilityPack.HtmlNode bodyNode = htmlDoc.DocumentNode.SelectSingleNode("//body");

                        if (bodyNode != null)
                        {
                            // This section is for the package items
                            if (aQuoteGroup.QuoteRequest.QuoteType != QuoteRequest.QuoteTypes.LandOnly)
                                flightHotelInformation.Flights = ProcessFlights(htmlDoc, aQuoteGroup);

                            flightHotelInformation.Hotels = ProcessHotels(htmlDoc, aQuoteGroup);
                        }

                    }

                }

                return flightHotelInformation;
            }
            catch (Exception ex)
            {
                Logger.LogException("Error Parsing Fligh Data from Vacation Express : ", ex);
            }

            return null;
        }

        protected List<Staging.Flight> ProcessFlights(HtmlAgilityPack.HtmlDocument aHTML, QuoteGroup aQuoteGroup)
        {
            var lRemoveTbody = false;
            var lFlightXPath = "//*[@id=\"tdFlightDetails\"]/table/tbody/tr";
            var lFlightRows = aHTML.DocumentNode.SelectNodes(lFlightXPath);
            
            if (lFlightRows == null)
            {
                lRemoveTbody = true;
                lFlightRows = aHTML.DocumentNode.SelectNodes(lFlightXPath.Replace("/tbody", ""));
            }

            if (lFlightRows == null)
                Logger.LogError("Unable to find Flight Node header in HTML");

            var lFlightRowCnt = lFlightRows.Count();

            List<Staging.Flight> airList = new List<Staging.Flight>();

            Guid lLegGUID = Guid.NewGuid();
            // For VE the only provide one flight on the Use Main Screen
            // There is no need to process if we pull the alternative rows because 
            // It iwll duplicate for the entry
            /*
            Guid lItinGUID = Guid.NewGuid();
            Staging.Flight.SIDES lSide = Staging.Flight.SIDES.DEPARTURE;

            try
            {
                var lValidRows = lFlightRows.Count() / 2;
                for (int i = 1; i < lValidRows; i++)
                {
                    if (FlipSide(lFlightRows[i]))
                    {
                        lSide = Staging.Flight.SIDES.RETURN;
                        continue;
                    }

                    var lFlight = ProcFlightNode(lFlightRows[i], lSide);
                    if (lFlight != null)
                    {
                        lFlight.LegGUID = lLegGUID;
                        lFlight.QuoteGroupId = aQuoteGroup.Id;
                        lFlight.Side = lSide;
                        lFlight.ItineraryGUID = lItinGUID;
                        lFlight.TourOperatorID = this.TourOperatorID;

                        if (lSide == Staging.Flight.SIDES.DEPARTURE)
                        {
                            lFlight.DepartDate = aQuoteGroup.QuoteRequest.DepartureDate.ToShortDateString();
                            lFlight.ArrivalDate = aQuoteGroup.QuoteRequest.DepartureDate.ToShortDateString();
                        }
                        else
                        {
                            lFlight.DepartDate = aQuoteGroup.QuoteRequest.ReturnDate.ToShortDateString();
                            lFlight.ArrivalDate = aQuoteGroup.QuoteRequest.ReturnDate.ToShortDateString();

                        }
                        airList.Add(lFlight);
                    }
                    else
                        break;
                }
            }
            catch (Exception e)
            {
                Logger.LogException("Problem processing Flights", e);
            }
            */
            airList.AddRange(ProcessAlternateFlights(aHTML, aQuoteGroup, lRemoveTbody));

            return airList;
        }

        public List<Staging.Flight> ProcessAlternateFlights(HtmlAgilityPack.HtmlDocument aHTML, QuoteGroup aQuoteGroup, bool aRemoveTbody)
        {
            List<Staging.Flight> lOutput = new List<Staging.Flight>();
            Staging.Flight.SIDES lSide = Staging.Flight.SIDES.DEPARTURE;
            Guid lLegGUID = Guid.NewGuid();
            Guid lItinGUID = Guid.NewGuid();
            // Get the alternate flights root node
            string lAltAirPath = "//*[@id=\"alternateair\"]/table";
            if ( aRemoveTbody )
                lAltAirPath = lAltAirPath.Replace("/tbody", "");

            var lFlightTables = aHTML.DocumentNode.SelectNodes(lAltAirPath);

            if (lFlightTables == null)
                Logger.LogError("Unable to find Flight Alternate Node header in HTML");

            foreach (var lFlightsNode in lFlightTables)
            {
                lSide = Staging.Flight.SIDES.DEPARTURE;
                lItinGUID = Guid.NewGuid();
                // This is a table which contains the at least 3 rows
                // 1 for the depart
                // 2 for the line
                // 3 for the return
                //HtmlAgilityPack.HtmlNode lFlightNode = lFlightRow;
                var lLines = lFlightsNode.SelectNodes("./tbody/tr[2]/td/table/tbody/tr");
                var lExtraPrice = GetExtraPrice(lFlightsNode);
                for (int i = 2; i <= lLines.Count(); i++)
                {
                    var lFlightNode = lFlightsNode.SelectSingleNode($"./tbody/tr[2]/td/table/tbody/tr[{i}]");
                    if (FlipSide(lFlightNode.SelectSingleNode("./td")))
                    {
                        lSide = Staging.Flight.SIDES.RETURN;
                        lLegGUID = Guid.NewGuid();
                        continue;
                    }

                    var lFlight = ProcFlightNode(lFlightNode, lSide);
                    if (lFlight != null)
                    {
                        lFlight.LegGUID = lLegGUID;
                        lFlight.QuoteGroupId = aQuoteGroup.Id;
                        lFlight.Side = lSide;
                        lFlight.ItineraryGUID = lItinGUID;
                        lFlight.TourOperatorID = this.TourOperatorID;
                        lFlight.ExtraCost = lExtraPrice;

                        if (lSide == Staging.Flight.SIDES.DEPARTURE)
                        {
                            lFlight.DepartDate = aQuoteGroup.QuoteRequest.DepartureDate.ToShortDateString();
                            lFlight.ArrivalDate = aQuoteGroup.QuoteRequest.DepartureDate.ToShortDateString();
                        }
                        else
                        {
                            lFlight.DepartDate = aQuoteGroup.QuoteRequest.ReturnDate.ToShortDateString();
                            lFlight.ArrivalDate = aQuoteGroup.QuoteRequest.ReturnDate.ToShortDateString();

                        }
                        
                        lOutput.Add(lFlight);
                    }
                    else
                        break;
                }
            }

            //*//*[@id="tdFlightDetails"]/table/tbody/tr[2]/td[1]
            //*[@id="tdAirUpgrade"]/font
            Logger.LogInfo($"Loaded {lOutput.Count()} Alternate flights");
            return lOutput;
        }

        private string GetExtraPrice(HtmlAgilityPack.HtmlNode aFlightNode)
        {
            var lNode = aFlightNode.SelectSingleNode("./tbody/tr[1]/td[2]/font");
            if (lNode != null)
                return lNode.InnerHtml;

            return "";
        }

        private Staging.Flight ProcAltFlightNode(HtmlAgilityPack.HtmlNode aNode, Staging.Flight.SIDES lSide)
        {
            Staging.Flight lFlight = new Staging.Flight();
            try
            {
                var lLocations = aNode.SelectSingleNode("./tbody/tr[2]/td/table/tbody/tr[2]/td").InnerHtml;
                var lNode2 = lLocations.Split(new string[] { " - " }, StringSplitOptions.None);
                lFlight.DepartLocation = lNode2[0];
                lFlight.ArrivalLocation = lNode2[1];
                lFlight.DepartTime = aNode.SelectSingleNode("./tbody/tr[2]/td/table/tbody/tr[2]/td[3]").InnerHtml.Replace("&nbsp;-&nbsp;", "");
                lFlight.ArrivalTime = aNode.SelectSingleNode("./tbody/tr[2]/td/table/tbody/tr[2]/td[4]").InnerHtml.Replace("&nbsp;", "");
                var lAirFields = aNode.SelectSingleNode("./tbody/tr[2]/td/table/tbody/tr[2]/td[5]").InnerHtml.Split(new string[] { "&nbsp;" }, StringSplitOptions.None);
                lFlight.Carrier = lAirFields[0];
                lFlight.Aircraft = lAirFields[1];
            } catch ( Exception )
            {
                return null;
            }
            return lFlight;
        }

        public List<Staging.Hotel> ProcessHotels(HtmlAgilityPack.HtmlDocument aHTML, QuoteGroup aQuoteGroup)
        {
            //*******************hotel
            List<Staging.Hotel> lResorts = new List<Staging.Hotel>();

            var lResortRows = aHTML.DocumentNode.SelectNodes("//*[@id=\"mainresulttable\"]/tbody/tr");
            foreach (var lResortRow in lResortRows)
            {
                try
                {
                    Staging.Hotel lResort = new Staging.Hotel();
                    if (GetResort(lResortRow, ref lResort))
                        GetRoom(lResortRow, ref lResort, aQuoteGroup);

                    //if (lResort.Name != "Barcelo Maya Beach Resort")
                    //    continue;

                    lResorts.Add(lResort);
                }
                catch (Exception e)
                {
                    Logger.LogException("Failed to add Resort", e);
                }
            }
            return lResorts;
        }

        public override List<Staging.Flight> ProcessStagingFlights(List<Staging.Flight> aData)
        {
            aData.Where(x => x.Carrier == null || x.Carrier.Trim() == "").ToList().ForEach(x => x.Carrier = TourOperator.VACATION_EXPRESS);
            return aData;
        }
        public bool GetResort(HtmlAgilityPack.HtmlNode aNode, ref Staging.Hotel aResort)
        {
            var lResortName = ReplaceWhite(aNode.SelectSingleNode("./td/table/tbody/tr/td/a").InnerHtml.Trim());
            if (lResortName.Contains("<img src"))
                return false;
            if (lResortName.Contains("Unavailable Remarks"))
                return false;
            aResort.Name = lResortName;
            aResort.Stars = "1";

            if (aResort.Name == null || aResort.Name == "")
                throw new Exception("Resort Name was null");

            return true;
        }

        public void GetRoom(HtmlAgilityPack.HtmlNode aNode, ref Staging.Hotel aResort, QuoteGroup aQuoteGroup)
        {
            var lRoomsRoot = aNode.SelectNodes("./td/table/tbody/tr[3]/td/table/tbody/tr");
            //*[@id="roomrate35"]/table/tbody/tr[3]/td[2]/input[2]
            for (int i = 2; i < lRoomsRoot.Count() - 1; i++)
            {
                try
                {
                    Staging.HotelRate lRoomType = new Staging.HotelRate() { LandOnly = aQuoteGroup.QuoteRequest.QuoteType == QuoteRequest.QuoteTypes.LandOnly }; ;
                    lRoomType.RoomType = ReplaceWhite(lRoomsRoot[i].SelectSingleNode("./td/a").InnerHtml.Trim());
                    var lStep1 = lRoomsRoot[i].SelectSingleNode("./td[2]/input[2]");
                    if (lStep1 != null)
                    {
                        var lStep2 = lStep1.Attributes["Value"];
                        if (lStep2 != null)
                            lRoomType.Price = lStep2.Value;
                    }
                    // Grap the Child price if it exists
                    var lStepChild = lRoomsRoot[i].SelectSingleNode("./td[3]/input[2]");
                    if (lStepChild != null)
                    {
                        var lKidPrice = lStepChild.Attributes["Value"];
                        if (lKidPrice != null)
                            lRoomType.ChildPrice = lKidPrice.Value;
                    }

                    //if (lRoomType.RoomType != "Family Room")
                    //    continue;

                    aResort.HotelRateTypes.Add(lRoomType);
                }
                catch (Exception e)
                {
                    Logger.LogException("Exception process Room Type", e);
                }
            }
        }

        public string ReplaceWhite(string aInput)
        {
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            var lStep1 = regex.Replace(aInput, " ");
            return lStep1.Replace(Environment.NewLine, "");
        }
        public bool FlipSide(HtmlAgilityPack.HtmlNode aRow)
        {
            return aRow.InnerHtml.Contains("<hr>");
        }

        public Staging.Flight ProcFlightNode(HtmlAgilityPack.HtmlNode aRow, Staging.Flight.SIDES aSide)
        {
            string FuncName = ClassName + $"ProcFlightNode ({aRow}, {aSide}) ";

            Staging.Flight lFlight = new Staging.Flight();
            try
            {
                var lElements = aRow.SelectNodes("./td");
                var lLocations = lElements[0].InnerHtml.Trim();
                if (lLocations.Split(new string[] { " - " }, StringSplitOptions.None).Count() < 2)
                    return null;

                lFlight.DepartLocation = lLocations.Split(new string[] { " - " }, StringSplitOptions.None)[0];
                lFlight.ArrivalLocation = lLocations.Split(new string[] { " - " }, StringSplitOptions.None)[1];
                var lDepartDate = lElements[1].InnerHtml.Trim();
                var lDepartTime = lElements[2].InnerHtml.Trim();
                var lArriveTime = lElements[3].InnerHtml.Trim();
                lFlight.DepartTime = lDepartTime.Replace("&nbsp;-&nbsp;", "");
                lFlight.ArrivalTime = lArriveTime.Replace("&nbsp;", "");
                var lAirLineData = lElements[4].InnerHtml.Split(new string[] { "&nbsp;" }, StringSplitOptions.None);
                lFlight.Carrier = lAirLineData[0];
                lFlight.Aircraft = lAirLineData[1];
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Exception", e);
                return null;
            }
            return lFlight;
        }


        private bool CheckForErrorResponse(out Staging.FlightHotelInformation aFlightHotelInformation)
        {
            aFlightHotelInformation = new Staging.FlightHotelInformation();
            return false;
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
