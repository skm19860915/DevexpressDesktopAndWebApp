using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace BlitzerCore.Models
{

    public enum TaskStatusTypes
    {
        NEW,
        INPROGRESS,
        ONHOLD,
        REVIEW,
        COMPLETED,
        DELETED
    }

    public enum TaskTargets
    {
        Company, 
        Contact
    }

    public enum TaskPriorityTypes
    {
        Important,
        Normal,
        Low,
        Optional
    }

    public enum DurationTypes { Minute, Hour, Day, Week, Month }
    public enum TaskTypes
    {
        WORK,      // StandardWork
        ISSUE,
        FollowUp
    }

    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public enum Locations
        {
            DEFAULT
        }

        private List<int> mParents = new List<int>();
        public Task()
        {
            mParents = new List<int>();
            CreatedOn = DateTime.Now;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            DatesCalculated = true;
            Duration = 30;
            Priority = 5;
        }

        public DateTime? StartDate { get; set; }
        public bool DatesCalculated { get; set; }
        public DateTime EndDate { get; set; }
        public int ClientOrgID { get; set; }
        public int? SprintId { get; set; }
        [ForeignKey("SprintId")]
        public virtual Sprint Sprint { get; set; }
        public string IssuerID { get; set; }
        [ForeignKey("IssuerID")]
        public virtual Agent Issuer { get; set; }
        public string OwnerID { get; set; }
        [ForeignKey("OwnerID")]
        public virtual Agent Owner { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public int Completed { get; set; }
        public int? Priority { get; set; }
        public int? OpportunityID { get; set; }

        [ForeignKey("OpportunityID")]
        public virtual Opportunity Opportunity { get; set; }

        public string Description { get; set; }
        public Nullable<int> FixedField { get; set; }
        public virtual Contact UpdatedBy { get; set; }
        public bool Active { get; set; }
        public TaskTypes TaskType { get; set; }
        public TaskPriorityTypes PriorityType { get; set; }
        public double PercentComplete { get; set; }
        public DateTime? CompletedDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        public virtual Agent CreatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string LastUpdatedById { get; set; }
        [ForeignKey("LastUpdatedById")]
        public virtual Agent LastUpdatedBy { get; set; }
        public bool DayLocked { get; set; }
        public bool Private { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? HoldUntil { get; set; }
        public string Results { get; set; }
        public string Comment { get; set; }
        public TaskTargets Target { get; set; }
        public int? TargetCompanyId { get; set; }
        [ForeignKey("TargetCompanyId")]
        public virtual Company TargetCompany { get; set;  }
        public string TargetContactId { get; set; }
        [ForeignKey("TargetContactId")]
        public virtual Contact TargetContact { get; set; }
        public int? UserStoryId { get; set; }
        [ForeignKey("UserStoryId")]
        public virtual UserStory UserStory { get; set; }
        public bool CompletedNotificationSent { get; set; }
        public Nullable<DateTime> ActiveBaseLineDate { get; set; }
        public bool Sunday { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool isReoccuring { get; set; }
        public Boolean isComplete() => (Status == TaskStatusTypes.COMPLETED);

        public bool isActive
        {
            get
            {
                return (Status == TaskStatusTypes.INPROGRESS ||
                    Status == TaskStatusTypes.NEW ||
                    Status == TaskStatusTypes.ONHOLD ||
                    Status == TaskStatusTypes.REVIEW);
            }
        }

        public TaskStatusTypes Status { get; set; }

        public override string ToString()
        {

            string lStartT = String.Format("{0:ddd M/d/yyyy h:mm tt}", this.StartDate);
            string lEndT = String.Format("{0:ddd M/d/yyyy h:mm tt}", this.EndDate);
            string lDebug = Name.PadRight(40) + " ID=" + Id + " Duration=" + Duration + " Calced = " + DatesCalculated + "  Start = " + lStartT.PadRight(20) + " End           = " + lEndT.PadRight(20);

            return lDebug;
        }
   }
}