using System;
using System.Collections.Generic;
using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BlitzerCore.DataAccess
{
    public class AgentDataAccess
    {
        const string ClassName = "AgentDataAccess::";
        IDbContext DbContext { get; set; }
        public DbSet<Agent> Table { get; set; }

        public AgentDataAccess(IDbContext aContext)
        {
            DbContext = aContext;
            Table = DbContext.Agents;

        }

        public int Save(Agent aAgent)
        {
            string FuncName = $"{ClassName}Save (Agent = {aAgent.Id}";
            string lAction = "Added";
            var lCount = 0;
            try
            {
                if (aAgent.Id != null)
                {
                    Table.Update(aAgent);
                    lAction = "Updated";
                }
                else
                    Table.Add(aAgent);

                lCount = DbContext.SaveChanges();
                Logger.LogInfo($"{FuncName} {lAction} {lCount} Agent records");
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to save Agent", e);
                throw e;
            }
            return lCount;

        }

        public List<Agent> GetAgents(Company aCompany)
        {
            return DbContext.Agents.Where(x=>x.EmployerId == aCompany.Id).ToList();
        }

        public AgentProfile GetAgentProfile(Agent aAgent)
        {
            return DbContext.AgentProfiles.FirstOrDefault(x => x.AgentId == aAgent.Id);
        }
        public double GetFixedCosts(Agent aAgent )
        {
            var lTeamMemberIDs = DbContext.TeamMembers.Where(i => DbContext.TeamMembers.Where(u => u.MemberId == aAgent.Id).Select(y => y.TeamId).Contains(i.TeamId)).Select(t => t.MemberId).ToList();
            var lAgents = new List<Agent>();

            if (lTeamMemberIDs.Count > 0)
                lAgents = DbContext.Agents
                .Include(x=>x.Profile)
                .Where(x => lTeamMemberIDs.Contains(x.Id)).ToList();
            else
                lAgents = DbContext.Agents
                .Include(x => x.Profile)
                .Where(x => x.Id == aAgent.Id).ToList();

            return lAgents.Where(x=>x.Profile != null).Sum(x => x.Profile.MonthlyFixedCost);
        }

        public Agent Get(string aAgentID)
        {
            return Table
                .Include(x => x.Profile)
                .FirstOrDefault(x => x.Id == aAgentID);
        }

        public List<Agent> GetAll(int aCompanyId)
        {
            return Table
                .Include(x => x.Profile)
                .Where(x => x.EmployerId == aCompanyId)
                .ToList();
        }
    }
}
