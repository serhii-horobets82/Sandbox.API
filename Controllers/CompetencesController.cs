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
        private readonly EvoflareDbContext _context;

        public CompetencesController(EvoflareDbContext context)
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
                    .ThenInclude(l => l.CompetenceCertificate)
                        .ThenInclude(c => c.Certificate)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public class CompetenceRow
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public int? CompetenceLevel { get; set; }
            public int? RoleLevel { get; set; }
            public List<LevelInfo> Levels { get; set; }

            public class LevelInfo
            {
                public IEnumerable<Certificate> Certificates { get; set; }
            }
        }

        [HttpGet("rows")]
        public async Task<List<CompetenceRow>> GetCompetences()
        {
            // getting all the competences from DB
            var competences = await _context.EcfCompetence
                .Include(c => c.EcfCompetenceLevel)
                    .ThenInclude(l => l.CompetenceCertificate)
                        .ThenInclude(c => c.Certificate)
                .ToListAsync();
            return competences.Select(c => new CompetenceRow
            {
                Id = c.Id,
                Name = c.Name,
                Levels = Enumerable.Range(1, 5)
                    .Select(i =>
                    {
                        var level = c.EcfCompetenceLevel.FirstOrDefault(cl => cl.Level == i);
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
                    .ToList()
            }).ToList();
        }
    }
}
