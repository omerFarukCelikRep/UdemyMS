using Microsoft.AspNetCore.Mvc;
using UdemyMS.Common.Web.Controllers;
using UdemyMS.Microservices.Catalog.Interfaces.Services;
using UdemyMS.Microservices.Catalog.WebApi.Models.Requests.Category;

namespace UdemyMS.Microservices.Catalog.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoriesController : BaseController
{
    private readonly ICategoryService _categoryService;
    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await _categoryService.GetAllAsync(cancellationToken);

        return GetResult(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var result = await _categoryService.GetByIdAsync(id, cancellationToken);

        return GetResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CategoryCreateRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _categoryService.CreateAsync(request, cancellationToken);

        return GetResult(result);
    }
}