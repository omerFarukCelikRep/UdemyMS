using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using UdemyMS.Common.Utilities;

namespace UdemyMS.Microservices.PhotoStock.WebApi.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCustomAuthentication(configuration)
                .AddControllers(opts => opts.Filters.Add(new AuthorizeFilter()));

        services.AddEndpointsApiExplorer()
                .AddSwaggerGen();

        return services;
    }

    private static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opts =>
                {
                    opts.Authority = configuration[Constants.IdentityServer.ConfigName];
                    opts.Audience = Constants.IdentityServer.Resources.PhotoStock;
                    opts.RequireHttpsMetadata = false;
                });

        return services;
    }
}