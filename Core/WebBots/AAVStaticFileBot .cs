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
    public class AAVStaticFileBot : WebBotBase, IAAVacationSrv
    {

        const string ClassName = "AAVStaticFileBot::";
        AAVacationBot lAAVBot = null;

        public AAVStaticFileBot(IDbContext aContext) : base (null)
        {
            Logger.LogInfo($"{ClassName}AAVStaticFileBot CTor");
            mContext = aContext;
            lAAVBot = new AAVacationBot(aContext);
            if (aContext != null)
                TourOperatorID = new TourOperatorDataAccess(aContext).Get(TourOperator.AA_VACATIONS).Id;
        }

        protected void init()
        {
            
        }
        public override ITourOperatorDBConverter GetDBConverter()
        {
            return new AAConverter();
        }

        public override bool Login(string aUserName, string aPassword)
        {
            Logger.LogInfo($"{ClassName}Login Login({aUserName},{aPassword})");
            return true;
        }

        public override double getPriceMultiplier(QuoteGroup aQuoteGroup)
        {
            return 1;
        }
        public override double getChildPriceMultiplier(QuoteGroup aQuoteGroup)
        {
            return 0;
        }
        public override Staging.FlightHotelInformation FindTrips(QuoteGroup aQuoteGroup, string aStartLocation, string aEndLocation, DateTime aDepartDate, DateTime aReturnDate, Quote aBookTrip = null)
        {
            string FuncName = $"{ClassName}FindTrips";
            Logger.LogInfo($"{FuncName} (aQuoteGroup = {aQuoteGroup.Id},,,,) - ");
            string lFilePath = @"wwwroot\AAV_RDU_to_CUN.html";
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.OptionFixNestedTags = true;
            string lHTML = System.IO.File.ReadAllText(lFilePath);
            var lData = lAAVBot.PageParse(lHTML, aQuoteGroup);
            Logger.LogDebug($"{FuncName} Loaded {lData.Flights.Count()} Flights and {lData.Hotels.Count()} Resorts");
            return FilterResorts(lData);
        }

        private Staging.FlightHotelInformation FilterResorts(Staging.FlightHotelInformation aData)
        {
            string FuncName = $"{ClassName}FilterResorts ";

            return aData;
        }

        public override void ConvertFlightsFromStagingToProd(QuoteGroup aQuoteGroup)
        {
            lAAVBot.ConvertFlightsFromStagingToProd(aQuoteGroup);
        }

        public override void ConvertResortsFromStagingToProd(QuoteGroup aQuoteGroup, QuoteRequestBusiness aQRBiz)
        {
            lAAVBot.ConvertResortsFromStagingToProd(aQuoteGroup, aQRBiz);
        }

        public Staging.FlightHotelInformation PageParse(string aHTML, QuoteGroup aQuoteGroup)
        {
            return lAAVBot.PageParse(aHTML, aQuoteGroup);

        }
        public string ParsePrice(string aInput)
        {
            return lAAVBot.ParsePrice(aInput);
        }
    }
}