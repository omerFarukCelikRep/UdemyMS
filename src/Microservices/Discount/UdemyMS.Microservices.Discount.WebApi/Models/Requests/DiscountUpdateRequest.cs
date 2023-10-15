using UdemyMS.Microservices.Discount.WebApi.Models.Dtos;

namespace UdemyMS.Microservices.Discount.WebApi.Models.Requests;

public class DiscountUpdateRequest
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int Rate { get; set; }
    public string Code { get; set; }

    public static implicit operator DiscountUpdateDto(DiscountUpdateRequest request) => new()
    {
        Id = request.Id,
        UserId = request.UserId,
        Rate = request.Rate,
        Code = request.Code
    };
}