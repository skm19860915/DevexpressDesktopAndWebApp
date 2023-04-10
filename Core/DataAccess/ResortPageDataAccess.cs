using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using BlitzerCore.Utilities;
using System;

namespace BlitzerCore.DataAccess
{
    public class ResortPageDataAccess
    {
        IDbContext mContext = null;
        const string ClassName = "ResortPageDataAccess::";

        public Microsoft.EntityFrameworkCore.DbSet<UIResortPage> Table { get; set; }

        public ResortPageDataAccess(IDbContext aContext)
        {
            mContext = aContext;
            Table = mContext.ResortPages;
        }

        public int Save (Amenity aAmenity)
        {
            if (aAmenity.ID > 0)
                mContext.Amenities.Update(aAmenity);
            else
                mContext.Amenities.Add(aAmenity);

            try
            {
                int lCnt = mContext.SaveChanges();
                Logger.LogInfo(ClassName + $"Save updated {lCnt} records");
                return lCnt;
            } catch ( Exception e )
            {
                Logger.LogException(ClassName + "Save(Amenity) failed", e);
                return 0;
            }
        }

        public int Save(UIResortPage aResort, bool aCommit = true)
        {
            try
            {
                if (aResort.Id > 0)
                    mContext.ResortPages.Update(aResort);
                else
                    mContext.ResortPages.Add(aResort);

                if (aCommit)
                {
                    var lCnt = mContext.SaveChanges();
                    Logger.LogInfo(ClassName + $"Save(UIResortPae) updated {lCnt} records");
                    return lCnt;
                }

                return 0;
            }
            catch ( Exception e )
            {
                Logger.LogException("Failed to save Resort", e);
            }

            return 0;
        }

        public UIResortPage Get(int aID)
        {
            return Table
                .FirstOrDefault(x => x.Id == aID);
        }
        public List<UIResortPage> GetAll()
        {
            return Table.ToList();
        }

        internal List<Comparable> GetComparables()
        {
            var lData = mContext.Comparables;
            //if ( lData != null && lData.Count() > 0 )
            //    return mContext.Comparables.ToList();

            return new List<Comparable>();
        }
    }
}

