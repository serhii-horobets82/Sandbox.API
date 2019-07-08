
### Context info
```
dotnet ef dbcontext info --context TechnicalEvaluationContext
```

### Generate models from DB (power shell)
```
Scaffold-DbContext "Server=localhost,14330;Database=EvoflareDB;User ID=sa;Password=DatgE66VbHy7" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force -Schema dbo -Context "TechnicalEvaluationContext"
```

### Generate models from DB (command line)
```
dotnet ef dbcontext scaffold "Server=localhost,14330;Database=EvoflareDB;User Id=sa;Password=DatgE66VbHy7" Microsoft.EntityFrameworkCore.SqlServer -o Models --force --schema dbo
```
### Connect to DB
```
sqlcmd -S localhost,14330 -d EvoflareDB -U sa -PDatgE66VbHy7
```

### Generate script from DB
```
mssql-scripter -S localhost,14330 -d EvoflareDB -U sa -PDatgE66VbHy7 --schema-and-data --exclude-headers --include-schemas dbo  > init.sql
```