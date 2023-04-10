using System;
using System.Collections.Generic;
using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BlitzerCore.DataAccess
{
    public class UserStoryDataAccess
    {
        const string ClassName = "UserStoryDataAccess::";
        IDbContext DbContext { get; set; }
        public DbSet<UserStory> Table { get; set; }

        public UserStoryDataAccess(IDbContext aContext)
        {
            DbContext = aContext;
            Table = DbContext.UserStories;

        }

        public int Save(UserStory aUserStory)
        {
            string FuncName = $"{ClassName}Save (UserStory = {aUserStory.Id}";
            string lAction = "Added";
            var lCount = 0;
            try
            {
                if (aUserStory.Id > 0)
                {
                    Table.Update(aUserStory);
                    lAction = "Updated";
                }
                else
                    Table.Add(aUserStory);

                lCount = DbContext.SaveChanges();
                Logger.LogInfo($"{FuncName} {lAction} {lCount} UserStory records");
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to save UserStory", e);
                throw e;
            }
            return lCount;

        }

        internal List<UserStory> NullSprint()
        {
            return Table
                .Where(x => x.SprintID == null && x.Status != UserStoryStatus.Deployed)
                .ToList();
        }

        public List<UserStory> Get(Feature aFeature)
        {
            return Table
                .Include(x=>x.Sprint)
                .Include(x=>x.Work)
                .Where(x => x.FeatureId == aFeature.Id).ToList();
        }

        public List<UserStory> Get(Agent aAgent)
        {
            return Table
                .Include(x => x.CreatedBy)
                .Include(x => x.LastUpdatedBy)
                .Include(x => x.Work)
                .Include(x => x.Sprint)
                .OrderBy(x=>x.Name)
                //.Where(x => x.OwnerID == aAgent.Id || x.IssuerID == aAgent.Id)
                .ToList();
        }

        public UserStory Get(int aUserStoryID)
        {
            return Table
                .Include(x => x.CreatedBy)
                .Include(x => x.LastUpdatedBy)
                .Include(x => x.Work)
                .Include(x => x.Sprint)
                .FirstOrDefault(x => x.Id == aUserStoryID);
        }

        public IEnumerable<UserStory> GetAll()
        {
            return Table
                .Include(x => x.CreatedBy)
                .Include(x => x.LastUpdatedBy)
                .Include(x => x.Work)
                .Include(x => x.Sprint)
                .Include(x => x.Feature)
                .OrderBy(x => x.Priority).ThenBy(x1 => x1.FeatureId);
        }
    }
}


