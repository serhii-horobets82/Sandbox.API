using System.Threading.Tasks;
using Evoflare.API.Options;
using Evoflare.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Evoflare.API.Controllers
{
    [Route("api/[controller]")]
    public class EnvironmentController : BaseController
    {
        private readonly ThirdPartySetting _config;
        private readonly IEnvironmentManager _environment;
        private readonly ILogger<EnvironmentController> _logger;

        public EnvironmentController(IEnvironmentManager environment, IOptions<ThirdPartySetting> config, ILogger<EnvironmentController> logger)
        {
            _config = config.Value;
            _environment = environment;
            _logger = logger;
        }

        [HttpPost("/")]
        [AllowAnonymous]
        public async Task<ActionResult> Create([FromHeader] string clientId, [FromBody] EnvironmentDefinition payload)
        {
            if (!string.Equals(
                clientId,
                _config.Landing.Key.ToString(),
                System.StringComparison.InvariantCultureIgnoreCase))
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }

            try
            {
                await _environment.StartNewEnvironment(payload);
            }
            catch (System.Exception e)
            {
                _logger.LogError($"{nameof(_environment.StartNewEnvironment)} fails", e);
                throw;
            }

            return Ok();
        }
    }

    public class EnvironmentDefinition
    {
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public int OrganizationType { get; set; }
        public string UserName { get; set; }
    }
}
