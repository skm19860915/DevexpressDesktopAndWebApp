using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using BlitzerCore.Business;
using BlitzerCore.Models;
using BlitzerCore.Utilities;
using BlitzerCore.Models.UI;
using BlitzerCore.Helpers;
using BlitzerCore.UIHelpers;
using BlitzerCore.DataAccess;
using WebApp.AspNetHelper;
using WebApp.DataServices;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;

namespace WebApp.Controllers
{
    public enum TripViewMode { Active, Closed, All }
    public class TripController : BaseController
    {
        public const string ClassName = "TripController::";
        const long MaxBlobSize = 1048576;

        CloudBlobClient _client;
        IConfiguration Configuration { get; }

        public TripController(IDbContext aContext, UserManager<BlitzerUser> userManager, IConfiguration aConfig) : base(aContext, userManager)
        {
            Configuration = aConfig;
        }



        // GET: BookingController/Details/5
        /// <summary>
        /// Displays a list of Travels in the Booking
        /// </summary>
        /// <param name="id">QuoteID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Profiles(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Profiles()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm]UITrip aUITrip)
        {
            var lTripBiz = new TripBusiness(mContext);
            var lUITrip = Convert(aUITrip);
            var lTrip = lTripBiz.Get(aUITrip.Id);
            MergeTrips(lTrip, lUITrip);
            int lCount = lTripBiz.Save(lTrip);
            return RedirectToAction("Create", "Booking", new { id = lTrip.ID });
        }

        private void MergeTrips(Trip aTrip, Trip aUITrip)
        {
            aTrip.Name = aUITrip.Name;
        }

        Trip Convert ( UITrip aTrip )
        {
            Trip lOutput = new TripBusiness(mContext).Get( aTrip.Id);
            if (lOutput == null)
                lOutput = new Trip();

            // lOutput.AgentID = aTrip.AgentID;
            lOutput.Balance = DataHelper.ConvertFromCurrency( aTrip.Balance);
            lOutput.DaysToStart = aTrip.DaysToStart;
            lOutput.StartDate = DataHelper.GetDateTime(aTrip.OutBoundDate);
            lOutput.EndDate = DataHelper.GetDateTime( aTrip.OutBoundDate);
            lOutput.Name = aTrip.Name;
            lOutput.ID = aTrip.Id;

            return lOutput;
        }

        [HttpGet]
        public ActionResult Index(TripViewMode aViewMode = TripViewMode.Active)
        {
            var lTripBiz = new TripBusiness(mContext);
            Agent lAgent = GetCurrentAgent();
            var lTrips = lTripBiz.GetAll(lAgent).Where(x=>x.TripStatus != Trip.Statuses.Deleted).ToList();
            if ( aViewMode == TripViewMode.Active )
                lTrips = lTrips.Where(x => x.TripStatus == Trip.Statuses.Active).ToList();
            else if (aViewMode == TripViewMode.Closed)
                lTrips = lTrips.Where(x => x.TripStatus == Trip.Statuses.Completed).ToList();
            else 
                lTrips = lTrips.Where(x => x.TripStatus != Trip.Statuses.Deleted).ToList();
            return View(TripUIHelper.ConvertList(lTrips));
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var lTripBiz= new TripBusiness(mContext);
            var lOppBiz = new OpportunityBusiness(mContext);
            var lTrip = lTripBiz.Get(id);
            if (lTrip == null)
            {
                var lOpp = lOppBiz.GetOpportunity(id);
                if (lOpp != null)
                    return RedirectToAction("View", "Opportunity", new { id = id });

                return RedirectToAction("Index", "Portal");
            }
            IsDanger(GetCurrentUser());

            UITrip lUITrip = TripUIHelper.Convert(lTrip);

            await SetReturnUrl(lUITrip);
            return View(lUITrip);
        }

