using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlitzerCore.Models;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [ApiVersion("1.0")]

    public class MarketingAdsController : ControllerBase
    {
        private readonly IDbContext _context;

        public MarketingAdsController(IDbContext context)
        {
            _context = context;
        }

        // GET: api/MarketingAds
        [HttpGet]
        public ActionResult<IEnumerable<MarketingAd>> GetMarketingAd()
        {
            //return await _context.MarketingAd.ToListAsync();
            throw new NotImplementedException();
        }

        // GET: api/MarketingAds/5
        [HttpGet("{id}")]
        public MarketingAd GetMarketingAd(int id)
        {
            //var marketingAd = await _context.MarketingAd.FindAsync(id);

            //if (marketingAd == null)
            //{
            //    return NotFound();
            //}

            //return marketingAd;
            throw new NotImplementedException();
        }

        // PUT: api/MarketingAds/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public IActionResult PutMarketingAd(int id, MarketingAd marketingAd)
        {
            //if (id != marketingAd.AdID)
            //{
            //    return BadRequest();
            //}

            //_context.Entry(marketingAd).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!MarketingAdExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return NoContent();
            throw new NotImplementedException();
        }

        // POST: api/MarketingAds
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public ActionResult<MarketingAd> PostMarketingAd(MarketingAd marketingAd)
        {
            //_context.MarketingAd.Add(marketingAd);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetMarketingAd", new { id = marketingAd.AdID }, marketingAd);
            throw new NotImplementedException();
        }

        // DELETE: api/MarketingAds/5
        [HttpDelete("{id}")]
        public ActionResult<MarketingAd> DeleteMarketingAd(int id)
        {
            //var marketingAd = await _context.MarketingAd.FindAsync(id);
            //if (marketingAd == null)
            //{
            //    return NotFound();
            //}

            //_context.MarketingAd.Remove(marketingAd);
            //await _context.SaveChangesAsync();

            //return marketingAd;
            throw new NotImplementedException();
        }

        private bool MarketingAdExists(int id)
        {
            //return _context.MarketingAd.Any(e => e.AdID == id);
            throw new NotImplementedException();
        }
    }
}
