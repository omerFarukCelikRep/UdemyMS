using UdemyMS.Common.Core.Utilities.Results;
using UdemyMS.Microservices.PhotoStock.WebApi.Models.Dtos;

namespace UdemyMS.Microservices.PhotoStock.WebApi.Services;

public interface IPhotoService
{
    Task<Result<PhotoSaveDto>> SaveAsync(IFormFile file, CancellationToken cancellationToken = default);
}