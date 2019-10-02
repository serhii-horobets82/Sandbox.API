using System;
using System.Collections.Generic;
using System.Linq;
using Evoflare.API.Configuration;
using Evoflare.API.Constants;
using Evoflare.API.Core.Permissions;
using Evoflare.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Evoflare.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SetupController : ControllerBase
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IConfiguration configuration;
        private readonly IDbContextFactory contextFactory;

        public SetupController(
            IServiceProvider serviceProvider,
            IConfiguration configuration,
            IDbContextFactory contextFactory)
        {
            this.serviceProvider = serviceProvider;
            this.configuration = configuration;
            this.contextFactory = contextFactory;
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
            var context = contextFactory.CreateDefault();
            DbInitializer.Seed(setupParams, context, serviceProvider, configuration);
            return Ok(setupParams);
        }

        [HttpPost("init-db")]
        [Authorize(Policy = PolicyTypes.ApiKeyPolicy)]
        public IActionResult SetupDB(SetupParams setupParams)
        {
            var dbInstances = this.configuration.GetSection("DatabaseSettings").Get<List<DBInstance>>();

            if (dbInstances != null && Request.Headers.TryGetValue(CustomHeaders.ServerId, out var headerValues))
            {
                // get ID from request header
                var serverId = headerValues.First();
                var context = contextFactory.CreateFromHeaders();
                DbInitializer.Seed(setupParams, context, serviceProvider, configuration);
                return Ok(context.AppVersion.First());
            }
            return BadRequest();
        }

        [HttpPost("seed-360-feedback")]
        [Authorize(Policy = PolicyTypes.AdminPolicy.Crud)]
        public IActionResult Seed360FeedbackData()
        {
            var context = contextFactory.CreateDefault();
            SeedTestData.RemoveExisting360EvaluationData(context);
            SeedTestData.Seed_360EvaluationForAnalytics(context);
            return Ok();
        }
    }
}
