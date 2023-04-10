using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BlitzerCore.Utilities;
using BlitzerCore.Models;

namespace BlitzerCore.DataAccess
{
    public class TaskDataAccess
    {
        const string ClassName = "TaskDataAccess::";
        const string ObjectName = "Task";
        IDbContext DbContext { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<Task> Table { get; set; }

        public TaskDataAccess(IDbContext aContext)
        {
            DbContext = aContext;
            Table = DbContext.Tasks;
        }

        public Task Get(int aID)
        {
            return Table
                .Include(x => x.Opportunity)
                .Include(x => x.Owner)
                .Include(x => x.TargetCompany)
                .Include(x=>x.CreatedBy)
                .Include(x=>x.LastUpdatedBy)
                .FirstOrDefault(x => x.Id == aID);
        }
        public List<Task> Get(Sprint aSprint)
        {
            return Table
                .Include(x => x.Opportunity)
                .Include(x => x.Owner)
                .Include(x => x.TargetCompany)
                .Where(x => x.SprintId == aSprint.Id).ToList();
        }

        public List<Task> Get(UserStory aUserStory)
        {
            return Table
                .Include(x => x.Opportunity)
                .Include(x => x.Owner)
                .Include(x => x.TargetCompany)
                .Where(x => x.UserStoryId == aUserStory.Id).ToList();
        }
        public IEnumerable<Task> Get(Agent aAgent)
        {
            var lTeamMemberIDs = DbContext.TeamMembers.Where(i => DbContext.TeamMembers.Where(u => u.MemberId == aAgent.Id).Select(y => y.TeamId).Contains(i.TeamId)).Select(t => t.MemberId).ToList();

            if ( lTeamMemberIDs.Count > 0 )
            return Table
                .Include(x => x.Opportunity)
                .Where(x => lTeamMemberIDs.Contains( x.OwnerID));
            else
                return Table
                    .Include(x => x.Opportunity)
                    .Where(x => x.OwnerID == aAgent.Id || x.IssuerID == aAgent.Id);
        }

        public IEnumerable<Task> Get(Company aCompany)
        {
                return Table
                    .Include(x => x.Opportunity)
                    .Include(x=>x.Owner)
                    .Where(x => x.Owner.EmployerId == aCompany.Id );
        }

        public IEnumerable<Task> GetActiveTasks()
        {
            return Table
                .Include(x => x.Opportunity)
                .Where(x => x.Status != TaskStatusTypes.COMPLETED && x.Status != TaskStatusTypes.DELETED).ToList();
        }

        public List<Task> Get(Opportunity aOpp)
        {
            return Table
                .Include(x => x.Opportunity)
                .Where(x => x.OpportunityID == aOpp.ID).ToList();
        }

        public int Save(Task aObject)
        {
            string FuncName = ClassName + $"Save (Task = {aObject.Id})";
            string lAction = "Added";
            try
            {
                if (aObject.Id > 0)
                {
                    Table.Update(aObject);
                    lAction = "Updated";
                }
                else
                    Table.Add(aObject);

                var lCnt = DbContext.SaveChanges();
                Logger.LogInfo(FuncName + $"{lAction} {lCnt} {ObjectName} records");
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + $" Failed to save {ObjectName} [{aObject.Id}]", e);
            }

            return 0;
        }

        internal int Delete(int aObjectId)
        {
            string FuncName = ClassName + "Delete ";
            if (aObjectId == 0)
                return 0;

            var lObject = Get(aObjectId);
            Table.Remove(lObject);

            try
            {
                var lCnt = DbContext.SaveChanges();
                Logger.LogInfo(FuncName + $"Updated {lCnt} {ObjectName} records");
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + $"Failed to delete {ObjectName} [{aObjectId}]", e);
            }

            return 0;
        }

    }
}
