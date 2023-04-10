using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using BlitzerCore.Models;
using BlitzerCore.Utilities;

namespace BlitzerCore.DataAccess
{
    public class OpportunityDataAccess
    {
        const string ClassName = "OpportunityDataAccess::";
        IDbContext mContext = null;
        public OpportunityDataAccess(IDbContext aContext)
        {
            mContext = aContext;
        }
        public Opportunity Save(Opportunity aOpportunity, bool aCommit = true)
        {
            if (aOpportunity.Stage == 0)
                throw new System.Data.InvalidConstraintException("Opportunity requires a Valid Stage");

            // Want to avoid inserting duplicate AppUsers
            foreach (var lTraveler in aOpportunity.Travelers)
            {
                if (lTraveler == null)
                    continue;

                //if (lTraveler.UserID != null)
                //    lTraveler.User = null;
            }
            if ( aOpportunity.ID == 0 )
                mContext.Opportunities.Add(aOpportunity);
            else
                mContext.Opportunities.Update(aOpportunity);

            if (aCommit == false)
                return aOpportunity;

            try
            {
                var lCnt = mContext.SaveChanges();
                Logger.LogInfo(ClassName + $"Save - Updated {lCnt} records");
            } catch ( Exception e )
            {
                Logger.LogException(ClassName + "Save - Failed to save Opportunity", e);
            }

            return aOpportunity;
        }

        public  (int, int) GetWeeklyBookings(Agent aAgent )
        {

            DateTime baseDate = DateTime.Today;

            var today = baseDate;
            var thisWeekStart = baseDate.AddDays(-(int)baseDate.DayOfWeek);
            var thisWeekEnd = thisWeekStart.AddDays(7).AddSeconds(-1);

            var lOpps = GetActiveOpportunities(aAgent);

            var lNew = lOpps.Count(x => x.CreatedOn > thisWeekStart && x.CreatedOn < thisWeekEnd);
            var lWon = lOpps.Count(x => x.OppClosedDate > thisWeekStart && x.OppClosedDate < thisWeekEnd && x.Stage == OpportunityStages.Won);

            return (lNew, lWon);
        }

        public Opportunity Get ( int aOppID)
        {
            return mContext.Opportunities
                //.Include(opp => opp.Stage)
                .Include(opp => opp.InboundAirPort)
                .Include(opp=> opp.OutboundAirPort)
                .Include(opp => opp.Travelers).ThenInclude(sub => sub.User.Emails)
                .Include(opp => opp.Travelers).ThenInclude(sub => sub.User.PhoneNumbers)
                .Include(opp => opp.Travelers).ThenInclude(sub => sub.User.HouseHold)
                .Include(opp => opp.Agent).ThenInclude(sub=> sub.Emails)
                .Include(opp => opp.Agent).ThenInclude(sub => sub.PhoneNumbers)
                .Include(opp => opp.QuoteRequests).ThenInclude(sub=>sub.QuoteGroups)
                .Include(opp => opp.NoteEntries).ThenInclude(x1 => x1.Contact)
                .Include(opp => opp.NoteEntries).ThenInclude(x1 => x1.Writer)
                .Include(opp => opp.NoteEntries).ThenInclude(x1 => x1.Company)
                .Include(opp => opp.Tasks)
                .Include(opp => opp.Files).ThenInclude(x1=>x1.FileType)
                .Where(x => x.ID == aOppID)
                .FirstOrDefault();
        }

