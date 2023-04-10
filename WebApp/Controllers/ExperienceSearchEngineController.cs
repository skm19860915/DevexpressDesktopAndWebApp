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
    public class ExperienceSearchEngineController : ControllerBase
    {
        private readonly IDbContext _context;

        public ExperienceSearchEngineController(IDbContext context)
        {
            _context = context;
        }

        // GET: api/ExperienceSearchEngine/Washington
        [HttpGet("{aLocation}")]
        public List<Ad> Get(string aLocation)
        {
            return new ExperiencesBusiness(_context).GetExperiencesStream(aLocation);
        }
    }
}
