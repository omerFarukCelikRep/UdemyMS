using UdemyMS.Microservices.Catalog.Entities.DbSets;

namespace UdemyMS.Microservices.Catalog.Entities.Dtos.Categories;
public record CategoryCreateDto
{
    public string Name { get; set; } = string.Empty;

    public static implicit operator Category(CategoryCreateDto categoryCreateDto)
    {
        return new Category
        {
            Name = categoryCreateDto.Name,
            CreatedDate = DateTime.Now
        };
    }
}