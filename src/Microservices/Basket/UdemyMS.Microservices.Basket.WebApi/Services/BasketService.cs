namespace UdemyMS.Microservices.Basket.WebApi.Services;

public class BasketService : IBasketService
{
    private readonly RedisService _redisService;
    public BasketService(RedisService redisService)
    {
        _redisService = redisService;
    }
}