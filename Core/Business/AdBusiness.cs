using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models;

namespace BlitzerCore.Business
{
    public class AdBusiness : AdEngine
    {
        public AdBusiness(IDbContext aContext) : base (aContext)
        {
        }

        public SavedAd Save(string aUserID, int aAdID)
        {
            var lSavedAd = new SavedAd() { UserID = aUserID, AdID = aAdID, When = DateTime.Now };
            var lResult = mContext.SavedAds.Add(lSavedAd);
            mContext.SaveChangesAsync();

            return lSavedAd;
        }

        public void Delete(string aUserID, int aAdID)
        {
            mContext.SavedAds.Where(x => x.UserID == aUserID && x.AdID == aAdID).FirstOrDefault().Status = SavedAd.Statuses.DEACTIVE;
            mContext.SaveChangesAsync();
        }

        public IEnumerable<SavedAd> Get(string aUserID)
        {
            return mContext.SavedAds.Where(x => x.UserID == aUserID && x.Status == SavedAd.Statuses.ACTIVE);

        }
        public List<Ad> GetAdStream(string aUserId, int aAdID)
        {
            List<Ad> lResults = new List<Ad>();
            Ad lAd = null;

            var lBlobs = mContext.Blobs;


            var lQuery = (from lDBAd in mContext.Ads
                          join lDBBlobs in mContext.Blobs on lDBAd.AdID equals lDBBlobs.AdID
                          join lSavedAds in mContext.SavedAds on lDBAd.AdID equals lSavedAds.AdID
                          select new
                          {
                              AdID = lDBAd.AdID,
                              AdType = lDBAd.AdType,
                              Addr1 = lDBAd.Address1,
                              Addr2 = lDBAd.Address2,
                              City = lDBAd.City,
                              State = lDBAd.State,
                              Country = lDBAd.Country,
                              Description = lDBBlobs.Description,
                              BlobID = lDBBlobs.BlobID,
                              URL = lDBBlobs.URL,
                              Type = lDBBlobs.Type,
                              Title = lDBBlobs.Header,
                              BlobType = lDBBlobs.Type
                          }).OrderBy(x => x.AdID);

            foreach (var lDBAd in lQuery)
            {
                if (lResults.Count(x => x.AdID == lDBAd.AdID) == 0)
                {
                    lAd = new Ad();
                    lAd.AdID = lDBAd.AdID;
                    lAd.AdType = lDBAd.AdType;
                    lAd.Address1 = lDBAd.Addr1;
                    lAd.Address2 = lDBAd.Addr2;
                    lAd.City = lDBAd.City;
                    lAd.State = lDBAd.State;
                    lAd.Country = lDBAd.Country;
                    lResults.Add(lAd);
                }
                else
                {
                    lAd = lResults.Where(x => x.AdID == lDBAd.AdID).First();
                }

                if (lAd.Blobs == null)
                    lAd.Blobs = new List<Blob>();

                Blob lBlob = null;
                lBlob = new Blob();
                lBlob.BlobID = lDBAd.BlobID;
                lBlob.URL = lDBAd.URL;
                lBlob.Type = lDBAd.Type;
                lBlob.Header = lDBAd.Title;
                lBlob.Description = lDBAd.Description;
                lAd.Blobs.Add(lBlob);

            }

            return DetermineResultsStream(aAdID, lResults);
        }

    }
}
