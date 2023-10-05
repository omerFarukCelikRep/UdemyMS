namespace UdemyMS.Microservices.Basket.WebApi.Models.Dtos.Baskets;

public class BasketItemGetDto
{
    public string CourseId { get; set; } = string.Empty;
    public string CourseName { get; set; } = string.Empty;
    public decimal Price { get; set; }
}