namespace Evoflare.API
{
    using System.Linq;
    using Boxed.AspNetCore;
    using Evoflare.API.Constants;
    using Evoflare.API.Filters;
    using Evoflare.API.Options;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;
    using Newtonsoft.Json;

    public static class MvcCoreBuilderExtensions
    {
        /// <summary>
        /// Add cross-origin resource sharing (CORS) services and configures named CORS policies. See
        /// https://docs.asp.net/en/latest/security/cors.html
        /// </summary>
        public static IMvcCoreBuilder AddCustomCors(this IMvcCoreBuilder builder) =>
            builder.AddCors(
                options =>
                {
                    // Create named CORS policies here which you can consume using application.UseCors("PolicyName")
                    // or a [EnableCors("PolicyName")] attribute on your controller or action.
                    options.AddPolicy(
                        CorsPolicyName.AllowAny,
                        x => x
                        //.AllowAnyOrigin()
                        .WithOrigins(
                            "http://localhost:8080",
                            "https://evoflare.azurewebsites.net",
                            "http://evoflare.azurewebsites.net",
                            "https://evoflare-web.herokuapp.com",
                            "https://evoflare-web-dev.herokuapp.com",
                            "https://evoflare-web-dev01.herokuapp.com",
                            "https://evoflare-web-dev02.herokuapp.com",
                            "https://evoflare-app-01.herokuapp.com/",
                            "https://evoflare-app-02.herokuapp.com/",
                            "https://evoflare-app.herokuapp.com/",
                            "http://evoflareappdev.z16.web.core.windows.net",
                            "https://evoflareappdev.z16.web.core.windows.net"
                        )
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
                });

        /// <summary>
        /// Adds customized JSON serializer settings.
        /// </summary>
        public static IMvcCoreBuilder AddCustomJsonOptions(
                this IMvcCoreBuilder builder,
                IHostingEnvironment hostingEnvironment) =>
            builder.AddJsonOptions(
                options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                    if (hostingEnvironment.IsDevelopment())
                    {
                        // Pretty print the JSON in development for easier debugging.
                        options.SerializerSettings.Formatting = Formatting.Indented;
                    }

                    // Parse dates as DateTimeOffset values by default. You should prefer using DateTimeOffset over
                    // DateTime everywhere. Not doing so can cause problems with time-zones.
                    options.SerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;

                    // Output enumeration values as strings in JSON.
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });

        public static IMvcCoreBuilder AddCustomMvcOptions(
                this IMvcCoreBuilder builder,
                IHostingEnvironment hostingEnvironment) =>
            builder.AddMvcOptions(
                options =>
                {
                    // Global exception handling
                    options.Filters.Add<ExceptionActionFilter>();

                    // Controls how controller actions cache content from the appsettings.json file.
                    var cacheProfileOptions = builder
                        .Services
                        .BuildServiceProvider()
                        .GetRequiredService<CacheProfileOptions>();
                    foreach (var keyValuePair in cacheProfileOptions)
                    {
                        options.CacheProfiles.Add(keyValuePair);
                    }

                    var jsonInputFormatterMediaTypes = options
                        .InputFormatters
                        .OfType<JsonInputFormatter>()
                        .First()
                        .SupportedMediaTypes;
                    var jsonOutputFormatterMediaTypes = options
                        .OutputFormatters
                        .OfType<JsonOutputFormatter>()
                        .First()
                        .SupportedMediaTypes;

                    // Add RESTful JSON media type (application/vnd.restful+json) to the JSON input and output formatters.
                    // See http://restfuljson.org/
                    jsonInputFormatterMediaTypes.Insert(0, ContentType.RestfulJson);
                    jsonOutputFormatterMediaTypes.Insert(0, ContentType.RestfulJson);

                    // Add Problem Details media type (application/problem+json) to the JSON input and output formatters.
                    // See https://tools.ietf.org/html/rfc7807
                    jsonOutputFormatterMediaTypes.Insert(0, ContentType.ProblemJson);

                    // Remove string and stream output formatters. These are not useful for an API serving JSON or XML.
                    options.OutputFormatters.RemoveType<StreamOutputFormatter>();
                    options.OutputFormatters.RemoveType<StringOutputFormatter>();

                    // Returns a 406 Not Acceptable if the MIME type in the Accept HTTP header is not valid.
                    options.ReturnHttpNotAcceptable = true;
                });
    }
}