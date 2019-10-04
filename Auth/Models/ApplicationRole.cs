using System.Collections.Generic;
using Evoflare.API.Core.Permissions;
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
        public AccessFlag DefaultPermission { get; set; }

        public string PolicyName { get; set; }

        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}