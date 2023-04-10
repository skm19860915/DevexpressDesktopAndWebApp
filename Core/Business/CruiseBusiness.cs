using System;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models;
using BlitzerCore.DataAccess;
using BlitzerCore.Models.UI;
using BlitzerCore.UIHelpers;

namespace BlitzerCore.Business
{
    public class CruiseBusiness : CompanyBusiness
    {
        readonly CruiseDataAccess mDataAccess;

        public CruiseBusiness(IDbContext aContext) : base(aContext)
        {
            mDataAccess = new CruiseDataAccess(DbContext);
        }

        public CruiseLine GetCruiseLine (int aId )
        {
            return mDataAccess.GetCruiseLine(aId);
        }

        public List<SKU> GetCruises(CruiseLine aCruiseLine)
        {
            return mDataAccess.GetCruises (aCruiseLine);
        }

        public int Save(UICruiseLine aUICruiseLine, bool aCommit = true)
        {
            var lCruise = CruiseLineUIHelper.Convert(DbContext, aUICruiseLine);

            return new CruiseDataAccess(DbContext).Save(lCruise);
        }
    }
}
