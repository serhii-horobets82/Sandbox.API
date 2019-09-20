using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using Npgsql.EntityFrameworkCore.PostgreSQL.Migrations;

namespace Evoflare.API.Data
{
    public class BaseMigrationsSqlGenerator : NpgsqlMigrationsSqlGenerator
    {
        public BaseMigrationsSqlGenerator(
            MigrationsSqlGeneratorDependencies dependencies,
            INpgsqlOptions npgsqlOptions) : base(dependencies, npgsqlOptions)
        {
        }

        protected override void Generate(
            MigrationOperation operation,
            IModel model,
            MigrationCommandListBuilder builder)
        {
            if (operation is CreateTableOperation op)
            {
                op.Columns
                    .Where(item => item.ColumnType == "datetime")
                    .ToList()
                    .ForEach(e => e.ColumnType = "timestamp");
            }
            base.Generate(operation, model, builder);
        }
    }
}