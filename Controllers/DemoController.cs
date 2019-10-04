using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evoflare.API.Auth.Identity;
using Evoflare.API.Auth.Models;
using Evoflare.API.Data;
using Evoflare.API.Models;
using Evoflare.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Evoflare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class DemoController : ControllerBase
    {
        private readonly IRepository<Employee> employeeRepository;
        private readonly IRepository<ApplicationUser> userRepository;
        private readonly IUserManager userManager;
        public DemoController(IRepository<Employee> employeeRepository, IUserManager userManager, IRepository<ApplicationUser> userRepository)
        {
            this.employeeRepository = employeeRepository;
            this.userRepository = userRepository;
            this.userManager = userManager;
        }

        [HttpGet("users")]
        public IActionResult GetDemoUsers()
        {
            var users = userManager.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).Where(e => e.LockoutEnabled).ToList();
            Func<ApplicationUser, IList<string>> getRoles = (ApplicationUser user) =>
            {
                return userManager.GetRolesAsync(user).Result;
            };
            var result = users.Select(e => new { Id = e.Id, Roles = getRoles(e), Name = e.FirstName, Surname = e.LastName, Email = e.Email, Password = DbInitializer.DefaultPassword });
            return new OkObjectResult(result);
        }

        [HttpGet("databases")]
        public IActionResult GetDatabases()
        {
            return Ok(ConfigurationManager.DatabaseInstances);
        }
    }
}