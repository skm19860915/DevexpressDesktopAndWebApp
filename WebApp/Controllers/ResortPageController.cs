using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using BlitzerCore.Business;
using BlitzerCore.DataAccess;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;

namespace WebApp.Controllers
{
    public class ResortPageController : BaseController
    {
        const string ClassName = "ResortPageController::";

        public ResortPageController(IDbContext aContext) : base(aContext)
        {

        }
        // GET: ResortController
        public ActionResult Index()
        {
            return View(new HotelBusiness(DbContext).GetAll());
        }

        // GET: ResortController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                return View(new ResortPageBusiness(mContext).Get(id));
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to retreieve ResortPage[" + id + "]", e);
                throw new InvalidOperationException();
            }
        }

        // GET: ResortController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ResortController/Create
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


       /// <summary>
       /// Display the Marketing page for the hotel
       /// </summary>
       /// <param name="id">Hotel Page ID</param>
       /// <returns></returns>
        public ActionResult Edit(int id)
        {
            string FuncName = $"{ClassName}::Edit({id}) - ";

            if (id == 0)
            {
                Logger.LogError("Invalid Resort Page passed to id [" + id + "]");
                throw new InvalidOperationException();
            }

            try
            {
                var lUIPage = new ResortPageBusiness(mContext).Get(id);
                return View(lUIPage);
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to retreieve edit page [" + id + "]", e);
            }

            throw new InvalidOperationException();

        }

        /// <summary>
        /// Display the Marketing page for the hotel
        /// </summary>
        /// <param name="id">Hotel ID</param>
        /// <returns></returns>
        public ActionResult Summary(int id)
        {
            string FuncName = $"{ClassName}::Summary({id}) - ";

            if (id == 0)
                throw new InvalidOperationException("Invalid ID passed to ResortController");

            try
            {
                var lHotel = new HotelBusiness(mContext).Get(id);
                if (lHotel.PageId == null)
                {
                    var lHotelPage = new ResortPageBusiness(mContext).CreateNewPage(lHotel, GetCurrentAgent());
                    lHotel.PageId = lHotelPage.Id;
                }
                var lUIPage = new ResortPageBusiness(mContext).Get(lHotel.PageId.Value);
                return View(lUIPage);
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to retreieve edit ResortPage [" + id + "]", e);
            }

            throw new InvalidOperationException();

        }

        // POST: ResortController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Summary([FromForm] UIResortPage aResort)
        {
            string FuncName = $"{ClassName}::Summary(UIResrtPage) - ";

            try
            {
                new PageDataAccess(mContext).Save(aResort);
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to save Summary Page", e);
            }
            return RedirectToAction(nameof(PageController.Index), "Resort");
        }


        /// <summary>
        /// Display the Marketing page for the hotel
        /// </summary>
        /// <param name="id">Marketing Page ID</param>
        /// <returns></returns>
        public ActionResult View(int id)
        {
            string FuncName = $"{ClassName}::View({id}) - ";

            if (id == 0)
                throw new InvalidOperationException("Invalid ID passed to ResortController");

            try
            {
                var lUIPage = new ResortPageBusiness(mContext).Get(id);
                return View("Details", lUIPage);
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to retreieve view Page [" + id + "]", e);
            }

            throw new InvalidOperationException();

        }

        // POST: ResortController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] UIResortPage aResort)
        {
            try
            {
                new PageDataAccess(mContext).Save(aResort);
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to save Resort Page", e);
            }
            return RedirectToAction(nameof(PageController.Index), "Page");
        }

        // GET: ResortController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ResortController/Delete/5
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
