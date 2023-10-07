using Microsoft.AspNetCore.Mvc;
using UdemyMS.Common.Core.Utilities.Results;
using UdemyMS.Common.Web.Controllers;
using UdemyMS.Microservices.PhotoStock.WebApi.Models.Responses;
using UdemyMS.Microservices.PhotoStock.WebApi.Services;

namespace UdemyMS.Microservices.PhotoStock.WebApi.Controllers;
public class PhotosController : BaseController
{
    private readonly IPhotoService _photoService;
    public PhotosController(IPhotoService photoService)
    {
        _photoService = photoService;
    }

    [HttpPost]
    public async Task<IActionResult> SaveAsync(IFormFile file, CancellationToken cancellationToken = default)
    {
        var result = await _photoService.SaveAsync(file, cancellationToken);

        return GetResult(Result<PhotoSaveResponse>.Success(result.Data!, StatusCodes.Status200OK));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(string url, CancellationToken cancellationToken = default)
    {
        var result = await _photoService.DeleteAsync(url, cancellationToken);

        return GetResult(result);
    }
}