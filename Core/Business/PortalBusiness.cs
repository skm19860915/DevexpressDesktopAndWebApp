using System;
using System.Collections.Generic;
using BlitzerCore.DataAccess;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using System.Linq;
using BlitzerCore.UIHelpers;

namespace BlitzerCore.Business
{
    public class PortalBusiness
    {
        readonly IDbContext mContext;
        public const int PRIMARY_DECISION_MAKER = 1;

        public PortalBusiness(IDbContext aContext)
        {
            mContext = aContext;
        }

        public PortalData GetPortalData(Agent aUser)
        {
            var lOutput = new PortalData();
            var lOppDA = new OpportunityDataAccess(mContext);
            var lOpps = lOppDA.GetActiveOpportunities(aUser);
            lOpps = lOpps.Where(x => x.Stage != OpportunityStages.Won
            && x.Stage != OpportunityStages.Loss
            && x.Status == Opportunity.OpportunityStatus.Active).ToList();

            if (lOpps != null)
                lOutput.Opportunities = OpportunityUIHelper.Convert(mContext, SortOpps(lOpps));
            else
                lOutput.Opportunities = new List<UIOpportunity>();

            (lOutput.NewBookings, lOutput.WonBookings) = lOppDA.GetWeeklyBookings(aUser);

            var lTrips = new TripBusiness(mContext).GetActiveTrips(aUser).OrderBy(x=>x.StartDate).ToList();
            var lMacOpp = lTrips.Where(x => x.ID == 2402).FirstOrDefault();
            lOutput.Trips = TripUIHelper.Convert(lTrips);
            var lMacOpp1 = lOutput.Trips.Where(x => x.Id == 2402).FirstOrDefault();

            var lTasks = new TaskBusiness(mContext).GetMyTasks(aUser).Where(x => (x.HoldUntil == null || x.HoldUntil.Value < DateTime.Now) && x.Status != TaskStatusTypes.REVIEW).ToList();
            lTasks.AddRange (new TaskBusiness(mContext).GetReviewTasks(aUser));
            switch ( aUser.ViewMode)
            {
                case ViewModes.Delegated: 
                    lTasks = lTasks.Where(x => x.OwnerID != x.IssuerID).ToList();
                    break;
                case ViewModes.MyTasksOnly:
                    lTasks = lTasks.Where(x => x.OwnerID == aUser.Id || x.IssuerID == aUser.Id).ToList();
                    break;
                case ViewModes.Bugs:
                    lTasks = lTasks.Where(x => x.TaskType == TaskTypes.ISSUE).ToList();
                    break;
                case ViewModes.Completed:
                    lTasks = lTasks.Where(x => x.Status == TaskStatusTypes.COMPLETED).ToList();
                    break;
            }
            lOutput.Tasks = TaskUIHelper.Convert(lTasks, aUser);
            return lOutput;
        }

        private List<Opportunity> SortOpps(List<Opportunity> aOpps)
        {
            List<Opportunity> lOutput = new List<Opportunity>();
            lOutput.AddRange(Filter(aOpps, OpportunityStages.New));
            lOutput.AddRange(Filter(aOpps, OpportunityStages.QuoteSent));
            lOutput.AddRange(Filter(aOpps, OpportunityStages.Negotiations));
            lOutput.AddRange(Filter(aOpps, OpportunityStages.OnHold));
            return lOutput;
        }

        public List<Opportunity> Filter(List<Opportunity> aOpps, OpportunityStages aStage)
        {
            var lData = aOpps.Where(x => x.Stage == aStage);
            if (lData != null)
                return lData.OrderBy(x => x.CreatedOn).ToList();
            return new List<Opportunity>();
        }

        public UISearch Search(string aText, Agent aAgent )
        {
            var lOutput = new UISearch();
            lOutput.Contacts = ContactUIHelper.Convert( new ContactDataAccess(mContext).FindContacts(aText).Where(x=>x.Deleted == false).ToList());
            lOutput.Opportunities = OpportunityUIHelper.Convert(mContext, new OpportunityDataAccess(mContext).Find(aText, aAgent).ToList().Where(x => x.Stage != OpportunityStages.Won)).ToList();
            var lTrips = new TripDataAccess(mContext).Find(aText, aAgent);
            lOutput.Trips = TripUIHelper.Convert(lTrips).ToList();
            return lOutput;
        }
    }
}
