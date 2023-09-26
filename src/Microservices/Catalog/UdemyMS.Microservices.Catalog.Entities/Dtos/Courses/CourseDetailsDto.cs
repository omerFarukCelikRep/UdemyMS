﻿namespace UdemyMS.Microservices.Catalog.Entities.Dtos.Courses;
public class CourseDetailsDto
{
    public string Id { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }
    public decimal Price { get; set; }

    public string Thumbnail { get; set; }

    public Guid UserId { get; set; }

    public CourseFeatureListDto Feature { get; set; }

    public string CategoryId { get; set; }
    public CourseCategoryDetailsDto Category { get; set; }
}
