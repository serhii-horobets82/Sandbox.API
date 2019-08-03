using System.Threading.Tasks;
using Evoflare.API.Models;
using Evoflare.API.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;

namespace Evoflare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        private readonly EvoflareDbContext appContext;

        public VersionController(EvoflareDbContext appContext)
        {
            this.appContext = appContext;
        }

        [HttpGet(Name = "GetAppVersion")]
        [SwaggerResponse(StatusCodes.Status200OK, "Version of application in database", typeof(string))]
        public async Task<AppVersion> GetAppVersion()
        {
            return await appContext.AppVersion.FirstOrDefaultAsync();
        }

        [HttpGet("demousers")]
        public IActionResult GetDemoUser()
        {
            appContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var employees = appContext.Employee.Include(c => c.Users).ToArray();
            var result = employees.Select(e => new { Title = e.NameTemp, UserName = e.Users.Email });
            return new OkObjectResult(result);
        }
    }
}
