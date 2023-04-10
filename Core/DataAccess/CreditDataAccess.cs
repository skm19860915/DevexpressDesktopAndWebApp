using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BlitzerCore.DataAccess
{
    public class CreditDataAccess
    {
        const string ClassName = "CreditDataAccess::";
        IDbContext DbContext { get; set; }
        public DbSet<Credit> Table { get; set; }

        public CreditDataAccess(IDbContext aContext)
        {
            DbContext = aContext;
            Table = DbContext.Credits;

        }

        public int Save(Credit aCredit)
        {
            string FuncName = $"{ClassName}Save (Credit = {aCredit.Id}";
            string lAction = "Added";
            var lCount = 0;
            try
            {
                if (aCredit.Id > 0)
                {
                    Table.Update(aCredit);
                    lAction = "Updated";
                }
                else
                    Table.Add(aCredit);

                lCount = DbContext.SaveChanges();
                Logger.LogInfo($"{FuncName} {lAction} {lCount} Credit records");
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to save Credit", e);
                throw e;
            }
            return lCount;

        }

        public Credit Get(int aCreditID)
        {
            return Table
                .Include(x=>x.Traveler)
                .FirstOrDefault(x => x.Id == aCreditID);
        }

        public List<Credit> Get(Agent aAgent)
        {
            var lTeamMemberIDs = DbContext.TeamMembers.Where(i => DbContext.TeamMembers.Where(u => u.MemberId == aAgent.Id).Select(y => y.TeamId).Contains(i.TeamId)).Select(t => t.MemberId).ToList();

            if (lTeamMemberIDs.Count > 0)
                return Table
                    .Where(x => lTeamMemberIDs.Contains(x.OriginalBooking.Trip.AgentId))
                    .Include(x=>x.Traveler)
                    .ToList();
            else
                return Table
                    .Where(x => x.OriginalBooking.Trip.AgentId == aAgent.Id)
                    .Include(x => x.Traveler)
                    .ToList();
        }
    }
}
