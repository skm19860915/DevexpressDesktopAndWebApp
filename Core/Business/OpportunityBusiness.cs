using System;
using System.Collections.Generic;
using System.Linq;
using BlitzerCore.Helpers;
using BlitzerCore.Models;
using BlitzerCore.Utilities;
using BlitzerCore.DataAccess;
using BlitzerCore.UIHelpers;

using System.Security.Cryptography.X509Certificates;

namespace BlitzerCore.Business
{
    public class OpportunityBusiness
    {
        readonly static string ClassName = "OpportunityBusiness::";
        readonly IDbContext mContext;
        public const int PRIMARY_DECISION_MAKER = 1;

        public OpportunityBusiness(IDbContext aContext)
        {
            mContext = aContext;
        }

        public List<BlitzerCore.Models.UserMap> Convert(List<BlitzerCore.Models.UI.UIContact> aClients)
        {
            var lResults = new List<BlitzerCore.Models.UserMap>();
            BlitzerCore.Models.Client lNewClient = null;
            bool isFirst = true;

            foreach (var lClient in aClients)
            {
                try
                {
                    var lEmails = new List<Email>();
                    if (lClient.PrimaryEmail != null)
                        lEmails.Add(new Email() { Address = lClient.PrimaryEmail, EmailTypeID = Email.PERSONNEL_EMAIL });
                    lNewClient = new BlitzerCore.Models.Client()
                    {
                        First = lClient.First,
                        Last = lClient.Last,
                        Middle = lClient.Middle,
                        DOB = DateHelper.ConvertDate(lClient.DOB),
                        Emails = lEmails,
                        RelationshipID = lClient.RelationshipID

                    };
                    if (lClient.Cell != null)
                        lNewClient.PhoneNumbers = new List<Phone>() { new Phone() { PhoneNumber = lClient.Cell, Defaut = true, PhoneTypeID = (int)PhoneType.ShortCuts.Cell } };

                    lResults.Add(new UserMap() { User = lNewClient, Primary = isFirst });
                    isFirst = false;
                }
                catch (Exception e)
                {
                    Logger.LogException("Failed to Convert Client [" + lClient.Name + "] to part of Opportunity", e);
                    throw e;
                }
            }
            return lResults;
        }

        public void Lost(Opportunity aOpp)
        {
            var FuncName = ClassName + $"::Lost(OppId = {aOpp.ID}) - ";
            try
            {
                aOpp.Stage = OpportunityStages.Loss;
                aOpp.Status = Opportunity.OpportunityStatus.Inactive;
                if (aOpp.OppClosedDate == null || aOpp.OppClosedDate < DateTime.Now.AddYears(-2))
                    aOpp.OppClosedDate = DateTime.Now;
                new OpportunityDataAccess(mContext).Save(aOpp);
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Error marking opportunity as lost", e);
            }
        }

        public List<Models.Opportunity> GetOpportunities(Contact lContact)
        {
            return new OpportunityDataAccess(mContext).GetActiveOpportunities(lContact);
        }

        public List<Opportunity> GetOppsAndTrips(Agent lContact)
        {
            List<Opportunity> lOutput = GetActive(lContact);
            lOutput.AddRange(new TripBusiness(mContext).GetAll(lContact)
                .Where(x => x.TripStatus != Trip.Statuses.Cancelled
                && x.TripStatus != Trip.Statuses.Deleted
                && x.TripStatus != Trip.Statuses.Completed));
            return lOutput;
        }

        public List<Opportunity> GetActive(Agent aAgent)
        {
            var lOpportunityDA = new OpportunityDataAccess(mContext);
            return lOpportunityDA.GetActiveOpportunities(aAgent).Where(x => x.Stage != OpportunityStages.Won && x.Stage != OpportunityStages.Loss).OrderBy(x => x.Name).ToList();
        }

        public List<Opportunity> Get(Contact aContact)
        {
            var lOpportunityDA = new OpportunityDataAccess(mContext);
            return lOpportunityDA.GetActiveOpportunities(aContact).Where(x => x.Stage != OpportunityStages.Won && x.Stage != OpportunityStages.Loss).OrderBy(x => x.Name).ToList();
        }

        public List<Opportunity> GetInActive(Contact aContact)
        {
            var lOpportunityDA = new OpportunityDataAccess(mContext);
            return lOpportunityDA.GetInActiveOpportunities(aContact).OrderBy(x => x.Name).ToList();
        }
        public List<Opportunity> GetAll()
        {
            var lOpportunityDA = new OpportunityDataAccess(mContext);
            return lOpportunityDA.GetAll();
        }

