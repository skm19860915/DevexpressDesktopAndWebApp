using System;
using BlitzerCore.DataAccess;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlitzerCore.Business;

namespace WebApp.Controllers
{
    public class ResortPicGalleryController : BaseController
    {
        public ResortPicGalleryController(IDbContext aContext) : base(aContext)
        {
        }
        
        // GET: ResortPicGalleryController
        public ActionResult Index(int ResortID, int CategoryID)
        {
            return View(new ResortPageBusiness(mContext).GetIndexPage(ResortID, CategoryID));
        }
    }
}
