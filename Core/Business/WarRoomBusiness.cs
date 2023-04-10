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
using BlitzerCore.Utilities;
using Microsoft.Extensions.Configuration;

namespace BlitzerCore.Business
{
    public class WarRoomBusiness
    {
        private IDbContext DbContext { get; set; }
        public IConfiguration Configuration { get; }

        const string ClassName = "WarRoomBusiness::";
        const string CSS_NOCHANGE = "NoChange";
        const string CSS_GAIN = "Gain";
        const string CSS_LOSS = "Loss";

        DateTime YearStart => new DateTime(DateTime.Now.Year, 1, 1);
        DateTime YearEnd => new DateTime(DateTime.Now.Year, 12, 31);

        const int Facebook = 1;
        const int Instagram = 2;
        const int Youtube = 3;
        const int Website = 4;
        const int Google = 5;
        const int Friend = 6;
        const int Existing = 7;

        public WarRoomBusiness(IDbContext mContext, IConfiguration aConfiguration)
        {
            this.DbContext = mContext;
            Configuration = aConfiguration;
        }
        public UIWarRoom Populate(Agent aAgent)
        {
            var lOutput = new UIWarRoom();

            var lOppDA = new OpportunityDataAccess(DbContext);
            var lOpps = lOppDA.GetActiveOpportunities(aAgent);
            lOutput.Opportunities = OpportunityUIHelper.Convert(DbContext, lOpps.Where(x => x.Stage != OpportunityStages.Won
            && x.Stage != OpportunityStages.Loss
            && x.Status == Opportunity.OpportunityStatus.Active).OrderBy(x => x.StartDate)
                .ToList()); ;
            var lTasks = new TaskBusiness(DbContext).GetMyTasks(aAgent);
            var lFollowUp = lTasks.Where(x => x.TaskType == TaskTypes.FollowUp);
            var lIssues = lTasks.Where(x => x.TaskType == TaskTypes.ISSUE);
            var lMyTasks = lTasks.Where(x => x.TaskType == TaskTypes.WORK && x.OwnerID == aAgent.Id);
            lOutput.FollowUps = TaskUIHelper.Convert(lFollowUp, aAgent);
            lOutput.Issues = TaskUIHelper.Convert(lIssues, aAgent);
            lOutput.Tasks = TaskUIHelper.Convert(lMyTasks, aAgent);
            lOutput.Notes = NoteUIHelper.Convert( new NotesDataAccess(DbContext).Get());

            var lFinances = new FinancialBusiness(DbContext, Configuration).Get(aAgent);
            lOutput.YTDSales = lFinances.Sales_YTD;
            lOutput.YTDComm = lFinances.Unrealized_YTD;
            lOutput.MonthlyP_L = lFinances.PL_MTD;
            if (lOutput.MonthlyP_L.Contains("("))
                lOutput.PLCSS = CSS_LOSS;
            else
                lOutput.PLCSS = CSS_GAIN;

            lOutput.Sales = Get(aAgent);

            var lData = new BookingBusiness(DbContext, Configuration).Get(aAgent).Where(x => x.Trip.EndDate > YearStart && x.Trip.EndDate < YearEnd);
            string lCSS = "";
            lOutput.Existing = GetDelta(Existing, lData, out lCSS);
            lOutput.ExistingCSS = lCSS;
            lOutput.Facebook = GetDelta(Facebook, lData, out lCSS);
            lOutput.FacebookCSS = lCSS;
            lOutput.Website = GetDelta(Website, lData, out lCSS);
            lOutput.WebsiteCSS = lCSS;
            lOutput.Instagram = GetDelta(Instagram, lData, out lCSS);
            lOutput.InstagramCSS = lCSS;
            lOutput.Friend = GetDelta(Friend, lData, out lCSS);
            lOutput.FriendCSS = lCSS;
            lOutput.Google = GetDelta(Google, lData, out lCSS);
            lOutput.GoogleCSS = lCSS;
            return lOutput;

        }
        private string GetDelta(int aRefferalType, IEnumerable<Booking> aData, out string aCSS)
        {
            DateTime now = DateTime.Now;
            var lCStart = now.AddDays(-30);
            var lCEnd = now;

            var lPStart = now.AddDays(-61);
            var lPEnd = lCStart.AddMonths(-31);


            var ThisMonth = aData.Where(x => x.Trip.CreatedOn >= lCStart && x.Trip.CreatedOn <= lCEnd && x.Trip.ReferralId == aRefferalType).Count();
            var LastMonth = aData.Where(x => x.Trip.CreatedOn >= lPStart && x.Trip.CreatedOn <= lPEnd && x.Trip.ReferralId == aRefferalType).Count();

            if (ThisMonth == 0 && LastMonth == 0)
            {
                aCSS = CSS_NOCHANGE;
                return "No Change";
            }
            if (LastMonth == 0)
            {
                aCSS = CSS_GAIN;
                return $"{Math.Round((double)ThisMonth * 100, 2)}%";
            }

            var lRetVal = Math.Round(((double)(ThisMonth - LastMonth) / LastMonth * 100), 2);
            if (lRetVal > 0)
                aCSS = CSS_GAIN;
            else
                aCSS = CSS_LOSS;

            return $"{lRetVal}%";
        }


        public List<SalesData> Get(Agent aAgent)
        {
            var lOutput = new List<SalesData>();
            var lData = new BookingBusiness(DbContext, Configuration).Get(aAgent).Where(x => x.Trip.EndDate > YearStart && x.Trip.EndDate < YearEnd);
            lOutput.Add( GetData(1, lData));
            lOutput.Add(GetData(2, lData));
            lOutput.Add(GetData(3, lData));
            lOutput.Add(GetData(4, lData));
            lOutput.Add(GetData(5, lData));
            lOutput.Add(GetData(6, lData));
            lOutput.Add(GetData(7, lData));
            lOutput.Add(GetData(8, lData));
            lOutput.Add(GetData(9, lData));
            lOutput.Add(GetData(10, lData));
            lOutput.Add(GetData(11, lData));
            lOutput.Add(GetData(12, lData));
            return lOutput;
        }

        private SalesData GetData(int aMonth, IEnumerable<Booking> aData)
        {
            List<string> Months = new List<string>()
            {
                "", "January", "February", "March", "April", "May", "June",
                "July", "August", "September", "October", "November", "December"
            };

            var lOutput = new SalesData();
            DateTime now = DateTime.Now;
            var lStart = new DateTime(now.Year, aMonth, 1);
            var lEnd = lStart.AddMonths(1).AddDays(-1);
            var lData = aData.Where(x => x.Trip.EndDate >= lStart && x.Trip.EndDate <= lEnd);
            lOutput.Month = Months[aMonth];
            lOutput.Existing = lData.Where(x => x.Trip.ReferralId == Existing).Sum(y => y.Amount);
            lOutput.Facebook = lData.Where(x => x.Trip.ReferralId == Facebook).Sum(y => y.Amount);
            lOutput.Instagram = lData.Where(x => x.Trip.ReferralId == Instagram).Sum(y => y.Amount);
            lOutput.Youtube = lData.Where(x => x.Trip.ReferralId == Youtube).Sum(y => y.Amount);
            lOutput.Website = lData.Where(x => x.Trip.ReferralId == Website).Sum(y => y.Amount);
            lOutput.Google = lData.Where(x => x.Trip.ReferralId == Google).Sum(y => y.Amount);
            lOutput.Friend = lData.Where(x => x.Trip.ReferralId == Friend).Sum(y => y.Amount);
            lOutput.Commission = lData.Sum(y => y.ICCommission);
            return lOutput;
        }
    }
}
