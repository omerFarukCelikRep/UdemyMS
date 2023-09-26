using UdemyMS.Microservices.Catalog.Entities.DbSets;

namespace UdemyMS.Microservices.Catalog.Entities.Dtos.Courses;
public class CourseUpdateDto
{
    public string Id { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public string Thumbnail { get; set; }

    public Guid UserId { get; set; }

    public CourseFeatureUpdateDto Feature { get; set; }

    public string CategoryId { get; set; }

    public static implicit operator Course(CourseUpdateDto courseUpdateDto) => new()
    {
        Id = ObjectId.Parse(courseUpdateDto.Id),
        Name = courseUpdateDto.Name,
        CreatedDate = DateTime.Now,
        Description = courseUpdateDto.Description,
        CategoryId = courseUpdateDto.CategoryId,
        Price = courseUpdateDto.Price,
        Thumbnail = courseUpdateDto.Thumbnail,
        UserId = courseUpdateDto.UserId,
        Feature = new()
        {
            CreatedDate = DateTime.Now,
            Duration = courseUpdateDto.Feature.Duration
        }
    };
}