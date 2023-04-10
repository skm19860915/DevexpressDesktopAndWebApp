using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlitzerCore.DataAccess;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;

namespace BlitzerCore.Models.UI
{
    public class UIFeature : BaseUI
    {
        public int Id { get; set; }
        public string OwnerID { get; set; }
        public int? SystemId { get; set; }
        public virtual UIContact Owner { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string KanbanColor { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public FeatureStatus Status { get; set; }
        public string StatusStr
        {
            get
            {
                switch( Status )
                {
                    case FeatureStatus.Deployed: return "Deployed";
                    case FeatureStatus.Approved: return "Approved";
                    case FeatureStatus.Requested: return "Requested";
                    case FeatureStatus.InProgress: return "InProgress";
                    case FeatureStatus.OnHold: return "OnHold";
                }
                return "Undefined";
            }
        }
        public OperationalStatus OperationalStatus { get; set; }
        public bool isActive { get; set; }
        public List<UIUserStory> UserStories { get; set; }
    }
}
