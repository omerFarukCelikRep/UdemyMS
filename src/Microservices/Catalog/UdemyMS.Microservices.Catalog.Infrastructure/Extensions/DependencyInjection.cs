using Microsoft.Extensions.DependencyInjection;
using UdemyMS.Microservices.Catalog.Infrastructure.Services;
using UdemyMS.Microservices.Catalog.Interfaces.Services;

namespace UdemyMS.Microservices.Catalog.Infrastructure.Extensions;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddServices();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICourseService, CourseService>();

        return services;
    }
}
