using System.ComponentModel.DataAnnotations;
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

        [Display(Name = "Profile picture url")]
        public string PictureUrl { get; set; }
    }
}