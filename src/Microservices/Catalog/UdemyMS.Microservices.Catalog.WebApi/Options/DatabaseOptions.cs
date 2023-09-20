using System.ComponentModel.DataAnnotations;

namespace UdemyMS.Microservices.Catalog.WebApi.Options;

public record DatabaseOptions
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Connection { get; set; }
}
