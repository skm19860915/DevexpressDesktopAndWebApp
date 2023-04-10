using System.IO;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BlitzerCore.Models;
using BlitzerCore.Utilities;
using BlitzerCore.DataAccess;

namespace BlitzerCore.Business
{
    public class BlitzerBusiness
    {
        private IDbContext DbContext { get; set; }

        private bool LivePull {get;set;}

        public BlitzerBusiness(IDbContext aContext, IConfiguration aConfiguration)
        {
            this.DbContext = aContext;
            string lsLivePull = "False";
            try
            {
                if ( aConfiguration != null )
                    lsLivePull = aConfiguration["AppSettings:LivePull"];
            }
            catch
            {}

            LivePull = lsLivePull.ToUpper() == "TRUE";

        }
        public IEnumerable<TourOperator> GetTourOperators(Contact aUser)
        {
            return new CompanyDataAccess(DbContext).GetTourOperators(aUser);
        }
        public IEnumerable<Company> GetSuppliers(Contact aUser)
        {
            return new CompanyDataAccess(DbContext).GetSuppliers(aUser);
        }

        public IWebTravelSrv GetWebService(TourOperator aTourOperator)
        {
            if (System.IO.Directory.GetCurrentDirectory().Contains("NUnit") || LivePull == false)
                return GetFileWebBot(aTourOperator);
            else
                return GetWebBot(aTourOperator);
        }

        private IWebTravelSrv GetWebBot ( TourOperator aOperator )
        {


            if (aOperator.Name == TourOperator.VACATION_EXPRESS)
                return new BlitzerCore.WebBots.VacationExpressBot(DbContext);
            else if (aOperator.Name == TourOperator.DELTA_VACATIONS)
                return new BlitzerCore.WebBots.WorldAgentDirectBot(DbContext);
            else if (aOperator.Name == TourOperator.JOURNESE)
                return new BlitzerCore.WebBots.JourneseBot(DbContext);
            else
                return null;
        }
        private IWebTravelSrv GetFileWebBot(TourOperator aOperator)
        {
            if (aOperator == null)
                return null;

            if (aOperator.Name == TourOperator.VACATION_EXPRESS)
                return new BlitzerCore.WebBots.VacationExpressFileBot(DbContext, LivePull);
            else if (aOperator.Name == TourOperator.DELTA_VACATIONS)
                return new BlitzerCore.WebBots.WADStaticFileBot(DbContext, LivePull);
            else
                return null;
        }
    }
}
