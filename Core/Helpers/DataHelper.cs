using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using BlitzerCore.DataAccess;
using BlitzerCore.Helpers;
using BlitzerCore.Utilities;

namespace BlitzerCore.Helpers
{
    public enum MimeTypes { MP4, Picture, PNG, JPG }
    public class DataHelper
    {
        const string ClassName = "DataHelper::";
        public enum ErrorCodes { LoginFailure, DeltaVacationsNotSetup, AAVacationsNotSetup }
        public static string GetDateString(DateTime aDT)
        {
            if (aDT.Year > 1900)
                return aDT.ToString("M/dd/yy");

            return "";
        }

        public static string MimeType(string aFileName)
        {
            string lSuffix = aFileName.Split('.')[aFileName.Count(x => x == '.')].ToUpper();
            switch (lSuffix)
            {
                case "MP4":
                    return "video/mp4";
                case "PNG":
                    return "image/png";
                case "JPEG":
                case "JPG":
                    return "image/jpeg";
                default:
                    return "";
            }
        }

        public static string GetDateString(DateTime? aDT)
        {
            if (aDT == null)
                return "";

            return GetDateString(aDT.Value);
        }
        public static string GetDateTimeString(DateTime aDT)
        {
            return aDT.ToShortDateString() + " " + aDT.ToShortTimeString();
        }

        public static string GetDateTimeString(DateTime? aDT)
        {
            if (aDT == null)
                return null;

            return GetDateTimeString(aDT.Value);
        }

        public static string GetShortDateString(DateTime start)
        {
            return start.ToString("MMM dd");
        }

        public static string GetLongDateString(DateTime start)
        {
            return start.ToString("MMM dd, yyyy");
        }

        public static string FormatPhoneNumber(string aNumber)
        {
            if (aNumber == null)
                return "";
            
            if ( aNumber.Length < 10)
                return aNumber;

            long lConverted = 0;
            if (Int64.TryParse(aNumber, out lConverted))
                return String.Format("{0: (###) ###-####}", lConverted);
            else
                return aNumber;
        }
        public static DateTime GetDateTime(string aDate)
        {
            return DateTime.Parse(aDate);
        }

        //public static List<Relationship> GetRelationShips(IDbContext aDbContext)
        //{
        //    return aDbContext.Relationships.ToList();
        //}


        public static string Age(DateTime aCreatedOn)
        {
            int lAge = DateTime.Now.Subtract(aCreatedOn).Days;
            if (lAge == 0)
            {
                lAge = Convert.ToInt32(DateTime.Now.Subtract(aCreatedOn).TotalHours);
                if (lAge == 0)
                {
                    lAge = Convert.ToInt32(DateTime.Now.Subtract(aCreatedOn).TotalMinutes);
                    return lAge.ToString() + " mins";
                }
                else
                    return lAge.ToString() + " Hours";
            }
            else
            {
                if (lAge > 10000)
                    return "Invalid";

                return lAge.ToString() + " Days";
            }
        }

        public static string GetTimeString(DateTime aInput)
        {
            return aInput.ToString("h:mm tt");
        }
        public static string GetTimeString(DateTime? aInput)
        {
            if (aInput == null || aInput.HasValue == false)
                return "";

            return GetTimeString(aInput.Value);
        }


        internal static string ConvertWebString(string aName)
        {
            string lOutput = aName.Trim();
            lOutput = aName.Replace("&amp;", "&");
            lOutput = lOutput.Replace("\"", "");
            return lOutput;
        }

        public static double ConvertFromCurrency(string aValue)
        {
            double lOutput = 0;
            int lMultiplier = 1;
            if (aValue == null || aValue == "")
                return 0;

            if (aValue.Contains('('))
            {
                lMultiplier = -1;
                aValue = aValue.Replace("(", string.Empty).Replace(")", string.Empty);
            }

            aValue = aValue.Replace("$", "");
            if (Double.TryParse(aValue, out lOutput))
                return lMultiplier * lOutput;
            else
                return -1;

        }

        public static string ConvertToCurrency(double aPrice)
        {
            return String.Format("{0:C}", aPrice);
        }

        public static DateTime? GetDate(string aValue)
        {
            DateTime lOutput = DateTime.Now;
            if (DateTime.TryParse(aValue, out lOutput))
                return lOutput;
            else
                return null;
        }

        internal static int GetInt(string aValue)
        {
            string FuncName = ClassName + "GetInt";
            int lOutput = 0;
            if (int.TryParse(aValue, out lOutput))
                return lOutput;
            else
            {
                Logger.LogWarning(FuncName = $" - Failed to parse [{aValue}]");
                return 0;
            }
        }

        internal static string CleanEmail(string aEmail)
        {
            if (aEmail == null)
                return "";
            string lOutput = aEmail.Replace("\'", string.Empty);
            lOutput = lOutput.Replace("(", string.Empty);
            lOutput = lOutput.Replace(")", string.Empty);
            lOutput = lOutput.Replace("-", string.Empty);
            lOutput = lOutput.Replace("\"", string.Empty);
            lOutput = lOutput.Replace("\t", string.Empty);
            lOutput = lOutput.Replace(" ", string.Empty);
            return lOutput;
        }

        public static string CleanPhoneNumber(string aPhoneNumber)
        {
            if (aPhoneNumber == null)
                return "";
            string lOutput = aPhoneNumber.Replace("\'", string.Empty);
            lOutput = lOutput.Replace("(", string.Empty);
            lOutput = lOutput.Replace(")", string.Empty);
            lOutput = lOutput.Replace("-", string.Empty);
            lOutput = lOutput.Replace("\"", string.Empty);
            lOutput = lOutput.Replace("\t", string.Empty);
            lOutput = lOutput.Replace(" ", string.Empty);
            return lOutput;
        }
    }
}
