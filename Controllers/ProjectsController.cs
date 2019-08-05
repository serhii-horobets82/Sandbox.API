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
    public class ProjectsController : BaseController
    {
        private readonly EvoflareDbContext _context;

        public ProjectsController(EvoflareDbContext context)
        {
            _context = context;
        }

        // GET: api/Projects/basic
        [HttpGet("basic")] // admin
        public async Task<IEnumerable<dynamic>> GetProjectsBasic()
        {
            var projects = await _context.Project
                .Include(p => p.EmployeeRelations)
                    .ThenInclude(r => r.Manager)
                .ToListAsync();
            var rel = await _context.EmployeeRelations
                .Where(r => r.ManagerId.HasValue && !r.TeamId.HasValue && r.ProjectId.HasValue)
                .Include(r => r.Manager)
                .ToDictionaryAsync(r => r.ProjectId.Value, r => r.Manager);

            return projects.Select(p => {
                var hasManager = rel.TryGetValue(p.Id, out var manager);
                return new
                {
                    p.Id,
                    p.Name,
                    p.CreatedDate,
                    ManagerId = hasManager ? manager.Id : 0,
                    ManagerFullName = hasManager ? manager.Name + " " + manager.Surname : string.Empty
                };
            });
        }


        // GET: api/Projects
        [HttpGet] // manager
        public async Task<IEnumerable<Project>> GetProjects()
        {
            var employeeId = GetEmployeeId();
            // get project only where I am a manager.
            // TODO: manager could be on the Project level, also on Team level. Should filter teams if the last case.
            var projects = await _context.Project
                .Where(p => p.EmployeeRelations.Any(r => r.ManagerId == employeeId))
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
        [HttpGet("{id}")] // manager
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

        //// PUT: api/Projects/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutProject([FromRoute] int id, [FromBody] Project project)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != project.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(project).State = EntityState.Modified;
        //    foreach (var item in project.CustomerContact)
        //    {
        //        item.OrganizationId = 1;
        //        item.ProjectId = project.Id;

        //        if (item.Id == 0)
        //        {
        //            _context.CustomerContact.Add(item);
        //        }
        //        else
        //        {
        //            // TODO: this may lead to incorrect history of updates
        //            _context.Entry(item).State = EntityState.Modified;
        //        }
        //    }

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProjectExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        public class ProjectViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int? ManagerId { get;set; }
        }
        // POST: api/Projects
        [HttpPost] // admin
        public async Task<IActionResult> PostProject([FromBody] ProjectViewModel projectVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var project = new Project
            {
                Name = projectVm.Name,
                CreatedDate = DateTime.UtcNow,
                OrganizationId = 1
            };
            if (projectVm.ManagerId.HasValue)
            {
                project.EmployeeRelations.Add(new EmployeeRelations {
                    ManagerId = projectVm.ManagerId,
                    OrganizationId = 1
                });
            }
            _context.Project.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProjectsBasic", new { id = project.Id }, project);
        }

        // POST: api/Projects/1/assign-manager
        [HttpPost("{id}/assign-manager")] // admin
        public async Task<IActionResult> AssignProjectManager([FromRoute] int id, [FromBody] EmployeeRelations relation)
        {
            var rel = await _context.EmployeeRelations
                .Where(r => r.ProjectId == id && r.ManagerId.HasValue && !r.TeamId.HasValue)
                .ToListAsync();
            if (rel.Any())
            {
                return BadRequest("Cannot re-assign manager for a project.");
            }

            var d = new EmployeeRelations
            {
                ManagerId = relation.ManagerId,
                ProjectId = id,
                OrganizationId = 1
            };
            _context.EmployeeRelations.Add(d);
            await _context.SaveChangesAsync();

            return Ok(new { id = d.Id });
        }
        //// DELETE: api/Projects/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteProject([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var project = await _context.Project.FindAsync(id);
        //    if (project == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Project.Remove(project);
        //    await _context.SaveChangesAsync();

        //    return Ok(project);
        //}

        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.Id == id);
        }
    }
}