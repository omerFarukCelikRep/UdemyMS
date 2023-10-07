using UdemyMS.Common.Core.Utilities.Results;
using UdemyMS.Microservices.PhotoStock.WebApi.Models.Dtos;

namespace UdemyMS.Microservices.PhotoStock.WebApi.Services;

public class PhotoService : IPhotoService
{
    private const string RootPath = "wwwroot/photos";
    private const string PhotoPath = "photos";
    public async Task<Result<PhotoSaveDto>> SaveAsync(IFormFile file, CancellationToken cancellationToken = default)
    {
        if (file is null || file.Length <= (long)default)
            return Result<PhotoSaveDto>.Error("Photo Not Found", StatusCodes.Status404NotFound); //TODO:Magic string

        var path = Path.Combine(Directory.GetCurrentDirectory(), RootPath, file.FileName);

        using var stream = new FileStream(path, FileMode.Create);
        await file.CopyToAsync(stream, cancellationToken);

        var returnPath = $"{PhotoPath}/{file.FileName}";
        PhotoSaveDto savedPhoto = new() { Url = returnPath };

        return Result<PhotoSaveDto>.Success(savedPhoto, StatusCodes.Status200OK);
    }
}