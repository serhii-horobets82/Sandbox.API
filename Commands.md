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

Use **VS Code** task *"app: Scaffold database"* or  

```cmd
dotnet ef dbcontext scaffold "Server=localhost,14330;Database=EvoflareDB;User ID=sa;Password=DatgE66VbHy7" Microsoft.EntityFrameworkCore.SqlServer -c EvoflareDbContext -o Models -v --context-dir Data --schema dbo --data-annotations --force"
```

### Connect to DB (MSSQL)

```cmd
sqlcmd -S localhost,14330 -d EvoflareDB -U sa -PDatgE66VbHy7
```

### Connect to DB (POSTGRES)

```cmd
psql -h ec2-174-129-41-127.compute-1.amazonaws.com -p 5432 -U wktwispbsmilsd -d d86q4skiq33jf4
```

### Generate script from DB

```cmd
mssql-scripter -S localhost,14330 -d EvoflareDB -U sa -PDatgE66VbHy7 --schema-and-data --exclude-headers --include-schemas dbo  > init.sql
```

### Backup database

```cmd
Backup-SqlDatabase -ServerInstance localhost,14330 -Database EvoflareDB -BackupAction Database
```

## Postgress

```sql
psql -h localhost -p 54320 -U postgres -d postgres
```
