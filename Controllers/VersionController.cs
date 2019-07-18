using System.Threading.Tasks;
using Evoflare.API.Models;
using Evoflare.API.Core.Models;
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
        private readonly EvoflareDbContext appContext;

        public VersionController(EvoflareDbContext appContext)
        {
            this.appContext = appContext;
        }

        [HttpGet(Name = "GetAppVersion")]
        [SwaggerResponse(StatusCodes.Status200OK, "Version of application in database", typeof(string))]
        public async Task<AppVersion> GetAppVersion()
        {
            return await appContext.AppVersion.FirstOrDefaultAsync();
        }
    }
}
