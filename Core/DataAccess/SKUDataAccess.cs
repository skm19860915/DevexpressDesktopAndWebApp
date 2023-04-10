using System;
using System.Collections.Generic;
using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BlitzerCore.DataAccess
{
    public class SKUDataAccess
    {
        const string ClassName = "SKUDataAccess::";
        IDbContext DbContext { get; set; }
        public DbSet<SKU> Table { get; set; }

        public SKUDataAccess(IDbContext aContext)
        {
            DbContext = aContext;
            Table = DbContext.SKUs;

        }

        public int Save(SKU aSKU)
        {
            string FuncName = $"{ClassName}Save (SKU = {aSKU.SKUID}";
            string lAction = "Added";
            var lCount = 0;
            try
            {
                if (aSKU.SKUID > 0)
                {
                    Table.Update(aSKU);
                    lAction = "Updated";
                }
                else
                    Table.Add(aSKU);

                lCount = DbContext.SaveChanges();
                Logger.LogInfo($"{FuncName} {lAction} {lCount} SKU records");
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to save SKU", e);
                throw e;
            }
            return lCount;

        }
        public List<SKU> GetSKUs(SKU aSKU)
        {
            var lOutput = DbContext.SKUs
                .Include(x => x.Provider)
                .Where(x => x.ProviderID == aSKU.SKUID)
                .ToList();

            return lOutput.OfType<SKU>().ToList();
        }

        public List<SKU> GetSKUs(Company aCompany)
        {
            return DbContext.SKUs
                .Include(x => x.Provider)
                .Where(x => x.ProviderID == aCompany.Id)
                .ToList();
        }

        public SKU GetSKU(int id)
        {
            return DbContext.SKUs
                .Include(x => x.Provider)
                .FirstOrDefault(x => x.SKUID == id) as SKU;
        }
    }
}


