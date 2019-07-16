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
        private readonly EvoflareDbContext db;

        public RolesController(EvoflareDbContext db)
        {
            this.db = db;
        }

        [HttpGet("", Name = "GetRoles")]
        [SwaggerResponse(StatusCodes.Status200OK, "List of all roles.", typeof(List<EcfRole>))]
        public async Task<List<EcfRole>> Get()
        {
            return await db.EcfRole.ToListAsync();
        }

        [HttpGet("{id}", Name = "GetRole")]
        [SwaggerResponse(StatusCodes.Status200OK, "The Role with the specified unique identifier.", typeof(EcfRole))]
        [SwaggerResponse(StatusCodes.Status304NotModified, "The Role has not changed since the date given in the If-Modified-Since HTTP header.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A Role with the specified unique identifier could not be found.")]
        public async Task<EcfRole> Get(int id)
        {
            return await db.EcfRole.FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
