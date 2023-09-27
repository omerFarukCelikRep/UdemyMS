using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UdemyMS.ExternalServices.IdentityServer.Data;

namespace UdemyMS.ExternalServices.IdentityServer.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEFCoreServices(configuration)
                .AddIdentity();

        return services;
    }

    private static IServiceCollection AddEFCoreServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddDbContext<IdentityServerDbContext>(options => options.UseSqlServer(configuration.GetConnectionString(IdentityServerDbContext.Connection)));
    }

    private static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityServerDbContext>()
                .AddDefaultTokenProviders();

        return services;
    }
}
