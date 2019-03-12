namespace Evoflare.API.Constants
{
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
        public const string Admin = "Admin";
        public const string Manager = "Manager";
        public const string User = "User";
    }
}