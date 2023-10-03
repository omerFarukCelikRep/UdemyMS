using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using UdemyMS.Common.Utilities;
using UdemyMS.Microservices.Catalog.Interfaces.Options;
using UdemyMS.Microservices.Catalog.WebApi.Options;

namespace UdemyMS.Microservices.Catalog.WebApi.Extensions;

public static class DependencyInjection
{
    private const string DatabaseSectionName = "Database";
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions(configuration)
                .AddCustomAuthentication(configuration)
                .AddControllers(opts => opts.Filters.Add(new AuthorizeFilter()));

        services.AddEndpointsApiExplorer()
                .AddSwaggerGen();
        return services;
    }

    private static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseOptions>(configuration.GetSection(DatabaseSectionName));

        services.AddSingleton<IDatabaseOptions>(sp => sp.GetRequiredService<IOptions<DatabaseOptions>>().Value);

        return services;
    }

    private static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opts =>
                {
                    opts.Authority = configuration[Constants.IdentityServer.ConfigName];
                    opts.Audience = Constants.IdentityServer.Resources.Catalog;
                    opts.RequireHttpsMetadata = false;
                });

        return services;
    }
}