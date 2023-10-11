using UdemyMS.Common.Core.Utilities.Results;
using UdemyMS.Microservices.Discount.WebApi.Models.Dtos;

namespace UdemyMS.Microservices.Discount.WebApi.Services;

public interface IDiscountService
{
    Task<Result<List<DiscountListDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<DiscountDetailsDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result> SaveAsync(DiscountCreateDto discount, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(DiscountUpdateDto discountUpdate, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<DiscountDetailsDto>> GetByCodeAndUserIdAsync(string code, int userId, CancellationToken cancellationToken = default);
}