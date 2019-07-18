using Microsoft.AspNetCore.Identity;

namespace Evoflare.API.Auth.Models
{
    public class ApplicationRole : IdentityRole<string>
    {
        public ApplicationRole() { }
        public ApplicationRole(string name) : this()
        {
            this.Name = name;
        }
        public ApplicationRole(string name, string accessGroup) : this()
        {
            this.Name = name;
            this.AccessGroup = accessGroup;
        }
        public string AccessGroup { get; set; }
    }
}