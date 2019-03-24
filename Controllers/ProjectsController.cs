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
    public class ProjectsController : ControllerBase
    {
        private readonly TechnicalEvaluationContext _context;

        public ProjectsController(TechnicalEvaluationContext context)
        {
            _context = context;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<IEnumerable<Project>> GetProjects()
        {
            var projects = await _context.Project
                //.Where(p => p.OrganizationId == 1)
                .Include(_ => _.Team)
                    .ThenInclude(t => t.EmployeeRelations)
                        .ThenInclude(r => r.Position)
                .Include(p => p.CustomerContact)
                .ToListAsync();

            var employees = await _context.Employee.Include(e => e.EmployeeType).ToListAsync();
            foreach (var proj in projects)
            {
                proj.EmployeeRelations = null;
                foreach (var team in proj.Team)
                {
                    team.Project = null;
                    foreach (var relation in team.EmployeeRelations)
                    {
                        relation.Project = null;
                        relation.Team = null;
                       
                        if (relation.Employee != null)
                        {
                            relation.Employee.EmployeeRelationsEmployee = null;
                            relation.Employee.EmployeeRelationsManager = null;
                            relation.Employee.PositionCreatedByNavigation = null;
                            relation.Employee._360pendingEvaluator = null;
                            relation.Employee.EcfEmployeeEvaluationEndBy = null;
                            relation.Employee.EcfEmployeeEvaluationStartBy = null;
                            relation.Employee.EmployeeEvaluationStartedBy = null;
                            relation.Employee.EmployeeEvaluationEndedBy = null;
                        }
                        if (relation.Manager != null)
                        {
                            relation.Manager.EmployeeRelationsEmployee = null;
                            relation.Manager.EmployeeRelationsManager = null;
                            relation.Manager.PositionCreatedByNavigation = null;
                            relation.Manager._360pendingEvaluator = null;
                            relation.Manager.EcfEmployeeEvaluationEndBy = null;
                            relation.Manager.EcfEmployeeEvaluationStartBy = null;
                            relation.Manager.EmployeeEvaluationStartedBy = null;
                            relation.Manager.EmployeeEvaluationEndedBy = null;
                        }
                        if (relation.Position != null)
                        {
                            relation.Position.EmployeeRelations = null;
                            relation.Position.CreatedByNavigation = null;
                            relation.Position.UpdatedByNavigation = null;
                        }
                    }
                }
            }
            return projects;
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var project = await _context.Project.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        // PUT: api/Projects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject([FromRoute] int id, [FromBody] Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != project.Id)
            {
                return BadRequest();
            }

            _context.Entry(project).State = EntityState.Modified;
            foreach (var item in project.CustomerContact)
            {
                item.OrganizationId = 1;
                item.ProjectId = project.Id;

                if (item.Id == 0)
                {
                    _context.CustomerContact.Add(item);
                }
                else
                {
                    // TODO: this may lead to incorrect history of updates
                    _context.Entry(item).State = EntityState.Modified;
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
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

        // POST: api/Projects
        [HttpPost]
        public async Task<IActionResult> PostProject([FromBody] Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            project.OrganizationId = 1;
            
            _context.Project.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProject", new { id = project.Id }, project);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var project = await _context.Project.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Project.Remove(project);
            await _context.SaveChangesAsync();

            return Ok(project);
        }

        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.Id == id);
        }
    }
}