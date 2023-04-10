using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.DataAccess;
using BlitzerCore.Helpers;
using BlitzerCore.Business;
using BlitzerCore.Models.ASP;

namespace BlitzerCore.UIHelpers
{
    public class ContactUIHelper
    {
        public static List<BlitzerCore.Models.UserMap> Convert(IDbContext mContext, IEnumerable<BlitzerCore.Models.UI.UIContact> aClients, Contact aAgent)
        {
            var lOutput = new List<BlitzerCore.Models.UserMap>();
            var lUserDa = new ContactDataAccess(mContext);
            if (aClients == null)
                return lOutput;
            Contact lFirst = null;

            foreach (BlitzerCore.Models.UI.UIContact lClient in aClients)
            {

                if ((lClient.First == null || lClient.First == "") &&
                    (lClient.Last == null || lClient.Last == ""))
                    continue;
                var lNewClient = Convert(mContext, lClient, aAgent);
                if (lFirst == null)
                {
                    lFirst = lNewClient;
                    new ContactBusiness(mContext).AddHouseHoldMember(lFirst, null);
                }
                else
                    new ContactBusiness(mContext).AddHouseHoldMember(lFirst, lNewClient);

                lOutput.Add(new UserMap() { UserID = lClient.Id, User = lNewClient });
            }

            return lOutput;
        }

        public static UIContact Convert(IDbContext aDbContext, ASPContact aInput)
        {
            UIContact lUIContact = new UIContact();
            return lUIContact;
        }
        public static Contact Convert(IDbContext aDbContext, BlitzerCore.Models.UI.UIContact aInput, Contact aAgent)
        {
            if (aInput.PrimaryEmail == null)
                aInput.PrimaryEmail = "";

            if (aInput.PrimaryEmail != null && aInput.PrimaryEmail.Length > 0 && (aInput.PrimaryEmail == null || aInput.PrimaryEmail.Length == 0))
                aInput.PrimaryEmail = aInput.PrimaryEmail;

            if (aInput.Cell != null && aInput.Cell.Length > 0 && (aInput.PrimaryPhone == null || aInput.PrimaryPhone.Length == 0))
                aInput.PrimaryPhone = aInput.Cell;

            // Must override value set in Lookup list of 0 added.  Issues is there are no Employers with ID = 0
            if (aInput.EmployerId == 0)
                aInput.EmployerId = null;

            Contact lOutput = new ContactDataAccess(aDbContext).Get(aInput.Id);
            if (lOutput == null && (aAgent as Agent) != null )
            {
                lOutput = new ContactBusiness(aDbContext).Create(aAgent as Agent);
                // this will happen with unit testing
                if (aInput.Id != null && aInput.Id.Length > 0)
                    lOutput.Id = aInput.Id;
            }

            if (aInput.Id == null || aInput.Id.Trim().Length == 0)
                lOutput.Id = Guid.NewGuid().ToString();  // Can't use email at this point because a Client may only provide their cell phone

            if (aInput.RelationshipID == Relationship.Husband) // Husband
            {
                lOutput.MaritalStatus = MaritalStatuses.Married;
                lOutput.Gender = Gender.Male;
            }
            else if (aInput.RelationshipID == Relationship.Wife) // Wife
            {
                lOutput.MaritalStatus = MaritalStatuses.Married;
                lOutput.Gender = Gender.Female;
            }

            lOutput.EmployerId = aInput.EmployerId;
            lOutput.DOB = DateHelper.ConvertDate(aInput.DOB);
            lOutput.Anniversary = DateHelper.ConvertDate(aInput.Anniversary);
            lOutput.Address1 = aInput.Address1;
            lOutput.Address2 = aInput.Address2;
            lOutput.City = aInput.City;
            lOutput.State = aInput.State;
            lOutput.ZipCode = aInput.ZipCode;
            lOutput.Gender = aInput.Gender;
            lOutput.Deleted = aInput.Deleted;
            lOutput.First = aInput.First;
            lOutput.Middle = aInput.Middle;
            lOutput.Middle_IsBlank = aInput.Middle_IsBlank;
            lOutput.Last = aInput.Last;
            lOutput.Suffix = aInput.Suffix;
            lOutput.NickName = aInput.NickName;
            lOutput.Title = aInput.Title;
            lOutput.Notes = aInput.Notes;
            lOutput.GlobalEntryNumber = aInput.GlobalEntryNumber;
            lOutput.TSANumber = aInput.TSANumber;
            lOutput.PassportNumber = aInput.PassportNumber;
            lOutput.PassportExpirationDate = aInput.PassportExpirationDate;
            lOutput.PassportIssueAgency = aInput.PassportIssueAgency;
            lOutput.PassportIssueDate = aInput.PassportIssueDate;

            string lCleanPhoneNumber = DataHelper.CleanPhoneNumber(aInput.PrimaryPhone);
            if (lOutput.PhoneNumbers == null) lOutput.PhoneNumbers = new List<Phone>();
            if (lOutput.PhoneNumbers.Count(x => x.Defaut == true) == 0)
                lOutput.PhoneNumbers.Add(new Phone() { PhoneNumber = lCleanPhoneNumber, UserId = aInput.Id, Defaut = true, PhoneTypeID = (int)PhoneType.ShortCuts.Cell });
            else
                lOutput.PhoneNumbers.First(x => x.Defaut == true).PhoneNumber = lCleanPhoneNumber;

            string lCleanEmail = DataHelper.CleanEmail(aInput.PrimaryEmail);
            if (lOutput.Emails == null) lOutput.Emails = new List<Email>();
            if (lOutput.Emails.Count(x => x.Preferred == true) == 0)
                lOutput.Emails.Add(new Email() { Address = aInput.PrimaryEmail, UserId = aInput.Id, EmailTypeID = Email.PERSONNEL_EMAIL, Preferred = true });
            else
                lOutput.Emails.First(x => x.Preferred == true).Address = lCleanEmail;

            return lOutput;
        }

