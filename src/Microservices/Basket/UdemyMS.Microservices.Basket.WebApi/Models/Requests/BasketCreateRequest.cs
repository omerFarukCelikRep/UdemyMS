namespace UdemyMS.Microservices.Basket.WebApi.Models.Requests;

public class BasketCreateRequest
{
    public string UserId { get; set; } = string.Empty;
    public string DiscountCode { get; set; } = string.Empty;
    public List<BasketItemCreateRequest> BasketItems { get; set; } = new();
    public decimal TotalPrice => BasketItems.Sum(x => x.Price);
}