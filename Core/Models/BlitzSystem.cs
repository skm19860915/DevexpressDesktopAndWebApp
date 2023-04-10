using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public enum OperationalStatus { Development, Down, Partially, Operational }
    public enum SystemStatus { Requested, Approved, InProgress, OnHold, Deployed, Deleted }
    public class BlitzSystem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public SystemStatus Status { get; set; }
        public OperationalStatus Operational { get; set; }
        public string OwnerID { get; set; }
        [ForeignKey("OwnerID")]
        public virtual Contact Owner { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        public virtual Agent CreatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string LastUpdatedById { get; set; }
        [ForeignKey("LastUpdatedById")]
        public virtual Agent LastUpdatedBy { get; set; }
        public List<Feature> Features { get; set; }
    }
}
