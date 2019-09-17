using System;
using System.Collections.Generic;
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

                /* 
                // fill one year salary plan (+/- 6 monthes from today)
                var start = DateTime.Now.AddMonths(-6);
                var salaryList = new List<ViewModels.EmployeeSalary>();
                var lastSalaryRecord = source.EmployeeSalary.FirstOrDefault(e => e.Period < DateTime.Now);
                for (int i = 0; i < 12; i++)
                {
                    salaryList.Add(new ViewModels.EmployeeSalary
                    {
                        Id = lastSalaryRecord.Id,
                        Period = start.AddMonths(1),
                        Basic = lastSalaryRecord.Basic,
                        Bonus = lastSalaryRecord.Bonus
                    });
                }
                destination.Salary = salaryList;
                */
            }
        }
    }
}
