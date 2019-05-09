using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evoflare.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace Evoflare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetencesController : ControllerBase
    {
        private readonly TechnicalEvaluationContext _context;

        public CompetencesController(TechnicalEvaluationContext context)
        {
            this._context = context;
        }

        [HttpGet("", Name = "GetCompetences")]
        [SwaggerResponse(StatusCodes.Status200OK, "List of all competences.", typeof(List<EcfCompetence>))]
        public async Task<List<EcfCompetence>> Get()
        {
            return await _context.EcfCompetence
                .Include(level => level.EcfCompetenceLevel)
                .ToListAsync();
        }

        [HttpGet("{id}", Name = "GetCompetence")]
        [SwaggerResponse(StatusCodes.Status200OK, "The Competence with the specified unique identifier.", typeof(EcfCompetence))]
        [SwaggerResponse(StatusCodes.Status304NotModified, "The Competence has not changed since the date given in the If-Modified-Since HTTP header.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A Competence with the specified unique identifier could not be found.")]
        public async Task<EcfCompetence> Get(string id)
        {
            return await _context.EcfCompetence
                .Include(level => level.EcfCompetenceLevel)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public class EcfCompetenceRow
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public int? CompetenceLevel { get; set; }
            public int? RoleLevel { get; set; }
            public List<bool> Levels { get; set; }
            //public class RowLevel
            //{
            //    public int Level { get; set; }
            //}
        }

        [HttpGet("rows")]
        public async Task<List<EcfCompetenceRow>> GetCompetences([FromHeader(Name = "_EmployeeId")] int id)
        {
            // getting all the competences from DB
            var competences = await _context.EcfCompetence
                .Include(c => c.EcfCompetenceLevel)
                .ToListAsync();
            return competences.Select(c => new EcfCompetenceRow
            {
                Id = c.Id,
                Name = c.Name,
                Levels = Enumerable.Range(1, 5)
                    .Select(i => c.EcfCompetenceLevel.Any(l => l.Level == i))
                    //.Select(i => new EcfCompetenceRow.RowLevel { Level = i ? 1 : 0 })
                    .ToList()
            }).ToList();
        }
    }
}