        internal IEnumerable<Opportunity> Find(string aText, Agent aOwner)
        {
            var lSW = Logger.StartStopWatch();
            var lSW2 = Logger.StartStopWatch();
            // get Team ID 
            try
            {
                var lTeamMemberIDs = mContext.TeamMembers.Where(i => mContext.TeamMembers.Where(u => u.MemberId == aOwner.Id).Select(y => y.TeamId).Contains(i.TeamId)).Select(t => t.MemberId).ToList();
                Logger.StopStopWatch(lSW, ClassName + " Pulling Team");

                if (lTeamMemberIDs.Count > 0)
                    return mContext.Opportunities
                        .Include(opp => opp.InboundAirPort)
                        .Include(opp => opp.OutboundAirPort)
                        .Include(opp => opp.Travelers).ThenInclude(sub => sub.User).ThenInclude(sub1 => sub1.Emails)
                        .Include(opp => opp.Travelers).ThenInclude(sub => sub.User).ThenInclude(sub1 => sub1.PhoneNumbers)
                        .Include(opp => opp.Travelers).ThenInclude(sub => sub.User).ThenInclude(sub1 => sub1.HouseHold)
                        .Include(opp => opp.Agent).ThenInclude(sub => sub.Emails)
                        .Include(opp => opp.Agent).ThenInclude(sub => sub.PhoneNumbers)
                        .Where(opp => lTeamMemberIDs.Contains(opp.AgentId) && opp.Name.Contains(aText));
                else
                    return mContext.Opportunities
                        .Include(opp => opp.InboundAirPort)
                        .Include(opp => opp.OutboundAirPort)
                        .Include(opp => opp.Travelers).ThenInclude(sub => sub.User).ThenInclude(sub1 => sub1.Emails)
                        .Include(opp => opp.Travelers).ThenInclude(sub => sub.User).ThenInclude(sub1 => sub1.PhoneNumbers)
                        .Include(opp => opp.Travelers).ThenInclude(sub => sub.User).ThenInclude(sub1 => sub1.HouseHold)
                        .Include(opp => opp.Agent).ThenInclude(sub => sub.Emails)
                        .Include(opp => opp.Agent).ThenInclude(sub => sub.PhoneNumbers)
                        .Where(opp => opp.AgentId == aOwner.Id && opp.Name.Contains(aText));
            }
            finally
            {
                Logger.StopStopWatch(lSW, ClassName + "GetActiveOpportunities(Agent)");
            }
        }

        public List<Opportunity> Get(Contact aContact)
        {
            return (from O in mContext.Opportunities
                    join U in mContext.UserMaps on O.ID equals U.OpportunityID
                    where U.User.Id == aContact.Id
                    && O.Stage != OpportunityStages.Loss && O.Stage != OpportunityStages.Won
                    select O).ToList();
        }

        public List<Opportunity> GetActiveOpportunities (Contact aUser)
        {
            return mContext.Opportunities
                .Include(opp => opp.InboundAirPort)
                .Include(opp => opp.OutboundAirPort)
                .Include(opp => opp.Travelers).ThenInclude(sub => sub.User).ThenInclude(sub1 => sub1.Emails)
                .Include(opp => opp.Travelers).ThenInclude(sub => sub.User).ThenInclude(sub1 => sub1.PhoneNumbers)
                .Include(opp => opp.Travelers).ThenInclude(sub => sub.User).ThenInclude(sub1 => sub1.HouseHold)
                .Include(opp => opp.Agent).ThenInclude(sub => sub.Emails)
                .Include(opp => opp.Agent).ThenInclude(sub => sub.PhoneNumbers)
                .Where(opp => opp.Stage != OpportunityStages.Won && opp.Stage != OpportunityStages.Loss)
                .SelectMany(x => x.Travelers).Where(y => y.User.Id == aUser.Id )
                .Select(x => x.Opportunity).ToList();
        }

        public List<Opportunity> GetInActiveOpportunities(Contact aUser)
        {
            var lOutput = mContext.Opportunities
                .Include(opp => opp.InboundAirPort)
                .Include(opp => opp.OutboundAirPort)
                .Include(opp => opp.Travelers).ThenInclude(sub => sub.User).ThenInclude(sub1 => sub1.Emails)
                .Include(opp => opp.Travelers).ThenInclude(sub => sub.User).ThenInclude(sub1 => sub1.PhoneNumbers)
                .Include(opp => opp.Travelers).ThenInclude(sub => sub.User).ThenInclude(sub1 => sub1.HouseHold)
                .Include(opp => opp.Agent).ThenInclude(sub => sub.Emails)
                .Include(opp => opp.Agent).ThenInclude(sub => sub.PhoneNumbers)
                .Where(opp => opp.Stage == OpportunityStages.Loss)
                .SelectMany(x => x.Travelers).Where(y => y.User.Id == aUser.Id)
                .Select(x => x.Opportunity).ToList();
            return lOutput;
        }

