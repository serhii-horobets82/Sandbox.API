using System.ComponentModel.DataAnnotations;

namespace Evoflare.API.Auth.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string IdentityId { get; set; }
        public ApplicationUser Identity { get; set; }

        [Display(Name = "User location")]
        public string Location { get; set; }

        [Display(Name = "User locale")]
        public string Locale { get; set; }

        [Display(Name = "Profile picture url")]
        public string PictureUrl { get; set; }
    }
}