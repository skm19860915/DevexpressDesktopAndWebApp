using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using BlitzerCore.DataAccess;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Helpers;
using BlitzerCore.Utilities;

namespace BlitzerCore.Business
{
    public class UserStoryBusiness
    {
        private IDbContext DbContext { get; set; }

        public UserStoryBusiness(IDbContext mContext)
        {
            DbContext = mContext;
        }

        public int Save(UserStory aUserStory, Agent aAgent)
        {
            SetDeployed(aUserStory);
            ComputeStatus(aUserStory);
            UpdateTracking(aUserStory, aAgent);
            var lResult = new UserStoryDataAccess(DbContext).Save(aUserStory);
            UpdateParents(aUserStory, aAgent, false);
            return lResult;
        }

        private void UpdateParents(UserStory aUserStory, Agent aAgent, bool aSave = true)
        {
            if (aSave == true)
                Save(aUserStory, aAgent);
            new FeatureBusiness(DbContext).Update(aUserStory.FeatureId, aAgent);
        }

        private void SetDeployed(UserStory aUS)
        {
            if (aUS.isDeployed)
                return;

            if (aUS.Status == UserStoryStatus.Deployed)
                aUS.DeploymentDate = DateTime.Now;
        }

        private void ComputeStatus ( UserStory aUS)
        {
            if (aUS == null || aUS.Work == null )
                return;

            // If there are no defects and the Req is deployed, it must be operational
            if ( aUS.Work.Count == 0 && aUS.Status == UserStoryStatus.Deployed)
                aUS.Operational = OperationalStatus.Operational;
            else if (aUS.isDeployed == false)
                aUS.Operational = OperationalStatus.Development;
            else
            {
                var lDefects = aUS.Work.Count(x => x.TaskType == TaskTypes.ISSUE && x.isActive);
                if (lDefects > 0)
                    aUS.Operational = OperationalStatus.Down;
                else
                    aUS.Operational = OperationalStatus.Operational;                
            }
        }

        public static int GetOpenDefects(UserStory aStory)
        {
            if (aStory.Work == null || aStory.Work.Count == 0)
                return 0;
            return aStory.Work.Count(x => x.TaskType == TaskTypes.ISSUE & x.isActive);
        }

        public static string GetKanbanColor(UserStory aUserStory)
        {
            switch (aUserStory.Operational)
            {
                case OperationalStatus.Operational: return "lightgreen";
                case OperationalStatus.Partially: return "yellow";
                case OperationalStatus.Down: return "lightsalmon";
            }

            return "white";
        }

        public List<UserStory> GetAll(Agent aAgent)
        {
            var lResults = new UserStoryDataAccess(DbContext).Get(aAgent);
            lResults.ForEach(x => ComputeStatus(x));
            return lResults;
        }

        internal void Update(int? aUserStoryId, Agent aAgent)
        {
            if (aUserStoryId == null)
                return;
            var lUS = Get(aUserStoryId.Value);
            Save(lUS, aAgent);
        }

        public List<UserStory> GetOpenRequirements()
        {
            var lResult = new UserStoryDataAccess(DbContext).GetAll()
                .Where(x => x.Status == UserStoryStatus.Requested || x.Status == UserStoryStatus.Approved || ( x.Status == UserStoryStatus.Deployed && x.Work.Count(y=>y.isActive && y.TaskType == TaskTypes.ISSUE ) > 0))
                .ToList();
            lResult.ForEach(x => ComputeStatus(x));
            Logger.LogInfo("Found " + lResult?.Count() + " Open User Stories");
            return lResult;
        }

        public List<UserStory> Get(Feature aFeature)
        {
            var lResult = new UserStoryDataAccess(DbContext).Get(aFeature).Where(x => x.Status != UserStoryStatus.Deleted).ToList();
            lResult.ForEach(x => ComputeStatus(x));
            Logger.LogInfo("Found " + lResult?.Count() + " User Story for Feature : " + aFeature.Name);
            return lResult;
        }

        public UserStory Get(int aUSId)
        {
            var lResult = new UserStoryDataAccess(DbContext).Get(aUSId);
            ComputeStatus(lResult);
            return lResult;
        }

        public UserStory Create(Agent aAgent, int? aFeatureId)
        {
            var lUserStory = new UserStory();
            lUserStory.IssuerID = aAgent.Id;
            UpdateTracking(lUserStory, aAgent);
            lUserStory.FeatureId = aFeatureId;
            lUserStory.Priority = 5;
            return lUserStory;
        }

        public static string GetKanbanMsg(UserStory aStory)
        {
            if (aStory == null || aStory.Sprint == null)
                return "";
            if (aStory.SprintID == null)
                return "Not Assigned";
            return aStory.Sprint.Name + " " + DataHelper.GetShortDateString(aStory.Sprint.Start) + "-" + DataHelper.GetShortDateString(aStory.Sprint.End);
        }

        private void UpdateTracking(UserStory lUserStory, Agent aAgent)
        {
            lUserStory.LastUpdatedOn = DateTime.Now;
            lUserStory.LastUpdatedById = aAgent.Id;
            if (lUserStory.CreatedById == null)
            {
                lUserStory.CreatedOn = DateTime.Now;
                lUserStory.CreatedById = aAgent.Id;
            }
        }
    }
}