        public List<Opportunity> GetActiveOpportunities(Agent aOwner)
        {
            var lSW = Logger.StartStopWatch();
            var lSW2 = Logger.StartStopWatch();
            // get Team ID 
            try
            {
                var lTeamMemberIDs = mContext.TeamMembers.Where(i => mContext.TeamMembers.Where(u => u.MemberId == aOwner.Id).Select(y => y.TeamId).Contains(i.TeamId)).Select(t => t.MemberId).ToList();
                Logger.StopStopWatch(lSW, ClassName + " Pulling Team");

                if ( lTeamMemberIDs.Count > 0 )
                return mContext.Opportunities
                    .Include(opp => opp.InboundAirPort)
                    .Include(opp => opp.OutboundAirPort)
                    .Include(opp => opp.Travelers).ThenInclude(sub => sub.User).ThenInclude(sub1 => sub1.Emails)
                    .Include(opp => opp.Travelers).ThenInclude(sub => sub.User).ThenInclude(sub1 => sub1.PhoneNumbers)
                    .Include(opp => opp.Travelers).ThenInclude(sub => sub.User).ThenInclude(sub1 => sub1.HouseHold)
                    .Include(opp => opp.Agent).ThenInclude(sub => sub.Emails)
                    .Include(opp => opp.Agent).ThenInclude(sub => sub.PhoneNumbers)
                    .Include(opp => opp.QuoteRequests)
                    .Where(opp => lTeamMemberIDs.Contains(opp.AgentId) && opp.Stage != OpportunityStages.Loss && opp.Stage != OpportunityStages.Won)
                    .ToList();
                else
                    return mContext.Opportunities
                        .Include(opp => opp.InboundAirPort)
                        .Include(opp => opp.OutboundAirPort)
                        .Include(opp => opp.Travelers).ThenInclude(sub => sub.User).ThenInclude(sub1 => sub1.Emails)
                        .Include(opp => opp.Travelers).ThenInclude(sub => sub.User).ThenInclude(sub1 => sub1.PhoneNumbers)
                        .Include(opp => opp.Travelers).ThenInclude(sub => sub.User).ThenInclude(sub1 => sub1.HouseHold)
                        .Include(opp => opp.Agent).ThenInclude(sub => sub.Emails)
                        .Include(opp => opp.Agent).ThenInclude(sub => sub.PhoneNumbers)
                        .Include(opp => opp.QuoteRequests)
                        .Where(opp => opp.AgentId == aOwner.Id && opp.Stage != OpportunityStages.Loss && opp.Stage != OpportunityStages.Won)
                        .ToList();
            }
            finally
            {
                Logger.StopStopWatch(lSW, ClassName + "GetActiveOpportunities(Agent)");
            }
        }

        /// <summary>
        /// Find an active opportunity
        /// </summary>
        /// <param name="aPrimeEmail"></param>
        /// <param name="aStart"></param>
        /// <param name="aEnd"></param>
        /// <returns></returns>
        public Opportunity Find(string aPrimeEmail, DateTime aStart, DateTime aEnd)
        {
            var lClientID = mContext.Emails.Where(x => x.Address == aPrimeEmail).Select(x => x.UserId).FirstOrDefault();
            if (lClientID == null)
                return null;

            return (from O in mContext.Opportunities
                join U in mContext.UserMaps on O.ID equals U.OpportunityID
                join E in mContext.Emails on U.UserID equals E.UserId
                    where E.Preferred == true && E.Address == aPrimeEmail && O.Status == Opportunity.OpportunityStatus.Active
                    && O.Stage != OpportunityStages.Loss && O.Stage != OpportunityStages.Won
                    select O).FirstOrDefault();
        }

        internal List<Opportunity> GetAll()
        {
            return mContext.Opportunities
                //.Include(opp => opp.Stage)
                .Include(opp => opp.InboundAirPort)
                .Include(opp => opp.OutboundAirPort)
                .Include(opp => opp.Travelers).ThenInclude(sub => sub.User)
                .Include(opp => opp.Travelers).ThenInclude(sub => sub.User.Emails)
                .Include(opp => opp.Travelers).ThenInclude(sub => sub.User.PhoneNumbers)
                .Include(opp => opp.Travelers).ThenInclude(sub => sub.User.HouseHold)
                .Include(opp => opp.Agent).ThenInclude(sub => sub.Emails)
                .Include(opp => opp.Agent).ThenInclude(sub => sub.PhoneNumbers).ToList();
        }

        internal void Delete(Opportunity aOpp)
        {
            mContext.Opportunities.Remove(aOpp);
        }
    }
}
