namespace UdemyMS.Microservices.Basket.WebApi.Models.Dtos.Baskets;

public class BasketCreateDto
{
    public string UserId { get; set; } = string.Empty;
    public string DiscountCode { get; set; } = string.Empty;
    public List<BasketItemCreateDto> BasketItems { get; set; } = new();
    public decimal TotalPrice => BasketItems.Sum(x => x.Price);
}