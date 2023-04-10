using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
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
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class SystemController : BaseController
    {
        public const int NewsPageSize = 4;
        const string ClassName = "SystemController::";

        public SystemController(IDbContext context, UserManager<BlitzerUser> userManager) : base(context, userManager)
        {
        }

        public async Task<ActionResult> Create()
        {
            string FuncName = $"{ClassName}Create()";
            Logger.EnterFunction(FuncName);
            try
            {
                var lSystem = new SystemBusiness(mContext).Create(GetCurrentAgent());

                var lUISystem = SystemUIHelper.Convert(lSystem);
                ViewBag.Agents = ListHelper.GetTeamMembers(new TeamBusiness(DbContext).GetTeamMembers(GetCurrentAgent()));
                Logger.LogDebug($"{FuncName} returned new System");
                await SetReturnUrl(lUISystem);
                return View(lUISystem);
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + " Failed to create new System", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
            return RedirectToAction("Index", "Portal");

        }

        public async System.Threading.Tasks.Task<IActionResult> Kanban()
        {
            await SecuritySetup();
            return View(new KanbanSystemModel
            {
                Systems = SystemUIHelper.Convert(new SystemBusiness(DbContext).GetAll(GetCurrentAgent())),
                Statuses = new SystemStatus[] { SystemStatus.Requested, SystemStatus.Approved, SystemStatus.InProgress, SystemStatus.OnHold, SystemStatus.Deployed },
                Employees = new TeamBusiness(DbContext).GetTeamMembers(GetCurrentAgent())
            });
        }

        // : /Contacts/5
        public async Task<ActionResult> Edit(int id)
        {
            string FuncName = $"{ClassName}Edit(id={id})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lSystem = new SystemBusiness(mContext).Get(id);

                var lUISystem = SystemUIHelper.Convert(lSystem);
                //ViewBag.Agents = ListHelper.GetTeamMembers(new TeamDataAccess(DbContext).GetTeamMembers(GetCurrentAgent()));
                Logger.LogDebug($"{FuncName} returned new System");
                await SetReturnUrl(lUISystem);
                return View(lUISystem);
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + " Failed to edit System", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
            return RedirectToAction("Index", "Portal");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] UISystem aSystem)
        {
            string FuncName = $"{ClassName}Edit(UISystem = {aSystem.Id})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lSystem = SystemUIHelper.Convert(DbContext,aSystem);
                var lCnt = new SystemBusiness(mContext).Save(lSystem, GetCurrentAgent());
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to save System", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }

            return ReturnToCaller(aSystem);
        }

        public IActionResult Index()
        {
            return Kanban().Result;
        }
    }
}