        public Opportunity Find(Opportunity aTargetOpp)
        {
            string FuncName = ClassName + $"Find(Opportunity)";
            var lOpportunityDA = new OpportunityDataAccess(mContext);
            var lPrimary = aTargetOpp.Travelers.Where(x => x.Primary == true).Select(y => y.User).FirstOrDefault();
            if (lPrimary == null)
                return null;

            // Do not return a previous opp because the email addr was empty
            if (lPrimary.Emails == null
                || lPrimary.Emails.Count() == 0
                || lPrimary.Emails[0].Address == null
                || lPrimary.Emails[0].Address.Trim().Length == 0)
                return null;

            var lOpp = lOpportunityDA.Find(lPrimary.Emails[0].Address, aTargetOpp.StartDate, aTargetOpp.EndDate);
            if (lOpp != null)
            {
                // Do not merge opportunty of the existing opportunty is Lost or Won
                if (lOpp.Stage == OpportunityStages.Loss || lOpp.Stage == OpportunityStages.Won)
                {
                    return null;
                }
                Logger.LogWarning($"{FuncName} - FOUND existing opportunity matching Email {lPrimary.Emails[0].Address}, Start {aTargetOpp.StartDate} and End Dates{aTargetOpp.EndDate}");
            }
            else
                Logger.LogInfo($"{FuncName} - NO exising opportunity found");
            return lOpp;
        }
        public BlitzerCore.Models.Opportunity Save(BlitzerCore.Models.UI.UIOpportunity aUIOpportunity, Contact aUser, bool aCommit = true)
        {

            var lOpp = this.GetOpportunity(aUIOpportunity.Id);
            if (lOpp == null)
            {
                Convert(aUIOpportunity);
            }

            // Update the Notes and Status because that is the only thing that can be updated on the main screen
            lOpp.Notes = aUIOpportunity.Notes;
            return Save(lOpp, aUser, aCommit);
        }

        public int Save(Note aNote, Opportunity aOpp)
        {
            return new NoteBusiness(mContext).Save(aNote, aOpp);
        }

        internal static void UpdateTracking(Opportunity aOpportunity, Contact aUser)
        {
            if (aOpportunity.ID == 0)
            {
                aOpportunity.CreatedById = aUser.Id;
                aOpportunity.CreatedOn = DateTime.Now;
            }
            aOpportunity.UpdatedById = aUser.Id;
            aOpportunity.UpdatedOn = DateTime.Now;

        }

        public BlitzerCore.Models.Opportunity Save(BlitzerCore.Models.Opportunity aOpportunity, Contact aUser, bool aCommit = true)
        {
            aOpportunity.Name = GetDefaultName(aOpportunity);
            ProcessTravelers(aOpportunity);
            UpdateTracking(aOpportunity, aUser);
            new OpportunityDataAccess(mContext).Save(aOpportunity, aCommit);
            return aOpportunity;
        }

        public string GetDefaultName(Opportunity aOpportunity)
        {
            var lPart1OfName = "";
            List<string> Months = new List<string>()
            {
                "", "January", "February", "March", "April", "May", "June",
                "July", "August", "September", "October", "November", "December"
            };

            var lPrimaryUser = aOpportunity.Travelers.Where(x => x.Primary == true).Select(y => y.User).FirstOrDefault();
            if (lPrimaryUser == null)
                lPrimaryUser = aOpportunity.Travelers.Select(y => y.User).FirstOrDefault();

            var lDestinationboundAirPort = new AirPortDataAccess(mContext).Get(aOpportunity.InboundAirPortID);
            var lDestination = lDestinationboundAirPort.CountryName;
            if (lDestination == "US" || lDestination == "United States")
                if (lDestinationboundAirPort.City != null)
                    lDestination = lDestinationboundAirPort.City;
                else
                    lDestination = lDestinationboundAirPort.Name;


            if (lPrimaryUser != null)
            {
                lPart1OfName = lPrimaryUser.Last;
                if (lPart1OfName == null || lPart1OfName.Length == 0)
                    lPart1OfName = lPrimaryUser.First;
            }
            return lPart1OfName + " " + lDestination + " " + Months[aOpportunity.StartDate.Month] + " " + aOpportunity.StartDate.Year;
        }

        public BlitzerCore.Models.Opportunity GetOpportunity(int aOpportunityID)
        {
            return new OpportunityDataAccess(mContext).Get(aOpportunityID);
        }

        public BlitzerCore.Models.Opportunity GetOpportunity(Agent aAgent, int? aOpportunityID)
        {
            if (aOpportunityID == null || aOpportunityID < 1)
            {
                return CreateNewOpportunity(aAgent);
            }

            int lOppId = aOpportunityID.Value;
            return GetOpportunity(lOppId);
        }

