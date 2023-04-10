using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using BlitzerCore.DataAccess;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Helpers;
using BlitzerCore.UIHelpers;
using BlitzerCore.Utilities;

namespace BlitzerCore.TourOps
{
    public class VacationExpress
    {
        const string ClassName = "VacationExpress::";
        private IDbContext DbContext { get; set; }

        public VacationExpress(IDbContext mContext)
        {
            this.DbContext = mContext;
        }

        void ParsePage(string aText)
        {

        }

        public string GetDepartureAirport ( string aAirPortCode )
        {
            string FuncName = ClassName + $"GetDepartureAirport(AirPortCode = {aAirPortCode}):";
            var lAirPort = new AirPortDataAccess(DbContext).Get(aAirPortCode);
            if ( lAirPort == null )
            {
                throw new Exception($"{FuncName}Invalid airport code passed to VacationExpress");
            }

            if (lAirPort.City == null || lAirPort.City == "")
            {
                Logger.LogError($"Unable to select Departure City because there is no city for {aAirPortCode}");
                return "";
            }

            string lOutput = $"{lAirPort.City}, {lAirPort.State}";
            if (lAirPort.State == null || lAirPort.State == "")
                return $"{lAirPort.City}";

            return lOutput;
        }

        public string GetDestinationAirport (string aAirPortCode )
        {
            string FuncName = ClassName + $"GetDestinationAirport(AirPortCode = {aAirPortCode}):";
            var lCompany = new CompanyDataAccess(DbContext).Get(TourOperator.VACATION_EXPRESS);
            if (lCompany == null)
            {
                throw new Exception($"{FuncName}Invalid tour operator VacationExpress");
            }
            var lVEDestination = new LookupManager(DbContext).Get(lCompany.Id, aAirPortCode);
            if ( lVEDestination == null )
            {
                Logger.LogError($"VacationExpress LookupManager can't find the lookup for ${aAirPortCode}");
                throw new Exception($"{FuncName}Unable to find destination lookup");
            }
            return lVEDestination;
        }
    }
}
