using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlitzerCore.Business;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Models.ASP;
using BlitzerCore.Utilities;
using BlitzerCore.UIHelpers;
using WebApp.AspNetHelper;
using BlitzerCore.DataAccess;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize]

    public class CompanyController : BaseController
    {
        const string ClassName = "CompanyController::";
        // GET: CompanyController
        public CompanyController(IDbContext context) : base(context)
        {
        }

        public ActionResult Index()
        {
            string FuncName = $"{ClassName}Index() -";
            Logger.EnterFunction(FuncName);
            try
            {
                var lData = new CompanyDataAccess(mContext).GetAll(GetCurrentAgent());
                var lUIData = CompanyUIHelper.Convert(mContext, lData);
                Logger.LogDebug($"{FuncName} Returning {lData.Count()} rows");
                return View(lUIData);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }

        // GET: CompanyController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            string FuncName = $"{ClassName}Details(id = {id}) -";
            try
            {
                var lCompany = new CompanyDataAccess(mContext).Get(id);

                if (lCompany == null)
                {
                    Logger.LogDebug($"{FuncName} Company not found");
                    return NotFound();
                }

                if (lCompany.GetType() == typeof(Hotel))
                    return RedirectToAction("Details", "Hotel", new { id = id });

                Logger.LogDebug($"{FuncName} returned Comipany");
                TourOperator lTO = lCompany as TourOperator;
                UICompany lUICompany = null;
                if (lTO != null)
                    lUICompany = CompanyUIHelper.Convert(mContext, lTO);
                else
                    lUICompany = CompanyUIHelper.Convert(mContext, lCompany);
                await SetReturnUrl(lUICompany);
                return View(lUICompany);
            }
            finally
            {
            }
        }

        // GET: CompanyController/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.BusinessTypes = AspNetHelper.ListHelper.GetBusinessTypes(new BusinessTypeDataAccess(DbContext).GetAll());
            var lUI = new UICompany();
            await SetReturnUrl(lUI);
            return View(lUI);
        }

        // POST: CompanyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] UICompany aCompany)
        {
            string FuncName = $"{ClassName}Create(UICompany = {aCompany.Id})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lCnt = new CompanyBusiness(mContext).Save(aCompany, GetCurrentAgent());
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to save Country Page", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
            if (aCompany.Id == 0)
                return RedirectToAction("Index", "Portal");
            return ReturnToCaller(aCompany);
        }

        // GET: CompanyController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            string FuncName = $"{ClassName}Edit(id = {id}) -";
            try
            {
                var lCompany = new CompanyBusiness(mContext).Get(id);

                var lUIContact = CompanyUIHelper.Convert(mContext, lCompany);
                ViewBag.BusinessTypes = AspNetHelper.ListHelper.GetBusinessTypes(new BusinessTypeDataAccess(DbContext).GetAll());
                //PopulateLookups(lUIContact);
                if (lUIContact == null)
                {
                    Logger.LogDebug($"{FuncName} Company not found");
                    return NotFound();
                }

                Logger.LogDebug($"{FuncName} returned Company");
                await SetReturnUrl(lUIContact);
                return View(lUIContact);
            }
            finally
            {
            }
        }

        // POST: CompanyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm]UICompany aCompany)
        {
            string FuncName = $"{ClassName}Edit(Contact = {aCompany.Id})";
            Logger.EnterFunction(FuncName);
            try
            {
                //UIContact lContact = ContactUIHelper.Convert(aContact);
                var lCnt = new CompanyBusiness(mContext).Save(aCompany, GetCurrentAgent());
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName}Failed to save company", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
            return ReturnToCaller(aCompany);
        }

        // GET: CompanyController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CompanyController/Delete/5
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