        public BlitzerCore.Models.Opportunity Convert(BlitzerCore.Models.UI.UIOpportunity aOpportunity)
        {
            BlitzerCore.Models.Opportunity lOpp = new BlitzerCore.Models.Opportunity();
            var lTravelBiz = new TravelBusiness(mContext);
            lOpp.Stage = OpportunityStages.New;
            lOpp.StartDate = BlitzerCore.Helpers.DateHelper.ConvertDate(aOpportunity.OutBoundDate).Value;
            lOpp.EndDate = BlitzerCore.Helpers.DateHelper.ConvertDate(aOpportunity.InBoundDate).Value;
            lOpp.OutboundAirPort = lTravelBiz.GetAirPort(aOpportunity.OutBoundAirport);
            lOpp.InboundAirPort = lTravelBiz.GetAirPort(aOpportunity.InBoundAirPort);
            lOpp.Travelers = Convert(aOpportunity.Travelers);
            lOpp.Travelers.ForEach(x => x.Opportunity = lOpp);
            lOpp.Name = GetTripName(aOpportunity);
            lOpp.AgentId = aOpportunity.AgentId;
            lOpp.Notes = aOpportunity.Notes;
            return lOpp;
        }

        public void UpdateStage(int aOpportunityId, OpportunityStages aStage, Contact aUser, bool aCommit = true)
        {
            var lOpp = GetOpportunity(aOpportunityId);
            UpdateStage(lOpp, aStage, aUser, aCommit);
        }

        public void UpdateStage(Opportunity aOpportunity, OpportunityStages aStage, Contact aUser, bool aCommit = true)
        {
            aOpportunity.Stage = aStage;
            Save(aOpportunity, aUser, aCommit);
        }

        public void CloseCreateQuoteTask(Opportunity aOpp)
        {
            var lTaskDA = new TaskDataAccess(mContext);
            List<Task> lTasks = lTaskDA.Get(aOpp);
            var lTask = lTasks.FirstOrDefault(x => x.Name == "Create Quote" && x.Status != TaskStatusTypes.COMPLETED);
            if (lTask == null)
                return;
            lTask.Status = TaskStatusTypes.COMPLETED;
            lTaskDA.Save(lTask);
        }

        public BlitzerCore.Models.UI.UIOpportunity Convert(BlitzerCore.Models.Opportunity aOpportunity)
        {
            BlitzerCore.Models.UI.UIOpportunity lOpp = new BlitzerCore.Models.UI.UIOpportunity();
            ////lOpp.OutBoundDate = aOpportunity.OutBoundDate).Value;
            ////lOpp.EndDate = Helpers.DateHelper.ConvertDate(aOpportunity.InBoundDate).Value;
            ////lOpp.OutboundAirPort = GetAirPort(aOpportunity.OutBoundAirport);
            ////lOpp.InboundAirPort = GetAirPort(aOpportunity.InBoundAirPort);
            //lOpp.Travellers = Convert(aOpportunity.Travallers);
            ////lOpp.Name = GetTripName(aOpportunity);
            ////lOpp.AgentID = aOpportunity.AgentID;
            return lOpp;
        }

        public BlitzerCore.Models.Opportunity CreateNewOpportunity(Contact aContact, Agent aOwner)
        {
            BlitzerCore.Models.Opportunity lOpp = new BlitzerCore.Models.Opportunity();
            lOpp.Agent = aOwner;
            lOpp.AgentId = aOwner.Id;
            lOpp.CreatedOn = DateTime.Now;
            lOpp.CreatedBy = lOpp.Agent;
            lOpp.CreatedById = lOpp.AgentId;
            lOpp.Travelers = new List<UserMap>();

            if (aContact != null)
            {
                lOpp.Travelers.Add(new UserMap() { Opportunity = lOpp, UserID = aContact.Id, User = aContact });
                if (aContact.HouseHold != null && aContact.HouseHold.Members != null)
                {
                    var lMembers = aContact.HouseHold.Members.Where(x => x.Id != aContact.Id);
                    foreach (var lMember in lMembers)
                        lOpp.Travelers.Add(new UserMap() { Opportunity = lOpp, User = lMember });
                }
            }

            return lOpp;
        }

        public BlitzerCore.Models.Opportunity CreateNewOpportunity(Agent aOwner)
        {
            BlitzerCore.Models.Opportunity lOpp = new BlitzerCore.Models.Opportunity();
            var lRelationships = new RelationshipDataAccess(mContext).GetRelationships();
            lOpp.AgentId = aOwner.Id;
            foreach (var lRel in lRelationships)
                lOpp.Travelers.Add(new UserMap() { User = new BlitzerCore.Models.Client() { RelationshipID = lRel.RelationshipID } });
            return lOpp;
        }

