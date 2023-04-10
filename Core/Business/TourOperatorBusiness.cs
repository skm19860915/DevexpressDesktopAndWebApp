using System;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models;
using BlitzerCore.DataAccess;

namespace BlitzerCore.Business
{
    public class TourOperatorBusiness : CompanyBusiness
    {
        readonly TourDataAccess mDataAccess;

        public TourOperatorBusiness(IDbContext aContext) : base(aContext)
        {
            mDataAccess = new TourDataAccess(DbContext);
        }

        public new TourOperator Get(string aName)
        {
            return new TourOperatorDataAccess(DbContext).Get(aName);
        }

        public List<Tour> GetTours(TourOperator aTourOperator)
        {
            return mDataAccess.GetTours (aTourOperator);
        }
    }
}
