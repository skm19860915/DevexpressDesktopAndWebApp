using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Security.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using BlitzerCore.Models;
using BlitzerCore.Utilities;
using BlitzerCore.Business;
using BlitzerCore.DataAccess;
using BlitzerCore.Models.UI;
using BlitzerCore.Helpers;
using BlitzerCore.UIHelpers;
using WebApp.AspNetHelper;
using WebApp.Services;
using WebApp.SrvUtilities;
using Microsoft.Extensions.DependencyInjection;

namespace WebApp.Controllers
{
    public class QuoteRequestController : BaseController
    {
        public IConfiguration Configuration { get; }
        const string ClassName = "QuoteRequestController::";
        public IBackgroundTaskQueue _queue { get; }
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public QuoteRequestController(IDbContext context, IConfiguration aConfig, IBackgroundTaskQueue queue, IServiceScopeFactory serviceScopeFactory) : base(context)
        {
            Configuration = aConfig;
            _queue = queue;
            _serviceScopeFactory = serviceScopeFactory;
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            string FuncName = ClassName + $"Edit(QuoteRequestId = {id})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lQRBiz = new QuoteRequestBusiness(mContext, Configuration);
                var lQGBiz = new QuoteGroupBusiness(mContext);
                var lUIQR = new BlitzerCore.Models.UI.UIQuoteRequest();
                if (id == null)
                {
                    lUIQR.Contacts = lQRBiz.GetDefaultClients();
                    lUIQR.AgentId = GetCurrentAgent().Id;
                }
                else
                {
                    var lQR = lQRBiz.GetQuoteRequest(id.Value);
                    lUIQR = QuoteRequestUIHelper.Convert(mContext, lQR, true);
                    //lUIQR.Quotes = lUIQR.Quotes.Where(x => x.Status == QuoteStatus.NotReady || x.Status == QuoteStatus.Ready).ToList();
                    lUIQR.ActiveQuoteGroups = QuoteGroupUIHelper.Convert(mContext, lQR.QuoteGroups);
                    lUIQR.EnableSendQuoteBtn = new QuoteRequestBusiness(mContext).IsReadyToSend(lQR);
                }
                PopulateLookups(lUIQR);
                return View(lUIQR);
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Exception ", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
            return RedirectToAction("Index", "Portal");
        }

        // POST: QuoteController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] UIQuoteRequest aQuoteRequest)
        {
            string FuncName = ClassName + $"Edit (UIQuoteRequest = {aQuoteRequest.Id})";
            Logger.EnterFunction(FuncName);
            try
            {
                // BUG HACK.  For new QRs the ID = the OppID.  Then you know it is incorrect
                if (aQuoteRequest.OpportunityID == aQuoteRequest.Id)
                    aQuoteRequest.Id = new OpportunityBusiness(mContext).GetOpportunity(aQuoteRequest.OpportunityID).QuoteRequests.Last().QuoteRequestID;

                new QuoteRequestBusiness(mContext).Save(aQuoteRequest, GetCurrentAgent());
                return RedirectToAction("Index", "Portal");
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + " Failed to update QuoteRequest", e);
                throw new InvalidOperationException(e.Message);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }

        // POST: QuoteController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PullQuotes([FromForm] UIQuoteRequest aQuoteRequest)
        {
            string FuncName = ClassName + $"PullQuotes (UIQuoteRequest = {aQuoteRequest.Id})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lQR = new QuoteRequestBusiness(mContext).Save(aQuoteRequest, GetCurrentAgent());
                return RedirectToAction("Search", "QuoteRequest", new { id = lQR.QuoteRequestID });
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + " Failed to update QuoteRequest", e);
                throw new InvalidOperationException(e.Message);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }

        public void PopulateLookups(UIQuoteRequest aUIQR)
        {
            aUIQR.AirPortCodes = ListHelper.GetAirPortCodes(new TravelBusiness(mContext).GetAirPorts());
            aUIQR.RelationShips = ListHelper.GetRelationShips(new RelationshipDataAccess(mContext).GetRelationships());
            aUIQR.QuoteButtonDisabled = (TourOperatorRegistry.GetAllWebBots(mContext).Count() == aUIQR.ErrorMsgs.Count());
            ViewBag.Refferals = ListHelper.GetRefferals(new BlitzerDataAccess(mContext).GetRefferals());
            ViewBag.NumberOfAdults = ListHelper.GetNumberOfAdultsList();
            ViewBag.KidsAges = ListHelper.GetKidsAgesList();
            ViewBag.QuoteTypes = ListHelper.GetQuoteTypes();
        }

        /// <summary>
        /// Shows all the flights for a QuoteGroup
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SelectFlights(int id)
        {
            string FuncName = ClassName + $"SelectFlights (UIQuoteRequest = {id})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lQG = new QuoteGroupBusiness(mContext).Get(id);
                var lUIQG = QuoteGroupUIHelper.ConvertFlights(mContext, lQG);
                return View(lUIQG);
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to retrieve quote view", e);
                throw new InvalidOperationException(e.Message);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }

        [HttpGet]
        public ActionResult FlightSelected(int id, int aQuoteGroupId)
        {
            string FuncName = ClassName + $"FlightSelected ({id}, {aQuoteGroupId})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lTicket = new QuoteDataAccess(mContext).GetTicket(id);
                var lQG = new QuoteGroupBusiness(mContext).Get(lTicket.QuoteGroupId);
                lQG.SelectedQuoteRequestTicketId = id;
                new QuoteGroupBusiness(mContext).Save(lQG);
                return RedirectToAction("Edit", new { id = lQG.QuoteRequestID });
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to retrieve quote view", e);
                throw new InvalidOperationException(e.Message);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }

