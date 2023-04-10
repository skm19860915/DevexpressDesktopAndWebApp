using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BlitzerCore.Business;
using BlitzerCore.DataAccess;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using Microsoft.AspNetCore.Authorization;
using WebApp.DataServices;

namespace WebApp.Controllers
{
    public class PageController : BaseController
    {
        PageDataAccess DataAccess { get; set; }
        public PageController(IDbContext aContext) : base (aContext)
        {
            DataAccess = new PageDataAccess(aContext);
        }
        // GET: PageController
        [Authorize]
        public ActionResult Index()
        {
            
            var lPage = DataAccess.GetPage(1);
            return View(lPage);
        }

        public ActionResult AddBlock(int id)
        {
            var lPage = new DAPattern<Page>().Get(id, mContext);
            var lBlock = new Block() { Title = lPage.Title + " Block", BlockTitle = lPage.Title + " Needs Attention" };
            new DAPattern<Block>().Save(lBlock, mContext);
            var lPToB = new PageToBlockMap() { BlockId = lBlock.Id };
            new DAPattern<PageToBlockMap>().Save(lPToB, mContext);
            if (lPage.Blocks == null)
                lPage.Blocks = new List<PageToBlockMap>();

            lPage.Blocks.Add(lPToB);
            new DAPattern<Page>().Save(lPage, lPage.Id, mContext);
            switch (lPage.PageTypeId)
            {
                case 1:
                    return RedirectToAction(nameof(ResortPageController.Edit), "Resort", new { id = id });
                default:
                    return RedirectToAction(nameof(CountryController.Edit), "Country", new { id = id });
            }
        }

        /// <summary>
        /// Display 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Redirect(int id)
        {
            return RedirectToAction(nameof(ResortPageController.View), "Resort", new { id = id });
        }


        // GET: PageController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PageController/Create
        public ActionResult Create()
        {
            var lSlug = new Page();
            lSlug.PageTypes = new PageTypesBusiness(mContext).PageTypes();
            return View(lSlug);
        }

        // POST: PageController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] Page aPage)
        {
            try
            {
                switch (aPage.PageTypeId)
                {
                    case 1:
                        var lHotel = new Hotel() { Name = "New Hotel" };
                        var lResortPage = new ResortPageBusiness(mContext).CreateNewPage(lHotel, GetCurrentAgent());
                        return RedirectToAction(nameof(ResortPageController.Edit), "Resort", new { id = lResortPage.Id });
                    default:
                        var lCountryPage = new CountryPageBusiness(mContext).CreateNewPage(aPage.Title, GetCurrentAgent().Id);
                        return RedirectToAction(nameof(CountryController.Edit), "Country", new { id = lCountryPage.Id });
                }
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to create Page", e);
                return View(aPage);
            }
        }

        // GET: PageController/Edit/5
        public ActionResult Edit(int id)
        {
            return RedirectToAction(nameof(ResortPageController.Edit), "Resort", new { id = id });
        }

        // POST: PageController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] Page aPage)
        {
            try
            {
                throw new NotImplementedException();
                //DataAccess.Save(aPage);
                //return Redirect(Request.Headers["Referer"].ToString());
            }
            catch
            {
                return View();
            }
        }

        // GET: PageController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PageController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, [FromForm] Page aPage)
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
