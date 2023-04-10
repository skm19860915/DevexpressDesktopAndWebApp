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

namespace WebApp.WebApi
{
        [Route("api/[controller]/[action]")]
        public class AirPortController : Controller
        {
            private RepositoryContext _context;

            public AirPortController(RepositoryContext context)
            {
                _context = context;
            }

            [HttpGet]
            public object Get(DataSourceLoadOptions loadOptions)
            {
                return DataSourceLoader.Load(new AirPortDataAccess(_context).GetAll(), loadOptions);
            }

        [HttpPost]
        public IActionResult Post(string values)
        {
            var lAirPort = new AirPort();
            JsonConvert.PopulateObject(values, lAirPort);

            if (!TryValidateModel(lAirPort))
                return BadRequest("Error");

            new AirPortDataAccess(_context).Save(lAirPort);

            return Ok();
        }

        [HttpPut]
        public IActionResult Put(int key, string values)
        {
            var lAirport = new AirPortDataAccess(_context).Get(key);
            JsonConvert.PopulateObject(values, lAirport);

            if (!TryValidateModel(lAirport))
                return BadRequest("Error");

            new AirPortDataAccess(_context).Save(lAirport);

            return Ok();
        }
    }
}
