using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using BlitzerCore.DataAccess;
using BlitzerCore.Business;
using BlitzerCore.UIHelpers;
using BlitzerCore.Helpers;
using WebApp.AspNetHelper;
using WebApp.Services;
using WebApp.DataServices;
using BlitzerCore.Models.Exceptions;

namespace WebApp.Controllers
{
    public class PaymentController : BaseController
    {
        const string ClassName = "PaymentController::";
        public IConfiguration Configuration { get; }

        public PaymentController(IDbContext context, UserManager<BlitzerUser> userManager, IConfiguration aConfiguration) : base(context, userManager)
        {
            Configuration = aConfiguration;
        }
        // GET: PaymentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PaymentController/Create
        public ActionResult Create(int id)
        {
            string FuncName = $"{ClassName}Create (id={id})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lBooking = new BookingBusiness(DbContext, Configuration).Get(id);
                var lUIPayment = new PaymentBusiness(DbContext, Configuration).Create(lBooking);
                var lTrip = new TripBusiness(DbContext).Get(lBooking.TripID);
                ViewBag.Travelers = ListHelper.GetTravelers(lTrip.Travelers);
                ViewBag.Cards = ListHelper.GetCreditCards(DbContext, lTrip.Travelers);
                return View(lUIPayment);
            } catch ( Exception e )
            {
                Logger.LogException(FuncName + $" Failed to create UIPayment", e);
            } finally
            {
                Logger.LeaveFunction(FuncName);
            }

            return RedirectToAction("Edit", "Booking", new { id = id });
        }

        // POST: PaymentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] UIPayment aPayment)
        {
            string FuncName = $"{ClassName}Create (UIPayment={aPayment.PaymentId})";
            Logger.EnterFunction(FuncName);
            try
            {
                new PaymentBusiness(DbContext, Configuration).Save(aPayment, GetCurrentAgent());
            } catch ( BookingDoesnotExist )
            {
                var lBooking = new BookingBusiness(DbContext, Configuration).Get(aPayment.BookingId);
                var lUIPayment = new PaymentBusiness(DbContext, Configuration).Create(lBooking);
                var lTrip = new TripBusiness(DbContext).Get(lBooking.TripID);
                ModelState.AddModelError("Error", "Booking not found in agents portal");
                ViewBag.Travelers = ListHelper.GetTravelers(lTrip.Travelers);
                ViewBag.Cards = ListHelper.GetCreditCards(DbContext, lTrip.Travelers);
                return View(aPayment);
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + $" Failed to Save UIPayment", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }

            return RedirectToAction("Edit", "Booking", new { id = aPayment.BookingId });
        }

        // GET: PaymentController/Edit/5
        /// <summary>
        /// Edit a payment for a trip
        /// </summary>
        /// <param name="id">Payment ID</param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            string FuncName = $"{ClassName}Edit (id={id})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lPayment = new PaymentBusiness(DbContext, Configuration).Get(id);
                var lUIPayment = PaymentUIHelper.Convert(lPayment);
                var lTrip = new TripBusiness(DbContext).Get(lPayment.Booking.TripID);
                ViewBag.Travelers = ListHelper.GetTravelers(lTrip.Travelers);
                ViewBag.Cards = ListHelper.GetCreditCards(DbContext, lTrip.Travelers);
                IsDanger(GetCurrentUser());
                return View(lUIPayment);
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + $" Failed to create UIPayment", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }

            return RedirectToAction("Edit", "Booking", new { id = id });
        }

        // POST: PaymentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] UIPayment aPayment)
        {
            string FuncName = $"{ClassName}Edit (UIPayment={aPayment.PaymentId})";
            Logger.EnterFunction(FuncName);
            try
            {
                new PaymentBusiness(DbContext, Configuration).Save(aPayment, GetCurrentAgent());
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + $" Failed to Save UIPayment", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }

            return RedirectToAction("Edit", "Booking", new { id = aPayment.BookingId });
        }

        // GET: PaymentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PaymentController/Delete/5
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
