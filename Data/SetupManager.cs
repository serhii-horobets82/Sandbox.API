using System;
using System.Globalization;
using Evoflare.API.Models;

namespace Evoflare.API.Data
{

    public class SetupConfig
    {
        public PredefinedConfig Id { get; set; }
        public string Description { get; set; }
    }

    public class SetupParams
    {
        public PredefinedConfig Id { get; set; }
        public string AdminEmail { get; set; }
        public string OrganizationName { get; set; }
        public string DefaultPassword { get; set; }
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