namespace UdemyMS.Microservices.Basket.WebApi.Models.Requests;

public class BasketItemCreateRequest
{
    public string CourseId { get; set; }
    public string CourseName { get; set; }
    public decimal Price { get; set; }
}
