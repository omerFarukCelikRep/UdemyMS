﻿using MongoDB.Bson;
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
        var database = client.GetDatabase(databaseOptions.DatabaseName);

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

    public async Task<Result<CourseDetailsDto>> GetByIdAsync(string courseId, CancellationToken cancellationToken = default)
    {
        var courseCursor = await _courses.FindAsync(c => c.Id == ObjectId.Parse(courseId), cancellationToken: cancellationToken);
        var course = await courseCursor.FirstOrDefaultAsync(cancellationToken);
        if (course is null)
            return Result<CourseDetailsDto>.Error("Course Not Found", (int)HttpStatusCode.NotFound); //TODO:Magic string

        var courseDetails = (CourseDetailsDto)course;

        var categoryCursor = await _categories.FindAsync(c => c.Id == ObjectId.Parse(course.CategoryId), cancellationToken: cancellationToken);
        var category = await categoryCursor.FirstOrDefaultAsync(cancellationToken);
        courseDetails.Category = (CourseCategoryDetailsDto)category;

        return Result<CourseDetailsDto>.Success(courseDetails, (int)HttpStatusCode.OK);
    }

    public async Task<Result<List<CourseListDto>>> GetAllByUserIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        var coursesCursor = await _courses.FindAsync(c => c.UserId == Guid.Parse(userId), cancellationToken: cancellationToken);
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

    public async Task<Result<CourseDetailsDto>> CreateAsync(CourseCreateDto courseCreate, CancellationToken cancellationToken = default)
    {
        Course course = courseCreate;
        await _courses.InsertOneAsync(course, cancellationToken: cancellationToken);

        return Result<CourseDetailsDto>.Success(course, (int)HttpStatusCode.OK);
    }

    public async Task<Result> UpdateAsync(CourseUpdateDto courseUpdate, CancellationToken cancellationToken = default)
    {
        Course course = courseUpdate;

        var result = await _courses.ReplaceOneAsync(c => c.Id == course.Id, course, cancellationToken: cancellationToken);
        if (result == null)
            return Result.Error("Course Not Found", (int)HttpStatusCode.NotFound); //TODO:Magic string

        return Result.Success((int)HttpStatusCode.NoContent);
    }

    public async Task<Result> DeleteAsync(string courseId, CancellationToken cancellationToken = default)
    {
        var result = await _courses.DeleteOneAsync(c => c.Id == ObjectId.Parse(courseId), cancellationToken);
        if (result.DeletedCount > 0)
            return Result.Success((int)HttpStatusCode.NoContent);

        return Result.Error("Course Not Found", (int)HttpStatusCode.NotFound); //TODO:Magic string
    }
}