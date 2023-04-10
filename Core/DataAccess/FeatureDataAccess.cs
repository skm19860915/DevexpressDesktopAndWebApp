using System;
using System.Collections.Generic;
using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BlitzerCore.DataAccess
{
    class FeatureDataAccess
    {
        const string ClassName = "FeatureDataAccess::";
        IDbContext DbContext { get; set; }
        public DbSet<Feature> Table { get; set; }

        public FeatureDataAccess(IDbContext aContext)
        {
            DbContext = aContext;
            Table = DbContext.Features;

        }

        public int Save(Feature aFeature)
        {
            string FuncName = $"{ClassName}Save (Feature = {aFeature.Id}";
            string lAction = "Added";
            var lCount = 0;
            try
            {
                if (aFeature.Id > 0)
                {
                    Table.Update(aFeature);
                    lAction = "Updated";
                }
                else
                    Table.Add(aFeature);

                lCount = DbContext.SaveChanges();
                Logger.LogInfo($"{FuncName} {lAction} {lCount} Feature records");
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to save Feature", e);
                throw e;
            }
            return lCount;

        }

        public Feature Get(int aFeatureID)
        {
            return Table
                .Include(x => x.UserStories).ThenInclude(x1 => x1.Sprint)
                .FirstOrDefault(x => x.Id == aFeatureID);
        }
        public List<Feature> Get(Agent aAgent)
        {
            return Table
                .Include(x=>x.UserStories).ThenInclude(x1=>x1.Sprint)
                .OrderBy(x=>x.Name)
                .ToList();
        }
    }
}

