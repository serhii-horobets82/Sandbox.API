using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using Evoflare.API.Data;
using Microsoft.AspNetCore.Authorization;
using System;
using Npgsql;
using System.Data.SqlClient;
using Evoflare.API.Core.Models;

namespace Evoflare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class VersionController : BaseController
    {
        private readonly IDbContextFactory contextFactory;

        public VersionController(IDbContextFactory contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        [HttpGet(Name = "GetAppVersion")]
        [SwaggerResponse(StatusCodes.Status200OK, "Version of application in database", typeof(AppVersion))]
        public async Task<IActionResult> GetAppVersion()
        {
            var context = contextFactory.CreateFromHeaders();
            try
            {
                var version = await context.AppVersion.FirstOrDefaultAsync();
                return Ok(version);
            }
            catch (Exception ex)
            {
                // 42P01: relation "core.AppVersion" does not exist - Database empty
                if ((ex is PostgresException pgEx && pgEx.SqlState == "42P01") ||
                   (ex is SqlException sqlEx && sqlEx.Number == 2714))
                {
                    return BadRequest("Database is empty!");
                    //     var setupParams = new SetupParams
                    //     {
                    //         Id = PredefinedConfig.DefaultConfig,
                    //         AdminEmail = "xxx@evoflare.com",
                    //         OrganizationName = "xxxx",
                    //         DefaultPassword = "qwerty",
                    //     };
                    //     DbInitializer.Seed(setupParams, context, serviceProvider, configuration);
                    //     var version = await context.AppVersion.FirstOrDefaultAsync();
                    //     return Ok(version);
                }
                return BadRequest(ex);
            }
        }

        [HttpGet("env")]
        public IActionResult GetEnvVariables()
        {
            return Ok(System.Environment.GetEnvironmentVariables());
        }
    }
}