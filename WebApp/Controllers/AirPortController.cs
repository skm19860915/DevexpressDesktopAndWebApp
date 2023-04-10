using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;
using BlitzerCore.Business;
using BlitzerCore.DataAccess;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize]
    public class AirPortController : BaseController
    {
        public AirPortController(IDbContext aContext) : base(aContext)
        {

        }
        // GET: CountryController
        public ActionResult Index()
        {
            return View(new AirPortDataAccess(mContext).GetAll().ToList());
        }
    }
}
