using System;
using System.Collections.Generic;
using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BlitzerCore.DataAccess
{
    public class SystemDataAccess
    {
        const string ClassName = "SystemDataAccess::";
        IDbContext DbContext { get; set; }
        public DbSet<BlitzSystem> Table { get; set; }

        public SystemDataAccess(IDbContext aContext)
        {
            DbContext = aContext;
            Table = DbContext.Systems;

        }

        public int Save(BlitzSystem aBlitzSystem)
        {
            string FuncName = $"{ClassName}Save (BlitzSystem = {aBlitzSystem.Id}";
            string lAction = "Added";
            var lCount = 0;
            try
            {
                if (aBlitzSystem.Id > 0)
                {
                    Table.Update(aBlitzSystem);
                    lAction = "Updated";
                }
                else
                    Table.Add(aBlitzSystem);

                lCount = DbContext.SaveChanges();
                Logger.LogInfo($"{FuncName} {lAction} {lCount} BlitzSystem records");
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to save BlitzSystem", e);
                throw e;
            }
            return lCount;

        }

        public BlitzSystem Get(int aBlitzSystemID)
        {
            return Table
                .Include(x => x.Features).ThenInclude(x1 => x1.UserStories)
                .FirstOrDefault(x => x.Id == aBlitzSystemID);
        }
        public List<BlitzSystem> Get(Agent aAgent)
        {
            return Table
                .Include(x=>x.Features).ThenInclude(x1=>x1.UserStories)
                .ToList();
        }
    }
}

