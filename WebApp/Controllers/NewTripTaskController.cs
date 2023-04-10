using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Business;
using BlitzerCore.Models;
using BlitzerCore.Utilities;
using BlitzerCore.Models.UI;
using BlitzerCore.Helpers;
using BlitzerCore.UIHelpers;

namespace WebApp.Controllers
{
    public class NewTripTaskController : BaseController
    {
        public NewTripTaskController(IDbContext aContext) : base(aContext)
        {
        }
        public IActionResult Index()
        {
            var lNewTaskManBiz = new NewTripTaskManager(mContext);
            Agent lAgent = GetCurrentAgent();
            var lTasks = lNewTaskManBiz.GetTaskTemplates(lAgent);
            var lUITasks = TaskTemplateUIHelper.Convert(lTasks);
            return View(lUITasks);
        }
    }
}
