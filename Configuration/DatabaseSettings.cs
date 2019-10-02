using System.Collections.Generic;
using System.Linq;
using Evoflare.API.Constants;

namespace Evoflare.API.Configuration
{
    public enum DataBaseType{
        MSSQL,
        POSTGRES
    }

    public class DatabaseSettings
    {
        public DBInstance[] DBInstances { get; set; }
    }
    

    public class DBInstance
    {
        public string Id { get; set; }
        // Connection string name from list of ConnectionStrings (appsettings)
        public string ConnectionStringName { get; set; } = DatabaseOptions.DefaultConnectionName;
        // Environment variable name with connection string 
        public string ConnectionStringEnvironmentName { get; set; }
        public DataBaseType Type { get; set; } = DataBaseType.MSSQL;
        public int CommandTimeout { get; set; } = DatabaseOptions.CommandTimeout;
    }
}