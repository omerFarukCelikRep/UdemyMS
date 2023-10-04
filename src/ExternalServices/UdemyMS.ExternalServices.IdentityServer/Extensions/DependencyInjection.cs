using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UdemyMS.Common.Utilities;
using UdemyMS.ExternalServices.IdentityServer.Data.Context;
using UdemyMS.ExternalServices.IdentityServer.Data.DbSets;
using UdemyMS.ExternalServices.IdentityServer.Validators;

namespace UdemyMS.ExternalServices.IdentityServer.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEFCoreServices(configuration)
                .AddCustomIdentity()
                .AddCustomIdentityServer()
                .AddLocalApiAuthentication()
                .AddControllers();

        services.AddEndpointsApiExplorer()
                        .AddSwaggerGen();


        return services;
    }

    private static IServiceCollection AddEFCoreServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddDbContext<IdentityServerDbContext>(options => options.UseSqlServer(configuration.GetConnectionString(IdentityServerDbContext.Connection)));
    }

    private static IServiceCollection AddCustomIdentity(this IServiceCollection services)
    {
        services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityServerDbContext>()
                .AddDefaultTokenProviders();

        return services;
    }

    private static IServiceCollection AddCustomIdentityServer(this IServiceCollection services)
    {
        services.AddIdentityServer()
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiResources(Config.ApiResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients)
            .AddAspNetIdentity<AppUser>()
            .AddResourceOwnerValidator<IdentityResourceOwnerPasswordValidator>();

        services.AddLogging(options => options.AddFilter(Constants.IdentityServer.LogFilterName, LogLevel.Debug));

        return services;
    }
}
