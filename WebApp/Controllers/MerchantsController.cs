using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlitzerCore.Models;
using BlitzerCore.Business;
using BlitzerCore.Utilities;
using BlitzerCore.Helpers;
using System.Net.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [ApiVersion("1.0")]
    public class MerchantsController : ControllerBase
    {
        private readonly IDbContext _context;
        private readonly IConfiguration _config;
        public readonly IWebHostEnvironment _environment;

        public MerchantsController(IDbContext context, IWebHostEnvironment environment, IConfiguration aConfig)
        {
            _context = context;
            _config = aConfig;
            _environment = environment;
        }

        // GET: api/Merchants
        [HttpGet]
        public ActionResult<IEnumerable<Merchant>> GetMerchant()
        {
            //return await _context.Merchant.ToListAsync();
            throw new NotImplementedException();
        }

        [HttpGet]
        public List<Blob> GetExperianceAdBlobs (int aMerchantID, int aAdType )
        {
            var lMerBiz = new MerchantBusiness(_context, _config);
            return lMerBiz.GetBlobs(aMerchantID, (Ad.AdTypes)aAdType);
        }

        ////[HttpPost]
        ////public Ad UpdateAd ( string aUserID, Ad aAd)
        ////{
        ////    //return new Layer.Business.MerchantBusiness(_context).AddAd(aAd);
        ////    throw new NotImplementedException();
        ////}
        [HttpPost]
        public ActionResult UploadExperienceBlob(int aMerchantID, [FromForm] MerchantBlob aBlob)
        {
            Logger.LogInfo("Merchant[" + aMerchantID + "] called UploadExperienceBlob for slot[" + aBlob.Slot + "]");
            string lContentPath = AppHelper.GetTempPath(_environment.ContentRootPath);

            new MerchantBusiness(_context, _config).SaveExperienceBlob(aMerchantID, aBlob, lContentPath);
            return Ok();
        }
        [HttpPost]
        public ActionResult UploadServieBlob(int aUserID, [FromForm] MerchantBlob aBlob)
        {
            Logger.LogInfo("Merchant["+aUserID+"] called UploadServiceBlob for slot["+aBlob.Slot+"]");
            try
            {
                string lContentPath = AppHelper.GetTempPath(_environment.ContentRootPath);
                new MerchantBusiness(_context, _config).SaveServiceBlob(aUserID, aBlob, lContentPath);
                return Ok();
            } catch ( Exception e)
            {
                Logger.LogException("Failed to upload Blob:", e);
                return Problem(e.Message);
            }
        }

        // GET: api/Merchants/5
        [HttpGet("{id}")]
        public ActionResult<Merchant> Get(int id)
        {
            return new MerchantBusiness(_context, _config).Get(id);
        }

        // GET: api/Merchants/5
        [HttpGet("{id}")]
        public List<int> GetServices(int id)
        {
            return new MerchantBusiness(_context, _config).GetServices(id);
        }

        // GET: api/Merchants/5
        [HttpGet("{aServiceID}")]
        public List<Merchant> GetBySupportedServices(int aServiceID)
        {
            return new MerchantBusiness(_context, _config).GetBySupportedServices(aServiceID);
        }

        // POST: api/Merchants
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public Merchant Save(Merchant aMerchant)
        {
            if (aMerchant == null)
                return null;

            aMerchant.Id = aMerchant.Id;
            return new MerchantBusiness(_context, _config).Save(aMerchant);
        }

        [HttpPost]
        public ActionResult AddService(int aMerchantID, int aServiceID)
        {
            Logger.LogInfo("MerchantController.AddService called with MerchantID = " + aMerchantID + " Service ID = " + aServiceID);

            try
            {
                if (new MerchantBusiness(_context, _config).AddService(aMerchantID, aServiceID) == true)
                    return Ok();

                return new StatusCodeResult(500);
            } catch ( Exception e )
            {
                Logger.LogException("Failed to Add Server for merchant=" + aMerchantID, e);
                return new StatusCodeResult(500);
            }
        }
        ////// DELETE: api/Merchants/5
        ////[HttpDelete("{id}")]
        ////public Merchant DeleteMerchant(string id)
        ////{
        ////    //var merchant = await _context.Merchant.FindAsync(id);
        ////    //if (merchant == null)
        ////    //{
        ////    //    return NotFound();
        ////    //}

        ////    //_context.Merchant.Remove(merchant);
        ////    //await _context.SaveChangesAsync();

        ////    //return merchant;
        ////    throw new NotImplementedException();
        ////}

        private bool MerchantExists(string id)
        {
            return new MerchantBusiness(_context, _config).Exists(id);
        }
    }
}
