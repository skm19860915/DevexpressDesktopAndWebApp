using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Policy;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Models.ASP;
using BlitzerCore.Utilities;
using BlitzerCore.Business;
using BlitzerCore.Helpers;
using BlitzerCore.DataAccess;
using BlitzerCore.UIHelpers;
using WebApp.SrvUtilities;
using WebApp.Services;
using WebApp.AspNetHelper;

namespace WebApp.Controllers
{
    public class QuoteController : BaseController
    {
        const string ClassName = "QuoteController::";
        public IConfiguration Configuration { get; }

        public QuoteController(IDbContext context, IConfiguration aConfig) : base(context)
        {
            Configuration = aConfig;

        }

        /// <summary>
        /// Create new Quote from Quote Request ID
        /// </summary>
        /// <param name="id">Quote Group ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult New(int id)
        {
            string FuncName = ClassName + $"New (QuoteGroupId = {id})";
            Logger.EnterFunction(FuncName);

            try
            {
                var lQuoteGroup = new QuoteGroupBusiness(mContext).Get(id);
                UIQuote lUIQuote = PopulateQuote(lQuoteGroup);
                if (lUIQuote == null)
                    return RedirectToAction("Index", "Portal");

                return View("Edit", lUIQuote);
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to edit quote", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }

            throw new InvalidOperationException();
        }

        private UIQuote PopulateQuote(QuoteGroup aQuoteGroup)
        {
            string lId = aQuoteGroup != null ? aQuoteGroup.Id.ToString() : "Null";
            string FuncName = ClassName + $"PopulateQuote (id = {lId})";
            if (aQuoteGroup == null)
            {
                Logger.LogWarning(FuncName + " - Can't create new quote because invalid quote request passed to quote controller");
                return null;
            }
            else
            {
                var lQBiz = new QuoteBusiness(mContext);
                var lQuote = lQBiz.CreateQuote(aQuoteGroup);
                lQuote.QuoteRequest = new QuoteRequestBusiness(mContext).Get(lQuote.QuoteRequestID);
                var lUIQuote = QuoteUIHelper.Convert(mContext, lQuote);
                ViewBag.TourOperators = ListHelper.GetTourOperators(new BlitzerBusiness(mContext, null).GetTourOperators(GetCurrentUser()));
                ViewBag.Suppliers = ListHelper.GetSuppliers(new BlitzerBusiness(mContext, null).GetSuppliers(GetCurrentUser()));
                ViewBag.RoomTypes = ListHelper.GetProductTypes(new AccommodationDataAccess(mContext).GetAllRoomTypes().Cast<SKU>().ToList());
                lUIQuote.QuoteRequestID = lQuote.QuoteRequestID;
                return lUIQuote;
            }
        }

        private void PopulateLookups(ASPQuote aUIQuote)
        {
            aUIQuote.Resorts = ListHelper.GetResorts(new HotelBusiness(mContext).GetList(GetCurrentAgent()));
            aUIQuote.ResortRoomTypes = (List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>)ListHelper.GetProductTypes(new AccommodationDataAccess(mContext).GetAllRoomTypes(null).Cast<SKU>().ToList());
            aUIQuote.TourOperators = ListHelper.GetTourOperators(new TourOperatorDataAccess(mContext).GetAll());
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            string FuncName = ClassName + $"Edit (QuoteId = {id})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lQBiz = new QuoteBusiness(mContext);
                UIQuote lUIQuote = null;
                if (id > 0)
                {
                    var lQuote = lQBiz.GetQuote(id);
                    lUIQuote = QuoteUIHelper.Convert(mContext, lQuote);
                    lUIQuote.QuoteRequestID = lQuote.QuoteRequestID;
                }
                else
                {
                    var lQuote = lQBiz.GetBotQuote(-id);
                    lQuote.Exclude = !lQuote.Exclude;
                    lQBiz.Save(lQuote);
                    return Redirect(Request.Headers["Referer"].ToString());
                }
                ViewBag.TourOperators = ListHelper.GetTourOperators(new BlitzerBusiness(mContext, Configuration).GetTourOperators(GetCurrentUser()));
                ViewBag.Suppliers = ListHelper.GetSuppliers(new BlitzerBusiness(mContext, Configuration).GetSuppliers(GetCurrentUser()));
                List<SKU> lProdTypes = new AccommodationDataAccess(mContext).GetAllRoomTypes().Cast<SKU>().ToList();
                ViewBag.SKU = ListHelper.GetProductTypes(lProdTypes);
                return View(lUIQuote);
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + " Failed to edit quote", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }

            throw new InvalidOperationException();
        }

