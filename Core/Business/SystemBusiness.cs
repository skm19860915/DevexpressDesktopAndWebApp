
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
    public class SystemBusiness
    {
        private IDbContext DbContext { get; set; }

        public SystemBusiness(IDbContext mContext)
        {
            DbContext = mContext;
        }

        public int Save(BlitzSystem aSystem, Agent aAgent)
        {
            ComputeStatus(aSystem);
            //UpdateTracking(aSystem, aAgent);
            return new SystemDataAccess(DbContext).Save(aSystem);
        }

        private void ComputeStatus(BlitzSystem aSystem)
        {
            if (aSystem == null || aSystem.Features == null)
                return;

            var areNonDeployed = aSystem.Features.All(x => x.isDeployed == false);
            if (areNonDeployed == true)
                aSystem.Operational = OperationalStatus.Development;
            else
            {
                var lCount = aSystem.Features.Count();
                var lPartial = aSystem.Features.Count(x => x.Operational == OperationalStatus.Partially);
                var lDefects = aSystem.Features.Count(x => x.Operational == OperationalStatus.Down);
                if (lDefects == lCount)
                    aSystem.Operational = OperationalStatus.Down;
                else if (lPartial > 0 || lDefects > 0 )
                    aSystem.Operational = OperationalStatus.Partially;
                else
                    aSystem.Operational = OperationalStatus.Operational;
            }
        }

        public void Update(int? aSystemId, Agent aAgent)
        {
            if (aSystemId == null)
                return;
            var lSystem = Get(aSystemId.Value);
            Save(lSystem, aAgent);
        }

        public List<BlitzSystem> GetAll(Agent aAgent)
        {
            var lOutput = new SystemDataAccess(DbContext).Get(aAgent);
            lOutput.ForEach(x => ComputeStatus(x));
            return lOutput;
        }

        public static string GetKanbanMsg(BlitzSystem aSystem)
        {
            if (aSystem == null)
                return "";
            if (aSystem.Features == null || aSystem.Features.Count() == 0)
                return "Need Features";

            var lFinished = aSystem.Features.Count(x => x.Status == FeatureStatus.Deployed && x.Status != FeatureStatus.Deleted);
            var lTotal = aSystem.Features.Count(x => x.Status != FeatureStatus.Deleted);
            return $"Completed {lFinished}/{lTotal}"; 
        }
        public static string GetKanbanColor(BlitzSystem aSystem)
        {
            switch (aSystem.Operational)
            {
                case OperationalStatus.Operational: return "lightgreen";
                case OperationalStatus.Partially: return "yellow";
                case OperationalStatus.Down: return "lightsalmon";
            }

            return "white";
        }

        public BlitzSystem Get(int aUSId)
        {
            var lSystem = new SystemDataAccess(DbContext).Get(aUSId);
            ComputeStatus(lSystem);
            return lSystem;
        }

        public BlitzSystem Create(Agent aAgent)
        {
            var lSystem = new BlitzSystem();
            lSystem.OwnerID = aAgent.Id;
            return lSystem;
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

        private void UpdateTracking(BlitzSystem lSystem, Agent aAgent)
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

