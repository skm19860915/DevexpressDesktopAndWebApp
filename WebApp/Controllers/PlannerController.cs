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
    public class PlannerController : BaseController
    {
        public const int NewsPageSize = 4;
        const string ClassName = "SystemController::";

        public PlannerController(IDbContext context, UserManager<BlitzerUser> userManager) : base(context, userManager)
        {
        }


        public async System.Threading.Tasks.Task<IActionResult> Execute()
        {
            await SecuritySetup();
            return View();
        }

    }
}
