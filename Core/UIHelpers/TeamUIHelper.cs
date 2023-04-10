using System;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.DataAccess;

namespace BlitzerCore.UIHelpers
{
    public class TeamUIHelper
    {
        public static Team Convert(IDbContext aDbContext, BlitzerCore.Models.UI.UITeam aInput)
        {
            var lTeam = new TeamDataAccess(aDbContext).Get(aInput.Id);
            return lTeam;
        }
        public static UITeam Convert(IDbContext aDbContext, BlitzerCore.Models.Team aInput)
        {
            var lTeam = new UITeam();
            lTeam.Id = aInput.Id;
            return lTeam;
        }
    }
}
