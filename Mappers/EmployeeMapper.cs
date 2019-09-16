using System;
using System.Linq;
using Boxed.Mapping;
using Evoflare.API.Models;

namespace Evoflare.API.Mappers
{
    public class EmployeeMapper : IMapper<Employee, ViewModels.Employee>
    {
        public void Map(Employee source, ViewModels.Employee destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            destination.Id = source.Id;
            destination.Name = source.Name;
            destination.Surname = source.Surname;
            destination.HiringDate = source.HiringDate;
            if (source.EmployeeType != null)
            {
                destination.EmployeeType = new ViewModels.EmployeeType
                {
                    Id = source.EmployeeType.Id,
                    Type = source.EmployeeType.Type
                };
            }

            if (source.EmployeeSalary != null)
            {
                destination.Salary = source.EmployeeSalary.Select(e => new ViewModels.EmployeeSalary
                {
                    Id = e.Id,
                    Period = e.Period,
                    Basic = e.Basic,
                    Bonus = e.Bonus
                });
            }
        }
    }
}
