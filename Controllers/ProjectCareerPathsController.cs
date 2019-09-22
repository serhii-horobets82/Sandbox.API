using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Evoflare.API.Models;
using static Evoflare.API.Controllers.RoleGradesController;

namespace Evoflare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectCareerPathsController : ControllerBase
    {
        private readonly EvoflareDbContext _context;

        public ProjectCareerPathsController(EvoflareDbContext context)
        {
            _context = context;
        }

        // GET: api/ProjectCareerPaths/1
        [HttpGet("{projectId}")]
        public async Task<ActionResult<IEnumerable<ProjectCareerPath>>> GetProjectCareerPath(int projectId)
        {
            return await _context.ProjectCareerPath
                .Include(p => p.Role)
                .Where(p => p.ProjectId == projectId)
                .ToListAsync();
        }

        // GET: api/ProjectCareerPaths/1/competences
        [HttpGet("{projectId}/competences")]
        public async Task<ActionResult<dynamic>> GetProjectCareerPathCompetences(int projectId)
        {
            var result = await _context.ProjectCareerPath
                .Include(p => p.ProjectPosition)
                    .ThenInclude(pp => pp.ProjectPositionCompetence)
                .Where(p => p.ProjectId == projectId)
                .ToListAsync();
            var competences = await _context.Competence
                .Include(c => c.CompetenceLevel)
                    .ThenInclude(l => l.CompetenceCertificate)
                        .ThenInclude(c => c.Certificate)
                .ToListAsync();
            var skillsById = competences.ToDictionary(s => s.Id, s => s.CompetenceLevel);
            return result.Select(r => new
            {
                CareerPathId = r.Id,
                Positions = r.ProjectPosition.Select(p => new
                {
                    RoleGradeId = p.RoleGradeId,
                    Competences = p.ProjectPositionCompetence
                        .Select(c => new CompetenceRow
                        {
                            RoleGradeCompetenceId = c.Id,
                            Id = c.CompetenceId,
                            Name = c.Competence.Name,
                            CompetenceLevel = c.CompetenceLevel.Level,
                            CompetenceLevelId = c.CompetenceLevelId,
                            Levels = Enumerable.Range(1, 5)
                            .Select(i =>
                            {
                                var level = skillsById[c.CompetenceId].FirstOrDefault(cl => cl.Level == i);
                                if (level == null) return null;
                                return new CompetenceRow.LevelInfo
                                {
                                    Certificates = level.CompetenceCertificate.Any()
                                        ? level.CompetenceCertificate
                                            .Select(cc => cc.Certificate)
                                            .Select(cc => new Certificate
                                            {
                                                Id = cc.Id,
                                                Name = cc.Name,
                                                Company = cc.Company,
                                                Technology = cc.Technology,
                                                Stack = cc.Stack,
                                                CertificationLevel = cc.CertificationLevel
                                            })
                                            .ToList()
                                        : null
                                };
                            })
                            .ToArray()
                        })
                }).ToDictionary(p => p.RoleGradeId)
            }).ToDictionary(r => r.CareerPathId);
        }

        // GET: api/ProjectCareerPaths/5
        [HttpGet("{projectId}/{id}")]
        public async Task<ActionResult<ProjectCareerPath>> GetProjectCareerPath(int projectId, int id)
        {
            var projectCareerPath = await _context.ProjectCareerPath
                .Include(p => p.Role)
                .FirstOrDefaultAsync(p => p.Id == id && p.ProjectId == projectId);

            if (projectCareerPath == null)
            {
                return NotFound();
            }

            return projectCareerPath;
        }

        // PUT: api/ProjectCareerPaths/5
        [HttpPut("{projectId}/{id}")]
        public async Task<IActionResult> PutProjectCareerPath(int projectId, int id, ProjectCareerPath projectCareerPath)
        {
            if (id != projectCareerPath.Id || projectId != projectCareerPath.ProjectId)
            {
                return BadRequest();
            }

            projectCareerPath.OrganizationId = 1;
            _context.Entry(projectCareerPath).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectCareerPathExists(id))
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

        // POST: api/ProjectCareerPaths
        [HttpPost("{projectId}")]
        public async Task<ActionResult<ProjectCareerPath>> PostProjectCareerPath(int projectId, ProjectCareerPath projectCareerPath)
        {
            projectCareerPath.OrganizationId = 1;
            _context.ProjectCareerPath.Add(projectCareerPath);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProjectCareerPath", new {projectId = projectId, id = projectCareerPath.Id }, projectCareerPath);
        }

        // DELETE: api/ProjectCareerPaths/5
        [HttpDelete("{projectId}/{id}")]
        public async Task<ActionResult<ProjectCareerPath>> DeleteProjectCareerPath(int projectId, int id)
        {
            var projectCareerPath = await _context.ProjectCareerPath.FindAsync(id);
            if (projectCareerPath == null || projectCareerPath.ProjectId != projectId)
            {
                return NotFound();
            }

            _context.ProjectCareerPath.Remove(projectCareerPath);
            await _context.SaveChangesAsync();

            return projectCareerPath;
        }

        private bool ProjectCareerPathExists(int id)
        {
            return _context.ProjectCareerPath.Any(e => e.Id == id);
        }
    }
}
