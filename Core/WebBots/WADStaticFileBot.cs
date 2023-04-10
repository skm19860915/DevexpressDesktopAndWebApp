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
    public class WADStaticFileBot : WebBotBase, IDeltaVacationSrv
    {

        const string ClassName = "WADStaticFileBot::";
        WorldAgentDirectBot lWADBot = null;
        protected bool LivePull { get; }

        public WADStaticFileBot(IDbContext aContext, bool aLivePull = false) : base(null)
        {
            Logger.LogInfo($"{ClassName}WADStaticFileBot CTor");
            mContext = aContext;
            LivePull = aLivePull;
            lWADBot = new WorldAgentDirectBot(aContext);
            if (aContext != null)
                TourOperatorID = new TourOperatorDataAccess(aContext).Get(TourOperator.DELTA_VACATIONS).Id;
        }

        protected void init()
        {

        }
        public override ITourOperatorDBConverter GetDBConverter()
        {
            return new WorldAgentDirectConverter();
        }

        public override bool Login(string aUserName, string aPassword)
        {
            Logger.LogInfo($"{ClassName}Login Login({aUserName},{aPassword})");
            return true;
        }
        protected override string GetFilePath()
        {
            string CWD = System.IO.Directory.GetCurrentDirectory();
            if (CWD.Contains("NUnit") == true)
                return @"..\..\..\..\Downloads\UNITTEST_WAD_WebHTML.html";
            else if (LivePull == false)
            {
                var lProdFile = @"..\Downloads\WAD_WebHTML.html";
                if (System.IO.File.Exists(lProdFile))
                    return lProdFile;

                return @"..\Downloads\UNITTEST_WAD_WebHTML.html";
            }
            else
                return @"..\Downloads\WAD_WebHTML.html";
        }

        public override Staging.FlightHotelInformation FindTrips(QuoteGroup aQuoteGroup, string aStartLocation, string aEndLocation, DateTime aDepartDate, DateTime aReturnDate, Quote aBookTrip = null)
        {
            string FuncName = $"{ClassName}FindTrips";
            Logger.LogInfo($"{FuncName} (aQuoteRequest = {aQuoteGroup.Id},,,,) - ");
            //string lFilePath = @"wwwroot\WAD_RDU_to_CUN.html";
            string lFilePath = GetFilePath();
            string absolute = Path.GetFullPath(lFilePath);
            Logger.LogInfo($"WAD File Bot path [{absolute}]");
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.OptionFixNestedTags = true;
            string lHTML = System.IO.File.ReadAllText(lFilePath);
            var lData = lWADBot.PageParse(lHTML, aQuoteGroup);
            Logger.LogDebug($"{FuncName} Loaded {lData.Flights.Count()} Flights and {lData.Hotels.Count()} Resorts");
            return FilterResorts(lData);
        }

        public override double getPriceMultiplier(QuoteGroup aQuoteGroup)
        {
            return 1;
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
            lWADBot.ConvertFlightsFromStagingToProd(aQuoteGroup);
        }

        public override void ConvertResortsFromStagingToProd(QuoteGroup aQuoteGroup, QuoteRequestBusiness aQRBiz)
        {
            lWADBot.ConvertResortsFromStagingToProd(aQuoteGroup, aQRBiz);
        }

        public Staging.FlightHotelInformation PageParse(string aHTML, QuoteGroup aQuoteGroup)
        {
            return lWADBot.PageParse(aHTML, aQuoteGroup);

        }
        public string ParsePrice(string aInput)
        {
            return lWADBot.ParsePrice(aInput);
        }
    }
}