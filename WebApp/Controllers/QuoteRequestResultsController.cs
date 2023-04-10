using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class QuoteRequestResultsController : Controller
    {
        // GET: QuoteRequestResultsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: QuoteRequestResultsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: QuoteRequestResultsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuoteRequestResultsController/Create
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

        // GET: QuoteRequestResultsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: QuoteRequestResultsController/Edit/5
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

        // GET: QuoteRequestResultsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: QuoteRequestResultsController/Delete/5
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
