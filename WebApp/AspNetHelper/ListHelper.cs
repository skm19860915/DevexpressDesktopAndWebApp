using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using Microsoft.AspNetCore.Mvc.Rendering;
using BlitzerCore.UIHelpers;
using BlitzerCore.Business;
using BlitzerCore.Helpers;

namespace WebApp.AspNetHelper
{
    public class ListHelper
    {
        public static List<BlitzerCore.Models.UserMap> Convert(IDbContext mContext, IEnumerable<BlitzerCore.Models.UI.UIContact> aClients, Agent aAgent)
        {
            var lOutput = new List<BlitzerCore.Models.UserMap>();
            if (aClients == null)
                return lOutput;
            bool lFirst = true;

            foreach (BlitzerCore.Models.UI.UIContact lClient in aClients)
            {
                if ((lClient.First == null || lClient.First == "") &&
                    (lClient.Last == null || lClient.Last == ""))
                    continue;
                var lNewClient = ContactUIHelper.Convert(mContext, lClient, aAgent);
                if (lFirst == true)
                {
                    lFirst = false;
                }
                lOutput.Add(new UserMap() { User = lNewClient });
            }

            return lOutput;
        }
        public static List<SelectListItem> GetResorts(List<Hotel> aResorts)
        {
            var lOutput = new List<SelectListItem>();
            foreach (var lItem in aResorts)
                lOutput.Add(new SelectListItem() { Value = lItem.Id.ToString(), Text = lItem.Name });

            return lOutput;
        }

        public static List<SelectListItem> GetTravelers(List<UserMap> aTravelers)
        {
            var lOutput = new List<SelectListItem>();
            foreach (var lItem in aTravelers)
                lOutput.Add(new SelectListItem() { Value = lItem.UserID, Text = lItem.User.Name });

            return lOutput;
        }

        public static List<SelectListItem> GetFeatures(List<Feature> aData)
        {
            var lOutput = new List<SelectListItem>();
            foreach (var lItem in aData)
                lOutput.Add(new SelectListItem() { Value = lItem.Id.ToString(), Text = lItem.Name });

            return lOutput;
        }
        public static List<SelectListItem> GetList(List<Opportunity> aData)
        {
            var lOutput = new List<SelectListItem>();
            foreach (var lItem in aData.OrderBy(x=>x.Name))
                lOutput.Add(new SelectListItem() { Value = lItem.ID.ToString(), Text = lItem.Name });

            return lOutput;
        }

        public static List<SelectListItem> GetUserStories(List<UserStory> aData)
        {
            var lOutput = new List<SelectListItem>();
            foreach (var lItem in aData)
                lOutput.Add(new SelectListItem() { Value = lItem.Id.ToString(), Text = lItem.Name });

            return lOutput;
        }

        public static List<SelectListItem> GetSystems(List<BlitzSystem> aData)
        {
            var lOutput = new List<SelectListItem>();
            foreach (var lItem in aData)
                lOutput.Add(new SelectListItem() { Value = lItem.Id.ToString(), Text = lItem.Name });

            return lOutput;
        }

        public static List<SelectListItem> GetTourOperators(IEnumerable<TourOperator> aValues)
        {
            var lOutput = new List<SelectListItem>();
            foreach (var lItem in aValues)
                lOutput.Add(new SelectListItem() { Value = lItem.Id.ToString(), Text = lItem.Name + " " + CompanyUIHelper.GetPrimaryPhone(lItem)  });

            return lOutput;
        }

        public static List<SelectListItem> GetSuppliers(IEnumerable<Company> aValues)
        {
            var lOutput = new List<SelectListItem>();
            foreach (var lItem in aValues)
                lOutput.Add(new SelectListItem() { Value = lItem.Id.ToString(), Text = lItem.Name + " " + CompanyUIHelper.GetPrimaryPhone(lItem) });

            return lOutput;
        }

        public static dynamic GetRefferals(List<ReferralSource> aValues)
        {
            var lOutput = new List<SelectListItem>();
            foreach (var lItem in aValues)
                lOutput.Add(new SelectListItem() { Value = lItem.Id.ToString(), Text = lItem.Name });

            return lOutput;
        }

        public static List<SelectListItem> GetSprints(IEnumerable<Sprint> aValues)
        {
            var lOutput = new List<SelectListItem>();
            foreach (var lItem in aValues)
                lOutput.Add(new SelectListItem() { Value = lItem.Id.ToString(), Text = lItem.Name + " (" + DataHelper.GetDateString(lItem.Start) + "-" + DataHelper.GetDateString(lItem.End) + ")" });

            return lOutput;
        }

        internal static List<SelectListItem> GetTeamMembers(List<Agent> aAgents)
        {
            var lOutput = new List<SelectListItem>();
            foreach (var lItem in aAgents)
                lOutput.Add(new SelectListItem() { Value = lItem.Id.ToString(), Text = lItem.Name });

            return lOutput;
        }

        internal static List<SelectListItem> GetMemberShips(IEnumerable<Company> aMemberShips)
        {
            var lOutput = new List<SelectListItem>();
            foreach (var lItem in aMemberShips)
                lOutput.Add(new SelectListItem() { Value = lItem.Id.ToString(), Text = lItem.Name });

            return lOutput;
        }

