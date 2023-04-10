using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class UserStoryController : BaseController
    {
        const string ClassName = "UserStoryController::";
        public UserStoryController(IDbContext context, UserManager<BlitzerUser> userManager) : base(context, userManager)
        {
        }

        /// <summary>
        /// Create Feature with FeatureId
        /// </summary>
        /// <param name="id">int - FeatureId </param>
        /// <returns></returns>
        public async Task<ActionResult> Create(int? aFeatureId)
        {
            string FuncName = $"{ClassName}Create()";
            Logger.EnterFunction(FuncName);
            try
            {
                var lUserStory = new UserStoryBusiness(mContext).Create(GetCurrentAgent(), aFeatureId);

                var lUIContact = UserStoryUIHelper.Convert(lUserStory);
                ViewBag.Agents = ListHelper.GetTeamMembers(new TeamDataAccess(DbContext).GetTeamMembers(GetCurrentAgent()));
                ViewBag.Features = ListHelper.GetFeatures(new FeatureBusiness(DbContext).GetAll(GetCurrentAgent()));
                ViewBag.Sprints = ListHelper.GetSprints(new SprintBusiness(DbContext).GetAll(GetCurrentAgent()));
                Logger.LogDebug($"{FuncName} returned new UserStory");
                await SetReturnUrl(lUIContact);
                return View(lUIContact);
            } catch (Exception e )
            {
                Logger.LogException(FuncName + " Failed to create new User Story", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
            return RedirectToAction("Index", "Portal");

        }

        /// <summary>
        /// Show Kanban board for Features
        /// </summary>
        /// <param name="id">FeatureID</param>
        /// <returns></returns>
        public async Task<IActionResult> Kanban(int id)
        {
            var lFeature = new FeatureBusiness(DbContext).Get(id);
            await SecuritySetup();
            var lUserStories = UserStoryUIHelper.Convert(new UserStoryBusiness(DbContext).Get(lFeature));
            UpdateKanbanNames(lUserStories);
            return View(new KanbanRequirementModel
            {
                Feature  = lFeature,
                UserStories = lUserStories,
                Statuses = new UserStoryStatus[] { UserStoryStatus.Requested, UserStoryStatus.Approved, UserStoryStatus.InProgress, UserStoryStatus.ReadyForTest, UserStoryStatus.ReadyToDeploy, UserStoryStatus.Deployed },
                Employees = new TeamDataAccess(DbContext).GetTeamMembers(GetCurrentAgent())
            });
        }

        /// <summary>
        /// Show Kanban board for User Story Tasks
        /// </summary>
        /// <param name="id">UserStoryID</param>
        /// <returns></returns>
        public async Task<IActionResult> KanbanUSTasks(int id)
        {
            var lUserStory = new UserStoryBusiness(DbContext).Get(id);
            //UpdateKanbanNames(lUserStories);
            await SecuritySetup();
            return View(new KanbanTasksByUSModel

            {
                UserStory = lUserStory,
                Tasks = TaskUIHelper.Convert(new TaskBusiness(DbContext).Get(lUserStory)),
                Statuses = new TaskStatusTypes[] { TaskStatusTypes.NEW, TaskStatusTypes.INPROGRESS, TaskStatusTypes.REVIEW, TaskStatusTypes.ONHOLD, TaskStatusTypes.COMPLETED },
                Employees = new TeamDataAccess(DbContext).GetTeamMembers(GetCurrentAgent())
            });
        }

        private void UpdateKanbanNames(List<UIUserStory> aUserStories)
        {
            foreach ( var lUserStory in aUserStories)
            {
                if (lUserStory.Work == null || lUserStory.Work.Count() == 0 )
                    lUserStory.Name += "  - No Work";
                else
                    lUserStory.Name += "  -  (" + lUserStory.Work.Count(x => x.Status == TaskStatusTypes.COMPLETED && x.Status != TaskStatusTypes.DELETED) + "/" + lUserStory.Work.Count(x=>x.Status != TaskStatusTypes.DELETED)+")";
            }
        }

        // : /Contacts/5
        public async Task<ActionResult> Edit(int id)
        {
            string FuncName = $"{ClassName}Edit(id={id})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lUserStory = new UserStoryBusiness(mContext).Get(id);

                var lUIContact = UserStoryUIHelper.Convert(lUserStory);
                ViewBag.Agents = ListHelper.GetTeamMembers(new TeamDataAccess(DbContext).GetTeamMembers(GetCurrentAgent()));
                ViewBag.Features = ListHelper.GetFeatures(new FeatureBusiness(DbContext).GetAll(GetCurrentAgent()));
                ViewBag.Sprints = ListHelper.GetSprints(new SprintBusiness(DbContext).GetAll(GetCurrentAgent()));
                Logger.LogDebug($"{FuncName} returned new UserStory");
                await SetReturnUrl(lUIContact);
                return View(lUIContact);
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + " Failed to edit User Story", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
            return RedirectToAction("Index", "Portal");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] UIUserStory aUserStory)
        {
            string FuncName = $"{ClassName}Edit(UIUserStory = {aUserStory.Id})";
            Logger.EnterFunction(FuncName);
            try
            {
                 var lUserStory = UserStoryUIHelper.Convert(DbContext, aUserStory);
                var lCnt = new UserStoryBusiness(mContext).Save(lUserStory, GetCurrentAgent());
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to save user story", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }

            return ReturnToCaller(aUserStory);
        }

        public async System.Threading.Tasks.Task<IActionResult> Index()
        {
            string FuncName = $"{ClassName}Index() -";
            try
            {
                var lSprints = new SprintBusiness(mContext).GetAll(GetCurrentAgent(), true);
                Logger.LogDebug($"{FuncName} Returning {lSprints.Count()} Sprints");
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
