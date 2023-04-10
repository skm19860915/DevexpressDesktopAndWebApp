using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Helpers;
using BlitzerCore.Business;
using BlitzerCore.Utilities;
using System.IO;

namespace BlitzerCore.UIHelpers
{
    public class QuoteRequestUIHelper
    {
        readonly static string ClassName = "QuoteRequestUIHelper::";
        public static List<BlitzerCore.Models.UI.UIQuoteRequest> Convert(IDbContext aContext, List<BlitzerCore.Models.QuoteRequest> aRequestQuotes)
        {
            var lOutput = new List<BlitzerCore.Models.UI.UIQuoteRequest>();
            foreach (var lQuote in aRequestQuotes)
                lOutput.Add(Convert(aContext, lQuote));

            return lOutput;
        }

        public static BlitzerCore.Models.UI.UIQuoteRequest Convert(IDbContext aContext, QuoteRequest aRequest, bool aDeepCopy = false)
        {
            string FuncName = ClassName + "Convert (QuoteRequest) => UIQuoteRequest";
            //Logger.EnterFunction(FuncName);
            try
            {
                BlitzerCore.Models.UI.UIQuoteRequest lRequest = new BlitzerCore.Models.UI.UIQuoteRequest();

                if (aRequest.Agent == null)
                {
                    throw new System.IO.InvalidDataException("Agent for Quote Request ID[" + aRequest.QuoteRequestID + "] is invalid");
                }
                var lQRBiz = new QuoteRequestBusiness(aContext);
                lRequest.Id = aRequest.QuoteRequestID;
                lRequest.Name = aRequest.Opportunity.Name;
                lRequest.OpportunityID = aRequest.Opportunity.ID;
                lRequest.AgentId = aRequest.AgentId;
                lRequest.Agent = ContactUIHelper.Convert(aRequest.Agent);
                if (aRequest.Agent.PrimaryPhoneNumber != "" )
                    lRequest.AgentPhone = aRequest.Agent.PrimaryPhoneNumber;
                //lRequest.ActiveQuoteGroups = QuoteGroupUIHelper.Convert(aContext, new QuoteGroupBusiness(aContext).Get(aRequest, QuoteGroupFilter.Active), false);

                lRequest.DepartureCityCode = UpdateAirPortCodes(aRequest.DepartureAirPort?.Code);
                if ( aRequest.DestinationAirPort != null )
                    lRequest.DestinationCityCode = UpdateAirPortCodes(aRequest.DestinationAirPort.Code);
                lRequest.SentQuote = DataHelper.GetDateTimeString(aRequest.SentQuote);
                lRequest.StartDate = aRequest.DepartureDate.ToShortDateString();
                lRequest.EndDate = aRequest.ReturnDate.ToShortDateString();
                if (aRequest.QuoteGroups != null && 
                    aRequest.QuoteGroups.Any() &&
                    aRequest.QuoteGroups.First().ClientViews != null &&
                    aRequest.QuoteGroups.First().ClientViews.Any() && 
                    aRequest.QuoteGroups.First().ClientViews != null)
                    lRequest.Viewed = $"({aRequest.QuoteGroups.First().ClientViews.Count()}) {DataHelper.GetDateTimeString (aRequest.QuoteGroups.First().ClientViews.OrderBy(x=>x.When).Last().When)}";
                else
                    lRequest.Viewed = "Not Yet";

                lRequest.When = DataHelper.GetDateTimeString(aRequest.When);
                lRequest.Finished = DataHelper.GetDateTimeString(aRequest.Finished);
                // Quotes should only come from the QuoteGroup
                // -- DO NOT UNCOMMENT
                // if ( aDeepCopy == true )
                //    lRequest.Quotes = QuoteGroupUIHelper.ConvertToQuotes(aContext, aRequest.QuoteGroups, false);
                lRequest.Notes = aRequest.Notes;
                if (aRequest.QuoteGroups != null && aRequest.QuoteGroups.Any())
                {
                    lRequest.ClientNotes =  lQRBiz.GetOpenQuoteGroup (aRequest).Note;
                }
                // Screen requires four contact lines
                for ( int i = 0; i < aRequest.Opportunity.Travelers.Count(); i++)
                {
                    var lTraveler = aRequest.Opportunity.Travelers[i].User;
                    lRequest.Contacts.Add(ContactUIHelper.Convert(lTraveler));
                }
                lRequest.QuoteType = GetQuoteType ( aRequest.QuoteType);
                lRequest.NumberOfAdults = $"{aRequest.NumberOfAdults}";
                lRequest.Child1Age = $"{aRequest.Child1Age}";
                lRequest.Child2Age = $"{aRequest.Child2Age}";
                lRequest.Child3Age = $"{aRequest.Child3Age}";
                // Creeate Blanks
                var lSize = aRequest.Opportunity.Travelers.Count();
                for (var ii = lSize; ii < 4;ii++)
                    lRequest.Contacts.Add(new UIContact() { RelationshipID = Relationship.NotDefined });

                return lRequest;
            } finally
            {
                //Logger.LeaveFunction(FuncName);
            }
        }

        private static QuoteRequest.QuoteTypes GetQuoteType ( int aType )
        {
            return (QuoteRequest.QuoteTypes)aType;
        }

