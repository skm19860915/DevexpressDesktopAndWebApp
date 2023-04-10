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
        public class CountryLookupController : Controller
        {
            private RepositoryContext _context;

            public CountryLookupController(RepositoryContext context)
            {
                _context = context;
            }

            [HttpGet]
            public object Get(DataSourceLoadOptions loadOptions)
            {
                return DataSourceLoader.Load(new CountryDataAccess(_context).GetAll(), loadOptions);
            }

        [HttpPost]
        public IActionResult Post(string values)
        {
            var lCountry = new Country();
            JsonConvert.PopulateObject(values, lCountry);

            if (!TryValidateModel(lCountry))
                return BadRequest("Error");

            new CountryDataAccess(_context).Save(lCountry);

            return Ok();
        }

        [HttpPut]
        public IActionResult Put(int key, string values)
        {
            var lCountry = new CountryDataAccess(_context).Get(key);
            JsonConvert.PopulateObject(values, lCountry);

            if (!TryValidateModel(lCountry))
                return BadRequest("Error");

            new CountryDataAccess(_context).Save(lCountry);

            return Ok();
        }

    }
}
