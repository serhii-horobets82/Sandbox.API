﻿using System;
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
    public class EmployeesController : ControllerBase
    {
        private readonly TechnicalEvaluationContext _context;

        public EmployeesController(TechnicalEvaluationContext context)
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

        [HttpGet("managers")]
        public IEnumerable<Employee> GetManagers()
        {
            return _context.Employee.Where(e => e.IsManager);
        }

        [HttpGet("type/:typeId")]
        public IEnumerable<Employee> GetEmployeesByType(int employeeType)
        {
            return _context.Employee.Where(e => e.EmployeeTypeId == employeeType);
        }

        [HttpGet("types")]
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
        // TODO: remove this from header, it should go from user 
        [HttpGet("profile")]
        public async Task<IActionResult> GetEmployeeProfile([FromHeader(Name = "_EmployeeId")] int id)
        {
            var employee = await _context.Employee
                .Include(e => e.EmployeeType)
                .Include(e => e.EmployeeRelationsEmployee)
                    .ThenInclude(e => e.Team.Project)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        public class ProfileEcfCompetence
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public int CompetenceLevel { get; set; }
            public int RoleLevel { get; set; }
            public List<int> Levels { get; set; }
        }

        // GET: api/Employees/profile/ecf-evaluation
        // TODO: remove from header, should come from user
        [HttpGet("profile/ecf-evaluation")]
        public async Task<List<ProfileEcfCompetence>> GetEmployeeProfileEcf([FromHeader(Name = "_EmployeeId")] int id)
        {
            var competences = await _context.EcfCompetence
                .Include(c => c.EcfCompetenceLevel)
                .ToListAsync();
            var competencesById = competences.ToDictionary(c => c.Id);

            var competencesFromRoles = await _context.EmployeeRelations
                .Where(p => p.EmployeeId == id)
                .Select(p => p.Position)
                .SelectMany(p => p.PositionRole.Select(r => r.Role))
                .SelectMany(r => r.EcfRoleCompetence.Select(c => c.CompetenceId))
                .ToListAsync();

            var lastEvaluation = await _context.EmployeeEvaluation
                .Include(e => e.EcfEmployeeEvaluation)
                    .ThenInclude(e => e.EcfEvaluationResult)
                .FirstOrDefaultAsync(e => e.EmployeeId == id && !e.Archived);

            var pastEvaluationsByCompetence = new Dictionary<string, EcfEvaluationResult>();
            if (lastEvaluation != null)
            {
                if (lastEvaluation.EcfEmployeeEvaluation != null && lastEvaluation.EcfEmployeeEvaluation.Any())
                {
                    pastEvaluationsByCompetence = lastEvaluation
                        .EcfEmployeeEvaluation.First()
                        .EcfEvaluationResult
                        .ToDictionary(e => e.Competence, e => new EcfEvaluationResult
                        {
                            Competence = e.Competence,
                            CompetenceLevel = e.CompetenceLevel
                        });
                }
            }

            foreach (var item in competencesFromRoles)
            {
                if (!pastEvaluationsByCompetence.ContainsKey(item))
                {
                    pastEvaluationsByCompetence.Add(item, new EcfEvaluationResult
                    {
                        Competence = item
                    });
                }
            }

            var profileCompetences = new List<ProfileEcfCompetence>();
            foreach(var competenceId in pastEvaluationsByCompetence.Keys)
            {
                var competence = competencesById[competenceId];
                var c = pastEvaluationsByCompetence[competenceId];
                var d = new ProfileEcfCompetence
                {
                    Id = c.Competence,
                    Name = competence.Name,
                    RoleLevel = 0,
                    CompetenceLevel = c.CompetenceLevel ?? 0,
                    Levels = Enumerable.Range(1, 5).Select(i => 0).ToList()                    
                };
                foreach(var l in competence.EcfCompetenceLevel)
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