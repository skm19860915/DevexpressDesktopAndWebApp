using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BlitzerCore.Models;
using BlitzerCore.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using BlitzerCore.Utilities;
using BlitzerCore.UIHelpers;
using WebApp.DataServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    //private readonly IDbContext mContext;

    [ApiController]
    [Route("api/[controller]/[action]")]
    [ApiVersion("1.0")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<BlitzerUser> _UserManager;
        private readonly IDbContext mContext;
        public UserController(UserManager<BlitzerUser> aUserManager, IDbContext aDBContext)
        {
            _UserManager = aUserManager;
            mContext = aDBContext;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<BlitzerUser> Get()
        {
            throw new NotImplementedException();
        }

        // GET api/<controller>/5
        [HttpGet("{aUserID}")]
        public ActionResult GetSavedAds(string aUserID)
        {
            return Ok(new AdBusiness(mContext).Get(aUserID));
        }

        [HttpGet("{aUserId}/{aLastViewAdID}")]
        public ActionResult GetSavedAdStream(string aUserId, int aLastViewAdID)
        {
            Logger.LogInfo("User : " + aUserId + " Called UserControllerGetSavedAdStream with AdID : " + aLastViewAdID);
            return Ok(new AdBusiness(mContext).GetAdStream(aUserId, aLastViewAdID));
        }

        // GET api/<controller>/5
        [HttpGet("{aUserID}/{aAdID}")]
        public ActionResult DeleteSavedAd(string aUserID, int aAdID)
        {
            try
            {
                new AdBusiness(mContext).Delete(aUserID, aAdID);
                return Ok();
            } catch ( Exception e )
            {
                return BadRequest(e.Message);
            }

        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public BlitzerCore.Models.UI.UIContact Get(string id)
        {
            Logger.LogInfo("UserController::Get called with : " + id);

            var lSecUser = new ContactBusiness(mContext).Get(id);
            if (lSecUser == null)
            {
                return null;
            }

            Logger.LogInfo("UserController::Get return FirstName of  : " + lSecUser.First);

            return ContactUIHelper.Convert(lSecUser);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
