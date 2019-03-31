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
    public class PositionsController : ControllerBase
    {
        private readonly TechnicalEvaluationContext _context;

        public PositionsController(TechnicalEvaluationContext context)
        {
            _context = context;
        }

        // GET: api/Positions
        /// <summary>
        /// Get list of all available positions across the Organization
        /// </summary>
        [HttpGet]
        public async Task<IEnumerable<Position>> GetPosition()
        {
            return await LoadPositions();
        }

        // GET: api/Positions/project/1
        /// <summary>
        /// Get list of positions by project
        /// </summary>
        [HttpGet("project/{id}")]
        public async Task<IEnumerable<Position>> GetPositionByProject(int id)
        {
            return await LoadPositions(id);
        }

        private async Task<IEnumerable<Position>> LoadPositions(int? projectId = null)
        {
            var positions = _context.Position.AsQueryable();
            if (projectId.HasValue)
            {
                positions = positions.Where(p => p.ProjectId == projectId.Value);
            }
            return await positions.Select(p => new Position
            {
                Id = p.Id,
                Name = p.Name,
                PositionRole = p.PositionRole.Select(pr => new PositionRole
                {
                    Id = pr.Id,
                    RoleId = pr.RoleId,
                    Role = new EcfRole
                    {
                        Id = pr.Role.Id,
                        Name = pr.Role.Name,
                        Summary = pr.Role.Summary,
                        Description = pr.Role.Description,
                        EcfRoleCompetence = pr.Role.EcfRoleCompetence.Select(c => new EcfRoleCompetence
                        {
                            Id = c.Id,
                            CompetenceId = c.CompetenceId,
                            CompetenceLevel = c.CompetenceLevel,
                            Competence = new EcfCompetence
                            {
                                Id = c.Competence.Id,
                                Name = c.Competence.Name,
                                Summary = c.Competence.Summary,
                                EcfCompetenceLevel = c.Competence.EcfCompetenceLevel
                                    .Select(l => new EcfCompetenceLevel
                                    {
                                        Id = l.Id,
                                        Level = l.Level
                                    })
                                    .ToList()
                            },
                        }).ToList()
                    }
                }).ToList()
            })
            .ToListAsync();
        }

        [HttpGet("/simple")]
        public IEnumerable<Position> GetPositionSimple()
        {
            return _context.Position
                .Include(position => position.PositionRole)
                    .ThenInclude(positionRole => positionRole.Role)
                    .ThenInclude(role => role.EcfRoleCompetence)
                    .ThenInclude(roleCompetence => roleCompetence.Competence);
        }

        // GET: api/Positions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPosition([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var position = await _context.Position
                .Include(c => c.PositionRole)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (position == null)
            {
                return NotFound();
            }

            return Ok(position);
        }

        // PUT: api/Positions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPosition([FromRoute] int id, [FromBody] Position position)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != position.Id)
            {
                return BadRequest();
            }

            position.UpdatedBy = 1;
            position.UpdatedDate = DateTime.UtcNow;
            _context.Entry(position).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PositionExists(id))
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

        // POST: api/Positions
        [HttpPost]
        public async Task<IActionResult> PostPosition([FromBody] Position position)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //position.CreatedDate = DateTime.UtcNow;
            position.CreatedBy = 1;
            foreach(var positionRole in position.PositionRole)
            {
                //positionRole.DateTime = DateTime.UtcNow;
            }
            _context.Position.Add(position);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPosition", new { id = position.Id }, position);
        }

        // DELETE: api/Positions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosition([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var position = await _context.Position.FindAsync(id);
            if (position == null)
            {
                return NotFound();
            }

            _context.Position.Remove(position);
            await _context.SaveChangesAsync();

            return Ok(position);
        }

        private bool PositionExists(int id)
        {
            return _context.Position.Any(e => e.Id == id);
        }
    }
}