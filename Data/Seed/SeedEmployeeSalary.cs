using System;
using System.Globalization;
using System.Linq;
using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Evoflare.API.Data
{
    public static partial class DbInitializer
    {

        public static bool SeedEmployeeSalary(EvoflareDbContext context)
        {
            if (context.EmployeeSalary.Any()) return false;
            IDbContextTransaction trans = null;

            if (true && context.Database.IsSqlServer())
            {
                trans = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [EmployeeSalary] ON");
            }

            var items = context.Employee.ToList().Select((emp, i) => GenerateSalary(i, emp));

            context.EmployeeSalary.AddRange(items);

            context.SaveChanges();

            if (true && context.Database.IsSqlServer())
            {
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [EmployeeSalary] OFF");
                trans.Commit();
            }

            return true;
        }

        private static EmployeeSalary GenerateSalary(int index, Employee emp)
        {
            var random = new Random(DateTime.Now.Second);
            var randomDay = DateTime.Now.AddYears(-1 * random.Next(10)).AddMonths(random.Next(12));
            return new EmployeeSalary { Id = (index + 1), EmployeeId = emp.Id, Period = randomDay, Basic = random.Next(50) * 100, Bonus = random.Next(10) * 100, Archived = false };
        }
    }
}