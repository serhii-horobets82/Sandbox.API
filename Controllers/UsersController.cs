using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evoflare.API.Auth.Models;
using Evoflare.API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Evoflare.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationDbContext context;

        public UsersController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<JObject>> GetAsync()
        {
            var hasPermission = await UserHasRole(Constants.Roles.Admin);
            if (!hasPermission) return null;

            return await context.ApplicationUsers.Select(c => new JObject
            {
                { "id",  c.Id },
                { "email",  c.Email },
                { "firstName",  c.FirstName },
                { "lastName",  c.LastName }
            }).ToListAsync();
        }

        private async Task<bool> UserHasRole(string role)
        {
            var user = await userManager.GetUserAsync(User);
            IEnumerable<string> roles = await userManager.GetRolesAsync(user);

            return roles.Contains(role);
        }

    }
}
