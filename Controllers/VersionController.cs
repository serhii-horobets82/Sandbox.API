using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Evoflare.API.Core.Models;
using Evoflare.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Swashbuckle.AspNetCore.Annotations;

namespace Evoflare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class VersionController : BaseController
    {
        private readonly IRepository<AppVersion> appVersionRepository;

        public VersionController(IRepository<AppVersion> appVersionRepository)
        {
            this.appVersionRepository = appVersionRepository;
        }

        [HttpGet(Name = "GetAppVersion")]
        [SwaggerResponse(StatusCodes.Status200OK, "Version of application in database", typeof(AppVersion))]
        public async Task<IActionResult> GetAppVersion()
        {
            try
            {
                var version = await appVersionRepository.GetListAsync();
                return Ok(version);
            }
            catch (Exception ex)
            {
                // 42P01: relation "core.AppVersion" does not exist - Database empty
                if ((ex is PostgresException pgEx && pgEx.SqlState == "42P01") ||
                    (ex is SqlException sqlEx && sqlEx.Number == 2714))
                {
                    return BadRequest("Database is empty!");
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