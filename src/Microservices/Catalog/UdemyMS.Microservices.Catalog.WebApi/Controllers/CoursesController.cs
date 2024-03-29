﻿using Microsoft.AspNetCore.Mvc;
using UdemyMS.Common.Web.Controllers;
using UdemyMS.Microservices.Catalog.Interfaces.Services;
using UdemyMS.Microservices.Catalog.WebApi.Models.Requests.Courses;

namespace UdemyMS.Microservices.Catalog.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CoursesController : BaseController
{
    private readonly ICourseService _courseService;
    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await _courseService.GetAllAsync(cancellationToken);

        return GetResult(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var result = await _courseService.GetByIdAsync(id, cancellationToken);

        return GetResult(result);
    }

    [HttpGet]
    [Route("/api/[controller]/GetAllByUserId/{userId}")]
    public async Task<IActionResult> GetAllByUserIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        var result = await _courseService.GetAllByUserIdAsync(userId, cancellationToken);

        return GetResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CourseCreateRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _courseService.CreateAsync(request, cancellationToken);

        return GetResult(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(CourseUpdateRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _courseService.UpdateAsync(request, cancellationToken);

        return GetResult(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var result = await _courseService.DeleteAsync(id, cancellationToken);

        return GetResult(result);
    }
}