using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using BlitzerCore.DataAccess;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Helpers;
using BlitzerCore.UIHelpers;
using BlitzerCore.Utilities;

namespace BlitzerCore.Business
{


    public class ContactBusiness
    {
        public static readonly string NEWUSERPWD = "N1tu!880";
        const string ClassName = "ContactBusiness::";

        private IDbContext DbContext { get; set; }
        private ContactDataAccess DataAccess { get; set; }

        public ContactBusiness(IDbContext mContext)
        {
            this.DbContext = mContext;
            DataAccess = new ContactDataAccess(mContext);
        }
        public Contact Get(string aContactId)
        {
            var lData = DataAccess.Get(aContactId);
            if (lData != null && lData.HouseHold != null)
                lData.HouseHold.Members = new HouseHoldBusiness(DbContext).GetMembers(lData.HouseHold);
            return lData;
        }

        public IEnumerable<FOP> GetCreditCards(UIContact aClient)
        {
            var lContact = Get(aClient.Id);
            return GetCCs(lContact);
        }

        public Contact Get(Contact aContact)
        {
            return DataAccess.Get(aContact);
        }

        public string GetPortalLink(Contact aContact, IConfiguration aConfig, string aText = null)
        {
            string lText = aText;
            if (lText == null)
                lText = "Activate Here";

            string lURL = aConfig["AppSettings:AppURL"];
            if (lURL.ToUpper().Contains("HTTP") == false)
                lURL = "https://" + lURL;

            if (aContact.ActivationDate == null)
                return $"<a href=\"{lURL}/Client/Activate/{aContact.Id}\"><i></i>{lText}</a>";
            else
                return $"<a href=\"{lURL}/Client\"><i></i>{lText}</a>";
        }

        public void SendPortalLink(Contact aContact, IConfiguration aConfig)
        {
            if (aContact.ActivationDate == null)
                SendPortalActivationEmail(aContact, aConfig);
            else
                SendPortalLoginLink(aContact, aConfig);
        }

        public void SendPortalLoginLink(Contact aContact, IConfiguration aConfig)
        {
            string FuncName = ClassName + $"SendPortalLink(Contact = {aContact.PrimaryEmail})";

            IEmailHelper lEmailer = new CoreEmailHelper(aConfig);
            var lSubject = "Eze2Travel Portal Link";
            var lBody = "Hello " + aContact.First + ", " + CoreEmailHelper.NewLine;
            lBody += CoreEmailHelper.NewLine;
            lBody += "Click the following this " + GetPortalLink(aContact, aConfig, "Eze2Travel") + " link to open your portal";
            lBody += GetEmailFooter();

            lEmailer.SendEmail(new List<string>() { aContact.PrimaryEmail }, new List<string>(), lSubject, lBody);
            Logger.LogInfo($"{FuncName} - Sent Portal Activation Emai");
        }

        private void SendPortalActivationEmail(Contact aContact, IConfiguration aConfig)
        {
            string FuncName = ClassName + $"SendPortalActivationEmail(Contact = {aContact.PrimaryEmail})";

            IEmailHelper lEmailer = new CoreEmailHelper(aConfig);
            var lSubject = "Eze2Travel Portal Activation";
            var lBody = "Hello " + aContact.First + ", " + CoreEmailHelper.NewLine;
            lBody += CoreEmailHelper.NewLine;
            lBody += "Please click the following link to activate your profile where we shall provide your quote ";
            lBody += GetPortalLink(aContact, aConfig);
            lBody += GetEmailFooter();

            lEmailer.SendEmail(new List<string>() { aContact.PrimaryEmail }, new List<string>(), lSubject, lBody);
            Logger.LogInfo($"{FuncName} - Sent Portal Activation Emai");

        }

        public string GetEmailFooter()
        {
            string lText = "";
            lText += CoreEmailHelper.NewLine;
            lText += CoreEmailHelper.NewLine;
            lText += "Thanks," + CoreEmailHelper.NewLine;
            lText += "Eze2Travel Support" + CoreEmailHelper.NewLine;
            lText += "(919) 815-0200" + CoreEmailHelper.NewLine;
            lText += "<img src=\"Https://www.eze2travel.com/images/Icons/EzeBlue.png\" width=\"83\" height=\"83\" alt='Eze2Travel Logo'>";
            return lText;
        }

        public IEnumerable<FOP> GetCCs(Contact lTraveler)
        {
            return new FOPDataAccess(DbContext).GetCards(lTraveler);
        }

        public Agent GetAgent(string aID)
        {
            return DataAccess.GetAgent(aID);
        }

        public List<HouseHoldMember> GetHouseHoldMembers(Contact aPrimary)
        {
            return HouseHoldHelper.GetMembers(DbContext, aPrimary);
        }

