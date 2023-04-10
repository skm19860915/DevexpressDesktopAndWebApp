using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlitzerCore.Models;
using BlitzerCore.Business;


namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [ApiVersion("1.0")]
    public class UserLocationPreferencesController : ControllerBase
    {
        private readonly IDbContext mContext;

        public UserLocationPreferencesController(IDbContext aDBContext)
        {
            mContext = aDBContext;
        }

        // GET: api/UserLocationPreferences/5
        [HttpGet]
        public ActionResult Get(string aUserid)
        {
            return Ok(new UserLocationPreferencesBusiness(mContext).Get(aUserid).Select(x => x.UserPreference));

        }

        // PUT: api/UserLocationPreferences/5
        [HttpPost]
        public ActionResult Store(string aUserid, [FromBody] List<int> aLocations)
        {
            new UserLocationPreferencesBusiness(mContext).Save(aUserid, aLocations);
            return Ok();
        }
    }
}
