using System;
using BlitzerCore.DataAccess;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlitzerCore.Utilities;
using BlitzerCore.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using WebApp.DataServices;

namespace WebApp.Controllers
{

    [Authorize]
    public class MediaController : BaseController
    {

        private readonly IConfiguration mConfig;
        MediaDataAccess DataAccess { get; set; }
        IWebHostEnvironment env;

        public MediaController(IDbContext aContext, IWebHostEnvironment aEnv, IConfiguration aConfig) : base(aContext)
        {
            DataAccess = new MediaDataAccess(aContext);
            env = aEnv;
            mConfig = aConfig;
        }


        // GET: MediaController
        public ActionResult Index()
        {
            var lResults = new MediaDataAccess(mContext).GetAll();
            return View(lResults);
        }

        // GET: MediaController/Details/5
        public ActionResult Details(int id)
        {
            var lMedia = new DAPattern<Media>().Get(id, GetContext());
            return View(lMedia);
        }

        // GET: MediaController/Create
        /// <summary>
        /// Create a new Graphic Item for a the parent Media
        /// </summary>
        /// <param name="id">Parent BlockID</param>
        /// <returns></returns>
        public ActionResult Create(int id)
        {
            var lParentBlock = new DAPattern<Block>().Get(id, (RepositoryContext)mContext);
            var lMedia = new Media() { };
            lParentBlock.Media = lMedia;
            new DAPattern<Block>().Save(lParentBlock, id, mContext);
            return RedirectToAction(nameof(Edit), new { id = lMedia.Id });
        }

        // POST: MediaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] Media aMedia)
        {
            try
            {
                new DAPattern<Media>().Save(aMedia, aMedia.Id, GetContext());
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MediaController/Edit/5
        public ActionResult Edit(int id)
        {
            var lMedia = DataAccess.Get(id);
            if (lMedia == null)
                lMedia = new Media();
            return View(lMedia);
        }

        // POST: MediaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] Media aMedia)
        {
            Page lParent = null;
            try
            {
                int lCnt = new BlitzerCore.Business.MediaBusiness(mContext, mConfig).Save(aMedia);
                lParent = DataAccess.GetParent(aMedia);
                if (lParent != null)
                    return RedirectToAction(nameof(ResortPageController.Edit), "Resort", new { id = lParent.Id });
                else
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to Save Media", e);
            }

            return RedirectToAction(nameof(Edit), new { id = aMedia.Id });
        }

        // GET: MediaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MediaController/Delete/5
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
