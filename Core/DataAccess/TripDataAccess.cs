using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlitzerCore.Business;
using Microsoft.EntityFrameworkCore;
using BlitzerCore.Utilities;
using BlitzerCore.Models;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Microsoft.Net.Http.Headers;

namespace BlitzerCore.DataAccess
{
    public class TripDataAccess
    {
        const string ClassName = "TripDataAccess::";
        IDbContext DbContext { get; set; }

        public TripDataAccess(IDbContext aContext)
        {
            DbContext = aContext;
        }

        public int Save(Trip aTrip, bool aCommit = true)
        {
            string FuncName = $"{ClassName}Save (Trip = {aTrip.ID})";
            try
            {
                if (aTrip.ID == 0)
                    DbContext.Trips.Add(aTrip);
                else
                    DbContext.Trips.Update(aTrip);

                if (aCommit)
                {
                    var lCount = DbContext.SaveChanges();
                    Logger.LogInfo($"{FuncName} Updated {lCount} trip records");
                    return lCount;
                }

                return 0;
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to save Trip {aTrip.Name}", e);
                return 0;
            }
        }
        public IEnumerable<Trip> GetAll()
        {
            return DbContext.Trips;

        }
        public Trip Get(int aID)
        {
            return DbContext.Trips
                .Include(x => x.Bookings).ThenInclude(sub1 => sub1.TourOperator)
                .Include(x => x.Bookings).ThenInclude(sub1 => sub1.Credits)
                .Include(x => x.Bookings).ThenInclude(sub1 => sub1.Supplier)
                .Include(x => x.Travelers).ThenInclude(y => y.User).ThenInclude(sub => sub.Emails)
                .Include(x => x.Bookings).ThenInclude(sub1 => sub1.Payments).ThenInclude(x2 => x2.Payee)
                .Include(x => x.Bookings).ThenInclude(sub1 => sub1.Credits).ThenInclude(sub2 => sub2.Traveler)
                .Include(x => x.NoteEntries).ThenInclude(x1 => x1.Contact)
                .Include(x => x.NoteEntries).ThenInclude(x1 => x1.Writer)
                .Include(x => x.NoteEntries).ThenInclude(x1 => x1.Company)
                .Include(x => x.Tasks)
                .Include(x => x.Agent)
                .Include(x=>x.Files).ThenInclude(x1=>x1.FileType)
                .Where(x => x.ID == aID)
                .FirstOrDefault();

        }

