using System.Collections.Generic;
using System.Threading.Tasks;
using Evoflare.API.Auth.Models;
using Evoflare.API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        private readonly UserManager<ApplicationUser> userManager;

        public ProfileController(UserManager<ApplicationUser> userManager, ApplicationDbContext appDbContext)
        {
            this.userManager = userManager;
            this.appDbContext = appDbContext;
        }

        // GET api/profile/me
        [HttpGet]
        public async Task<IActionResult> Me()
        {
            var user = await userManager.GetUserAsync(User);
            var profile = await appDbContext.Profile
                .Include(c => c.Identity)
                .SingleAsync(c => c.Identity.Id == user.Id);

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