        internal Opportunity GetOpportunity(BlitzerCore.Models.QuoteRequest aRequest, List<BlitzerCore.Models.UI.UIContact> aClients, Agent aAgent)
        {
            if (aRequest.OpportunityID == 0)
            {
                var lOpp = Convert(mContext, aRequest, aClients, aAgent);
                var lOppBiz = new OpportunityBusiness(mContext);
                var lTargetOpp = Find(lOpp);

                if (lTargetOpp == null)
                {
                    lOpp.Stage = GetDefaultStage();
                    lOpp.ReferralId = aRequest.RefferalId;
                    Save(lOpp, aAgent);
                    aRequest.OpportunityID = lOpp.ID;
                    return lOpp;
                }
                else
                {
                    aRequest.OpportunityID = lTargetOpp.ID;
                    lOpp.ReferralId = aRequest.RefferalId;
                    return new OpportunityDataAccess(mContext).Get(aRequest.OpportunityID);
                }
            }
            else
                return new OpportunityDataAccess(mContext).Get(aRequest.OpportunityID);
        }

        private string GetTripName(BlitzerCore.Models.UI.UIOpportunity aOpportunity)
        {
            BlitzerCore.Models.UI.UIContact lPrimary = aOpportunity.Travelers[0];
            return lPrimary.Last;
        }


        private OpportunityStages GetDefaultStage()
        {
            return OpportunityStages.New;
        }

        static void ProcessTravelers(Opportunity aOpp)
        {
            if (aOpp.Travelers.Count == 0 || aOpp.Travelers[0].User == null)
            {
                Logger.LogError(ClassName + "ProcessTravelers - Opportunity requires at least one Traveler");
                return;
            }
            // Set the Primary 
            aOpp.Travelers[0].Primary = true;
            Logger.LogInfo(ClassName + $"ProcessTravelers - Set {aOpp.Travelers[0].User.Name} as primary");
        }

        public static List<UserMap> BuildUserMap(IDbContext aDbContext, Opportunity aOpp, List<BlitzerCore.Models.UI.UIContact> aClients, Agent aAgent)
        {
            List<UserMap> lOutput = new List<UserMap>();
            var lNotInDB = aClients.Where(x => x.Id == null);
            var lInDB = aClients.Where(x => x.Id != null);
            var lContactBiz = new ContactBusiness(aDbContext);
            foreach (var lUIContact in lInDB)
            {

                if ((lUIContact.First == null || lUIContact.First == "") &&
                    (lUIContact.Last == null || lUIContact.Last == ""))
                    continue;

                var lExistingUser = lContactBiz.Get(lUIContact.Id);
                lContactBiz.Update(lExistingUser, lUIContact, aAgent);
                lOutput.Add(new UserMap() { UserID = lUIContact.Id, Opportunity = aOpp, User = lExistingUser });
            }

            var lConvertedUsers = ContactUIHelper.Convert(aDbContext, lNotInDB, aAgent);
            lConvertedUsers.ForEach(x => x.User.OwnedById = aAgent.Id);
            lConvertedUsers.ForEach(x => x.Primary = false);
            lOutput.AddRange(lConvertedUsers);
            if (lOutput.Count > 0)
                lOutput.First().Primary = true;

            return lOutput;
        }

        public static Opportunity Convert(IDbContext aDbContext, QuoteRequest lRequest, List<BlitzerCore.Models.UI.UIContact> aClients, Agent aAgent)
        {
            Opportunity lOpp = new Opportunity();
            lOpp.AgentId = lRequest.AgentId;
            lOpp.EndDate = lRequest.ReturnDate;
            lOpp.StartDate = lRequest.DepartureDate;
            lOpp.OutboundAirPortID = lRequest.DepartureAirPortID;
            lOpp.InboundAirPortID = lRequest.DestinationAirPortID;
            aClients.ForEach(x => x.AgentId = lOpp.AgentId);
            lOpp.Travelers = BuildUserMap(aDbContext, lOpp, aClients, aAgent);
            lOpp.Travelers[0].Primary = true;
            return lOpp;
        }

        internal void Delete(Opportunity aOpp)
        {
            new OpportunityDataAccess(mContext).Delete(aOpp);
        }

        public void QuoteSent(QuoteRequest aQuoteRequest, Agent aAgent)
        {
            var lNote = new NoteBusiness(mContext).CreateNote(aQuoteRequest.Opportunity);
            lNote.Writer = aAgent;
            lNote.Memo = "Quote sent out";
            new NoteBusiness(mContext).Save(lNote);
            aQuoteRequest.SentQuote = DateTime.Now;
            new QuoteRequestBusiness(mContext).Save(aQuoteRequest, aAgent);
            if (aQuoteRequest.Opportunity != null)
            {
                CloseCreateQuoteTask(aQuoteRequest.Opportunity);
                if (aQuoteRequest.Opportunity.Stage == OpportunityStages.New)
                    aQuoteRequest.Opportunity.Stage = OpportunityStages.QuoteSent;
            }
        }
    }
}
