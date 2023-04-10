using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using BlitzerCore.DataAccess;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Helpers;
using BlitzerCore.Utilities;
using BlitzerCore.UIHelpers;

namespace BlitzerCore.Business
{
    public class SKUBusiness
    {
        const string ClassName = "SKUBusiness::";
        private IDbContext DbContext { get; set; }
        private SKUDataAccess DataAccess { get; set; }

        public SKUBusiness(IDbContext mContext)
        {
            this.DbContext = mContext;
            DataAccess = new SKUDataAccess(mContext);
        }

        public SKU Get (int aSKUId)
        {
            return DataAccess.GetSKU(aSKUId);
        }

        public int Save ( SKU aSKU )
        {
            return DataAccess.Save(aSKU);
        }

        public int Save(UISKU aSKU)
        {
            var lSKU = SKUUIHelper.Convert(DbContext, aSKU);
            return DataAccess.Save(lSKU);
        }

    }
}
