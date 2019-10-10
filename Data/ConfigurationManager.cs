using System.Collections.Generic;
using Evoflare.API.Configuration;
using Microsoft.Extensions.Configuration;

namespace Evoflare.API.Data
{
    public class ConfigurationManager
    {
        public static void Init(IConfiguration configuration)
        {
            Configuration = configuration;
            AppSettings = configuration.GetSection("AppSettings").Get<AppSettings>();
            GlobalSettings = configuration.GetSection("GlobalSettings").Get<GlobalSettings>();
            DatabaseInstances = configuration.GetSection("DatabaseInstances").Get<List<DatabaseInstance>>();
            // Add primary DB 
            DatabaseInstances.Insert(0,
                new DatabaseInstance
                {
                    Id = "Primary",
                        Type = AppSettings.DatabaseType,
                        ConnectionStringEnvironmentName = (AppSettings.DatabaseType == DatabaseType.POSTGRES) ? "DATABASE_URL" : ""
                });
        }

        public static IConfiguration Configuration { get; set; }
        public static AppSettings AppSettings { get; set; }
        public static GlobalSettings GlobalSettings { get; set; }
        public static List<DatabaseInstance> DatabaseInstances { get; set; }
    }
}