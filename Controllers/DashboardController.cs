using System;
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
    [ApiController]
    [Authorize(Policy = "ApiUser", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]")]
    public class DashboardController : ControllerBase
    {
        private readonly ClaimsPrincipal _caller;
        private readonly EvoflareDbContext _appDbContext;

        public DashboardController(UserManager<ApplicationUser> userManager, EvoflareDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _caller = httpContextAccessor.HttpContext.User;
            _appDbContext = appDbContext;
        }

        // GET api/dashboard/home
        [HttpGet]
        public async Task<IActionResult> Home()
        {
            // retrieve the user info
            var userId = _caller.Claims.Single(c => c.Type == "id");
            var profile = await _appDbContext.Profile.Include(c => c.Identity).SingleAsync(c => c.Identity.Id == userId.Value);

            return new OkObjectResult(new
            {
                Message = "This is secure API and user data!",
                profile.Identity.FirstName,
                profile.Identity.LastName,
                profile.Identity.Gender,
                profile.Identity.Age,
                profile.Location,
                profile.Locale,
                profile.PictureUrl
            });
        }
    }
}
