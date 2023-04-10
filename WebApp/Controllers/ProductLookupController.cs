using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Business;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Models.ASP;
using BlitzerCore.Utilities;
using BlitzerCore.UIHelpers;
using WebApp.AspNetHelper;
using BlitzerCore.DataAccess;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Controllers
{
    public class ProductLookupController : BaseController
    {
        const string ClassName = "ProductLookupController::";

        public ProductLookupController(IDbContext context) : base(context)
        {
        }

        public JsonResult GetProductTypes(int aProviderId)
        {
            var FuncName = $"{ClassName}GetProductTypes(aHotelId={aProviderId})";

            List<SelectListItem> lOutput = new List<SelectListItem>();
            var lProvider = new CompanyBusiness(mContext).Get(aProviderId);
            if (lProvider as CruiseLine != null)
                return GetProductLookups(lProvider as CruiseLine);
            else if (lProvider as Hotel != null)
                return GetProductLookups(lProvider as Hotel);
            else if (lProvider as TourOperator != null)
                return GetProductLookups(lProvider as TourOperator);
            else if (lProvider as Company != null )
                return GetProductLookups (lProvider as Company);


            throw new Exception("Unsupport Product Line");
        }


        public JsonResult GetProductLookups(CruiseLine aCruiseLine)
        {
            var lData = new CruiseBusiness(mContext).GetCruises(aCruiseLine);
            var lOutput = ListHelper.GetProductTypes(lData.Cast<SKU>().ToList());
            lOutput.Insert(0, new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem() { Value = "0", Text = $"Select from {lOutput.Count} cruise types" });
            return ReturnProductTypes(lOutput);
        }

        public JsonResult GetProductLookups(Hotel aHotel)
        {
            var lData = new HotelBusiness(mContext).GetRoomTypes(aHotel).Cast<SKU>().ToList();
            var lOutput = ListHelper.GetProductTypes(lData);
            lOutput.Insert(0, new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem() { Value = "0", Text = $"Select from {lOutput.Count} room types" });
            return ReturnProductTypes(lOutput);
        }

        public JsonResult GetProductLookups(TourOperator aTourOperator)
        {
            var lData = new TourOperatorBusiness(mContext).GetTours(aTourOperator).Cast<SKU>().ToList();
            var lOutput = ListHelper.GetProductTypes(lData);
            lOutput.Insert(0, new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem() { Value = "0", Text = $"Select from {lOutput.Count} tour types" });
            return ReturnProductTypes(lOutput);
        }

        public JsonResult GetProductLookups(Company aAirLine)
        {
            var lData = new AirBusiness(mContext).GetProducts(aAirLine).Cast<SKU>().ToList();
            var lOutput = ListHelper.GetProductTypes(lData);
            lOutput.Insert(0, new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem() { Value = "0", Text = $"Select from {lOutput.Count} products" });
            return ReturnProductTypes(lOutput);
        }

        private JsonResult ReturnProductTypes(List<SelectListItem> aOutput)
        {
            var FuncName = $"{ClassName}ReturnProductTypes()";
            if (aOutput != null)
            {
                Logger.LogInfo($"{FuncName} Returning {aOutput.Count()} rows");
                return Json(aOutput);
            }
            else
                return Json(aOutput);

        }
    }
}
