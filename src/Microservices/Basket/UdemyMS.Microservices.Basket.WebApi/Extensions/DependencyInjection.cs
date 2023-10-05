using Microsoft.Extensions.Options;
using UdemyMS.Common.Web.Extensions;
using UdemyMS.Microservices.Basket.WebApi.Options;
using UdemyMS.Microservices.Basket.WebApi.Services;

namespace UdemyMS.Microservices.Basket.WebApi.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRedisServices(configuration)
                .AddCommonWebServices()
                .AddScoped<IBasketService, BasketService>()
                .AddControllers();

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
}
