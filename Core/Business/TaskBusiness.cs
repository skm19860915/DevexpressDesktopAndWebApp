using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Xml;
using System.Linq;
using BlitzerCore.DataAccess;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Helpers;
using BlitzerCore.UIHelpers;
using BlitzerCore.Utilities;

namespace BlitzerCore.Business
{
    public class TaskBusiness
    {
        private IDbContext DbContext { get; set; }
        private TaskDataAccess DataAccess { get; set; }
        const string ClassName = "TaskBusiness::";
        IConfiguration Configuration { get; }

        public TaskBusiness(IDbContext mContext, IConfiguration aConfig = null)
        {
            this.DbContext = mContext;
            DataAccess = new TaskDataAccess(mContext);
            Configuration = aConfig;
        }

        public List<Task> Get(Sprint aSprint)
        {
            if (aSprint == null)
                return new List<Task>();

            string FuncName = $"{ClassName}Get(Sprint={aSprint.Id})";
            var lTasks = DataAccess.Get(aSprint).Where(x => x.Status != TaskStatusTypes.DELETED && x.OpportunityID == null).ToList();
            Logger.LogInfo($"{FuncName} Returning {lTasks.Count()} tasks from sprint");
            return lTasks;
        }
        public static string GetKanbanColor(Task aTask)
        {
            if (aTask.TaskType == TaskTypes.ISSUE)
                return "lightsalmon";
            
            return "white";
        }

        /// <summary>
        /// Used to set the background color for the Kanban board
        /// </summary>
        /// <param name="aTask"></param>
        /// <returns></returns>
        public static int HasSlippeed(Task aTask)
        {
            if (aTask.Deadline == null || aTask.Deadline.Value > DateTime.Now)
                return 0;

            return 1;
        }

        public List<Task> GetReviewTasks(Agent aUser)
        {
            var lTasks = DataAccess
                .Get(aUser)
                .Where(x => x.Status == TaskStatusTypes.REVIEW
                    && x.IssuerID == aUser.Id)
                .OrderBy(x => x.Priority).ThenBy(x => x.CreatedOn)
                .ToList();

            return lTasks;
        }

