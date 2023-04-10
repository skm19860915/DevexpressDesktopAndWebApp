using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models;
using BlitzerCore.Utilities;
using BlitzerCore.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [ApiVersion("1.0")]
    public class ServicesController : ControllerBase
    {
        private readonly IDbContext mContext;

        public ServicesController(IDbContext aContext)
        {
            mContext = aContext;
        }
        // GET: api/Service/5
        [HttpGet]
        public List<Service> Get(int? aParentID)
        {
            return new ServiceBusiness(mContext).Get(aParentID);
        }

        [HttpGet]
        public ActionResult GetServicesAdStream(string aUserId, int aAdID)
        {
            Logger.LogInfo("User : " + aUserId + " Called GetDestinationsAdStream with AdID : " + aAdID);
            return Ok(new ServiceBusiness(mContext).GetAdStream(aUserId, aAdID));
        }
        // GET: api/Service/5
        [HttpGet]
        public IEnumerable<Service> GetAll()
        {
            return new ServiceBusiness(mContext).GetAll();
        }

        // POST: api/Service
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Service/5
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
