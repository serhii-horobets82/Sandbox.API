using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;
using Serilog;

namespace Evoflare.API.Core
{
    public class RouteConvention : IApplicationModelConvention
    {
        private const string DefaultRoutePrefix = "api/";
        private readonly string mainRoutePrefix;
        public RouteConvention(string mainRoutePrefix)
        {
            this.mainRoutePrefix = mainRoutePrefix;
        }
        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                var matchedSelectors = controller.Selectors.Where(x => x.AttributeRouteModel != null).ToList();
                if (matchedSelectors.Any())
                {
                    foreach (var selectorModel in matchedSelectors)
                    {
                        if (DefaultRoutePrefix != mainRoutePrefix)
                        {
                            selectorModel.AttributeRouteModel.Template = selectorModel.AttributeRouteModel.Template.Replace(DefaultRoutePrefix, mainRoutePrefix);
                        }
                    }
                }
            }
        }
    }
}