        /// <summary>
        /// Take all Tasks with a deadline of today and change priority to Important
        /// </summary>
        public void EscalteDeadLineTasks(DateTime aToday)
        {
            string FuncName = $"{ClassName}EscalteDeadLineTasks(Today={aToday.ToShortDateString()})";
            Logger.EnterFunction(FuncName);
            try
            {
                DateTime lTodaysDate = new DateTime(aToday.Year, aToday.Month, aToday.Day);
                var lActiveTasks = DataAccess.GetActiveTasks();
                var lDeadLinesToday = lActiveTasks.Where(x =>
                    x.PriorityType != TaskPriorityTypes.Important && x.Deadline != null &&
                    x.Deadline?.Date.Ticks <= aToday.Ticks);
                foreach (var lTask in lDeadLinesToday)
                {
                    lTask.PriorityType = TaskPriorityTypes.Important;
                    Logger.LogDebug($"Updating Priority on {lTask.Name}[{lTask.Id}]");
                    Save(lTask);
                }
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} ", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }

        public static Task_Icon GetIcon(Task aTask, Agent aAgent )
        {
            if (aTask.TaskType == TaskTypes.FollowUp)
                return Task_Icon.FollowUp;

            if ( aTask.IssuerID == aTask.OwnerID && aTask.TaskType == TaskTypes.ISSUE)
                return Task_Icon.Bug;

            if (aTask.IssuerID != aTask.OwnerID && aTask.Status == TaskStatusTypes.REVIEW)  
                return Task_Icon.Review;
            else if (aTask.IssuerID != aTask.OwnerID)
            {
                if (aTask.TaskType == TaskTypes.ISSUE)
                    return Task_Icon.Bug;

                if ( aAgent == null || aAgent.Id == aTask.IssuerID)
                    return Task_Icon.Delegated;

                return Task_Icon.Mine;
            }
            else if (aTask.Status == TaskStatusTypes.ONHOLD)
                return Task_Icon.OnHold;


            return Task_Icon.None;
        }

        public List<Task> Sort(List<Task> aUnSortedTasks)
        {
            var lSortedTasks = aUnSortedTasks.OrderBy(x => x.Priority);
            var lTasks2 = lSortedTasks.Where(x => x.PriorityType == TaskPriorityTypes.Important);
            var lTasks3 = lSortedTasks.Where(x => x.PriorityType == TaskPriorityTypes.Normal);
            var lTasks4 = lSortedTasks.Where(x => x.PriorityType == TaskPriorityTypes.Low);
            var lTasks5 = lSortedTasks.Where(x => x.PriorityType == TaskPriorityTypes.Optional);
            var lOutput = new List<Task>();
            lOutput.AddRange(lTasks2);
            lOutput.AddRange(lTasks3);
            lOutput.AddRange(lTasks4);
            lOutput.AddRange(lTasks5);
            return lOutput;
        }

        public List<Task> Get(UserStory aUserStory)
        {
            string FuncName = $"{ClassName}Get(UserStory={aUserStory.Id})";
            var lTasks = DataAccess.Get(aUserStory).Where(x => x.Status != TaskStatusTypes.DELETED).ToList();
            Logger.LogInfo($"{FuncName} Returning {lTasks.Count()} tasks from user story");
            return lTasks;
        }

        public Task Get(int aTaskId)
        {
            return DataAccess.Get(aTaskId);
        }
        public IEnumerable<Task> Get(Agent aAgent)
        {
            return DataAccess.Get(aAgent).Where(x=>x.Status != TaskStatusTypes.DELETED);
        }

        public IEnumerable<Task> Get(Company aEmployer)
        {
            return DataAccess.Get(aEmployer).Where(x => x.Status != TaskStatusTypes.DELETED);
        }

        public int Save(Task aTask, Agent aAgent = null)
        {
            string FuncName = $"{ClassName}Task(id={aTask.Id})";
            try
            {
                Logger.EnterFunction(FuncName);
                Agent lAgent = aAgent ?? aTask.CreatedBy;
                var lTriggered = false;
                var lNewTask = false;
                var lOrignalOwnerId = aTask.OwnerID;
                if ((aTask.Status == TaskStatusTypes.COMPLETED ||aTask.Status == TaskStatusTypes.REVIEW ) && aTask.CompletedDate == null)
                    aTask.CompletedDate = DateTime.Now;

                if (aTask.Id == 0 && aTask.OwnerID != aTask.IssuerID)
                {
                    Logger.LogDebug("Flagged as new Task");
                    lNewTask = true;
                }

                UpdateStats(aTask, lAgent);
                if (aTask.OwnerID != aTask.IssuerID && aTask.Status == TaskStatusTypes.REVIEW)
                {
                    Logger.LogDebug("Flagged as completing delegated task");
                    lTriggered = true;
                }
                var lResult = DataAccess.Save(aTask);
                UpdateParents(aTask, lAgent);
                if (lTriggered == true)
                    new NotificationBusiness(DbContext, Configuration).SendTaskComplete(aTask, aTask.IssuerID, lOrignalOwnerId);
                if (lNewTask == true)
                    new NotificationBusiness(DbContext, Configuration).SendNewTask(aTask);
                return lResult;
            } finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }

        public int Save(UITask aUITask, Agent aAgent )
        {
            var lTask = TaskUIHelper.Convert(DbContext, aUITask);
            return Save(lTask, aAgent);
        }

        private void UpdateParents(Task lTask, Agent aAgent)
        {
            new UserStoryBusiness(DbContext).Update(lTask.UserStoryId, aAgent);
        }

        private void UpdateStats(Task lTask, Agent aAgent)
        {
            lTask.LastUpdatedOn = DateTime.Now;
            
            if ( aAgent != null )
                lTask.LastUpdatedById = aAgent.Id;

            if (lTask.CreatedOn == null)
            {
                lTask.CreatedOn = DateTime.Now;
                if ( aAgent != null )
                    lTask.CreatedById = aAgent.Id;
            }
        }

        public IEnumerable<Task> GetMyTasks(Agent aUser)
        {
            var lTasks = DataAccess
                .Get(aUser)
                .Where(x => (x.Status != TaskStatusTypes.DELETED && x.Status != TaskStatusTypes.COMPLETED) 
                    && (x.OwnerID == aUser.Id || x.IssuerID == aUser.Id))
                .OrderBy(x=>x.Priority).ThenBy(x=>x.CreatedOn)
                .ToList();
            // Remove all tasks in review that the person owns
            lTasks = lTasks.Where(x => (x.Status == TaskStatusTypes.REVIEW && x.OwnerID == aUser.Id) == false).ToList();
            // Show tasks delegated and in the REVIEW state they person deleted
            var lTasks1 = lTasks.Where(x => x.Status == TaskStatusTypes.REVIEW && x.IssuerID == aUser.Id);
            var lResults = lTasks.Where(x => lTasks1.Contains(x) == false && x.Status != TaskStatusTypes.ONHOLD);
            // Next are important tasks
            var lOnHold = lTasks.Where(x => lTasks1.Contains(x) == false && x.Status == TaskStatusTypes.ONHOLD);
            var lTasks2 = lResults.Where(x => x.PriorityType == TaskPriorityTypes.Important);
            var lTasks3 = lResults.Where(x => x.PriorityType == TaskPriorityTypes.Normal);
            var lTasks4 = lResults.Where(x => x.PriorityType == TaskPriorityTypes.Low);
            var lTasks5 = lResults.Where(x => x.PriorityType == TaskPriorityTypes.Optional);
            var lOutput = new List<Task>();
            lOutput.AddRange(lTasks1);
            lOutput.AddRange(lTasks2);
            lOutput.AddRange(lTasks3);
            lOutput.AddRange(lTasks4);
            lOutput.AddRange(lTasks5);
            lOutput.AddRange(lOnHold);
            return lOutput;
        }

        public List<Task> GetTripTasks(Agent aUser)
        {
            var lTripTask = DataAccess.Get(aUser).Where(x => x.Opportunity != null
                    && x.Opportunity.Stage == OpportunityStages.Won
                    && (Trip)(x.Opportunity) != null).ToList();
            var lTasks = lTripTask
                .Where(x =>((x.Status != TaskStatusTypes.DELETED
                    && x.Status != TaskStatusTypes.COMPLETED
                    && ((Trip)(x.Opportunity)).TripStatus != Trip.Statuses.Cancelled
                    && ((Trip)(x.Opportunity)).TripStatus != Trip.Statuses.Deleted)
                    || (x.Status == TaskStatusTypes.COMPLETED && x.Opportunity.StartDate > DateTime.Now
                    && ((Trip)(x.Opportunity)).TripStatus != Trip.Statuses.Cancelled
                    && ((Trip)(x.Opportunity)).TripStatus != Trip.Statuses.Deleted
                    )))
                .OrderBy (x => x.Opportunity.StartDate)
                .ToList();
            return lTasks;
        }

        public List<Task> GetTripTasks(Company aCompany)
        {
            var lTripTask = DataAccess.Get(aCompany).Where(x => x.Opportunity != null
                                                                && x.Opportunity.Stage != OpportunityStages.Loss);
            var lTasks = lTripTask
                .Where(x => (
                // Get the not completed Opportunities
                (x.Status != TaskStatusTypes.DELETED && x.Status != TaskStatusTypes.COMPLETED && x.Opportunity.Stage != OpportunityStages.Won)
                // Show completed tasks
                || (x.Status == TaskStatusTypes.COMPLETED && x.Opportunity.StartDate > DateTime.Now)
                // Show trips not cancelled and not deleted
                || ( x.Opportunity.Stage == OpportunityStages.Won && ((Trip)(x.Opportunity)).TripStatus != Trip.Statuses.Cancelled
                                                                       && ((Trip)(x.Opportunity)).TripStatus != Trip.Statuses.Deleted)
                ));

            return lTasks.OrderBy(x => x.PriorityType).ThenBy(x => x.Opportunity.StartDate).ToList();
        }


        public Task Create(Opportunity aOpp, Agent aAgent, DateTime? aDueDate = null)
        {
            var lTask = new Task();
            if (aOpp != null)
            {
                lTask.OpportunityID = aOpp.ID;
                lTask.Opportunity = aOpp;
            }

            lTask.CreatedOn = DateTime.Now;
            lTask.CreatedBy = aAgent;
            lTask.CreatedById = aAgent.Id;
            lTask.UpdatedBy = lTask.CreatedBy;
            lTask.LastUpdatedOn = lTask.CreatedOn;
            lTask.LastUpdatedById = lTask.CreatedById;
            lTask.PriorityType = TaskPriorityTypes.Normal;
            lTask.Owner = aAgent;
            lTask.OwnerID = aAgent.Id;
            lTask.IssuerID = aAgent.Id;
            lTask.Deadline = aDueDate;
            var lSprint = new SprintBusiness(DbContext).GetCurrent(aAgent);
            if ( lSprint != null )
            {
                lTask.SprintId = lSprint.Id;
                lTask.Sprint = lSprint;
            }
                
            return lTask;
        }

        public Task Create(UserStory aUserStory, Agent aAgent, DateTime? aDueDate = null)
        {
            var lTask = new Task();
            if (aUserStory != null)
            {
                lTask.UserStoryId = aUserStory.Id;
                lTask.UserStory = aUserStory;
                lTask.SprintId = aUserStory.SprintID;
                lTask.Sprint = aUserStory.Sprint;
            }

            lTask.CreatedOn = DateTime.Now;
            lTask.CreatedBy = aAgent;
            lTask.CreatedById = aAgent.Id;
            lTask.UpdatedBy = lTask.CreatedBy;
            lTask.LastUpdatedOn = lTask.CreatedOn;
            lTask.LastUpdatedById = lTask.CreatedById;
            lTask.PriorityType = TaskPriorityTypes.Normal;
            lTask.Owner = aAgent;
            lTask.OwnerID = aAgent.Id;
            lTask.IssuerID = aAgent.Id;
            lTask.Deadline = aDueDate;
            return lTask;
        }

        internal void Create(QuoteRequest lQuoteRequest, string aTaskName, DateTime? aDueDate)
        {
            var lOpp = new OpportunityBusiness(DbContext).GetOpportunity(lQuoteRequest.OpportunityID);
            var lTask = Create(lOpp, lQuoteRequest.Agent, aDueDate);
            lTask.Name = aTaskName;
            Save(lTask);
        }
        internal void Create(Opportunity aOpp, string aTaskName, DateTime? aDueDate, Agent aAgent = null )
        {
            var lAgent = aAgent != null ? aAgent : aOpp.Agent;

            var lTask = Create(aOpp, lAgent, aDueDate);
            lTask.Name = aTaskName;
            Save(lTask);
        }
        internal void Create(Opportunity aOpp, string aTaskName, string aDescription, DateTime? aDueDate, Agent aAgent = null)
        {
            var lAgent = aAgent != null ? aAgent : aOpp.Agent;

            var lTask = Create(aOpp, lAgent, aDueDate);
            lTask.Name = aTaskName;
            lTask.Description = aDescription;
            Save(lTask);
        }
    }
}
