using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
using WebApp.BusinessHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;

namespace WebApp.Controllers
{
    [Authorize]
    public class OpportunityController : BaseController
    {
        IConfiguration mConfig;
        //IWebTravelSrv mWebBot = null;
        //IWebTravelSrv mAAVacationSrv = null;
        private RoleManager<IdentityRole> RoleManager { get; }
        const string ClassName = "OpportunityController::";
        const long MaxBlobSize = 1048576;

        CloudBlobClient _client;

        public OpportunityController(IDbContext context,IConfiguration aConfig, UserManager<BlitzerUser> userManager, RoleManager<IdentityRole> roleManager) : base(context, userManager)
        {
            mConfig = aConfig;
            //mWebBot = aWebBot;
            //mAAVacationSrv = aAAVacation;
            RoleManager = roleManager;
        }

        // GET: Opportunity
        public ActionResult Index()
        {
            return View();
        }

        // GET: Opportunity/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult ChangeOwner ( int aOppId, string aNewOwner)
        {
            var lOutputUrl = RedirectToAction("View", "Opportunity", new { id = aOppId });
            var lOpp = new OpportunityBusiness(DbContext).GetOpportunity(aOppId);
            if (lOpp == null)
                return lOutputUrl;

            var lAgent = new ContactBusiness(DbContext).GetAgent(aNewOwner);
            if ( lAgent == null )
                return lOutputUrl;

            lOpp.Agent = lAgent;
            new OpportunityBusiness(DbContext).Save(lOpp, lAgent);
            return lOutputUrl;
        }


        //[HttpGet("{id}/{aStageId}")]
        public ActionResult NewStage(int id, int aStageId)
        {
            string FuncName = ClassName + $"NewStage ('id = {id} StageID = {aStageId}')";

            try
            {
                var lOppBiz = new OpportunityBusiness(mContext);
                var lOpp = lOppBiz.GetOpportunity(id);
                lOpp.Stage = (OpportunityStages)aStageId;
                lOppBiz.Save(lOpp, GetCurrentUser());
                if ( lOpp.Stage == OpportunityStages.Loss )
                {
                    lOppBiz.Lost(lOppBiz.GetOpportunity(id));
                    return RedirectToAction("Index", "Portal");
                } 
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName}Failed to update Opportunity Stage", e);
            }
            return RedirectToAction(nameof(View), new { id = id });
        }

        /// <summary>
        /// Mark an opportunity as lost
        /// </summary>
        /// <param name="id">Opportunity ID</param>
        /// <returns></returns>
        public ActionResult Lost(int id)
        {
            var lOppBiz = new OpportunityBusiness(DbContext);
            lOppBiz.Lost(lOppBiz.GetOpportunity(id));
            return RedirectToAction("Index", "Portal");
        }