        public static List<SelectListItem> GetEmployers(IEnumerable<Company> aCompanies)
        {
            var lOutput = new List<SelectListItem>();
            lOutput.Add(new SelectListItem() { Value = "0", Text = "Not Listed" });
            foreach (var lItem in aCompanies)
                lOutput.Add(new SelectListItem() { Value = lItem.Id.ToString(), Text = lItem.Name });

            return lOutput;
        }

        public static List<SelectListItem> GetCruiseTypes(List<Cruise> aCruiseTypes)
        {
            var lOutput = new List<SelectListItem>();
            foreach (var lItem in aCruiseTypes)
                lOutput.Add(new SelectListItem() { Value = lItem.SKUID.ToString(), Text = lItem.Name });

            return lOutput;
        }

        public static List<SelectListItem> GetProductTypes(List<SKU> aProductTypes)
        {
            var lOutput = new List<SelectListItem>();
            foreach (var lItem in aProductTypes)
                lOutput.Add(new SelectListItem() { Value = lItem.SKUID.ToString(), Text = lItem.Name } );

            return lOutput;
        }

        public static List<SelectListItem> GetAirPortCodes(List<AirPort> aAirPorts)
        {
            var lOutput = new List<SelectListItem>();
            foreach (AirPort lItem in aAirPorts)
                lOutput.Add(new SelectListItem() { Value = lItem.Code, Text = lItem.Name });

            return lOutput;
        }

        public static List<SelectListItem> GetBusinessTypes(List<BusinessType> aTypes)
        {
            var lOutput = new List<SelectListItem>();
            foreach (var lItem in aTypes)
                lOutput.Add(new SelectListItem() { Value = lItem.ID.ToString(), Text = lItem.Type });

            return lOutput;
        }

        public static List<SelectListItem> GetRelationShips(List<Relationship> lRelationships)
        {
            var lOutput = new List<SelectListItem>();
            foreach (Relationship lItem in lRelationships)
                lOutput.Add(new SelectListItem() { Value = lItem.RelationshipID.ToString(), Text = lItem.Description });

            return lOutput;
        }

        public static List<SelectListItem> GetCreditCards(IDbContext aContext, List<UserMap> aContacts)
        {
            var lOutput = new List<SelectListItem>();
            foreach (var lUMap in aContacts)
            {
                var lCards = new FOPBusiness(aContext).GetCards(lUMap.User);
                foreach (FOP lCard in lCards)
                    lOutput.Add(new SelectListItem() { Value = lCard.Id.ToString(), Text = FOPUIHelper.Obscure(lCard.Number) + " " + lUMap.User.Name });
            }
            return lOutput;
        }

        public static List<SelectListItem> GetQuoteTypes()
        {
            var lOutput = new List<SelectListItem>();
            lOutput.Add(new SelectListItem() { Value = "0", Text = "Package" });
            lOutput.Add(new SelectListItem() { Value = "1", Text = "Land Only" });
            lOutput.Add(new SelectListItem() { Value = "2", Text = "Both" });
            return lOutput;
        }

        public static dynamic GetAgents(List<Agent> lAgents)
        {
            var lOutput = new List<SelectListItem>();
            foreach(var lAgent in lAgents)
                lOutput.Add(new SelectListItem() { Value = lAgent.Id, Text = lAgent.Name });
            return lOutput;
        }

        public static List<SelectListItem> GetKidsAgesList()
        {
            var lOutput = new List<SelectListItem>();
            for (int i = 1; i <= 17; i++)
                lOutput.Add(new SelectListItem() { Value = $"{i}", Text = $"{i}" });
            return lOutput;
        }

        internal static dynamic GetAirPortIDs(List<AirPort> aAirPorts)
        {
            var lOutput = new List<SelectListItem>();
            foreach (AirPort lItem in aAirPorts)
                lOutput.Add(new SelectListItem() { Value = lItem.AirPortID.ToString(), Text = lItem.Name });

            return lOutput;
        }

        public static List<SelectListItem> GetNumberOfAdultsList()
        {
            var lOutput = new List<SelectListItem>();
            for (int i = 1; i <= 4; i++)
                lOutput.Add(new SelectListItem() { Value = $"{i}", Text = $"{i}" });
            return lOutput;
        }

        public static object GetOwnerTaskStati(bool isOwner)
        {
            var lOutput = new List<SelectListItem>();
            lOutput.Add(new SelectListItem() { Value = "0", Text =  "New"});
            lOutput.Add(new SelectListItem() { Value = "1", Text = "In Progress" });
            lOutput.Add(new SelectListItem() { Value = "2", Text = "On Hold" });
            lOutput.Add(new SelectListItem() { Value = "3", Text = "Review" });
            if (isOwner)
            {
                lOutput.Add(new SelectListItem() { Value = "4", Text = "Completed" });
                lOutput.Add(new SelectListItem() { Value = "5", Text = "Deleted" });
            }
            return lOutput;
        }
    }
}
