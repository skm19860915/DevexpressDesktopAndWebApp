
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
    public class SprintBusiness
    {
        private IDbContext DbContext { get; set; }

        public SprintBusiness(IDbContext mContext)
        {
            DbContext = mContext;
        }

        public int Save(Sprint aSprint, Agent aAgent)
        {
            //UpdateTracking(aSprint, aAgent);
            return new SprintDataAccess(DbContext).Save(aSprint);
        }

        public List<Sprint> GetAll(Agent aAgent, bool aAddBacklog = false)
        {
            var lDbSprints = new SprintDataAccess(DbContext).Get(aAgent);
            if (aAddBacklog == true)
            {
                var lBackLog = new Sprint() { Name = "BackLog" };
                lBackLog.UserStories = new UserStoryDataAccess(DbContext).NullSprint().Where(x => x.Status != UserStoryStatus.Deleted).ToList();
                lDbSprints.Add(lBackLog);
            }
            return lDbSprints;
        }

        public Sprint Get(int aUSId)
        {
            return new SprintDataAccess(DbContext).Get(aUSId);
        }

        public Sprint Create(Agent aAgent)
        {
            var lSprint = new Sprint();
            lSprint.Start = GetStartDate();
            lSprint.End = GetEndDate(lSprint.Start);
            lSprint.OwnerID = aAgent.Id;
            return lSprint;
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

        private void UpdateTracking(Sprint lSprint, Agent aAgent)
        {
            //lUserStory.LastUpdatedOn = DateTime.Now;
            //lUserStory.LastUpdatedById = aAgent.Id;
            //if (lUserStory.CreatedById == null)
            //{
            //    lUserStory.CreatedOn = DateTime.Now;
            //    lUserStory.CreatedById = aAgent.Id;
            //}
        }

        public Sprint GetCurrent(Agent aAgent)
        {
            var lSprints = GetAll(aAgent).Where(x => x.Status == Sprint.StatusTypes.Current);
            if (lSprints == null || lSprints.Count() == 0)
                return null;

            return lSprints.FirstOrDefault();
        }
    }
}

