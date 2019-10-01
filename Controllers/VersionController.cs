using System.Threading.Tasks;
using Evoflare.API.Models;
using Evoflare.API.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using Evoflare.API.Data;
using Microsoft.AspNetCore.Authorization;

namespace Evoflare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class VersionController : BaseController
    {
        private readonly EvoflareDbContext appContext;
        private readonly IDbContextFactory contextFactory;

        public VersionController(EvoflareDbContext appContext, IDbContextFactory contextFactory)
        {
            this.appContext = appContext;
            this.contextFactory = contextFactory;
        }

        [HttpGet(Name = "GetAppVersion")]
        [SwaggerResponse(StatusCodes.Status200OK, "Version of application in database", typeof(string))]
        public async Task<AppVersion> GetAppVersion()
        {
            var context = contextFactory.CreateFromHeaders();
            return await context.AppVersion.FirstOrDefaultAsync();
        }
    }
}
