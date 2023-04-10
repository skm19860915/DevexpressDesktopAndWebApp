using System;
using System.Collections.Generic;
using System.Linq;
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
using WebApp.DataServices;

namespace WebApp.Controllers
{
    public class SprintController : BaseController
    {
        public const int NewsPageSize = 4;
        const string ClassName = "SprintController::";

        public SprintController(IDbContext context, UserManager<BlitzerUser> userManager) : base(context, userManager)
        {
        }

        public IActionResult Current()
        {
            var lCurrentSprints = new SprintBusiness(DbContext).GetAll(GetCurrentAgent()).Where(x => x.Status == Sprint.StatusTypes.Current);
            
            if (lCurrentSprints == null || lCurrentSprints.Count() == 0)
                return RedirectToAction(nameof(SprintController.Index));

            return RedirectToAction(nameof(SprintController.Kanban), "Sprint", new { id = lCurrentSprints.ElementAt(0).Id });
        }

        /// <summary>
        /// Show Kanban board for Task in Sprint
        /// </summary>
        /// <param name="id">SprintID</param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<IActionResult> Kanban(int id)
        {
            var lSprint = new SprintBusiness(DbContext).Get(id);
            var lTaskBusiness = new TaskBusiness(DbContext);
            var lTasks = lTaskBusiness.Get(lSprint).Where(x => x.OwnerID == GetCurrentUserID()).ToList();
            lTasks = lTaskBusiness.Sort(lTasks);
            await SecuritySetup();
            return View(new KanbanSprintModel
            {
                Sprint = lSprint,
                Tasks = TaskUIHelper.Convert(lTasks),
                TaskType = "Your Tasks",
                Statuses = new TaskStatusTypes[] { TaskStatusTypes.NEW, TaskStatusTypes.INPROGRESS, TaskStatusTypes.REVIEW, TaskStatusTypes.ONHOLD, TaskStatusTypes.COMPLETED },
                Employees = new TeamBusiness(DbContext).GetTeamMembers(GetCurrentAgent())
            });
        }

        /// <summary>
        /// Show Kanban board for Task in Sprint
        /// </summary>
        /// <param name="id">SprintID</param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<IActionResult> KanbanAll(int id)
        {
            var lSprint = new SprintBusiness(DbContext).Get(id);
            var lTaskBusiness = new TaskBusiness(DbContext);
            var lTasks = lTaskBusiness.Get(lSprint);
            lTasks = lTaskBusiness.Sort(lTasks);
            await SecuritySetup();
            return View("Kanban", new KanbanSprintModel
            {
                Sprint = lSprint,
                Tasks = TaskUIHelper.Convert(lTasks),
                TaskType = "All Tasks",
                Statuses = new TaskStatusTypes[] { TaskStatusTypes.NEW, TaskStatusTypes.INPROGRESS, TaskStatusTypes.REVIEW, TaskStatusTypes.ONHOLD, TaskStatusTypes.COMPLETED },
                Employees = new TeamBusiness(DbContext).GetTeamMembers(GetCurrentAgent())
            });
        }

        public ActionResult Create()
        {
            string FuncName = $"{ClassName}Create()";
            Logger.EnterFunction(FuncName);
            try
            {
                var lSprint = new SprintBusiness(mContext).Create(GetCurrentAgent());

                var lUISprint = SprintUIHelper.Convert(lSprint);
                ViewBag.Agents = ListHelper.GetTeamMembers(new TeamBusiness(DbContext).GetTeamMembers(GetCurrentAgent()));
                Logger.LogDebug($"{FuncName} returned new Sprint");
                return View(lUISprint);
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + " Failed to create new Sprint", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
            return RedirectToAction("Index", "Portal");

        }


        // : /Contacts/5
        public ActionResult Edit(int id)
        {
            string FuncName = $"{ClassName}Edit(id={id})";
            Logger.EnterFunction(FuncName);
            try
            {
                if ( id == 0 )
                    return RedirectToAction("Index", "Portal");

                var lSprint = new SprintBusiness(mContext).Get(id);

                var lUISprint = SprintUIHelper.Convert(lSprint);
                Logger.LogDebug($"{FuncName} returned new Sprint");
                return View(lUISprint);
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + " Failed to edit Sprint", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
            return RedirectToAction("Index", "Portal");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] UISprint aSprint)
        {
            string FuncName = $"{ClassName}Edit(UISprint = {aSprint.Id})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lSprint = SprintUIHelper.Convert(DbContext,aSprint);
                var lCnt = new SprintBusiness(mContext).Save(lSprint, GetCurrentAgent());
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to save Sprint", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }

            return RedirectToAction("Index", "Portal");
        }

        public async System.Threading.Tasks.Task<IActionResult> Index()
        {
            string FuncName = $"{ClassName}Index() -";
            try
            {
                var lSprints = new SprintBusiness(mContext).GetAll(GetCurrentAgent());
                Logger.LogDebug($"{FuncName} Returning {lSprints.Count()} rows");
                await SecuritySetup();
                return View(SprintUIHelper.Convert(lSprints));
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }
    }
}
