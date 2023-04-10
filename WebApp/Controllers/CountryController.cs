using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using BlitzerCore.Business;
using BlitzerCore.DataAccess;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    public class CountryController : BaseController
    {
        public CountryController(IDbContext aContext) : base(aContext)
        {

        }
        // GET: CountryController
        public ActionResult Index()
        {

            return View();
        }

        // GET: CountryController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var lPage = new CountryPageDataAccess(mContext).Get(id);
                return View(lPage);
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to retreieve CountryPage[" + id + "]", e);
                throw new InvalidOperationException();
            }
        }

        private UICountry GetPage(int id)
        {
            return new CountryPageDataAccess(mContext).Get(id);
        }

        // GET: CountryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CountryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
        [Authorize]
        public ActionResult AddBlock(int id)
        {
            if (id == 0)
                return RedirectToAction(nameof(CountryController.Edit), new { id = id });

            try
            {
                new CountryPageBusiness(mContext).AddBlock(id);
                return RedirectToAction(nameof(CountryController.Edit), new { id = id });
            }
            catch (Exception e)
            {
                Logger.LogException("Failed tedit CountryPage [" + id + "]", e);
            }

            throw new InvalidOperationException();

        }

        // GET: CountryController/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            if (id == 0)
                throw new InvalidOperationException("Invalid ID passed to CountryController");

            try
            {
                var lPage = View(new CountryPageDataAccess(mContext).Get(id));
                return lPage;
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to retreieve edit CountryPage [" + id + "]", e);
            }

            throw new InvalidOperationException();

        }

        // POST: CountryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([FromForm] UICountry aCountry)
        {
            try
            {
                var lCnt = new CountryPageDataAccess(mContext).Save(aCountry);
                Logger.LogInfo("Saved " + lCnt + "Records");
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to save Country Page", e);
            }
            return RedirectToAction(nameof(PageController.Index), "Page");
        }

        // GET: CountryController/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CountryController/Delete/5
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
