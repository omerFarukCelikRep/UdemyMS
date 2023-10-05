namespace UdemyMS.Microservices.Basket.WebApi.Models.Dtos.Baskets;

public class BasketGetDto
{
    public string UserId { get; set; } = string.Empty;
    public string DiscountCode { get; set; } = string.Empty;
    public List<BasketItemGetDto> BasketItems { get; set; } = new();
    public decimal TotalPrice => BasketItems.Sum(x => x.Price);
}
