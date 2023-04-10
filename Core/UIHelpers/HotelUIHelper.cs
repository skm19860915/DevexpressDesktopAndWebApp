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
    public class HotelUIHelper :CompanyUIHelper
    {
        public static Hotel Convert(IDbContext DbContext, UIHotel aHotel)
        {
            Hotel lHotel = new HotelBusiness(DbContext).Get(aHotel.Id);
            lHotel.Name = aHotel.Name;
            lHotel.Rating = aHotel.Rating;
            lHotel.Promo = aHotel.Promo;
            lHotel.Description = aHotel.Description;
            lHotel.AirportDistance = aHotel.AirportDistance;
            try
            {
                if ( aHotel.AirPortCode != null )
                    lHotel.AirPortID = new AirPortDataAccess(DbContext).Get(aHotel.AirPortCode).AirPortID;
            }
            catch (Exception) { }
            lHotel.PageId = aHotel.PageId;
            lHotel.Address1 = aHotel.Address1;
            lHotel.Address2 = aHotel.Address2;
            lHotel.City = aHotel.City;
            lHotel.BusinessTypeID = aHotel.BusinessTypeID;
            lHotel.Visiblity = aHotel.Visibility;
            lHotel.State = aHotel.State;
            lHotel.ZipCode = aHotel.ZipCode;
            lHotel.Area = aHotel.Location;
            StorePhoneNumber(aHotel, lHotel);
            StoreEmailAddr(aHotel, lHotel);
            //aBase.QuoteData = new QuoteBusiness(aContext).GetQuoteSummary(aBase.Id);
            lHotel.Website = aHotel.WebSite;
            lHotel.Memo = aHotel.Memo;
            //aHotel.AllInclusive;
            return lHotel;
        }

        public static Hotel Convert(IDbContext DbContext, UICompany aInput)
        {
            Hotel lOutput = new Hotel();
            CompanyUIHelper.Copy(DbContext, lOutput, aInput);
            return lOutput;
        }

        public static List<UIHotel> Convert(IDbContext DbContext, List<Hotel> aHotels)
        {

            List<UIHotel> lOutput = new List<UIHotel>();
            foreach (var lHotel in aHotels)
                lOutput.Add(Convert(DbContext, lHotel));

            return lOutput;
        }

        public static new Company Convert(IDbContext aContext, UICompany aCompany, Agent aAgent)
        {
            if (aCompany == null) return null;


            Hotel lOutput = new HotelBusiness(aContext).Get(aCompany.Id);
            if (lOutput == null)
            {
                lOutput = new HotelBusiness(aContext).Create(aCompany, aAgent);
            }

            return lOutput;
        }


        public static UIHotel Convert(IDbContext DbContext, Hotel aHotel)
        {
            UIHotel lHotel = new UIHotel();
            lHotel.Id = aHotel.Id;
            lHotel.Name = aHotel.Name;
            Copy(DbContext, lHotel, aHotel);
            lHotel.PageId = aHotel.PageId;
            lHotel.Location = aHotel.Area;
            if (aHotel.Rating != null)
                lHotel.Rating = aHotel.Rating.Value;
            else
                lHotel.Rating = 0;
            lHotel.Description = aHotel.Description;
            lHotel.Promo = aHotel.Promo;
            lHotel.AirportDistance = aHotel.AirportDistance;
            if ( aHotel.AirPortID != null )
                lHotel.AirPortCode = new AirPortDataAccess(DbContext).Get(aHotel.AirPortID.Value).Code;

            lHotel.AllInclusive = aHotel.Amenities.Exists(x => x.AmenityID == (int)Amenity.AmenityTypes.AllInclusive);
            lHotel.AdultsOnly = aHotel.Amenities.Exists(x => x.AmenityID == (int)Amenity.AmenityTypes.AdultsOnly);

            lHotel.PrimaryEmail = GetEmail(aHotel);
            lHotel.PrimaryPhone = GetPrimaryPhone(aHotel);
            lHotel.Page = new PageDataAccess(DbContext).GetResort(aHotel);
            if ( lHotel.Page == null ) lHotel.Page = new Page();
            if ( lHotel.Page.HeaderImage == null ) lHotel.Page.HeaderImage = new Block();
            if ( lHotel.Page.HeaderImage.Media == null ) lHotel.Page.HeaderImage.Media = new Media();
            if ( lHotel.Page.HeaderImage.Media.Size1600x1200 == null ) lHotel.Page.HeaderImage.Media.Size1600x1200 = new Photo();

            if (lHotel.Page.ImageURL == "/images/1600x1200/Empty.Jpg" || lHotel.Page.ImageURL == "")
                lHotel.Page.HeaderImage.Media.Size1600x1200.Location = "/Common/Pics/DefaultResort.jpg";
            return lHotel;
        }
    }
}
