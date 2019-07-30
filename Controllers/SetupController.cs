using Evoflare.API.Core.Permissions;
using Evoflare.API.Data;
using Evoflare.API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Evoflare.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class SetupController : ControllerBase
    {
        private readonly EvoflareDbContext context;

        public SetupController(
            EvoflareDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        [Authorize(Policy = PolicyTypes.AdminPolicy.Crud)]
        public IActionResult RecreateDb()
        {
            DbInitializer.RecreateDatabase(context, 30000);
            return Ok();
        }
    }
}
