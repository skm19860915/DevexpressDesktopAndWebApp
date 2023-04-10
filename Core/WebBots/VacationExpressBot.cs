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
using BlitzerCore.Helpers;
using HtmlAgilityPack;

namespace BlitzerCore.WebBots
{
    public class VacationExpressBot : WebBotBase, IVacationExpressSrv
    {

        const string ClassName = "VacationExpressBot::";
        const string CONNECTINGFLIGHT = "Connecting Flight";
        const string OUTBOUNDFLIGHT = "Outbound Flight";
        const string LEGHEADER = "prod-subheader";
        const string FLIGHERHEADER = "prod-header";
        const string NOFLIGHTSAVAILABLE = "We are unable to find an airfare in our system";

        const string WEBSITE = "https://pro.vacationexpress.com/res/stwmain.aspx?action=HOME";
        public VacationExpressBot(IDbContext aContext, IWebDriver aWebDriver = null) : base (aWebDriver)
        {
            mContext = aContext;
            if (aContext != null)
                TourOperatorID = new TourOperatorDataAccess(aContext).Get(TourOperator.VACATION_EXPRESS).Id;
        }
        public override string GetTourOperatorName()
        {
            return TourOperator.VACATION_EXPRESS;
        }

        public override ITourOperatorDBConverter GetDBConverter()
        {
            return new VacationExpressConverter();
        }

