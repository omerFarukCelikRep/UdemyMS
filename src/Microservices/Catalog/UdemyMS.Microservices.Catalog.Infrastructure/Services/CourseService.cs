using MongoDB.Driver;
using UdemyMS.Microservices.Catalog.Entities.DbSets;
using UdemyMS.Microservices.Catalog.Interfaces.Options;
using UdemyMS.Microservices.Catalog.Interfaces.Services;

namespace UdemyMS.Microservices.Catalog.Infrastructure.Services;
public class CourseService : ICourseService
{

    private const string CourseCollectionName = "Courses";
    private const string CategoryCollectionName = "Categories";

    private readonly IMongoCollection<Course> _courses;
    private readonly IMongoCollection<Category> _categories;
    public CourseService(IDatabaseOptions databaseOptions)
    {
        var client = new MongoClient(databaseOptions.Connection);
        var database = client.GetDatabase(databaseOptions.Name);

        _courses = database.GetCollection<Course>(CourseCollectionName);
        _categories = database.GetCollection<Category>(CategoryCollectionName);
    }
}