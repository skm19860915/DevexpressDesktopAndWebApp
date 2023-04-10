using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Business;
using BlitzerCore.UIHelpers;
using BlitzerCore.DataAccess;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using BlitzerCore.Helpers;
using WebApp.AspNetHelper;
using WebApp.DataServices;
using Microsoft.Extensions.Configuration;

namespace WebApp.Controllers
{
    public class ReconcileController : BaseController
    {
        const string ClassName = "BookingController::";
        public IConfiguration Configuration { get; }

        public ReconcileController(IDbContext aContext, IConfiguration aConfiguration) : base(aContext)
        {
            Configuration = aConfiguration;
        }
        // GET: ReconcileController
        public ActionResult Index()
        {
            List<Agent> lAgents = new List<Agent>();
            var lBookingBiz = new BookingBusiness(DbContext, Configuration);
            var lBookings = lBookingBiz.Get(DateTime.Now);
            // Fix bug of Agents not populate with Objects
            var lAgentIDs = lBookings.Select(x => x.Trip.AgentId.ToUpper()).Distinct();
            foreach (var lAgentId in lAgentIDs)
                lAgents.Add(new ContactBusiness(DbContext).GetAgent(lAgentId));
            foreach (var lBooking in lBookings)
                lBooking.Trip.Agent = lAgents.First(x => x.Id.ToUpper() == lBooking.Trip.AgentId.ToUpper());
            var lUIBookings = BookingUIHelper.Convert(lBookings);
            return View(lUIBookings);
        }

        // GET: ReconcileController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var lBooking = new BookingBusiness(mContext, Configuration).Get(id);
                var lUIBooking = BookingUIHelper.Convert(lBooking);
                lUIBooking.Trip = TripUIHelper.Convert(new TripBusiness(DbContext).Get(lUIBooking.TripID));
                ViewBag.TourOperators = ListHelper.GetTourOperators(new BlitzerBusiness(mContext, null).GetTourOperators(GetCurrentUser()));
                ViewBag.Suppliers = ListHelper.GetSuppliers(new BlitzerBusiness(mContext, null).GetSuppliers(GetCurrentUser()));
                await SetReturnUrl(lUIBooking);
                return View(lUIBooking);
            }
            catch (Exception e)
            {
                BlitzerCore.Utilities.Logger.LogException("Failed to Get Booking", e);
            }

            return RedirectToAction(nameof(PortalController.Index), nameof(PortalController));

        }

        // POST: ReconcileController/Edit/5
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
            catch (Exception e)
            {
                BlitzerCore.Utilities.Logger.LogException("Failed to save booking", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }

            return RedirectToAction(nameof(ReconcileController.Index), "Reconcile");
        }

        // GET: ReconcileController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReconcileController/Delete/5
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
