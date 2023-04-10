using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using BlitzerCore.Models;
using WebApp.Services;
using NUnitTests.Business;
using NUnitTests.Helpers;
using System.Linq;
using BlitzerCore.Utilities;
using BlitzerCore.Business;
using Microsoft.EntityFrameworkCore;
using BlitzerCore.DataAccess;
using BlitzerCore.WebBots;
using NUnitTests.Bots;
using BlitzerCore.Business.DBConverters;

namespace NUnitTests.ServiceStubs
{
    public class AAVacationsStub : WebBotBase
    {
        public AAVacationsStub(IDbContext aContext ) : base (null)
        {
            mContext = aContext;
            if (aContext != null )
                TourOperatorID = new TourOperatorDataAccess(aContext).Get(TourOperator.AA_VACATIONS).Id;
        }

        public override ITourOperatorDBConverter GetDBConverter()
        {
            return new AAConverter();
        }

        public override Staging.FlightHotelInformation FindTrips(BlitzerCore.Models.QuoteGroup aQuoteGroup, string aStartLocation, string aEndLocation, DateTime aDepartDate, DateTime aReturnDate, BlitzerCore.Models.Quote aBookTrip = null)
        {
            mData.Flights = new AAVacationTests().GetBulkFlights(mContext);
            mData.Hotels = new AAVacationTests().GetBulkHotels(mContext);
            mData.Hotels.ForEach(x => x.QuoteGroupId = aQuoteGroup.Id);
            mData.Flights.ForEach(x => x.QuoteGroupId = aQuoteGroup.Id);
            return mData;
        }

        public override void ConvertFlightsFromStagingToProd(BlitzerCore.Models.QuoteGroup aQuoteGroup)
        {
            int aInputRequestID = aQuoteGroup.Id;

            try
            {
                var lStaggedFlights = mContext.Staging_Flights.Where(x => x.QuoteGroupId == aInputRequestID).OrderBy(x => x.LegGUID).ToList();
                var lFlights = new AirBusiness(mContext, null).Convert(lStaggedFlights, aQuoteGroup);
                lFlights.ToList().ForEach(x => x.QuoteGroupId = aInputRequestID);
                var lCount = new QuoteDataAccess(mContext).Save(lFlights);
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to convert flights from Staging to Prod", e);
                throw;
            }

        }
        public override void ConvertResortsFromStagingToProd(BlitzerCore.Models.QuoteGroup aQuoteGroup, QuoteRequestBusiness aQRBiz)
        {

            // Update Resort Locations if necessary
            //UpdateAccommodationsFromStagging(aQuoteRequest.QuoteRequestID);

            try
            {

                var lHotels = mContext.Accommodations;
                var lRoomTypes = mContext.RoomTypes;

                // Get the stagged resorts
                var lStaggedResortQuotes = mContext.Staging_HotelRates
                    .Include(b => b.HotelStaging)
                    .Where(x => x.HotelStaging != null && x.HotelStaging.QuoteGroupId == aQuoteGroup.Id)
                    .ToList();
                int lStaggedCount = lStaggedResortQuotes.Count();

                var lHotelBiz = new HotelBusiness(mContext);
                lHotelBiz.CreateMissingAccommodations(aQuoteGroup, new StagingDataAccess(mContext).GetAccommodations(aQuoteGroup), this);
                lHotelBiz.CreateMissingRoomTypes(lStaggedResortQuotes, this, aQuoteGroup);
                aQRBiz.DeleteQuoteRequestResorts(aQuoteGroup, this.TourOperatorID);
                aQRBiz.CreateResortResults(aQuoteGroup, lHotels.ToList(), lRoomTypes.ToList(), lStaggedResortQuotes, this); ;
                var lCount = mContext.SaveChanges();
                Logger.LogInfo("Saved " + lCount + " QuoteRequestResort records");
            }
            catch (Exception e)
            {
                Logger.LogException("Exception convert Staging Resorts to Prod", e);
                throw;
            }

        }

    }
}
