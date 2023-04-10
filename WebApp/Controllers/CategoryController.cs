using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Utilities;

namespace WebApp.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                return View();
            } catch ( Exception e )
            {
                Logger.LogException("Failed to execute category index", e);
                throw new InvalidOperationException(e.Message);
            }
        }
    }
}
