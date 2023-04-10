using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlitzerCore.Utilities;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;

namespace BlitzerCore.DataAccess
{
    public class ContactDataAccess
    {
        const string ClassName = "ContactDataAccess::";
        IDbContext mContext = null;

        public ContactDataAccess(IDbContext aContext)
        {
            mContext = aContext;
        }

        public IEnumerable<Agent> GetAgents()
        {
            return mContext.Agents;

        }

        public void AddClientRole(Contact aUser)
        {
            mContext.ExecCommand ($"exec AddClientRole '{aUser.PrimaryEmail}'");
        }
        public WebSrvLogin GetLogin(int aTourOperatorID, string aAgentID)
        {
            return mContext.WebSrvLogins.Where(x => x.AgentId == aAgentID && x.TourOperatorID == aTourOperatorID).FirstOrDefault();
        }

        public Contact Get(string aUserID)
        {
            var lContect = mContext.AppUsers
                    .Include(y => y.PhoneNumbers)
                    .Include(x => x.Emails)
                    .Include(x => x.Employer)
                    .Include(x => x.Cards)
                    .Include(x=>x.HouseHold)
                    .Include(x=>x.OwnedBy)
                    .Include(x=>x.NoteEntries).ThenInclude(x1=>x1.Writer)
                    .Include(x => x.MemberShips).ThenInclude(y => y.Company)
                    .FirstOrDefault(x => x.Id == aUserID);

            return lContect;
        }

        public List<UIContactCore> FindContacts(string aFName, string aMName, string aLName)
        {
            bool FNameValid = aFName != null && aFName.Length > 0;
            bool MNameValid = aMName != null && aMName.Length > 0;
            bool LNameValid = aLName != null && aLName.Length > 0;
            if ( FNameValid && !MNameValid && !LNameValid )
                return mContext.AppUsers.Where(x => x.First.ToUpper().Contains(aFName.ToUpper()))
                    .Select(x => new UIContactCore() { Id = x.Id, First = x.First, Middle = x.Middle, Last = x.Last })
                    .ToList();

            if (FNameValid && !MNameValid && LNameValid)
                return mContext.AppUsers.Where(x => x.First.ToUpper().Contains(aFName.ToUpper()) && x.Last.ToUpper().Contains(aLName.ToUpper()))
                    .Select(x => new UIContactCore() { Id = x.Id, First = x.First, Middle = x.Middle, Last = x.Last })
                    .ToList();

            if (!FNameValid && !MNameValid && LNameValid)
                return mContext.AppUsers.Where(x => x.Last.ToUpper().Contains(aLName.ToUpper()))
                    .Select(x => new UIContactCore() { Id = x.Id, First = x.First, Middle = x.Middle, Last = x.Last })
                    .ToList();

            return mContext.AppUsers.Where(x => x.First.Contains(aFName) && x.Middle.Contains(aMName) && x.Last.Contains(aLName))
                .Select(x => new UIContactCore() { Id = x.Id, First = x.First, Middle = x.Middle, Last = x.Last })
                .ToList();
        }

        public List<Contact> FindContacts(string aText)
        {
            if (aText == null)
                return new List<Contact>();

            var lUsers = mContext.AppUsers
                .Where(x => (x.First.ToUpper() + x.Middle.ToUpper() + x.Last.ToUpper()).Contains(aText.ToUpper()))
                .Include(sub=>sub.PhoneNumbers)
                .Include(sub => sub.Emails)
                .ToList();
            var lUserIds = mContext.PhoneNumbers
                .Where(x =>x.PhoneNumber.Contains(aText))
                .Include(sub => sub.User)
                .Select(x=>x.UserId)
                .ToList();

            if (lUsers != null && lUsers.Count() > 0)
                return lUsers;
            else
                return mContext.AppUsers
                    .Where(x=>lUserIds.Contains(x.Id))
                    .Include(sub=>sub.PhoneNumbers)
                    .Include(sub=>sub.Emails).ToList();
        }

        public Agent GetAgent(string aUserID)
        {
            return mContext.Agents
                .Include(x => x.PhoneNumbers)
                .Include(y => y.Emails)
                .Include(y=>y.Profile)
                .Include(x=>x.Profile).ThenInclude(x1=>x1.DefaultAirPort)
                .Include(y => y.PrimaryTeam).ThenInclude(sub => sub.Primary)
                .Include(x=>x.Employer)
                .FirstOrDefault(x => x.Id == aUserID);
        }
        public int Save(Contact aUser)
        {
            string FuncName = $"{ClassName}Save -";
            string lAction = "Added";
            var lCount = 0;
            try
            {
                if (aUser.Id == null || aUser.Id.Length == 0 || mContext.AppUsers.Count(x => x.Id == aUser.Id) == 0)
                    mContext.AppUsers.Add(aUser);
                else
                {
                    mContext.AppUsers.Update(aUser);
                    lAction = "Updated";
                }
                lCount = mContext.SaveChanges();
                Logger.LogInfo($"{FuncName} {lAction} {lCount} contact records");
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to save contact {aUser.Name}", e);
            }
            return lCount;
        }

        public string GetBySystemId(string aSysId)
        {
            var lUser = mContext.AppUsers.FirstOrDefault(x => x.SystemId == aSysId);
            if (lUser == null)
                return "";

            var lId = lUser.Id;
            return lId;
        }

        public IEnumerable<MemberShip> GetMemberShips()
        {
            return mContext.MemberShips
                .Include(x => x.Company)
                .Include(x => x.Contact).ToList();
        }

        public List<Contact> GetAll(Agent aAgent)
        {
            var lTeamMemberIDs = mContext.TeamMembers.Where(i => mContext.TeamMembers.Where(u => u.MemberId == aAgent.Id).Select(y => y.TeamId).Contains(i.TeamId)).Select(t => t.MemberId).ToList();

            if (lTeamMemberIDs.Count > 0)
                return mContext.AppUsers
                    .Include(x => x.PhoneNumbers)
                    .Include(x => x.Emails)
                    .Where(x => lTeamMemberIDs.Contains(x.OwnedById))
                    .ToList();
            else
                return mContext.AppUsers
                .Include(x => x.PhoneNumbers)
                .Include(x => x.Emails)
                .Where(x => x.OwnedById == aAgent.Id)
                .ToList();
        }

        public Contact GetContactByEmail(string aEmail)
        {
            if (aEmail == null || aEmail.Trim().Length == 0)
                return null;

            return mContext.Emails.Where(x => x.Address == aEmail)
                            .Include(Contact => Contact.User).ThenInclude(sub => sub.Emails)
                            .Select(x => x.User)
                            .FirstOrDefault();
        }

        public Contact Get(Contact aUser)
        {
            if (aUser == null)
                return null;

            return Get(aUser.Id);
        }

        public List<int> GetTripIDs(Contact aContact)
        {
            return mContext.UserMaps
                .Where(x => x.UserID == aContact.Id)
                .Select(x => x.Opportunity.ID)
                .ToList();
        }

        public List<Contact> GetAll()
        {
            return mContext.AppUsers
                    .Include(x => x.PhoneNumbers)
                    .ToList();
        }
    }
}
