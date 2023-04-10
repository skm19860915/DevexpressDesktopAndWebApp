using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography.Xml;
using System.Threading;
using System.Security.Policy;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using BlitzerCore.DataAccess;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Business;
using BlitzerCore.Utilities;
using BlitzerCore.UIHelpers;
using WebApp.Services;

namespace WebApp.Controllers
{
    [Authorize]
    public class FilterController : BaseController
    {
        const string ClassName = "FilterController::";

        public IConfiguration Configuration { get; }
        public IWebTravelSrv mTravelSrv = null;
        private static Mutex mMutex = new Mutex();
        
        public FilterController(IDbContext context) : base(context)
        {
        }

        public ActionResult Edit(int id)
        {
            var lUIFilter = new FilterBusiness(mContext).GetView(id);
            CheckHotels(lUIFilter);
            return View(lUIFilter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">QuoteGroup ID</param>
        /// <returns></returns>
        public ActionResult IncludeRoomTypes(int id)
        {
            string FuncName = ClassName + $"IncludeRoomTypes(int = {id})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lQuoteGroup = new QuoteGroupBusiness(mContext).Get(id);
                IEnumerable<SKU> lSKUs = new QuoteDataAccess(mContext).GetRoomTypes(lQuoteGroup);
                var lUISKUs = SKUUIHelper.Convert(lSKUs).OrderBy(x => x.Provider).ThenBy(x1 => x1.Name).ToList();
                var lFilter = new QuoteGroupBusiness(mContext).GetFilter(lQuoteGroup);
                var lMarkedSkus = new FilterBusiness(mContext).GetFilteredRoomTypes(lFilter);
                if (lMarkedSkus != null) {
                    var lMarkedIDs = lMarkedSkus.Select(x => x.SKUID);
                    lUISKUs.Where(x => lMarkedIDs.Contains(x.Id)).ToList().ForEach(y => y.Checked = true);
                }
                var lUISKUFilter = new UISKUFilter();
                lUISKUFilter.SKUs = lUISKUs;
                lUISKUFilter.FilterId = new QuoteGroupBusiness(mContext).GetFilter(lQuoteGroup).FilterID;
                return View(lUISKUFilter);
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to retrieve quote view", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }

            throw new InvalidOperationException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">QuoteGroup ID</param>
        /// <returns></returns>
        public ActionResult IncludeSubRoomTypes(int id)
        {
            string FuncName = ClassName + $"IncludeSubRoomTypes(int = {id})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lQuoteGroup = new QuoteGroupBusiness(mContext).Get(id);
                IEnumerable<SKU> lSKUs = new QuoteDataAccess(mContext).GetSubRoomTypes(lQuoteGroup);
                var lUISKUs = SKUUIHelper.Convert(lSKUs).OrderBy(x => x.Provider).ThenBy(x1 => x1.Name).ToList();
                var lFilter = new QuoteGroupBusiness(mContext).GetFilter(lQuoteGroup);
                var lMarkedSkus = new FilterBusiness(mContext).GetFilteredRoomTypes(lFilter);
                if (lMarkedSkus != null)
                {
                    var lMarkedIDs = lMarkedSkus.Select(x => x.SKUID);
                    lUISKUs.Where(x => lMarkedIDs.Contains(x.Id)).ToList().ForEach(y => y.Checked = true);
                }
                var lUISKUFilter = new UISKUFilter();
                lUISKUFilter.SKUs = lUISKUs;
                lUISKUFilter.FilterId = new QuoteGroupBusiness(mContext).GetFilter(lQuoteGroup).FilterID;
                return View("IncludeRoomTypes", lUISKUFilter);
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to retrieve quote view", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }

            throw new InvalidOperationException();
        }


        // POST: ClientController/Quote
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IncludeRoomTypes([FromForm] UISKUFilter aSKUFilter)
        {
            var lSKUs = aSKUFilter.SKUs.Where(x => x.Checked == true).ToList();
            var lFilter = new FilterDataAccess(mContext).Get(aSKUFilter.FilterId);
            var lProviders = lSKUs.Select(x => x.ProviderId).Distinct();

            // Get the Old IncludeSku Filter for the resorts and delete them
            new FilterDataAccess(mContext).DeleteSKUs(lFilter.Accommodations);

            foreach (int lProverId in lProviders)
            {
                lFilter.Accommodations.Add(
                    new FilteredAccommodation()
                    {
                        AccommodationID = lProverId,
                        FilterID = lFilter.FilterID,
                        IncludedSKUs = GetIncludedSKUs(lSKUs.Where(x => x.ProviderId == lProverId))
                    }
                );
            }
            new FilterDataAccess(mContext).Save(lFilter);
            new QuoteGroupBusiness(mContext).ApplyFilter(lFilter);
            new FilterBusiness(mContext).Save(lFilter);
            return RedirectToAction("Edit", "QuoteRequest", new { id = lFilter.QuoteRequestID });
        }

        // POST: ClientController/Quote
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IncludeSubRoomTypes([FromForm] UISKUFilter aSKUFilter)
        {
            var lSKUs = aSKUFilter.SKUs.Where(x => x.Checked == true).ToList();
            var lFilter = new FilterDataAccess(mContext).Get(aSKUFilter.FilterId);
            var lProviders = lSKUs.Select(x => x.ProviderId).Distinct();

            // Get the Old IncludeSku Filter for the resorts and delete them
            new FilterDataAccess(mContext).DeleteSKUs(lFilter.Accommodations);

            foreach ( int lProverId in lProviders)
            {
                lFilter.Accommodations.Add(
                    new FilteredAccommodation()
                    {
                        AccommodationID = lProverId,
                        FilterID = lFilter.FilterID,
                        IncludedSKUs = GetIncludedSKUs(lSKUs.Where(x => x.ProviderId == lProverId))
                    }
                );
            }
            new FilterDataAccess(mContext).Save(lFilter);
            new QuoteGroupBusiness(mContext).ApplySubFilter(lFilter);
            new FilterBusiness(mContext).Save(lFilter);
            return RedirectToAction("Edit", "QuoteRequest", new { id = lFilter.QuoteRequestID });
        }

        private List<IncludedSKUs> GetIncludedSKUs(IEnumerable<UISKU> lSKUs)
        {
            var lOutput = new List<IncludedSKUs>();
            foreach ( var lSKU in lSKUs)
                lOutput.Add(new IncludedSKUs() { FilteredAccommodationId = lSKU.ProviderId, SKUId = lSKU.Id });

            return lOutput;
        }

        private void CheckHotels(BlitzerCore.Models.UI.UIFilter lUIFilter)
        {
            if (lUIFilter.Accommondations == null)
                return;

            lUIFilter.Accommondations.Where(x=> lUIFilter.SelectedAccommondations.Contains(x.ID)).ToList().ForEach(x => x.Checked = true);
        }

        public Filter Convert (BlitzerCore.Models.UI.UIFilter aFilter )
        {
            return new QuoteBusiness(mContext).Convert(aFilter);
        }

        /// <summary>
        /// After user updates the filter critia, save the filter and then show them the new quote
        /// </summary>
        /// <param name="aFilter"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit([FromForm] BlitzerCore.Models.UI.UIFilter aFilter)
        {
            // this is to prevent a double click by the client
            mMutex.WaitOne();

            try
            {
                var lQuoteBiz = new QuoteBusiness(mContext);
                if (aFilter.Accommondations != null)
                {
                    var lAnyAccom = aFilter.Accommondations.Where(x => x.Checked == true);
                    if (lAnyAccom != null)
                        aFilter.SelectedAccommondations = lAnyAccom.Select(y => y.ID).ToList();
                }

                // Total HACK!!!!!
                if (aFilter.SelectedLocation == "All")
                    aFilter.SelectedLocation = null;
                var lFilter = Convert(aFilter);
                new FilterBusiness(DbContext).Save(lFilter);
                var lQGBiz = new QuoteGroupBusiness(DbContext);
                var lQG = lQGBiz.GetQuoteGroup(lFilter);
                lQGBiz.ApplySubFilter(lFilter);
                new FilterBusiness(DbContext).Save(lFilter);
                return RedirectToAction("Edit", "QuoteRequest", new { id = lQG.QuoteRequestID });
            } finally
            {
                mMutex.ReleaseMutex();
            }
        }
    }
}
