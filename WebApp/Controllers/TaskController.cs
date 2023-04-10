using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
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
    public class TaskController : BaseController
    {
        public const int NewsPageSize = 4;
        const string ClassName = "TaskController::";
        IConfiguration Configuration { get; }

        public TaskController(IDbContext context, UserManager<BlitzerUser> userManager, IConfiguration aConfig) : base(context, userManager)
        {
            Configuration = aConfig;
        }

        //[HttpGet]
        //public ActionResult Task()
        //{
        //    PushOnURLStack();
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult ComputeEndTime(string StartDate, string Duration, string DType)
        //{
        //    using (IDbContextScope lScope = new DbContextScopeFactory().Create())
        //    {
        //        var lResult = new WorkBusiness(lScope).ComputeEndTime(StartDate, Duration, DType, AccountHelper.GetCurrentEmployee());
        //        return Json(lResult, JsonRequestBehavior.AllowGet);
        //    }
        //}


        /// <summary>
        /// Create new Task for an Opportunity
        /// </summary>
        /// <param name="id">Opportunity key</param>
        /// <returns></returns>
        ///

        [HttpGet]
        public async Task<ActionResult> NewCompanyTask(int id)
        {
            string FuncName = $"{ClassName}NewCompanyTask (CompanyId = {id})";
            Logger.EnterFunction(FuncName);
            try
            {
                ViewBag.Title = "New Task";
                var lTask = new TaskBusiness(DbContext, Configuration).Create((Opportunity)null, GetCurrentAgent());
                lTask.TargetCompanyId = id;
                lTask.TargetCompany = new CompanyBusiness(DbContext).Get(id);
                ViewBag.Opps = ListHelper.GetList(new OpportunityBusiness(DbContext).GetOppsAndTrips(GetCurrentAgent()));
                ViewBag.Agents = ListHelper.GetTeamMembers(new TeamBusiness(DbContext).GetTeamMembers(GetCurrentAgent()));
                var lUITask = TaskUIHelper.Convert(lTask);
                await SetReturnUrl(lUITask);
                return View("Create", lUITask);
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

        /// <summary>
        /// New Task for a Trip
        /// </summary>
        /// <param name="id">Trip</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> NewTripTask(int id)
        {
            string FuncName = $"{ClassName}NewTripTask (TripId = {id})";
            Logger.EnterFunction(FuncName);
            var lCurrentUser = GetCurrentAgent();
            try
            {
                ViewBag.Title = "New Task";
                var lTask = new TaskBusiness(DbContext, Configuration).Create((Opportunity)null, lCurrentUser);
                lTask.OpportunityID = id;
                lTask.Opportunity = new TripBusiness(DbContext).Get(id);
                if ((IsAdmin(lCurrentUser)).Result )
                    ViewBag.Opps = ListHelper.GetList(new OpportunityBusiness(DbContext).GetOppsAndTrips(lCurrentUser));
                ViewBag.Agents = ListHelper.GetTeamMembers(new TeamBusiness(DbContext).GetTeamMembers(lCurrentUser));
                var lUITask = TaskUIHelper.Convert(lTask);
                await SetReturnUrl(lUITask);
                return View("Create", lUITask);
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

        [HttpGet]
        public ActionResult Index()
        {
            var lTaskBiz = new TaskBusiness(mContext, Configuration);
            Agent lAgent = GetCurrentAgent();
            var lTasks = lTaskBiz.Get(lAgent.Employer).Where(x => x.Status != TaskStatusTypes.DELETED);
            var lUITasks = TaskUIHelper.Convert(lTasks);
            return View(lUITasks);
        }

        /// <summary>
        /// Create Task from US
        /// </summary>
        /// <param name="id">User Story ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> CreateByUs(int id)
        {
            string FuncName = $"{ClassName}CreateByUs (UserStoryID = {id})";
            Logger.EnterFunction(FuncName);
            try
            {
                ViewBag.Title = "New Task";
                UserStory lUserStory = null;
                var lCurrentUser = GetCurrentAgent();
                if (id != 0)
                    lUserStory = new UserStoryBusiness(DbContext).Get(id);
                var lTask = new TaskBusiness(DbContext, Configuration).Create(lUserStory, GetCurrentAgent());
                lTask.TargetCompanyId = ViewBag.CompanyId;
                lTask.TargetContactId = ViewBag.ContactId;
                if ((IsAdmin(lCurrentUser)).Result)
                {
                    ViewBag.Sprints = ListHelper.GetSprints(new SprintBusiness(DbContext).GetAll(lCurrentUser));
                    ViewBag.UserStories = ListHelper.GetUserStories(new UserStoryBusiness(DbContext).GetAll(lCurrentUser));
                }
                ViewBag.Opps = ListHelper.GetList(new OpportunityBusiness(DbContext).GetOppsAndTrips(lCurrentUser));
                ViewBag.Agents = ListHelper.GetTeamMembers(new TeamBusiness(DbContext).GetTeamMembers(lCurrentUser));
                var lUITask = TaskUIHelper.Convert(lTask);
                await SetReturnUrl(lUITask);
                return View("Create", lUITask);
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


        /// <summary>
        /// Create new Task for an Opportunity
        /// </summary>
        /// <param name="id">Opportunity key</param>
        /// <returns></returns>
        ///

        [HttpGet]
        public async Task<ActionResult> Create(int? id)
        {
            string FuncName = $"{ClassName}New (OpportunityID = {id})";
            Logger.EnterFunction(FuncName);
            try
            {
                ViewBag.Title = "New Task";
                Opportunity lOpp = null;
                var lCurrentUser = GetCurrentAgent();
                if ( id != null )   
                    lOpp = new OpportunityBusiness(DbContext).GetOpportunity(id.Value);
                var lTask = new TaskBusiness(DbContext, Configuration).Create(lOpp, lCurrentUser);
                lTask.TargetCompanyId = ViewBag.CompanyId;
                lTask.TargetContactId = ViewBag.ContactId;
                ViewBag.Opps = ListHelper.GetList( new OpportunityBusiness(DbContext).GetOppsAndTrips(lCurrentUser));
                ViewBag.Agents = ListHelper.GetTeamMembers( new TeamBusiness(DbContext).GetTeamMembers(lCurrentUser));
                if ((IsAdmin(lCurrentUser)).Result)
                {
                    ViewBag.Sprints = ListHelper.GetSprints(new SprintBusiness(DbContext).GetAll(lCurrentUser));
                    ViewBag.UserStories = ListHelper.GetUserStories(new UserStoryBusiness(DbContext).GetAll(lCurrentUser));
                }
                var lUITask = TaskUIHelper.Convert(lTask);
                await SetReturnUrl(lUITask);
                return View(lUITask);
            } catch ( Exception e )
            {
                Logger.LogException(FuncName + " Failed to create new Task for Opportunity", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }


            return RedirectToAction("Index", "Portal");
        }

        /// <summary>
        /// Create new Task for an Opportunity
        /// </summary>
        /// <param name="id">Opportunity key</param>
        /// <returns></returns>
        ///

        [HttpGet]
        public async Task<ActionResult> CreateForUS(int id)
        {
            string FuncName = $"{ClassName}CreateForUS (UserStoryID = {id})";
            Logger.EnterFunction(FuncName);
            try
            {
                ViewBag.Title = "New Task";
                UserStory lUS = null;
                var lCurrentUser = GetCurrentAgent();

                lUS = new UserStoryBusiness(DbContext).Get(id);
                var lTask = new TaskBusiness(DbContext, Configuration).Create(lUS, GetCurrentAgent());
                lTask.TargetCompanyId = ViewBag.CompanyId;
                lTask.TargetContactId = ViewBag.ContactId;
                ViewBag.Opps = ListHelper.GetList(new OpportunityBusiness(DbContext).GetOppsAndTrips(lCurrentUser));
                ViewBag.Agents = ListHelper.GetTeamMembers(new TeamBusiness(DbContext).GetTeamMembers(lCurrentUser));
                if ((IsAdmin(lCurrentUser)).Result)
                {
                    ViewBag.Sprints = ListHelper.GetSprints(new SprintBusiness(DbContext).GetAll(lCurrentUser));
                    ViewBag.UserStories = ListHelper.GetUserStories(new UserStoryBusiness(DbContext).GetAll(lCurrentUser));
                }
                var lUITask = TaskUIHelper.Convert(lTask);
                await SetReturnUrl(lUITask);
                return View("Create", lUITask);
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

        /// <summary>
        /// Edit Task
        /// </summary>
        /// <param name="id">Task ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            string FuncName = $"{ClassName}Edit (TaskId = {id})";
            Logger.EnterFunction(FuncName);
            var lCurrentUser = GetCurrentAgent();
            try
            {
                BlitzerCore.Models.Task lTask = new TaskBusiness(DbContext, Configuration).Get(id);
                var lUITask = TaskUIHelper.Convert(lTask);

                ViewBag.Stati = ListHelper.GetOwnerTaskStati(TripBusiness.IsIssuer(lTask, GetCurrentUser()));
                ViewBag.Agents = ListHelper.GetTeamMembers(new TeamBusiness(DbContext).GetTeamMembers(lCurrentUser));
                var lOpps = new OpportunityBusiness(DbContext).GetOppsAndTrips(lCurrentUser);
                if (lTask.OpportunityID != null && lOpps.Any(x => x.ID == lTask.OpportunityID) == false )
                    lOpps.Add(lTask.Opportunity);
                ViewBag.Opps = ListHelper.GetList(lOpps);
                if ((IsAdmin(lCurrentUser)).Result)
                {
                    ViewBag.Sprints = ListHelper.GetSprints(new SprintBusiness(DbContext).GetAll(lCurrentUser));
                    ViewBag.UserStories = ListHelper.GetUserStories(new UserStoryBusiness(DbContext).GetAll(lCurrentUser));
                }
                await SetReturnUrl(lUITask);
                return View(lUITask);
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

        [HttpPost]
        public ActionResult Create([FromForm] UITask aUITask)
        {
            string FuncName = $"{ClassName}Create (UITask = {aUITask.Id})";
            Logger.LogInfo(FuncName);
            return Save(aUITask);
        }
         
        [HttpPost]
        public ActionResult Edit([FromForm] UITask aUITask)
        {
            string FuncName = $"{ClassName}Edit (UITask = {aUITask.Id})";
            Logger.LogInfo(FuncName);
            return Save(aUITask);
        }

        private ActionResult Save(UITask aUITask)
        {
            string FuncName = $"{ClassName}Save (UITask = {aUITask.Id})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lTaskBiz = new TaskBusiness(DbContext, Configuration);
                var lTask = lTaskBiz.Save(aUITask, GetCurrentAgent());
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + " Failed to create new Task for Opportunity", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }

            return ReturnToCaller(aUITask);
        }

        //public ActionResult WorkItemforObj(int? ObjectiveID)
        //{
        //    //ViewBag.Url = Request.UrlReferrer;
        //    PushOnURLStack();

        //    ViewBag.Title = "New Task";
        //    using (IDbContextScope lScope = new DbContextScopeFactory().Create())
        //    {
        //        Work work = (new WorkBusiness(lScope)).GetDefaultWorkItem(AccountHelper.GetCurrentEmployee());
        //        work.ObjectiveID = ObjectiveID;

        //        if (ObjectiveID != null && ObjectiveID.HasValue)
        //        {
        //            Session["Objective"] = ObjectiveID;
        //        }
        //        var lWorkSnapShot = new WorkBusiness(lScope).CreateWorkSnapShot(work.ID,
        //            AccountHelper.GetCurrentUserId(), work.Owner.Primary.OrgID);
        //        lWorkSnapShot.ObjectiveID = ObjectiveID.Value;

        //        return View("WorkItem", lWorkSnapShot);
        //    }
        //}

        //public ActionResult GetEndDate(DateTime aStartDate, string aDuration)
        //{
        //    return Json("");
        //}

        //public ActionResult GetDuration(DateTime aStartDate, DateTime aEndDate)
        //{
        //    Employee lEmp = AccountHelper.GetCurrentEmployee();
        //    using (IDbContextScope lDBContext = new DbContextScopeFactory().Create(DbContextScopeOption.ForceCreateNew))
        //    {
        //        ContextTemplateDataAccess lContextDAL = new ContextTemplateDataAccess(lDBContext);

        //        return Json(Common.DurationHelper.ComputeDuration(aStartDate, aEndDate, lEmp, lContextDAL.GetContextTemplates()));
        //    }
        //}

        //[HttpPost]
        //public ActionResult WorkItem(WorkSnapShot model)
        //{

        //    GUIUpdates(model);
        //    try
        //    {
        //        bool lUnitTesting = (Request == null || Request.Url == null);
        //        string lObjective = "";
        //        if (lUnitTesting == false)
        //            lObjective = Session["Objective"] as string;

        //        using (IDbContextScope lScope = new DbContextScopeFactory().Create(DbContextScopeOption.ForceCreateNew))
        //        {
        //            Logger.LogMessage("WorkTop GUID=>" + lScope.GUID());
        //            var lWorkBusiness = new WorkBusiness(lScope);
        //            Work lModel = GetWork(model);
        //            lModel.ClientOrgID = GetClientOrgID();
        //            Work lResult = lWorkBusiness.saveWork(lModel, AccountHelper.GetCurrentUserId(), lObjective, lUnitTesting, GetClientOrgID());
        //            lScope.SaveChanges();
        //            return Json(new { Status = System.Net.HttpStatusCode.OK, Id = lResult.ID }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.LogException("WorkController.WorkItem(HTTPPOST)", e);
        //    }

        //    return Json(new { Status = System.Net.HttpStatusCode.NotFound, Message = "No Result Found" });
        //}

        //Work GetWork ( WorkSnapShot aModel )
        //{
        //    Work lWork = new Work();
        //    lWork.Active = aModel.isActive;
        //    lWork.Deadline = aModel.Deadline;
        //    lWork.StartDate = aModel.StartDate;
        //    lWork.EndDate = aModel.EndDate;
        //    lWork.OwnerID = aModel.OwnerID;
        //    lWork.Name = aModel.Name;
        //    lWork.ObjectiveID = aModel.ObjectiveID;
        //    lWork.Priority = aModel.Priority;
        //    lWork.ProjectID = aModel.ProjectID;
        //    lWork.FeatureID = aModel.FeatureID;
        //    lWork.CreatedAt = aModel.CreatedAt;
        //    lWork.ContextID = aModel.ContextID;
        //    lWork.DatesCalculated = aModel.DatesCalculated;
        //    lWork.ID = aModel.ID;
        //    lWork.Duration = aModel.Duration;
        //    lWork.DurationType = aModel.DurationType;
        //    lWork.LocationID = aModel.LocationID;
        //    lWork.UserStoryID = aModel.UserStoryID;
        //    if (lWork.UserStoryID < 1)
        //        lWork.UserStoryID = null;
        //    lWork.PriorityType = aModel.PriorityType;
        //    lWork.SprintID = aModel.SprintID;
        //    if (lWork.SprintID < 1 || lWork.UserStoryID != null )
        //        lWork.SprintID = null;
        //    lWork.Comment = aModel.Comment;
        //    lWork.Status = aModel.Status;
        //    lWork.IssuerID = aModel.IssuerID;
        //    lWork.DataType = aModel.DataType;
        //    lWork.Description = aModel.Description;
        //    lWork.MeetingID = aModel.MeetingID;

        //    return lWork;
        //}

        //public static void GUIUpdates(WorkSnapShot model)
        //{
        //    // Hack : Needed because entity fromwork big hassle tyring to set initial value to 0
        //    if (model.LocationID == 0)
        //        model.LocationID = 1;

        //    // Hack
        //    if (model.TempDatesCalculated != null)
        //        model.DatesCalculated = model.TempDatesCalculated.Value;   // Short term hack on server to get around problem on client side

        //    if (model.ContextID < 1)
        //        model.ContextID = AccountHelper.GetCurrentEmployee().DefaultContextID;

        //    if (model.UserStoryID < 1)
        //        model.UserStoryID = null;

        //    // If user clears deadline field, year will be 2000
        //    if (model.Deadline != null && model.Deadline.Value.Year == 2000)
        //    {
        //        model.Deadline = null;
        //        model.TempDeadLine = null;
        //    }
        //    // Problem : Deadline is not being reurned from the View if the user doesn't modify it
        //    if (model.Deadline == null && model.TempDeadLine != null)
        //        model.Deadline = model.TempDeadLine;
        //}

        //[ValidateInput(false)]
        //public ActionResult FullTaskGridViewPartial()
        //{
        //    Stopwatch lSW = new Stopwatch();
        //    lSW.Start();
        //    var lResult = TaskGridViewPartialFilter("All");
        //    Logger.LogStats("==> WorkController.TaskGridViewPartial ", lSW.ElapsedMilliseconds.ToString());
        //    return lResult;
        //}

        //[ValidateInput(false)]
        //public ActionResult TaskGridViewPartial()
        //{
        //    Stopwatch lSW = new Stopwatch();
        //    lSW.Start();
        //    var lFilterType = Session["WorkFilterType"];
        //    string lProcFilterType = null;
        //    if (lFilterType != null)
        //        lProcFilterType = lFilterType.ToString();

        //    var lResult = TaskGridViewPartialFilter(lProcFilterType);
        //    Logger.LogStats("==> WorkController.TaskGridViewPartial ", lSW.ElapsedMilliseconds.ToString());
        //    return lResult;
        //}

        //[ValidateInput(false)]
        //public ActionResult IssuesByUserStory(int aUserStoryID)
        //{
        //    ViewData["issueUserStoryID"] = aUserStoryID;

        //    Stopwatch lSW = new Stopwatch();
        //    lSW.Start();
        //    var lFilterType = Session["WorkFilterType"];
        //    string lProcFilterType = null;
        //    if (lFilterType != null)
        //        lProcFilterType = lFilterType.ToString();

        //    using (IDbContextScope lScope = new DbContextScopeFactory().Create())
        //    {
        //        var lData = new WorkBusiness(lScope).GetWorkUserStory(aUserStoryID).Where(x => x.DataType == Work.WorkTypes.ISSUE).ToList();
        //        Logger.LogStats("WorkController.IssuersByUserStory ", lSW.ElapsedMilliseconds.ToString());
        //        List<WorkSnapShot> lModel = new List<WorkSnapShot>();
        //        foreach (var lElement in lData)
        //            lModel.Add(new WorkSnapShot(lElement));

        //        return PartialView("~/Views/Shared/_IssueGridViewPartial.cshtml", lModel);
        //    }
        //}

        ///// <summary>
        ///// Method is a AJAX method for filter tasks on the dashboard
        ///// </summary>
        ///// <param name="FilterType">Used to show the filter</param>
        ///// <returns></returns>
        //[ValidateInput(false)]
        //public ActionResult TaskGridViewPartialFilter(string FilterType)
        //{
        //    Stopwatch lSW = new Stopwatch();
        //    lSW.Start();

        //    //Logger.LogMessage("WorkController.TaskGridViewPartialFilter");
        //    Session["WorkFilterType"] = FilterType;
        //    using (IDbContextScope lScope = new DbContextScopeFactory().Create())
        //    {
        //        var lData = new WorkBusiness(lScope).getFilteredWork(FilterType, AccountHelper.GetCurrentEmployee(), GetClientOrgID()).OrderBy(x => x.StartDate);

        //        Logger.LogStats("==> WorkController.TaskGridViewPartial ", lSW.ElapsedMilliseconds.ToString());

        //        return PartialView("~/Views/Shared/_TaskGridViewPartial.cshtml", lData);
        //    }
        //}

        ///// <summary>
        ///// This method is called from the client to open the edit screen for a task
        ///// </summary>
        ///// <param name="taskId">Work ID</param>
        ///// <returns></returns>
        //[CustomAuthorize]
        //public ActionResult Details(int taskId)
        //{
        //    Stopwatch lSW = new Stopwatch();
        //    lSW.Start();
        //    try
        //    {
        //        using (IDbContextScope lScope = new DbContextScopeFactory().Create())
        //        {
        //            UrlHelper lHelper = new UrlHelper(this.ControllerContext.RequestContext);
        //            //ViewBag.Url = Request.UrlReferrer;
        //            PushOnURLStack();

        //            if (ViewBag.Url == null)
        //                ViewBag.Url = lHelper.Action("Dashboard", "Home");
        //            long lTime = lSW.ElapsedMilliseconds;
        //            WorkSnapShot lSnapShot = new WorkBusiness(lScope).CreateWorkSnapShot(taskId, AccountHelper.GetCurrentUserId(), GetClientOrgID(), false, null);
        //            lSnapShot.CompleteList = GetCompletionList();
        //            long lTime2 = lSW.ElapsedMilliseconds;
        //            ViewBag.Title = lSnapShot.ProjectName;
        //            Logger.LogStats("==> WorkController.Details", lSW.ElapsedMilliseconds.ToString());
        //            return View("~/Views/Work/WorkItem.cshtml", lSnapShot);
        //        }
        //    }
        //    finally
        //    {
        //        lSW.Stop();
        //        Logger.LogStats("WorkController.Details view render", lSW.ElapsedMilliseconds.ToString());
        //    }
        //}

        //public List<Complete> GetCompletionList()
        //{
        //    List<Complete> list = new List<Complete>();

        //    list.Add(new Complete() { ID = 0, Name = "0%" });
        //    list.Add(new Complete() { ID = 25, Name = "25%" });
        //    list.Add(new Complete() { ID = 50, Name = "50%" });
        //    list.Add(new Complete() { ID = 75, Name = "75%" });
        //    list.Add(new Complete() { ID = 100, Name = "100%" });

        //    return list;
        //}

        //public ActionResult SlippageNotification(int slipageId)
        //{
        //    Slippage model = new Slippage();
        //    var view = RazorViewEngineHelper.RenderViewToString(ControllerContext, @"~\Views\Work\_PartialSlippageNotification.cshtml", model, true);

        //    return Json(new { view = view }, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult NewsFeedBySource(int? aPage, int sourceId)
        //{
        //    Stopwatch lSW = new Stopwatch();
        //    lSW.Start();
        //    ActionResult lResult = null;

        //    try
        //    {
        //        using (IDbContextScope lScope = new DbContextScopeFactory().Create())
        //        {
        //            if (aPage == null) { aPage = 1; }
        //            var lNewsData = new NewsFeedBusiness(lScope).BuildNewsSnapShot(AccountHelper.GetCurrentOrgID(), sourceId);

        //            if (aPage == null) { aPage = 1; }
        //            lNewsData.TotalItems = lNewsData.Parent.Count();
        //            lNewsData.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)lNewsData.Parent.Count() / NewsPageSize));
        //            lNewsData.Parent = lNewsData.Parent.Skip(NewsPageSize * (aPage.Value - 1)).Take(NewsPageSize).ToList();
        //            lNewsData.CurrentPage = aPage.Value;

        //            lResult = PartialView("~/Views/Shared/_NewsFeed.cshtml", lNewsData);
        //        }
        //    }
        //    finally
        //    {
        //        lSW.Stop();
        //        Logger.LogStats("==>WorkController.NewFeedBySource", lSW.ElapsedMilliseconds.ToString());
        //    }

        //    return lResult;
        //}




        //[ValidateInput(false)]
        //public ActionResult TasksUnderProject(int projectId, bool aViewClosedTasks)
        //{
        //    Session["ViewClosedTasks"] = aViewClosedTasks;
        //    List<WorkSnapShot> lSnapShot = new List<WorkSnapShot>();

        //    try
        //    {
        //        // 4/29 - Why would I need the projectID from the session.   
        //        //projectId = (int)Session["ProjectID"];
        //    }
        //    catch (Exception)
        //    {
        //    }

        //    try
        //    {
        //        var lViewClosedTasks = (bool)Session["ViewClosedTasks"];
        //        using (IDbContextScope lScope = new DbContextScopeFactory().Create())
        //        {
        //            var lWorkListModel = new ProjectBusiness(lScope).getProjectWork((projectId > 0 ? projectId : 0), lViewClosedTasks);

        //            foreach (Work lWork in lWorkListModel)
        //            {
        //                WorkSnapShot lSnap = new WorkSnapShot();
        //                lSnap.Copy(lWork);
        //                lSnapShot.Add(lSnap);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.LogException("Failed to get tasks under project", e);
        //    }

        //    return PartialView("~/Views/Shared/_ShowTaskByProject.cshtml", lSnapShot);
        //}

        //[ValidateInput(false)]
        //public ActionResult TasksUnderUserStory(int userStoryId)
        //{
        //    ViewData["taskUserStoryId"] = userStoryId;
        //    using (IDbContextScope lScope = new DbContextScopeFactory().Create())
        //    {
        //        UserStoryBusiness lUserStoryBusiness = new UserStoryBusiness(lScope);
        //        IEnumerable<Common.Work> lWorks = lUserStoryBusiness.getWork(userStoryId).Where(x => x.Status != Work.StatusType.DELETED).OrderBy(x => x.StartDate);
        //        if (userStoryId > 0)
        //        {
        //            lWorks = lWorks.Where(p => p.UserStoryID == userStoryId && p.DataType == Work.WorkTypes.WORK).ToList();
        //        }
        //        else
        //        {
        //            lWorks = new List<Common.Work>();
        //        }

        //        ViewBag.UserStoryId = userStoryId;

        //        List<WorkSnapShot> lModel = new List<WorkSnapShot>();
        //        foreach ( var lWork in lWorks)
        //            lModel.Add(new WorkSnapShot(lWork));
        //        return PartialView("~/Views/Shared/_ShowTaskByUserStory.cshtml", lModel);
        //    }
        //}

        //[CustomAuthorize]
        //public ActionResult NewIssueForStory(int storyId, int ProjectID)
        //{
        //    //ViewBag.Url = Request.UrlReferrer;
        //    PushOnURLStack();

        //    ViewBag.Title = "New Issue";
        //    using (IDbContextScope lScope = new DbContextScopeFactory().Create())
        //    {
        //        WorkSnapShot lSnapShot = new WorkBusiness(lScope).CreateWorkSnapShot(0, AccountHelper.GetCurrentUserId(), AccountHelper.GetCurrentOrgID(), false, null);
        //        lSnapShot.DataType = Work.WorkTypes.ISSUE;

        //        if (storyId > 0)
        //        {
        //            lSnapShot.UserStoryID = storyId;
        //        }

        //        lSnapShot.ProjectID = ProjectID;
        //        var lProject = new ProjectBusiness(lScope).GetProjectById(ProjectID);
        //        if (lProject != null)
        //            lSnapShot.ProjectName = lProject.ProjectName;

        //        return View(@"~\Views\Work\WorkItem.cshtml", lSnapShot);
        //    }
        //}


        //[CustomAuthorize]
        //public ActionResult NewTaskForStory(int storyId, int? ProjectID, int? aSprintID)
        //{
        //    //ViewBag.Url = Request.UrlReferrer;
        //    PushOnURLStack();

        //    ViewBag.Title = "New Task";
        //    using (IDbContextScope lScope = new DbContextScopeFactory().Create())
        //    {
        //        WorkSnapShot lSnapShot = new WorkBusiness(lScope).CreateWorkSnapShot(0, AccountHelper.GetCurrentUserId(), GetClientOrgID(), false, null);
        //        if (storyId > 0)
        //        {
        //            lSnapShot.UserStoryID = storyId;
        //        }
        //        else
        //        {
        //            lSnapShot.SprintID = aSprintID;
        //            if (aSprintID > 0)
        //                lSnapShot.SprintName = new SprintBusiness(lScope).GetSprintById(aSprintID.Value).Name;
        //        }

        //        lSnapShot.ProjectID = ProjectID;
        //        if (ProjectID != null)
        //        {
        //            var lProject = new ProjectBusiness(lScope).GetProjectById(ProjectID.Value);
        //            if (lProject != null)
        //                lSnapShot.ProjectName = lProject.ProjectName;
        //        }
        //        return View(@"~\Views\Work\WorkItem.cshtml", lSnapShot);
        //    }
        //}


        //// GET: api/Work
        //public IEnumerable<Common.Work> GetWorks()
        //{
        //    Logger.LogMessage("WorkController::GetWorks called");
        //    string lID = User.Identity.Name;
        //    using (IDbContextScope lScope = new DbContextScopeFactory().Create())
        //    {
        //        return (new WorkBusiness(lScope)).getUserWork(AccountHelper.GetCurrentUserId());
        //    }
        //}

        //[ValidateInput(false)]
        //public ActionResult ExistingTaskGrid(int? aProjectID)
        //{
        //    Stopwatch lSW = new Stopwatch();
        //    lSW.Start();
        //    var lWorkSnapShot = new WorkDataAccess(new DbContextScope()).GetWorkSnapShotByProject(aProjectID);
        //    Logger.LogStats("WorkController.ExistingTaskGrid", lSW.ElapsedMilliseconds.ToString());
        //    return PartialView("~/Views/Work/_ExistingTasks.cshtml", lWorkSnapShot);
        //}

        //private object GetLighWorkSnapShot(IOrderedEnumerable<Work> aProjectWork)
        //{
        //    List<WorkSnapShot> lResults = new List<WorkSnapShot>();
        //    foreach (var lWork in aProjectWork)
        //    {
        //        lResults.Add(new WorkSnapShot(lWork));
        //    }

        //    return lResults;
        //}

        //[ValidateInput(false)]
        //public ActionResult WorkDependencyGrid(int workId)
        //{
        //    Stopwatch lSW = new Stopwatch();
        //    lSW.Start();
        //    try
        //    {
        //        using (IDbContextScope lScope = new DbContextScopeFactory().Create())
        //        {
        //            if (workId > 0)
        //                return PartialView("~/Views/Work/_WorkDependencyGrid.cshtml", (new WorkBusiness(lScope)).GetWorkByID(workId).Dependancies);
        //            else
        //                return PartialView("~/Views/Work/_WorkDependencyGrid.cshtml", new List<Work>());
        //        }
        //    }
        //    finally
        //    {
        //        lSW.Stop();
        //        Logger.LogStats("WorkController.WorkDependencyGrid", lSW.ElapsedMilliseconds.ToString());
        //    }
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="workId">This work item is dependent on the SourceID</param>
        ///// <param name="sourceId"></param>
        ///// <returns></returns>
        //public ActionResult SaveDependency(int workId, int sourceId)
        //{
        //    IDbContextScope lScope = new DbContextScopeFactory().Create();
        //    SprintBusiness lSprintBusiness = new SprintBusiness(lScope);
        //    WorkBusiness lWorkBusiness = new WorkBusiness(lScope);

        //    try
        //    {
        //        EFDomain.WorkDependencies model = new EFDomain.WorkDependencies()
        //        {
        //            SourceId = sourceId,
        //            WorkId = workId
        //        };

        //        var response = lWorkBusiness.saveDependency(model);
        //        lScope.SaveChanges();
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.LogException("WorkController.WorkItem(HTTPPOST)", e);
        //    }

        //    return ExistingTaskGrid(lWorkBusiness.GetWorkByID(sourceId).ProjectID.Value);
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="workId">This work item is dependent on the SourceID</param>
        ///// <param name="sourceId"></param>
        ///// <returns></returns>
        //public ActionResult RemoveDependency(int workId, int sourceId)
        //{
        //    using (IDbContextScope lScope = new DbContextScopeFactory().Create())
        //    {
        //        SprintBusiness lSprintBusiness = new SprintBusiness(lScope);
        //        WorkBusiness lWorkBusiness = new WorkBusiness(lScope);

        //        try
        //        {
        //            EFDomain.WorkDependencies model = new EFDomain.WorkDependencies()
        //            {
        //                SourceId = sourceId,
        //                WorkId = workId
        //            };

        //            var response = lWorkBusiness.removeDependency(model);
        //            lScope.SaveChanges();
        //        }
        //        catch (Exception e)
        //        {
        //            Logger.LogException("WorkController.WorkItem(HTTPPOST)", e);
        //        }
        //        return WorkDependencyGrid(sourceId);
        //    }
        //}

        //[HttpPost]
        //public ActionResult DeleteTaskById(string taskId)
        //{

        //    return Content("OK");
        //}

        //[HttpPost]
        //public ActionResult MarkTaskComplete(int workId)
        //{
        //    using (IDbContextScope lScope = new DbContextScopeFactory().Create())
        //    {
        //        WorkBusiness lWorkBusiness = new WorkBusiness(lScope);
        //        var result = lWorkBusiness.UpdateStatusToComplete(workId);
        //        int lCount = lScope.SaveChanges();
        //        if (result)
        //            return Content("Success");
        //        else
        //            return Content("Failed");
        //    }
        //}

        //[HttpPost]
        //public async Task<ActionResult> UploadWorkFile(string imageDescription, int aProjectID, string aFileIDs, int workId)
        //{
        //    if (aFileIDs == null)
        //        aFileIDs = "";

        //    try
        //    {
        //        if (Request.Files.Count > 0)
        //        {
        //            aFileIDs = aFileIDs.TrimEnd(',');
        //            string[] lTagArray = aFileIDs.Split(',');

        //            var files = Request.Files[0];
        //            var fileName = System.IO.Path.GetFileName(files.FileName);
        //            if (System.Diagnostics.Debugger.IsAttached)
        //                fileName = "Test-" + fileName;

        //            var fileDirectory = Guid.NewGuid();

        //            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
        //            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
        //            CloudBlobContainer container = blobClient.GetContainerReference("maincontainer");
        //            CloudBlockBlob blockBlob = container.GetBlockBlobReference($"{fileDirectory}/{fileName}");
        //            await blockBlob.UploadFromStreamAsync(files.InputStream);

        //            IDbContextScope lScope = new DbContextScopeFactory().Create();
        //            FileDataAccess lFDA = new FileDataAccess(lScope);
        //            var lTDA = new TagDataAccess(lScope);
        //            Common.File lFile = new Common.File()
        //            {
        //                Description = imageDescription,
        //                Name = fileName,
        //                EmployeeID = AccountHelper.GetCurrentUserId(),
        //                ProjectID = aProjectID,
        //                Date = DateTime.Now,
        //                URI = blockBlob.Uri.AbsoluteUri,
        //                WorkID = workId,
        //                FileDirectoryGuid = fileDirectory
        //            };

        //            lFDA.Save(lFile);

        //            for (int i = 0; i < lTagArray.Length; i++)
        //            {
        //                int lTagID = 0;
        //                if (Int32.TryParse(lTagArray[i], out lTagID))
        //                {
        //                    var lTag = lTDA.FindById(lTagID);
        //                    lFile.Tags.Add(lTag);
        //                }
        //            }

        //            lScope.SaveChanges();
        //            new ReportEmailer().DocumentAddedEmail(lFile);

        //            return Json(new { success = true, message = "File uploaded successfully", filename = fileName, description = imageDescription }, JsonRequestBehavior.AllowGet);
        //        }
        //        return Json(new { success = false, message = "Please select a file !", filename = string.Empty, description = string.Empty }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.LogException("WorkController.UploadWorkFile(HTTPPOST)", ex);
        //        return Json(new { success = false, message = ex.Message, filename = string.Empty, description = string.Empty }, JsonRequestBehavior.AllowGet);
        //    }

        //}

        //[HttpPost]
        //public async Task<ActionResult> DeleteWorkFile(int fileId, string fileName, string fileDirectory)
        //{
        //    try
        //    {
        //        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
        //        CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
        //        CloudBlobContainer container = blobClient.GetContainerReference("maincontainer");
        //        CloudBlockBlob blockBlob = container.GetBlockBlobReference($"{fileDirectory}/{fileName}");

        //        var result = await blockBlob.DeleteIfExistsAsync();
        //        if (result)
        //        {
        //            using (IDbContextScope lScope = new DbContextScopeFactory().Create())
        //            {
        //                FileDataAccess lFDA = new FileDataAccess(lScope);
        //                lFDA.Delete(fileId);
        //                lScope.SaveChanges();

        //                return Json(new { success = result, message = "File deleted successfully" }, JsonRequestBehavior.AllowGet);
        //            }
        //        }
        //        else
        //        {
        //            return Json(new { success = result, message = "Error occured during file Delete" }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.LogException("WorkController.DeleteWorkFile(HTTPPOST)", ex);
        //        return Json(new { success = false, message = ex.Message, filename = string.Empty, description = string.Empty }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //[HttpPost]
        //public async Task<ActionResult> EditWorkFile(string description, string fileIDs, string currentFileName, string currentFileDirectory, int fileId)
        //{
        //    if (fileIDs == null)
        //        fileIDs = "";

        //    try
        //    {
        //        fileIDs = fileIDs.TrimEnd(',');
        //        string[] lTagArray = fileIDs.Split(',');

        //        using (IDbContextScope lScope = new DbContextScopeFactory().Create())
        //        {
        //            FileDataAccess lFDA = new FileDataAccess(lScope);
        //            var lTDA = new TagDataAccess(lScope);

        //            var fileForUpdate = lFDA.FindById(fileId);
        //            fileForUpdate.Description = description;
        //            var currentTags = fileForUpdate.Tags.ToList();
        //            foreach (var currentTag in currentTags)
        //            {
        //                fileForUpdate.Tags.Remove(currentTag); // removing TagFiles from linking table
        //            }

        //            for (int i = 0; i < lTagArray.Length; i++)
        //            {
        //                int lTagID = 0;
        //                if (Int32.TryParse(lTagArray[i], out lTagID))
        //                {
        //                    var lTag = lTDA.FindById(lTagID);
        //                    fileForUpdate.Tags.Add(lTag);
        //                }
        //            }

        //            if (Request.Files.Count > 0)
        //            {
        //                string fileName = string.Empty, blobUrl = string.Empty;
        //                var fileDirectory = Guid.NewGuid();
        //                var files = Request.Files[0];
        //                fileName = System.IO.Path.GetFileName(files.FileName);
        //                if (System.Diagnostics.Debugger.IsAttached)
        //                    fileName = "Test-" + fileName;

        //                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
        //                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
        //                CloudBlobContainer container = blobClient.GetContainerReference("maincontainer");
        //                CloudBlockBlob blockBlob = container.GetBlockBlobReference($"{currentFileDirectory}/{currentFileName}");
        //                var result = await blockBlob.DeleteIfExistsAsync();

        //                if (result)
        //                {
        //                    CloudBlockBlob uploadblockBlob = container.GetBlockBlobReference($"{fileDirectory}/{fileName}");
        //                    await uploadblockBlob.UploadFromStreamAsync(files.InputStream);

        //                    fileForUpdate.Name = fileName;
        //                    fileForUpdate.FileDirectoryGuid = fileDirectory;
        //                    fileForUpdate.URI = uploadblockBlob.Uri.AbsoluteUri;
        //                }
        //            }

        //            lScope.SaveChanges();

        //            return Json(new { success = true, message = "File modified successfully" }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.LogException("WorkController.EditWorkFile(HTTPPOST)", ex);
        //        return Json(new { success = false, message = ex.Message, filename = string.Empty, description = string.Empty }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //public ActionResult LoadEditFileUpload(int fileId)
        //{
        //    string view = string.Empty;
        //    try
        //    {
        //        using (IDbContextScope lScope = new DbContextScopeFactory().Create())
        //        {
        //            List<FileSnapShot> lFiles = new List<FileSnapShot>();
        //            var lData = new FileDataAccess(lScope).GetFiles(AccountHelper.GetCurrentOrgID()).FirstOrDefault(x => x.ID == fileId);


        //            ViewBag.fileId = fileId;
        //            view = RazorViewEngineHelper.RenderViewToString(ControllerContext, @"~\Views\Shared\_EditFileDialog.cshtml", lData, true);
        //            return Json(new { view = view }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.LogException("WorkController.LoadEditFileUpload", ex);
        //    }
        //    return Json(new { view = view }, JsonRequestBehavior.AllowGet);
        //}

        //[ValidateInput(false)]
        //public ActionResult FilesUnderWork(int workId)
        //{
        //    Stopwatch lSW = new Stopwatch();
        //    lSW.Start();

        //    ViewData["WorkId"] = workId;

        //    using (IDbContextScope lScope = new DbContextScopeFactory().Create())
        //    {
        //        List<FileSnapShot> lFiles = new List<FileSnapShot>();
        //        var lData = new FileDataAccess(lScope).GetFiles(AccountHelper.GetCurrentOrgID()).Where(x => x.WorkID == workId);
        //        foreach (var lFile in lData)
        //        {
        //            lFile.Owner = new EmployeeBusiness(lScope).GetEmployeeById(lFile.EmployeeID);
        //            lFiles.Add(new FileSnapShot(lFile));
        //        }

        //        Logger.LogStats("==>WorkController.UploadWorkFile", lSW.ElapsedMilliseconds.ToString());

        //        return PartialView("~/Views/Shared/_FilesUnderWork.cshtml", lFiles);
        //    }
        //}
    }
}