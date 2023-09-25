using Microsoft.AspNetCore.Mvc;
using UdemyMS.Common.Web.Controllers;
using UdemyMS.Microservices.Catalog.Interfaces.Services;

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
    [Route("/api/[contoller]/GetAllByUserId/{userId}")]
    public async Task<IActionResult> GetAllByUserIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        var result = await _courseService.GetAllByUserIdAsync(userId, cancellationToken);

        return GetResult(result);
    }
}