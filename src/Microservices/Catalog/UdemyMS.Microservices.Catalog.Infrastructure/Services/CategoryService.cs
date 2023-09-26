using MongoDB.Driver;
using UdemyMS.Microservices.Catalog.Entities.DbSets;
using UdemyMS.Microservices.Catalog.Interfaces.Options;
using UdemyMS.Microservices.Catalog.Interfaces.Services;

namespace UdemyMS.Microservices.Catalog.Infrastructure.Services;
public class CategoryService : ICategoryService
{
    private const string CollectionName = "Categories";

    private readonly IMongoCollection<Category> _categories;
    public CategoryService(IDatabaseOptions databaseOptions)
    {
        var client = new MongoClient(databaseOptions.Connection);
        var database = client.GetDatabase(databaseOptions.Name);

        _categories = database.GetCollection<Category>(CollectionName);
    }
}