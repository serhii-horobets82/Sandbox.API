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
            return new OkObjectResult(SetupManager.Configurations);
        }


        [HttpPost]
        [Authorize(Policy = PolicyTypes.AdminPolicy.Crud)]
        public IActionResult ResetDB(SetupParams setupParams)
        {
            //DbInitializer.Initialize(serviceProvider, configuration, true);
            DbInitializer.Seed(setupParams, context, serviceProvider, configuration);
            return Ok(setupParams);
        }
    }
}
