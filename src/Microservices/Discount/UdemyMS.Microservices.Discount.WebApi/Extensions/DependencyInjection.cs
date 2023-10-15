using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.IdentityModel.Tokens.Jwt;
using UdemyMS.Common.Web.Extensions;
using UdemyMS.Microservices.Discount.WebApi.Data.Contexts;
using UdemyMS.Microservices.Discount.WebApi.Services;

namespace UdemyMS.Microservices.Discount.WebApi.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        var requiredAuthorizationPolicy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();


        services.Configure<DapperOptions>(opts => opts.Connection = configuration.GetConnectionString(DapperContext.ConnectionName)!)
                .AddCommonWebServices()
                .AddCustomAuthentication(configuration)
                .AddScopedServices()
                .AddControllers(opts => opts.Filters.Add(new AuthorizeFilter(requiredAuthorizationPolicy)));

        services.AddEndpointsApiExplorer()
                .AddSwaggerGen();

        return services;
    }

    private static IServiceCollection AddScopedServices(this IServiceCollection services)
    {
        services.AddScoped<IDapperContext, DapperContext>()
                .AddScoped<IDiscountService, DiscountService>();

        return services;
    }

    private static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove(Common.Utilities.Constants.Identity.Claim.Sub);

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