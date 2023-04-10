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
    [Route("api/DestinationsSearchEngine")]
    [ApiVersion("1.0")]
    public class DestinationsSearchEngineController : ControllerBase
    {
        private readonly IDbContext _context;

        public DestinationsSearchEngineController(IDbContext context)
        {
            _context = context;
        }

        // GET: api/DestinationsSearchEngineController/Washington
        [HttpGet("{aLocation}")]
        public List<Ad> Get(string aLocation)
        {
            return new DestinationBusiness(_context).GetDestinationsStream(aLocation);
        }
    }
}