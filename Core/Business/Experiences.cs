using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models;

namespace BlitzerCore.Business
{
    public class ExperiencesBusiness : AdEngine
    {
        public ExperiencesBusiness(IDbContext aContext) : base ( aContext)
        {
        }

        //public Page GetMainPage(string aUserID)
        //{
        //    var lMainPage = new Page();

        //    Blob lImage1 = new Blob();
        //    lImage1.Header = "";
        //    lImage1.URL = new Uri("https://blitzerblobs.blob.core.windows.net/pictures-ads/relax.jpg");

        //    Blob lImage2 = new Blob();
        //    lImage2.Header = "";
        //    lImage2.URL = new Uri("https://blitzerblobs.blob.core.windows.net/pictures-ads/Tour.jpg");

        //    Blob lImage3 = new Blob();
        //    lImage3.Header = "";
        //    lImage3.URL = new Uri("https://blitzerblobs.blob.core.windows.net/pictures-ads/Meal.jpg");

        //    Blob lImage4 = new Blob();
        //    lImage4.Header = "";
        //    lImage4.URL = new Uri("https://blitzerblobs.blob.core.windows.net/pictures-ads/Kayaking.jpg");

        //    Blob lImage5 = new Blob();
        //    lImage5.Header = "";
        //    lImage5.URL = new Uri("https://blitzerblobs.blob.core.windows.net/pictures-ads/HotAirBallons.jpg");

        //    Blob lImage6 = new Blob();
        //    lImage6.Header = "";
        //    lImage6.URL = new Uri("https://blitzerblobs.blob.core.windows.net/pictures-ads/Camping.jpg");

        //    List<Blob> lImages = new List<Blob>();
        //    lImages.Add(lImage1);
        //    lImages.Add(lImage2);
        //    lImages.Add(lImage3);
        //    lImages.Add(lImage4);
        //    lImages.Add(lImage5);
        //    lImages.Add(lImage6);
        //    lMainPage.Images = lImages;
        //    return lMainPage;
        //}

        public List<Ad> GetExperiencesStream(string aLocation)
        {
            //Merchant lMerch = new Merchant()
            //{
            //    Address1 = "4601 Presidents Drive",
            //    City = "Lanham",
            //    State = "Maryland",
            //    ZipCode = "20706",
            //    MerchantName = "Outdoor Advenures"
            //};

            //var lAds = GetAdStream("", 22);

            //foreach (var lAd in lAds)
            //    lAd.Merchant = lMerch;

            //return lAds;
            throw new NotImplementedException();
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
                          where lDBAd.AdType == Ad.AdTypes.Experience 
                          select new
                          {
                              AdID = lDBAd.AdID,
                              AdType = lDBAd.AdType,
                              City = lDBAd.City,
                              State = lDBAd.State,
                              Country = lDBAd.Country,
                              Description = lDBBlobs.Description,
                              BlobID = lDBBlobs.BlobID,
                              URL = lDBBlobs.URL,
                              Order = lDBBlobs.Order,
                              Type = lDBBlobs.Type,
                              Title = lDBBlobs.Header,
                              BlobType = lDBBlobs.Type
                          }).ToList();


            foreach (var lDBAd in lQuery)
            {
                if (lResults.Count(x => x.AdID == lDBAd.AdID) == 0)
                {
                    lAd = new Ad();
                    lAd.AdID = lDBAd.AdID;
                    lAd.AdType = lDBAd.AdType;
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
                lBlob.URL = lDBAd.URL;
                lBlob.BlobID = lDBAd.BlobID;
                lBlob.Type = lDBAd.Type;
                lBlob.Header = lDBAd.Title;
                lBlob.Order = lDBAd.Order;
                lBlob.Description = lDBAd.Description;
                lAd.Blobs.Add(lBlob);

            }

            return DetermineResultsStream(aAdID, lResults);
        }
    }
}
