using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BlitzerCore.Business;
using BlitzerCore.Models;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [ApiVersion("1.0")]
    public class LocationController : ControllerBase
    {
        private readonly IDbContext mContext;
        
        public LocationController(IDbContext aDBContext)
        {
            mContext = aDBContext;
        }

        // GET: api/Location
        [HttpGet]
        public IEnumerable<Location> GetAll()
        {
            return new LocationBusiness(mContext).GetAll();
        }

        // GET: api/Location/5
        [HttpGet("{id}", Name = "Get")]
        public Location Get(int id)
        {
            return new LocationBusiness(mContext).Get(id);
        }

        // PUT: api/Location/5
        [HttpPut("{id}")]
        public ActionResult Put(int aParentID, [FromBody] Location aNewLocation)
        {
            //return Ok(new LocationBusiness(mContext).Add(aParentID, aNewLocation));
            throw new NotImplementedException();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
