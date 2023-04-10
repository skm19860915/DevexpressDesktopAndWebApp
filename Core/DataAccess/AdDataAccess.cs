using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models;
using BlitzerCore.Utilities;

namespace BlitzerCore.DataAccess
{
    public class AdDataAccess
    {
        IDbContext mContext = null;

        public AdDataAccess(IDbContext aContext)
        {
            mContext = aContext;
        }

        public Ad Save(Ad aAd)
        {
            mContext.Ads.Add(aAd);
            mContext.SaveChangesAsync();

            return aAd;
        }
        public IEnumerable<Ad> GetAll()
        {
            return mContext.Ads;

        }
        public Ad Get(int aID)
        {
            return mContext.Ads.Where(x => x.AdID == aID).FirstOrDefault();
        }
        public Ad GetByMerchant(int aID)
        {
            return mContext.Ads.FirstOrDefault(x => x.Merchant.Id == aID);
        }
    }
}
