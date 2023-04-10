using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using System;

namespace BlitzerCore.DataAccess
{

    public class TeamDataAccess
    {

        const string ClassName = "TeamDataAccess::";
        const string ObjectName = "Team";
        IDbContext DbContext { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<Team> Table { get; set; }

        public TeamDataAccess(IDbContext aContext)
        {
            DbContext = aContext;
            Table = DbContext.Teams;
        }

        public Team Get(int aID)
        {
            return Table
                .FirstOrDefault(x => x.Id == aID);
        }

        public List<Agent> GetTeamMembers(Agent aAgent)
        {
            return DbContext.TeamMembers.Where(i => DbContext.TeamMembers.Where(u => u.MemberId == aAgent.Id).Select(y => y.TeamId).Contains(i.TeamId)).Select(t => t.Member).ToList();
        }

        public bool AddTeamMember(Team aTeam, Agent aAgent)
        {
            string FuncName = ClassName + "AddTeamMember ";
            try
            {
                if (DbContext.TeamMembers.Count(x => x.TeamId == aTeam.Id && x.MemberId == aAgent.Id) > 0)
                {
                    Logger.LogInfo(FuncName + $" Agent {aAgent.Name} was already a member of Team {aTeam.Name}");
                    return true;
                }

                var lNewTeam = new TeamMember() { TeamId = aTeam.Id, MemberId = aAgent.Id };
                DbContext.TeamMembers.Add(lNewTeam);

                var lCnt = DbContext.SaveChanges();
                Logger.LogInfo(FuncName + $"Added {lCnt} {ObjectName} records");
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + $" Failed to add agent {aAgent.Name} to Team {aTeam.Name}", e);
            }

            return false;
        }

        public List<Team> GetAll()
        {
            return Table.ToList();
        }

        public int Save(Team aObject)
        {
            string FuncName = ClassName + "Save ";
            if (aObject.Id > 0)
                Table.Update(aObject);
            else
                Table.Add(aObject);

            try
            {
                var lCnt = DbContext.SaveChanges();
                Logger.LogInfo(FuncName + $"Updated {lCnt} {ObjectName} records");
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
