using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Helpers;
using BlitzerCore.Business;
using BlitzerCore.Utilities;
using BlitzerCore.DataAccess;
using BlitzerCore.Models.ASP;


namespace BlitzerCore.UIHelpers
{
    public class TaskUIHelper
    {
        const string ClassName = "TaskUIHelper::";

        public static List<UITask> Convert(IEnumerable<Task> aTasks, Agent aAgent = null)
        {
            List<UITask> lOutput = new List<UITask>();
            if (aTasks == null)
                return lOutput;

            foreach (var lTask in aTasks)
                lOutput.Add(Convert(lTask, aAgent));

            return lOutput;
        }

        public static UITask Convert(Task aTask, Agent aAgent = null)
        {
            string FuncName = ClassName + "Convert(Task)";

            var lUITask = new UITask();
            if (aTask == null)
            {
                var lMsg = FuncName + " Null Task passed into convert";
                Logger.LogError(lMsg);
                throw new InvalidDataException(lMsg);
            }
            lUITask.Id = aTask.Id;
            lUITask.Age = DataHelper.Age(aTask.CreatedOn);
            lUITask.Slipped = TaskBusiness.HasSlippeed(aTask);

            lUITask.OwnerID = aTask.OwnerID;
            if (aTask.Owner != null)
                lUITask.OwnerName = aTask.Owner.Name;
            lUITask.DayLocked = aTask.DayLocked;
            if ( aTask.CreatedBy != null )
                lUITask.CreatedById = aTask.CreatedBy.Name;
            if (aTask.LastUpdatedBy != null)
                lUITask.LastUpdatedBy = aTask.LastUpdatedBy.Name;
            lUITask.LastUpdatedOn = DataHelper.GetDateTimeString(aTask.LastUpdatedOn);
            lUITask.CreatedOn = DataHelper.GetDateTimeString(aTask.CreatedOn);
            lUITask.Private = aTask.Private;
            lUITask.Deadline = aTask.Deadline;
            lUITask.DeadlineStr = DataHelper.GetDateString(aTask.Deadline);
            lUITask.Sunday = aTask.Sunday;
            lUITask.Monday = aTask.Monday;
            lUITask.HoldUntil = aTask.HoldUntil;
            lUITask.IssuerID = aTask.IssuerID;
            lUITask.SprintId = aTask.SprintId;
            lUITask.UserStoryId = aTask.UserStoryId;
            lUITask.Tuesday = aTask.Tuesday;
            lUITask.Wednesday = aTask.Wednesday;
            lUITask.Thursday = aTask.Thursday;
            lUITask.Friday = aTask.Friday;
            lUITask.Saturday = aTask.Saturday;
            lUITask.TargetCompanyId = aTask.TargetCompanyId;
            if (aTask.TargetCompany != null)
            {
                lUITask.TargetCompanyName = aTask.TargetCompany.Name;
            }
            lUITask.TargetContactId = aTask.TargetContactId;
            if (aTask.TargetContact != null)
                lUITask.TargetContactName = aTask.TargetContact.Name;
            lUITask.Duration = aTask.Duration;
            lUITask.Completed = aTask.Completed;
            lUITask.Name = aTask.Name;
            lUITask.TaskType = aTask.TaskType;
            lUITask.PriorityType = aTask.PriorityType;
            lUITask.Priority = aTask.Priority;
            lUITask.Description = aTask.Description;
            lUITask.PercentComplete = aTask.PercentComplete;
            //lUITask.TaskPriorityType = aTask.TaskPriorityType;
            lUITask.StartDate = DataHelper.GetDateString(aTask.StartDate);
            if (aTask.Opportunity != null)
                lUITask.TripStartDate = DataHelper.GetDateString(aTask.Opportunity.StartDate);
            lUITask.EndDate = DataHelper.GetDateString(aTask.EndDate);
            lUITask.CompletedDate = aTask.CompletedDate;
            lUITask.KanbanColor = TaskBusiness.GetKanbanColor(aTask);
            lUITask.Icon = TaskBusiness.GetIcon(aTask, aAgent);
            lUITask.Deadline = aTask.Deadline;
            lUITask.Status = aTask.Status;
            lUITask.Comment = aTask.Comment;
            lUITask.Result = aTask.Results;
            lUITask.OpportunityID = aTask.OpportunityID;
            if (aTask.Opportunity != null)
            {
                lUITask.OpportunityName = aTask.Opportunity.Name;
                lUITask.ParentName = aTask.Opportunity.Name;
                if (aTask.Opportunity is Trip)
                    lUITask.IsTrip = true;
                else
                    lUITask.IsTrip = false;
            }
            else
                lUITask.ParentName = "N/A";
            return lUITask;
        }

        public static Task Convert(IDbContext aContext, UITask aUITask)
        {
            Task lTask = new TaskBusiness(aContext).Get(aUITask.Id);
            if (lTask == null)
                lTask = new Task();

            lTask.Name = aUITask.Name;
            lTask.Comment = aUITask.Comment;
            lTask.OpportunityID = aUITask.OpportunityID;
            lTask.Status = aUITask.Status;
            lTask.OwnerID = aUITask.OwnerID;
            lTask.Deadline = aUITask.Deadline;
            lTask.Priority = aUITask.Priority;
            lTask.TaskType = aUITask.TaskType;
            lTask.PriorityType = aUITask.PriorityType;
            lTask.IssuerID = aUITask.IssuerID;
            lTask.HoldUntil = aUITask.HoldUntil;
            lTask.UserStoryId = aUITask.UserStoryId;
            lTask.SprintId = aUITask.SprintId;
            lTask.Results = aUITask.Result;
            lTask.TargetCompanyId = aUITask.TargetCompanyId;
            lTask.TargetContactId = aUITask.TargetContactId;
            return lTask;
        }
    }
}
