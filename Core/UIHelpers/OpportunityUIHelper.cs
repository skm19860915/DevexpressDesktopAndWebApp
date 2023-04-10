using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using BlitzerCore.Helpers;
using BlitzerCore.Business;
 
namespace BlitzerCore.UIHelpers
{
    public class OpportunityUIHelper
    {
        public static BlitzerCore.Models.UI.UIOpportunity Convert(IDbContext aContext, Opportunity aOpp)
        {
            var lOpp = new BlitzerCore.Models.UI.UIOpportunity();
            var lUser = aOpp.Travelers.Select(x => x.User);
            lOpp.Travelers = ContactUIHelper.Convert(lUser);
            if ( aOpp.OutboundAirPort != null )
                lOpp.OutBoundAirport = aOpp.OutboundAirPort.Code;
            if ( aOpp.InboundAirPort != null )
                lOpp.InBoundAirPort = aOpp.InboundAirPort.Code;
            
            // var lQuoteRequests = new QuoteRequestBusiness(aContext, mTravelSrv, mConfig).GetQuoteRequestByOppId(aOpp.ID);
            var lQuoteRequests = new QuoteRequestBusiness(aContext, null).GetQuoteRequestByOppId(aOpp.ID);
            lOpp.QuoteRequests = QuoteRequestUIHelper.Convert(aContext, lQuoteRequests);
            lOpp.Name = aOpp.Name;
            if (aOpp.QuoteRequests.Count() > 0)
                lOpp.Notes = aOpp.QuoteRequests[0].Notes;
            lOpp.OutBoundDate = DataHelper.GetDateString(aOpp.StartDate);
            lOpp.InBoundDate = DataHelper.GetDateString(aOpp.EndDate);
            lOpp.Tasks = TaskUIHelper.Convert(aOpp.Tasks);
            lOpp.Age = DataHelper.Age(aOpp.CreatedOn);
            lOpp.Notes = aOpp.Notes;
            if (lOpp.Owner == null)
                aOpp.Agent = new ContactBusiness(aContext).GetAgent(aOpp.AgentId);
            lOpp.AgentId = aOpp.AgentId;
            if ( aOpp.Agent != null )
                lOpp.Owner = aOpp.Agent.Name;
            lOpp.Id = aOpp.ID;
            lOpp.Stage = aOpp.Stage;
            lOpp.StageStr = aOpp.Stage.ToString();
            lOpp.IsTrip = aOpp is Trip;
            lOpp.Notes = aOpp.Notes;
            lOpp.NoteEntries = NoteUIHelper.Convert(aOpp.NoteEntries);

            if (aOpp.Files != null)
                lOpp.Files = aOpp.Files.Select(x => FileUIHelper.Convert(x)).ToList();

            return lOpp;
        }

        public static BlitzerCore.Models.Opportunity Convert(IDbContext aContext, UIOpportunity aOpp)
        {
            var lOpp = new OpportunityBusiness(aContext).GetOpportunity(aOpp.Id);
            lOpp.Notes = aOpp.Notes;
            return lOpp;
        }


        public static List<UIOpportunity> Convert(IDbContext aContext, List<Opportunity> aOpps)
        {
            var lOutput = new List<UIOpportunity>();
            foreach (var lOpp in aOpps)
                lOutput.Add(Convert(aContext, lOpp));
            return lOutput;
        }
        public static List<UIOpportunity> Convert(IDbContext aContext, IEnumerable<Opportunity> aOpps)
        {
            var lOutput = new List<UIOpportunity>();
            foreach (var lOpp in aOpps)
                lOutput.Add(Convert(aContext, lOpp));
            return lOutput;
        }
    }
}
