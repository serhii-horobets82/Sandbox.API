# Commands

## Database related

### Context info

```cmd
dotnet ef dbcontext info --context EvoflareDbContext
```

### Create migration

```cmd
dotnet ef migrations add InitialCreate --context EvoflareDbContext
```

### Update database with migration

```cmd
dotnet ef database update --context EvoflareDbContext
```

### Remove migration

```cmd
dotnet ef database remove --context EvoflareDbContext
```

### Generate models (scaffold) from DB (power shell)

```cmd
Scaffold-DbContext "Server=localhost,14330;Database=EvoflareDB;User ID=sa;Password=DatgE66VbHy7" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force -Schema dbo -Context "EvoflareDbContext"
```

### Generate models from DB (command line)

Use **VS Code** task *"Scaffold database (main)"* or  

```cmd
dotnet ef dbcontext scaffold "Server=localhost,14330;Database=EvoflareDB;User ID=sa;Password=DatgE66VbHy7" Microsoft.EntityFrameworkCore.SqlServer -c EvoflareDbContext -o Models -v --context-dir Data --schema dbo --data-annotations --force"
```

### Connect to DB

```cmd
sqlcmd -S localhost,14330 -d EvoflareDB -U sa -PDatgE66VbHy7
```

### Generate script from DB

```cmd
mssql-scripter -S localhost,14330 -d EvoflareDB -U sa -PDatgE66VbHy7 --schema-and-data --exclude-headers --include-schemas dbo  > init.sql
```

### Backup database

```cmd
Backup-SqlDatabase -ServerInstance localhost,14330 -Database EvoflareDB -BackupAction Database
```
