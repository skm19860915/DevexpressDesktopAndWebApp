using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Business;
using BlitzerCore.DataAccess;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using WebApp.DataServices;

namespace WebApp.Controllers
{
    public class MexicoController : BaseController
    {
        public int CountryID { get; set; }

        public MexicoController(IDbContext aContext) : base(aContext)
        {
            CountryID = 2;
        }

        public IActionResult Index()
        {
            var lCountry = new DAPattern<Country>().Get(CountryID, mContext);
            if (lCountry.PageId == null)
                return null;
                
            var lPage = new CountryPageDataAccess(mContext).Get(lCountry.PageId.Value);
            lPage.Resorts = new ResortPageBusiness(mContext).GetByCountry(CountryID);
            return View(lPage);
        }

        public IActionResult Top_5_All_Inclusive_Resorts()
        {
            int id = 10;

            try
            {
                var lPage = new RankingPageBusiness(mContext).Get(id);
                return View(lPage);
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to retreieve ResortPage[" + id + "]", e);
                throw new InvalidOperationException();
            }
        }

        public IActionResult Top_5_Adults_Only()
        {
            int id = 15;

            try
            {
                var lPage = new RankingPageBusiness(mContext).Get(id);
                return View(lPage);
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to retreieve ResortPage[" + id + "]", e);
                throw new InvalidOperationException();
            }
        }

        public IActionResult Top_5_Luxury()
        {
            int id = 16;

            try
            {
                var lPage = new RankingPageBusiness(mContext).Get(id);
                return View(lPage);
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to retreieve ResortPage[" + id + "]", e);
                throw new InvalidOperationException();
            }
        }

    }
}
