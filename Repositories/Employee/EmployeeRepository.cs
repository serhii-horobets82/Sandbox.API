using Evoflare.API.Models;

namespace Evoflare.API.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EvoflareDbContext context) : base(context) { }
    }
}
