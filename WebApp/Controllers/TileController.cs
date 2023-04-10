using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.DataAccess;
using BlitzerCore.Models.UI;
using BlitzerCore.Models;
using WebApp.DataServices;
using BlitzerCore.Business;
using BlitzerCore.Utilities;

namespace WebApp.Controllers
{
    public class TileController : BaseController
    {
        public TileController(IDbContext aContext) : base(aContext)
        {
        }

        // GET: TileController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TileController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TileController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TileController/Create
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

        // GET: TileController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(new TileDataAccess(mContext).Get(id));
        }

        // POST: TileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] Tile aTile)
        {
            try
            {
                int lCnt = new TileBusiness(mContext).Save(aTile);
                return Ok();
            }
            catch ( Exception e )
            {
                Logger.LogException("Failed to Save Tile", e);
            }

            return Ok();
        }

        // GET: TileController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TileController/Delete/5
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
