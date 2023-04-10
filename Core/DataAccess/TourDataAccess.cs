using System;
using System.Collections.Generic;
using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BlitzerCore.DataAccess
{
    class TourDataAccess
    {
        const string ClassName = "TourDataAccess::";
        IDbContext DbContext { get; set; }
        public DbSet<Tour> Table { get; set; }

        public TourDataAccess(IDbContext aContext)
        {
            DbContext = aContext;
            Table = DbContext.Tours;

        }

        public int Save(Tour aTour)
        {
            string FuncName = $"{ClassName}Save (Tour = {aTour.SKUID}";
            string lAction = "Added";
            var lCount = 0;
            try
            {
                if (aTour.SKUID > 0)
                {
                    Table.Update(aTour);
                    lAction = "Updated";
                }
                else
                    Table.Add(aTour);

                lCount = DbContext.SaveChanges();
                Logger.LogInfo($"{FuncName} {lAction} {lCount} Tour records");
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to save Tour", e);
                throw e;
            }
            return lCount;

        }
        public List<Tour> GetTours(TourOperator aTourOperator)
        {
            var lOutput = DbContext.SKUs
                .Include(x => x.Provider)
                .Where(x => x.ProviderID == aTourOperator.Id)
                .ToList();

            return lOutput.OfType<Tour>().ToList();
        }

        public Tour GetTour(int id)
        {
            return DbContext.SKUs
                .Include(x => x.Provider)
                .FirstOrDefault(x => x.SKUID == id) as Tour;
        }
    }
}


