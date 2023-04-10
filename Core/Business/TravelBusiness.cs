using System;
using System.Collections.Generic;
using System.Linq;
using BlitzerCore.Helpers;
using BlitzerCore.Models;
using BlitzerCore.DataAccess;
using BlitzerCore.Utilities;
using System.Collections.Specialized;

namespace BlitzerCore.Business
{
    public class TravelBusiness
    {
        readonly IDbContext mContext;
        const string ClassName = "OpportunityController::";

        public TravelBusiness(IDbContext aContext)
        {
            mContext = aContext;
        }

        /// <summary>
        /// This will return the airport from a full name description with the Airport
        /// code in parens or just the airport code
        /// </summary>
        /// <param name="aAirPortCode"></param>
        /// <returns></returns>
        public AirPort GetAirPort(string aAirPortCode)
        {
            string FuncName = $"{ClassName}GetAirPort (aAirPortCode = {aAirPortCode})";

            try
            {
                if ( aAirPortCode == null )
                    return new AirBusiness(mContext).GetDefaultAirPort();

                string lTarget = String.Concat(aAirPortCode.Where(c => !Char.IsWhiteSpace(c)));
                string lAirPortCode = lTarget;
                if (lAirPortCode.Length > 3)
                {
                    int lStart = lTarget.IndexOf('(');
                    if (lStart < 0)
                        return new AirBusiness(mContext).GetDefaultAirPort();
                    lStart += 1;
                    int lLen = 3;
                    lAirPortCode = lTarget.Substring(lStart, lLen);
                }

                var lResult = new AirPortDataAccess(mContext).Get(lAirPortCode);
                if (lResult == null)
                    return new AirBusiness(mContext).GetDefaultAirPort();
                else
                    return lResult;
            }
            catch ( Exception e )
            {
                Logger.LogException($"{FuncName} Failed to convert AirportCode ", e);
            }

            return new AirBusiness(mContext).GetDefaultAirPort();

        }

        public List<AirPort> GetAirPorts()
        {
            return new AirPortDataAccess(mContext).GetAll().ToList();
        }

    }
}
