using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Evoflare.API.Models;
using Evoflare.API.Repositories;

namespace Evoflare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController : BaseController
    {
        //private readonly IEmployeeRepository _employeeRepository;

        private readonly IRepository<Employee> _employeeRepository;
        public SalaryController(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> GetAllEmployee()
        {
            return await _employeeRepository.GetListAsync(e => e.EmployeeType);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
                return InvokeHttp404();

            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> Post(Employee employee)
        {
            await _employeeRepository.InsertAsync(employee);

            return CreatedAtAction("employee", new { id = employee.Id }, employee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
                return InvokeHttp404();

            await _employeeRepository.DeleteAsync(employee);
            return Ok();
        }
    }
}
