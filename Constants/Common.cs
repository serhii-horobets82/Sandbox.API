namespace Evoflare.API.Constants
{

    public class CustomHeaders
    {
        public const string ServerId = "X-Server-ID";
        public const string ApiKey = "X-Api-Key";
    }

    public class DatabaseOptions
    {
        public const string CoreSchemaName = "core";
        public const string SecuritySchemaName = "security";
        public const string DefaultConnectionName = "DefaultConnection";
        public const string MigrationTableName = "Migrations";
        public const string MigrationTableScheme = "core";
        public const int CommandTimeout = 300;
    }

    /// <summary>
    ///     Codes for the representation of human sexes is an international standard
    ///     ISO/IEC 5218
    /// </summary>
    public enum Gender
    {
        Unknown = 0,
        Male = 1,
        Female = 2
    }

    /// <summary>
    /// Roles
    /// </summary>
    public class Roles
    {
        public const string SysAdmin = "SysAdmin";
        public const string Admin = "Admin";

        public const string Manager = "Manager";
        public const string ChiefManager = "ChiefManager";
        public const string HR = "HR";
        public const string ChiefHR = "ChiefHR";
        public const string User = "User";
    }

    public static class JwtClaimIdentifiers
    {
        public const string DatabaseId = "dbId";
        public const string EmployeeId = "empId";
        public const string OrganizationId = "orgId";
        public const string OrganizationName = "orgName";
        public const string Email = "email";
        public const string Rol = "rol";
        public const string Id = "id";
    }

    public static class JwtClaims
    {
        public const string ApiAccess = "api_access";
    }

    public class CustomClaims
    {
        public const string Permission = "permission";

    }
}