using UdemyMS.Common.Core.Utilities.Results;
using UdemyMS.Microservices.Catalog.Entities.Dtos.Categories;

namespace UdemyMS.Microservices.Catalog.Interfaces.Services;
public interface ICategoryService
{
    Task<Result<CategoryDetailsDto>> CreateAsync(CategoryCreateDto createCategory, CancellationToken cancellationToken = default);
    Task<Result<List<CategoryListDto>>> GetAllAsync(CancellationToken cancellationToken = default);
}