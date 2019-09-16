using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Evoflare.API.Models;
using Evoflare.API.Repositories;
using Boxed.Mapping;

namespace Evoflare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController : BaseController
    {
        private readonly EvoflareDbContext _context;
        private readonly IMapper<Employee, ViewModels.Employee> _employeeMapper;
        private readonly IRepository<Employee> _employeeRepository;
        public SalaryController(IRepository<Employee> employeeRepository, EvoflareDbContext context, IMapper<Employee, ViewModels.Employee> employeeMapper)
        {
            _employeeRepository = employeeRepository;
            _context = context;
            _employeeMapper = employeeMapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ViewModels.Employee>> GetAllEmployee()
        {
            var employeeId = GetEmployeeId();
            // get project only where I am a manager.
            // TODO: manager could be on the Project level, also on Team level. Should filter teams if the last case.
            var projects = await _context.Project
                .Where(p => p.EmployeeRelations.Any(r => r.ManagerId == employeeId))
                .Include(_ => _.Team)
                    .ThenInclude(t => t.EmployeeRelations)
                .ToListAsync();

            var employees = _context.Employee
                .Include(e => e.EmployeeSalary)
                .Include(e => e.EmployeeType);
            return _employeeMapper.MapList(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
                return InvokeHttp404();

            return Ok(employee);
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> Edit(int id, ViewModels.EmployeeSalary vmSalary)
        {
            var salary = await _context.EmployeeSalary.FindAsync(vmSalary.Id); 

            if (salary == null)
                return InvokeHttp404();

            return Ok(salary);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<EmployeeSalary>> Post(int id, ViewModels.EmployeeSalary vmSalary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var salary = new EmployeeSalary {
                EmployeeId = id,
                Basic = vmSalary.Basic,
                Bonus = vmSalary.Bonus,
                Period = vmSalary.Period
            };
             _context.EmployeeSalary.Add(salary);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSalary", new { id = salary.Id }, salary);
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