        [HttpGet]
        public ActionResult ModifyUsers(int id)
        {
            var lTrip = new TripBusiness(DbContext).Get(id);
            var lUITrip = TripUIHelper.Convert(lTrip);
            return View(lUITrip);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var lTrip = new TripBusiness(DbContext).Get(id);
            ViewBag.OutboundAirPortID = ListHelper.GetAirPortIDs(new TravelBusiness(mContext).GetAirPorts());
            ViewBag.InboundAirPortID = ListHelper.GetAirPortIDs(new TravelBusiness(mContext).GetAirPorts());
            ViewBag.Agents = ListHelper.GetTeamMembers(new TeamBusiness(DbContext).GetTeamMembers(GetCurrentAgent()));
            IsDanger(GetCurrentUser());
            return View(lTrip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] Trip aTrip)
        {
            new TripBusiness(DbContext).Save(aTrip);
            return RedirectToAction(nameof(TripController.Details), new { id = aTrip.ID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details([FromForm] UITrip aTrip)
        {
            Note lNote = new Note()
            {
                Memo = aTrip.Note_Text,
                OpportunityId = aTrip.Id,
                When = DateTime.Now,
                Where = aTrip.Note_Where,
                Who = aTrip.Note_Who,
                WriterId = GetCurrentUserID()
            };

            // If the name changes, save the trip
            var lTripBiz = new TripBusiness(DbContext);
            var lTrip = lTripBiz.Get(aTrip.Id);
            lTripBiz.Save(lTrip);
            lTripBiz.SaveNote(lNote, lTrip);
            return ReturnToCaller(aTrip);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Agent ID</param>
        /// <returns></returns>
        [HttpGet]
        public async System.Threading.Tasks.Task<IActionResult> Kanban(string id = null)
        {
            var lTaskBusiness = new TaskBusiness(DbContext);
            List<BlitzerCore.Models.Task> lTasks = new List<BlitzerCore.Models.Task>();
            var lCurrentAgent = id == null ? GetCurrentAgent() : new ContactBusiness(mContext).GetAgent(id);
            if ( id != null )
                lTasks =lTaskBusiness.GetTripTasks(lCurrentAgent);
            else
                lTasks = lTaskBusiness.GetTripTasks(new CompanyBusiness(mContext).Get( lCurrentAgent.EmployerId));


            var lSource = id == null ? BlitzerCore.Models.Kanban.Source.CompanyTasks : BlitzerCore.Models.Kanban.Source.MyTasks;
            var lViewMode = BlitzerCore.Models.Kanban.ViewMode.MyTasks;
            var lProfile = new AgentDataAccess(mContext).GetAgentProfile(lCurrentAgent);
            if (lProfile != null)
                lViewMode = lProfile.ViewMode;

            // On the Kanban board, new to change the owner to the Reviewer with the task is in the Review State
            var lUITasks = TaskUIHelper.Convert(lTasks);
            var lUIRTasks = lUITasks.Where(x => x.Status == TaskStatusTypes.REVIEW);
            foreach ( var lUITask in lUIRTasks)
            {
                lUITask.OwnerID = lUITask.IssuerID;
                lUITask.OwnerName = lUITask.OwnerName;
            }
              
            await SecuritySetup();
            return View(new KanbanSprintModel
            {
                Sprint = null,
                Source = lSource,
                ViewMode = lViewMode,
                Tasks = lUITasks,
                Statuses = new TaskStatusTypes[] { TaskStatusTypes.NEW, TaskStatusTypes.INPROGRESS, TaskStatusTypes.REVIEW, TaskStatusTypes.ONHOLD, TaskStatusTypes.COMPLETED },
                Employees = new TeamBusiness(DbContext).GetTeamMembers(GetCurrentAgent())
            });
        }

        [HttpGet]
        public ActionResult Create(int id)
        {
            //todo : Find a new Opportunity 
            var lUITrip = TripUIHelper.Convert(new TripBusiness(mContext).Get(id));
            return View(lUITrip);
        }

        /// <summary>
        /// Send Final Payment Email
        /// </summary>
        /// <param name="id">Trip Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> FinalPayment(int id)
        {
            string FuncName = ClassName + $"FinalPayment (TridId = {id})";
            Logger.EnterFunction(FuncName);

            try
            {
                var lTrip = new TripBusiness(mContext).Get(id);
                await VerifyUsers(lTrip);
                new NotificationBusiness(mContext, Configuration).SendFinalPayment(lTrip, GetCurrentAgent());
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to send final payment", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
            return RedirectToAction(nameof(Details), new { id = id });
        }

        private async Task<bool> VerifyUsers(Trip lTrip)
        {
            var lContactHelper = new WebApp.BusinessHelpers.ContactHelper(DbContext);

            foreach (Contact lContact in lTrip.Travelers.Select(x => x.User))
                await lContactHelper.CreateUser(_userManager, Configuration, lContact);

            return true;
        }

        [HttpGet]
        public ActionResult PrintedDocs(int id)
        {
            string FuncName = ClassName + $"PrintedDocs (TridId = {id})";

            try
            {
                var lTripBiz = new TripBusiness(mContext);
                var lTrip = lTripBiz.Get(id);
                if (lTrip.DocumentsPrintedOn == null)
                    lTrip.DocumentsPrintedOn = DateTime.Now;
                else
                    lTrip.DocumentsPrintedOn = null;
                lTripBiz.Save(lTrip);
                var lNote = new Note()
                {
                    OpportunityId = lTrip.ID,
                    Memo = "Took documents to the post office",
                    When = DateTime.Now,
                    Where = "",
                    Who = null,
                    WriterId = GetCurrentUserID()
                };
                new NoteBusiness(mContext).Save(lNote, lTrip);
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName}Failed to save Trip", e);
            }
            return RedirectToAction(nameof(Details), new { id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewProfiles([FromForm] UIUserProfilesTabsModel aOpp = null)
        {
            return RedirectToAction(nameof(Create), new { id = aOpp.TripID });
        }

        // GET: BookingController/Details/5
        /// <summary>
        /// Displays a list of Travels in the Booking
        /// </summary>
        /// <param name="id">Trip ID</param>
        /// <returns></returns>
        [HttpGet()]
        public ActionResult NewProfiles(int id)
        {
            string aUserID = null;
            var lTrip = new TripBusiness(mContext).Get(id);
            if (lTrip == null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.NotFound;
                return View();
            } else
            {
                var lOutput = new UIUserProfilesTabsModel();

                foreach ( var lUserMap in lTrip.Travelers)
                {
                    lOutput.TravelerIDs.Add(lUserMap.UserID);
                    lOutput.TravelerNames.Add(lUserMap.User.Name);
                }

                lOutput.TripID = id;
                if (aUserID != null)
                    lOutput.ActiveClient = aUserID;
                else
                    lOutput.ActiveClient = lTrip.Travelers.FirstOrDefault().UserID;
                return View(lOutput);
            }
        }

        private bool Validate(UIQuoteGroup aQuote)
        {
            string FuncName = ClassName + $"Validate";
            if ( aQuote.Quotes == null )
            {
                ViewBag.ErrorMsgs = new List<ErrorMsg>();
                ViewBag.ErrorMsgs.Add(new ErrorMsg() { Header = "Error", Description = "You must select a quote" });
                Logger.LogError(FuncName + " QuoteGroup passed with null quote list");
                return false;
            }

            return true;
        }

        //// GET: api/Trip/5
        //[HttpGet("{aUserID}/{aTripID?}")]
        //public ActionResult GetSummary(string aUserID, int? aTripID)
        //{
        //    if (aTripID == null)
        //        return Ok(new TripBusiness(mContext).GetSummary(0));
        //    else
        //        return Ok(new TripBusiness(mContext).GetSummary(aTripID.Value));
        //}

        //// GET: api/Trip/5
        //[HttpGet]
        //public ActionResult GetDetail(string aUserID, string aWhen, int? aTripID)
        //{
        //    Logger.LogInfo("User : " + aUserID + " called Get with TripID : " + aTripID);
        //    if (aTripID == null)
        //        return Ok(new TripBusiness(mContext).GetDetail(0, aWhen));
        //    else
        //        return Ok(new TripBusiness(mContext).GetDetail(aTripID.Value, aWhen));
        //}

        // POST: api/Trip
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Trip/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // POST: api/Trip
        [HttpPost]
        public int SaveFileDetail(UIFile fileInfo)
        {
            File lFile = new File()
            {
                Name = fileInfo.Name,
                OpportunityId = fileInfo.OpportunityId,
                Date = DateTime.Now,
                FileTypeId = fileInfo.FileTypeId,
                URI = fileInfo.URI,
                Version = 1,
                Description = fileInfo.Description,
                ID = fileInfo.ID
            };


            var id = new FileBusiness(DbContext).Save(lFile);
            return id;
        }

        CloudBlobClient Client
        {
            get
            {
                if (this._client == null)
                {
                    //AzureStorageAccount accountModel = AzureStorageAccount.FileUploader;
                    //var credentials = new StorageCredentials(accountModel.AccountName, accountModel.AccessKey);
                    var credentials = new StorageCredentials("blitzerblobs", "3hJlPM3Uv2FEdNW2+G63nlbrw+p0O8dOsowxGhCntw6mB3dVQzjJCl8vXyoLHzTp1HWhNvnKNoWUCZhUYmGNAA==");
                    var account = new CloudStorageAccount(credentials, true);
                    this._client = account.CreateCloudBlobClient();
                }
                return this._client;
            }
        }

        CloudBlobContainer _container;
        CloudBlobContainer Container
        {
            get
            {
                if (this._container == null)
                {
                    //AzureStorageAccount accountModel = AzureStorageAccount.FileUploader;
                    //this._container = Client.GetContainerReference(accountModel.ContainerName);
                    this._container = Client.GetContainerReference("documents");
                }
                return this._container;
            }
        }

        //[Route("api/file-uploader-azure-access", Name = "FileTripUploaderAzureAccessApi")]
        public object Process(string command, string blobName)
        {
            try
            {
                return UploadBlob(blobName);
            }
            catch
            {
                return CreateErrorResult();
            }
        }

        object UploadBlob(string blobName)
        {
            if (blobName.Contains("/"))
                return CreateErrorResult("Invalid blob name.");

            string prefix = Guid.NewGuid().ToString("N");
            string fullBlobName = $"{prefix}_{blobName}";
            CloudBlockBlob blob = Container.GetBlockBlobReference(fullBlobName);

            if (blob.Exists() && blob.Properties.Length > MaxBlobSize)
            {
                blob.Delete();
                return CreateErrorResult();
            }

            var policy = new SharedAccessBlobPolicy
            {
                SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(1),
                Permissions = SharedAccessBlobPermissions.Write
            };
            string url = blob.Uri + blob.GetSharedAccessSignature(policy, null, null, SharedAccessProtocol.HttpsOnly, null);

            return CreateSuccessResult(url);
        }

        object CreateSuccessResult(string url, string url2 = null)
        {
            return new
            {
                success = true,
                accessUrl = url,
                accessUrl2 = url2
            };
        }

        object CreateErrorResult(string error = null)
        {
            if (string.IsNullOrEmpty(error))
                error = "Unspecified error.";

            return new
            {
                success = false,
                error = error
            };
        }
    }

}

