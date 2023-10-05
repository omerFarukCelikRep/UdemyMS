using System.Text.Json;
using UdemyMS.Common.Core.Utilities.Results;
using UdemyMS.Microservices.Basket.WebApi.Models.Dtos.Baskets;

namespace UdemyMS.Microservices.Basket.WebApi.Services;

public class BasketService : IBasketService
{
    private readonly RedisService _redisService;
    public BasketService(RedisService redisService)
    {
        _redisService = redisService;
    }

    public async Task<Result<BasketGetDto>> GetAsync(string userId, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var basketAsString = await _redisService.GetDatabase()
                                                .StringGetAsync(userId);
        if (string.IsNullOrWhiteSpace(basketAsString))
            return Result<BasketGetDto>.Error("Basket Not Found", StatusCodes.Status404NotFound); //TODO:Magic string

        var basket = JsonSerializer.Deserialize<BasketGetDto>(basketAsString!);

        return Result<BasketGetDto>.Success(basket, StatusCodes.Status200OK);
    }
}