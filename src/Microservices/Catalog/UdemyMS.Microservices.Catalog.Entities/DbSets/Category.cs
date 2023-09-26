using UdemyMS.Microservices.Catalog.Entities.Dtos.Categories;
using UdemyMS.Microservices.Catalog.Entities.Dtos.Courses;

namespace UdemyMS.Microservices.Catalog.Entities.DbSets;
public class Category : BaseEntity<ObjectId>
{
    public string Name { get; set; } = string.Empty;

    public static implicit operator CategoryListDto(Category category) => new()
    {
        Id = category.Id.ToString(),
        Name = category.Name
    };

    public static implicit operator CategoryDetailsDto(Category category) => new()
    {
        Id = category.Id.ToString(),
        Name = category.Name
    };

    public static implicit operator CourseCategoryListDto(Category category) => new()
    {
        Name = category.Name
    };

    public static implicit operator CourseCategoryDetailsDto(Category category) => new()
    {
        Name = category.Name
    };
}