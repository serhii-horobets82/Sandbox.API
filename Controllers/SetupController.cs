using System;
using Evoflare.API.Core.Permissions;
using Evoflare.API.Data;
using Evoflare.API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using static Evoflare.API.PoliciesExtensions;

namespace Evoflare.API.Controllers
{
    [Authorize(Policy = nameof(AdminRequirement), AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class SetupController : ControllerBase
    {
        private readonly EvoflareDbContext context;
        private readonly IServiceProvider serviceProvider;
        private readonly IConfiguration configuration;

        public SetupController(
            EvoflareDbContext context,
            IServiceProvider serviceProvider,
            IConfiguration configuration)
        {
            this.context = context;
            this.serviceProvider = serviceProvider;
            this.configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var configurations = new[] {
                 new {Id = "config01", Schema = "Typical functional organization", Users = 10 },
                 new {Id = "config02", Schema = "Typical divisional organization", Users = 25 },
                 new {Id = "config03", Schema = "Typical matrix organization", Users = 15 }
            };
            return new OkObjectResult(configurations);
        }


        [HttpPost]
        [Authorize(Policy = PolicyTypes.AdminPolicy.Crud)]
        public IActionResult RecreateDb()
        {
            DbInitializer.Initialize(serviceProvider, configuration, true);
            return Ok();
        }
    }
}