        internal List<Trip> Find(string aText, Agent aAgent)
        {
            // get Team ID 
            var lSW = Logger.StartStopWatch();
            try
            {
                if (aText == null)
                    return new List<Trip>();

                var lTeamMemberIDs = DbContext.TeamMembers.Where(i => DbContext.TeamMembers.Where(u => u.MemberId == aAgent.Id).Select(y => y.TeamId).Contains(i.TeamId)).Select(t => t.MemberId).ToList();

                if (lTeamMemberIDs.Count > 0)
                    return DbContext.Trips
                        //.Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.Payments)
                        //.Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.TourOperator)
                        //.Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.Credits)
                        //.Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.Supplier)
                        //.Include(opp => opp.InboundAirPort)
                        //.Include(opp => opp.OutboundAirPort)
                        .Include(opp => opp.Travelers).ThenInclude(sub => sub.User.Emails)
                        .Include(opp => opp.Travelers).ThenInclude(sub => sub.User.PhoneNumbers)
                        .Include(opp => opp.Travelers).ThenInclude(sub => sub.User.HouseHold)
                        .Include(opp => opp.Agent).ThenInclude(sub => sub.Emails)
                        .Include(opp => opp.Agent).ThenInclude(sub => sub.PhoneNumbers)
                        .Where(x => lTeamMemberIDs.Contains(x.AgentId) && x.Name.ToUpper().Contains(aText.ToUpper())).ToList();
                else
                    return DbContext.Trips
                        .Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.Payments)
                        .Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.TourOperator)
                        .Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.Credits)
                        .Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.Supplier)
                        .Include(opp => opp.InboundAirPort)
                        .Include(opp => opp.OutboundAirPort)
                        .Include(opp => opp.Travelers).ThenInclude(sub => sub.User.Emails)
                        .Include(opp => opp.Travelers).ThenInclude(sub => sub.User.PhoneNumbers)
                        .Include(opp => opp.Travelers).ThenInclude(sub => sub.User.HouseHold)
                        .Include(opp => opp.Agent).ThenInclude(sub => sub.Emails)
                        .Include(opp => opp.Agent).ThenInclude(sub => sub.PhoneNumbers)
                        .Where(x => x.AgentId == aAgent.Id && x.Name.Contains(aText)).ToList();
            }
            finally
            {
                Logger.StopStopWatch(lSW, ClassName + $"Get(Agent={aAgent.Id})");
            }
        }

        public List<Trip> Get(Contact aUser)
        {
            var lOppIDs = DbContext.Trips
                .SelectMany(x => x.Travelers).Where(y => y.User.Id == aUser.Id)
                .Select(x => x.OpportunityID);

            return DbContext.Trips
                .Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.Payments)
                .Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.Credits)
                .Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.TourOperator)
                .Include(opp => opp.InboundAirPort)
                .Include(opp => opp.OutboundAirPort)
                .Include(opp => opp.Travelers).ThenInclude(sub => sub.User.Emails)
                .Include(opp => opp.Travelers).ThenInclude(sub => sub.User.PhoneNumbers)
                .Include(opp => opp.Travelers).ThenInclude(sub => sub.User.HouseHold)
                .Include(opp => opp.Agent).ThenInclude(sub => sub.Emails)
                .Include(opp => opp.Agent).ThenInclude(sub => sub.PhoneNumbers)
                .Where(x => lOppIDs.Contains(x.ID)).ToList();
        }
        public List<Trip> Get(Agent aAgent)
        {
            List<Trip> lOutput = null;
            // get Team ID 
            var lSW = Logger.StartStopWatch();
            try
            {
                var lTeamMemberIDs = DbContext.TeamMembers.Where(i => DbContext.TeamMembers.Where(u => u.MemberId == aAgent.Id).Select(y => y.TeamId).Contains(i.TeamId)).Select(t => t.MemberId).ToList();

                if (lTeamMemberIDs.Count > 0)
                    lOutput = DbContext.Trips
                        .Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.Payments)
                        .Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.TourOperator)
                        .Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.Credits)
                        .Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.Supplier)
                        .Include(opp => opp.InboundAirPort)
                        .Include(opp => opp.OutboundAirPort)
                        .Include(opp => opp.Travelers).ThenInclude(sub => sub.User.Emails)
                        .Include(opp => opp.Travelers).ThenInclude(sub => sub.User.PhoneNumbers)
                        .Include(opp => opp.Travelers).ThenInclude(sub => sub.User.HouseHold)
                        .Include(opp => opp.Agent).ThenInclude(sub => sub.Emails)
                        .Include(opp => opp.Agent).ThenInclude(sub => sub.PhoneNumbers)
                        .Where(x => lTeamMemberIDs.Contains(x.AgentId)).ToList();
                else
                    lOutput = DbContext.Trips
                        .Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.Payments)
                        .Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.TourOperator)
                        .Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.Credits)
                        .Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.Supplier)
                        .Include(opp => opp.InboundAirPort)
                        .Include(opp => opp.OutboundAirPort)
                        .Include(opp => opp.Travelers).ThenInclude(sub => sub.User.Emails)
                        .Include(opp => opp.Travelers).ThenInclude(sub => sub.User.PhoneNumbers)
                        .Include(opp => opp.Travelers).ThenInclude(sub => sub.User.HouseHold)
                        .Include(opp => opp.Agent).ThenInclude(sub => sub.Emails)
                        .Include(opp => opp.Agent).ThenInclude(sub => sub.PhoneNumbers)
                        .Where(x => x.AgentId == aAgent.Id).ToList();
            }
            finally
            {
                Logger.StopStopWatch(lSW, ClassName + $"Get(Agent={aAgent.Id})");
            }

            var lOpp = lOutput.Where(x => x.ID == 2402).FirstOrDefault();
            return lOutput;

        }

        internal List<Trip> GetActiveTrips()
        {
            string FuncName = $"{ClassName}GetActiveTrips ()";

            var lSW = Logger.StartStopWatch();
            try
            {
                Logger.EnterFunction(FuncName);

                return DbContext.Trips
                    .Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.Payments)
                    .Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.Credits)
                    .Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.TourOperator)
                    .Include(opp => opp.InboundAirPort)
                    .Include(opp => opp.OutboundAirPort)
                    .Include(opp=> opp.Tasks)
                    .Include(opp => opp.Travelers).ThenInclude(sub => sub.User.Emails)
                    .Include(opp => opp.Travelers).ThenInclude(sub => sub.User.PhoneNumbers)
                    .Include(opp => opp.Travelers).ThenInclude(sub => sub.User.HouseHold)
                    .Include(opp => opp.Agent).ThenInclude(sub => sub.Emails)
                    .Include(opp => opp.Agent).ThenInclude(sub => sub.PhoneNumbers)
                    .Where(x => x.TripStage != TripStage.Traveled && x.TripStatus != Trip.Statuses.Deleted && x.TripStatus != Trip.Statuses.Cancelled && x.EndDate > DateTime.Now).ToList();
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }

        public List<Trip> GetActiveTrips(Contact aContact)
        {
            string FuncName = $"{ClassName}GetActiveTrips (Contact = {aContact.Id})";

            //List<Trip> lOutput = null;
            // get Team ID 
            var lSW = Logger.StartStopWatch();
            try
            {
                Logger.EnterFunction(FuncName);

                var lOppIDs = DbContext.Trips
                    .SelectMany(x => x.Travelers).Where(y => y.User.Id == aContact.Id)
                    .Select(x => x.OpportunityID);

                return DbContext.Trips
                    .Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.Payments)
                    .Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.Credits)
                    .Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.TourOperator)
                    .Include(opp => opp.InboundAirPort)
                    .Include(opp => opp.OutboundAirPort)
                    .Include(opp => opp.Travelers).ThenInclude(sub => sub.User.Emails)
                    .Include(opp => opp.Travelers).ThenInclude(sub => sub.User.PhoneNumbers)
                    .Include(opp => opp.Travelers).ThenInclude(sub => sub.User.HouseHold)
                    .Include(opp => opp.Agent).ThenInclude(sub => sub.Emails)
                    .Include(opp => opp.Agent).ThenInclude(sub => sub.PhoneNumbers)
                    .Where(x => lOppIDs.Contains(x.ID) && x.TripStage != TripStage.Traveled && x.Stage == OpportunityStages.Won && x.TripStatus != Trip.Statuses.Deleted && x.TripStatus != Trip.Statuses.Cancelled && x.EndDate > DateTime.Now).ToList();
            }
            finally
            {
                Logger.StopStopWatch(lSW, ClassName + $"Get(Agent={aContact.Id})");
                Logger.LeaveFunction(FuncName);
            }
        }


        internal List<Trip> GetActiveTrips(Agent aAgent)
        {
            string FuncName = $"{ClassName}GetActiveTrips (Agent = {aAgent.Id})";

            List<Trip> lOutput = null;
            // get Team ID 
            var lSW = Logger.StartStopWatch();
            try
            {
                Logger.EnterFunction(FuncName);
                var lTeamMemberIDs = DbContext.TeamMembers.Where(i => DbContext.TeamMembers.Where(u => u.MemberId == aAgent.Id).Select(y => y.TeamId).Contains(i.TeamId)).Select(t => t.MemberId).ToList();

                if (lTeamMemberIDs.Count > 0)
                    lOutput = DbContext.Trips
                        .Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.Payments).ThenInclude(sub2=>sub2.Card)
                        .Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.TourOperator)
                        .Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.Credits)
                        .Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.Supplier)
                        .Include(opp => opp.InboundAirPort)
                        .Include(opp => opp.OutboundAirPort)
                        .Include(opp => opp.Travelers).ThenInclude(sub => sub.User.Emails)
                        .Include(opp => opp.Travelers).ThenInclude(sub => sub.User.PhoneNumbers)
                        .Include(opp => opp.Travelers).ThenInclude(sub => sub.User.HouseHold)
                        .Include(opp => opp.Agent).ThenInclude(sub => sub.Emails)
                        .Include(opp => opp.Agent).ThenInclude(sub => sub.PhoneNumbers)
                        .Where(x => lTeamMemberIDs.Contains(x.AgentId) && x.TripStage != TripStage.Traveled && x.TripStatus != Trip.Statuses.Deleted && x.TripStatus != Trip.Statuses.Cancelled && x.EndDate > DateTime.Now).ToList();
                else
                    lOutput = DbContext.Trips
                        .Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.Payments)
                        .Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.TourOperator)
                        .Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.Credits)
                        .Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.Supplier)
                        .Include(opp => opp.InboundAirPort)
                        .Include(opp => opp.OutboundAirPort)
                        .Include(opp => opp.Travelers).ThenInclude(sub => sub.User.Emails)
                        .Include(opp => opp.Travelers).ThenInclude(sub => sub.User.PhoneNumbers)
                        .Include(opp => opp.Travelers).ThenInclude(sub => sub.User.HouseHold)
                        .Include(opp => opp.Agent).ThenInclude(sub => sub.Emails)
                        .Include(opp => opp.Agent).ThenInclude(sub => sub.PhoneNumbers)
                        .Where(x => x.AgentId == aAgent.Id && x.TripStage != TripStage.Traveled && x.TripStatus != Trip.Statuses.Deleted && x.TripStatus != Trip.Statuses.Cancelled && x.EndDate > DateTime.Now).ToList();
            }
            finally
            {
                Logger.StopStopWatch(lSW, ClassName + $"Get(Agent={aAgent.Id})");
                Logger.LeaveFunction(FuncName);
            }

            var lOpp = lOutput.Where(x => x.ID == 2402).FirstOrDefault();
            return lOutput;
        }

        public List<Trip> GetAll(Agent aAgent)
        {
            var lTeamMemberIDs = DbContext.TeamMembers.Where(i => DbContext.TeamMembers.Where(u => u.MemberId == aAgent.Id).Select(y => y.TeamId).Contains(i.TeamId)).Select(t => t.MemberId).ToList();

            if (lTeamMemberIDs.Count > 0)
                return DbContext.Trips
                .Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.Payments)
                .Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.TourOperator)
                .Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.Supplier)
                .Include(opp => opp.InboundAirPort)
                .Include(opp => opp.OutboundAirPort)
                .Where(x => lTeamMemberIDs.Contains(x.AgentId)).ToList();
            else
                return DbContext.Trips
                .Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.Payments)
                .Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.TourOperator)
                .Include(opp => opp.Bookings).ThenInclude(sub1 => sub1.Supplier)
                .Include(opp => opp.InboundAirPort)
                .Include(opp => opp.OutboundAirPort)
                .Where(opp => opp.AgentId == aAgent.Id).ToList();
        }

        public Opportunity ConvertToTrip(int aOpportunityId)
        {
            string FuncName = $"{ClassName}ConvertToTrip (Opportunity = {aOpportunityId})";
            string lErrMsg = $"{FuncName} - Failed to conver opportunity {aOpportunityId} to a Trip";
            try
            {
                Logger.EnterFunction(FuncName);

                var lOpp = new OpportunityDataAccess(DbContext).Get(aOpportunityId);

                var lTrip = new Trip()
                {
                    Agent = lOpp.Agent,
                    Activity = lOpp.Activity,
                    Name = lOpp.Name,
                    Status = lOpp.Status,
                    ID = lOpp.ID,
                    AgentId = lOpp.AgentId,
                    InboundAirPort = lOpp.InboundAirPort,
                    InboundAirPortID = lOpp.InboundAirPortID,
                    OutboundAirPort = lOpp.OutboundAirPort,
                    OutboundAirPortID = lOpp.OutboundAirPortID,
                    Stage = lOpp.Stage,
                    QuoteRequests = lOpp.QuoteRequests,
                    StartDate = lOpp.StartDate,
                    EndDate = lOpp.EndDate,
                    CreatedBy = lOpp.CreatedBy,
                    CreatedOn = lOpp.CreatedOn
                };

                if (System.IO.Directory.GetCurrentDirectory().Contains("NUnit") == false)
                    DbContext.ExecCommand($"Exec ConvertToTrip {lOpp.ID}");
                else
                {
                    DbContext.Opportunities.Remove(lOpp);
                    DbContext.SaveChanges();
                    DbContext.Trips.Add(lTrip);
                }

                DbContext.SaveChanges();
                return lOpp;
            }
            catch (Exception e)
            {
                Logger.LogException(lErrMsg, e);
                return null;
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }
    }
}
