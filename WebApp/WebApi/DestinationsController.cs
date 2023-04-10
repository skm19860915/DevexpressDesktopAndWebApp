using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlitzerCore.Models;
using BlitzerCore.Utilities;
using BlitzerCore.Business;
using BlitzerCore.Models.UI;
using BlitzerCore.DataAccess;
using WebApp.DataServices;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ApiDestinationsController : Controller
    {
        public IDbContext DbContext { get; set; }
        public ApiDestinationsController(IDbContext context) 
        {
            DbContext = context;
        }


        [HttpGet("{aUserId}/{aDesAdID}")]
        public ActionResult GetDestinationsAdStream(string aUserId, int aDesAdID)
        {
            Logger.LogInfo("User : " + aUserId + " Called GetDestinationsAdStream with AdID : " + aDesAdID);
            return Ok(new DestinationBusiness(DbContext).GetAdStream(aUserId, aDesAdID));
        }

        //[HttpGet("{aDesUserId:alpha}")]
        //public Page GetDestinationsMainScreen(string aDesUserId)
        //{
        //    return new DestinationBusiness(_context).GetMainPage(aDesUserId);
        //}

        // POST: api/Destinations/Click
        [HttpPost("{aUserId}/{aDesAdID}")]
        public IActionResult Click(string aUserId, int aDesAdID)
        {
            Logger.LogInfo("User : " + aUserId + " Called DestinationClick with ID = " + aDesAdID);
            return Ok(new JsonModel(JsonType.Success, "AD click completed"));
        }

        // POST: api/Destinations/Save
        [HttpPost("{aUserId}/{aDesAdID}")]
        public IActionResult Save(string aUserId, int aDesAdID)
        {
            Logger.LogInfo("User : " + aUserId + " Called Save with ID = " + aDesAdID);
            return Ok(new AdBusiness(DbContext).Save(aUserId, aDesAdID));
        }

        // PUT: api/Destinations/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }


    }
}