        // GET: Opportunity/Create
        public ActionResult New(string id)
        {
            string FuncName = $"{ClassName}New (string = {id})";
            try
            {
                Logger.EnterFunction(FuncName);
                var lContactId = id;
                var lContactBiz = new ContactBusiness(mContext);
                var lAgent = GetCurrentAgent();
                Logger.LogDebug("step1");
                var lContact = lContactBiz.Get(lContactId);
                //Logger.LogDebug("step1.5");
                var lQR = new QuoteRequestBusiness(mContext).New(lContact, lAgent);
                Logger.LogDebug("step2");
                var lUIQR = QuoteRequestUIHelper.Convert(mContext, lQR);
                Logger.LogDebug("step3");
                lUIQR.SendQuote = true;
                lUIQR.SendInsurance = false;
                Logger.LogDebug("step4");
                PopulateLookups(lUIQR);
                Logger.LogDebug("step5");
                return View(lUIQR);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }

        // POST: Opportunity/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> New(UIQuoteRequest aQuoteRequest)
        {
            string FuncName = $"{ClassName}New (QuoteRequest)";
            try
            {
                Logger.EnterFunction(FuncName);
                List<ErrorMsg> lErrors = ValidateRequest(aQuoteRequest);
                if (lErrors.Count() == 0  )
                {
                    var lCBiz = new ContactBusiness(mContext);
                    var lUserExist = lCBiz.GetByEmail(aQuoteRequest.Contacts[0].PrimaryEmail);
                    var lQuoteRequest = new QuoteRequestBusiness(mContext).Save(aQuoteRequest, new ContactBusiness(DbContext).GetAgent(aQuoteRequest.AgentId));
                    new QuoteRequestBusiness(mContext).CreateTasks(lQuoteRequest, aQuoteRequest);

                    if (lUserExist == null)
                        await CreateUser(_userManager, mConfig, lQuoteRequest.Opportunity.Travelers[0].User);
                    else
                        Logger.LogWarning($"Not Creating a new system system because {lUserExist.Name} already existed");

                    return RedirectToAction("Edit", "QuoteRequest", new { id = lQuoteRequest.QuoteRequestID});
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(x => x.Errors).ToString();
                    Logger.LogInfo("Model state wasn't valid : "  + errors);
                    PopulateLookups(aQuoteRequest);
                    aQuoteRequest.ErrorMsgs = lErrors;
                    return View(aQuoteRequest);
                }
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName+" Failed to save new QuoteRequest", e);
                PopulateLookups(aQuoteRequest);
                return View(aQuoteRequest);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }

        private List<ErrorMsg> ValidateRequest(UIQuoteRequest aQuoteRequest)
        {
            List<ErrorMsg> lOutput = new List<ErrorMsg>();
            if (aQuoteRequest.RefferalId == null)
                lOutput.Add(new ErrorMsg() { Header = "You must enter a Refferer" });
            if (aQuoteRequest.DestinationCityCode == null || aQuoteRequest.DestinationCityCode == "")
                lOutput.Add(new ErrorMsg() { Header = "You must enter a Destination City Code" });
            if (aQuoteRequest.DepartureCityCode == null || aQuoteRequest.DepartureCityCode == "")
                lOutput.Add(new ErrorMsg() { Header = "You must enter a Departure City Code" });
            return lOutput;
        }

        private async Task<bool> CreateUser(UserManager<BlitzerUser> aUserManager, IConfiguration aConfig, Contact aUser)
        {
            string FuncName = $"{ClassName}CreateUser (UIContact={aUser.PrimaryEmail})";
            Logger.EnterFunction(FuncName);
            var lErrors = await new ContactHelper(DbContext).CreateUser(aUserManager, aConfig, aUser);
            if ( lErrors != null && lErrors.Count() > 0 )
            {
                string lMsg = "";
                foreach (IdentityError error in lErrors)
                {
                    ModelState.AddModelError("", error.Description);
                    lMsg += error.Description;
                }
                Logger.LogError($"{FuncName} - Failed to create AspNetUser : " + aUser.PrimaryEmail, lMsg);

            }
            return true;
        }

        // GET: Opportunity/Edit/5
        public ActionResult View(int id)
        {
            try
            {
                var lUser = GetCurrentUser();
                var lOpp = new OpportunityBusiness(mContext).GetOpportunity(new ContactDataAccess(mContext).GetAgent(lUser.Id), id);
                IsDanger(lUser);
                var lUIOpp = OpportunityUIHelper.Convert(mContext, lOpp);
                var lAgents = new CompanyBusiness(mContext).GetAgents(lUser.Employer);
                ViewBag.Agents = ListHelper.GetAgents(lAgents);
                return View(lUIOpp);
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to get Opportunity.View", e);
            }

            throw new InvalidOperationException();
        }

        public void PopulateLookups(UIQuoteRequest aUIQR)
        {
            aUIQR.AirPortCodes = ListHelper.GetAirPortCodes(new TravelBusiness(mContext).GetAirPorts());
            aUIQR.RelationShips = ListHelper.GetRelationShips(new RelationshipDataAccess(mContext).GetRelationships());
            aUIQR.QuoteButtonDisabled = (TourOperatorRegistry.GetAllWebBots(mContext).Count() == aUIQR.ErrorMsgs.Count());
            ViewBag.Refferals = ListHelper.GetRefferals(new BlitzerDataAccess(mContext).GetRefferals());
            ViewBag.NumberOfAdults = ListHelper.GetNumberOfAdultsList();
            ViewBag.KidsAges  = ListHelper.GetKidsAgesList();
            ViewBag.QuoteTypes = ListHelper.GetQuoteTypes();
        }

        public ActionResult Edit(int? id)
        {
            var lQRBiz = new QuoteRequestBusiness(mContext, mConfig);
            UIQuoteRequest lUIRequest = null;
            if (id == null || id == 0)
            {
                lUIRequest = lQRBiz.GetNewQuoteRequest(GetCurrentAgent());
            }
            else
            {
                lUIRequest = QuoteRequestUIHelper.Convert(mContext, lQRBiz.GetQuoteRequest(id.Value));
            }

            PopulateLookups(lUIRequest);
            return View(lUIRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save([FromForm] BlitzerCore.Models.UI.UIQuoteRequest aRequest)
        {
            string FuncName = $"{ClassName}Save (UIQuoteRequest = {aRequest.QuoteID})";
            Logger.EnterFunction(FuncName);
            try
            {
                new QuoteRequestBusiness(mContext).Save(aRequest, GetCurrentUser() as Agent);
                return RedirectToAction("Portal", "Home", new { aQuoteRequest = aRequest });
            }
            catch
            {
                return View();
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }

        // POST: Opportunity/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] BlitzerCore.Models.UI.UIOpportunity aUIRequest)
        {
            string FuncName = $"{ClassName}Edit (UIQuoteRequest = {aUIRequest.Id})";
            Logger.EnterFunction(FuncName);
            try
            {
                new OpportunityBusiness(mContext).Save(aUIRequest, GetCurrentUser());
                return RedirectToAction("Index", "Portal");
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + " Problem processing opportunity", e);
                Logger.LeaveFunction(FuncName);
                return RedirectToAction("Index", "Portal");
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }

        // POST: Opportunity/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNote([FromForm] BlitzerCore.Models.UI.UIOpportunity aUIRequest)
        {
            string FuncName = $"{ClassName}AddNote (UIQuoteRequest = {aUIRequest.Id})";
            Logger.EnterFunction(FuncName);
            try
            {
                Note lNote = new Note()
                {
                    Memo = aUIRequest.Note_Text,
                    OpportunityId = aUIRequest.Id,
                    When = DateTime.Now,
                    Where = aUIRequest.Note_Where,
                    Who = aUIRequest.Note_Who,
                    WriterId = GetCurrentUserID()
                };

                var lOppBiz = new OpportunityBusiness(mContext);
                var lOpp = lOppBiz.GetOpportunity(aUIRequest.Id);
                lOppBiz.Save(lNote, lOpp);

                return RedirectToAction("Index", "Portal");
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + " Problem processing opportunity", e);
                Logger.LeaveFunction(FuncName);
                return RedirectToAction("Index", "Portal");
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }

        // GET: Opportunity/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Opportunity/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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

        //[Route("api/file-uploader-azure-access", Name = "FileUploaderAzureAccessApi")]
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
            var lExists = blob.Exists();

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
