using UdemyMS.Microservices.Catalog.Entities.Dtos.Categories;

namespace UdemyMS.Microservices.Catalog.WebApi.Models.Requests.Category;

public class CategoryCreateRequest
{
    public string Name { get; set; } = string.Empty;

    public static implicit operator CategoryCreateDto(CategoryCreateRequest request) => new()
    {
        Name = request.Name
    };
}