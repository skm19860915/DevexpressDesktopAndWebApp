using System;
using System.Collections.Generic;
using System.Linq;
using BlitzerCore.Business;
using BlitzerCore.Models;
using BlitzerCore.Utilities;
using BlitzerCore.Models.UI;
using BlitzerCore.Helpers;
using BlitzerCore.UIHelpers;
using BlitzerCore.DataAccess;

namespace BlitzerCore.UIHelpers
{
    public class UserStoryUIHelper
    {
        public static List<UIUserStory> Convert(List<UserStory> aStories)
        {
            var lOutput = new List<UIUserStory>();
            if (aStories == null)
                return lOutput;
            foreach (var lUserStory in aStories)
                lOutput.Add(Convert(lUserStory));

            return lOutput;
        }

        public static UIUserStory Convert(UserStory aStory)
        {
            var lUIStory = new UIUserStory();
            lUIStory.Description = aStory.Description;
            lUIStory.Name = aStory.Name;
            lUIStory.Status = aStory.Status;
            lUIStory.OwnerID = aStory.OwnerID;
            lUIStory.Owner = ContactUIHelper.Convert(aStory.Owner);
            lUIStory.SprintID = aStory.SprintID;
            lUIStory.Priority = aStory.Priority;
            lUIStory.IssuerID = aStory.IssuerID;
            lUIStory.LOE = aStory.LOE;
            lUIStory.FeatureId = aStory.FeatureId;
            if (aStory.Feature != null)
                lUIStory.FeatatureName = aStory.Feature.Name;
            else
                Logger.LogWarning("UserStoryUIHelper::Convert - Feature was null for ID=" + aStory.FeatureId);
            lUIStory.DeploymentDate = DataHelper.GetDateString( aStory.DeploymentDate);
            lUIStory.Id = aStory.Id;
            lUIStory.Info = UserStoryBusiness.GetKanbanMsg(aStory);
            lUIStory.KanbanColor = UserStoryBusiness.GetKanbanColor(aStory);
            var lCnt = UserStoryBusiness.GetOpenDefects(aStory);
            lUIStory.Defects = lCnt > 0 ? lCnt.ToString() : ""; 
            lUIStory.Work = TaskUIHelper.Convert(aStory.Work);
            lUIStory.Comment = aStory.Comment;
            return lUIStory;
        }
        public static UserStory Convert(IDbContext aContext, UIUserStory aStory)
        {
            var lUStory = new UserStoryDataAccess(aContext).Get(aStory.Id);
            if (lUStory == null)
                lUStory = new UserStory();

            lUStory.Description = aStory.Description;
            lUStory.Name = aStory.Name;
            lUStory.Status = aStory.Status;
            lUStory.OwnerID = aStory.OwnerID;
            lUStory.SprintID = aStory.SprintID;
            lUStory.Priority = aStory.Priority;
            lUStory.IssuerID = aStory.IssuerID;
            lUStory.LOE = aStory.LOE;
            lUStory.FeatureId = aStory.FeatureId;
            if (aStory.DeploymentDate == "" || aStory.DeploymentDate == null)
                lUStory.DeploymentDate = null;
            lUStory.Id = aStory.Id;
            lUStory.Comment = aStory.Comment;
            return lUStory;
        }
    }
}
