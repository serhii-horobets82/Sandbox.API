using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Evoflare.API.Data
{
    public class ExportDataFromDB
    {

        public class CustomFormatter : IFormatProvider, ICustomFormatter
        {
            public string Format(string format, object arg, IFormatProvider formatProvider)
            {
                if (arg is null)
                {
                    return "null";
                }
                else if (arg is String)
                {
                    return $"\"{arg}\"".Replace("\n", "\\n");
                }
                else if (arg is bool)
                {
                    return arg.ToString().ToLower();
                }
                else if (arg is DateTime)
                {
                    return $"DateTime.ParseExact(\"{((DateTime)arg).ToString("O")}\", \"O\", CultureInfo.InvariantCulture)";
                }
                // format everything else normally
                if (arg is IFormattable)
                    return ((IFormattable)arg).ToString(format, formatProvider);
                else
                    return arg.ToString();
            }

            public object GetFormat(Type formatType)
            {
                return (formatType == typeof(ICustomFormatter)) ? this : null;
            }
        }

        private static void GenerateSeedClass(EvoflareDbContext context, string tmplSeedClass, string currDirectory)
        {

            if (context.Employee.Any())
            {
                var tableName = "Employee";
                var sb = new StringBuilder();

                // list of non-virtual properties
                var props = typeof(Employee).GetProperties().Where(e => !e.GetAccessors()[0].IsVirtual);
                // construct string with formatting like "fieldName1 = {field1Index}, fieldName2 = {field2Index} ... "
                var format = String.Join(", ", props.Select((x, i) => $"{x.Name} = {{{i}}}"));

                foreach (var item in context.Employee)
                {
                    sb.Append($"\t\t\t\tnew {tableName} {{");

                    // list of values in same order
                    var values = props.Select(x => x.GetValue(item)).ToArray();
                    // fill format string with values 
                    sb.Append(string.Format(new CustomFormatter(), format, values));

                    //sb.Append(string.Format(new CustomFormatter(), formatString.ToString(), fields.Values.Select(x => x.GetValue(item)).ToArray()));
                    sb.Append(" },\n");
                }
                var content = string.Format(tmplSeedClass, tableName, sb.ToString());

                File.WriteAllText($"{currDirectory}\\Data\\Seed\\Seed{tableName}.cs", content);
            }
        }


        public static void Run(EvoflareDbContext context)
        {
            // get solution root folder
            var currDirectory = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"));
            var tmplSeedClass = File.ReadAllText($"{currDirectory}\\Data\\SeedTableWithIdentity.tmpl");

            //var exportList = new string [] { "Organization", "EmployeeType", "Employee", "EmployeeEvaluation", "EmployeeRelations" };
            var excludeList = new string[] { "ActivityLog", "AppVersion", "ApplicationRole", "ApplicationUser", "UserProfile", "IdentityRoleClaim`1", "IdentityUserClaim`1", "IdentityUserLogin`1", "IdentityUserRole`1", "IdentityUserToken`1" };
            // list of all context DbSet
            var entityTypes = context.Model.GetEntityTypes().Where(i => !excludeList.Contains(i.ClrType.Name));

            foreach (var entityType in entityTypes)
            {
                // get DbSet by type
                var dbSetType = entityType.ClrType;
                var tableName = dbSetType.Name;
                var sqlTableName = tableName;
                // remove underscore symbol (_360* tables)
                if (tableName.StartsWith("_"))
                    sqlTableName = tableName.Substring(1);
                var dbSet = context.Set(dbSetType);

                var sb = new StringBuilder();

                // list of non-virtual properties
                var props = dbSetType.GetProperties().Where(e => !e.GetAccessors()[0].IsVirtual);
                // construct string with formatting like "fieldName1 = {field1Index}, fieldName2 = {field2Index} ... "
                var format = String.Join(", ", props.Select((x, i) => $"{x.Name} = {{{i}}}"));

                foreach (var dbRow in dbSet)
                {
                    sb.Append($"\t\t\t\tnew {tableName} {{");

                    // list of values in same order
                    var values = props.Select(x => x.GetValue(dbRow)).ToArray();
                    // fill format string with values 
                    sb.Append(string.Format(new CustomFormatter(), format, values));

                    sb.Append(" },\n");
                }
                if (sb.Length > 0)
                {
                    var content = string.Format(tmplSeedClass, tableName, sqlTableName, sb.ToString());
                    File.WriteAllText($"{currDirectory}\\Data\\Seed\\Seed{tableName}.cs", content);
                }
            }
        }
    }
}
