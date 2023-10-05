using UdemyMS.Common.Core.Utilities.Results;
using UdemyMS.Microservices.Basket.WebApi.Models.Dtos.Baskets;

namespace UdemyMS.Microservices.Basket.WebApi.Services;

public interface IBasketService
{
    Task<Result<BasketGetDto>> GetAsync(string userId, CancellationToken cancellationToken = default);
    Task<Result> SaveOrUpdateAsync(BasketCreateDto basketCreateDto, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(string userId, CancellationToken cancellationToken = default);
}