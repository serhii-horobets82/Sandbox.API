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
    public class OrganizationStructureTypesController : ControllerBase
    {
        private readonly EvoflareDbContext _context;

        public OrganizationStructureTypesController(EvoflareDbContext context)
        {
            _context = context;
        }

        // GET: api/OrganizationStructureTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrganizationStructureType>>> GetOrganizationStructureType()
        {
            return await _context.OrganizationStructureType.ToListAsync();
        }

        // GET: api/OrganizationStructureTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrganizationStructureType>> GetOrganizationStructureType(int id)
        {
            var organizationStructureType = await _context.OrganizationStructureType.FindAsync(id);

            if (organizationStructureType == null)
            {
                return NotFound();
            }

            return organizationStructureType;
        }

        // PUT: api/OrganizationStructureTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganizationStructureType(int id, OrganizationStructureType organizationStructureType)
        {
            if (id != organizationStructureType.Id)
            {
                return BadRequest();
            }

            _context.Entry(organizationStructureType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationStructureTypeExists(id))
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

        // POST: api/OrganizationStructureTypes
        [HttpPost]
        public async Task<ActionResult<OrganizationStructureType>> PostOrganizationStructureType(OrganizationStructureType organizationStructureType)
        {
            _context.OrganizationStructureType.Add(organizationStructureType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrganizationStructureType", new { id = organizationStructureType.Id }, organizationStructureType);
        }

        // DELETE: api/OrganizationStructureTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrganizationStructureType>> DeleteOrganizationStructureType(int id)
        {
            var organizationStructureType = await _context.OrganizationStructureType.FindAsync(id);
            if (organizationStructureType == null)
            {
                return NotFound();
            }

            _context.OrganizationStructureType.Remove(organizationStructureType);
            await _context.SaveChangesAsync();

            return organizationStructureType;
        }

        private bool OrganizationStructureTypeExists(int id)
        {
            return _context.OrganizationStructureType.Any(e => e.Id == id);
        }
    }
}
