using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlitzerCore.Models;
using BlitzerCore.Business;
using BlitzerCore.Utilities;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [ApiVersion("1.0")]
    public class ExperiencesController : ControllerBase
    {

        private readonly IDbContext _context;

        public ExperiencesController(IDbContext context)
        {
            _context = context;
        }

        // GET: api/Experiences/GetExperiencesAdStream/123123/5
        [HttpGet("{aUserId}/{aExpAdID}")]
        public ActionResult GetExperiencesAdStream(string aUserId, int aExpAdID)
        {
            Logger.LogInfo("User : " + aUserId + " Called GetExperiencesAdStream with AdID : " + aExpAdID);
            return Ok(new ExperiencesBusiness(_context).GetAdStream(aUserId, aExpAdID));
        }

        // GET: api/Experiences/123123
        //[HttpGet("{aExpUserId}", Name = "ExperiencesMainScreen")]
        //public Page GetExperiencesMainScreen(string aExpUserId)
        //{
        //    return new ExperiencesBusiness(_context).GetMainPage(aExpUserId);
        //}

        [HttpPost("{aUserId}/{aExpAdID}")]
        public IActionResult Click(string aUserId, int aExpAdID)
        {
            Logger.LogInfo("User : " + aUserId + " Called click on Experiance with AdID : " + aExpAdID);
            return Ok(new JsonModel(JsonType.Success, "Experience click completed"));
        }

        // POST: api/Experiences/Save
        [HttpPost("{aUserId}/{aExpAdID}")]
        public IActionResult Save(string aUserId, int aExpAdID)
        {
            Logger.LogInfo("User : " + aUserId + " Called Save on Experiance with AdID : " + aExpAdID);
            return Ok(new JsonModel(JsonType.Success, "Experience Save completed"));
        }

        // POST: api/Experiences
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Experiences/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
