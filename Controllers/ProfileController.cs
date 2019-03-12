using System.Collections.Generic;
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
        private readonly ApplicationDbContext appDbContext;
        private readonly ClaimsPrincipal caller;
        private readonly UserManager<ApplicationUser> userManager;

        public ProfileController(UserManager<ApplicationUser> userManager, ApplicationDbContext appDbContext,
            IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            caller = httpContextAccessor.HttpContext.User;
            this.appDbContext = appDbContext;
        }

        // GET api/profile/me
        [HttpGet]
        public async Task<IActionResult> Me()
        {
            ApplicationUser user = await userManager.GetUserAsync(User);

            // retrieve the user info
            var userId = User.Claims.Single(c => c.Type == "id");

            var profile = await appDbContext.Profile
                .Include(c => c.Identity)
                .SingleAsync(c => c.Identity.Id == userId.Value);

            IEnumerable<string> roles = await userManager.GetRolesAsync(profile.Identity);

            return new OkObjectResult(new
            {
                profile.Identity.Id,
                profile.Identity.Email,
                profile.Identity.EmailConfirmed,
                profile.Identity.FirstName,
                profile.Identity.LastName,
                profile.Identity.FullName,
                profile.Identity.Age,
                profile.Identity.Gender,
                profile.PictureUrl,
                profile.Location,
                profile.Locale,
                roles
            });
        }
    }
}
