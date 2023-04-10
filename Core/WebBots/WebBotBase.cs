using System.Collections.Generic;
using BlitzerCore.Models;
using System;
using System.Linq;
using BlitzerCore.Business;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using BlitzerCore.Utilities;
using log4net.Repository;

namespace BlitzerCore.WebBots
{
    public class WebBotBase : IWebTravelSrv
    {
        const string ClassName = "WebBotBase::";

        protected IWebDriver mChrome = null;
        protected IDbContext mContext = null;
        protected const string IATA_Number = "34726042";

        protected Staging.FlightHotelInformation mData = new Staging.FlightHotelInformation();
        public int TourOperatorID { get; set; }

        public WebBotBase(IWebDriver aWebBase)
        {
            mChrome = aWebBase;
        }

        public virtual bool Login(string aLogin, string aPassWord)
        {
            return true;
        }
        protected virtual string GetFilePath()
        {
            return @"..\Downloads\WebHTML.html";
        }
        protected IWebElement WaitForElement(By aElementID)
        {
            IWebElement lLink = null;
            int lCnt = 0;
            while (lLink == null && lCnt < 10)
            {
                try
                {
                    lLink = mChrome.FindElement(aElementID);
                    System.Threading.Thread.Sleep(250);
                    lCnt++;
                    lLink = mChrome.FindElement(aElementID);
                }
                catch (Exception ) {}
            }

            if (lCnt >= 10)
                throw new Exception($"Failed to find HTML Element {aElementID}");

            return lLink;
        }

        public void InputTextByName ( String aInputField, string aValue)
        {
            var lFieldId = By.Name(aInputField);
            var lWebElement = mChrome.FindElement(lFieldId);
            lWebElement.Clear();
            lWebElement.SendKeys(aValue);
            Logger.LogInfo($"Successfully Set text to {aValue} Link");
        }

        public void InputTextById(String aInputId, string aValue)
        {
            var lFieldId = By.Id(aInputId);
            var lWebElement = mChrome.FindElement(lFieldId);
            lWebElement.Clear();
            lWebElement.SendKeys(aValue);
            Logger.LogInfo($"Successfully Set with id={aInputId} text to {aValue} Link");
        }

        public void ClickCheckBox(string aWebText)
        {
            var lLinkID = By.Name(aWebText);
            var lLink = WaitForElement(lLinkID);
            lLink.Click();
            Logger.LogInfo($"Successfully clicked the {aWebText} Link");
        }

        public void SelectTextByName ( string aSelectListName, string aValue)
        {
            try
            {
                var lWebElement = mChrome.FindElement(By.Name(aSelectListName));
                SelectElement lSelectElement = new SelectElement(lWebElement);
                lSelectElement.SelectByText(aValue);
                Logger.LogInfo($"Set Select value {aValue} in named field {aSelectListName}");
            }
            catch (Exception)
            {
                string lErrMsg = $"Unable to Set Select value {aValue} in named field {aSelectListName}";
                Logger.LogError(lErrMsg);
                throw new InvalidOperationException(lErrMsg);
            }

        }

        public void SelectTextById(string aSelectListName, string aValue)
        {
            try
            {
                var lWebElement = mChrome.FindElement(By.Id(aSelectListName));
                SelectElement lSelectElement = new SelectElement(lWebElement);
                lSelectElement.SelectByValue(aValue);
                Logger.LogInfo($"Set Select value {aValue} in named field {aSelectListName}");
            }
            catch (Exception)
            {
                string lErrMsg = $"Unable to Set Select value {aValue} in named field {aSelectListName}";
                Logger.LogError(lErrMsg);
                throw new InvalidOperationException(lErrMsg);
            }

        }
        public void SelectTextByClass(string aSelectListName, string aValue)
        {
            try
            {
                var lWebElement = mChrome.FindElement(By.ClassName(aSelectListName));
                SelectElement lSelectElement = new SelectElement(lWebElement);
                lSelectElement.SelectByText(aValue);
                Logger.LogInfo($"Set Select value {aValue} in named field {aSelectListName}");
            }
            catch (Exception)
            {
                string lErrMsg = $"Unable to Set Select value {aValue} in named field {aSelectListName}";
                Logger.LogError(lErrMsg);
                throw new InvalidOperationException(lErrMsg);
            }

        }

