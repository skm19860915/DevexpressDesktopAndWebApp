using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models;
using BlitzerCore.Utilities;

namespace BlitzerCore.DataAccess
{
    public class StagingDataAccess
    {
        const string ClassName = "StagingDataAccess::";
        IDbContext mContext;
        public StagingDataAccess(IDbContext aContext)
        {
            mContext = aContext;
        }

        public int Save(List<Staging.Flight> aFlights)
        {
            string FuncName = ClassName + "Save (List<Staging.Flight>) - ";

            List<int> IDs = mContext.Staging_Flights.Select(x => x.FlightStagingID).ToList();
            foreach (Staging.Flight lFlightStaging in aFlights)
            {
                if (IDs.Contains( lFlightStaging.FlightStagingID) == false )
                    mContext.Staging_Flights.Add(lFlightStaging);
                else
                    mContext.Staging_Flights.Update(lFlightStaging);
            }
            try
            {
                var lCnt = mContext.SaveChanges();
                Logger.LogInfo(FuncName + $"Updated {lCnt} Staging.Flights ");
                return lCnt;
            } catch ( Exception e)
            {
                Logger.LogException(FuncName + " Failed to update Staging.Flights", e);
            }

            return 0;
        }

        public int Save(List<Staging.Hotel> aHotels)
        {
            foreach (Staging.Hotel lHotelStaging in aHotels)
                Save(lHotelStaging, false);

            return mContext.SaveChanges();
        }

        public int Save(Staging.Hotel aHotel, bool aCommit = true )
        {
            if (aHotel.HotelStagingID == 0)
                mContext.Staging_Hotels.Add(aHotel);
            else
                mContext.Staging_Hotels.Update(aHotel);

            if (aCommit == true)
                return mContext.SaveChanges();
            else
                return 0;
        }

        public List<Staging.Hotel> GetAccommodations(QuoteGroup aQuoteGroup)
        {
            return mContext.Staging_Hotels.Where(x => x.QuoteGroupId == aQuoteGroup.Id).ToList();
        }
    }
}
