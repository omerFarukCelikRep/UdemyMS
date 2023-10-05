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

    public async Task<Result> DeleteAsync(string userId, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var status = await _redisService.GetDatabase()
                                        .KeyDeleteAsync(userId);

        return status
            ? Result.Success(StatusCodes.Status204NoContent)
            : Result.Error("Basket Not Found", StatusCodes.Status404NotFound); //TODO:Magic string
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

    public async Task<Result> SaveOrUpdateAsync(BasketCreateDto basketCreateDto, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var status = await _redisService.GetDatabase()
                                        .StringSetAsync(basketCreateDto.UserId, JsonSerializer.Serialize(basketCreateDto));

        return status
            ? Result.Success(StatusCodes.Status200OK)
            : Result.Error("Basket could not updated or saved", StatusCodes.Status400BadRequest); //TODO:Magic string
    }
}