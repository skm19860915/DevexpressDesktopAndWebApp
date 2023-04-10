using Microsoft.AspNetCore.Mvc;
using System;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using BlitzerCore.DataAccess;

namespace WebApp.Controllers
{
    public class GermanyController : BaseController
    {
        public int CountryID { get; set; }

        public GermanyController(IDbContext aContext) : base(aContext)
        {
            CountryID = 21;
        }

        public IActionResult Index()
        {
            try
            {
                var lPage = new CountryPageDataAccess(mContext).Get(CountryID);
                return View(lPage);
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to retreieve Germany Page[" + CountryID + "]", e);
                throw new InvalidOperationException();
            }
        }

        // GET: CountryController/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
                throw new InvalidOperationException("Invalid ID passed to CountryController");

            try
            {
                return View(new CountryPageDataAccess(mContext).Get(id));
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
        public ActionResult Edit([FromForm] UICountry aCountry)
        {
            try
            {
                new CountryPageDataAccess(mContext).Save(aCountry);
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to save Country Page", e);
            }
            return RedirectToAction(nameof(PageController.Index), "Page");
        }

    }
}
