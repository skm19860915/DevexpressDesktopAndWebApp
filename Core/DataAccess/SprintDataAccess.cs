using System;
using System.Collections.Generic;
using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BlitzerCore.DataAccess
{
    class SprintDataAccess
    {
        const string ClassName = "SprintDataAccess::";
        IDbContext DbContext { get; set; }
        public DbSet<Sprint> Table { get; set; }

        public SprintDataAccess(IDbContext aContext)
        {
            DbContext = aContext;
            Table = DbContext.Sprints;

        }

        public int Save(Sprint aSprint)
        {
            string FuncName = $"{ClassName}Save (Sprint = {aSprint.Id}";
            string lAction = "Added";
            var lCount = 0;
            try
            {
                if (aSprint.Id > 0)
                {
                    Table.Update(aSprint);
                    lAction = "Updated";
                }
                else
                    Table.Add(aSprint);

                lCount = DbContext.SaveChanges();
                Logger.LogInfo($"{FuncName} {lAction} {lCount} Sprint records");
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to save Sprint", e);
                throw e;
            }
            return lCount;

        }

        public Sprint Get(int aSprintID)
        {
            return Table
                .FirstOrDefault(x => x.Id == aSprintID);
        }
        public List<Sprint> Get(Agent aAgent)
        {
            return Table
                .Include(x=>x.UserStories)
                .OrderBy(x=>x.Start).ToList();
        }
    }
}