        protected void init()
        {
            try
            {
                // TODO Auto-generated method stub
                ChromeOptions lOptions = new ChromeOptions();
                // This is required to run on the Web Server
                lOptions.AddArgument("--headless");
                //lOptions.AddArgument("--start-maximized");
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
            //mChrome.Manage().Window.Maximize();
            //mChrome.FindElement(By.Name("arcNumber")).SendKeys(IATA_Number);
            //mChrome.FindElement(By.Name("txtloginemai")).SendKeys(aUserName);
            mChrome.FindElement(By.Name("txtloginemail")).SendKeys(aUserName);
            Logger.LogInfo("Successfully send UserName to website");
            mChrome.FindElement(By.Name("txtloginpassword")).SendKeys(aPassword);
            Logger.LogInfo("Successfully send password to website");
            //mChrome.FindElement(By.ClassName ("//*[@id=\"interstitial - screen\"]/div/div/div[2]/div[2]/div[3]/div[2]/button")).Click();
            mChrome.FindElement(By.ClassName("floatright")).Click();
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
                var lVELookup = new BlitzerCore.TourOps.VacationExpress(mContext);

                // If there are Kids, switch to family resort
                if (lNumKids > 0 && aEndLocation == "CUN")
                {
                    aEndLocation = lVELookup.GetDestinationAirport("-CN");
                    Logger.LogDebug($"Switch Destination to Family resort with Kids {aEndLocation}");
                }
                else
                {
                    aEndLocation = lVELookup.GetDestinationAirport(aEndLocation);
                }

                Logger.LogDebug($" Depart Location = {aStartLocation}");
                Logger.LogDebug($" Destination Location = {aEndLocation}");

                // Goto the New Booking Screen
                //mChrome.FindElement(By.LinkText("New Booking")).Click();
                ClickLink("New Booking");
                var lHTML = "";
                for (int lNumberOfStops = 0; lNumberOfStops < 2; lNumberOfStops++)
                {

                    lHTML = ExecuteSearch(aQuoteGroup, aStartLocation, aEndLocation, aDepartDate, aReturnDate, lNumberOfStops);
                    bool lHaveFoundTrips = TestForError(lHTML) == false;
                    if (lHaveFoundTrips)
                        break;
                }
                return PageParse(lHTML, aQuoteGroup, aBookTrip);
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
                mChrome.FindElement(By.XPath("//*[@id='imgl']")).Click();

            // Verify we are on the Query Screen
            Logger.EnterFunction(FuncName);
            var lTargetElement = By.Id("productcode");
            WaitForElement(lTargetElement);

            if (aQuoteGroup.QuoteRequest.QuoteType != QuoteRequest.QuoteTypes.LandOnly)
            {
                var lDepartID = By.Id("cbDepart");
                WaitForElement(lDepartID);
                var lDepartField = mChrome.FindElement(lDepartID);
                // Find the depature select list
                try
                {
                    SelectElement lDepartList = new SelectElement(lDepartField);
                    lDepartList.SelectByValue(aStartLocation);
                }
                catch (Exception)
                {
                    string lErrMsg = $"{FuncName} - Unable to find the Depart Airport Field";
                    Logger.LogError(lErrMsg);
                    throw new InvalidOperationException(lErrMsg);
                }

                // Select non Stop Flights
                {
                    ClickLink("Advanced Options");
                    var lConnectionLinkXPath = "//*[@id='maxstops']";
                    try
                    {
                        var lMaxStops = mChrome.FindElement(By.XPath(lConnectionLinkXPath));
                        SelectElement lKidSelect = new SelectElement(lMaxStops);
                        lKidSelect.SelectByValue($"{aNumberOfStops}");
                    }
                    catch (Exception)
                    {
                        string lErrMsg = $"{FuncName} - Unable to Select Max Number Of Stops";
                        Logger.LogError(lErrMsg);
                        throw new InvalidOperationException(lErrMsg);
                    }

                }
            }
            // Find Destination and input text
            var lDestination = mChrome.FindElement(By.Name("productcode"));
            try
            {
                SelectElement lSelect = new SelectElement(lDestination);
                lSelect.SelectByText(aEndLocation);
            }
            catch (Exception)
            {
                string lErrMsg = $"{FuncName} - Unable to find the Destination Airport Field";
                Logger.LogError(lErrMsg);
                throw new InvalidOperationException(lErrMsg);
            }

            // Set the Start Date
            mChrome.FindElement(By.Name("tbCheckInHotelReq")).Clear();
            mChrome.FindElement(By.Name("tbCheckInHotelReq")).SendKeys(aDepartDate.ToShortDateString());
            // Set the End DAte
            mChrome.FindElement(By.Name("tbCheckOutHotelReq")).Clear();
            mChrome.FindElement(By.Name("tbCheckOutHotelReq")).SendKeys(aReturnDate.ToShortDateString());

            // Set the Number of Adults
            var lNumOfAdults = $"{aQuoteGroup.QuoteRequest.NumberOfAdults}";
            try
            {
                var lGUIAdults = mChrome.FindElement(By.Name("numadts"));
                SelectElement lAdultSelect = new SelectElement(lGUIAdults);
                lAdultSelect.SelectByText(lNumOfAdults);
                Logger.LogInfo($"Set the number of Adults to {lNumOfAdults}");
            }
            catch (Exception)
            {
                string lErrMsg = $"{FuncName} - Unable to Set Number of Adults to {lNumOfAdults}";
                Logger.LogError(lErrMsg);
                throw new InvalidOperationException(lErrMsg);
            }


            // If they have kids, enter info
            if (lNumKids > 0)
            {
                try
                {
                    var lGUINumKids = mChrome.FindElement(By.XPath("//*[@id='room1']/td[4]/select"));
                    SelectElement lKidSelect = new SelectElement(lGUINumKids);
                    lKidSelect.SelectByText($"{ lNumKids}");
                }
                catch (Exception)
                {
                    string lErrMsg = $"{FuncName} - Unable to Set Number of Kids to ${lNumKids}";
                    Logger.LogError(lErrMsg);
                    throw new InvalidOperationException(lErrMsg);
                }

                // Set the Age of Child 1 
                try
                {
                    var lGUIKid1 = mChrome.FindElement(By.XPath("//*[@id='chd1age1']"));
                    SelectElement lKidSelect = new SelectElement(lGUIKid1);
                    lKidSelect.SelectByText($"{aQuoteGroup.QuoteRequest.Child1Age.Value}");
                }
                catch (Exception)
                {
                    string lErrMsg = $"{FuncName} - Unable to Set Age of Kid 1 to ${aQuoteGroup.QuoteRequest.Child1Age.Value}";
                    Logger.LogError(lErrMsg);
                    throw new InvalidOperationException(lErrMsg);
                }

                // Set the Age of Child 2 
                if (lNumKids > 1)
                {
                    try
                    {
                        var lGUIKid2 = mChrome.FindElement(By.XPath("//*[@id='chd2age1']"));
                        SelectElement lKidSelect = new SelectElement(lGUIKid2);
                        lKidSelect.SelectByText($"{aQuoteGroup.QuoteRequest.Child2Age.Value}");
                    }
                    catch (Exception)
                    {
                        string lErrMsg = $"{FuncName} - Unable to Set Age of Kid 2 to ${aQuoteGroup.QuoteRequest.Child2Age.Value}";
                        Logger.LogError(lErrMsg);
                        throw new InvalidOperationException(lErrMsg);
                    }
                }
            }
            else
            {
                // Need to set number of kids to zero in case the last query had kids
                try
                {
                    var lGUINumKids = mChrome.FindElement(By.XPath("//*[@id='room1']/td[4]/select"));
                    SelectElement lKidSelect = new SelectElement(lGUINumKids);
                    lKidSelect.SelectByText($"{lNumKids}");
                }
                catch (Exception)
                {
                    string lErrMsg = $"{FuncName} - Unable to Reset Number of Kids to ${lNumKids}";
                    Logger.LogError(lErrMsg);
                    throw new InvalidOperationException(lErrMsg);
                }
            }

            // Click the continue button to start Query
            mChrome.FindElement(By.XPath("//*[@id=\"btnContinue\"]")).Click();

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
            return @"..\Downloads\VE_WebHTML.html";
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
                var lDisintCnt = new StagingDataAccess(mContext).GetAccommodations(aQuoteGroup).Select(x => x.Name)
                    .Distinct();
                lHotelBiz.CreateMissingAccommodations(aQuoteGroup, new StagingDataAccess(mContext).GetAccommodations(aQuoteGroup).Where(x => x.TourOperatorID == TourOperatorID), this);

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
        public override bool MakePayment(Payment aPayment)
        {
            string FuncName = ClassName + $"MakePayment (Payment[{aPayment.PaymentID}]) - ";
            var lAgentId = aPayment.Booking.Trip.AgentId;
            var lAgentLogin = new ContactDataAccess(mContext).GetLogin(TourOperatorID, lAgentId);
            if (lAgentLogin == null)
            {
                Logger.LogError(FuncName + "Travel Agent Login required to make payment for OperatorID = [" + TourOperatorID + "] Agent ID = [" + lAgentId + "]");
                throw new InvalidOperationException(FuncName + "Missing User Login data");
            }

            Login(lAgentLogin.UserName, lAgentLogin.Password);

            // There seams to be an issue with advancing to fast
            int lWaitTime = new Random().Next(1500, 3500);
            System.Threading.Thread.Sleep(lWaitTime);

            // Click on My Reservations
            ClickLink("My Reservations");
            // Verify the booking number is there
            if (LinkTextExists(aPayment.Booking.BookingNumber) == false)
            {
                Logger.LogWarning($"{FuncName} - Could not find the booking number {aPayment.Booking.BookingNumber} on the agents login screen");
                throw new BlitzerCore.Models.Exceptions.BookingDoesnotExist();
            }
            // Click Reservations Number
            ClickLink(aPayment.Booking.BookingNumber);

            // There seams to be an issue with advancing to fast
            lWaitTime = new Random().Next(1500, 3500);
            System.Threading.Thread.Sleep(lWaitTime);

            // Get the Total Due
            //*[@id="main_form"]/table[1]/tbody/tr[1]/td/table/tbody/tr[3]/td/table/tbody/tr[2]/td[3]/table/tbody/tr[6]/td
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.OptionFixNestedTags = true;
            htmlDoc.LoadHtml(mChrome.PageSource);

            var lXPath1 = "//*[@id='main_form']/table[1]/tbody/tr[1]/td/table/tbody/tr[3]/td/table/tbody/tr[2]/td[3]/table/tbody/tr[6]/td";
            var lXPath2 = "//*[@id='main_form']/table[1]/tr[1]/td/table/tr[3]/td/table/tr[2]/td[3]/table/tr[6]/td";
            HtmlNode lTotalNode = htmlDoc.DocumentNode.SelectSingleNode(lXPath1);
            if (lTotalNode == null)
                lTotalNode = htmlDoc.DocumentNode.SelectSingleNode(lXPath2);
            var lTotalField = lTotalNode.InnerHtml.Trim();
            var lSplit = lTotalField.Split(new string[] { "</b>" }, StringSplitOptions.RemoveEmptyEntries);
            var lsBalance = lSplit[1];
            var lSystemBalance = DataHelper.ConvertFromCurrency(lsBalance);
            if (lSystemBalance != aPayment.Booking.Trip.Balance)
                Logger.LogWarning($"{FuncName} Balance do not match");
            else
            {
                EnterPayment(aPayment);
            }
            return false;
        }

        private bool EnterPayment(Payment aPayment)
        {
            // Click the Payment LInk
            // There seams to be an issue with advancing to fast
            int lWaitTime = new Random().Next(1500, 3500);
            System.Threading.Thread.Sleep(lWaitTime);

            ClickLinkById("paybtn");
            // Click "View Payments" Link
            ClickLink("View Payments");
            // Click id="btnApplyPayment" on "Click to Enter a New Payment"
            ClickLinkById("btnApplyPayment");
            // Cick the Credit Card Radio Button or id="paymenttypecc"
            ClickLinkById("paymenttypecc");
            WaitForElement(By.Id("btnContinueToPayment"));

            // Click the Contine button or id="btnContinueToPayment"
            ClickButton("btnContinueToPayment");
            WaitForElement(By.Id("txtcnumber1"));
            // Input Credit Card id="txtcnumber1"
            InputTextById("txtcnumber1", aPayment.Card.Number);
            // Input Security Code id="txtsecuritycode1"
            InputTextById("txtsecuritycode1", aPayment.Card.CVN);
            // Select Month = "selexpmonth1" example 01
            SelectTextById("selexpmonth1", FormatCCMonth(aPayment.Card.Expiration));
            // Select Year = selexpyear1 example 22
            SelectTextById("selexpyear1", FormatCCYear(aPayment.Card.Expiration));
            // Input Name id = txtcnameother1
            InputTextById("txtcnameother1", $"{aPayment.Payee.First} {aPayment.Payee.Last}");
            // Input address id = txtbilladdress11
            InputTextById("txtbilladdress11", $"{aPayment.Payee.Address1} {aPayment.Payee.Address2}");
            // Select State id=selbillstate1 example MD
            SelectTextById("selbillstate1", aPayment.Payee.State);
            // Input Dollar Amount id=txtdollaramt1
            InputTextById("txtdollaramt1", DataHelper.ConvertToCurrency(aPayment.Amount));
            // Input City id=txtbillcity1
            InputTextById("txtbillcity1", aPayment.Payee.City);
            // Input zip id=txtbillzip1
            InputTextById("txtbillzip1", aPayment.Payee.ZipCode);
            // INput Country.  US selected by Default
            // Click the Apply Button id=btnApply
            // Check for Error txt at XPath=//*[@id="Table1"]/tbody/tr[2]/td/table[2]/tbody/tr[5]/td/text()
            return false;
        }

        private string FormatCCYear(string aExpiration)
        {
            var lElements = aExpiration.Split('/');
            var lsYear = lElements[1].Trim();
            if (lsYear.Length == 4)
                return lsYear.Skip(2).ToString();

            return lsYear;
        }

        public override bool CreateBooking(Quote aQuote)
        {
            // Click New Booking

            // Go thru process of get quote
            // Select Depature Airport

            // Select the View Rooms & Quotes Check Box
            // 1) Find name of Link in td[1]
            // //*[@id="table3700"]/tbody/tr[1]/td[1]/a
            // html / body / div[3] / form / table[1] / tbody / tr / td / table / tbody / tr / td / table / tbody / tr[4] / td / table / tbody / tr[7] / td / table / tbody / tr[1] / td[1] / a
            //*[@id="table1900"]/tbody/tr[1]/td[1]/a
            // 2) Goto the link in td[2] click checkbox in the input field
            // Row one has he name
            // Row two has the Row header
            // //*[@id="roomrate4"]/table/tbody/tr[3]/td[4]/input[2]
            // 3) Click the Select button
            //*[@id="btnbooknow4"] Forth Row Down
            return false;
        }


        private string FormatCCMonth(string aExpiration)
        {
            var lElements = aExpiration.Split('/');
            var lsMonth = lElements[0].Trim();
            if (lsMonth.Length == 1)
                return "0" + lsMonth;

            return lsMonth;
        }

        public Staging.FlightHotelInformation PageParse(string aHTML, QuoteGroup aQuoteGroup, Quote aBookTrip = null)
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
                            // Only need to parse flight if we are not booking a trip
                            if (aBookTrip == null)
                            {
                                // This section is for the package items
                                if (aQuoteGroup.QuoteRequest.QuoteType != QuoteRequest.QuoteTypes.LandOnly)
                                    flightHotelInformation.Flights = ProcessFlights(htmlDoc, aQuoteGroup);

                                flightHotelInformation.Hotels = ProcessHotels(htmlDoc, aQuoteGroup);
                            }
                            else
                            {
                                BookHotel(htmlDoc, aQuoteGroup, aBookTrip);
                            }
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

        private void BookHotel(HtmlAgilityPack.HtmlDocument aHTML, QuoteGroup aQuoteGroup, Quote aBookTrip)
        {
            string FuncName = ClassName + $"BookHotel (SKU[{aBookTrip.AccommodationRoomTypeID}]) - ";

            var lResortRows = aHTML.DocumentNode.SelectNodes("//*[@id=\"mainresulttable\"]/tbody/tr");
            foreach (var lResortRow in lResortRows)
            {
                try
                {
                    Staging.Hotel lResort = new Staging.Hotel();
                    if (GetResort(lResortRow, ref lResort))
                        if (lResort.Name == aBookTrip.Accommodation.Name)
                            BookRoom(lResortRow, ref lResort, aQuoteGroup, aBookTrip);
                }
                catch (Exception e)
                {
                    Logger.LogException(FuncName + "Failed to add Resort", e);
                }
            }
        }

        private void BookRoom(HtmlAgilityPack.HtmlNode aResortNode, ref Staging.Hotel lResort, QuoteGroup aQuoteGroup, Quote aBookTrip)
        {
            var lRoomsRoot = aResortNode.SelectNodes("./td/table/tbody/tr[3]/td/table/tbody/tr");
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
                }
                catch (Exception e)
                {
                    Logger.LogException("Exception process Room Type", e);
                }
            }
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
            if (aRemoveTbody)
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

                    if (lFlightNode.Name == "tr" && lFlightNode.InnerHtml.Contains("Operated By"))
                    {
                        // Sometimes there can be additiona informaiton for a flight which will be seperated by a new line.  
                        // example - It can tell you that an airline is operated by another carrier
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
            }
            catch (Exception)
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
                    {
                        var lTarget = lResorts.FirstOrDefault(x => x.Name == lResort.Name);
                        if (lTarget != null)
                            GetRoom(lResortRow, ref lTarget, aQuoteGroup);
                        else
                            GetRoom(lResortRow, ref lResort, aQuoteGroup);
                    }

                    if (lResort.Name == null)
                        continue;

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
