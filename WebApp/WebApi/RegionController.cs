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
        public class RegionController : Controller
        {
            private IDbContext _context;

            public RegionController(IDbContext context)
            {
                _context = context;
            }

            [HttpGet]
            public object Get(DataSourceLoadOptions loadOptions)
            {
            //IDbContext mContext = null;
            return DataSourceLoader.Load(_context.Regions, loadOptions);
            }

    }
}
