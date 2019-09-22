
using System;
using System.Collections.Generic;

namespace Evoflare.API.ViewModels
{
    public class EmployeeType
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }

    public class EmployeeSalary
    {
        public int Id { get; set; }
        public DateTime Period { get; set; }
        public int Basic { get; set; }
        public int Bonus { get; set; }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? HiringDate { get; set; }
        public DateTime? PeDate { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public IEnumerable<EmployeeSalary> Salary { get; set; }
    }
}