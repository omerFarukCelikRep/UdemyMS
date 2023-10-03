using Microsoft.AspNetCore.Mvc;
using System.Net;
using UdemyMS.Common.Core.Utilities.Results;
using UdemyMS.Common.Web.Controllers;
using UdemyMS.Microservices.PhotoStock.WebApi.Models.Responses;

namespace UdemyMS.Microservices.PhotoStock.WebApi.Controllers;
public class PhotosController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> SaveAsync(IFormFile file, CancellationToken cancellationToken = default)
    {
        if (file is null || file.Length <= (long)default)
            return GetResult(Result.Error("Photo Not Found", (int)HttpStatusCode.BadRequest)); //TODO:Magic string

        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", file.FileName);

        using var stream = new FileStream(path, FileMode.Create);
        await file.CopyToAsync(stream, cancellationToken);

        var returnPath = $"photos/{file.FileName}";
        PhotoSaveResponse response = new() { Url = returnPath };

        return GetResult(Result<PhotoSaveResponse>.Success(response, (int)HttpStatusCode.OK));
    }

    [HttpDelete]
    public Task<IActionResult> DeleteAsync(string url, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
            return Task.FromCanceled<IActionResult>(cancellationToken);

        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", url);
        if (!System.IO.File.Exists(path))
            return Task.FromResult(GetResult(Result.Error("PhotoNotFound", (int)HttpStatusCode.NotFound)));

        System.IO.File.Delete(path);

        return Task.FromResult(GetResult(Result.Success((int)HttpStatusCode.NoContent)));
    }
}