        public static List<BlitzerCore.Models.UI.UIContact> Convert(IEnumerable<Contact> aClients, bool aDeepCopy = true)
        {
            var lOutput = new List<BlitzerCore.Models.UI.UIContact>();
            if (aClients == null)
                return lOutput;

            foreach (var lClient in aClients)
            {
                lOutput.Add(Convert(lClient, aDeepCopy));
            }
            return lOutput;
        }

        public static List<UIContactCore> ConvertCore (List<Contact> aUsers )
        {
            var lOutput = new List<UIContactCore>();
            foreach (var lContact in aUsers)
                lOutput.Add(new UIContactCore() { Id = lContact.Id, Email = lContact.PrimaryEmail, First = lContact.First, Middle = lContact.Middle, Last = lContact.Last });

            return lOutput;
        }

        public static ASPContact ASPConvert(Contact lData)
        {
            var lOutput = new ASPContact();
            return Convert(lData, true, lOutput) as ASPContact;
        }

        public static UIContact Convert(Contact lData, bool aDeepCopy = true, ASPContact lBase = null)
        {
            if (lData == null) return null;


            UIContact lOutput = new UIContact();
            if (lBase != null)
                lOutput = lBase;

            lOutput.EmployerId = lData.EmployerId;
            if (lData.Employer != null)
                lOutput.EmployerName = lData.Employer.Name;
            lOutput.Name = lData.Name;
            lOutput.Suffix = lData.Suffix;
            lOutput.NickName = lData.NickName;
            lOutput.Title = lData.Title;
            lOutput.ProfileComplete = new ContactBusiness(null).PassesProfileCheck(lData);
            lOutput.Address1 = lData.Address1;
            lOutput.Address2 = lData.Address2;
            lOutput.City = lData.City;
            lOutput.First = lData.First;
            lOutput.Middle = lData.Middle;
            lOutput.Last = lData.Last;
            lOutput.Gender = lData.Gender;
            lOutput.Id = lData.Id;
            lOutput.Deleted = lData.Deleted;
            lOutput.State = lData.State;
            lOutput.ZipCode = lData.ZipCode;
            lOutput.Middle_IsBlank = lData.Middle_IsBlank;
            lOutput.Notes = lData.Notes;
            lOutput.PrimaryEmail = GetEmail(lData);
            lOutput.PrimaryPhone = DataHelper.FormatPhoneNumber(lData.PrimaryPhoneNumber);
            lOutput.NickName = lData.NickName;
            lOutput.GlobalEntryNumber = lData.GlobalEntryNumber;
            lOutput.TSANumber = lData.TSANumber;
            lOutput.PassportNumber = lData.PassportNumber;
            lOutput.PassportExpirationDate = lData.PassportExpirationDate;
            lOutput.PassportIssueAgency = lData.PassportIssueAgency;
            lOutput.PassportIssueDate = lData.PassportIssueDate;
            lOutput.DOB = DataHelper.GetDateString(lData.DOB);
            lOutput.Anniversary = DataHelper.GetDateString(lData.Anniversary);
            lOutput.AddressLine1 = AddressLine(lOutput);
            lOutput.AddressLine2 = CityStateLine(lOutput);
            lOutput.MemberShips = lData.MemberShips;
            lOutput.Cards = FOPUIHelper.Convert (lData.Cards);
            if ( aDeepCopy == true && lData.HouseHold != null )
                lOutput.HouseHoldMembers = ContactUIHelper.Convert(lData.HouseHold.Members, false);
            if (lData.Gender != null)
                lOutput.Gender = lData.Gender.Value;
            lOutput.NoteEntries = NoteUIHelper.Convert(lData.NoteEntries);

            SetRelationShip(lOutput, lData);

            return lOutput;
        }

