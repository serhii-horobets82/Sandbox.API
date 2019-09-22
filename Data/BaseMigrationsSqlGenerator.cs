using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
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
            if (operation is CreateIndexOperation indexOp)
            {
                if (indexOp.Filter != null)
                    indexOp.Filter = indexOp.Filter.Replace("[", "\"").Replace("]", "\"");
            }

            if (operation is CreateTableOperation tableOp)
            {
                tableOp.Columns
                .Where(e => e.FindAnnotation("SqlServer:ValueGenerationStrategy") != null)
                .ToList()
                .ForEach(e =>
                {
                    e.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);
                });
                tableOp.Columns
                    .Where(item => item.ColumnType == "datetime")
                    .ToList()
                    .ForEach(e =>
                    {
                        e.ColumnType = "timestamp";
                        if (e.DefaultValueSql != null)
                            e.DefaultValueSql = e.DefaultValueSql.Replace("getutcdate", "now");
                    });
            }
            base.Generate(operation, model, builder);
        }
    }
}