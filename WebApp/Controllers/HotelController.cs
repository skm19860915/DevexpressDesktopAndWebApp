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
    public class HotelController : BaseController
    {
        const string ClassName = "HotelController::";
        // GET: ResortController
        public HotelController(IDbContext context) : base(context)
        {
        }

        public ActionResult Index()
        {
            string FuncName = $"{ClassName}Index() -";
            Logger.EnterFunction(FuncName);
            try
            {
                var lData = new AccommodationDataAccess(mContext).GetAll();
                var lUIData = HotelUIHelper.Convert(mContext, lData);
                Logger.LogDebug($"{FuncName} Returning {lData.Count()} rows");
                return View(lUIData);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }

        // GET: ResortController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            string FuncName = $"{ClassName}Details(id = {id}) -";
            try
            {
                var lHotel = new AccommodationDataAccess(mContext).Get(id);

                if (lHotel == null)
                {
                    Logger.LogDebug($"{FuncName} Hotel not found");
                    return NotFound();
                }

                Logger.LogDebug($"{FuncName} returned Hotel");
                var lUIHotel = HotelUIHelper.Convert(DbContext, lHotel);
                await SetReturnUrl(lUIHotel);
                return View(lUIHotel);
            }
            finally
            {
            }
        }

        // GET: ResortController/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.BusinessTypes = AspNetHelper.ListHelper.GetBusinessTypes(new BusinessTypeDataAccess(DbContext).GetAll());
            var lUI = new UIHotel();
            await SetReturnUrl(lUI);
            return View(lUI);
        }

        // POST: ResortController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] UIHotel aHotel)
        {
            string FuncName = $"{ClassName}Create(UIHotel = {aHotel.Id})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lCnt = new HotelBusiness(mContext).Save(aHotel);
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to save Country Page", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
            if (aHotel.Id == 0)
                return RedirectToAction("Index", "Portal");
            return ReturnToCaller(aHotel);
        }

        // GET: ResortController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            string FuncName = $"{ClassName}Edit(id = {id}) -";
            try
            {
                var lHotel = new HotelBusiness(mContext).Get(id);

                var lUIContact = HotelUIHelper.Convert(DbContext, lHotel);
                ViewBag.BusinessTypes = AspNetHelper.ListHelper.GetBusinessTypes(new BusinessTypeDataAccess(DbContext).GetAll());
                ViewBag.AirPortCodes = ListHelper.GetAirPortCodes(new TravelBusiness(mContext).GetAirPorts());

                //PopulateLookups(lUIContact);
                if (lUIContact == null)
                {
                    Logger.LogDebug($"{FuncName} Hotel not found");
                    return NotFound();
                }

                Logger.LogDebug($"{FuncName} returned Hotel");
                await SetReturnUrl(lUIContact);
                return View(lUIContact);
            }
            finally
            {
            }
        }

        // POST: ResortController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm]UIHotel aHotel)
        {
            string FuncName = $"{ClassName}Edit(Contact = {aHotel.Id})";
            Logger.EnterFunction(FuncName);
            try
            {
                //UIContact lContact = ContactUIHelper.Convert(aContact);
                var lCnt = new HotelBusiness(mContext).Save(aHotel);
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to save Hotel", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
            return ReturnToCaller(aHotel);
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

        public JsonResult GetRoomTypes (int aHotelId)
        {
            var FuncName = $"{ClassName}GetRoomTypes(aHotelId={aHotelId})";

            var lData = new HotelBusiness(mContext).GetRoomTypes(aHotelId).Cast<SKU>().ToList();
            var lOutput = ListHelper.GetProductTypes(lData);
            lOutput.Insert(0, new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem() { Value = "0", Text = $"Select from {lOutput.Count} room types" });
            Logger.LogInfo($"{FuncName} Returning {lOutput.Count()} rows");
            return Json(lOutput);
        }

        public ActionResult RoomTypes(int id)
        {
            string FuncName = $"{ClassName}RoomTypes : ";

            if (id == 0)
                throw new InvalidOperationException($"{FuncName} Invalid ID passed");

            var lHotel = new HotelBusiness(mContext).Get(id);
            var lRoomTypes = new AccommodationDataAccess(mContext).GetAllRoomTypes(lHotel);
            ViewBag.ResortId = id;
            ViewBag.ResortName = lHotel.Name;
            return View(lRoomTypes);
        }

        public ActionResult CreateRoomType(int id)
        {
            string FuncName = $"{ClassName}CreateRoomType : ";
            try
            {

                if (id == 0)
                    throw new InvalidOperationException($"{FuncName} Invalid ID passed");

                var lHotel = new HotelBusiness(mContext).Get(id);
                var lNewRoomType = new RoomType() { AccommodiationID = lHotel.Id, Accommodation = lHotel };
                return View("RoomType", lNewRoomType);

            } catch ( Exception e)
            {
                Logger.LogException($"{FuncName} - Failed", e);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult EditRoomType(int id)
        {
            string FuncName = $"{ClassName}EditRoomType : ";

            if (id == 0)
                throw new InvalidOperationException($"{FuncName} Invalid Room Type ID passed");

            var lRoomType = new AccommodationDataAccess(mContext).GetRoomType(id);
            return View("RoomType", lRoomType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRoomType([FromForm] RoomType aRoomType)
        {
            string FuncName = $"{ClassName}EditRoomType : ";
            try
            {
                new SKUDataAccess(mContext).Save(aRoomType);
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to save CreateRoomType", e);
            }
            return RedirectToAction(nameof(HotelController.RoomTypes), "Hotel", new { id = aRoomType.AccommodiationID});
        }

    }
}
