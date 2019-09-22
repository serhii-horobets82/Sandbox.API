using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Globalization;
using System.Linq;


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
            var gen = new Random();
            var randomDay = DateTime.Now;
            randomDay.AddYears(-1 * gen.Next(10));
            randomDay.AddMonths(gen.Next(12));
            return new EmployeeSalary { Id = (index + 1), EmployeeId = emp.Id, Period = randomDay, Basic = gen.Next(50) * 100, Bonus = gen.Next(10) * 100, Archived = false };
        }
    }
}
