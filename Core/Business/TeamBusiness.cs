using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Extensions.Configuration;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.UIHelpers;
using BlitzerCore.Utilities;
using BlitzerCore.DataAccess;
using BlitzerCore.Helpers;

namespace BlitzerCore.Business
{
    public class TeamBusiness 
    {
        const string ClassName = "TeamBusiness::";
        IDbContext DbContext { get; set; }
        TeamDataAccess mDataAccess = null;

        public TeamBusiness(IDbContext aContext)
        {
            DbContext = aContext;
            mDataAccess = new TeamDataAccess(DbContext);
        }

        public void AddMember ( Team aTeam, Agent aAgent )
        {
            mDataAccess.AddTeamMember(aTeam, aAgent);
        }

        public List<Agent> GetTeamMembers(Agent lCurrentUser)
        {
            var lTeamMembers = new TeamDataAccess(DbContext).GetTeamMembers(lCurrentUser);
            if (lTeamMembers == null || lTeamMembers.Count() == 0)
                return new List<Agent>() { lCurrentUser };

            return lTeamMembers;
        }
    }
}