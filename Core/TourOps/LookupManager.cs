using System;
using System.Collections.Generic;
using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using Microsoft.EntityFrameworkCore;


namespace BlitzerCore.TourOps
{
    public class LookupManager
    {
        const string ClassName = "LookupManager::";
        IDbContext DbContext { get; set; }
        public DbSet<DataMap> Table { get; set; }

        public LookupManager(IDbContext aContext)
        {
            DbContext = aContext;
            Table = DbContext.DataMaps;

        }

        public int Save(DataMap aDataMap)
        {
            string FuncName = $"{ClassName}Save (DataMap = {aDataMap.ID}";
            string lAction = "Added";
            var lCount = 0;
            try
            {
                if (aDataMap.ID > 0)
                {
                    Table.Update(aDataMap);
                    lAction = "Updated";
                }
                else
                    Table.Add(aDataMap);

                lCount = DbContext.SaveChanges();
                Logger.LogInfo($"{FuncName} {lAction} {lCount} DataMap records");
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to save DataMap", e);
                throw e;
            }
            return lCount;

        }

        public string Get(int aTourOperator, string aInput)
        {
            var lDataMap = Table.FirstOrDefault(x => x.TourOperatorID == aTourOperator && x.input == aInput);
            if (lDataMap == null)
                return null;

            return lDataMap.output;
        }
    }
}
