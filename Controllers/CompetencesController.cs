﻿using System.Collections.Generic;
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
        private readonly TechnicalEvaluationContext db;

        public CompetencesController(TechnicalEvaluationContext db)
        {
            this.db = db;
        }

        [HttpGet("", Name = "GetCompetences")]
        [SwaggerResponse(StatusCodes.Status200OK, "List of all competences.", typeof(List<Competence>))]
        public async Task<List<Competence>> Get()
        {
            return await db.Competence
                .Include(level => level.CompetenceLevel)
                .ToListAsync();
        }

        [HttpGet("{id}", Name = "GetCompetence")]
        [SwaggerResponse(StatusCodes.Status200OK, "The Competence with the specified unique identifier.", typeof(Competence))]
        [SwaggerResponse(StatusCodes.Status304NotModified, "The Competence has not changed since the date given in the If-Modified-Since HTTP header.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A Competence with the specified unique identifier could not be found.")]
        public async Task<Competence> Get(string id)
        {
            return await db.Competence
                .Include(level => level.CompetenceLevel)
                .FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
