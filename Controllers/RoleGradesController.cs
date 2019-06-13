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
    public class RoleGradesController : ControllerBase
    {
        private readonly TechnicalEvaluationContext _context;

        public RoleGradesController(TechnicalEvaluationContext context)
        {
            _context = context;
        }

        // GET: api/RoleGrades/role
        [HttpGet("role")]
        public async Task<ActionResult<IEnumerable<EmployeeType>>> GetRolesWithGradesAndSkills()
        {
            var roles = await _context.EmployeeType
                    .Include(t => t.RoleGrade)
                    .ToListAsync();
            return roles;
        }

        // GET: api/RoleGrades/role/1
        [HttpGet("role/{id}")]
        public async Task<ActionResult<EmployeeType>> GetOrganizationRole(int id)
        {
            var role = await _context.EmployeeType
                    .Include(t => t.RoleGrade)
                    .FirstOrDefaultAsync(e => e.Id == id);
            if (role == null)
            {
                return NotFound();
            }
            return role;
        }

        // POST: api/RoleGrades/role/1
        [HttpPut("role/{id}")]
        public async Task<IActionResult> PutOrganizationRole(int id, EmployeeType employeeType)
        {
            employeeType.OrganizationId = 1;
            _context.Entry(employeeType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationRoleExists(id))
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

        // POST: api/RoleGrades/role
        [HttpPost("role")]
        public async Task<ActionResult<EmployeeType>> PostOrganizationRole(EmployeeType employeeType)
        {
            employeeType.OrganizationId = 1;
            _context.EmployeeType.Add(employeeType);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrganizationRole), new { id = employeeType.Id }, employeeType);
        }

        // DELETE: api/RoleGrades/role/5
        [HttpDelete("role/{id}")]
        public async Task<ActionResult<EmployeeType>> DeleteOrganizationRole(int id)
        {
            var item = await _context.EmployeeType.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.EmployeeType.Remove(item);
            await _context.SaveChangesAsync();

            return item;
        }
        //// GET: api/CareerPaths
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<CareerPath>>> GetCareerPath()
        //{
        //    return await _context.CareerPath.ToListAsync();
        //}

        //// GET: api/CareerPaths/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<CareerPath>> GetCareerPath(int id)
        //{
        //    var careerPath = await _context.CareerPath.FindAsync(id);

        //    if (careerPath == null)
        //    {
        //        return NotFound();
        //    }

        //    return careerPath;
        //}

        // GET: api/RoleGrades/1
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleGrade>> GetRoleGrade(int id)
        {
            var grade = await _context.RoleGrade
                .FirstOrDefaultAsync(c => c.Id == id);
            if (grade == null)
            {
                return NotFound();
            }
            return grade;
        }

        // PUT: api/RoleGrades/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoleGrade(int id, RoleGrade grade)
        {
            if (id != grade.Id)
            {
                return BadRequest();
            }

            grade.OrganizationId = 1;
            _context.Entry(grade).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleGradeExists(id))
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

        // POST: api/RoleGrades
        [HttpPost]
        public async Task<ActionResult<RoleGrade>> PostRoleGrade(RoleGrade roleGrade)
        {
            roleGrade.OrganizationId = 1;
            _context.RoleGrade.Add(roleGrade);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRoleGrade), new { id = roleGrade.Id }, roleGrade);
        }

        // DELETE: api/RoleGrades/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RoleGrade>> DeleteRoleGrade(int id)
        {
            var item = await _context.RoleGrade.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.RoleGrade.Remove(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public class CompetenceRow
        {
            public int RoleGradeCompetenceId { get; set; }
            public object Id { get; set; }
            public string Name { get; set; }
            public LevelInfo[] Levels { get; set; }
            public int CompetenceLevel { get; set; }
            public int CompetenceLevelId { get; set; }

            public class LevelInfo
            {
                public IEnumerable<Certificate> Certificates { get; set; }
            }
        }
        // GET: api/RoleGrades/1/competences
        [HttpGet("{roleId}/competences")]
        public async Task<IActionResult> GetRoleGradeCompetences(int roleId)
        {
            var grades = await _context.RoleGrade
                .Include(r => r.RoleGradeCompetence)
                .Where(c => c.EmployeeTypeId == roleId)
                .ToListAsync();
            var skills = await _context.EcfCompetence
                .Include(c => c.EcfCompetenceLevel)
                    .ThenInclude(l => l.CompetenceCertificate)
                        .ThenInclude(c => c.Certificate)
                .ToListAsync();
            var skillsById = skills.ToDictionary(s => s.Id, s => s.EcfCompetenceLevel);
            var result = grades
                .Select(g => new
                {
                    GradeId = g.Id,
                    Rows = g.RoleGradeCompetence.Select(c => new CompetenceRow
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
                })
                .ToDictionary(g => g.GradeId);
            return Ok(result);
        }

        // GET: api/RoleGrades/1/competences
        [HttpGet("{roleId}/{gradeId}")]
        public async Task<IActionResult> GetCompetencesByRoleAndGrade(int roleId, int gradeId)
        {
            var grades = await _context.RoleGrade
                .Where(g => g.Id == gradeId)
                .Include(r => r.RoleGradeCompetence)
                //.Where(c => c.EmployeeTypeId == roleId)
                .ToListAsync();
            var skills = await _context.EcfCompetence
                .Include(c => c.EcfCompetenceLevel)
                    .ThenInclude(l => l.CompetenceCertificate)
                        .ThenInclude(c => c.Certificate)
                .ToListAsync();
            var skillsById = skills.ToDictionary(s => s.Id, s => s.EcfCompetenceLevel);
            var result = grades
                .Select(g => new
                {
                    GradeId = g.Id,
                    Rows = g.RoleGradeCompetence.Select(c => new CompetenceRow
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
                })
                .ToDictionary(g => g.GradeId);
            return Ok(result);
        }
        //public async Task<IActionResult> GetRoleGradeCompetencesByGrade(int roleId, int gradeId)
        //{
        //    var grades = await _context.RoleGrade
        //        .Include(r => r.RoleGradeCompetence)
        //        .Where(c => c.EmployeeTypeId == roleId)
        //        .ToListAsync();
        //    var skills = await _context.EcfCompetence.Include(c => c.EcfCompetenceLevel).ToListAsync();
        //    var skillsById = skills.ToDictionary(s => s.Id, s => s.EcfCompetenceLevel.Select(l => l.Level).ToArray());
        //    var result = grades
        //        .Select(g => new
        //        {
        //            GradeId = g.Id,
        //            Rows = g.RoleGradeCompetence.Select(c => new CompetenceRow
        //            {
        //                RoleGradeCompetenceId = c.Id,
        //                Id = c.CompetenceId,
        //                Name = c.Competence.Name,
        //                CompetenceLevel = c.CompetenceLevel.Level,
        //                Levels = Enumerable.Range(1, 5).Select(i => skillsById[c.CompetenceId].Contains(i)).ToArray()
        //            })
        //        })
        //        .ToDictionary(g => g.GradeId);
        //    return Ok(result);
        //}

        public class PostRoleGradeCompetencesModel : RoleGradeCompetence
        {
            public int CompetenceLevelMark { get; set; }
        }

        // GET: api/RoleGrades/1/competences
        [HttpPost("{roleId}/competences")]
        public async Task<IActionResult> PostRoleGradeCompetences(int roleId, List<PostRoleGradeCompetencesModel> rows)
        {
            var competenceLevels = await _context.EcfCompetenceLevel.ToListAsync();
            var lookup = competenceLevels.ToLookup(l => l.CompetenceId);
            rows.ForEach(r =>
            {
                var level = lookup[r.CompetenceId].First(c => c.Level == r.CompetenceLevelMark);
                r.CompetenceLevelId = level.Id;

                if (r.Id == 0)
                {
                    _context.RoleGradeCompetence.Add(r as RoleGradeCompetence);
                }
                else
                {
                    _context.Entry(r as RoleGradeCompetence).Property(nameof(RoleGradeCompetence.CompetenceLevelId)).IsModified = true;
                }
            });
            await _context.SaveChangesAsync();
            var skills = await _context.EcfCompetence
                .Include(c => c.EcfCompetenceLevel)
                    .ThenInclude(l => l.CompetenceCertificate)
                        .ThenInclude(c => c.Certificate)
                .ToListAsync();
            var skillsById = skills.ToDictionary(s => s.Id, s => s.EcfCompetenceLevel);
            var result = rows.Select(c => new CompetenceRow
            {
                RoleGradeCompetenceId = c.Id,
                Id = c.CompetenceId,
                Name = c.Competence.Name,
                CompetenceLevel = c.CompetenceLevel.Level,
                Levels = Enumerable.Range(1, 5)
                    .Select(i =>
                    {
                        var level = skillsById[c.CompetenceId].FirstOrDefault(cl => cl.Level == i);
                        if (level == null) return null;

                        return new CompetenceRow.LevelInfo
                        {
                            Certificates = level.CompetenceCertificate.Any()
                                ? level.CompetenceCertificate.Select(cc => cc.Certificate).ToList()
                                : null
                        };
                    })
                    .ToArray()
            });
            return Ok(result);
        }

        private bool RoleGradeExists(int id)
        {
            return _context.RoleGrade.Any(e => e.Id == id);
        }
        private bool OrganizationRoleExists(int id)
        {
            return _context.EmployeeType.Any(e => e.Id == id);
        }
    }
}
