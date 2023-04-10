using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;
using BlitzerCore.Models;
using BlitzerCore.Utilities;
using BlitzerCore.Business;
using BlitzerCore.Models.UI;
using BlitzerCore.UIHelpers;
using BlitzerCore.DataAccess;
using WebApp.DataServices;
using WebApp.AspNetHelper;

namespace WebApp.Services
{
    public class BlazorService : IBlazorService
    {
        public enum RequirementTypes { PendingApproval, UnderReview}
        const string ClassName = "BlazorService::";
        public IDbContext DbContext { get; set; }
        public BlazorService(IDbContext context)
        {
            DbContext = context;
        }

        public HouseHold GetHouseHold(string aMember)
        {
            HouseHold lOutput = null;
            string FuncName = $"{ClassName}GetHouseHoldMembers (id={aMember})";
            Logger.EnterFunction(FuncName);
            try
            {
                if (aMember == null || aMember.Length == 0 )
                    return lOutput;
                var lContact = new ContactBusiness(DbContext).Get(aMember);
                if (lContact == null)
                    return lOutput;

                var lData = new HouseHoldBusiness(DbContext).Get(lContact);
                Logger.LogInfo("Members in household = " + lData.Members.Count());
                return lData;
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to find household members", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }

            return null;

        }

        public List<SelectListItem> GetActiveSprintsList()
        {
            var lSprints = new SprintBusiness(DbContext).GetAll(null)
                .Where(x => x.Status == Sprint.StatusTypes.Current || x.Status == Sprint.StatusTypes.Future).ToList();
            var lUISprints = ListHelper.GetSprints(lSprints);

            return lUISprints;
        }
        public List<UISprint> GetActiveSprints()
        {
            var lSprints = new SprintBusiness(DbContext).GetAll(null)
                .Where(x => x.Status == Sprint.StatusTypes.Current || x.Status == Sprint.StatusTypes.Future).ToList();
            var lUISprints = SprintUIHelper.Convert(lSprints);

            return lUISprints;
        }
        public UITrip GetTrip(int id)
        {
            string FuncName = $"{ClassName}Get (id={id})";
            Logger.EnterFunction(FuncName);
            try
            {
                if (id == 0)
                    return null;

                var lData = new TripBusiness(DbContext).Get(id);
                Logger.LogInfo("Travelers on trip = " + lData.Travelers.Count);
                var lUIData = TripUIHelper.Convert(lData, true);
                if (lData != null)
                    Logger.LogInfo($"Found {lData.Name} trip");
                else
                    Logger.LogInfo($"No Trip Found");
                return lUIData;
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to find contacts", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }

            return null;
        }

        public List<Contact> AddContact(int aTripId, string aContactId)
        {
            string FuncName = $"{ClassName}AddContact (TripId={aTripId}, ContactID={aContactId})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lTripBiz = new TripBusiness(DbContext);
                lTripBiz.AddContact(aTripId, aContactId);
                return lTripBiz.Get(aTripId).Travelers.Select(x => x.User).ToList();
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to add contacts", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }

            return new List<Contact>();

        }

        public List<UIUserStory> GetOpenRequirements()
        {
            return GetRequirements(RequirementTypes.PendingApproval);

        }

        public List<UIUserStory> GetUnderReViewRequirements()
        {
            return GetRequirements(RequirementTypes.UnderReview);
        }

        private List<UIUserStory> GetRequirements(RequirementTypes aType)
        {
            string FuncName = $"{ClassName}GetOpenRequirements ()";
            Logger.EnterFunction(FuncName);
            try
            {
                var lRequirementBiz = new UserStoryBusiness(DbContext);
                List<UserStory> lOpenReqs = null;
                if (aType == RequirementTypes.PendingApproval)
                    lOpenReqs = new UserStoryDataAccess(DbContext).GetAll().Where(x=> x.Status != UserStoryStatus.ReadyForTest && x.Status != UserStoryStatus.Deleted && x.Status != UserStoryStatus.Deployed).ToList();
                else
                    lOpenReqs = new UserStoryDataAccess(DbContext).GetAll().Where(x => x.Status == UserStoryStatus.ReadyForTest).OrderBy(x=>x.FeatureId).ToList();
                return UserStoryUIHelper.Convert(lOpenReqs);
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to add contacts", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }

            return new List<UIUserStory>();

        }

        public List<Contact> RemoveContact(int aTripId, string aContactId)
        {
            string FuncName = $"{ClassName}RemoveContact (TripId={aTripId}, ContactID={aContactId})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lTripBiz = new TripBusiness(DbContext);
                lTripBiz.RemoveContact(aTripId, aContactId);
                return lTripBiz.Get(aTripId).Travelers.Select(x => x.User).ToList();
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to add contacts", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }

            return new List<Contact>();

        }

        public List<UIContactCore> RemoveHouseHoldMember(int aHouseHoldId, string aContactId)
        {
            string FuncName = $"{ClassName}RemoveHouseHoldMember (HouseHold={aHouseHoldId}, ContactID={aContactId})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lHouseHoldBiz = new HouseHoldBusiness(DbContext);
                var lHouseHold = lHouseHoldBiz.Get(aHouseHoldId);
                var lContact = new ContactBusiness(DbContext).Get(aContactId);
                lHouseHoldBiz.RemoveMember (lHouseHold, lContact);
                return ContactUIHelper.ConvertCore(lHouseHoldBiz.GetMembers(lHouseHold));
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to remove household member", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }

            return new List<UIContactCore>();

        }

        public List<UIContactCore> AddHouseHoldMember(int aHouseHoldId, string aContactId)
        {
            string FuncName = $"{ClassName}AddHouseHoldMember (HouseHold={aHouseHoldId}, ContactID={aContactId})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lHouseHoldBiz = new HouseHoldBusiness(DbContext);
                var lHouseHold = lHouseHoldBiz.Get(aHouseHoldId);
                var lContact = new ContactBusiness(DbContext).Get(aContactId);
                lHouseHoldBiz.AddMember(lHouseHold, lContact);
                Logger.LogInfo($"{FuncName} MemberCount = {lHouseHold.Members.Count}");
                return ContactUIHelper.ConvertCore(lHouseHoldBiz.GetMembers(lHouseHold));
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to add household member", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }

            return new List<UIContactCore>();

        }

    }
}
