using System;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models;
using BlitzerCore.DataAccess;

namespace BlitzerCore.Business
{
    public class QTRBusiness
    {
        const string ClassName = "QuoteGroupBusiness::";
        IDbContext DbContext { get; }
        List<QuoteToResultsMapper> ExistingMappings { get; set; }
        public QTRBusiness(IDbContext aContext)
        {
            DbContext = aContext;
            Init();
        }

        private void Init()
        {
            //ExistingMappings = new QuoteGroupDataAccess(DbContext).get
        }

        public void DeleteMappings ( QuoteGroup aGroup )
        {
            new QuoteGroupDataAccess(DbContext).DeleteMappings(aGroup);
        }

        public QuoteToResultsMapper CreateMap(List<QuoteToResultsMapper> aQuotes, QuoteGroup aQuoteGroup, QuoteRequestResort aQuoteResort)
        {
            return ProcessMap (aQuotes, new QuoteToResultsMapper() { QuoteGroupID = aQuoteGroup.Id, QuoteRequestResortID = aQuoteResort.QuoteRequestResortID, QuoteRequestResort = aQuoteResort });
        }

        private QuoteToResultsMapper ProcessMap(List<QuoteToResultsMapper> aQuotes, QuoteToResultsMapper aMap)
        {
            if ( aMap.QuoteRequestResortID == null && aMap.FlightItineraryId == null )
                throw new NotImplementedException();

            aQuotes.Add(aMap);
            return aMap;
        }

        public QuoteToResultsMapper CreateMap(List<QuoteToResultsMapper> aQuotes, QuoteGroup aQuoteGroup, FlightItinerary aQuoteTicket)
        {
            return ProcessMap(aQuotes, new QuoteToResultsMapper() { QuoteGroupID = aQuoteGroup.Id, FlightItineraryId = aQuoteTicket.FlightItineraryId, FlightItinerary = aQuoteTicket });
        }
    }
}
