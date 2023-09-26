﻿using UdemyMS.Common.Core.Utilities.Results;
using UdemyMS.Microservices.Catalog.Entities.Dtos.Courses;

namespace UdemyMS.Microservices.Catalog.Interfaces.Services;
public interface ICourseService
{
    Task<Result<CourseDetailsDto>> CreateAsync(CourseCreateDto courseCreate, CancellationToken cancellationToken = default);
}