        public static Agent ConvertAgent(IDbContext aDbContext, UIContact aInput)
        {
            if (aInput.PrimaryEmail == null)
                aInput.PrimaryEmail = "";

            if (aInput.PrimaryEmail != null && aInput.PrimaryEmail.Length > 0 && (aInput.PrimaryEmail == null || aInput.PrimaryEmail.Length == 0))
                aInput.PrimaryEmail = aInput.PrimaryEmail;

            if (aInput.Cell != null && aInput.Cell.Length > 0 && (aInput.PrimaryPhone == null || aInput.PrimaryPhone.Length == 0))
                aInput.PrimaryPhone = aInput.Cell;

            Agent lOutput = new ContactDataAccess(aDbContext).GetAgent(aInput.Id);
            if (lOutput == null)
            {
                lOutput = new Agent();
                // this will happen with unit testing
                if (aInput.Id != null && aInput.Id.Length > 0)
                    lOutput.Id = aInput.Id;
            }

            if (aInput.Id == null || aInput.Id.Trim().Length == 0)
                lOutput.Id = Guid.NewGuid().ToString();  // Can't use email at this point because a Client may only provide their cell phone

            if (aInput.RelationshipID == Relationship.Husband) // Husband
            {
                lOutput.MaritalStatus = MaritalStatuses.Married;
                lOutput.Gender = Gender.Male;
            }
            else if (aInput.RelationshipID == Relationship.Wife) // Wife
            {
                lOutput.MaritalStatus = MaritalStatuses.Married;
                lOutput.Gender = Gender.Female;
            }

            lOutput.OwnedById = aInput.AgentId;
            lOutput.DOB = DateHelper.ConvertDate(aInput.DOB);
            lOutput.Address1 = aInput.Address1;
            lOutput.Address2 = aInput.Address2;
            lOutput.City = aInput.City;
            lOutput.State = aInput.State;
            lOutput.ZipCode = aInput.ZipCode;
            lOutput.First = aInput.First;
            lOutput.Middle = aInput.Middle;
            lOutput.Last = aInput.Last;
            lOutput.Suffix = aInput.Suffix;
            lOutput.Title = aInput.Title;
            lOutput.NickName = aInput.NickName;
            lOutput.Notes = aInput.Notes;
            lOutput.Deleted = aInput.Deleted;
            lOutput.GlobalEntryNumber = aInput.GlobalEntryNumber;
            lOutput.TSANumber = aInput.TSANumber;
            lOutput.PassportNumber = aInput.PassportNumber;
            lOutput.PassportExpirationDate = aInput.PassportExpirationDate;
            lOutput.PassportIssueAgency = aInput.PassportIssueAgency;
            lOutput.PassportIssueDate = aInput.PassportIssueDate;

            string lCleanPhoneNumber = DataHelper.CleanPhoneNumber(aInput.PrimaryPhone);
            if (lOutput.PhoneNumbers == null) lOutput.PhoneNumbers = new List<Phone>();
            if (lOutput.PhoneNumbers.Count(x => x.Defaut == true) == 0)
                lOutput.PhoneNumbers.Add(new Phone() { PhoneNumber = lCleanPhoneNumber, UserId = aInput.Id, Defaut = true, PhoneTypeID = (int)PhoneType.ShortCuts.Cell });
            else
                lOutput.PhoneNumbers.Single(x => x.Defaut == true).PhoneNumber = lCleanPhoneNumber;

            string lCleanEmail = DataHelper.CleanEmail(aInput.PrimaryEmail);
            if (lOutput.Emails == null) lOutput.Emails = new List<Email>();
            if (lOutput.Emails.Count(x => x.Preferred == true) == 0)
                lOutput.Emails.Add(new Email() { Address = aInput.PrimaryEmail, UserId = aInput.Id, EmailTypeID = Email.PERSONNEL_EMAIL, Preferred = true });
            else
                lOutput.Emails.Single(x => x.Preferred == true).Address = lCleanEmail;

            return lOutput;
        }

