using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Evoflare.API.Auth.Models;
using Evoflare.API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Evoflare.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProfileController : ControllerBase
    {
        private readonly ClaimsPrincipal caller;
        private readonly ApplicationDbContext appDbContext;

        public ProfileController(UserManager<ApplicationUser> userManager, ApplicationDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
        {
            caller = httpContextAccessor.HttpContext.User;
            this.appDbContext = appDbContext;
        }

        // GET api/profile/me
        [HttpGet]
        public async Task<IActionResult> Me()
        {
            // retrieve the user info
            var userId = caller.Claims.Single(c => c.Type == "id");
            var role = await appDbContext.UserRoles.SingleOrDefaultAsync(c => c.UserId == userId.Value);

            return new OkObjectResult(new
            {
                userId,
                caller.Identity.IsAuthenticated,
                role
            });
        }
    }
}
