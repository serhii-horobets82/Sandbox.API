using Evoflare.API.Configuration;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Evoflare.API
{
    using Boxed.AspNetCore;
    using Evoflare.API.Constants;
    using Evoflare.API.Data;
    using Evoflare.API.Options;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;
    using System.Reflection;

    //using Evoflare.API.Data;

    public static partial class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds developer friendly error pages for the application which contain extra debug and exception information.
        /// Note: It is unsafe to use this in production.
        /// </summary>
        public static IApplicationBuilder UseDeveloperErrorPages(this IApplicationBuilder application) =>
            application
                // When a database error occurs, displays a detailed error page with full diagnostic information. It is
                // unsafe to use this in production. Uncomment this if using a database.
                // .UseDatabaseErrorPage(DatabaseErrorPageOptions.ShowAll);
                // When an error occurs, displays a detailed error page with full diagnostic information.
                // See http://docs.asp.net/en/latest/fundamentals/diagnostics.html
                .UseDeveloperExceptionPage();

        /// <summary>
        /// Uses the static files middleware to serve static files. Also adds the Cache-Control and Pragma HTTP
        /// headers. The cache duration is controlled from configuration.
        /// See http://andrewlock.net/adding-cache-control-headers-to-static-files-in-asp-net-core/.
        /// </summary>
        public static IApplicationBuilder UseStaticFilesWithCacheControl(this IApplicationBuilder application)
        {
            var cacheProfile = application
                .ApplicationServices
                .GetRequiredService<CacheProfileOptions>()
                .Where(x => string.Equals(x.Key, CacheProfileName.StaticFiles, StringComparison.Ordinal))
                .Select(x => x.Value)
                .SingleOrDefault();
            return application
                .UseStaticFiles(
                    new StaticFileOptions()
                    {
                        OnPrepareResponse = context =>
                        {
                            context.Context.ApplyCacheProfile(cacheProfile);
                        },
                    });
        }

        /// <summary>
        /// Creating new and\or seed database 
        /// </summary>
        /// <param name="application"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseDbSeed(this IApplicationBuilder application, IConfiguration configuration)
        {
            var appSettings = configuration.GetSection<AppSettings>();
            if (appSettings.SeedDatabase)
            {
                DbInitializer.Initialize(application.ApplicationServices, configuration);
            }

            return application;
        }


        public static IApplicationBuilder UseCustomSwaggerUI(this IApplicationBuilder application, Assembly assembly) =>
        application.UseSwaggerUI(
            options =>
            {
                options.InjectStylesheet("/swagger/custom.css");

                options.IndexStream = () => assembly.GetManifestResourceStream("Evoflare.API.wwwroot.swagger.index.html");

                // Set the Swagger UI browser document title.
                options.DocumentTitle = typeof(Startup)
                .Assembly
                .GetCustomAttribute<AssemblyProductAttribute>()
                .Product;
                // Set the Swagger UI to render at '/'.
                options.RoutePrefix = string.Empty;
                // Show the request duration in Swagger UI.
                options.DisplayRequestDuration();

                options.DocExpansion(DocExpansion.None);

                var provider = application.ApplicationServices.GetService<IApiVersionDescriptionProvider>();
                foreach (var apiVersionDescription in provider
                    .ApiVersionDescriptions
                    .OrderByDescending(x => x.ApiVersion))
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{apiVersionDescription.GroupName}/swagger.json",
                        $"Version {apiVersionDescription.ApiVersion}");
                }
            });
    }
}