        private static void SetRelationShip(UIContact aUIContact, Contact lData)
        {
            if (lData.Gender == Gender.Male && lData.MaritalStatus == MaritalStatuses.Married)
            {
                aUIContact.RelationshipID = Relationship.Husband;
                aUIContact.Relationship = "Husband";
            }
            else if (lData.Gender == Gender.Female && lData.MaritalStatus == MaritalStatuses.Married)
            {
                aUIContact.RelationshipID = Relationship.Wife;
                aUIContact.Relationship = "Wife";
            }
            else
            {
                aUIContact.RelationshipID = Relationship.NotDefined;
                aUIContact.Relationship = "Not Defined";
            }
        }

        private static string GetCell(Contact lData)
        {
            Phone lResult = null;

            if (lData.PhoneNumbers != null && lData.PhoneNumbers.Count > 0)
            {
                lResult = lData.PhoneNumbers.FirstOrDefault(x => x.Defaut == true);
                if (lResult != null)
                    return lResult.PhoneNumber;

                lResult = lData.PhoneNumbers.FirstOrDefault(x => x.PhoneTypeID == 1);

                if (lResult != null)
                    return lResult.PhoneNumber;
            }

            return "";
        }
        public static string AddressLine(UIContact aSrc)
        {
            if (aSrc == null)
                return "";
            string lOutput = aSrc.Address1;
            if (aSrc.Address2 != null && aSrc.Address2.Length > 0)
                lOutput += ", " + aSrc.Address2;

            return lOutput;
        }

        public static string CityStateLine(UIContact aSrc)
        {
            if (aSrc == null)
                return "";

            string lOutput = $"{aSrc.City}";
            if (aSrc.State != null && aSrc.State.Length > 1)
                lOutput += $", {aSrc.State} ";
            lOutput += $"{aSrc.ZipCode}";
            return lOutput;
        }

        private static string GetEmail(Contact lData)
        {
            Email lResult = null;

            if (lData.Emails != null && lData.Emails.Count > 0)
            {
                lResult = lData.Emails.FirstOrDefault(x => x.Preferred == true);
                if (lResult != null)
                    return lResult.Address;

                lResult = lData.Emails.FirstOrDefault(x => x.EmailTypeID == 1);

                if (lResult != null)
                    return lResult.Address; ;
            }

            return "";
        }
    }
}
