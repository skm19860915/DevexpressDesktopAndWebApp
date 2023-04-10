using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models;
using BlitzerCore.Utilities;

namespace BlitzerCore.DataAccess
{
    public class TourOperatorDataAccess
    {
        const string ClassName = "TourOperatorDataAccess::";
        IDbContext mContext = null;

        public TourOperatorDataAccess(IDbContext aContext)
        {
            mContext = aContext;
        }

        public TourOperator Get(string aOperatorName) => mContext.TourOperators.First(x => x.Name == aOperatorName);

        public TourOperator Get(int id) => mContext.TourOperators.First(x => x.Id == id);

        public int Save(TourOperator aOperator)
        {
            string FuncName = ClassName + $"Save (TourOperator = {aOperator.Id})";
            mContext.TourOperators.Add(aOperator);
            try
            {
                return mContext.SaveChanges();
            } catch ( Exception e )
            {
                Logger.LogException(FuncName + " Failed to add TourOperator", e);
            }
            return 0;
        }

        public IEnumerable<TourOperator> GetAll()
        {
            return mContext.TourOperators;
        }
    }
}
