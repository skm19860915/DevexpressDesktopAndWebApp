using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models;
using BlitzerCore.Helpers;
using Microsoft.EntityFrameworkCore;

namespace BlitzerCore.DataAccess
{
    public class BlitzerDataAccess
    {
        IDbContext mContext = null;

        public BlitzerDataAccess(IDbContext aContext)
        {
            mContext = aContext;
        }

        public ErrorMsg GetErrorMsg(DataHelper.ErrorCodes aErrorCode)
        {
            return mContext.ErrorMsgs.FirstOrDefault(x => x.Code == (int)aErrorCode);
        }

        public DBVersion GetLatestDBVersion() {
            int lMaxID = mContext.DBVersions.Max(x => x.Id);
            if (lMaxID == 0)
                return new DBVersion();

            return mContext.DBVersions.Find(lMaxID);
        }

        public List<ReferralSource> GetRefferals()
        {
            return mContext.Referrals.ToList();
        }

        public List<NameReplacement> GetNameReplacements()
        {
            return mContext.NameReplacements
                .Include(x=>x.Hotel)
                .ToList();
        }

        public List<RegisterBooking> GetRegisterBookings()
        {
            return mContext.RegisterBookings
                .Include(x => x.Supplier)
                .ToList();
        }

        public bool IsClient(string aEmail)
        {
            return false;
        }
    }
}
