using System;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models;
using System.Linq;
using BlitzerCore.Utilities;

namespace BlitzerCore.Business.ScreenParsers
{
    public static class VacationExpress
    {

        public static FlightItinerary GetFlights ( string aText )
        {
            var lOutput = new FlightItinerary();
            if (aText == null || aText.Length == 0)
                return lOutput;

            var lOutBound = GetOutBoundFlights(aText);
            var lInBound = GetInBoundFlights(aText);

            lOutput.OutBound = new Leg() { Flights = lOutBound };
            lOutput.InBound = new Leg() { Flights = lInBound };
            return lOutput;
        }

        private static List<Flight> GetOutBoundFlights(string aText)
        {
            var lFlightStrs = GetFlightStrs(aText);
            var lOFlightStrs = GetOutBoundFlightStrs(lFlightStrs);
            return ConvertFlights(lOFlightStrs);
        }

        private static List<Flight> ConvertFlights(List<string> aFlights)
        {
            var lOutput = new List<Flight>();

            foreach (string lStr in aFlights)
                lOutput.Add(ConvertFlight(lStr));

            return lOutput;
        }

        public static List<Quote> GetRooms(string aScreenText, IDbContext aContext)
        {
            var lOutput = new List<Quote>();
            string lRoomData = StripFlighInfo(aScreenText);
            string lResortName = "";
            lRoomData = StripHotelIntro(lRoomData, ref lResortName);
            lOutput = GetQuotes(lRoomData, aContext);
            return lOutput;
        }

        private static List<Quote> GetQuotes(string lRoomData, IDbContext aContext)
        {
            var lOutput = new List<Quote>();
            Quote lQuote = null;

            do
            {
                lQuote = GetQuote(ref lRoomData, aContext);
                if (lQuote != null)
                    lOutput.Add(lQuote);
            } while (lQuote != null);

            return lOutput;
        }

        private static Quote GetQuote(ref string aData, IDbContext aContext)
        {
            if (aData == "")
                return null;

            var lOutput = new Quote();
            var lResortName = GetLine(ref aData).Split(new[] { " - " }, StringSplitOptions.None)[0];
            var lBlank = GetLine(ref aData);
            lBlank = GetLine(ref aData);
            var lPrice = GetLine(ref aData);
            lBlank = GetLine(ref aData);

            if (aData.Contains("Available") == false)
                aData = "";

            return lOutput;
        }

        private static string GetLine ( ref string aInput )
        {
            var lIndex = aInput.IndexOf(Environment.NewLine) + Environment.NewLine.Length;
            if (aInput == "" || lIndex == 1)
                return null;

            var lOutput = aInput.Substring(0, lIndex-2);
            aInput = aInput.Substring(lIndex );
            return lOutput;
        }

        private static string StripHotelIntro(string lRoomData, ref string aResortName)
        {
            var lOutput = StripFirstLine(lRoomData, 16);
            return lOutput;
        }

        private static string StripFirstLine(string aData, int aLines = 1)
        {
            for ( int i = 0; i < aLines; i++)
                aData = aData.Substring (aData.IndexOf(Environment.NewLine) + Environment.NewLine.Length);

            return aData;
        }

        private static string StripFlighInfo(string aScreenText)
        {
            var lRemainder = aScreenText.Split(new [] { "Display Type" }, StringSplitOptions.RemoveEmptyEntries)[1];
            lRemainder = lRemainder.Substring(lRemainder.IndexOf(Environment.NewLine) + 2);
            lRemainder = lRemainder.Substring(lRemainder.IndexOf(Environment.NewLine) + 5);
            return lRemainder;

        }

        private static Flight ConvertFlight(string lStr)
        {
            var lOutput = new Flight();
            var lFields = lStr.Split('\t');
            string lDepart = lFields[2].Trim();
            lDepart = lDepart.Substring(0, lDepart.Length - 2);
            string lArrive = lFields[3].Trim();
            lOutput.Depart = ParseTime(lDepart);
            lOutput.Arrive = ParseTime(lArrive);
            lOutput.Identifer = lFields[4].Split('#')[1];
            lOutput.Carrier = lFields[4].Split('#')[0];
            //Utilities.
            return lOutput;
        }

        private static DateTime ParseTime(string aTimeStr)
        {
            return DateTime.ParseExact(aTimeStr, "hh:mmtt", System.Globalization.CultureInfo.InvariantCulture);
        }

        private static List<string> GetOutBoundFlightStrs(List<string> aFlights)
        {
            var lDepartDate = aFlights[0].Split('\t')[1];
            return aFlights.Where(x => x.Contains(lDepartDate)).ToList();
        }

        private static List<string> GetInBoundFlightStrs(List<string> aFlights)
        {
            var lDepartDate = aFlights[0].Split('\t')[1];
            return aFlights.Where(x => x.Contains(lDepartDate) == false).ToList();
        }

        private static List<string> GetFlightStrs(string aText)
        {
            var lOutput = new List<string>();
            bool lFlag = false;

            var lData = aText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            foreach ( var lLine in lData)
            {
                if (lLine.Trim().Length == 0)
                    return lOutput;

                if (lFlag)
                    lOutput.Add(lLine);

                if (lLine.Contains("Departure"))
                    lFlag = true;
            }

            return new List<string>();
        }

        private static List<Flight> GetInBoundFlights(string aText)
        {
            var lFlightStrs = GetFlightStrs(aText);
            var lIFlightStrs = GetInBoundFlightStrs(lFlightStrs);
            return ConvertFlights(lIFlightStrs);
        }

    }
}
