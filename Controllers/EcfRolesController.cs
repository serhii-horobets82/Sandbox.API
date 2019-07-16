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
    public class EcfRolesController : ControllerBase
    {
        private readonly EvoflareDbContext _context;

        public EcfRolesController(EvoflareDbContext context)
        {
            _context = context;
        }

        // GET: api/EcfRoles
        [HttpGet]
        public IEnumerable<EcfRole> GetRole([FromQuery] bool? withCompetences)
        {
            if (withCompetences.HasValue && withCompetences.Value)
            {
                return _context.EcfRole
                    .Include(r => r.EcfRoleCompetence)
                        .ThenInclude(c => c.Competence)
                            .ThenInclude(c => c.EcfCompetenceLevel);
            }
            return _context.EcfRole;
        }

        // GET: api/EcfRoles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole([FromRoute] int id, [FromQuery] bool? withCompetences)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EcfRole role = null;
            if (withCompetences.HasValue && withCompetences.Value)
            {
                role = await _context.EcfRole
                    .Include(r => r.EcfRoleCompetence)
                        .ThenInclude(c => c.Competence)
                            .ThenInclude(c => c.EcfCompetenceLevel)
                    .FirstOrDefaultAsync(r => r.Id == id);
            }
            else
            {
                role = await _context.EcfRole.FindAsync(id);
            }

            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        // PUT: api/EcfRoles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole([FromRoute] int id, [FromBody] EcfRole role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != role.Id)
            {
                return BadRequest();
            }

            _context.Entry(role).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(id))
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

        // POST: api/EcfRoles
        [HttpPost]
        public async Task<IActionResult> PostRole([FromBody] EcfRole role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.EcfRole.Add(role);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRole", new { id = role.Id }, role);
        }

        // DELETE: api/EcfRoles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = await _context.EcfRole.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            _context.EcfRole.Remove(role);
            await _context.SaveChangesAsync();

            return Ok(role);
        }

        private bool RoleExists(int id)
        {
            return _context.EcfRole.Any(e => e.Id == id);
        }
    }
}