using System.Collections.Generic;
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
    public class RolesController : ControllerBase
    {
        private readonly TechnicalEvaluationContext db;

        public RolesController(TechnicalEvaluationContext db)
        {
            this.db = db;
        }

        [HttpGet("", Name = "GetRoles")]
        [SwaggerResponse(StatusCodes.Status200OK, "List of all roles.", typeof(List<Role>))]
        public async Task<List<Role>> Get()
        {
            return await db.Role.ToListAsync();
        }

        [HttpGet("{id}", Name = "GetRole")]
        [SwaggerResponse(StatusCodes.Status200OK, "The Role with the specified unique identifier.", typeof(Role))]
        [SwaggerResponse(StatusCodes.Status304NotModified, "The Role has not changed since the date given in the If-Modified-Since HTTP header.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A Role with the specified unique identifier could not be found.")]
        public async Task<Role> Get(int id)
        {
            return await db.Role.FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
