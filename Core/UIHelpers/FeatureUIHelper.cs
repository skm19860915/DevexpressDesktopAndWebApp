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
    public class FeatureUIHelper
    {
        public static UIFeature Convert ( Feature aFeature )
        {
            var lUIFeature = new UIFeature();
            lUIFeature.Description = aFeature.Description;
            lUIFeature.Id = aFeature.Id;
            lUIFeature.Name = aFeature.Name;
            lUIFeature.OwnerID = aFeature.OwnerID;
            lUIFeature.Priority = aFeature.Priority;
            lUIFeature.OperationalStatus = aFeature.Operational;
            lUIFeature.SystemId = aFeature.SystemId;
            lUIFeature.Status = aFeature.Status;
            lUIFeature.UserStories = UserStoryUIHelper.Convert(aFeature.UserStories);
            lUIFeature.KanbanColor = FeatureBusiness.GetKanbanColor(aFeature);
            lUIFeature.Info = FeatureBusiness.GetKanbanMsg(aFeature);
            return lUIFeature;
        }

        public static Feature Convert (IDbContext aContext, UIFeature aFeature )
        {
            var lFeature = new FeatureBusiness(aContext).Get(aFeature.Id);
            if (lFeature == null)
                lFeature = new Feature();

            lFeature.Description = aFeature.Description;
            lFeature.Id = aFeature.Id;
            lFeature.Name = aFeature.Name;
            lFeature.Priority = aFeature.Priority;
            lFeature.Operational = aFeature.OperationalStatus;
            lFeature.OwnerID = aFeature.OwnerID;
            lFeature.Status = aFeature.Status;
            lFeature.SystemId = aFeature.SystemId;

            return lFeature;
        }

        public static List<UIFeature> Convert ( List<Feature> aFeatures)
        {
            var lOutput = new List<UIFeature>();
            if (aFeatures == null || aFeatures.Count == 0)
                return lOutput;

            foreach (var lFeature in aFeatures)
                lOutput.Add(Convert(lFeature));

            return lOutput;
        }
    }
}
