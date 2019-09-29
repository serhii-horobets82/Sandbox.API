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
    public class EmployeeEvaluationsController : BaseController
    {
        private readonly EvoflareDbContext _context;

        public EmployeeEvaluationsController(EvoflareDbContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeEvaluations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeEvaluation>>> GetEmployeeEvaluation()
        {
            return await _context.EmployeeEvaluation
                .Where(e => !e.Archived)
                .ToListAsync();
        }

        // GET: api/EmployeeEvaluations/5/whom-evaluate
        [HttpGet("{employeeId}/whom-evaluate")]
        public async Task<ActionResult<IEnumerable<EmployeeEvaluation>>> GetWhomEvaluate(int employeeId, [FromQuery] bool? history)
        {
            var employeeEvaluation = _context.EmployeeEvaluation
                .Where(e => e.EcfEmployeeEvaluation.Any(t => t.EvaluationId == employeeId));

            if (!history.HasValue || (history.HasValue && !history.Value))
            {
                employeeEvaluation = employeeEvaluation.Where(e => !e.Archived);
            }

            return await employeeEvaluation
                .Include(e => e.Employee)
                .Include(e => e.EcfEmployeeEvaluation)
                .ToListAsync();
        }

        // GET: api/EmployeeEvaluations/5/evaluators
        [HttpGet("{employeeId}/evaluators")]
        public async Task<IActionResult> Get360Evaluators(int employeeId)
        {
            // TODO: only works for employee. Add implementation for Manager
            var employeeEvaluation = _context.EmployeeEvaluation
                .Where(e => e.EcfEmployeeEvaluation.Any(ee => ee.EvaluatorId == employeeId));

            var a = await _context.EmployeeRelations
                .Where(r => r.EmployeeId == employeeId)
                .ToListAsync();
            var teams = a.Select(r => r.TeamId).Distinct().ToHashSet();
            var directManagerIds = a.Select(r => r.ManagerId).Where(m => m != null).Distinct().ToList();

            var directManagers = await _context.Employee
                .Where(e => directManagerIds.Contains(e.Id))
                .ToListAsync();

            var peersAndManagers = await _context.EmployeeRelations
                .Where(r => teams.Contains(r.TeamId))
                .Include(r => r.Employee)
                .Include(r => r.Manager)
                .ToListAsync();

            var customers = await _context.Team
                .Where(t => teams.Contains(t.Id))
                .SelectMany(t => t.Project.CustomerContact)
                .ToListAsync();

            var result = new
            {
                Customers = customers,
                Peers = peersAndManagers.Select(r => r.Manager).Where(m => m != null)
                    .Concat(
                        peersAndManagers.Select(r => r.Employee).Where(e => e != null)
                    )
                    .Where(e => e.Id != employeeId)
                    .Distinct().ToList()
            };
            return Ok(result);
        }

        // GET: api/EmployeeEvaluations/5
        [HttpGet("{employeeId}")]
        public async Task<ActionResult<IEnumerable<EmployeeEvaluation>>> GetEvaluations(int employeeId)
        {
            var employeeEvaluation = await _context.EmployeeEvaluation
                .Where(e => e.EmployeeId == employeeId)
                .ToListAsync();

            return employeeEvaluation;
        }

        // GET: api/EmployeeEvaluations/ecf-evaluation/1
        [HttpGet("ecf-evaluation/{evaluationId}")]
        public async Task<ActionResult<EmployeeEvaluation>> GetEvaluationById(int evaluationId)
        {
            var employeeEvaluation = await _context.EmployeeEvaluation
                .Include(e => e.Employee)
                .Include(e => e.EcfEmployeeEvaluation)
                    .ThenInclude(e => e.EcfEvaluationResult)
                        .ThenInclude(e => e.CompetenceNavigation)
                            .ThenInclude(c => c.CompetenceLevel)
                .FirstOrDefaultAsync(e => e.Id == evaluationId);

            return employeeEvaluation;
        }

        // POST: api/EmployeeEvaluations/ecf-evaluation/{evaluationId}
        // evaluationId is actual Evaluation id, NOT the EcfEvaluation id
        [HttpPost("ecf-evaluation/{evaluationId}")]
        public async Task<ActionResult<EcfEvaluationResult>> PostEcfEvaluationMark(int evaluationId, EcfEvaluationResult ecfEvaluation)
        {
            ecfEvaluation.Date = DateTime.UtcNow;
            _context.Entry(ecfEvaluation).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return ecfEvaluation;
        }

        // PUT: api/EmployeeEvaluations/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutEmployeeEvaluation(int id, EmployeeEvaluation employeeEvaluation)
        //{
        //    if (id != employeeEvaluation.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(employeeEvaluation).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EmployeeEvaluationExists(id))
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

        // POST: api/EmployeeEvaluations
        [HttpPost]
        public async Task<ActionResult<EmployeeEvaluation>> PostEmployeeEvaluation(EmployeeEvaluation employeeEvaluation)
        {
            employeeEvaluation.Archived = false;
            employeeEvaluation.OrganizationId = 1;
            employeeEvaluation.StartDate = DateTime.UtcNow;
            employeeEvaluation.StartedById = GetEmployeeId();

            var lastEvaluation = await _context.EmployeeEvaluation
                .Include(e => e.EcfEmployeeEvaluation)
                    .ThenInclude(e => e.EcfEvaluationResult)
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeEvaluation.EmployeeId && !e.Archived);

            var pastEvaluationsByCompetence = new Dictionary<int, EcfEvaluationResult>();
            if (lastEvaluation != null)
            {
                lastEvaluation.Archived = true;
                _context.Entry(lastEvaluation).State = EntityState.Modified;
                if (lastEvaluation.EcfEmployeeEvaluation != null && lastEvaluation.EcfEmployeeEvaluation.Any())
                {
                    pastEvaluationsByCompetence = lastEvaluation
                        .EcfEmployeeEvaluation.First()
                        .EcfEvaluationResult
                        .ToDictionary(e => e.Competence, e => new EcfEvaluationResult
                        {
                            EvaluationId = employeeEvaluation.Id,
                            Competence = e.Competence,
                            CompetenceLevel = e.CompetenceLevel
                        });
                }
            }

            var competencesFromRoles = await _context.EmployeeRelations
                .Where(p => p.EmployeeId == employeeEvaluation.EmployeeId)
                .Select(p => p.Position)
                .SelectMany(p => p.PositionRole.Select(r => r.Role))
                .SelectMany(r => r.RoleCompetence.Select(c => c.CompetenceId))
                .ToListAsync();

            foreach (var item in competencesFromRoles)
            {
                if (!pastEvaluationsByCompetence.ContainsKey(item))
                {
                    pastEvaluationsByCompetence.Add(item, new EcfEvaluationResult
                    {
                        Competence = item,
                        EvaluationId = employeeEvaluation.Id
                    });
                }
            }
            employeeEvaluation.EcfEmployeeEvaluation = new List<EcfEmployeeEvaluation>
            {
                new EcfEmployeeEvaluation
                {
                    EcfEvaluationResult = pastEvaluationsByCompetence.Values.ToList(),
                    OrganizationId = 1
                }
            };

            foreach (var item in employeeEvaluation._360employeeEvaluation)
            {
                item.OrganizationId = 1;
                item.StartDate = DateTime.UtcNow;
            }

            _context.EmployeeEvaluation.Add(employeeEvaluation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployeeEvaluation", new { id = employeeEvaluation.Id }, employeeEvaluation);
        }

        // DELETE: api/EmployeeEvaluations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EmployeeEvaluation>> DeleteEmployeeEvaluation(int id)
        {
            var employeeEvaluation = await _context.EmployeeEvaluation.FindAsync(id);
            if (employeeEvaluation == null)
            {
                return NotFound();
            }

            _context.EmployeeEvaluation.Remove(employeeEvaluation);
            await _context.SaveChangesAsync();

            return employeeEvaluation;
        }

        // GET: api/EmployeeEvaluations/i-evaluate-360
        [HttpGet("i-evaluate-360")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetIEvaluate360()
        {
            var employeeId = GetEmployeeId();
            var settings = await _context._360evaluationSchedule.FirstOrDefaultAsync();
            
            if (settings == null || settings.StartDate < DateTime.UtcNow)
            {
                return (await BuildRelations(_context, employeeId))
                    .Select(e => new _360employeeEvaluation
                    {
                        EndDate = DateTime.MinValue,
                        Evaluation = new EmployeeEvaluation
                        {
                            Employee = e
                        }
                    })
                    .ToList();
            }
            var employeesToEvaluate = await _context._360employeeEvaluation
                .Where(e => e.EvaluatorEmployeeId == employeeId /*&& e.EndDate == null && e.Evaluation.EndDate == null*/)
                .Include(e => e.Evaluation.Employee)
                .OrderByDescending(e => e.StartDate)
                //.GroupBy(e => e.StartDate)
                //.Select(e => new { Date = e.Key, Employees = e })
                .ToListAsync();

            return employeesToEvaluate;
        }

        private static async Task<List<Employee>> BuildRelations(EvoflareDbContext context, int employeeId)
        {
            var relations = await context.EmployeeRelations
                .Where(r => r.EmployeeId == employeeId)
                .Include(r => r.Employee)
                .Include(r => r.Manager)
                .ToListAsync();
            var teams = relations.Select(r => r.TeamId).Distinct().ToHashSet();
            var directManagerIds = relations.Select(r => r.ManagerId).Where(m => m != null).Distinct().ToHashSet();
            var directManagers = relations
                .Select(r => r.Manager).Where(m => m != null)
                .Where(e => directManagerIds.Contains(e.Id))
                .ToList();

            var peersAndManagers = relations.Where(r => teams.Contains(r.TeamId)).ToList();
            var peers = peersAndManagers.Select(r => r.Employee).Where(e => e != null)
                .Concat(
                    peersAndManagers.Select(r => r.Manager).Where(m => m != null)
                )
                .Concat(directManagers)
                .Where(e => e.Id != employeeId)
                .Distinct().ToList();

            return peers;
        }

        private bool EmployeeEvaluationExists(int id)
        {
            return _context.EmployeeEvaluation.Any(e => e.Id == id);
        }
    }
}
