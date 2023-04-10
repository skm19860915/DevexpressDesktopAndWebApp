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
    public class CruiseLineUIHelper :CompanyUIHelper
    {
        public static CruiseLine Convert(IDbContext DbContext, UICompany aCruiseLine)
        {
            CruiseLine lCruiseLine = new CruiseLine();
            if ( aCruiseLine.Id > 0 )
                lCruiseLine = new CruiseBusiness(DbContext).GetCruiseLine(aCruiseLine.Id);
            lCruiseLine.Name = aCruiseLine.Name;
            lCruiseLine.Rating = aCruiseLine.Rating;
            //lCruiseLine.Promo = aCruiseLine.Promo;
            lCruiseLine.Description = aCruiseLine.Description;
            lCruiseLine.PageId = aCruiseLine.PageId;
            lCruiseLine.Street = aCruiseLine.Address1;
            lCruiseLine.Street2 = aCruiseLine.Address2;
            lCruiseLine.City = aCruiseLine.City;
            lCruiseLine.BusinessTypeID = aCruiseLine.BusinessTypeID;
            lCruiseLine.Visiblity = aCruiseLine.Visibility;
            lCruiseLine.State = aCruiseLine.State;
            lCruiseLine.ZipCode = aCruiseLine.ZipCode;
            //lCruiseLine.ar = aCruiseLine.Location;
            StorePhoneNumber(aCruiseLine, lCruiseLine);
            StoreEmailAddr(aCruiseLine, lCruiseLine);
            //aBase.QuoteData = new QuoteBusiness(aContext).GetQuoteSummary(aBase.Id);
            lCruiseLine.Website = aCruiseLine.WebSite;
            lCruiseLine.Memo = aCruiseLine.Memo;
            //aCruiseLine.AllInclusive;
            return lCruiseLine;
        }

        //public static CruiseLine Convert(IDbContext DbContext, UICruiseLine aInput)
        //{
        //    CruiseLine lOutput = new CruiseLine();
        //    CompanyUIHelper.Copy(DbContext, lOutput, aInput);
        //    return lOutput;
        //}

        public static List<UICruiseLine> Convert(IDbContext DbContext, List<CruiseLine> aCruiseLines)
        {

            List<UICruiseLine> lOutput = new List<UICruiseLine>();
            foreach (var lCruiseLine in aCruiseLines)
                lOutput.Add(Convert(DbContext, lCruiseLine));

            return lOutput;
        }

        public static new Company Convert(IDbContext aContext, UICruiseLine aCompany, Agent aAgent)
        {
            if (aCompany == null) return null;


            Hotel lOutput = new HotelBusiness(aContext).Get(aCompany.Id);
            if (lOutput == null)
            {
                lOutput = new HotelBusiness(aContext).Create(aCompany, aAgent);
            }

            return lOutput;
        }


        public static UICruiseLine Convert(IDbContext DbContext, CruiseLine aCruiseLine)
        {
            UICruiseLine lCruiseLine = new UICruiseLine();
            lCruiseLine.Id = aCruiseLine.Id;
            lCruiseLine.Name = aCruiseLine.Name;
            Copy(DbContext, lCruiseLine, aCruiseLine);
            lCruiseLine.PageId = aCruiseLine.PageId;
            if (aCruiseLine.Rating != null)
                lCruiseLine.Rating = aCruiseLine.Rating.Value;
            else
                lCruiseLine.Rating = 0;
            lCruiseLine.Description = aCruiseLine.Description;
            lCruiseLine.Promo = aCruiseLine.Promo;

            lCruiseLine.PrimaryEmail = GetEmail(aCruiseLine);
            lCruiseLine.PrimaryPhone = GetPrimaryPhone(aCruiseLine);
            return lCruiseLine;
        }
    }
}
