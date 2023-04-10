using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models;

namespace BlitzerCore.DataAccess
{
    public class UserPreferencesDataAccess
    {
        IDbContext mContext = null;

        public UserPreferencesDataAccess(IDbContext aContext)
        {
            mContext = aContext;
        }

        public IEnumerable<UserLocationPreference> GetLocations(string aUserID)
        {
            return mContext.UserLocationPreferences.Where(x => x.UserID == aUserID);
        }

        public void Save(string aUserID, int aPref)
        {
            if (Exist(aUserID, aPref))
                return;

            mContext.UserLocationPreferences.Add(new UserLocationPreference() { UserID = aUserID, UserPreference = aPref });
         }

        public bool Exist(string aUserID, int aPref)
        {
            var lResult = mContext.UserLocationPreferences.Any(x => x.UserID == aUserID && x.UserPreference == aPref);
            return lResult;
        }

        public void DeleteUserPrefs(string aUserid)
        {
            var lUserLocations = GetLocations(aUserid).ToList();
            foreach (var lUserPrefLocation in lUserLocations)
                mContext.UserLocationPreferences.Remove(lUserPrefLocation);
        }
    }
}
