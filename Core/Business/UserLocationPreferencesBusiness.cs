using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models;
using BlitzerCore.DataAccess;

namespace BlitzerCore.Business
{
    public class UserLocationPreferencesBusiness
    {
        IDbContext mContext = null;

        public UserLocationPreferencesBusiness(IDbContext aContext)
        {
            mContext = aContext;
        }

        public IEnumerable<UserLocationPreference> Get(string aUserid)
        {
            return new UserPreferencesDataAccess(mContext).GetLocations(aUserid);
        }

        public void Save(string aUserid, List<int> aLocations)
        {
            var lBizObj = new UserPreferencesDataAccess(mContext);
            var lCurrentLocations = new LocationDataAccess(mContext).GetAll().ToList();
            lBizObj.DeleteUserPrefs(aUserid);
            mContext.SaveChanges();

            lBizObj = new UserPreferencesDataAccess(mContext);
            foreach (var lLocation in aLocations)
            {
                var lUserPref = new UserLocationPreference();
                lUserPref.UserID = aUserid;
                lUserPref.UserPreference = lLocation;
                lBizObj.Save(aUserid, lLocation);
            }

            mContext.SaveChanges();

        }
    }
}
