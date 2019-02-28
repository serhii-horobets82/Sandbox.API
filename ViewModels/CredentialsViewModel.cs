using Evoflare.API.ViewModelSchemaFilters;
using Swashbuckle.AspNetCore.Annotations;

namespace Evoflare.API.ViewModels
{
    [SwaggerSchemaFilter(typeof(CredentialSchemaFilter))]
    public class CredentialsViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
