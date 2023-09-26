using MongoDB.Bson;
using MongoDB.Driver;
using System.Net;
using UdemyMS.Common.Core.Utilities.Results;
using UdemyMS.Microservices.Catalog.Entities.DbSets;
using UdemyMS.Microservices.Catalog.Entities.Dtos.Courses;
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

    public async Task<Result<List<CourseListDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var coursesCursor = await _courses.FindAsync(c => true, cancellationToken: cancellationToken);
        var courses = coursesCursor.ToEnumerable(cancellationToken)
                                         .Select(course => (CourseListDto)course)
                                         .ToList();

        foreach (var course in courses)
        {
            var categoryCursor = await _categories.FindAsync(c => c.Id == ObjectId.Parse(course.CategoryId), cancellationToken: cancellationToken);
            var category = await categoryCursor.FirstOrDefaultAsync(cancellationToken);

            course.Category = (CourseCategoryListDto)category;
        }

        return Result<List<CourseListDto>>.Success(courses, (int)HttpStatusCode.OK);
    }
}