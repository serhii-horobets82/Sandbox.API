using System.ComponentModel.DataAnnotations;

namespace Evoflare.API.Core.Models
{
    public class Group
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [Display(Name = "Group name")]
        public string Name { get; set; }
    }
}