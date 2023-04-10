using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.DataAccess;

namespace WebApp.Controllers
{
    public class ContentController : BaseController
    {
        ContentDataAccess DataAccess { get; set; }
        public ContentController(IDbContext aContext) : base ( aContext)
        {
            DataAccess = new ContentDataAccess(aContext);
        }
        // GET: ContentController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ContentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ContentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContentController/Create
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

        // GET: ContentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(DataAccess.Get(id));
        }

        // POST: ContentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] Content aContent)
        {
            try
            {
                DataAccess.Save(aContent);
                Page lPage = DataAccess.GetParent(aContent);
                var lPDA = new PageDataAccess(mContext);
                lPage.HeaderImage.Media.Size1600x1200.Location = aContent.Photo.Location;
                lPDA.Save(lPage);

                switch (lPage.PageTypeId)
                {
                    case 1:
                        return RedirectToAction(nameof(ResortPageController.Edit), "ResortPage", new { id = lPage.Id });
                    case 2:
                        return RedirectToAction(nameof(CountryController.Edit), "Country", new { id = lPage.Id });
                    default:
                        return RedirectToAction(nameof(ResortPageController.Edit), "ResortPage", new { id = lPage.Id });
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: ContentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ContentController/Delete/5
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
