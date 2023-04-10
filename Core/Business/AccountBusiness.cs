using System;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;

namespace BlitzerCore.Business
{
    public class AccountBusiness
    {
        IDbContext mContext;

        public AccountBusiness(IDbContext aContext) 
        {
            mContext = aContext;
        }

        public void SaveLocation (string aUserID, LoginViewModel aModel)
        {
            if (aModel.Longitude == null || aModel.Latitude == null)
                return;

            UserLocation lLocation = new UserLocation() { UserID = aUserID, When = DateTime.Now, Latitude = aModel.Latitude, Longitude = aModel.Longitude };
            mContext.UserLocations.Add(lLocation);
            mContext.SaveChanges();
        }
    }
}
