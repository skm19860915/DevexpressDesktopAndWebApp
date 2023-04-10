using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlitzerCore.DataAccess;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;

namespace BlitzerCore.Models
{
    public enum UserStoryStatus { Requested, Approved, InProgress, ReadyForTest, ReadyToDeploy, Deployed, Deleted }
    public class UserStory 
    {
        public UserStory()
        {
        }

        [Key]
        public int Id { get; set; }
        public override string ToString()
        {
            return $"{Id} : {Name}";
        }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        public virtual Agent CreatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string LastUpdatedById { get; set; }
        [ForeignKey("LastUpdatedById")]
        public virtual Agent LastUpdatedBy { get; set; }
        public string IssuerID { get; set; }
        [ForeignKey("IssuerID")]
        public virtual Contact Issuer { get; set; }
        public string OwnerID { get; set; }
        [ForeignKey("OwnerID")]
        public virtual Contact Owner { get; set; }
        public virtual List<Task> Work { get; set; }
        public string Description { get; set; }
        public string AcceptanceCriteria { get; set; }
        public int Priority { get; set; }
        public int? LOE { get; set; }
        public bool Private { get; set; }
        public string Comment { get; set; }
        public UserStoryStatus Status { get; set; }
        public OperationalStatus Operational { get; set; }
        public int? SprintID { get; set; }
        [ForeignKey("SprintID")]
        public virtual Sprint Sprint { get; set; }
        public int? FeatureId { get; set; }
        [ForeignKey("FeatureId")]
        public virtual Feature Feature { get; set; }
        public DateTime? DeploymentDate { get; set; }
        public bool isDeployed => DeploymentDate != null;
        public string BranchName { get; set; }
        public double PercentComplete { get; set; }
        public bool isActive
        {
            get
            {
                return (Status == UserStoryStatus.Approved ||
                    Status == UserStoryStatus.InProgress ||
                    Status == UserStoryStatus.ReadyToDeploy ||
                    Status == UserStoryStatus.Requested );
            }
        }
    }
}