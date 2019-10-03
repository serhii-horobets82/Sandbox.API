using System;
using System.Linq;
using Evoflare.API.Core.Permissions;
using Evoflare.API.Data;
using Evoflare.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Evoflare.API.Controllers
{
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
            IConfiguration configuration
        )
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
            DbInitializer.Seed(setupParams, context, serviceProvider, configuration);
            return Ok(setupParams);
        }

        [HttpPost("init-db")]
        [Authorize(Policy = PolicyTypes.ApiKeyPolicy)]
        public IActionResult SetupDB(SetupParams setupParams)
        {
            DbInitializer.Seed(setupParams, context, serviceProvider, configuration);
            return Ok(context.AppVersion.First());
        }

        [HttpPost("seed-360-feedback")]
        [Authorize(Policy = PolicyTypes.AdminPolicy.Crud)]
        public IActionResult Seed360FeedbackData()
        {
            SeedTestData.RemoveExisting360EvaluationData(context);
            SeedTestData.Seed_360EvaluationForAnalytics(context);
            return Ok();
        }
    }
}