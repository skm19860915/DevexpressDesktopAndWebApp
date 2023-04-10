
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using BlitzerCore.DataAccess;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Helpers;
using BlitzerCore.UIHelpers;

namespace BlitzerCore.Business
{
    public class FeatureBusiness
    {
        private IDbContext DbContext { get; set; }

        public FeatureBusiness(IDbContext mContext)
        {
            DbContext = mContext;
        }

        public int Save(Feature aFeature, Agent aAgent)
        {
            SetDeployed(aFeature);
            ComputeStatus(aFeature);
            var lResults = new FeatureDataAccess(DbContext).Save(aFeature);
            UpdateParents(aFeature, aAgent, false);
            return lResults;
            //UpdateTracking(aFeature, aAgent);
        }

        private void UpdateParents(Feature aFeature, Agent aAgent, bool aSave = true)
        {
            if (aSave)
                Save(aFeature, aAgent);
            new SystemBusiness(DbContext).Update(aFeature.SystemId, aAgent);
        }

        private void SetDeployed(Feature aFeature)
        {
            if (aFeature.isDeployed)
                return;

            if (aFeature.Status == FeatureStatus.Deployed)
                aFeature.DeploymentDate = DateTime.Now;
        }

        public void Update(int? aFeatureId, Agent aAgent)
        {
            if (aFeatureId == null)
                return;
            var lFeature = Get(aFeatureId.Value);
            Save(lFeature, aAgent);
        }

        public void ComputeStatus(Feature aFeature)
        {
            if (aFeature == null || aFeature.UserStories == null)
                return;

            var isNotDeployed = aFeature.UserStories.All(x => x.isDeployed == false);
            if (aFeature.UserStories.Count == 0 && aFeature.Status == FeatureStatus.Deployed)
                aFeature.Operational = OperationalStatus.Operational;
            else if (isNotDeployed == true)
                aFeature.Operational = OperationalStatus.Development;
            else
            {
                var lUSCount = aFeature.UserStories.Count();
                var lDefects = aFeature.UserStories.Count(x => x.Operational == OperationalStatus.Down);
                if (lDefects == lUSCount)
                    aFeature.Operational = OperationalStatus.Down;
                else if (lDefects > 0)
                    aFeature.Operational = OperationalStatus.Partially;
                else
                    aFeature.Operational = OperationalStatus.Operational;
            }
        }

        public List<Feature> GetAll(Agent aAgent)
        {
            var lResults = new FeatureDataAccess(DbContext).Get(aAgent);
            lResults.ForEach(x => ComputeStatus(x));

            return lResults;

        }

        public static string GetKanbanMsg(Feature aFeature)
        {
            if (aFeature == null)
                return "";
            if (aFeature.UserStories == null || aFeature.UserStories.Count() == 0)
                return "Need Requirements";
            
            var lFinished = aFeature.UserStories.Count(x => x.Status == UserStoryStatus.Deployed && x.Status != UserStoryStatus.Deleted);
            var lTotal = aFeature.UserStories.Count(x=>x.Status != UserStoryStatus.Deleted);
            return $"Completed {lFinished}/{lTotal}"; 
        }

        internal static string GetKanbanColor(Feature aFeature)
        {
            switch (aFeature.Operational)
            {
                case OperationalStatus.Operational: return "lightgreen";
                case OperationalStatus.Partially: return "yellow";
                case OperationalStatus.Down: return "lightsalmon";
            }

            return "white";
        }

        public Feature Get(int aUSId)
        {
            var lOutput = new FeatureDataAccess(DbContext).Get(aUSId);
            // Things can change in background
            ComputeStatus(lOutput);
            return lOutput;
        }

        public Feature Create(Agent aAgent, int? aSystemId )
        {
            var lFeature = new Feature();
            lFeature.OwnerID = aAgent.Id;
            lFeature.SystemId = aSystemId;
            return lFeature;
        }

        private DateTime GetEndDate(DateTime aStart)
        {
            DateTime lOutput = new DateTime(aStart.Year, aStart.Month, aStart.Day, 17, 0, 0);
            return lOutput.AddDays(11);
        }

        private DateTime GetStartDate()
        {
            DateTime lOutput = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);
            while (lOutput.DayOfWeek != DayOfWeek.Monday)
                lOutput = lOutput.AddDays(1);

            return lOutput;
        }

        private void UpdateTracking(Feature lFeature, Agent aAgent)
        {
            //lUserStory.LastUpdatedOn = DateTime.Now;
            //lUserStory.LastUpdatedById = aAgent.Id;
            //if (lUserStory.CreatedById == null)
            //{
            //    lUserStory.CreatedOn = DateTime.Now;
            //    lUserStory.CreatedById = aAgent.Id;
            //}
        }
    }
}

