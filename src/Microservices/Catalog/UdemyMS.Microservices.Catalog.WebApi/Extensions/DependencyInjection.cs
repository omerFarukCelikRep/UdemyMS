using Microsoft.Extensions.Options;
using UdemyMS.Microservices.Catalog.Interfaces.Options;
using UdemyMS.Microservices.Catalog.WebApi.Options;

namespace UdemyMS.Microservices.Catalog.WebApi.Extensions;

public static class DependencyInjection
{
    private const string DatabaseSectionName = "Database";
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions(configuration);
        return services;
    }

    private static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseOptions>(configuration.GetSection(DatabaseSectionName));

        services.AddSingleton<IDatabaseOptions>(sp => sp.GetRequiredService<IOptions<DatabaseOptions>>().Value);

        return services;
    }
}