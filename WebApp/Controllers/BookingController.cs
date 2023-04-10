using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Authorization;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Business;
using BlitzerCore.Helpers;
using BlitzerCore.Utilities;
using BlitzerCore.UIHelpers;
using WebApp.AspNetHelper;
using BlitzerCore.DataAccess;
using WebApp.DataServices;
using Microsoft.Extensions.Configuration;

namespace WebApp.Controllers
{
    [Authorize]
    public class BookingController : BaseController
    {
        const string ClassName = "BookingController::";
        public IConfiguration Configuration { get; }

        public BookingController(IDbContext aContext, UserManager<BlitzerUser> userManager, IConfiguration aConfiguration) : base(aContext, userManager)
        {
            Configuration = aConfiguration;
        }

        // GET: BookingController
        public ActionResult Index()
        {
            return View();
        }

        // GET: BookingController/Edit/5
        public async Task<ActionResult> Create(int id)
        {
            UIBooking lBooking = new UIBooking() { TripID = id, BookingNumber = "TBD" };
            ViewBag.TourOperators = ListHelper.GetTourOperators(new BlitzerBusiness(mContext, null).GetTourOperators(GetCurrentUser()));
            ViewBag.Suppliers = ListHelper.GetSuppliers(new BlitzerBusiness(mContext, null).GetSuppliers(GetCurrentUser()));
            await SetReturnUrl(lBooking);
            return View(lBooking);
        }

        // POST: BookingController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] UIBooking aBooking)
        {
            try
            {
                Booking lBooking = BookingUIHelper.Convert(DbContext, aBooking);
                int Cnt = new BookingBusiness(mContext, Configuration).Save(lBooking, GetCurrentAgent());
                return ReturnToCaller(aBooking);
            }
            catch ( Exception e)
            {
                BlitzerCore.Utilities.Logger.LogException("Failed to save booking", e);
                return View();
            }
        }


        // GET: BookingController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            string FuncName = ClassName + $"Edit (id = {id}) ";
            try
            {
                var lBooking = new BookingBusiness(mContext, Configuration).Get(id);
                var lUIBooking = BookingUIHelper.Convert(lBooking);
                lUIBooking.Trip = TripUIHelper.Convert(new TripBusiness(DbContext).Get(lUIBooking.TripID));
                ViewBag.TourOperators = ListHelper.GetTourOperators(new BlitzerBusiness(mContext, Configuration).GetTourOperators(GetCurrentUser()));
                ViewBag.Suppliers = ListHelper.GetSuppliers(new BlitzerBusiness(mContext, Configuration).GetSuppliers(GetCurrentUser()));
                await SetReturnUrl(lUIBooking);
                return View(lUIBooking);
            }catch ( Exception e )
            {
                BlitzerCore.Utilities.Logger.LogException($"{FuncName}Failed to Get Booking", e);
            }

            return RedirectToAction(nameof(PortalController.Index), nameof(PortalController));
        }

        // POST: BookingController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] UIBooking aBooking)
        {
            string FuncName = ClassName + $"Edit (UIBooking = {aBooking.BookingID})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lBooking = BookingUIHelper.Convert(DbContext, aBooking);
                int lCnt = new BookingBusiness(mContext, Configuration).Save(lBooking, GetCurrentAgent());
                return ReturnToCaller(aBooking);
            }
            catch ( Exception e )
            {
                BlitzerCore.Utilities.Logger.LogException("Failed to save booking", e);
            } finally
            {
                Logger.LeaveFunction(FuncName);
            }

            return RedirectToAction(nameof(TripController.Details), "Trip", new { id = aBooking.TripID });
        }

        // GET: BookingController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BookingController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
