using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using Boxed.AspNetCore;
using Boxed.AspNetCore.Swagger;
using Boxed.AspNetCore.Swagger.OperationFilters;
using Boxed.AspNetCore.Swagger.SchemaFilters;
using CorrelationId;
using Evoflare.API.Auth;
using Evoflare.API.Auth.Models;
using Evoflare.API.Configuration;
using Evoflare.API.Models;
using Evoflare.API.OperationFilters;
using Evoflare.API.Options;
using Evoflare.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace Evoflare.API
{
    /// <summary>
    ///     <see cref="IServiceCollection" /> extension methods which extend ASP.NET Core services.
    /// </summary>
    public static class CustomServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabaseContexts(this IServiceCollection services,
            IConfiguration configuration, string connectionName = "DefaultConnection")
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName();

            // services.AddDbContext<EvoflareDBContext>(options => options.UseSqlServer(
            //     configuration.GetConnectionString(connectionName),
            //     sqlServerOptions => sqlServerOptions.CommandTimeout(300)));

            services.AddDbContext<EvoflareDbContext>(options => options.UseSqlServer(
            configuration.GetConnectionString(connectionName),
            sqlServerOptions => sqlServerOptions.CommandTimeout(300)));

            // services.AddDbContext<TechnicalEvaluationContext>(
            //     options => options.UseSqlServer(configuration.GetConnectionString(connectionName),
            //         // start migration
            //         optionsBuilder => optionsBuilder.MigrationsAssembly(assemblyName.Name))
            // );
            return services;
        }

        //AddCustomAuthentication

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services,
            IConfiguration configuration)
        {
            var appSettings = configuration.GetSection<AppSettings>();
            var secretKey = Encoding.ASCII.GetBytes(appSettings.Secret);

            var jwtAppSettingOptions = configuration.GetSection(nameof(JwtIssuerOptions));
            var signingKey = new SymmetricSecurityKey(secretKey);

            services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();

            // Register the ConfigurationBuilder instance of FacebookAuthSettings
            services.Configure<FacebookAuthSettings>(configuration.GetSection(nameof(FacebookAuthSettings)));
            services.Configure<GithubAuthSettings>(configuration.GetSection(nameof(GithubAuthSettings)));


            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            // api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser",
                    policy => policy.RequireClaim(Constants.JwtClaimIdentifiers.Rol, Constants.JwtClaims.ApiAccess));
            });

            // add identity
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    // configure identity options
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 6;

                    options.SignIn.RequireConfirmedEmail = true;
                    // Set for correct userManager.GetUserAsync execution
                    options.ClaimsIdentity.UserIdClaimType = Constants.JwtClaimIdentifiers.Id;
                })
                //.AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<EvoflareDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }


        public static IServiceCollection AddCorrelationIdFluent(this IServiceCollection services)
        {
            services.AddCorrelationId();
            return services;
        }

        /// <summary>
        ///     Configures caching for the application. Registers the <see cref="IDistributedCache" /> and
        ///     <see cref="IMemoryCache" /> types with the services collection or IoC container. The
        ///     <see cref="IDistributedCache" /> is intended to be used in cloud hosted scenarios where there is a shared
        ///     cache, which is shared between multiple instances of the application. Use the <see cref="IMemoryCache" />
        ///     otherwise.
        /// </summary>
        public static IServiceCollection AddCustomCaching(this IServiceCollection services)
        {
            return services
                // Adds IMemoryCache which is a simple in-memory cache.
                .AddMemoryCache()
                // Adds IDistributedCache which is a distributed cache shared between multiple servers. This adds a
                // default implementation of IDistributedCache which is not distributed. See below:
                .AddDistributedMemoryCache();
        }
        // Uncomment the following line to use the Redis implementation of IDistributedCache. This will
        // override any previously registered IDistributedCache service.
        // Redis is a very fast cache provider and the recommended distributed cache provider.
        // .AddDistributedRedisCache(options => { ... });
        // Uncomment the following line to use the Microsoft SQL Server implementation of IDistributedCache.
        // Note that this would require setting up the session state database.
        // Redis is the preferred cache implementation but you can use SQL Server if you don't have an alternative.
        // .AddSqlServerCache(
        //     x =>
        //     {
        //         x.ConnectionString = "Server=.;Database=ASPNET5SessionState;Trusted_Connection=True;";
        //         x.SchemaName = "dbo";
        //         x.TableName = "Sessions";
        //     });

        /// <summary>
        ///     Configures the settings by binding the contents of the appsettings.json file to the specified Plain Old CLR
        ///     Objects (POCO) and adding <see cref="IOptions{TOptions}" /> objects to the services collection.
        /// </summary>
        public static IServiceCollection AddCustomOptions(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            return services
                // ConfigureSingleton registers IOptions<T> and also T as a singleton to the services collection.
                .ConfigureAndValidateSingleton<ApplicationOptions>(configuration)
                .ConfigureAndValidateSingleton<CompressionOptions>(
                    configuration.GetSection(nameof(ApplicationOptions.Compression)))
                .ConfigureAndValidateSingleton<ForwardedHeadersOptions>(
                    configuration.GetSection(nameof(ApplicationOptions.ForwardedHeaders)))
                .ConfigureAndValidateSingleton<CacheProfileOptions>(
                    configuration.GetSection(nameof(ApplicationOptions.CacheProfiles)))
                .ConfigureAndValidateSingleton<AppSettings>(
                    configuration.GetSection(nameof(AppSettings)));
        }

        /// <summary>
        ///     Adds dynamic response compression to enable GZIP compression of responses. This is turned off for HTTPS
        ///     requests by default to avoid the BREACH security vulnerability.
        /// </summary>
        public static IServiceCollection AddCustomResponseCompression(this IServiceCollection services)
        {
            return services
                .AddResponseCompression(
                    options =>
                    {
                        // Add additional MIME types (other than the built in defaults) to enable GZIP compression for.
                        var customMimeTypes = services
                                                  .BuildServiceProvider()
                                                  .GetRequiredService<CompressionOptions>()
                                                  .MimeTypes ?? Enumerable.Empty<string>();
                        options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(customMimeTypes);
                    })
                .Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
        }

        /// <summary>
        ///     Add custom routing settings which determines how URL's are generated.
        /// </summary>
        public static IServiceCollection AddCustomRouting(this IServiceCollection services)
        {
            return services.AddRouting(
                options =>
                {
                    // All generated URL's should be lower-case.
                    options.LowercaseUrls = true;
                });
        }

        /// <summary>
        ///     Adds the Strict-Transport-Security HTTP header to responses. This HTTP header is only relevant if you are
        ///     using TLS. It ensures that content is loaded over HTTPS and refuses to connect in case of certificate
        ///     errors and warnings.
        ///     See https://developer.mozilla.org/en-US/docs/Web/Security/HTTP_strict_transport_security and
        ///     http://www.troyhunt.com/2015/06/understanding-http-strict-transport.html
        ///     Note: Including subdomains and a minimum maxage of 18 weeks is required for preloading.
        ///     Note: You can refer to the following article to clear the HSTS cache in your browser:
        ///     http://classically.me/blogs/how-clear-hsts-settings-major-browsers
        /// </summary>
        public static IServiceCollection AddCustomStrictTransportSecurity(this IServiceCollection services)
        {
            return services
                .AddHsts(
                    options =>
                    {
                        // Preload the HSTS HTTP header for better security. See https://hstspreload.org/
                        // options.IncludeSubDomains = true;
                        // options.MaxAge = TimeSpan.FromSeconds(31536000); // 1 Year
                        // options.Preload = true;
                    });
        }

        public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services)
        {
            return services
                .AddHealthChecks()
                // Add health checks for external dependencies here. See https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks
                .Services;
        }

        public static IServiceCollection AddCustomApiVersioning(this IServiceCollection services)
        {
            return services.AddApiVersioning(
                options =>
                {
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ReportApiVersions = true;
                });
        }

        /// <summary>
        ///     Adds Swagger services and configures the Swagger services.
        /// </summary>
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(
                options =>
                {
                    var assembly = typeof(Startup).Assembly;
                    var assemblyProduct = assembly.GetCustomAttribute<AssemblyProductAttribute>().Product;
                    var assemblyVersion = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version;
                    var assemblyDescription = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description;

                    options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme",
                        Name = "Authorization",
                        In = "header",
                        Type = "apiKey"
                    });

                    options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                    {
                        {"Bearer", Enumerable.Empty<string>()}
                    });


                    options.DescribeAllEnumsAsStrings();
                    options.DescribeAllParametersInCamelCase();
                    options.DescribeStringEnumsInCamelCase();
                    options.EnableAnnotations();

                    // Add the XML comment file for this assembly, so its contents can be displayed.
                    options.IncludeXmlCommentsIfExists(assembly);

                    options.OperationFilter<ApiVersionOperationFilter>();
                    options.OperationFilter<CorrelationIdOperationFilter>();
                    options.OperationFilter<ForbiddenResponseOperationFilter>();
                    options.OperationFilter<UnauthorizedResponseOperationFilter>();

                    // Show an example model for JsonPatchDocument<T>.
                    options.SchemaFilter<JsonPatchDocumentSchemaFilter>();

                    var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
                    foreach (var apiVersionDescription in provider.ApiVersionDescriptions)
                    {
                        var info = new Info
                        {
                            Title = assemblyProduct,
                            Description = apiVersionDescription.IsDeprecated
                                ? $"{assemblyDescription} This API version has been deprecated."
                                : assemblyDescription,
                            Version = assemblyVersion,
                            Contact = new Contact()
                            {
                                Name = "Evoflare team",
                                Email = "mail.evoflare@gmail.com"
                            }
                        };
                        options.SwaggerDoc(apiVersionDescription.GroupName, info);
                    }
                });
        }
    }
}