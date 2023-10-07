using UdemyMS.Microservices.PhotoStock.WebApi.Models.Dtos;

namespace UdemyMS.Microservices.PhotoStock.WebApi.Models.Responses;

public class PhotoSaveResponse
{
    public string Url { get; set; } = null!;

    public static implicit operator PhotoSaveResponse(PhotoSaveDto photoSaveDto) => new()
    {
        Url = photoSaveDto.Url
    };
}