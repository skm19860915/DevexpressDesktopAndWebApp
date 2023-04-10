using BlitzerCore.Models;
using BlitzerCore.DataAccess;
using System;
using System.Collections.Generic;

using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Threading.Tasks;
using BlitzerCore.Utilities;

namespace BlitzerCore.Business
{
    public class MerchantBusiness
    {
        private IDbContext mContext;
        private readonly IConfiguration mConfig;

        public MerchantBusiness(IDbContext context, IConfiguration aConfig)
        {
            mContext = context;
            mConfig = aConfig;
        }

        public Ad AddAd(Ad aAd)
        {
            throw new NotImplementedException();
        }
        public Merchant Get(int aID)
        {
            return new MerchantDataAccess(mContext).Get(aID);
        }

        public Merchant Save(Merchant aMerchant)
        {
            return new MerchantDataAccess(mContext).Save(aMerchant);
        }

        public bool Exists(string id)
        {
            throw new NotImplementedException();
        }

        public List<Merchant> GetBySupportedServices(int aServiceID)
        {
            return new MerchantDataAccess(mContext).GetBySupportedServices(aServiceID);
        }

        public bool AddService(int aMerchantID, int aServiceID)
        {
            return new MerchantDataAccess(mContext).AddService(aMerchantID, aServiceID);
        }

        public List<Blob> GetBlobs(int aMerchantID, Ad.AdTypes aType)
        {
            return new BlobDataAccess(mContext).GetByType(aMerchantID, aType);
        }

        public List<int> GetServices(int aMerchantID)
        {
            return new MerchantDataAccess(mContext).GetServices(aMerchantID);
        }

        public bool SaveExperienceBlob(int aUserID, MerchantBlob aBlob, string aPath)
        {
            return SaveBlob(aUserID, aBlob, Ad.AdTypes.Experience, aPath);
        }

        public bool SaveServiceBlob(int aUserID, MerchantBlob aBlob, string aPath)
        {
            return SaveBlob(aUserID, aBlob, Ad.AdTypes.Service, aPath);
        }

        public bool SaveBlob(int aMerchantID, MerchantBlob aBlob, Ad.AdTypes aType, string aPath)
        {
            // Find the Ad 
            try
            {
                var lAdDataService = new AdDataAccess(mContext);
                var lAd = lAdDataService.GetByMerchant(aMerchantID);

                // Create Ad if necessary
                if (lAd == null)
                {
                    lAd = new Ad() { AdType = aType, MerchantId = aMerchantID };
                    lAdDataService.Save(lAd);
                }
                else
                    lAd.AdType = aType;

                if (lAd == null)
                    return false;

                string lBaseName = aBlob.Picture.FileName.Split('.')[0];
                string lExtName = aBlob.Picture.FileName.Split('.')[1];

                string lFileName = lBaseName + "__" + Guid.NewGuid().ToString() + "." + lExtName;
                // Delete the old blob
                var lBlobDS = new BlobDataAccess(mContext);
                var lBlob = lBlobDS.GetByAdAndOder(lAd.AdID, aBlob.Slot);
                if ( lBlob == null )
                    lBlob = new Blob() { AdID = lAd.AdID, Order = aBlob.Slot };

                lBlob.Header = aBlob.Header;
                lBlob.Description = aBlob.Description;
                    
                return true;
            } catch ( Exception e)
            {
                Logger.LogException("Failed to save Merchant Blob", e);
                return false;
            }
        }
    }
}