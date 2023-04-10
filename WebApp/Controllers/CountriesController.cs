using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlitzerCore.Models;
using BlitzerCore.Utilities;
using BlitzerCore.Business;
using BlitzerCore.Models.UI;
using BlitzerCore.DataAccess;

namespace WebApp.Controllers
{
    public class CountriesController : BaseController
    {
        public CountriesController(IDbContext context) : base ( context)
        {
        }

        public ActionResult Index()
        {
            var lCountryies = new CountryPageDataAccess(mContext).GetAll();
            return View(lCountryies);
        }
    }
}
