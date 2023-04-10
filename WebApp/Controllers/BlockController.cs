using System;
using BlitzerCore.DataAccess;
using BlitzerCore.Business;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlitzerCore.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using WebApp.DataServices;

namespace WebApp.Controllers
{
    [Authorize]
    public class BlockController : BaseController
    {
        private readonly IConfiguration mConfig;
        MediaDataAccess DataAccess { get; set; }
        IWebHostEnvironment env;

        public BlockController(IDbContext aContext, IWebHostEnvironment aEnv, IConfiguration aConfig) : base(aContext)
        {
            DataAccess = new MediaDataAccess(aContext);
            env = aEnv;
            mConfig = aConfig;
        }

        // GET: BlockController
        public ActionResult Index()
        {
            return View(new BlockDataAccess(DbContext).GetAll());
        }

        // GET: BlockController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BlockController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlockController/Create
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

        // GET: BlockController/Edit/5
        public ActionResult Edit(int id)
        {
            var lBlock =  new BlockDataAccess(mContext).Get(id);
            if (lBlock == null)
                lBlock = new Block();
            return View(lBlock);
        }

        // POST: BlockController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] Block aBlock)
        {
            //Page lParent = null;
            try
            {
                //var lBlock = new DAPattern<Block>().Get(aBlock.Id, ((Blitzer.DataServices.RepositoryContext)mContext));
                //if (aBlock.PageMap != null && aBlock.PageMap.Id == 0)
                //    aBlock.PageMap.BlockId = aBlock.Id;
                new BlockBusiness(mContext).Save(aBlock);
                //var lBlock = new DAPattern<Block>().Save(aBlock, aBlock.Id, (RepositoryContext)mContext);
                //if (lMedia != null)
                //    lMedia.Copy(aBlock);
                //else
                //    lMedia = aBlock;
                //int lCnt = new BlitzerCore.Business.BlockBusiness(mContext, mConfig).Save(aBlock, env);
                //lParent = DataAccess.GetParent(aBlock);
                //if (lParent != null)
                //    return RedirectToAction(nameof(ResortController.Edit), "Resort", new { id = lParent.Id });
                //else
                //    return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to Save Block", e);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: BlockController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BlockController/Delete/5
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
