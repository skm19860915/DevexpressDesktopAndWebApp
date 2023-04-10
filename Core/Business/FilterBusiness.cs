using BlitzerCore.DataAccess;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using BlitzerCore.Helpers;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace BlitzerCore.Business
{
    // User Searches using bot
    // Bots stores results in QuoteRequestResuts
    // Business Layer Create a QuoteGroup because that has all the quotes to be sent to the user
    // BL Creates Filter from Default Filter
    // BL Apples Filter to QRR and Stores results in QRtoQRR Map
    // QuoteGroup is associated with Filter

    public class FilterBusiness
    {
        private IDbContext mContext;
        private FilterDataAccess mDataAccess = null;

        public FilterBusiness(IDbContext mContext)
        {
            this.mContext = mContext;
            mDataAccess = new FilterDataAccess(mContext);
        }

        /// <summary>
        /// Get all the Room Types available based on teh selected Accommodation
        /// </summary>
        /// <param name="aFilter"></param>
        /// <returns></returns>
        public IEnumerable<SKU> GetRoomTypes (Filter aFilter)
        {
            return new FilterDataAccess(mContext).GetSKUs(aFilter.Accommodations.Select(x => x.Accommodation));
        }

        /// <summary>
        /// Get each of the Filted Skus for the  filtr
        /// </summary>
        /// <param name="aFilter"></param>
        /// <returns></returns>
        public IEnumerable<SKU> GetFilteredRoomTypes(Filter aFilter)
        {
            return new FilterDataAccess(mContext).GetFilteredSKUs(aFilter);
        }

        /// <summary>
        /// Create a filter for an Agent based on the default filter they provided
        /// </summary>
        /// <param name="aDestinationAirPort"></param>
        /// <param name="aAgent"></param>
        /// <returns></returns>
        public Filter GetDefaultFilter(QuoteRequest aQuoteRequest)
        {
            Filter lOutput = new Filter();
            AgentAirPortPreference lTempFilter = mDataAccess.GetAgentDefaultFilter(aQuoteRequest.Agent, aQuoteRequest.DestinationAirPort);
            lOutput.Copy(lTempFilter);
            lOutput.QuoteRequest = aQuoteRequest;
            lOutput.AirFilters = GetAirFilter(aQuoteRequest.DestinationAirPort, aQuoteRequest.Agent);
            lOutput.QuoteRequestID = aQuoteRequest.QuoteRequestID;

            //var lFDA = new FilterDataAccess(mContext);
            //var lOutboundFilters = lFDA.GetDefaultFilter(aAgent, aDestinationAirPort, false, true, false);
            //lOutput.OutboundFilters = new List<AIFilterMAP>();
            //foreach (var lDefault in lOutboundFilters)
            //    lOutput.OutboundFilters.Add(new AIFilterMAP() { AIFilterID = lDefault.ID, FilterID = lOutput.FilterID });

            //var lInBoundFilters = lFDA.GetDefaultFilter(aAgent, aDestinationAirPort,false, false, true);
            //lOutput.InboundFilters = new List<AIFilterMAP>();
            //foreach (var lDefault in lInBoundFilters)
            //    lOutput.InboundFilters.Add(new AIFilterMAP() { AIFilterID = lDefault.ID, FilterID = lOutput.FilterID });

            return lOutput;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Quote Group ID</param>
        public IEnumerable<SKU> GetFilterSKUData(int id)
        {
            var lQuoteGroup = new QuoteGroupBusiness(mContext).Get(id);
            var lFilter = new QuoteGroupBusiness(mContext).GetFilter(lQuoteGroup);
            var lFilteredProviders = lFilter.Accommodations.Select(x => x.AccommodationID);
            var lSKUs = new QuoteGroupDataAccess(mContext).GetSKUs(lQuoteGroup);
            if (lFilteredProviders.Count() > 0)
                lSKUs = lSKUs.Where(x => lFilteredProviders.Contains(x.ProviderID));
            return lSKUs;
        }

        public UIFilter GetView(int aFilterID)
        {
            var lUIFilter = new BlitzerCore.Models.UI.UIFilter();
            var lQRBiz = new QuoteRequestBusiness(mContext, null);
            var lSelectedAccommodations = new List<int>();
            BlitzerCore.Models.QuoteRequest lQuoteRequest = null;

            // Find the Quote which hasn't had a Quote Sent yet
            Filter lFilter = new FilterDataAccess(mContext).Get(aFilterID);
            if (lFilter != null ) {
                lUIFilter = Convert(lFilter);
                lQuoteRequest = lFilter.QuoteRequest;
                lUIFilter.Accommondations = new HotelBusiness(mContext).GetFilterView(lQuoteRequest, out lSelectedAccommodations, new QuoteGroupBusiness(mContext).GetQuoteGroup(lFilter));
                lUIFilter.SelectedAccommondations = lSelectedAccommodations;
                var lLocations = GetQuoteLocations(lFilter.QuoteGroup);
                lUIFilter.Locations = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                lUIFilter.Locations.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem() { Text = "All", Value = null });
                foreach (var lLocation in lLocations)
                    lUIFilter.Locations.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem() { Text = lLocation, Value = lLocation });
                lUIFilter.Stars = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                lUIFilter.SelectedStars = lFilter.Stars.ToString();
                for ( int i = 1; i <= 5; i++ )
                    lUIFilter.Stars.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem() { Text = i.ToString(), Value = i.ToString() });
            }
            else
            {
                Logger.LogError("Attempted to get View for Filter, but the filter didn't exist");
            }

            return lUIFilter;
        }

        private List<string> GetQuoteLocations(BlitzerCore.Models.QuoteGroup aQuoteGroup)
        {
            return new QuoteRequestDataAccess(mContext).GetLocations(aQuoteGroup);
        }

        public static BlitzerCore.Models.UI.UIFilter Convert(Filter aFilter)
        {
            var lUIFilter = new BlitzerCore.Models.UI.UIFilter();
            lUIFilter.AdultOnly = aFilter.AdultOnly;
            lUIFilter.AdultsOnlySection = aFilter.AdultsOnlySection;
            lUIFilter.AllInclusive = aFilter.AllInclusive;
            lUIFilter.PerPersonBudget = DataHelper.ConvertToCurrency(aFilter.PerPersonBudget);
            lUIFilter.TripBudget = DataHelper.ConvertToCurrency(aFilter.TripBudget);
            lUIFilter.TripMinBudget = DataHelper.ConvertToCurrency(aFilter.TripMinBudget);
            lUIFilter.Stops_Equals_0 = aFilter.Stops_Equals_0;
            lUIFilter.Stops_Equals_1 = aFilter.Stops_Equals_1;
            lUIFilter.FilterID = aFilter.FilterID;
            lUIFilter.QuoteGroupID = aFilter.QuoteGroupID;
            lUIFilter.QuoteRequestID = aFilter.QuoteRequestID;

            return lUIFilter;
        }

        public ICollection<AIFilterMAP> GetAirFilter(AirPort aDestinationAirPort, Agent aAgent)
        {
            var lOutput = new List<AIFilterMAP>();
            var lAirDefaults = mDataAccess.GetDefaultFilter(aAgent, aDestinationAirPort, AIFilterMAP.MapTypes.AIR);
            foreach (var lAirDefault in lAirDefaults)
            {
                var lMap = new AIFilterMAP() { AIFilterID = lAirDefault.AirFlightFilter.Value, MapType = AIFilterMAP.MapTypes.AIR };
                lOutput.Add(lMap);
            }
            return lOutput;
        }

        public BlitzerCore.Models.QuoteRequest GetQuoteRequest(Filter aFilter)
        {

            return mDataAccess.GetQuoteRequest(aFilter);
        }

        public Filter Get(int aID)
        {
            var lFilter = mDataAccess.Get(aID);

            //if ( lFilter != null && lFilter.Quote != null && lFilter.Quote.QuoteRequest != null )   
            //    lFilter.AirFilters = GetAirFilter(lFilter.Quote.QuoteRequest.DestinationAirPort, lFilter.Quote.QuoteRequest.AgentId);
            return lFilter;
        }

        public Filter Save(Filter aFilter)
        {
            var lQuoteBiz = new QuoteBusiness(mContext);

            //BlitzerCore.Models.Quote lQuote = new QuoteDataAccess(mContext).Get(aFilter.QuoteID);
            //if (lQuote == null)
            //    lQuote = lQuoteBiz.CreateQuote(aFilter);
            //else
            //{
            //    Copy(lExistingFilter, aFilter);
            //    mDataAccess.Save(lExistingFilter);
            //}
            mDataAccess.Save(aFilter);
            return aFilter;
        }

        private void Copy(Filter aFilter, Filter aBaseFilter)
        {
            if (aFilter == null)
                aFilter = new Filter();
            aFilter.Copy(aBaseFilter);
        }

    }
}