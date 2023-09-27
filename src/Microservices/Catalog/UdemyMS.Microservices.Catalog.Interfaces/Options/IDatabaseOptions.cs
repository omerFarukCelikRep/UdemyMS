namespace UdemyMS.Microservices.Catalog.Interfaces.Options;
public interface IDatabaseOptions
{
    string DatabaseName { get; set; }
    string Connection { get; set; }
}