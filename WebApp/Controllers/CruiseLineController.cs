using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using BlitzerCore.Business;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Models.ASP;
using BlitzerCore.Utilities;
using BlitzerCore.UIHelpers;
using WebApp.AspNetHelper;
using BlitzerCore.DataAccess;

namespace WebApp.Controllers
{
    [Authorize]
    public class CruiseLineController : BaseController
    {
        const string ClassName = "CruiseController::";
        // GET: ResortController
        public CruiseLineController(IDbContext context) : base(context)
        {
        }

        public ActionResult Index()
        {
            string FuncName = $"{ClassName}Index() -";
            Logger.EnterFunction(FuncName);
            try
            {
                var lData = new CruiseDataAccess(mContext).GetAll();
                var lUIData = CruiseLineUIHelper.Convert(mContext, lData);
                Logger.LogDebug($"{FuncName} Returning {lData.Count()} rows");
                return View(lUIData);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }

        // GET: ResortController/Create
        public async Task<ActionResult> Create(int id)
        {
            var lCruiseLine = new CruiseBusiness(DbContext).GetCruiseLine(id);
            var lUI = new UISKU() { Provider = lCruiseLine.Name, ProviderId = lCruiseLine.Id };
            await SetReturnUrl(lUI);
            return View(lUI);
        }

        //POST: ResortController/Create
       [HttpPost]
       [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] UISKU aUICruise)
        {
            string FuncName = $"{ClassName}Create(UICruise = {aUICruise.Id})";
            Logger.EnterFunction(FuncName);
            try
            {
                aUICruise.Id = 0;
                var lCnt = new SKUBusiness(mContext).Save(aUICruise);
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to save cruise ", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
            return RedirectToAction(nameof(CruiseLineController.Index), "CruiseLine");
        }

        //GET: ResortController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            string FuncName = $"{ClassName}Edit(id = {id}) -";
            try
            {
                var lCruiseLine = new CruiseBusiness(mContext).GetCruiseLine(id);

                var lUICruiseLine = CruiseLineUIHelper.Convert(DbContext, lCruiseLine);
                ViewBag.BusinessTypes = AspNetHelper.ListHelper.GetBusinessTypes(new BusinessTypeDataAccess(DbContext).GetAll());
                ViewBag.AirPortCodes = ListHelper.GetAirPortCodes(new TravelBusiness(mContext).GetAirPorts());

                //PopulateLookups(lUIContact);
                if (lUICruiseLine == null)
                {
                    Logger.LogDebug($"{FuncName} CruiseLine not found");
                    return NotFound();
                }

                Logger.LogDebug($"{FuncName} returned CruiseLine");
                await SetReturnUrl(lUICruiseLine);
                return View(lUICruiseLine);
            }
            finally
            {
            }
        }

        // POST: ResortController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] UICruiseLine aCruiseLine)
        {
            string FuncName = $"{ClassName}Edit(CruiseLine = {aCruiseLine.Id})";
            Logger.EnterFunction(FuncName);
            try
            {
                //UIContact lContact = ContactUIHelper.Convert(aContact);
                var lCnt = new CruiseBusiness(mContext).Save(aCruiseLine);
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to save Cruise", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
            return ReturnToCaller(aCruiseLine);
        }

        //// GET: ResortController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: ResortController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        /// <summary>
        /// this is for the Drop List Box
        /// </summary>
        /// <param name="aCruiseLineId"></param>
        /// <returns></returns>
        public JsonResult GetCruises(int aCruiseLineId)
        {
            var FuncName = $"{ClassName}GetRoomTypes(aHotelId={aCruiseLineId})";

            //var lData = new HotelBusiness(mContext).GetRoomTypes(aHotelId).Cast<SKU>().ToList();
            //var lOutput = ListHelper.GetProductTypes(lData);
            //lOutput.Insert(0, new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem() { Value = "0", Text = $"Select from {lOutput.Count} room types" });
            //Logger.LogInfo($"{FuncName} Returning {lOutput.Count()} rows");
            //return Json(lOutput);
            return null;
        }


        public ActionResult Cruises(int id)
        {
            string FuncName = $"{ClassName}Cruises : ";

            if (id == 0)
                throw new InvalidOperationException($"{FuncName} Invalid ID passed");

            var lCruiseLine = new CruiseBusiness(mContext).GetCruiseLine(id);
            var lRoomTypes = new CruiseDataAccess(mContext).GetCruises (lCruiseLine);
            ViewBag.CruiseLineId = id;
            ViewBag.CruiseLine = lCruiseLine.Name;
            return View(lRoomTypes);
        }



        public async Task<ActionResult> Cruise(int id)
        {
            string FuncName = $"{ClassName}Cruise(id = {id}) -";
            try
            {
                var lCruise = new SKUDataAccess(mContext).GetSKU(id);

                if (lCruise == null)
                {
                    Logger.LogDebug($"{FuncName} Cruise not found");
                    return NotFound();
                }

                Logger.LogDebug($"{FuncName} returned Cruise");
                var lUICruise = SKUUIHelper.Convert(lCruise);
                return View(lUICruise);
            }
            finally
            {
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cruise([FromForm] UISKU aUISKU)
        {
            string FuncName = $"{ClassName}Cruise : ";
            try
            {
                new SKUBusiness(mContext).Save(aUISKU);
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to save Cruise", e);
            }
            return RedirectToAction(nameof(CruiseLineController.Index), "CruiseLine");
        }

    }
}
