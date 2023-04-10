using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.DataAccess;
using BlitzerCore.Helpers;
using BlitzerCore.Business;
using BlitzerCore.Models.ASP;

namespace BlitzerCore.UIHelpers
{
    public class CompanyUIHelper
    {
        public static List<UICompany> Convert(IDbContext aContext, IEnumerable<Company> aCompanies)
        {
            List<UICompany> lOutput = new List<UICompany>();
            foreach (var lComp in aCompanies)
            {
                TourOperator lTO = lComp as TourOperator;
                if (lTO != null)
                    lOutput.Add(Convert(aContext, lTO));
                else
                    lOutput.Add(Convert(aContext, lComp));
            }
            return lOutput;
        }
        public static string GetPrimaryPhone(Company aSrc)
        {
            return DataHelper.FormatPhoneNumber(GetCell(aSrc));
        }
        protected static void Copy(IDbContext aContext, UICompany aOutput, Company aInput)
        {

            aOutput.Name = aInput.Name;
            aOutput.Address1 = aInput.Street;
            aOutput.Address2 = aInput.Street2;
            aOutput.City = aInput.City;
            aOutput.Id = aInput.Id;
            if ( aInput.Rating != null )
                aOutput.Rating = aInput.Rating.Value;
            aOutput.PageId = aInput.PageId;
            aOutput.BusinessTypeID = aInput.BusinessTypeID;
            aOutput.Visibility = aInput.Visiblity;
            aOutput.State = aInput.State;
            aOutput.ZipCode = aInput.ZipCode;
            aOutput.PrimaryEmail = GetEmail(aInput);
            aOutput.PrimaryPhone = GetPrimaryPhone(aInput);
            //aBase.QuoteData = new QuoteBusiness(aContext).GetQuoteSummary(aBase.Id);
            aOutput.WebSite = aInput.Website;
            aOutput.Memo = aInput.Memo;
            if (aInput.BusinessType != null)
                aOutput.BusinessType = aInput.BusinessType.Type;
            aOutput.Contacts = ContactUIHelper.Convert(aInput.Contacts);
        }

        protected static void Copy(IDbContext aContext, Company aOutput, UICompany aInput)
        {

            aOutput.Name = aInput.Name;
            aOutput.Street = aInput.Address1;
            aOutput.Street2 = aInput.Address2;
            aOutput.Rating = aInput.Rating;
            aOutput.City = aInput.City;
            aOutput.Id = aInput.Id;
            aOutput.PageId = aInput.PageId;
            aOutput.BusinessTypeID = aInput.BusinessTypeID;
            aOutput.Visiblity = aInput.Visibility;
            aOutput.State = aInput.State;
            aOutput.ZipCode = aInput.ZipCode;
            StoreEmailAddr(aInput, aOutput);
            StorePhoneNumber( aInput, aOutput);
            aOutput.Website = aInput.WebSite;
            aOutput.Memo = aInput.Memo;
            aOutput.BusinessTypeID = aInput.BusinessTypeID;
            //aOutput.Contacts = ContactUIHelper.Convert(aInput.Contacts);
        }

        public static  UICompanyQuoteSummary GetQuoteSummary(IDbContext aContext, int id, Contact aAgent)
        {
            var lOutput = new UICompanyQuoteSummary();
            var lCompany = new HotelBusiness(aContext).Get(id);
            lOutput.ResortName = lCompany.Name;

            // If the PageId is null, we can't get the other info
            if (lCompany.PageId == null)
                return lOutput;

            var lUIResort = new ResortPageBusiness(aContext).Get(lCompany, aAgent);
            lOutput.Summary = lUIResort.CenterContent.Summary;
            if (lUIResort.MainImage != null)
                lOutput.ResortImageURL = lUIResort.MainImage.Media.ImagePath;
            return lOutput;
        }
        public static UICompany Convert(IDbContext aContext, TourOperator aCompany)
        {
            if (aCompany == null) return null;

            UICompany lOutput = new UICompany();
            Copy(aContext, lOutput, aCompany);
            lOutput.Bookings = BookingUIHelper.Convert(aCompany.Bookings);

            return lOutput;
        }
        public static UICompany Convert(IDbContext aContext, Company aCompany)
        {
            if (aCompany == null) return null;

            UICompany lOutput = new UICompany();
            Copy(aContext, lOutput, aCompany);

            return lOutput;
        }

