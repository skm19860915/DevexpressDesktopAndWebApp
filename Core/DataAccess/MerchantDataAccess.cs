using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models;
using BlitzerCore.Utilities;

namespace BlitzerCore.DataAccess
{
    public class MerchantDataAccess
    {
        IDbContext mContext = null;

        public MerchantDataAccess(IDbContext aContext)
        {
            mContext = aContext;
        }

        public Merchant Save(Merchant aMerchant)
        {
            if (Exists(aMerchant.Id) == false)
                mContext.Merchants.Add(aMerchant);
            else
            {
                var lMerchat = FindById(aMerchant.Id);
                lMerchat.Copy(aMerchant);
            }

            mContext.SaveChangesAsync();

            return aMerchant;
        }
        public IEnumerable<Merchant> GetAll()
        {
            return mContext.Merchants;

        }
        public Merchant Get(int aID)
        {
            return mContext.Merchants.FirstOrDefault(x => x.Id == aID);
        }

        public List<Merchant> GetBySupportedServices(int aServiceID)
        {
            var lResults = (from lMerchant in mContext.Merchants
                    join lMServices in mContext.MerchantServices
                    on lMerchant.Id equals lMServices.MerchantID
                            where lMServices.ServiceID == aServiceID
                            select lMerchant).ToList();

            return lResults;
        }

        public bool AddService(int aMerchantID, int aServiceID)
        {
            try
            {
                MerchantServices lMS = new MerchantServices() { MerchantID = aMerchantID, ServiceID = aServiceID };
                mContext.MerchantServices.Add(lMS);

                mContext.SaveChangesAsync();
                Logger.LogInfo("Successfully saved a merchant service for Merchant =" + aMerchantID + " ServiceID = " + aServiceID);
                return true;
            } catch ( Exception e )
            {
                Logger.LogException("Failed to save a MerchantService ", e);
                return false;
            }
        }

        public List<int> GetServices(int aMerchantID)
        {
            var lResults = (from lMerchant in mContext.Merchants
                            join lMServices in mContext.MerchantServices
                            on lMerchant.Id equals lMServices.MerchantID
                            where lMerchant.Id == aMerchantID
                            select lMServices.ServiceID).ToList();

            return lResults;
        }

        public bool Exists(int id)
        {
            try
            {
                return FindById(id) != null;

            }
            catch (Exception ex)
            {
                Logger.LogException("Failed to call MerchantDataAccess.Exists", ex);
                throw new Exception(ex.Message);
            }

        }

        private Merchant FindById(int id)
        {

            return mContext.Merchants.FirstOrDefault(x => x.Id == id);
        }
    }
}
