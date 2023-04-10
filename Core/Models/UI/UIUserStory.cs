using System;
using System.Collections.Generic;
using System.Text;

namespace BlitzerCore.Models.UI
{
    public class UIUserStory : BaseUI
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string DeploymentDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedById { get; set; }
        public virtual UIContact CreatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string LastUpdatedById { get; set; }
        public string KanbanColor { get; set; }
        public virtual UIContact LastUpdatedBy { get; set; }
        public string IssuerID { get; set; }
        public virtual UIContact Issuer { get; set; }
        public string OwnerID { get; set; }
        public String FeatatureName { get; set; }
        public string Defects { get; set; }
        public int? FeatureId { get; set; }
        public virtual UIContact Owner { get; set; }
        public virtual List<UITask> Work { get; set; }
        public string Description { get; set; }
        public string AcceptanceCriteria { get; set; }
        public int Priority { get; set; }
        public int? LOE { get; set; }
        public bool Private { get; set; }
        public UserStoryStatus Status { get; set; }
        public string StatusStr
        {
            get
            {
                switch (Status)
                {
                    case UserStoryStatus.Deployed: return "Deployed";
                    case UserStoryStatus.Approved: return "Approved";
                    case UserStoryStatus.Requested: return "Requested";
                    case UserStoryStatus.InProgress: return "InProgress";
                    case UserStoryStatus.ReadyForTest: return "Ready For Test";
                    case UserStoryStatus.ReadyToDeploy: return "Ready to Deploy";
                }
                return "Undefined";
            }
        }
        public string Comment { get; set; }
        public int? SprintID { get; set; }
        public double PercentComplete { get; set; }

    }
}
