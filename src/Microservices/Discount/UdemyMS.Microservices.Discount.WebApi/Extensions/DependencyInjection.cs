using UdemyMS.Microservices.Discount.WebApi.Data.Contexts;

namespace UdemyMS.Microservices.Discount.WebApi.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DapperOptions>(opts => opts.Connection = configuration.GetConnectionString(DapperContext.ConnectionName))
                .AddEndpointsApiExplorer()
                .AddSwaggerGen();

        return services;
    }
}
