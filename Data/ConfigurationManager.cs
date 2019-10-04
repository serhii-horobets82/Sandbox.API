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
            DatabaseInstances = configuration.GetSection("DatabaseInstances").Get<List<DatabaseInstance>>();
            AppSettings = configuration.GetSection("AppSettings").Get<AppSettings>();
        }

        public static IConfiguration Configuration { get; set; }
        public static AppSettings AppSettings { get; set; }
        public static List<DatabaseInstance> DatabaseInstances { get; set; }
    }
}