using System;
using System.Collections.Generic;
using System.Linq;
using BlitzerCore.Models;
using Microsoft.EntityFrameworkCore;
using BlitzerCore.Utilities;

namespace BlitzerCore.DataAccess
{
    public class FilterDataAccess
    {
        const string ClassName = "FilterDataAccess::";
        IDbContext mContext;
        public FilterDataAccess(IDbContext aContext)
        {
            mContext = aContext;
        }

        public IEnumerable<SKU> GetSKUs(IEnumerable<Hotel> aFilter)
        {
            var Ids = aFilter.Select(x => x.Id);
            return mContext.SKUs.Where(x => Ids.Contains(x.ProviderID));
        }

        public Filter GetQuoteGroupFilter(QuoteGroup aQGroup)
        {
            return mContext.Filters.Where(x => x.QuoteGroupID == aQGroup.Id).FirstOrDefault();
        }

        public int Save(AgentAirPortPreference aPref)
        {
            string FuncName = ClassName + $"Save (AgentAirPortPreference = ({aPref.ID}))";
            if (aPref.ID == 0)
                mContext.AgentAirPortPreferences.Add(aPref);
            else
                mContext.AgentAirPortPreferences.Update(aPref);

            try
            {
                var lCnt = mContext.SaveChanges();
                Logger.LogInfo(FuncName + $" Saved {lCnt} Filter Rows");
                return lCnt;
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + " Failed to save Filter", e);
            }

            return 0;
        }


        public AgentAirPortPreference GetAgentDefaultFilter(Agent aAgent, AirPort aDestinationAirPort)
        {
            string FuncName = ClassName + $"GetAgentDefaultFilter";
            Logger.EnterFunction(FuncName);
            var lCnt = mContext.AgentAirPortPreferences.Count();
            try
            {
                var lOutput = mContext.AgentAirPortPreferences
                    .Include(x => x.PreferredHotels)
                    .Where(x => x.AgentId == aAgent.Id && x.AirportID == aDestinationAirPort.AirPortID);


                if (lCnt > 0 && lOutput != null && lOutput.Count() > 0)
                {
                    Logger.LogDebug($"Looking for Agent filter for Agent : {aAgent.Id} Airport : {aDestinationAirPort.Code} Accommdations Cnt : {lOutput.FirstOrDefault().PreferredHotels.Count}");
                    return lOutput.FirstOrDefault();
                }
            }
            catch (Exception)
            {
                Logger.LogWarning(
                    $"{FuncName} {aAgent.Name}[{aAgent.Id}] has no default filter for {aDestinationAirPort}");
            }

            try
            {
                var lOutput = mContext.AgentAirPortPreferences
                    .Include(x => x.PreferredHotels)
                    .Where(x => x.AgentId == Defines.EZE2TRAVEL && x.AirportID == aDestinationAirPort.AirPortID);


                if (lOutput != null && lOutput.Count() > 0)
                {
                    Logger.LogDebug($"Looking for Eze2Travel Default filter for Agent : {aAgent.Id} Airport : {aDestinationAirPort.Code} Accommdations Cnt : {lOutput.FirstOrDefault().PreferredHotels.Count}");
                    return lOutput.FirstOrDefault();
                }
            }
            catch (Exception)
            {
                Logger.LogWarning(
                    $"{FuncName} EZE2TRAVEL({Defines.EZE2TRAVEL}) has no default filter for {aDestinationAirPort}");
            }

            Logger.LeaveFunction(FuncName);
            return new AgentAirPortPreference();
        }

        public List<AIDefaultFilter> GetDefaultFilter(Agent aAgent, AirPort aDestinationAirPort, AIFilterMAP.MapTypes aType)
        {

            var lOutput = mContext.AIDefaultFilters.Where(x => x.AgentId == aAgent.Id
            && x.AirPortID == aDestinationAirPort.AirPortID);
            if (lOutput != null && lOutput.Count() > 0)
                return lOutput.ToList();

            lOutput = mContext.AIDefaultFilters.Where(x => x.AgentId == Defines.EZE2TRAVEL && x.AirPortID == aDestinationAirPort.AirPortID);
            if (lOutput != null && lOutput.Count() > 0)
                return lOutput.ToList();

            return new List<AIDefaultFilter>();
        }

        public int DeleteSKUs(List<FilteredAccommodation> accommodations)
        {
            mContext.FilteredAccommodations.RemoveRange(mContext.FilteredAccommodations);
            return mContext.SaveChanges();
        }

        public AIFilter GetAIFilter(int aID)
        {
            return mContext.AIFilters.Where(x => x.AIFilterID == aID).FirstOrDefault();
        }

        public Filter Get(int aID)
        {
            return mContext.Filters
                .Include(x => x.AirFilters)
                .Include(x => x.QuoteGroup).ThenInclude(sub => sub.QuoteRequest).ThenInclude(sub1 => sub1.DepartureAirPort)
                .Include(x => x.QuoteGroup).ThenInclude(sub => sub.QuoteRequest).ThenInclude(sub1 => sub1.DestinationAirPort)
                .Include(x => x.OutboundFilters)
                .Include(x => x.InboundFilters)
                .Include(x => x.Accommodations)
                .Include(x => x.InboundFilters)
                .Where(x => x.FilterID == aID)
                .FirstOrDefault();
        }

