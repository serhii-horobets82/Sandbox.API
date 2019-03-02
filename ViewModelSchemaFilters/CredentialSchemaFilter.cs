using Evoflare.API.ViewModels;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Evoflare.API.ViewModelSchemaFilters
{
    public class CredentialSchemaFilter : ISchemaFilter
    {
        public void Apply(Schema model, SchemaFilterContext context)
        {
            var user = new CredentialsViewModel()
            {
                UserName = "admin@evoflare.com",
                Password = "qwerty"
            };
            model.Default = user;
            model.Example = user;
        }
    }
}