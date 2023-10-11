using UdemyMS.Microservices.Discount.WebApi.Data.Contexts;
using UdemyMS.Microservices.Discount.WebApi.Services;

namespace UdemyMS.Microservices.Discount.WebApi.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DapperOptions>(opts => opts.Connection = configuration.GetConnectionString(DapperContext.ConnectionName))
                .AddScopedServices()
                .AddEndpointsApiExplorer()
                .AddSwaggerGen();

        return services;
    }

    private static IServiceCollection AddScopedServices(this IServiceCollection services)
    {
        services.AddScoped<IDapperContext, DapperContext>()
                .AddScoped<IDiscountService, DiscountService>();

        return services;
    }
}