        // POST: QuoteController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] UIQuote aQuote)
        {
            string FuncName = ClassName + $"Edit (UIQuote = {aQuote.QuoteID})";
            bool lJustBooked = false;
            bool lJustSent = false;

            try
            {
                var lOldQuote = new QuoteBusiness(mContext).Get(aQuote.QuoteID);

                if ((lOldQuote == null && aQuote.Status == QuoteStatus.Booked) ||
                (lOldQuote != null && lOldQuote.Status != QuoteStatus.Booked && aQuote.Status == QuoteStatus.Booked))
                    lJustBooked = true;

                if ((lOldQuote == null && aQuote.Status == QuoteStatus.Sent) ||
                (lOldQuote != null && lOldQuote.Status != QuoteStatus.Sent && aQuote.Status == QuoteStatus.Sent))
                    lJustSent = true;

                var lQRBiz = new QuoteRequestBusiness(mContext);
                var lQR = lQRBiz.Get(aQuote.QuoteRequestID);
                var lQG = lQRBiz.GetOpenQuoteGroup(lQR);
                var llQuote = QuoteUIHelper.Convert(DbContext, aQuote);
                foreach (var lFlight in llQuote.Flights)
                {
                    lFlight.QuoteGroup = lQG;
                    lFlight.QuoteRequestID = aQuote.QuoteRequestID;
                }
                llQuote.QuoteGroupID = lQG.Id;
                llQuote.QuoteGroup = lQG;
                lQG.Quotes.Add(llQuote);
                new QuoteBusiness(mContext).Save(llQuote);

                if (lJustBooked)
                {
                    aQuote.QuoteID = llQuote.QuoteID;
                    new TripBusiness(mContext, Configuration).Book(aQuote, GetCurrentUser());
                    return RedirectToAction("Index", "Portal");
                }
                else if (lJustSent)
                {
                    new QuoteGroupBusiness(DbContext).SendQuoteGroup(GetCurrentAgent(), lQG, false);
                    return RedirectToAction("Index", "Portal");
                }
                else
                    return RedirectToAction(nameof(OpportunityController.Edit), "QuoteRequest", new { id = aQuote.QuoteRequestID });

            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + " Failed to Edit Quote", e);
                throw new InvalidOperationException(e.Message);
            }
        }

        // POST: BookingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New([FromForm] UIQuote aQuote)
        {
            string FuncName = ClassName + $"New (UIQuote = {aQuote.QuoteID})";
            Logger.EnterFunction(FuncName);

            if (!ModelState.IsValid)
            {
                var lQuote = new QuoteBusiness(mContext).Get(aQuote.QuoteID);
                return View("Edit", QuoteUIHelper.Convert(mContext, lQuote));
            }
            try
            {
                var lQRBiz = new QuoteRequestBusiness(mContext);
                var lQR = lQRBiz.Get(aQuote.QuoteRequestID);
                var lQG = lQRBiz.GetOpenQuoteGroup(lQR);
                var lQuote = QuoteUIHelper.Convert(DbContext, aQuote);
                lQuote.QuoteGroupID = lQG.Id;
                lQuote.QuoteGroup = lQG;
                lQG.Quotes.Add(lQuote);
                new QuoteBusiness(DbContext).Save(lQuote);
                return RedirectToAction(nameof(OpportunityController.Edit), "QuoteRequest", new { id = aQuote.QuoteRequestID });
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to Book Quote", e);
                throw new InvalidOperationException(e.Message);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }

        [HttpGet]
        public ActionResult View(int id)
        {
            string FuncName = ClassName + $"View(int = {id})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lQBiz = new QuoteBusiness(mContext);
                var lQuote = lQBiz.GetQuote(id);
                if (lQuote == null)
                {
                    Logger.LogWarning("Can't dislay QuoteView because Quote was null for id = " + id);
                    return RedirectToAction(nameof(PortalController.Index), "Portal");
                }
                var lUIQuote = QuoteUIHelper.Convert(mContext, lQuote);
                //lUIQuote.Name = lQBiz.GetQuoteName(lQuote);
                //lUIQuote.FilterID = lQBiz.GetFilterFromQuote(lQuote).FilterID;
                return View(lUIQuote);
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
    }
}
