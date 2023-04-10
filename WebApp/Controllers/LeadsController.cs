using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlitzerCore.Models;
using WebApp.DataServices;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    [ApiVersion("1.0")]

    public class LeadsController : ControllerBase
    {
        private readonly IDbContext _context;

        public LeadsController(IDbContext context)
        {
            _context = context;
        }

        // GET: api/Leads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetLeads()
        {
            return await ((RepositoryContext)_context).Leads.ToListAsync();
        }

        // GET: api/Leads/5
        [HttpGet("{UserId}")]
        public ActionResult<Client> GetLead(string UserId)
        {
            var lead = ((RepositoryContext)_context).Leads.Where(x => x.Id == UserId).FirstOrDefault();

            if (lead == null)
            {
                return NotFound();
            }

            return lead;
        }

        private string GetErrorStringFromModelState(ModelStateDictionary dictionary)
        {
            var error = dictionary.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage).ToList();
            if (error != null)
                return string.Join(",", error);

            return string.Empty;
        }

        // POST: api/Leads
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPost]
        //public async Task<ActionResult<LeadDB>> PostLead(Lead lead)
        //{
        //    ((RepositoryContext)_context).Leads.Add(lead);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetLead", new { UserId = lead.Id }, lead);
        //}

        // DELETE: api/Leads/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Client>> DeleteLead(int id)
        {
            var lead = await ((RepositoryContext)_context).Leads.FindAsync(id);
            if (lead == null)
            {
                return NotFound();
            }

            ((RepositoryContext)_context).Leads.Remove(lead);
            await _context.SaveChangesAsync();

            return lead;
        }

        private bool LeadExists(string id)
        {
            return ((RepositoryContext)_context).Leads.Any(e => e.Id == id);
        }
    }
}
