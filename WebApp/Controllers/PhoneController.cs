using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlitzerCore.Models;
using BlitzerCore.Business;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [ApiVersion("1.0")]

    public class PhoneController : ControllerBase
    {
        private readonly IDbContext _context;

        public PhoneController(IDbContext context)
        {
            _context = context;
        }


        [HttpGet("{aDesCriteria}")]
        public List<Ad> GetSearchDestinations(string aDesCriteria)
        {
            return new DestinationBusiness(_context).GetDestinationsStream(aDesCriteria);
        }

        //// GET: api/Phone/5
        //[HttpGet("{id}")]
        //public Phone GetPhone(int id)
        //{
        //    //var phone = await _context.Phone.FindAsync(id);

        //    //if (phone == null)
        //    //{
        //    //    return NotFound();
        //    //}

        //    //return phone;
        //    throw new NotImplementedException();

        //   // return NotFound();
        //}
    }
}
