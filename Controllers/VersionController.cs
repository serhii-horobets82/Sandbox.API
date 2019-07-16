using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Evoflare.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace Evoflare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        private readonly EvoflareDbContext db;

        public VersionController(EvoflareDbContext db)
        {
            this.db = db;
        }

        [HttpGet(Name = "GetAppVersion")]
        [SwaggerResponse(StatusCodes.Status200OK, "Version of application in database", typeof(string))]
        public async Task<AppVersion> GetAppVersion()
        {
            return await db.AppVersion.FirstOrDefaultAsync();
        }
    }
}
