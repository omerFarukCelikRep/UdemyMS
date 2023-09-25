using UdemyMS.Microservices.Catalog.Entities.Dtos.Courses;

namespace UdemyMS.Microservices.Catalog.WebApi.Models.Requests.Courses;

public class CourseCreateRequest
{
    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public string Thumbnail { get; set; }

    public Guid UserId { get; set; }

    public CourseFeatureCreateRequest Feature { get; set; }

    public string CategoryId { get; set; }

    public static implicit operator CourseCreateDto(CourseCreateRequest request) => new()
    {
        Name = request.Name,
        CategoryId = request.CategoryId,
        Thumbnail = request.Thumbnail,
        Description = request.Description,
        Price = request.Price,
        UserId = request.UserId,
        Feature = new CourseFeatureCreateDto
        {
            Duration = request.Feature.Duration
        }
    };
}
