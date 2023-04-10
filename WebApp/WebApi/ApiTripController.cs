using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http;
using BlitzerCore.Models;
using BlitzerCore.Utilities;
using BlitzerCore.Business;
using BlitzerCore.Models.UI;
using BlitzerCore.UIHelpers;
using BlitzerCore.DataAccess;
using WebApp.DataServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.WebApi
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApiTripController : ControllerBase
    {
        const string ClassName = "ApiTripController::";
        public IDbContext DbContext { get; set; }
        public ApiTripController(IDbContext context)
        {
            DbContext = context;
        }

        public ActionResult AddContact (int aTripId, string aContactId )
        {
            string FuncName = $"{ClassName}AddContact (TripId={aTripId}, ContactID={aContactId})";
            Logger.EnterFunction(FuncName);
            try
            {
                new TripBusiness(DbContext).AddContact(aTripId, aContactId);
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to add contacts", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }

            return NotFound();

        }

        public ActionResult RemoveContact(int aTripId, string aContactId)
        {
            string FuncName = $"{ClassName}RemoveContact (TripId={aTripId}, ContactID={aContactId})";
            Logger.EnterFunction(FuncName);
            try
            {
                new TripBusiness(DbContext).RemoveContact(aTripId, aContactId);
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to add contacts", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }

            return NotFound();

        }

        // GET api/<ApiTripController>/5
        [HttpGet]
        public ActionResult Get(int id)
        {
            string FuncName = $"{ClassName}Get (id={id})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lData = new TripBusiness(DbContext).Get(id);
                var lUIData = TripUIHelper.Convert(lData, true);
                if ( lData != null )
                    Logger.LogInfo($"Found {lData.Name} trip");
                else
                    Logger.LogInfo($"No Trip Found");
                return Ok(lUIData);
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to find contacts", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }

            return NotFound();
        }

        // POST api/<ApiTripController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ApiTripController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ApiTripController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
