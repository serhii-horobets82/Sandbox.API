using System.ComponentModel.DataAnnotations;
using Evoflare.API.Constants;
using Microsoft.AspNetCore.Identity;

namespace Evoflare.API.Auth.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {LastName}";

        public long? FacebookId { get; set; }

        [Display(Name = "User age")]
        public int Age { get; set; }

        [Display(Name = "User gender code")]
        public Gender Gender { get; set; }
    }
}