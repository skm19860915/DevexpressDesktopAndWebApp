using BlitzerCore.Business;
using BlitzerCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.WebApi
{
    public class PresentationController : Controller
    {
        private IDbContext mContext;
        public PresentationController(IDbContext mContext)
        {
            this.mContext = mContext;
        }

        [Route("api/presentation/{guid?}")]
        public ContentResult Index(string guid = null)
        {
            var lPresenationItem = new PresentationBusiness(mContext, null).GetPresentationItem(guid);
            if (lPresenationItem == null)
                return Content($"Presentation : {{ Name : \"\", GUID :  \"\", Url : \"https://info.eze2travel.com\" }}");

            var lDataString = $"Presentation : {{ Name : \"{lPresenationItem.Presentation.ClientName}\", GUID : \"{lPresenationItem.Presentation.Guid}\", Url : \"{lPresenationItem.WebPage.Url}\" }}";
            return Content(lDataString);
        }
    }
}
