using System;
using System.Collections.Generic;
using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BlitzerCore.DataAccess
{
    public class CruiseDataAccess
    {
        const string ClassName = "CruiseDataAccess::";
        IDbContext DbContext { get; set; }
        public DbSet<Cruise> Table { get; set; }

        public CruiseDataAccess(IDbContext aContext)
        {
            DbContext = aContext;
            Table = DbContext.Cruises;

        }

        public CruiseLine GetCruiseLine(int aId)
        {
           var lCompany = DbContext.Companies
                .First(x => x.Id == aId);

            return (CruiseLine)lCompany;
        }

        public int Save(Cruise aCruise)
        {
            string FuncName = $"{ClassName}Save (Cruise = {aCruise.SKUID}";
            string lAction = "Added";
            var lCount = 0;
            try
            {
                if (aCruise.SKUID > 0)
                {
                    Table.Update(aCruise);
                    lAction = "Updated";
                }
                else
                    Table.Add(aCruise);

                lCount = DbContext.SaveChanges();
                Logger.LogInfo($"{FuncName} {lAction} {lCount} Cruise records");
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to save Cruise", e);
                throw e;
            }
            return lCount;

        }

        public int Save(CruiseLine aCruiseLine)
        {
            string FuncName = $"{ClassName}Save (CruiseLine = {aCruiseLine.Id}";
            string lAction = "Added";
            var lCount = 0;
            try
            {
                if (aCruiseLine.Id > 0)
                {
                    DbContext.CruiseLines.Update(aCruiseLine);
                    lAction = "Updated";
                }
                else
                    DbContext.CruiseLines.Add(aCruiseLine);

                lCount = DbContext.SaveChanges();
                Logger.LogInfo($"{FuncName} {lAction} {lCount} CruiseLine records");
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to save Cruise", e);
                throw e;
            }
            return lCount;

        }

        public List<SKU> GetCruises(CruiseLine aCruiseLine)
        {
            var lOutput = DbContext.SKUs
                .Include(x => x.Provider)
                .Where(x => x.Provider.Id == aCruiseLine.Id)
                .ToList();
            return lOutput;
        }

        public List<CruiseLine> GetAll()
        {
            var lOutput = DbContext.Companies
                .Where(x => x.BusinessType.Type == BusinessType.CRUISING)
                .ToList();

            return lOutput.OfType<CruiseLine>().ToList();
        }
        public Cruise GetCruise(int id)
        {
            return DbContext.SKUs
                .Include(x => x.Provider)
                .FirstOrDefault(x => x.SKUID == id) as Cruise;
        }
    }
}


