using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using Microsoft.AspNetCore.Identity;
using BlitzerCore.Business;
using BlitzerCore.Utilities;
using BlitzerCore.Helpers;
using BlitzerCore.DataAccess;
using WebApp.DataServices;

namespace WebApp.Controllers
{
    [ApiVersion("1.0")]
    public class HomeController : BaseController
    {
        private readonly UserManager<BlitzerUser> _UserManager;
        private readonly ILogger<HomeController> _logger;
        public IConfiguration Configuration { get; }
        public const string ClassName = "HomeController::";

        public HomeController( IConfiguration aConfig, UserManager<BlitzerUser> aUserManager, ILogger<HomeController> logger, IDbContext aContext) : base ( aContext)
        {
            _logger = logger;
            _UserManager = aUserManager;
            Configuration = aConfig;
        }

        [HttpGet()]
        public IActionResult Index()
        {
            var lUserID = GetSystemId();
            var lUser = _UserManager.FindByIdAsync(lUserID).Result;
            AppRoles lRole = AppRoles.Admin;

            var lAsyncRoles = _UserManager.GetRolesAsync(lUser);
            if (lUser != null && lAsyncRoles != null && lAsyncRoles.Result != null)
            {
                var roles = lAsyncRoles.Result;
                if (roles.Contains("Administator"))
                    lRole = AppRoles.Admin;
                else if (roles.Contains("Agent"))
                    lRole = AppRoles.Agent;
                else if (roles.Contains("Client"))
                    lRole = AppRoles.Client;

                ViewBag.IsAdmin = lRole == AppRoles.Admin;
                ViewBag.IsClient = lRole == AppRoles.Client;
            }

            return View();
        }

        [HttpGet()]
        public IActionResult Version()
        {
            return View(new BlitzerDataAccess(DbContext).GetLatestDBVersion());
        }

        [HttpPost()]
        public IActionResult QuoteRequest([FromForm] UIPublicQuoteRequest aRequest)
        {
            try
            {
                var lQR = new UIQuoteRequest();
                lQR.AgentId = "silke@eze2travel.com";
                lQR.DepartureCityCode = "RDU";
                lQR.DestinationCityCode = "RDU";
                lQR.StartDate = aRequest.StartDate.ToShortDateString();
                lQR.EndDate = aRequest.EndDate.ToShortDateString();
                lQR.When = DateTime.Now.ToShortDateString();
                lQR.Notes = aRequest.Email + " Requested a trip from " + aRequest.Departure + " to " + aRequest.Destination;
                List<UIContact> lLeads = new List<UIContact>();
                lQR.Contacts = lLeads;
                for (int i = 0; i < aRequest.NumberOfAdults; i++)
                    lLeads.Add(new UIContact() { First = aRequest.Email.Split('@')[0] + " " + i, Last = aRequest.Email.Split('@')[1], RelationshipID = i > 4 ? 4 : i + 1 });
                lLeads[0].PrimaryEmail = aRequest.Email;
                new QuoteRequestBusiness(mContext, Configuration).Save(lQR, new ContactBusiness(mContext).Get(lQR.AgentId) as Agent);
                string lMessage = "We have recieved a quote request from " + aRequest.Email + " for a trip from " + aRequest.Departure + " to " + aRequest.Destination;
                new CoreEmailHelper(Configuration).SendEmail(new List<string>() { "Sales@Eze2Travel.com" }, null, "New Website Sales Quote Request", lMessage);
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to save Web Quote Request", e);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet()]
        public IActionResult Leisure()
        {
            return View();
        }

        [HttpGet()]
        public IActionResult Corporate()
        {
            return View();
        }
        [HttpGet()]
        public IActionResult AboutUs()
        {
            return View();
        }

        [HttpGet()]
        public IActionResult ChooseYourPath()
        {
            return View();
        }

        [HttpGet()]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet()]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
