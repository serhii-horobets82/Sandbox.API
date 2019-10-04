using Evoflare.API.Constants;

namespace Evoflare.API.Configuration
{
    public enum DatabaseType
    {
        MSSQL,
        POSTGRES
    }

    public class DatabaseInstances
    {
        // Avaliable database instances for current running  API 
        public DatabaseInstance[] DBInstances { get; set; }
    }

    public class DatabaseInstance
    {
        // Unique instance id (used also for header X-Server-ID)
        public string Id { get; set; }
        // Organization/customer name
        public string Name { get; set; }
        // Used for domain-based balancer ( [prefix].evoflare.com )
        public string DomainPrefix { get; set; }
        // Connection string name from list of ConnectionStrings (appsettings)
        public string ConnectionStringName { get; set; } = DatabaseOptions.DefaultConnectionName;
        // Environment variable name with connection string 
        public string ConnectionStringEnvironmentName { get; set; }
        public DatabaseType Type { get; set; } = DatabaseType.MSSQL;
        public int CommandTimeout { get; set; } = DatabaseOptions.CommandTimeout;
    }
}