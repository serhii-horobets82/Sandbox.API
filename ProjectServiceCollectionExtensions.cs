namespace Evoflare.API
{
    using Boxed.Mapping;
    using Evoflare.API.Commands;
    using Evoflare.API.Core.Models;
    using Evoflare.API.Mappers;
    using Evoflare.API.Models;
    using Evoflare.API.Repositories;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods add project services.
    /// </summary>
    /// <remarks>
    /// AddSingleton - Only one instance is ever created and returned.
    /// AddScoped - A new instance is created and returned for each request/response cycle.
    /// AddTransient - A new instance is created and returned each time.
    /// </remarks>
    public static class ProjectServiceCollectionExtensions
    {
        public static IServiceCollection AddProjectCommands(this IServiceCollection services) =>
            services
                .AddSingleton<IDeleteCarCommand, DeleteCarCommand>()
                .AddSingleton<IGetCarCommand, GetCarCommand>()
                .AddSingleton<IGetCarPageCommand, GetCarPageCommand>()
                .AddSingleton<IPatchCarCommand, PatchCarCommand>()
                .AddSingleton<IPostCarCommand, PostCarCommand>()
                .AddSingleton<IPutCarCommand, PutCarCommand>();

        public static IServiceCollection AddProjectMappers(this IServiceCollection services) =>
            services
                .AddSingleton<IMapper<Employee, ViewModels.Employee>, EmployeeMapper>();

        public static IServiceCollection AddProjectRepositories(this IServiceCollection services) =>
            services
                .AddSingleton<ICarRepository, CarRepository>()
                .AddSingleton<IRepository<Employee>, Repository<Employee>>()
                .AddSingleton<IRepository<Installation>, Repository<Installation>>();

        public static IServiceCollection AddProjectServices(this IServiceCollection services) =>
            services;
        //.AddSingleton<IEmployeeService, EmployeeService>();
    }
}
