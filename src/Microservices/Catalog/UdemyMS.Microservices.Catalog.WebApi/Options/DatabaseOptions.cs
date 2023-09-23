using System.ComponentModel.DataAnnotations;
using UdemyMS.Microservices.Catalog.Interfaces.Options;

namespace UdemyMS.Microservices.Catalog.WebApi.Options;

public record DatabaseOptions : IDatabaseOptions
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Connection { get; set; } = null!;
}
