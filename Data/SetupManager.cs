namespace Evoflare.API.Data
{
    public class SetupConfig
    {
        public PredefinedConfig Id { get; set; }
        public string Description { get; set; }
    }

    public class RandomData
    {
        public int Users { get; set; } = 10;
        public int Managers { get; set; } = 3;
        public int HRs { get; set; } = 2;
        public int Admins { get; set; } = 1;
    }

    public class SetupParams
    {
        public PredefinedConfig Id { get; set; }
        public string AdminEmail { get; set; }
        public string OrganizationName { get; set; } = "Evoflare Corp";
        public string OrganizationDomain { get; set; } = "evoflare.com";
        public bool ForceRecreate { get; set; } = false;
        public bool IsRestartAfter { get; set; } = false;

        public RandomData RandomData { get; set; } = null;
    }

    public enum PredefinedConfig
    {
        DefaultConfig = 0,
        FunctionalStructureConfig = 1,
        DivisionalStructureConfig = 2,
        MatrixStructureConfig = 3
    }

    public class SetupManager
    {
        public static SetupConfig[] Configurations = new SetupConfig[]
        {
            new SetupConfig { Id = PredefinedConfig.DefaultConfig, Description = "Default config" },
            new SetupConfig { Id = PredefinedConfig.FunctionalStructureConfig, Description = "Functional organization" },
            new SetupConfig { Id = PredefinedConfig.DivisionalStructureConfig, Description = "Divisional organization" },
            new SetupConfig { Id = PredefinedConfig.MatrixStructureConfig, Description = "Matrix organization" }
        };
    }
}