        public void SelectTextByXPath(string aSelectListName, string aValue)
        {
            try
            {
                var lWebElement = mChrome.FindElement(By.XPath(aSelectListName));
                SelectElement lSelectElement = new SelectElement(lWebElement);
                lSelectElement.SelectByText(aValue);
                Logger.LogInfo($"Set Select value {aValue} in named field {aSelectListName}");
            }
            catch (Exception)
            {
                string lErrMsg = $"Unable to Set Select value {aValue} in named field {aSelectListName}";
                Logger.LogError(lErrMsg);
                throw new InvalidOperationException(lErrMsg);
            }

        }
        public void ClickButton(string aWebId)
        {
            var lLinkID = By.Id(aWebId);
            var lLink = WaitForElement(lLinkID);
            lLink.Click();
            Logger.LogInfo($"Successfully clicked link with ID={aWebId}");
        }

        public void ClickLinkById(string aWebId)
        {
            var lLinkID = By.Id(aWebId);
            var lLink = WaitForElement(lLinkID);
            lLink.Click();
            Logger.LogInfo($"Successfully clicked link with ID={aWebId}");
        }

        public bool LinkTextExists (string aWebText )
        {
            var lLinkID = By.LinkText(aWebText);
            return mChrome.FindElement(lLinkID) != null;
        }

        public void ClickLink (string aWebText)
        {
            var lLinkID = By.LinkText(aWebText);
            var lLink = WaitForElement(lLinkID);
            lLink.Click();
            Logger.LogInfo($"Successfully clicked the {aWebText} Link");
        }

        public virtual bool CreateBooking(Quote aQuote)
        {
            // Must implement fo Unit Testing
            return false;
        }
        public virtual bool MakePayment(Payment aPayment)
        {
            // Must implement this for Unit Testing
            return false;
        }

        public void ClickButtonUsingXPath(string aPath)
        {
            var lLinkID = By.XPath(aPath);
            var lLink = WaitForElement(lLinkID);
            lLink.Click();
            Logger.LogInfo($"Successfully clicked the {aPath} Link");
        }

        public bool FilterHotels { get; set; } = true;
        public virtual string GetTourOperatorName()
        {
            throw new InvalidOperationException("This method should have been overridden");
        }

        public virtual ITourOperatorDBConverter GetDBConverter()
        {
            throw new NotImplementedException("Each Subclass must return a DBConverter");
        }

        public virtual void Close()
        {
            if (mChrome == null)
                return;
            try
            {
                mChrome.Close();
                mChrome.Quit();
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to close WebBotBase", e);
            }
        }

        public virtual void LoadBulk()
        {
        }

        public virtual double getPriceMultiplier(QuoteGroup aQuoteRequest)
        {
            return 1;
        }
        public virtual double getChildPriceMultiplier(QuoteGroup aQuoteRequest)
        {
            return 0;
        }
        protected virtual void SetStarRatings()
        {
            mData.Hotels.Take(10).ToList().ForEach(x => x.Stars = "5");
            mData.Hotels.Skip(10).Take(30).ToList().ForEach(x => x.Stars = "4.5");
            mData.Hotels.Skip(40).Take(50).ToList().ForEach(x => x.Stars = "4.0");
            mData.Hotels.Skip(90).ToList().ForEach(x => x.Stars = "3.5");
        }

        public virtual void SetData(List<Staging.Hotel> aData)
        {
            mData.Hotels = aData;
            SetStarRatings();
        }

        public virtual void SetData(List<Staging.Flight> aData)
        {
            mData.Flights = aData;
        }

        public virtual void LoadDefaultData(QuoteGroup aQuoteGroup, string aDepartCode, string aDestCode)
        {
        }
        public virtual List<Staging.Flight> ProcessStagingFlights(List<Staging.Flight> aData)
        {
            return aData;
        }
        public virtual List<Staging.Hotel> ProcessStagingHotels(List<Staging.Hotel> aData)
        {
            return aData.Where(x => string.IsNullOrEmpty(x.Name) == false).ToList();
        }
        public virtual List<Staging.HotelRate> ProcessStagingHotelRates(List<Staging.HotelRate> aData)
        {
            return aData;
        }

        public virtual void ConvertFlightsFromStagingToProd(BlitzerCore.Models.QuoteGroup aQuoteGroup)
        {

        }
        public virtual void ConvertResortsFromStagingToProd(BlitzerCore.Models.QuoteGroup aQuoteGroup, QuoteRequestBusiness aQRBiz)
        {

        }

        public virtual Staging.FlightHotelInformation FindTrips(BlitzerCore.Models.QuoteGroup aQuoteGroup, string aStartLocation, string aEndLocation, DateTime aDepartDate, DateTime aReturnDate, Quote aBookTrip = null)
        {

            return new Staging.FlightHotelInformation();
        }

    }
}
