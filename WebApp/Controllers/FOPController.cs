using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize]
    public class FOPController : BaseController
    {

        const string ClassName = "FOPController::";

        public FOPController(IDbContext context) : base(context)
        {
        }  
        
        // GET: FOPController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FOPController/Create
        public ActionResult Create(string id)
        {
            string FuncName = ClassName + $"Create (string={id})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lFOP = new FOPBusiness(DbContext).Create(id, GetCurrentUser());
                var lUIFOP = FOPUIHelper.Convert(lFOP);
                return View(lUIFOP);
            } catch (Exception e) {
                Logger.LogException(FuncName + "Failed to create CC", e);
            } finally
            {
                Logger.LeaveFunction(FuncName);
            }
            return RedirectToAction("Index", "Portal");            
        }

        // POST: FOPController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm]UIFOP aFOP)
        {
            string FuncName = $"{ClassName}Create (UIFOP = {aFOP.Id})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lFOPBiz = new FOPBusiness(DbContext);
                var lFOP = lFOPBiz.Save(aFOP, GetCurrentAgent());
                return RedirectToAction("Details", "Contacts", new { id = aFOP.UserID });
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + " Failed to create new Task for Opportunity", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }

            return RedirectToAction("Index", "Portal");
        }

        // GET: FOPController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FOPController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: FOPController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FOPController/Delete/5
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
