using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using UdemyMS.Common.Web.Extensions;
using UdemyMS.Microservices.Basket.WebApi.Options;
using UdemyMS.Microservices.Basket.WebApi.Services;

namespace UdemyMS.Microservices.Basket.WebApi.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        var requiredAuthorizationPolicy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();

        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove(Common.Utilities.Constants.Identity.Claim.Sub);

        services.AddRedisServices(configuration)
                .AddCommonWebServices()
                .AddScoped<IBasketService, BasketService>()
                .AddCustomAuthentication(configuration)
                .AddControllers(opts => opts.Filters.Add(new AuthorizeFilter(requiredAuthorizationPolicy)));

        services.AddEndpointsApiExplorer()
                .AddSwaggerGen();

        return services;
    }

    private static IServiceCollection AddRedisServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RedisOptions>(configuration.GetSection(Constants.Redis.ConfigName));

        services.AddSingleton(sp =>
        {
            var options = sp.GetRequiredService<IOptions<RedisOptions>>().Value;

            var redis = new RedisService(options.Host, options.Port);
            redis.Connect();

            return redis;
        });

        return services;
    }

    private static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opts =>
                {
                    opts.Authority = configuration[Common.Utilities.Constants.IdentityServer.ConfigName];
                    opts.Audience = Common.Utilities.Constants.IdentityServer.Resources.Basket;
                    opts.RequireHttpsMetadata = false;
                });

        return services;
    }
}
