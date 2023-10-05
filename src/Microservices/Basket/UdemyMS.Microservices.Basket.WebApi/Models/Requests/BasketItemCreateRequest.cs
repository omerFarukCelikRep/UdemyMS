using UdemyMS.Microservices.Basket.WebApi.Models.Dtos.Baskets;

namespace UdemyMS.Microservices.Basket.WebApi.Models.Requests;

public class BasketItemCreateRequest
{
    public string CourseId { get; set; } = string.Empty;
    public string CourseName { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public static implicit operator BasketItemCreateDto(BasketItemCreateRequest request)
    {
        return new()
        {
            CourseId = request.CourseId,
            CourseName = request.CourseName,
            Price = request.Price
        };
    }
}