using UdemyMS.Microservices.Catalog.Entities.Dtos.Courses;

namespace UdemyMS.Microservices.Catalog.WebApi.Models.Requests.Courses;

public class CourseUpdateRequest
{
    public string Id { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public string Thumbnail { get; set; }

    public Guid UserId { get; set; }

    public CourseFeatureUpdateRequest Feature { get; set; }

    public string CategoryId { get; set; }

    public static implicit operator CourseUpdateDto(CourseUpdateRequest request) => new()
    {
        Id = request.Id,
        Name = request.Name,
        Description = request.Description,
        Price = request.Price,
        Thumbnail = request.Thumbnail,
        UserId = request.UserId,
        CategoryId = request.CategoryId,
        Feature = new CourseFeatureUpdateDto
        {
            Duration = request.Feature.Duration
        }
    };
}