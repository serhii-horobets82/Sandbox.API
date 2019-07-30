using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evoflare.API.Auth.Models;
using Evoflare.API.Core.Permissions;
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
        private readonly EvoflareDbContext context;

        public UsersController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            EvoflareDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
        }

        [HttpGet]
        [Authorize(Policy = PolicyTypes.AdminPolicy.View)]
        public async Task<IActionResult> GetAsync()
        {
            var users = await context.Users.Include(e => e.IdNavigation).Where(e => e.LockoutEnabled).Select(c => new
            {
                id = c.Id,
                email = c.Email,
                firstName = c.FirstName,
                lastName = c.LastName,
                accessFailedCount = c.AccessFailedCount,
                gender = (int)c.Gender,
                age = c.Age,
                emloyeeId = c.IdNavigation.Id,
                emloyeeType = c.IdNavigation.EmployeeType.Type,
                hiringDate = c.IdNavigation.HiringDate
            }).ToListAsync();
            return new OkObjectResult(users);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = PolicyTypes.AdminPolicy.Crud)]
        public async Task<ActionResult> Delete(string id)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.LockoutEnabled = false;
            var result = await userManager.UpdateAsync(user);

            //var profile = await context.Profile.Include(e => e.Identity).FirstOrDefaultAsync(e => e.IdentityId == user.Id);
            //context.Profile.Remove(profile);
            //var employee = await context.Employee.Include(e => e.Users).FirstOrDefaultAsync(e => e.UserId == user.Id);
            //context.Employee.Remove(employee);
            //var result = await userManager.DeleteAsync(user);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, ApplicationUser userDto)
        {
            if (id != userDto.Id)
            {
                return BadRequest();
            }
            var user = await context.Users.FindAsync(id);
            context.Entry(user).State = EntityState.Modified;

            user.Gender = userDto.Gender;
            user.Age = userDto.Age;
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;

            await context.SaveChangesAsync();

            return Ok(user);
        }


        private async Task<bool> UserHasRole(string role)
        {
            var user = await userManager.GetUserAsync(User);
            IEnumerable<string> roles = await userManager.GetRolesAsync(user);

            return roles.Contains(role);
        }
    }
}
