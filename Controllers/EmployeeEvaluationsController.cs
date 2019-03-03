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
    public class EmployeeEvaluationsController : ControllerBase
    {
        private readonly TechnicalEvaluationContext _context;

        public EmployeeEvaluationsController(TechnicalEvaluationContext context)
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
                .Where(e => e.TechnicalEvaluatorId == employeeId);

            if (!history.HasValue || (history.HasValue && !history.Value))
            {
                employeeEvaluation = employeeEvaluation.Where(e => !e.Archived);
            }

            return await employeeEvaluation
                .Include(e => e.Employee)
                .ToListAsync();
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
                .Include(e => e.EcfEvaluation)
                    .ThenInclude(e => e.CompetenceNavigation)
                        .ThenInclude(c => c.EcfCompetenceLevel)
                .FirstOrDefaultAsync(e => e.Id == evaluationId);

            return employeeEvaluation;
        }

        // POST: api/EmployeeEvaluations/5
        [HttpPost("ecf-evaluation/{evaluationId}")]
        public async Task<ActionResult<EcfEvaluation>> DeleteEmployeeEvaluation(int evaluationId, EcfEvaluation ecfEvaluation)
        {
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
            employeeEvaluation.StartedById = 11;
            
            var lastEvaluation = await _context.EmployeeEvaluation
                .Include(e => e.EcfEvaluation)
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeEvaluation.EmployeeId && !e.Archived);

            var pastEvaluationsByCompetence = new Dictionary<string, EcfEvaluation>();
            if (lastEvaluation != null)
            {
                lastEvaluation.Archived = true;
                _context.Entry(lastEvaluation).State = EntityState.Modified;

                lastEvaluation.EcfEvaluation.ToDictionary(e => e.Competence, e => new EcfEvaluation
                {
                    EvaluationId = employeeEvaluation.Id,
                    Competence = e.Competence,
                    CompetenceLevel = e.CompetenceLevel
                });
            }

            var competencesFromRoles = await _context.EmployeeRelations
                .Where(p => p.EmployeeId == employeeEvaluation.EmployeeId)
                .Select(p => p.Position)
                .SelectMany(p => p.PositionRole.Select(r => r.Role))
                .SelectMany(r => r.EcfRoleCompetence.Select(c => c.CompetenceId))
                .ToListAsync();
            
            foreach (var item in competencesFromRoles)
            {
                if (!pastEvaluationsByCompetence.ContainsKey(item))
                {
                    pastEvaluationsByCompetence.Add(item, new EcfEvaluation
                    {
                        Competence = item,
                        EvaluationId = employeeEvaluation.Id
                    });
                }
            }
            employeeEvaluation.EcfEvaluation = pastEvaluationsByCompetence.Values;

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

        private bool EmployeeEvaluationExists(int id)
        {
            return _context.EmployeeEvaluation.Any(e => e.Id == id);
        }
    }
}