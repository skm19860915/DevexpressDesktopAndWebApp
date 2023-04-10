using System;
using System.Collections.Generic;
using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using Microsoft.EntityFrameworkCore;


namespace BlitzerCore.DataAccess
{
    public class BusinessTypeDataAccess
    {
        const string ClassName = "BusinessTypeDataAccess::";
        IDbContext DbContext { get; set; }
        public DbSet<BusinessType> Table { get; set; }

        public BusinessTypeDataAccess(IDbContext aContext)
        {
            DbContext = aContext;
            Table = DbContext.BusinessTypes;
        }

        public int Save(BusinessType aBusinessType)
        {
            string FuncName = $"{ClassName}Save (BusinessType = {aBusinessType.ID}";
            string lAction = "Added";
            var lCount = 0;
            try
            {
                if (aBusinessType.ID > 0)
                {
                    Table.Update(aBusinessType);
                    lAction = "Updated";
                }
                else
                    Table.Add(aBusinessType);

                lCount = DbContext.SaveChanges();
                Logger.LogInfo($"{FuncName} {lAction} {lCount} BusinessType records");
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to save BusinessType", e);
                throw e;
            }
            return lCount;

        }

        public BusinessType Get(int aBusinessTypeID)
        {
            return Table
                .FirstOrDefault(x => x.ID == aBusinessTypeID);
        }

        public List<BusinessType> GetAll()
        {
            return Table.ToList();
        }
    }
}
