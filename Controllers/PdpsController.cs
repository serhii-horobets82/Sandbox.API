using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Evoflare.API.Models;

namespace Evoflare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdpsController : ControllerBase
    {
        private readonly EvoflareDbContext _context;

        public PdpsController(EvoflareDbContext context)
        {
            _context = context;
        }

        // GET: api/Pdps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pdp>>> GetPdp()
        {
            return await _context.Pdp.ToListAsync();
        }

        // GET: api/Pdps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pdp>> GetPdp(int id)
        {
            var pdp = await _context.Pdp.FindAsync(id);

            if (pdp == null)
            {
                return NotFound();
            }

            return pdp;
        }

        // PUT: api/Pdps/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPdp(int id, Pdp pdp)
        {
            if (id != pdp.Id)
            {
                return BadRequest();
            }

            _context.Entry(pdp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PdpExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Pdps
        [HttpPost]
        public async Task<ActionResult<Pdp>> PostPdp(Pdp pdp)
        {
            _context.Pdp.Add(pdp);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPdp", new { id = pdp.Id }, pdp);
        }

        // DELETE: api/Pdps/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pdp>> DeletePdp(int id)
        {
            var pdp = await _context.Pdp.FindAsync(id);
            if (pdp == null)
            {
                return NotFound();
            }

            _context.Pdp.Remove(pdp);
            await _context.SaveChangesAsync();

            return pdp;
        }

        private bool PdpExists(int id)
        {
            return _context.Pdp.Any(e => e.Id == id);
        }
    }
}
