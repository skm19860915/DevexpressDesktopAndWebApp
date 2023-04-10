using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models;
using BlitzerCore.Utilities;

namespace BlitzerCore.DataAccess
{
    public class BlobDataAccess
    {
        IDbContext mContext = null;

        public BlobDataAccess(IDbContext aContext)
        {
            mContext = aContext;
        }

        public bool Exists(int id)
        {
            try
            {
                return FindById(id) != null;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.InnerException.InnerException.Message);
            }

        }

        private Blob FindById(int id)
        {                
            return mContext.Blobs.Where(x => x.BlobID == id).FirstOrDefault();
        }

        public Blob Save(Blob aBlob)
        {
            if ( Exists(aBlob.BlobID) == false )
                mContext.Blobs.Add(aBlob);
            else
            {
                var lBlob = FindById(aBlob.BlobID);
                lBlob.Copy(aBlob);
            } 

            int lCount = mContext.SaveChanges();
            if (lCount > 0)
                Logger.LogInfo("Successfully Saved Blob");
            else
                Logger.LogError("Failed to save blob with ID = " + aBlob.BlobID);

            return aBlob;
        }
        public IEnumerable<Blob> GetAll()
        {
            return mContext.Blobs;

        }
        public Blob Get(int aID)
        {
            return mContext.Blobs.Where(x => x.BlobID == aID).FirstOrDefault();
        }

        public Blob GetByAdAndOder(int aAdID, int aOrder)
        {
            return mContext.Blobs.FirstOrDefault(x => x.AdID == aAdID && x.Order == aOrder);
        }

        public List<Blob> GetByType(int aMerchantID, Ad.AdTypes aType)
        {
            var lResults = (from lAds in mContext.Ads
                            join lBlobs in mContext.Blobs
                            on lAds.AdID equals lBlobs.AdID
                            where lAds.MerchantId == aMerchantID
                            && lAds.AdType == aType
                            select lBlobs).ToList();

            return lResults;
        }
    }
}
