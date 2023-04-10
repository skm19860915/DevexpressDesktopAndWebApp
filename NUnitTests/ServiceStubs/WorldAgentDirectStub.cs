using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using BlitzerCore.Models;
using WebApp.Services;
using NUnitTests.Business;
using NUnitTests.Helpers;
//using java.sql;
using System.Linq;
using BlitzerCore.Utilities;
using BlitzerCore.Business;
using Microsoft.EntityFrameworkCore;
using BlitzerCore.DataAccess;
using BlitzerCore.WebBots;
using BlitzerCore.Business.DBConverters;

namespace NUnitTests.ServiceStubs

{
    public class WorldAgentDirectStub : WebBotBase
    {
        const string ClassName = "WorldAgentDirectStub::";
        public WorldAgentDirectStub(IDbContext aContext) : base (null)
        {
            mContext = aContext;
            if ( aContext != null )
                TourOperatorID = new TourOperatorDataAccess(aContext).Get(TourOperator.DELTA_VACATIONS).Id;
        }


        public override string GetTourOperatorName()
        {
            return TourOperator.DELTA_VACATIONS;
        }
        public override ITourOperatorDBConverter GetDBConverter()
        {
            return new WorldAgentDirectConverter();
        }

        public override void LoadBulk()
        {
            string FuncName = ClassName + "LoadBulk";
            var lRates = StagingHotelRateLoader.Load();
            var lHotels = StagingHotelLoader.Load().OrderBy(x => x.HotelStagingID).ToList();
            lHotels.ForEach(x => x.HotelRateTypes = lRates.Where(y => y.HotelStagingID == x.HotelStagingID).ToList());
            lHotels.ForEach(x => x.HotelStagingID = 0);
            Logger.LogInfo(FuncName + $" - Loaded {lHotels.Count} staging hotels");
            SetData(lHotels);
            SetData(StagingFlightLoader.Load());
        }

        public override void LoadDefaultData(BlitzerCore.Models.QuoteGroup aQuoteGroup, string aDepartCode, string aDestCode)
        {

            mData.Flights = new Air().CreateNonStopRountTrip(aQuoteGroup, aDepartCode, aDestCode);
            mData.Flights.AddRange(new Air().Create2ndNonStopRoundTrip(aQuoteGroup, aDepartCode, aDestCode));
            mData.Hotels = DataLake.GetStagingHotels(aQuoteGroup);
        }

        public override Staging.FlightHotelInformation FindTrips(BlitzerCore.Models.QuoteGroup aQuoteGroup, string aStartLocation, string aEndLocation, DateTime aDepartDate, DateTime aReturnDate, BlitzerCore.Models.Quote aBookTrip = null)
        {
            string FuncName = ClassName + "FindTrips - ";
            var lResortDA = new AccommodationDataAccess(mContext);
            if ((mData.Hotels == null || mData.Hotels.Count == 0) && (mData.Flights == null || mData.Flights.Count < 2))
            {
                LoadDefaultData(aQuoteGroup, aStartLocation, aEndLocation);
            }
            mData.Hotels.ForEach(x => x.QuoteGroupId = aQuoteGroup.Id);
            mData.Flights.ForEach(x => x.QuoteGroupId = aQuoteGroup.Id);

            Logger.LogInfo(FuncName + $" There are {lResortDA.GetAdultsOnly() } Adults Only Resorts");
            Logger.LogInfo(FuncName + $" There are {lResortDA.GetAllInclusive()} All Inclusive Resorts");

            return mData;
        }
        public override List<Staging.Flight> ProcessStagingFlights(List<Staging.Flight> aData)
        {
            //return StagingFlightLoader.Load();
            return aData;
        }

        public override void ConvertFlightsFromStagingToProd(BlitzerCore.Models.QuoteGroup aQuoteGroup)
        {
            int aInputQGID = aQuoteGroup.Id;

            try
            {
                var lStaggedFlights = mContext.Staging_Flights.Where(x => x.QuoteGroupId == aInputQGID).OrderBy(x => x.LegGUID).ToList();
                var lFlights = new AirBusiness(mContext, null).Convert(lStaggedFlights, aQuoteGroup);
                lFlights.ToList().ForEach(x => x.QuoteGroupId = aInputQGID);
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
                throw;
            }

        }
        public override void ConvertResortsFromStagingToProd(BlitzerCore.Models.QuoteGroup aQuoteGroup, QuoteRequestBusiness aQRBiz)
        {
            var lHotelBiz = new HotelBusiness(mContext);
            // Update Resort Locations if necessary
            //UpdateAccommodationsFromStagging(aQuoteRequest.QuoteRequestID);
            string FuncName = ClassName + "ConvertResortsFromStagingToProd - ";
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

                lHotelBiz.CreateMissingAccommodations(aQuoteGroup, new StagingDataAccess(mContext).GetAccommodations(aQuoteGroup), this);
                lHotelBiz.CreateMissingRoomTypes(lStaggedResortQuotes, this, aQuoteGroup);
                aQRBiz.DeleteQuoteRequestResorts(aQuoteGroup, this.TourOperatorID);
                aQRBiz.CreateResortResults(aQuoteGroup, lHotels.ToList(), lRoomTypes.ToList(), lStaggedResortQuotes, this); ;
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName+"Exception thrown", e);
                throw;
            }
        }
    }
}
