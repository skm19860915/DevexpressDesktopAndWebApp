using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
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
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class FeatureController : BaseController
    {
        public const int NewsPageSize = 4;
        const string ClassName = "FeatureController::";

        public FeatureController(IDbContext context, UserManager<WebApp.DataServices.BlitzerUser> userManager) : base(context, userManager)
        {
        }

        /// <summary>
        /// Create Feature From System ID
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Create(int? id)
        {
            string FuncName = $"{ClassName}Create(SystemId={id})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lFeature = new FeatureBusiness(mContext).Create(GetCurrentAgent(), id);

                var lUIFeature = FeatureUIHelper.Convert(lFeature);
                ViewBag.Agents = ListHelper.GetTeamMembers(new TeamBusiness(DbContext).GetTeamMembers(GetCurrentAgent()));
                ViewBag.Systems = ListHelper.GetSystems(new SystemDataAccess(DbContext).Get(GetCurrentAgent()));
                Logger.LogDebug($"{FuncName} returned new Feature");
                await SetReturnUrl(lUIFeature);
                return View(lUIFeature);
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + " Failed to create new Feature", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
            return RedirectToAction("Index", "Portal");

        }

        /// <summary>
        /// Shows Features which are part of a system
        /// </summary>
        /// <param name="id">system id</param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<IActionResult> Kanban(int? id)
        {
            List<Feature> lFeatures = new List<Feature>();
            BlitzSystem lSystem = null;

            if (id == null)
                lFeatures = new FeatureBusiness(DbContext).GetAll(GetCurrentAgent());
            else
            {
                lFeatures = new FeatureBusiness(DbContext).GetAll(GetCurrentAgent())
                    .Where(x => x.SystemId == id).ToList();
                lSystem = new SystemBusiness(DbContext).Get(id.Value);
            }

            await SecuritySetup();
            return View(new KanbanFeatureModel
            {
                System = lSystem,
                Features = FeatureUIHelper.Convert(lFeatures),
                Statuses = new FeatureStatus[] { FeatureStatus.Requested, FeatureStatus.Approved, FeatureStatus.InProgress, FeatureStatus.OnHold, FeatureStatus.Deployed },
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
                var lFeature = new FeatureBusiness(mContext).Get(id);

                var lUIFeature = FeatureUIHelper.Convert(lFeature);
                ViewBag.Agents = ListHelper.GetTeamMembers(new TeamBusiness(DbContext).GetTeamMembers(GetCurrentAgent()));
                ViewBag.Systems = ListHelper.GetSystems(new SystemDataAccess(DbContext).Get(GetCurrentAgent()));
                Logger.LogDebug($"{FuncName} returned new Feature");
                await SetReturnUrl(lUIFeature);
                return View(lUIFeature);
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + " Failed to edit Feature", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
            return RedirectToAction("Index", "Portal");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] UIFeature aFeature)
        {
            string FuncName = $"{ClassName}Edit(UIFeature = {aFeature.Id})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lFeature = FeatureUIHelper.Convert(DbContext,aFeature);
                var lCnt = new FeatureBusiness(mContext).Save(lFeature, GetCurrentAgent());
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to save Feature", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }

            return ReturnToCaller(aFeature);
        }

        public async System.Threading.Tasks.Task<IActionResult> Index()
        {
            string FuncName = $"{ClassName}Index() -";
            try
            {
                var lFeatures = new FeatureBusiness(mContext).GetAll(GetCurrentAgent());
                Logger.LogDebug($"{FuncName} Returning {lFeatures.Count()} rows");
                await SecuritySetup();
                return View(FeatureUIHelper.Convert(lFeatures));
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }
    }
}
