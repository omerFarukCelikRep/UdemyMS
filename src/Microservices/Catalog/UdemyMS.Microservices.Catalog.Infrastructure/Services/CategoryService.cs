using MongoDB.Bson;
using MongoDB.Driver;
using System.Net;
using UdemyMS.Common.Core.Utilities.Results;
using UdemyMS.Microservices.Catalog.Entities.DbSets;
using UdemyMS.Microservices.Catalog.Entities.Dtos.Categories;
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
        var database = client.GetDatabase(databaseOptions.DatabaseName);

        _categories = database.GetCollection<Category>(CollectionName);
    }

    public async Task<Result<List<CategoryListDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var categoriesCursor = await _categories.FindAsync(c => true, cancellationToken: cancellationToken);
        var categories = categoriesCursor.ToEnumerable(cancellationToken)
                                         .Select(category => (CategoryListDto)category)
                                         .ToList();

        return Result<List<CategoryListDto>>.Success(categories, (int)HttpStatusCode.OK);
    }

    public async Task<Result<CategoryDetailsDto>> CreateAsync(CategoryCreateDto createCategory, CancellationToken cancellationToken = default)
    {
        Category category = createCategory;
        await _categories.InsertOneAsync(category, cancellationToken: cancellationToken);

        return Result<CategoryDetailsDto>.Success(category, (int)HttpStatusCode.OK);
    }

    public async Task<Result<CategoryDetailsDto>> GetByIdAsync(string categoryId, CancellationToken cancellationToken = default)
    {
        var categoryCursor = await _categories.FindAsync(c => c.Id == ObjectId.Parse(categoryId), cancellationToken: cancellationToken);
        var category = await categoryCursor.FirstOrDefaultAsync(cancellationToken);

        return Result<CategoryDetailsDto>.Success(category, (int)HttpStatusCode.OK);
    }
}