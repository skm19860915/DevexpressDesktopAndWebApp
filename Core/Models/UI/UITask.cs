using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Helpers;
using BlitzerCore.Business;
using BlitzerCore.Utilities;
using BlitzerCore.DataAccess;
using BlitzerCore.Models.ASP;

namespace BlitzerCore.Models.UI
{
    public enum Task_Icon { Mine, Delegated, Review, OnHold, None, Bug, FollowUp }
    [NotMapped]
    public class UITask : BaseUI
    {
        public UITask() { }
        public UITask(Task aTask) { }

        public int Id { get; set; }
        public int? OpportunityID { get; set; }
        public string OpportunityName { get; set; }
        public bool? TempDatesCalculated { get; set; }
        public string Name { get; set; }
        public Task_Icon Icon {get; set;}
        public int PrimaryID { get; set; }
        public TaskStatusTypes Status { get; set; }
        public string StatusStr
        {
            get
            {
                switch (Status)
                {
                    case TaskStatusTypes.COMPLETED: return "Completed";
                    case TaskStatusTypes.INPROGRESS: return "InProgress";
                    case TaskStatusTypes.NEW: return "New";
                    case TaskStatusTypes.ONHOLD: return "On Hold";
                    case TaskStatusTypes.REVIEW: return "Review";
                    case TaskStatusTypes.DELETED: return "Deleted";
                }
                return "N/A";
            }
        }
        public int LocationID { get; set; }
        public string CurrentLocation { get; set; }
        public int ContextID { get; set; }
        public string IssuerID { get; set; }
        public int? UserStoryId { get; set; }
        public int? SprintId { get; set; }
        public string OwnerID { get; set; }
        public string ParentName { get; set; }
        public string OwnerName { get; set; }
        public bool? IsTrip { get; set; } 
        public TaskTypes TaskType { get; set; }
        public TaskPriorityTypes PriorityType { get; set; }
        public string PriorityTypeStr
        {
            get
            {
                switch (PriorityType)
                {
                    case TaskPriorityTypes.Important: return "Important";
                    case TaskPriorityTypes.Normal: return "Normal";
                    case TaskPriorityTypes.Low: return "Low";
                    case TaskPriorityTypes.Optional: return "Optional";
                }
                return "N/A";
            }
        }
        public string StartDate { get; set; }
        public string TripStartDate { get; set; }
        public string EndDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? HoldUntil { get; set; }
        public int? Priority { get; set; }
        public int Duration { get; set; }
        public int Completed { get; set; }
        public DurationTypes DurationType { get; set; }
        //public Common.Template.DURATION_TYPE CompletedType { get; set; }
        public double PercentComplete { get; set; }
        public bool DatesCalculated { get; set; }
        public string KanbanColor { get; set; }

        public bool Private { get; set; }
        public bool CanSaveFiles { get; set; }
        [DataType(DataType.Date)]
        public System.DateTime? Deadline { get; set; }
        public string DeadlineStr { get; set; }

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
        public string Description { get; set; }
        public string Comment { get; set; }
        public string Result { get; set; }
        public string CreatedById { get; set; }
        public string CreatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public string LastUpdatedOn { get; set; }
        public bool DayLocked { get; set; }
        public int IssuerEmployeeID { get; set; }
        public int? FeatureID { get; set; }
        public int? MeetingID { get; set; }
        public string SprintName { get; set; }
        public int CurrentUserID { get; set; }
        public string ProjectName { get; set; }
        public int? TemplateID { get; set; }
        public bool Sunday { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool isReoccuring { get; set; }
        public DateTime? ReOccuringStart { get; set; }
        public DateTime? ReOccuringEnd { get; set; }
        public int RecurrenceEndAfter { get; set; }
        public int? TargetCompanyId { get; set; }
        public string TargetCompanyName { get; set; }
        public string TargetContactId { get; set; }
        public string TargetContactName { get; set; }

        public bool Daily { get; set; }
        public bool Weekly { get; set; }
        public bool Monthly { get; set; }
        public bool Yearly { get; set; }
        public bool RecurrenceActive { get; set; }
        public int RecurrenceFrequency { get; set; }
        public string Age { get; set; }
        public string AppointmentStartTime { get; set; }
        public string AppointmentEndTime { get; set; }
        public int Slipped { get; set; }

        //public List<WorkSnapShot> GetShallowSnapShot(ICollection<Work> aWorkList)
        //{
        //    List<WorkSnapShot> lResults = new List<WorkSnapShot>();
        //    foreach (var lData in aWorkList)
        //        lResults.Add(new WorkSnapShot() { ID = lData.ID, Name = lData.Name });

        //    return lResults;
        //}
    }
}
