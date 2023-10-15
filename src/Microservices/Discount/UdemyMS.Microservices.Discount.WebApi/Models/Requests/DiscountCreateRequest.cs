using UdemyMS.Microservices.Discount.WebApi.Models.Dtos;

namespace UdemyMS.Microservices.Discount.WebApi.Models.Requests;

public class DiscountCreateRequest
{
    public string UserId { get; set; } = string.Empty;
    public int Rate { get; set; }
    public string Code { get; set; } = string.Empty;

    public static implicit operator DiscountCreateDto(DiscountCreateRequest request) => new()
    {
        UserId = request.UserId,
        Rate = request.Rate,
        Code = request.Code
    };
}