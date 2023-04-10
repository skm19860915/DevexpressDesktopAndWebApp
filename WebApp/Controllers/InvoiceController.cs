using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlitzerCore.Business;
using BlitzerCore.Models;
using Microsoft.AspNetCore.Authorization;
using BlitzerCore.UIHelpers;

namespace WebApp.Controllers
{
    [Authorize]
    public class InvoiceController : BaseController
    {
        public InvoiceController(IDbContext aContext) : base (aContext)
        {
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Trip ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Trip(int id)
        {
            Trip lTrip = new TripBusiness(mContext).Get(id);
            var lInvoice = new InvoiceBusiness(mContext).Get(lTrip);
            var lUIInvoice = InvoiceUIHelper.Convert(lInvoice);
            return View(lUIInvoice);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Invoice Id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Detail(int id)
        {
            var lInvoice = new InvoiceBusiness(mContext).Get(id);
            var lUIInvoice = InvoiceUIHelper.Convert(lInvoice);
            return View("Trip", lUIInvoice);
        }

        // POST: Invoice/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Invoice/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        // POST: Invoice/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Invoice/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: Invoice/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}