        public bool AddHouseHoldMember(Contact aPrimary, Contact aNewMember)
        {
            HouseHold lHouseHold = null;
            var lHouseHoldBiz = new HouseHoldDataAccess(DbContext);
            if (aPrimary == null)
                return false;

            if (aPrimary.HouseHold == null)
                aPrimary.HouseHold = new HouseHold();

            lHouseHold = aPrimary.HouseHold;

            if (aNewMember != null)
            {
                // Get the new members old Household
                int? lOldHHId = aNewMember.HouseHoldId;

                aNewMember.HouseHold = aPrimary.HouseHold;
                lHouseHold.AddMember(aNewMember);

                // Do not delete the old household if they are the same
                if (aPrimary.HouseHold != null && lOldHHId != null && lOldHHId.Value != aPrimary.HouseHoldId.Value)
                {
                    // Reassign member to new household
                    aNewMember.HouseHold = aPrimary.HouseHold;
                    // Delete old household if empty
                    if (lOldHHId != null && lOldHHId.Value > 0)
                    {
                        var lHouseHoldMembers = lHouseHoldBiz.GetMembers(lOldHHId.Value);
                        if (lHouseHoldMembers.Count() == 1)
                            lHouseHoldBiz.Delete(lOldHHId.Value);
                    }
                }
            }
            return new HouseHoldDataAccess(DbContext).Save(aPrimary.HouseHold) > 0;

        }

        public Contact GetByEmail(string primaryEmail)
        {
            return new ContactDataAccess(DbContext).GetContactByEmail(primaryEmail);
        }

        public static int? Age(Contact lMember)
        {
            // Save today's date.
            var today = DateTime.Today;

            if (lMember.DOB != null)
            {
                // Calculate the age.
                var age = today.Year - lMember.DOB.Value.Year;

                // Go back to the year in which the person was born in case of a leap year
                if (lMember.DOB.Value.Date > today.AddYears(-age))
                    age--;

                return age;
            }
            else if (lMember.Age != null)
                return lMember.Age;

            return null;

        }
        public bool PassesProfileCheck(Contact aContact)
        {
            if (ValidateString(aContact.First)
                && ValidateString(aContact.Middle, aContact.Middle_IsBlank)
                && ValidateString(aContact.Last)
                && aContact.DOB != null)
                return true;

            return false;
        }

        private bool ValidateString(string aValue, bool aBlankOnPurpose = false)
        {
            if (aBlankOnPurpose == true)
                return true;

            if (aValue != null && aValue.Length > 0)
                return true;

            return false;
        }
        public Contact Create(Agent aAgent)
        {
            if (aAgent.Id != null && aAgent.Id.Length > 0)
                return new Contact() { OwnedById = aAgent.Id };
            else
                return new Contact() { OwnedBy = aAgent };
        }
        private static void UpdateTracking(Contact aContact, Contact aAgent)
        {
            if (aContact.CreatedById == null || aContact.CreatedById == "")
            {
                aContact.CreatedById = aAgent.Id;
                aContact.CreatedOn = DateTime.Now;
            }
            aContact.UpdatedById = aAgent.Id;
            aContact.UpdatedOn = DateTime.Now;
        }

        public Contact Save(UIContact aContact, Contact aAgent)
        {
            var lContact = ContactUIHelper.Convert(DbContext, aContact, aAgent);
            Save(lContact, aAgent);
            return lContact;
        }
        public int Save(Contact aContact, Contact aAgent)
        {
            if (aContact.HouseHold == null)
            {
                aContact.HouseHold = new HouseHold();
            }

            UpdateTracking(aContact, aAgent);
            var lCnt = DataAccess.Save(aContact);

            // Need to save trip to automatically update the status on a trip
            // if the use updated the trip
            var lTripBiz = new TripBusiness(DbContext);
            var lTripIDs = DataAccess.GetTripIDs(aContact);
            if (lTripIDs != null)
            {
                foreach (var lTrip in lTripIDs)
                    lTripBiz.Update(lTrip);
            }

            return lCnt;
        }

        public List<Contact> GetAll()
        {
            var lOutput = DataAccess.GetAll();

            return lOutput.Where(x => x.Deleted == false).OrderBy(x => x.Name).ToList();
        }

        public List<UIContact> GetAll(Agent aAgent)
        {
            List<UIContact> lOutput = new List<UIContact>();
            var lData = DataAccess.GetAll(aAgent);

            foreach (var lClient in lData)
                lOutput.Add(ContactUIHelper.Convert(lClient));

            return lOutput.Where(x => x.Deleted == false).OrderBy(x => x.Name).ToList();
        }


        public Contact CreateChild(int lAge, Agent aAgent)
        {
            var lChild = new Contact() { Age = lAge, Id = Guid.NewGuid().ToString() };
            Save(lChild, aAgent);
            return lChild;
        }

        public void Update(Contact lExistingUser, UIContact lUIContact, Agent aAgent)
        {
            if (lExistingUser.Emails.Count == 0)
            {
                lExistingUser.Emails.Add(new Email() { Address = lUIContact.PrimaryEmail, UserId = lUIContact.Id, EmailTypeID = Email.PERSONNEL_EMAIL, Preferred = true });
                Save(lExistingUser, aAgent);
            }
        }
    }
}
