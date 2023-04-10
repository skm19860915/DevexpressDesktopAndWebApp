using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using BlitzerCore.Business;
using BlitzerCore.DataAccess;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using BlitzerCore.UIHelpers;
using BlitzerCore.Helpers;
using Microsoft.Extensions.Configuration;

namespace WebApp.Controllers
{
    [Authorize]
    public class CreditController : BaseController
    {
        const string ClassName = "FOPController::";
        public IConfiguration Configuration { get; }

        public CreditController(IDbContext aContext, IConfiguration aConfiguration) : base(aContext)
        {
            Configuration = aConfiguration;
        }

        // GET: CreditController
        public ActionResult Index()
        {
            var lCredits = new CreditBusiness(DbContext, Configuration).Get(GetCurrentAgent());
            var lUICredits = CreditUIHelper.Convert(lCredits);
            return View(lUICredits);
        }

        // GET: CreditController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CreditController/Create
        public ActionResult Create(int id)
        {
            int aBookingId = id;
            var lBooking = new BookingBusiness(mContext, Configuration).Get(id);
            var lUICredit = new UICredit() { Amount = DataHelper.ConvertToCurrency(lBooking.Payments.Sum(x=>x.Amount)), Reference = lBooking.BookingNumber, OriginalBookingId = aBookingId, When = DateTime.Now.AddYears(1)};
            return View("Edit", lUICredit);
        }

        // POST: CreditController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] UICredit aUICredit)
        {
            try
            {
                var lBooking = new BookingBusiness(DbContext, Configuration).Get(aUICredit.OriginalBookingId);
                new CreditBusiness(DbContext, Configuration).Save(aUICredit);
                return RedirectToAction("Edit", "Booking", new { id = aUICredit.OriginalBookingId });
            }
            catch
            {
                return View();
            }
        }

        // GET: CreditController/Edit/5
        public ActionResult Edit(int id)
        {
            var lCredit = new CreditBusiness(DbContext, Configuration).Get(id);
            var lUICredit = CreditUIHelper.Convert(lCredit);
            return View(lUICredit);
        }

        // GET: CreditController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CreditController/Delete/5
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
