using Evoflare.API.ViewModelSchemaFilters;
using Swashbuckle.AspNetCore.Annotations;

namespace Evoflare.API.ViewModels
{
    [SwaggerSchemaFilter(typeof(RegistrationFilter))]
    public class RegistrationViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}