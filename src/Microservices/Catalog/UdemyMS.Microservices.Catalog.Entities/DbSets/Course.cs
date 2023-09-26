using MongoDB.Bson.Serialization.Attributes;
using UdemyMS.Microservices.Catalog.Entities.Dtos.Courses;

namespace UdemyMS.Microservices.Catalog.Entities.DbSets;
public class Course : BaseEntity<ObjectId>
{
    public string Name { get; set; }

    public string Description { get; set; }

    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Price { get; set; }

    public string Thumbnail { get; set; }

    public Guid UserId { get; set; }

    public Feature Feature { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string CategoryId { get; set; }

    public static implicit operator CourseListDto(Course course) => new()
    {
        Id = course.Id.ToString(),
        Name = course.Name,
        Description = course.Description,
        Price = course.Price,
        Thumbnail = course.Thumbnail
    };

    public static implicit operator CourseDetailsDto(Course course) => new()
    {
        Id = course.Id.ToString(),
        Name = course.Name,
        Description = course.Description,
        Price = course.Price,
        Thumbnail = course.Thumbnail
    };
}