using UdemyMS.Microservices.Basket.WebApi.Models.Dtos.Baskets;

namespace UdemyMS.Microservices.Basket.WebApi.Models.Requests;

public class BasketCreateRequest
{
    public string UserId { get; set; } = string.Empty;
    public string DiscountCode { get; set; } = string.Empty;
    public List<BasketItemCreateRequest> BasketItems { get; set; } = new();
    public decimal TotalPrice => BasketItems.Sum(x => x.Price);

    public static implicit operator BasketCreateDto(BasketCreateRequest request)
    {
        return new()
        {
            UserId = request.UserId,
            DiscountCode = request.DiscountCode,
            BasketItems = request.BasketItems.Select(x => (BasketItemCreateDto)x)
                                             .ToList(),
        };
    }
}