        public QuoteRequest GetQuoteRequest(Filter aFilter)
        {
            var lQRID = (from Q in mContext.QuoteGroups
                         join F in mContext.Filters on Q.Id equals F.QuoteGroupID
                         where Q.Id == aFilter.QuoteGroupID
                         select Q.QuoteRequestID).FirstOrDefault();
            return new QuoteRequestDataAccess(mContext).Get(lQRID);
        }

        public int Save(AIFilter aFilter, bool aSaveChanges = true)
        {
            string FuncName = ClassName + $"Save (AIFilter = ({aFilter.ID}, CommitChanges = ({aSaveChanges}))";
            // Need to Add all the Filters because the Key is not unique
            // This is done on purpose
            if (GetAIFilter(aFilter.ID) == null)
                mContext.AIFilters.Add(aFilter);

            if (aSaveChanges)
            {
                try
                {
                    var lCnt = mContext.SaveChanges();
                    Logger.LogInfo(FuncName + $" Saved {lCnt} Filter Rows");
                    return lCnt;
                }
                catch (Exception e)
                {
                    Logger.LogException(FuncName + " Failed to save Filter", e);
                }
            }

            return 0;
        }

        void CleanUpAcommodations(Filter aFilter)
        {
            var lResortFilters = new Dictionary<int, List<int>>();
            foreach (var lInclAccom in aFilter.Accommodations)
                if (lInclAccom.IncludedSKUs != null)
                    lResortFilters.Add(lInclAccom.AccommodationID, lInclAccom.IncludedSKUs.Select(x => x.SKUId).ToList());
                else
                {
                    if (lResortFilters.Keys.Contains(lInclAccom.AccommodationID) == false)
                        lResortFilters.Add(lInclAccom.AccommodationID, new List<int>());
                }
            DeleteExistingAccommFilters(aFilter);
            foreach (var lFAId in lResortFilters.Keys)
            {
                var lIncludedSkus = GetIncludedSKUs(lFAId, lResortFilters[lFAId]);
                if (lIncludedSkus.Count() >= 0)
                    aFilter.Accommodations.Add(new FilteredAccommodation() { AccommodationID = lFAId, FilterID = aFilter.FilterID, IncludedSKUs = lIncludedSkus });
            }
        }

        private List<IncludedSKUs> GetIncludedSKUs(int aProviderId, List<int> aSKUs)
        {
            var lOutput = new List<IncludedSKUs>();
            foreach (var aSKUId in aSKUs)
                lOutput.Add(new IncludedSKUs() { FilteredAccommodationId = aProviderId, SKUId = aSKUId });
            return lOutput;
        }

        public IEnumerable<SKU> GetFilteredSKUs(Filter aFitler)
        {
            return mContext.FilteredAccommodations.Where(x => x.FilterID == aFitler.FilterID).SelectMany(x => x.IncludedSKUs).Select(x => x.SKU);
        }
        public int Save(Filter aFilter, bool aSaveChanges = true)
        {
            CleanUpAcommodations(aFilter);

            string FuncName = ClassName + $"Save (Filter = ({aFilter.FilterID}, CommitChanges = ({aSaveChanges}))";
            if (aFilter.FilterID == 0)
                mContext.Filters.Add(aFilter);
            else
                mContext.Filters.Update(aFilter);

            if (aSaveChanges)
            {
                try
                {
                    var lCnt = mContext.SaveChanges();
                    Logger.LogInfo(FuncName + $" Saved {lCnt} Filter Rows");
                    return lCnt;
                }
                catch (Exception e)
                {
                    Logger.LogException(FuncName + " Failed to save Filter", e);
                }
            }

            return 0;

        }

        private int DeleteExistingAccommFilters(Filter aFilter)
        {
            mContext.FilteredAccommodations.RemoveRange(mContext.FilteredAccommodations.Where(x => x.FilterID == aFilter.FilterID));
            aFilter.Accommodations = new List<FilteredAccommodation>();
            return mContext.SaveChanges();
        }

        /// <summary>
        /// Get Filter for Quote
        /// </summary>
        /// <param name="id">Quote ID</param>
        /// <returns></returns>
        //public Filter GetByQuoteID(int id)
        //{
        //    return (from Q in mContext.Quotes
        //            join F in mContext.Filters on Q.QuoteID equals F.QuoteID
        //            where Q.QuoteID == id
        //            select F).FirstOrDefault();
        //}

        public QuoteGroup GetQuoteByFilterID(int aFilterID)
        {
            return (mContext.Filters
                .Include(x => x.QuoteGroup)
                .Where(x => x.FilterID == aFilterID)
                .Select(x => x.QuoteGroup)).FirstOrDefault();
        }
    }
}
