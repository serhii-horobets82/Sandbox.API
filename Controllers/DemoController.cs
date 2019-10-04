using System.Linq;
using System.Threading.Tasks;
using Evoflare.API.Data;
using Evoflare.API.Models;
using Evoflare.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Evoflare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class DemoController : ControllerBase
    {
        private readonly IRepository<Employee> _employeeRepository;
        public DemoController(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetDemoUsers()
        {
            var users = await _employeeRepository.GetListAsync(e => e.EmployeeType, e => e.Users);
            var result = users.Where(e => e.Users.LockoutEnabled).Select(e => new { Id = e.Id, Name = e.Name, Surname = e.Surname, Email = e.Users.Email, Type = e.EmployeeType.Id, TypeName = e.EmployeeType.Type, Password = DbInitializer.DefaultPassword });
            return new OkObjectResult(result);
        }

        [HttpGet("databases")]
        public IActionResult GetDatabases()
        {
            return Ok(ConfigurationManager.DatabaseInstances);
        }
    }
}