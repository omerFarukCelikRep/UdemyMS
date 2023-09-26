using UdemyMS.Microservices.Catalog.Entities.DbSets;

namespace UdemyMS.Microservices.Catalog.Entities.Dtos.Courses;
public class CourseCreateDto
{
    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public string Thumbnail { get; set; }

    public Guid UserId { get; set; }

    public CourseFeatureCreateDto Feature { get; set; }

    public string CategoryId { get; set; }

    public static implicit operator Course(CourseCreateDto courseCreateDto) => new()
    {
        Name = courseCreateDto.Name,
        CreatedDate = DateTime.Now,
        Description = courseCreateDto.Description,
        CategoryId = courseCreateDto.CategoryId,
        Price = courseCreateDto.Price,
        Thumbnail = courseCreateDto.Thumbnail,
        UserId = courseCreateDto.UserId,
        Feature = new()
        {
            CreatedDate = DateTime.Now,
            Duration = courseCreateDto.Feature.Duration
        }
    };
}