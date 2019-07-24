using System.Threading.Tasks;
using Evoflare.API.Auth;
using Evoflare.API.Auth.Models;
using Evoflare.API.Helpers;
using Evoflare.API.Services;
using Evoflare.API.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Evoflare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IActivityLogService activityLogService;
        private readonly IJwtFactory jwtFactory;
        private readonly UserManager<ApplicationUser> userManager;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            IJwtFactory jwtFactory,
            IActivityLogService activityLogService)
        {
            this.userManager = userManager;
            this.jwtFactory = jwtFactory;
            this.activityLogService = activityLogService;
        }

        // POST api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody] CredentialsViewModel credentials)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await activityLogService.AddActivityAsync(credentials.UserName, "User request for auth token", 0);

            // try get user by name
            var user = await userManager.FindByNameAsync(credentials.UserName);
            // check the credentials
            if (user != null && await userManager.CheckPasswordAsync(user, credentials.Password))
            {
                // get all user roles
                var userRoles = await userManager.GetRolesAsync(user);
                var userClaims = await userManager.GetClaimsAsync(user);
                var authToken = await jwtFactory.GenerateAuthToken(user, userRoles, userClaims);
                return new OkObjectResult(authToken);
            }

            return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
        }
    }
}