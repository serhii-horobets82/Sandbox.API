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
    public class EmployeesController : BaseController
    {
        private readonly EvoflareDbContext _context;

        public EmployeesController(EvoflareDbContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public IEnumerable<Employee> GetEmployees([FromQuery] int? typeId)
        {
            var employees = _context.Employee.Include(e => e.EmployeeType);
            if (typeId.HasValue)
            {
                return employees.Where(e => e.EmployeeTypeId == typeId.Value);
            }
            return employees;
        }

        [HttpGet("salary")]
        public IEnumerable<Employee> GetEmployeesSalary()
        {
            var employees = _context.Employee.Include(e => e.EmployeeType);
            return employees;
        }


        [HttpGet("managers")]
        public IEnumerable<Employee> GetManagers()
        {
            return _context.Employee.Where(e => e.IsManager);
        }

        [HttpGet("roles/:typeId")]
        public IEnumerable<Employee> GetEmployeesByType(int employeeType)
        {
            return _context.Employee.Where(e => e.EmployeeTypeId == employeeType);
        }

        [HttpGet("roles")]
        public IEnumerable<EmployeeType> GetEmployeeTypes()
        {
            return _context.EmployeeType;
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = await _context.Employee
                .Include(e => e.EmployeeType)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee([FromRoute] int id, [FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.Id)
            {
                return BadRequest();
            }

            employee.IsManager = employee.EmployeeTypeId == 6;
            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employees
        [HttpPost]
        public async Task<IActionResult> PostEmployee([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            employee.IsManager = employee.EmployeeTypeId == 6;
            employee.OrganizationId = 1;
            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok(employee);
        }

        // GET: api/Employees/profile
        [HttpGet("profile")]
        public async Task<IActionResult> GetEmployeeProfile()
        {
            var employee = await _context.Employee
                .Include(e => e.EmployeeType)
                .Include(e => e.EmployeeRelationsEmployee)
                    .ThenInclude(e => e.Team.Project)
                .FirstOrDefaultAsync(e => e.Id == GetEmployeeId());

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // GET: api/Employees/suggestions-to-improve
        [HttpGet("suggestions-to-improve")]
        public async Task<List<_360employeeEvaluation>> GetSuggestionsToImprove()
        {
            var evaluation = await _context.EmployeeEvaluation.FirstOrDefaultAsync(e => e.EmployeeId == GetEmployeeId() && !e.Archived);
            if (evaluation != null)
            {
                var _360Feedbacks = await _context._360employeeEvaluation.Where(e => e.EvaluationId == evaluation.Id).ToListAsync();
                return _360Feedbacks;
            }

            return new List<_360employeeEvaluation>();
        }

        public class ProfileEcfCompetence
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int CompetenceLevel { get; set; }
            public int RoleLevel { get; set; }
            public List<int> Levels { get; set; }
        }

        // GET: api/Employees/profile/ecf-evaluation
        [HttpGet("profile/ecf-evaluation")]
        public async Task<List<ProfileEcfCompetence>> GetEmployeeProfileEcf()
        {
            // getting all the competences from DB
            var competences = await _context.Competence
                .Include(c => c.CompetenceLevel)
                .ToListAsync();
            var competencesById = competences.ToDictionary(c => c.Id);
            var employeeId = GetEmployeeId();
            // getting only Competences from roles
            var competencesFromRoles = await _context.EmployeeRelations
                .Where(p => p.EmployeeId == employeeId)
                .Select(p => p.Position)
                .SelectMany(p => p.PositionRole.Select(r => r.Role))
                .SelectMany(r => r.RoleCompetence.Select(c => new { RoleCompetenceLevel = c.CompetenceLevel, c.CompetenceId }))
                .ToListAsync();

            var lastEvaluation = await _context.EmployeeEvaluation
                .Include(e => e.EcfEmployeeEvaluation)
                    .ThenInclude(e => e.EcfEvaluationResult)
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId && !e.Archived);

            var pastEvaluationsByCompetence = 
                new Dictionary<int, (int competenceId, int competenceLevel, int roleCompetenceLevel)>();
            if (lastEvaluation != null)
            {
                if (lastEvaluation.EcfEmployeeEvaluation != null && lastEvaluation.EcfEmployeeEvaluation.Any())
                {
                    pastEvaluationsByCompetence = lastEvaluation
                        .EcfEmployeeEvaluation.First()
                        .EcfEvaluationResult
                        .ToDictionary(e => e.Competence, e => (e.Competence, e.CompetenceLevel ?? 0, 0));
                }
            }

            foreach (var item in competencesFromRoles)
            {
                if (!pastEvaluationsByCompetence.ContainsKey(item.CompetenceId))
                {
                    pastEvaluationsByCompetence.Add(item.CompetenceId, (item.CompetenceId, 0, item.RoleCompetenceLevel));
                }
                else
                {
                    var c = pastEvaluationsByCompetence[item.CompetenceId];
                    pastEvaluationsByCompetence[item.CompetenceId] = (c.competenceId, c.competenceLevel, item.RoleCompetenceLevel);
                }
            }

            var profileCompetences = new List<ProfileEcfCompetence>();
            foreach(var competenceId in pastEvaluationsByCompetence.Keys)
            {
                var competence = competencesById[competenceId];
                var c = pastEvaluationsByCompetence[competenceId];
                var d = new ProfileEcfCompetence
                {
                    Id = c.competenceId,
                    Name = competence.Name,
                    RoleLevel = c.roleCompetenceLevel,
                    CompetenceLevel = c.competenceLevel,
                    Levels = Enumerable.Range(1, 5).Select(i => 0).ToList()                    
                };
                foreach(var l in competence.CompetenceLevel)
                {
                    d.Levels[l.Level] = 1;
                }
                profileCompetences.Add(d);
            }

            return profileCompetences;
        }


        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }
    }
}