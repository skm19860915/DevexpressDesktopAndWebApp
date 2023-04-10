using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Business;
using BlitzerCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace WebApp.Controllers
{
    [Authorize]
    public class FinancialController : BaseController
    {
        public IConfiguration Configuration { get; }
        public FinancialController(IDbContext context, IConfiguration aConfiguration) : base(context)
        {
            Configuration = aConfiguration;
        }


        // GET: FinancialController
        public ActionResult Index()
        {

            return View(new FinancialBusiness(DbContext, Configuration).Get(GetCurrentAgent()));
        }

        // GET: FinancialController
        public ActionResult FinalPayments()
        {

            return View(new FinancialBusiness(DbContext, Configuration).GetFinalPayments(GetCurrentAgent()));
        }

        // GET: FinancialController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FinancialController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FinancialController/Create
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

        // GET: FinancialController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FinancialController/Edit/5
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

        // GET: FinancialController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FinancialController/Delete/5
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