        public static Company Convert(IDbContext aContext, UICompany aCompany, Agent aAgent)
        {
            if (aCompany == null) return null;


            Company lOutput = new CompanyBusiness(aContext).Get(aCompany.Id);
            if (lOutput == null)
            {

                lOutput = new CompanyBusiness(aContext).Create(aAgent, aCompany);
            }
            bool lFromDetailScreen = aCompany.Name == null && aCompany.Address1 == null && aCompany.Address2 == null && aCompany.WebSite == null;
            // Detail Screen only has the memo field
            if (lFromDetailScreen == false)
            {
                lOutput.Name = aCompany.Name;
                lOutput.Street = aCompany.Address1;
                lOutput.Street2 = aCompany.Address2;
                lOutput.City = aCompany.City;
                lOutput.Id = aCompany.Id;
                lOutput.BusinessTypeID = aCompany.BusinessTypeID;
                lOutput.Visiblity = aCompany.Visibility;
                lOutput.State = aCompany.State;
                lOutput.ZipCode = aCompany.ZipCode;
                lOutput.Website = aCompany.WebSite;
                lOutput.BusinessTypeID = aCompany.BusinessTypeID;

                StorePhoneNumber(aCompany, lOutput);
                StoreEmailAddr(aCompany, lOutput);
            }

            lOutput.Memo = aCompany.Memo;

            return lOutput;
        }

        protected static void StoreEmailAddr(UICompany aInput, Company aOutput)
        {
            string lCleanEmail = DataHelper.CleanEmail(aInput.PrimaryEmail);
            if (aOutput.Emails == null) aOutput.Emails = new List<Email>();
            if (aOutput.Emails.Count(x => x.Preferred == true) == 0)
                aOutput.Emails.Add(new Email() { Address = aInput.PrimaryEmail, CompanyId = aInput.Id, EmailTypeID = Email.BUSINESS_EMAIL, Preferred = true });
            else
                aOutput.Emails.Single(x => x.Preferred == true).Address = lCleanEmail;
        }

        protected static void SetPrimaryEmail (Company aOutput, UICompany aInput )
        {

        }

        protected static void StorePhoneNumber(UICompany aInput, Company aOutput)
        {
            string lCleanPhoneNumber = DataHelper.CleanPhoneNumber(aInput.PrimaryPhone);
            if (aOutput.PhoneNumbers == null) aOutput.PhoneNumbers = new List<Phone>();
            if (aOutput.PhoneNumbers.Count(x => x.Defaut == true) == 0)
                aOutput.PhoneNumbers.Add(new Phone() { PhoneNumber = lCleanPhoneNumber, CompanyId = aInput.Id, Defaut = true, PhoneTypeID = (int)PhoneType.ShortCuts.Cell });
            else
                aOutput.PhoneNumbers.Single(x => x.Defaut == true).PhoneNumber = lCleanPhoneNumber;
        }

        private static string GetCell(Company lData)
        {
            Phone lResult = null;

            if (lData.PhoneNumbers != null && lData.PhoneNumbers.Count > 0)
            {
                lResult = lData.PhoneNumbers.FirstOrDefault(x => x.Defaut == true);
                if (lResult != null)
                    return lResult.PhoneNumber;

                lResult = lData.PhoneNumbers.FirstOrDefault(x => x.PhoneTypeID == 1);

                if (lResult != null)
                    return lResult.PhoneNumber;
            }

            return "";
        }
        public static string AddressLine(UICompany aSrc)
        {
            if (aSrc == null)
                return "";
            string lOutput = aSrc.Address1;
            if (aSrc.Address2 != null && aSrc.Address2.Length > 0)
                lOutput += ", " + aSrc.Address2;

            return lOutput;
        }

        public static string CityStateLine(UICompany aSrc)
        {
            if (aSrc == null)
                return "";

            string lOutput = $"{aSrc.City}";
            if (aSrc.State != null && aSrc.State.Length > 1)
                lOutput += $", {aSrc.State} ";
            lOutput += $"{aSrc.ZipCode}";
            return lOutput;
        }

        protected static string GetEmail(Company lData)
        {
            Email lResult = null;

            if (lData.Emails != null && lData.Emails.Count > 0)
            {
                lResult = lData.Emails.FirstOrDefault(x => x.Preferred == true);
                if (lResult != null)
                    return lResult.Address;

                lResult = lData.Emails.FirstOrDefault(x => x.EmailTypeID == 1);

                if (lResult != null)
                    return lResult.Address; ;
            }

            return "";
        }

    }
}
