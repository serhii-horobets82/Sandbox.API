using System.Collections.Generic;
using System.Linq;

namespace Evoflare.API.Configuration
{

    public class DatabaseSettings
    {
        public DBInstance[] DBInstances { get; set; }
    }

    public class DBInstance
    {
        public string Id { get; set; }
        // Connection string name from list of ConnectionStrings (appsettings)
        public string ConnStrSettingsName { get; set; } = "DefaultConnection";
        // Environment variable name with connection string 
        public string ConnStrEnvVarName { get; set; } = "DATABASE_URL";
        public DataBaseType Type { get; set; } = DataBaseType.MSSQL;
        public int CommandTimeout { get; set; } = 300;
    }
}