using System.ComponentModel.DataAnnotations;
using Evoflare.API.Constants;
using Evoflare.API.ViewModelSchemaFilters;
using Swashbuckle.AspNetCore.Annotations;

namespace Evoflare.API.ViewModels
{
    [SwaggerSchemaFilter(typeof(RegistrationFilter))]
    public class RegistrationViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Locale { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
    }

    public class ActivationViewModel
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Locale { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
    }

}