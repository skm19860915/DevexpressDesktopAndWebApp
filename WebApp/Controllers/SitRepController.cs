using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Business;
using BlitzerCore.Models;
using Microsoft.Extensions.Configuration;

namespace WebApp.Controllers
{
    public class SitRepController : BaseController
    {
        public IConfiguration Configuration { get; }

        public SitRepController(IDbContext context, IConfiguration aConfiguration) : base(context)
        {
            Configuration = aConfiguration;
        }

        // GET: SitRepController
        public ActionResult Index()
        {
            return View(new WarRoomBusiness(DbContext, Configuration).Populate(GetCurrentAgent()));
        }

        // GET: SitRepController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SitRepController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SitRepController/Create
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

        // GET: SitRepController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SitRepController/Edit/5
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

        // GET: SitRepController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SitRepController/Delete/5
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
