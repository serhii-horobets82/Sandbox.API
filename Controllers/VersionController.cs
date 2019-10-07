using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Evoflare.API.Core.Models;
using Evoflare.API.Exceptions;
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
                var version = (await appVersionRepository.GetListAsync()).FirstOrDefault();
                if (version != null)
                    return Ok(version);
                throw new BadRequestException("Database is empty!");
            }
            catch (Exception ex)
            {
                // 42P01: relation "core.AppVersion" does not exist - Database empty
                if ((ex is PostgresException pgEx && pgEx.SqlState == "42P01") ||
                    (ex is SqlException sqlEx && sqlEx.Number == 4060))
                {
                    throw new BadRequestException("Database doesn't exist or is empty!");
                }
                throw;
            }
        }

        [HttpGet("env")]
        public IActionResult GetEnvVariables()
        {
            return Ok(System.Environment.GetEnvironmentVariables());
        }
    }
}