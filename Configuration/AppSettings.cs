namespace Evoflare.API.Configuration
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public bool RecreateDbOnStart { get; set; }
        public bool SeedDatabase { get; set; }
        public DatabaseType DatabaseType { get; set; } = DatabaseType.MSSQL;
    }
}
