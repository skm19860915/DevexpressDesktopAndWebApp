using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using BlitzerCore.Models;
using BlitzerCore.Business;

namespace BlitzerCore.Models
{
    public interface IWebTravelSrv
    {
        int TourOperatorID { get; }

        bool Login(string aUserName, string aPassword);
        Staging.FlightHotelInformation FindTrips(QuoteGroup aQuoteGroup, string aStartLocation, string aEndLocation, DateTime aDepartDate, DateTime aReturnDate, Quote aBookTrip = null);

        List<Staging.Flight> ProcessStagingFlights(List<Staging.Flight> aData);
        List<Staging.Hotel> ProcessStagingHotels(List<Staging.Hotel> aData);
        void ConvertFlightsFromStagingToProd(QuoteGroup aQuoteGroup);
        void ConvertResortsFromStagingToProd(QuoteGroup aQuoteGroup, QuoteRequestBusiness aQRBiz);
        ITourOperatorDBConverter GetDBConverter();
        void Close();
        double getPriceMultiplier(QuoteGroup aQuoteGroup);
        double getChildPriceMultiplier(QuoteGroup aQuoteGroup);
        bool MakePayment(Payment aPayment);
        bool CreateBooking(Quote aQuote);
    }
}
