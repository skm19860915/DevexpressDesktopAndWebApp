using System;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Business;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;

namespace BlitzerCore.UIHelpers
{
    public class SKUUIHelper
    {
        public static List<UISKU> Convert(IEnumerable<SKU> aSKUs)
        {
            List<UISKU> lOutput = new List<UISKU>();
            foreach (var lSKU in aSKUs)
            {
                if (lSKU is null)
                    continue;

                lOutput.Add(Convert(lSKU));
            }
            return lOutput;
        }

        public static SKU Convert(IDbContext DbContext, UISKU aUISKU)
        {
            var lSKU = new SKU();
            if (aUISKU.Id > 0 )
                lSKU = new SKUBusiness(DbContext).Get(aUISKU.Id);

            lSKU.Name = aUISKU.Name;
            lSKU.ProviderID = aUISKU.ProviderId;

            return lSKU;
        }

        public static UISKU Convert(SKU aSKU)
        {
            return new UISKU() { Id = aSKU.SKUID, Name = aSKU.Name, Provider = aSKU.Provider.Name, ProviderId = aSKU.ProviderID };
        }
    }
}
