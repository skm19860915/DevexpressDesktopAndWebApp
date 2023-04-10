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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.WebApi
{
    [Route("api/[controller]/[action]")]
    public class ApiContactController : ControllerBase
    {
        const string ClassName = "ApiContactController::";
        public IDbContext DbContext { get; set; }
        public ApiContactController(IDbContext context)
        {
            DbContext = context;
        }
        
        // GET: api/<ApiContactController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ApiContactController>/5
        [HttpGet]
        public ActionResult FindContacts(string aFName, string aMName, string aLName)
        {
            string FuncName = $"{ClassName}FindContacts ({aFName}, {aMName}, {aLName})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lData = new ContactDataAccess(DbContext).FindContacts(aFName, aMName, aLName);
                Logger.LogInfo($"Found {lData.Count} Contacts");
                return Ok(lData);
            } 
            catch (  Exception e )
            {
                Logger.LogException($"{FuncName} Failed to find contacts", e);
            } finally
            {
                Logger.LeaveFunction(FuncName);
            }

            return NotFound();
        }

        // POST api/<ApiContactController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ApiContactController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ApiContactController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
