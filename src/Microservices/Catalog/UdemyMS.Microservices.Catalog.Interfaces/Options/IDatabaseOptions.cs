namespace UdemyMS.Microservices.Catalog.Interfaces.Options;
public interface IDatabaseOptions
{
    string Name { get; set; }
    string Connection { get; set; }
}