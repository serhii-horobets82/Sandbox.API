using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Evoflare.API.Auth.Models;
using Evoflare.API.Models;
using Evoflare.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Evoflare.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProfileController : BaseController
    {
        private readonly EvoflareDbContext appDbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMemoryCache memoryCache;

        private readonly IEmailSender emailSender;

        public ProfileController(UserManager<ApplicationUser> userManager, EvoflareDbContext appDbContext,
        IMemoryCache memoryCache, IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.appDbContext = appDbContext;
            this.memoryCache = memoryCache;
            this.emailSender = emailSender;
        }

        [HttpPost]
        public async Task<IActionResult> Confirm()
        {
            await emailSender.SendEmailAsync("goroserg@gmail.com", "e-mail confirmation", "Hello world");
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Claims()
        {
            var claims = await memoryCache.GetOrCreateAsync($"Claims:{User.GetUserId()}", (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                return Task.FromResult(User.GetUserClaims());
            });
            return new OkObjectResult(claims);
        }

        [HttpGet]
        public IActionResult Permissions()
        {
            return new OkObjectResult(User.GetUserPermissions());
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
                roles,
                employeeId = GetEmployeeId()
            });
        }
    }
}