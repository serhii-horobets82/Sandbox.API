using System.Threading.Tasks;
using Evoflare.API.Models;
using Evoflare.API.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Evoflare.API.Repositories;
using System.Collections.Generic;
using Evoflare.API.Data;

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
        public async Task<IActionResult> GetGemoUsers()
        {
            var users = await _employeeRepository.GetListAsync(e => e.EmployeeType, e => e.Users);
            var result = users.Select(e => new { Id = e.Id, Name = e.Name, Surname = e.Surname, Email = e.Users.Email, Type = e.EmployeeType.Id, TypeName = e.EmployeeType.Type, Password = DbInitializer.DefaultPassword });
            return new OkObjectResult(result);
        }
    }
}
