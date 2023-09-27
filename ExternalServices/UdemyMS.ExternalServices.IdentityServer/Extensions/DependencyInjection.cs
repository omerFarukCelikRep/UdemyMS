using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UdemyMS.ExternalServices.IdentityServer.Data;

namespace UdemyMS.ExternalServices.IdentityServer.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEFCoreServices(configuration)
                .AddCustomIdentity()
                .AddCustomIdentityServer();

        return services;
    }

    private static IServiceCollection AddEFCoreServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddDbContext<IdentityServerDbContext>(options => options.UseSqlServer(configuration.GetConnectionString(IdentityServerDbContext.Connection)));
    }

    private static IServiceCollection AddCustomIdentity(this IServiceCollection services)
    {
        services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityServerDbContext>()
                .AddDefaultTokenProviders();

        return services;
    }

    private static IServiceCollection AddCustomIdentityServer(this IServiceCollection services)
    {
        services.AddIdentityServer()
            .AddInMemoryClients(new Client[]
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = { "https://localhost:5002/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },
                    FrontChannelLogoutUri = "https://localhost:5002/signout-oidc",
                    AllowedScopes = { "openid", "profile", "email", "phone" }
                }
            })
            .AddInMemoryIdentityResources(new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Phone(),
            })
            .AddAspNetIdentity<IdentityUser>();

        return services;
    }
}
