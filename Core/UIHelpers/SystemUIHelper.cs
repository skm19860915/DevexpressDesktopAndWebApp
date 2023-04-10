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
    public class SystemUIHelper
    {
        public static UISystem Convert ( BlitzSystem aSystem )
        {
            var lUISystem = new UISystem();
            lUISystem.Description = aSystem.Description;
            lUISystem.Id = aSystem.Id;
            lUISystem.Name = aSystem.Name;
            lUISystem.OwnerID = aSystem.OwnerID;
            lUISystem.Priority = aSystem.Priority;
            lUISystem.Status = aSystem.Status;
            lUISystem.KanbanColor = SystemBusiness.GetKanbanColor(aSystem);
            lUISystem.Features = FeatureUIHelper.Convert(aSystem.Features);
            lUISystem.Info = SystemBusiness.GetKanbanMsg(aSystem);
            return lUISystem;
        }

        public static BlitzSystem Convert (IDbContext aContext, UISystem aSystem )
        {
            var lSystem = new SystemBusiness(aContext).Get(aSystem.Id);
            if (lSystem == null)
                lSystem = new BlitzSystem();

            lSystem.Description = aSystem.Description;
            lSystem.Id = aSystem.Id;
            lSystem.Name = aSystem.Name;
            lSystem.Priority = aSystem.Priority;
            lSystem.OwnerID = aSystem.OwnerID;
            lSystem.Status = aSystem.Status;

            return lSystem;
        }

        public static List<UISystem> Convert(List<BlitzSystem> aSystems)
        {
            var lOutput = new List<UISystem>();
            foreach (var lSystem in aSystems)
                lOutput.Add(Convert(lSystem));

            return lOutput;
        }
    }
}