        private static int GetQuoteType(QuoteRequest.QuoteTypes aType)
        {
            return (int)aType;
        }

        private static string UpdateAirPortCodes(string aCode)
        {
            if (aCode == "-CN")
                return "CUN";

            return aCode;
        }

        public static string GetTravelerEmail (Contact aContact)
        {
            if (aContact.Emails == null || aContact.Emails.Any() == false )
                return "";
            return aContact.Emails[0].Address;
        }
        public static string GetTravelerCell(Contact aContact)
        {
            if (aContact.PhoneNumbers == null || aContact.PhoneNumbers.Any() == false )
                return "";
            return aContact.PhoneNumbers[0].PhoneNumber;
        }
        public static UIQuoteRequest Validate(IDbContext aContext, BlitzerCore.Models.UI.UIQuoteRequest aRequest)
        {
            DateTime lDepartDateTime = DateTime.Now;
            DateTime lReturnDateTime = DateTime.Now;

            if (DateTime.TryParse(aRequest.StartDate, out lDepartDateTime) == false)
                throw new InvalidDataException($"Depart date is in the incorrect format {aRequest.StartDate}");

            if (DateTime.TryParse(aRequest.EndDate, out lReturnDateTime) == false)
                throw new InvalidDataException($"Return date is in the incorrect format {aRequest.EndDate}");

            if (aRequest.Agent != null)
                aRequest.AgentId = aRequest.Agent.Id;

            return string.IsNullOrEmpty(aRequest.AgentId)
                ? throw new InvalidDataException("Agent ID is required to save a QuoteRequest")
                : aRequest;

            //if (aRequest.Contacts == null || aRequest.Contacts.Count() == 0 || aRequest.Contacts.Exists(x => x.RelationshipID == OpportunityBusiness.PRIMARY_DECISION_MAKER) == false)
            //    throw new InvalidDataException("QuoteRequest requires at least one PrimaryDecission Maker to save");

            // Can't turn down business.  If a person has enough info to request a quote.  Go with it
            // if (aRequest.Contacts.Exists(x => x.RelationshipID == OpportunityBusiness.PRIMARY_DECISION_MAKER && x.Cell == null))
            //     throw new InvalidDataException("QuoteRequest requires PrimaryDecission to have cell phone number");
            //return Convert(aContext, aRequest);
        }

        public static BlitzerCore.Models.QuoteRequest Convert(IDbContext aContext, BlitzerCore.Models.UI.UIQuoteRequest aInput)
        {
            string FuncName = ClassName + "Convert(UIQuoteRequest) => QuoteRequest";
            Logger.EnterFunction(FuncName);
            try
            {
                var lQuoteDataAccess = new TravelBusiness(aContext);
                DateTime lDepartDateTime = DateTime.Now;
                DateTime lReturnDateTime = DateTime.Now;

                DateTime.TryParse(aInput.StartDate, out lDepartDateTime);
                DateTime.TryParse(aInput.EndDate, out lReturnDateTime);

                QuoteRequest lOutput = null;
                var lQRBiz = new QuoteRequestBusiness(aContext);
                if (aInput.Id > 0)
                    lOutput = lQRBiz.Get(aInput.Id);
                else
                    lOutput = lQRBiz.Create(aInput.AgentId, aInput.DepartureCityCode, aInput.DestinationCityCode);

                lOutput.DepartureDate = lDepartDateTime;
                lOutput.ReturnDate = lReturnDateTime;
                lOutput.NumberOfAdults = 2;
                if (int.TryParse(aInput.NumberOfAdults, out int lNumberOfAdults))
                    lOutput.NumberOfAdults = lNumberOfAdults;
                if (int.TryParse(aInput.Child1Age, out int aChild1Age))
                    lOutput.Child1Age = aChild1Age;
                if (int.TryParse(aInput.Child2Age, out int lChild2Age))
                    lOutput.Child2Age = lChild2Age;
                if (int.TryParse(aInput.Child3Age, out int aChild3Age))
                    lOutput.Child1Age = aChild3Age;

                lOutput.NumberOfChildren = QuoteRequestBusiness.GetNumberOfChildren(lOutput);
                lOutput.When = DateTime.Now;
                lOutput.Finished = null;
                lOutput.AgentId = aInput.AgentId;
                lOutput.QuoteType = GetQuoteType( aInput.QuoteType);
                lOutput.OpportunityID = aInput.OpportunityID;
                lOutput.AgesOfKids = aInput.AgesOfKids;
                lOutput.DOBsOfKids = aInput.DOBsOfKids;
                lOutput.Notes = aInput.Notes;
                lOutput.RefferalId = aInput.RefferalId;

                lQRBiz.GetOpenQuoteGroup(lOutput).Note = aInput.ClientNotes;
                var lAirBiz = new AirBusiness(aContext);
                lOutput.DepartureAirPortID = lAirBiz.GetAirPort(aInput.DepartureCityCode).AirPortID;
                lOutput.DestinationAirPortID = lAirBiz.GetAirPort(aInput.DestinationCityCode).AirPortID;

                return lOutput;
            } finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }
    }
}
