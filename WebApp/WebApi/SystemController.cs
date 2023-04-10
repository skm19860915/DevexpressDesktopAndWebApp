using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models;
using BlitzerCore.DataAccess;
using WebApp.DataServices;
using Newtonsoft.Json;
using BlitzerCore.Business;

namespace WebApp.WebApi
{
    [Route("api/[controller]/[action]")]
    public class SystemController : Controller
    {
        private IDbContext _context;

        public SystemController(IDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult CreatePrintDocs()
        {
            BlitzerCore.Business.TripBusiness lTripBiz = new TripBusiness(_context);
            var lTrips = lTripBiz.GetActiveTrips();
            foreach (var lTrip in lTrips)
                lTripBiz.CreatePrintDocsTask(lTrip);
            return Ok();
        }

        [HttpGet]
        public IActionResult RegisterBooking()
        {
            BlitzerCore.Business.TripBusiness lTripBiz = new TripBusiness(_context);
            var lTrips = lTripBiz.GetActiveTrips();
            foreach (var lTrip in lTrips)
            {
                lTripBiz.RegisterBookings(lTrip);
            }

            return Ok();
        }
    }
}
