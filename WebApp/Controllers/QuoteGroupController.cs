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

namespace WebApp.Controllers
{
    public class QuoteGroupController : BaseController
    {
        const string ClassName = "QuoteGroupController::";
        public IConfiguration Configuration { get; }

        public QuoteGroupController(IDbContext context, IConfiguration aConfig = null) : base(context)
        {
            Configuration = aConfig;
        }

        // GET: QuoteGroupController
        public ActionResult Index()
        {
            return View();
        }

        // GET: QuoteGroupController/Details/5
        public ActionResult View(int id)
        {
            string FuncName = ClassName + $"Details(int = {id})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lQGBiz = new QuoteGroupBusiness(mContext);
                var lQuoteGroup = lQGBiz.Get(id);
                if (lQuoteGroup == null)
                {
                    Logger.LogWarning(FuncName + "Can't dislay QuoteView because Quote was null for id = " + id);
                    return RedirectToAction(nameof(PortalController.Index), "Portal");
                }
                var lUIQG = QuoteGroupUIHelper.Convert(mContext, lQuoteGroup);
                lUIQG.WarningNoEmail = false;
                try
                {
                    lUIQG.WarningNoEmail = new OpportunityBusiness(mContext).GetOpportunity(lQuoteGroup.QuoteRequest.OpportunityID).Travelers.First().User.PrimaryEmail.Length == 0;
                } catch (Exception )
                { 
                    lUIQG.WarningNoEmail = true;
                }
                lUIQG.CountryData = new CountryPageDataAccess(mContext).Get(18);
                return View(lUIQG);
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

        // GET: QuoteGroupController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuoteGroupController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        public ActionResult SendQuote(int id)
        {
            string FuncName = ClassName + $"SendQuoteGroup(aQuoteGroupId = {id})";
            Logger.EnterFunction(FuncName);

            try
            {
                var lQGBiz = new QuoteGroupBusiness(DbContext, Configuration);
                var lGroup = lQGBiz.Get(id);

                // Send notification to user to activate their portal account
                var lContactBiz = new ContactBusiness(mContext);
                var lTravler =  lGroup.QuoteRequest.Opportunity.Travelers[0];
                var lContact = lTravler.User;

                if (lQGBiz.SendQuoteGroup(GetCurrentAgent(),lGroup))
                    return RedirectToAction("View", "Opportunity", new { id = lGroup.QuoteRequest.OpportunityID });
                else
                    return RedirectToAction("Edit", "QuoteRequest", new { id = lGroup.QuoteRequest.QuoteRequestID });
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + " Failed to SendQuoteGroup", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }

            return RedirectToAction("Index", "Portal");
        }

        // GET: QuoteGroupController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: QuoteGroupController/Edit/5
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

        // GET: QuoteGroupController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: QuoteGroupController/Delete/5
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
