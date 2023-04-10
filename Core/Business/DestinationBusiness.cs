using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models;

namespace BlitzerCore.Business
{
    public class DestinationBusiness : AdEngine
    {

        public DestinationBusiness(IDbContext aContext) : base(aContext )
        {
            mContext = aContext;
        }

        public List<Region> GetRegions()
        {
            throw new NotImplementedException();
        }

        public List<Ad> GetDestinationsStream(string aDesCriteria)
        {
            return GetAdStream("", 1);
        }

        //public List<Page> GetPages(string aUserID, int aPageID)
        //{
        //    return new List<Page>();
        //}

        public List<Ad> GetAdStream(string aUserId, int aAdID)
        {
            List<Ad> lResults = new List<Ad>();
            Ad lAd = null;

            var lBlobs = mContext.Blobs;


            var lQuery = (from lDBAd in mContext.Ads
                          join lDBBlobs in mContext.Blobs
                          on lDBAd.AdID equals lDBBlobs.AdID
                          where lDBAd.AdType == Ad.AdTypes.Destination
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
