using System;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.DataAccess;
using BlitzerCore.Business;
using BlitzerCore.Helpers;

namespace BlitzerCore.UIHelpers
{
    public class SprintUIHelper
    {
        public static UISprint Convert ( Sprint aSprint )
        {
            var lUISprint = new UISprint();
            lUISprint.Description = aSprint.Description;
            lUISprint.End = aSprint.End;
            lUISprint.Id = aSprint.Id;
            lUISprint.Name = aSprint.Name;
            lUISprint.OwnerID = aSprint.OwnerID;
            lUISprint.Start = aSprint.Start;
            lUISprint.EndStr = DataHelper.GetDateString(aSprint.End);
            lUISprint.StartStr = DataHelper.GetDateString(aSprint.Start);
            lUISprint.Status = aSprint.Status;
            lUISprint.UserStories = UserStoryUIHelper.Convert(aSprint.UserStories);
            return lUISprint;
        }

        public static Sprint Convert (IDbContext aContext, UISprint aSprint )
        {
            var lSprint = new SprintBusiness(aContext).Get(aSprint.Id);
            if (lSprint == null)
                lSprint = new Sprint();

            lSprint.Description = aSprint.Description;
            lSprint.End = aSprint.End;
            lSprint.Id = aSprint.Id;
            lSprint.Name = aSprint.Name;
            lSprint.OwnerID = aSprint.OwnerID;
            lSprint.Start = aSprint.Start;
            lSprint.Status = aSprint.Status;

            return lSprint;
        }

        public static List<UISprint> Convert ( List<Sprint> aSprints)
        {
            var lOutput = new List<UISprint>();
            foreach (var lSprint in aSprints)
                lOutput.Add(Convert(lSprint));

            return lOutput;
        }
    }
}