        [HttpGet]
        public ActionResult Prices(int id)
        {
            var lQR = new QuoteRequestBusiness(mContext).GetQuoteRequest(id);
            //var lDataSet = 
            //if (lQR != null)
            //{
            //}
            //else
            //{
            //    var lQR = new QuoteRequestBusiness(mContext).GetQuoteRequest(id.Value);
            //    lUIQR = QuoteRequestUIHelper.Convert(mContext, lQR);
            //}
            //return View(lUIQR);
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create New Quote from Opportunity.  This is only called after the opp
        /// and QR has been created.  So Create a duplicate QR based on the initial info
        /// </summary>
        /// <param name="id">Opportunity ID</param>
        /// <returns></returns>
        public ActionResult New(int id)
        {
            var lOppBiz = new OpportunityBusiness(mContext);
            var lOpp = lOppBiz.GetOpportunity(id);
            var lQRS = lOpp.QuoteRequests;
            var lQR = lOpp.QuoteRequests.First();
            var QRBiz = new QuoteRequestBusiness(mContext);

            // Need to pull in all the underlying objects such as airports
            lQR = QRBiz.Get(lQR.QuoteRequestID);

            var lNewQR = QRBiz.Create(lQR.Agent, lQR.DepartureAirPort.Code, lQR.DestinationAirPort.Code, lOpp);
            lNewQR.DepartureDate = lQR.DepartureDate;
            lNewQR.ReturnDate = lQR.ReturnDate;
            lNewQR.QuoteType = lQR.QuoteType;
            lNewQR.NumberOfAdults = lQR.NumberOfAdults;
            lNewQR.Child1Age = lQR.Child1Age;
            lNewQR.Child2Age = lQR.Child2Age;
            var lQRUI = QuoteRequestUIHelper.Convert(mContext, lNewQR);
            PopulateLookups(lQRUI);
            new QuoteRequestBusiness(mContext).Save(lNewQR, GetCurrentAgent());
            return RedirectToAction("Edit", new { id = lNewQR.QuoteRequestID });
        }

        public ActionResult Preview(int id)
        {
            string FuncName = ClassName + $"Preview(QuoteRequestId = {id})";
            Logger.EnterFunction(FuncName);

            try
            {
                var lQuoteGroupBiz = new QuoteGroupBusiness(mContext);
                var lQuoteRequest = new QuoteRequestBusiness(mContext).GetQuoteRequest(id);
                var lQuoteGroup = lQuoteGroupBiz.Preview(lQuoteRequest);
                return RedirectToAction("View", "QuoteGroup", new { id = lQuoteGroup.Id });
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + " Failed to preview group", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }

            return StatusCode(500);
        }

        // GET: QuoteGroupController/Details/5
        public ActionResult ViewBotResults(int id)
        {
            string FuncName = ClassName + $"ViewBotResults(int = {id})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lQGBiz = new QuoteGroupBusiness(mContext);
                var lQuoteGroup = lQGBiz.Get(id);
                var lUIQuoteResult = lQGBiz.GetBotResults(lQuoteGroup);
                return View(lUIQuoteResult);
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to retrieve quote view", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }

            throw new InvalidOperationException();
        }

        /// <summary>
        /// Travelers call this method with a GUID to view the quote
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tck">GUID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Client(int id, string tck)
        {
            string FuncName = ClassName + $"Client (int= {id}, tck)";
            Logger.EnterFunction(FuncName);

            try
            {
                if (new QuoteGroupBusiness(mContext).ValidTicket(id, tck))
                    return RedirectToAction("View", "QuoteGroup", new { id = id });

                return Redirect("~/");
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to retrieve traveler view", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }

            throw new InvalidOperationException();

        }

        [HttpGet]
        public ActionResult Search(int id, List<IWebTravelSrv> aWebBots)
        {
            string FuncName = ClassName + "SearchWrapper";
            Logger.EnterFunction(FuncName);
            try
            {
                var lCurrentAgent = GetCurrentAgent();

                _queue.QueueBackgroundWorkItem(token => {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        System.Threading.Thread.Sleep(0);
                        string FuncName2 = ClassName + "Search";
                        try
                        {
                            Logger.EnterFunction(FuncName2);
                            var lDbContext = scope.ServiceProvider.GetService<IDbContext>();
                            var lQRBiz2 = new QuoteRequestBusiness(lDbContext, Configuration);
                            var lQuoteRequest2 = lQRBiz2.Get(id);
                            var lQuoteGroup = lQRBiz2.GetOpenQuoteGroup(lQuoteRequest2);
                            var lQuote = lQRBiz2.Search(lQuoteGroup, lCurrentAgent, aWebBots);
                        }
                        finally
                        {
                            Logger.LeaveFunction(FuncName2);
                        }
                    }

                    return System.Threading.Tasks.Task.CompletedTask;
                });
                var lQRBiz = new QuoteRequestBusiness(mContext, Configuration);
                var lQuoteRequest = lQRBiz.Get(id);
                return RedirectToAction("View", "Opportunity", new { id = lQuoteRequest.OpportunityID });
            }
            catch (Exception e)
            {
                //aQuoteRequest.ErrorMsgs.Add(new ErrorMsg() { Header = "Quote Failed", Description = e.Message });
                Logger.LogException("Quote Search Failed", e);

                return RedirectToAction("Edit", new { id = id });
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }


    }
}
