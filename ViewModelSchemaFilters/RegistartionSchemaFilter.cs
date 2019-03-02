using Evoflare.API.ViewModels;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Evoflare.API.ViewModelSchemaFilters
{
    public class RegistrationFilter : ISchemaFilter
    {
        public void Apply(Schema model, SchemaFilterContext context)
        {
            var user = new RegistrationViewModel
            {
                Email = "user1@evoflare.com",
                Password = "qwerty",
                FirstName = "Typical",
                LastName = "User"
            };
            model.Default = user;
            model.Example = user;
        }
    }
}