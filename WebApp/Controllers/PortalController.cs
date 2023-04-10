using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using BlitzerCore.Models;
using BlitzerCore.Business;
using BlitzerCore.Utilities;
using Microsoft.AspNetCore.Authorization;
using WebApp.DataServices;

namespace WebApp.Controllers
{
    [Authorize]
    public class PortalController : BaseController
    {
        const string ClassName = "PortalController::";
        public PortalController(IDbContext context, UserManager<BlitzerUser> userManager) : base (context, userManager)
        {
        }
        public async Task<IActionResult> Index()
        {
            return await GetData(ViewModes.MyTasksOnly);
        }

        public async Task<IActionResult> ViewAll()
        {
            return await GetData(ViewModes.AllTasks);
        }

        protected async Task<IActionResult> GetData(ViewModes aMode)
        {
            string FuncName = $"{ClassName}Index";
            var lUser = GetCurrentUser() as Agent;
            if (lUser == null)
                return RedirectToAction("Login", "Account");
            lUser.ViewMode = aMode;
            var lViewModel = new PortalBusiness(mContext).GetPortalData(lUser);
            await SecuritySetup();
            lViewModel.IsAdmin = ViewBag.IsAdmin;
            lViewModel.IsClient = UserRole == AppRoles.Client;
            return View("Index", lViewModel);
        }
        public IActionResult Search([FromForm] PortalData aPortal)
        {
            return View(new PortalBusiness(DbContext).Search(aPortal.SearchText, GetCurrentAgent()));
        }
    }
}