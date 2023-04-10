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
using BlitzerCore.DataAccess;
using BlitzerCore.Business;
using BlitzerCore.Business.DBConverters;
using BlitzerCore.Utilities;

namespace BlitzerCore.WebBots
{
    public class VacationExpressFileBot : WebBotBase, IVacationExpressSrv
    {

        const string ClassName = "VacationExpressFileBot::";
        VacationExpressBot mWebBot = null;
        protected bool LivePull { get;  }

        public VacationExpressFileBot(IDbContext aContext, bool aLivePull = false) : base (null)
        {
            Logger.LogInfo($"{ClassName}VacationExpressFileBot CTor");
            mContext = aContext;
            LivePull = aLivePull;
            mWebBot = new VacationExpressBot(aContext);
            if (aContext != null)
                TourOperatorID = new TourOperatorDataAccess(aContext).Get(TourOperator.VACATION_EXPRESS).Id;
        }

        protected void init()
        {
            
        }
        public override ITourOperatorDBConverter GetDBConverter()
        {
            return new VacationExpressConverter();
        }

        public override bool Login(string aUserName, string aPassword)
        {
            Logger.LogInfo($"{ClassName}Login Login({aUserName},{aPassword})");
            return true;
        }
        public override double getPriceMultiplier(QuoteGroup aQuoteGroup)
        {
            return aQuoteGroup.QuoteRequest.NumberOfAdults; ;
        }
        protected override string GetFilePath()
        {
            string CWD = System.IO.Directory.GetCurrentDirectory();
            if (CWD.Contains("NUnit") == true)
                return @"..\..\..\..\Downloads\UNITTEST_VE_WebHTML.html";
            else if (LivePull == false)
            {
                var lProdFile = @"..\Downloads\VE_WebHTML.html";
                if (System.IO.File.Exists(lProdFile))
                    return lProdFile;

                return @"..\Downloads\UNITTEST_VE_WebHTML.html";
            }
            else
                return @"..\Downloads\VE_WebHTML.html";
        }

        public override Staging.FlightHotelInformation FindTrips(QuoteGroup aQuoteGroup, string aStartLocation, string aEndLocation, DateTime aDepartDate, DateTime aReturnDate, Quote aBookTrip = null)
        {
            string FuncName = $"{ClassName}FindTrips";
            Logger.LogInfo($"{FuncName} (QuoteGroup = {aQuoteGroup.Id},,,,) - ");

            //string lFilePath = @"C:\Users\redop\source\repos\Blitzer\WebApp\wwwroot\VE_BWI_to_CUN.html";
            //string lFilePath = @"..\..\..\..\WebApp\wwwroot\VE_BWI_to_CUN.html";
            //string lFilePath = @"..\..\..\..\WebApp\wwwroot\STWMain.html";
            //string lFilePath = @"C:\Users\redop\source\repos\Blitzer\WebApp\wwwroot\STWMain.HTML";
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.OptionFixNestedTags = true;
            string lHTML = System.IO.File.ReadAllText(GetFilePath());
            //string lHTML = File.ReadAllText(lFilePath);
            var lData = mWebBot.PageParse(lHTML, aQuoteGroup);
            Logger.LogDebug($"{FuncName} Loaded {lData.Flights.Count()} Flights and {lData.Hotels.Count()} Resorts");
            return FilterResorts(lData);
        }

        private Staging.FlightHotelInformation FilterResorts(Staging.FlightHotelInformation aData)
        {
            string FuncName = $"{ClassName}FilterResorts ";

            //if (FilterHotels == false)
            //    return aData;
            //else
            //    Logger.LogInfo($"{FuncName} - Filtering Hotels");

            //var lValentin = "Valentin Imperial, Riviera Maya - Adults Only";
            //var lUNICO = "UNICO 20º87º Hotel Riviera Maya -Adults Only";
            //var lZiva = "Hyatt Ziva Cancun";
            //var lXcaret = "Hotel Xcaret Mexico";
            //var lFairMont = "Fairmont Mayakoba";

            //var lHotelNames = new List<string>() { lValentin, lUNICO, lZiva, lXcaret, lFairMont };

            //var lOutput = aData.Hotels.Where(x => lHotelNames.Contains(x.Name));
            //if (lOutput != null)
            //    aData.Hotels = lOutput.ToList();
            //else
            //    aData.Hotels = new List<Staging.Hotel>();

            //Logger.LogDebug($"{FuncName} Filtered to {aData.Hotels.Count()} Hotels");

            return aData;
        }

        public override void ConvertFlightsFromStagingToProd(QuoteGroup aQuoteGroup)
        {
            mWebBot.ConvertFlightsFromStagingToProd(aQuoteGroup);
        }

        public override void ConvertResortsFromStagingToProd(QuoteGroup aQuoteGroup, QuoteRequestBusiness aQRBiz)
        {
            mWebBot.ConvertResortsFromStagingToProd(aQuoteGroup, aQRBiz);
        }

        public Staging.FlightHotelInformation PageParse(string aHTML, QuoteGroup aQuoteGroup)
        {
            return mWebBot.PageParse(aHTML, aQuoteGroup);

        }
        public string ParsePrice(string aInput)
        {
            return mWebBot.ParsePrice(aInput);
        }
    }
}